--
-- PostgreSQL database dump
--

\restrict lYp2tP6thRP8dJnSpN7KK4XTy238DDmfbWA3OXHdznfViT7Vabah1w9PbrMYNaX

-- Dumped from database version 18.0
-- Dumped by pg_dump version 18.0

-- Started on 2025-12-21 15:50:43

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 894 (class 1247 OID 17629)
-- Name: cinsiyet_tipi; Type: TYPE; Schema: public; Owner: postgres
--

CREATE TYPE public.cinsiyet_tipi AS ENUM (
    'ERKEK',
    'KADIN'
);


ALTER TYPE public.cinsiyet_tipi OWNER TO postgres;

--
-- TOC entry 247 (class 1255 OID 17850)
-- Name: check_doktor_limit(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.check_doktor_limit() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
DECLARE
    doktor_sayisi INTEGER;
BEGIN
    -- Eklenmek istenen hastanedeki mevcut doktor sayısını bul
    SELECT COUNT(*) INTO doktor_sayisi FROM Doktorlar WHERE hastane_id = NEW.hastane_id;
    
    -- Eğer sayı 10 veya daha fazlaysa HATA FIRLAT ve işlemi durdur
    IF doktor_sayisi >= 10 THEN
        RAISE EXCEPTION 'Bu hastane için doktor kotası (10) dolmuştur. Daha fazla kayıt yapılamaz!';
    END IF;

    RETURN NEW;
END;
$$;


ALTER FUNCTION public.check_doktor_limit() OWNER TO postgres;

--
-- TOC entry 246 (class 1255 OID 17798)
-- Name: func_hasta_log(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.func_hasta_log() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- SENARYO 1: Hasta Silinirse (DELETE)
    IF (TG_OP = 'DELETE') THEN
        INSERT INTO Hastalar_Log (
            hasta_id, islem_tipi, eski_ad, eski_soyad, eski_tel
        )
        VALUES (
            OLD.hasta_id, 'SILME', OLD.hasta_ad, OLD.hasta_soyad, OLD.telefon_no
        );
        RETURN OLD;

    -- SENARYO 2: Bilgiler Güncellenirse (UPDATE)
    ELSIF (TG_OP = 'UPDATE') THEN
        INSERT INTO Hastalar_Log (
            hasta_id, islem_tipi, eski_ad, eski_soyad, eski_tel
        )
        VALUES (
            NEW.hasta_id, 'GUNCELLEME', OLD.hasta_ad, OLD.hasta_soyad, OLD.telefon_no
        );
        RETURN NEW;

    -- SENARYO 3: Yeni Hasta Eklenirse (INSERT)
    ELSIF (TG_OP = 'INSERT') THEN
        INSERT INTO Hastalar_Log (hasta_id, islem_tipi)
        VALUES (NEW.hasta_id, 'KAYIT');
        RETURN NEW;
    END IF;
    RETURN NULL;
END;
$$;


ALTER FUNCTION public.func_hasta_log() OWNER TO postgres;

--
-- TOC entry 266 (class 1255 OID 17786)
-- Name: func_randevu_log(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.func_randevu_log() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- SENARYO 1: Veri Silindiyse (DELETE)
    IF (TG_OP = 'DELETE') THEN
        INSERT INTO Randevu_Loglari (
            randevu_id, hasta_id, doktor_id, islem_tipi, eski_durum
        )
        VALUES (
            OLD.randevu_id, OLD.hasta_id, OLD.doktor_id, 'SILME', OLD.randevu_durum
        );
        RETURN OLD;
        
    -- SENARYO 2: Durum Güncellendiyse (UPDATE)
    ELSIF (TG_OP = 'UPDATE') THEN
        -- Sadece randevu durumu değiştiyse log tut
        IF OLD.randevu_durum IS DISTINCT FROM NEW.randevu_durum THEN
            INSERT INTO Randevu_Loglari (
                randevu_id, hasta_id, doktor_id, islem_tipi, eski_durum, yeni_durum
            )
            VALUES (
                NEW.randevu_id, NEW.hasta_id, NEW.doktor_id, 'DURUM_DEGISTI', 
                OLD.randevu_durum, NEW.randevu_durum
            );
        END IF;
        RETURN NEW;
    END IF;
    RETURN NULL;
END;
$$;


ALTER FUNCTION public.func_randevu_log() OWNER TO postgres;

--
-- TOC entry 265 (class 1255 OID 17774)
-- Name: ilaclog(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.ilaclog() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
	IF OLD.stok_adet IS DISTINCT FROM NEW.stok_adet THEN
		INSERT INTO Ilaclar_log(
			ilac_id, 
            eski_stok, 
            yeni_stok, 
            degistiren_kisi, 
            islem_tipi
		)
		VALUES(
			NEW.ilac_id, 
            OLD.stok_adet, 
            NEW.stok_adet, 
            current_user, -- Veritabanı kullanıcısı
            TG_OP         -- İşlem Tipi (UPDATE)
		);
	END IF;

	RETURN NEW;
END;
$$;


ALTER FUNCTION public.ilaclog() OWNER TO postgres;

--
-- TOC entry 244 (class 1255 OID 17755)
-- Name: ilacstokkontrol(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.ilacstokkontrol() RETURNS trigger
    LANGUAGE plpgsql
    AS $$ 
BEGIN 
	IF NEW.stok_adet < 0 THEN
		RAISE EXCEPTION 'Hata : İlaç stok adedi negatif olamaz!Girilen Değer : %',NEW.stok_adet;
	END IF;
	RETURN NEW;
END;
	$$;


ALTER FUNCTION public.ilacstokkontrol() OWNER TO postgres;

--
-- TOC entry 264 (class 1255 OID 17760)
-- Name: sp_doktorekle(character varying, character varying, integer, character varying, text, character varying, text, public.cinsiyet_tipi, uuid, integer); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.sp_doktorekle(IN p_ad character varying, IN p_soyad character varying, IN p_brans_id integer, IN p_tc_hash character varying, IN p_tc_sifreli text, IN p_sifre_hash character varying, IN p_sifre_sifreli text, IN p_cinsiyet public.cinsiyet_tipi, IN p_sekreter_id uuid, IN p_hastane_id integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
    --  KONTROL -- 
    IF EXISTS (
	SELECT 1 FROM Doktorlar 
	WHERE tc_hash = p_tc_hash) 
	THEN
        RAISE EXCEPTION 'Bu T.C. Kimlik Numarası ile kayıtlı bir doktor zaten mevcut!';
    END IF;

    -- KAYIT -- 
    INSERT INTO Doktorlar (
        doktor_ad, doktor_soyad, brans_id, 
        tc_hash, tc_sifreli, sifre_hash, sifre_sifreli, 
        cinsiyet, sekreter_id, hastane_id
    )
    VALUES (
        p_ad, p_soyad, p_brans_id, 
        p_tc_hash, p_tc_sifreli, p_sifre_hash, p_sifre_sifreli, 
        p_cinsiyet, p_sekreter_id, p_hastane_id
    );
END;
$$;


ALTER PROCEDURE public.sp_doktorekle(IN p_ad character varying, IN p_soyad character varying, IN p_brans_id integer, IN p_tc_hash character varying, IN p_tc_sifreli text, IN p_sifre_hash character varying, IN p_sifre_sifreli text, IN p_cinsiyet public.cinsiyet_tipi, IN p_sekreter_id uuid, IN p_hastane_id integer) OWNER TO postgres;

--
-- TOC entry 254 (class 1255 OID 17761)
-- Name: sp_hastakayit(character varying, character varying, character varying, text, character, character varying, text, public.cinsiyet_tipi); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.sp_hastakayit(IN p_ad character varying, IN p_soyad character varying, IN p_tc_hash character varying, IN p_tc_sifreli text, IN p_tel character, IN p_sifre_hash character varying, IN p_sifre_sifreli text, IN p_cinsiyet public.cinsiyet_tipi)
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- 1. KONTROL BLOĞU (GÜNCELLENDİ)
    -- TC Hash'i VEYA Telefon Numarası eşleşen var mı?
    IF EXISTS (
        SELECT 1 FROM Hastalar 
        WHERE tc_hash = p_tc_hash OR telefon_no = p_tel
    ) THEN
        -- Hata mesajını da durumu açıklayacak şekilde güncelledik
        RAISE EXCEPTION 'Bu T.C. Kimlik Numarası veya Telefon Numarası ile kayıtlı bir hasta zaten mevcut!';
    END IF;

    -- 2. KAYIT BLOĞU (AYNI KALDI)
    INSERT INTO Hastalar (
        hasta_ad, hasta_soyad, tc_hash, tc_sifreli, 
        telefon_no, sifre_hash, sifre_sifreli, cinsiyet
    )
    VALUES (
        p_ad, p_soyad, p_tc_hash, p_tc_sifreli, 
        p_tel, p_sifre_hash, p_sifre_sifreli, p_cinsiyet
    );
END;
$$;


ALTER PROCEDURE public.sp_hastakayit(IN p_ad character varying, IN p_soyad character varying, IN p_tc_hash character varying, IN p_tc_sifreli text, IN p_tel character, IN p_sifre_hash character varying, IN p_sifre_sifreli text, IN p_cinsiyet public.cinsiyet_tipi) OWNER TO postgres;

--
-- TOC entry 248 (class 1255 OID 17872)
-- Name: sp_randevuolustur(timestamp without time zone, character varying, integer, uuid, boolean); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.sp_randevuolustur(IN p_tarih timestamp without time zone, IN p_saat character varying, IN p_brans_id integer, IN p_doktor_id uuid, IN p_durum boolean)
    LANGUAGE plpgsql
    AS $$
BEGIN
    INSERT INTO Randevular (
        randevu_tarih, 
        randevu_saat, 
        brans_id, 
        doktor_id, 
        randevu_durum, 
        hasta_id
    ) VALUES (
        p_tarih::DATE,      -- Timestamp'i DATE'e çeviriyoruz
        p_saat::TIME,       -- String'i TIME'a çeviriyoruz (Örn: '14:30' -> 14:30:00)
        p_brans_id, 
        p_doktor_id, 
        p_durum, 
        NULL                -- Başlangıçta hasta yok, NULL
    );
END;
$$;


ALTER PROCEDURE public.sp_randevuolustur(IN p_tarih timestamp without time zone, IN p_saat character varying, IN p_brans_id integer, IN p_doktor_id uuid, IN p_durum boolean) OWNER TO postgres;

--
-- TOC entry 245 (class 1255 OID 17848)
-- Name: sp_randevusil(integer); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.sp_randevusil(IN p_randevu_id integer)
    LANGUAGE plpgsql
    AS $$
DECLARE
    v_durum BOOLEAN;
BEGIN
    -- 1. Önce randevunun durumunu (Dolu/Boş) çekiyoruz
    SELECT randevu_durum INTO v_durum 
    FROM Randevular 
    WHERE randevu_id = p_randevu_id;

    -- 2. Kontrol Ediyoruz
    IF v_durum = TRUE THEN
        -- Eğer randevu DOLU ise hata fırlat (C# bunu yakalayacak)
        RAISE EXCEPTION 'Bu randevu dolu (Hasta Var)! Silemezsiniz.';
    ELSE
        -- Eğer randevu BOŞ ise slotu komple sil
        DELETE FROM Randevular WHERE randevu_id = p_randevu_id;
    END IF;
END;
$$;


ALTER PROCEDURE public.sp_randevusil(IN p_randevu_id integer) OWNER TO postgres;

--
-- TOC entry 249 (class 1255 OID 17895)
-- Name: sp_receteolustur(text, integer, integer); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.sp_receteolustur(IN p_tani text, IN p_randevuid integer, INOUT p_receteid integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- Randevu tablosundan doktor ve hasta bilgilerini alarak Reçete tablosuna ekle
    INSERT INTO receteler (tani_teshis, recete_tarih, randevu_id, hasta_id, doktor_id)
    SELECT 
        p_tani,
        CURRENT_DATE,
        r.randevu_id, 
        r.hasta_id, 
        r.doktor_id
    FROM "Randevular" r  -- Tablo adın Tbl_Randevular ise burayı düzelt
    WHERE r.randevu_id = p_randevuid
    RETURNING recete_id INTO p_receteid; -- Oluşan ID'yi değişkene ata
END;
$$;


ALTER PROCEDURE public.sp_receteolustur(IN p_tani text, IN p_randevuid integer, INOUT p_receteid integer) OWNER TO postgres;

--
-- TOC entry 263 (class 1255 OID 17759)
-- Name: sp_sekreterekle(character varying, character varying, character varying, text, character varying, text, public.cinsiyet_tipi, integer); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.sp_sekreterekle(IN p_ad character varying, IN p_soyad character varying, IN p_tc_hash character varying, IN p_tc_sifreli text, IN p_sifre_hash character varying, IN p_sifre_sifreli text, IN p_cinsiyet public.cinsiyet_tipi, IN p_hastane_id integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
-- Kontrol -- 
	IF EXISTS(
	SELECT 1 FROM Sekreterler
	WHERE tc_hash = p_tc_hash
	)
	THEN
		RAISE EXCEPTION 'Bu T.C. Kimlik Numarası ile kayıtlı bir sekreter zaten mevcut!';
	END IF;
-- Kayıt Ekle -- 
INSERT INTO Sekreterler (
        sekreter_ad, sekreter_soyad, tc_hash, tc_sifreli, 
        sifre_hash, sifre_sifreli, cinsiyet, hastane_id
    )
    VALUES (
        p_ad, p_soyad, p_tc_hash, p_tc_sifreli, 
        p_sifre_hash, p_sifre_sifreli, p_cinsiyet, p_hastane_id
    );
END;
$$;


ALTER PROCEDURE public.sp_sekreterekle(IN p_ad character varying, IN p_soyad character varying, IN p_tc_hash character varying, IN p_tc_sifreli text, IN p_sifre_hash character varying, IN p_sifre_sifreli text, IN p_cinsiyet public.cinsiyet_tipi, IN p_hastane_id integer) OWNER TO postgres;

--
-- TOC entry 243 (class 1255 OID 17611)
-- Name: trg_hastane_log_func(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.trg_hastane_log_func() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    IF OLD.telefon IS DISTINCT FROM NEW.telefon THEN
        INSERT INTO Hastaneler_Log (hastane_id, eski_telefon, yeni_telefon, degistiren_kisi)
        VALUES (OLD.hastane_id, OLD.telefon, NEW.telefon, current_user);
    END IF;
    
    RETURN NEW;
END;
$$;


ALTER FUNCTION public.trg_hastane_log_func() OWNER TO postgres;

--
-- TOC entry 261 (class 1255 OID 17845)
-- Name: trg_randevukontrol(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.trg_randevukontrol() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- Eğer aynı doktorun, aynı tarihinde ve saatinde kayıt varsa HATA FIRLAT
    IF EXISTS (SELECT 1 FROM Randevular 
               WHERE doktor_id = NEW.doktor_id 
                 AND randevu_tarih = NEW.randevu_tarih 
                 AND randevu_saat = NEW.randevu_saat) THEN
        RAISE EXCEPTION 'Bu doktora belirtilen tarih ve saatte zaten bir randevu slotu açılmış!';
    END IF;
    RETURN NEW;
END;
$$;


ALTER FUNCTION public.trg_randevukontrol() OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 225 (class 1259 OID 17669)
-- Name: branslar; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.branslar (
    brans_id integer NOT NULL,
    brans_ad character varying(100) NOT NULL,
    hastane_id integer
);


ALTER TABLE public.branslar OWNER TO postgres;

--
-- TOC entry 224 (class 1259 OID 17668)
-- Name: branslar_brans_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.branslar_brans_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.branslar_brans_id_seq OWNER TO postgres;

--
-- TOC entry 5080 (class 0 OID 0)
-- Dependencies: 224
-- Name: branslar_brans_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.branslar_brans_id_seq OWNED BY public.branslar.brans_id;


--
-- TOC entry 226 (class 1259 OID 17679)
-- Name: doktorlar; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.doktorlar (
    doktor_id uuid DEFAULT gen_random_uuid() NOT NULL,
    doktor_ad character varying(30) NOT NULL,
    doktor_soyad character varying(30) NOT NULL,
    brans_id integer,
    tc_hash character varying(64) NOT NULL,
    tc_sifreli text NOT NULL,
    sifre_hash character varying(64) NOT NULL,
    sifre_sifreli text NOT NULL,
    cinsiyet public.cinsiyet_tipi,
    sekreter_id uuid,
    hastane_id integer
);


ALTER TABLE public.doktorlar OWNER TO postgres;

--
-- TOC entry 231 (class 1259 OID 17741)
-- Name: duyurular; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.duyurular (
    duyurular_id integer NOT NULL,
    duyurular_text character varying(200),
    sekreter_id uuid
);


ALTER TABLE public.duyurular OWNER TO postgres;

--
-- TOC entry 230 (class 1259 OID 17740)
-- Name: duyurular_duyurular_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.duyurular_duyurular_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.duyurular_duyurular_id_seq OWNER TO postgres;

--
-- TOC entry 5083 (class 0 OID 0)
-- Dependencies: 230
-- Name: duyurular_duyurular_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.duyurular_duyurular_id_seq OWNED BY public.duyurular.duyurular_id;


--
-- TOC entry 222 (class 1259 OID 17633)
-- Name: hastalar; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.hastalar (
    hasta_id uuid DEFAULT gen_random_uuid() NOT NULL,
    hasta_ad character varying(30) NOT NULL,
    hasta_soyad character varying(30) NOT NULL,
    tc_hash character varying(64) NOT NULL,
    tc_sifreli text NOT NULL,
    telefon_no character(11),
    sifre_hash character varying(64) NOT NULL,
    sifre_sifreli text NOT NULL,
    cinsiyet public.cinsiyet_tipi
);


ALTER TABLE public.hastalar OWNER TO postgres;

--
-- TOC entry 237 (class 1259 OID 17789)
-- Name: hastalar_log; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.hastalar_log (
    log_id integer NOT NULL,
    hasta_id uuid,
    islem_tipi character varying(20),
    degistiren_kisi character varying(50) DEFAULT CURRENT_USER,
    degisim_tarihi timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    eski_ad character varying(30),
    eski_soyad character varying(30),
    eski_tel character(11)
);


ALTER TABLE public.hastalar_log OWNER TO postgres;

--
-- TOC entry 236 (class 1259 OID 17788)
-- Name: hastalar_log_log_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.hastalar_log_log_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.hastalar_log_log_id_seq OWNER TO postgres;

--
-- TOC entry 5085 (class 0 OID 0)
-- Dependencies: 236
-- Name: hastalar_log_log_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.hastalar_log_log_id_seq OWNED BY public.hastalar_log.log_id;


--
-- TOC entry 219 (class 1259 OID 17593)
-- Name: hastaneler; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.hastaneler (
    hastane_id integer NOT NULL,
    hastane_ad character varying(75) NOT NULL,
    sehir character varying(30) NOT NULL,
    telefon character(11)
);


ALTER TABLE public.hastaneler OWNER TO postgres;

--
-- TOC entry 242 (class 1259 OID 17849)
-- Name: hastaneler_hastane_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.hastaneler ALTER COLUMN hastane_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.hastaneler_hastane_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 221 (class 1259 OID 17614)
-- Name: ilaclar; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.ilaclar (
    ilac_id integer NOT NULL,
    ilac_ad character varying(50) NOT NULL,
    stok_adet integer NOT NULL,
    hastane_id integer
);


ALTER TABLE public.ilaclar OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 17613)
-- Name: ilaclar_ilac_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.ilaclar_ilac_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.ilaclar_ilac_id_seq OWNER TO postgres;

--
-- TOC entry 5087 (class 0 OID 0)
-- Dependencies: 220
-- Name: ilaclar_ilac_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.ilaclar_ilac_id_seq OWNED BY public.ilaclar.ilac_id;


--
-- TOC entry 233 (class 1259 OID 17764)
-- Name: ilaclar_log; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.ilaclar_log (
    log_id integer NOT NULL,
    ilac_id integer NOT NULL,
    eski_stok integer,
    yeni_stok integer,
    degistiren_kisi character varying(50) DEFAULT CURRENT_USER,
    degisim_tarihi timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    islem_tipi character varying(20)
);


ALTER TABLE public.ilaclar_log OWNER TO postgres;

--
-- TOC entry 232 (class 1259 OID 17763)
-- Name: ilaclar_log_log_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.ilaclar_log_log_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.ilaclar_log_log_id_seq OWNER TO postgres;

--
-- TOC entry 5088 (class 0 OID 0)
-- Dependencies: 232
-- Name: ilaclar_log_log_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.ilaclar_log_log_id_seq OWNED BY public.ilaclar_log.log_id;


--
-- TOC entry 235 (class 1259 OID 17777)
-- Name: randevu_loglari; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.randevu_loglari (
    log_id integer NOT NULL,
    randevu_id integer,
    hasta_id uuid,
    doktor_id uuid,
    islem_tipi character varying(20),
    degistiren_kisi character varying(50) DEFAULT CURRENT_USER,
    islem_tarihi timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    eski_durum boolean,
    yeni_durum boolean
);


ALTER TABLE public.randevu_loglari OWNER TO postgres;

--
-- TOC entry 234 (class 1259 OID 17776)
-- Name: randevu_loglari_log_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.randevu_loglari_log_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.randevu_loglari_log_id_seq OWNER TO postgres;

--
-- TOC entry 5089 (class 0 OID 0)
-- Dependencies: 234
-- Name: randevu_loglari_log_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.randevu_loglari_log_id_seq OWNED BY public.randevu_loglari.log_id;


--
-- TOC entry 229 (class 1259 OID 17711)
-- Name: randevular; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.randevular (
    randevu_id integer NOT NULL,
    randevu_tarih date NOT NULL,
    randevu_saat time without time zone NOT NULL,
    brans_id integer NOT NULL,
    doktor_id uuid,
    randevu_durum boolean DEFAULT false,
    hasta_id uuid,
    hasta_sikayet character varying(200)
);


ALTER TABLE public.randevular OWNER TO postgres;

--
-- TOC entry 228 (class 1259 OID 17710)
-- Name: randevular_brans_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.randevular_brans_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.randevular_brans_id_seq OWNER TO postgres;

--
-- TOC entry 5090 (class 0 OID 0)
-- Dependencies: 228
-- Name: randevular_brans_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.randevular_brans_id_seq OWNED BY public.randevular.brans_id;


--
-- TOC entry 227 (class 1259 OID 17709)
-- Name: randevular_randevu_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.randevular_randevu_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.randevular_randevu_id_seq OWNER TO postgres;

--
-- TOC entry 5091 (class 0 OID 0)
-- Dependencies: 227
-- Name: randevular_randevu_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.randevular_randevu_id_seq OWNED BY public.randevular.randevu_id;


--
-- TOC entry 241 (class 1259 OID 17825)
-- Name: recetedetay; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.recetedetay (
    detay_id integer NOT NULL,
    kullanim_sekli character varying(100),
    adet integer DEFAULT 1,
    recete_id integer,
    ilac_id integer
);


ALTER TABLE public.recetedetay OWNER TO postgres;

--
-- TOC entry 240 (class 1259 OID 17824)
-- Name: recetedetay_detay_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.recetedetay_detay_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.recetedetay_detay_id_seq OWNER TO postgres;

--
-- TOC entry 5093 (class 0 OID 0)
-- Dependencies: 240
-- Name: recetedetay_detay_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.recetedetay_detay_id_seq OWNED BY public.recetedetay.detay_id;


--
-- TOC entry 239 (class 1259 OID 17801)
-- Name: receteler; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.receteler (
    recete_id integer NOT NULL,
    tani_teshis character varying(200),
    recete_tarih date DEFAULT CURRENT_DATE,
    randevu_id integer,
    hasta_id uuid,
    doktor_id uuid
);


ALTER TABLE public.receteler OWNER TO postgres;

--
-- TOC entry 238 (class 1259 OID 17800)
-- Name: receteler_recete_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.receteler_recete_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.receteler_recete_id_seq OWNER TO postgres;

--
-- TOC entry 5095 (class 0 OID 0)
-- Dependencies: 238
-- Name: receteler_recete_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.receteler_recete_id_seq OWNED BY public.receteler.recete_id;


--
-- TOC entry 223 (class 1259 OID 17648)
-- Name: sekreterler; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.sekreterler (
    sekreter_id uuid DEFAULT gen_random_uuid() NOT NULL,
    sekreter_ad character varying(30) NOT NULL,
    sekreter_soyad character varying(30) NOT NULL,
    tc_hash character varying(64) NOT NULL,
    tc_sifreli text NOT NULL,
    sifre_hash character varying(64) NOT NULL,
    sifre_sifreli text NOT NULL,
    cinsiyet public.cinsiyet_tipi,
    hastane_id integer
);


ALTER TABLE public.sekreterler OWNER TO postgres;

--
-- TOC entry 4832 (class 2604 OID 17672)
-- Name: branslar brans_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.branslar ALTER COLUMN brans_id SET DEFAULT nextval('public.branslar_brans_id_seq'::regclass);


--
-- TOC entry 4837 (class 2604 OID 17744)
-- Name: duyurular duyurular_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.duyurular ALTER COLUMN duyurular_id SET DEFAULT nextval('public.duyurular_duyurular_id_seq'::regclass);


--
-- TOC entry 4844 (class 2604 OID 17792)
-- Name: hastalar_log log_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.hastalar_log ALTER COLUMN log_id SET DEFAULT nextval('public.hastalar_log_log_id_seq'::regclass);


--
-- TOC entry 4829 (class 2604 OID 17617)
-- Name: ilaclar ilac_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ilaclar ALTER COLUMN ilac_id SET DEFAULT nextval('public.ilaclar_ilac_id_seq'::regclass);


--
-- TOC entry 4838 (class 2604 OID 17767)
-- Name: ilaclar_log log_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ilaclar_log ALTER COLUMN log_id SET DEFAULT nextval('public.ilaclar_log_log_id_seq'::regclass);


--
-- TOC entry 4841 (class 2604 OID 17780)
-- Name: randevu_loglari log_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.randevu_loglari ALTER COLUMN log_id SET DEFAULT nextval('public.randevu_loglari_log_id_seq'::regclass);


--
-- TOC entry 4834 (class 2604 OID 17714)
-- Name: randevular randevu_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.randevular ALTER COLUMN randevu_id SET DEFAULT nextval('public.randevular_randevu_id_seq'::regclass);


--
-- TOC entry 4835 (class 2604 OID 17715)
-- Name: randevular brans_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.randevular ALTER COLUMN brans_id SET DEFAULT nextval('public.randevular_brans_id_seq'::regclass);


--
-- TOC entry 4849 (class 2604 OID 17828)
-- Name: recetedetay detay_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.recetedetay ALTER COLUMN detay_id SET DEFAULT nextval('public.recetedetay_detay_id_seq'::regclass);


--
-- TOC entry 4847 (class 2604 OID 17804)
-- Name: receteler recete_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.receteler ALTER COLUMN recete_id SET DEFAULT nextval('public.receteler_recete_id_seq'::regclass);


--
-- TOC entry 5056 (class 0 OID 17669)
-- Dependencies: 225
-- Data for Name: branslar; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.branslar VALUES (1, 'Dahiliye', 1);
INSERT INTO public.branslar VALUES (2, 'Göz Hastalıkları', 1);
INSERT INTO public.branslar VALUES (3, 'Kulak Burun Boğaz', 1);
INSERT INTO public.branslar VALUES (4, 'Genel Cerrahi', 1);
INSERT INTO public.branslar VALUES (5, 'Nöroloji', 1);
INSERT INTO public.branslar VALUES (6, 'Kardiyoloji', 1);
INSERT INTO public.branslar VALUES (7, 'Ortopedi', 1);
INSERT INTO public.branslar VALUES (8, 'Cildiye', 1);
INSERT INTO public.branslar VALUES (9, 'Psikiyatri', 1);
INSERT INTO public.branslar VALUES (10, 'Üroloji', 1);
INSERT INTO public.branslar VALUES (11, 'Dahiliye', 2);
INSERT INTO public.branslar VALUES (12, 'Göz Hastalıkları', 2);
INSERT INTO public.branslar VALUES (13, 'Kulak Burun Boğaz', 2);
INSERT INTO public.branslar VALUES (14, 'Genel Cerrahi', 2);
INSERT INTO public.branslar VALUES (15, 'Nöroloji', 2);
INSERT INTO public.branslar VALUES (16, 'Kardiyoloji', 2);
INSERT INTO public.branslar VALUES (17, 'Ortopedi', 2);
INSERT INTO public.branslar VALUES (18, 'Cildiye', 2);
INSERT INTO public.branslar VALUES (19, 'Psikiyatri', 2);
INSERT INTO public.branslar VALUES (20, 'Üroloji', 2);
INSERT INTO public.branslar VALUES (21, 'Dahiliye', 3);
INSERT INTO public.branslar VALUES (22, 'Göz Hastalıkları', 3);
INSERT INTO public.branslar VALUES (23, 'Kulak Burun Boğaz', 3);
INSERT INTO public.branslar VALUES (24, 'Genel Cerrahi', 3);
INSERT INTO public.branslar VALUES (25, 'Nöroloji', 3);
INSERT INTO public.branslar VALUES (26, 'Kardiyoloji', 3);
INSERT INTO public.branslar VALUES (27, 'Ortopedi', 3);
INSERT INTO public.branslar VALUES (28, 'Cildiye', 3);
INSERT INTO public.branslar VALUES (29, 'Psikiyatri', 3);
INSERT INTO public.branslar VALUES (30, 'Üroloji', 3);
INSERT INTO public.branslar VALUES (31, 'Dahiliye', 4);
INSERT INTO public.branslar VALUES (32, 'Göz Hastalıkları', 4);
INSERT INTO public.branslar VALUES (33, 'Kulak Burun Boğaz', 4);
INSERT INTO public.branslar VALUES (34, 'Genel Cerrahi', 4);
INSERT INTO public.branslar VALUES (35, 'Nöroloji', 4);
INSERT INTO public.branslar VALUES (36, 'Kardiyoloji', 4);
INSERT INTO public.branslar VALUES (37, 'Ortopedi', 4);
INSERT INTO public.branslar VALUES (38, 'Cildiye', 4);
INSERT INTO public.branslar VALUES (39, 'Psikiyatri', 4);
INSERT INTO public.branslar VALUES (40, 'Üroloji', 4);
INSERT INTO public.branslar VALUES (41, 'Dahiliye', 5);
INSERT INTO public.branslar VALUES (42, 'Göz Hastalıkları', 5);
INSERT INTO public.branslar VALUES (43, 'Kulak Burun Boğaz', 5);
INSERT INTO public.branslar VALUES (44, 'Genel Cerrahi', 5);
INSERT INTO public.branslar VALUES (45, 'Nöroloji', 5);
INSERT INTO public.branslar VALUES (46, 'Kardiyoloji', 5);
INSERT INTO public.branslar VALUES (47, 'Ortopedi', 5);
INSERT INTO public.branslar VALUES (48, 'Cildiye', 5);
INSERT INTO public.branslar VALUES (49, 'Psikiyatri', 5);
INSERT INTO public.branslar VALUES (50, 'Üroloji', 5);
INSERT INTO public.branslar VALUES (51, 'Dahiliye', 6);
INSERT INTO public.branslar VALUES (52, 'Göz Hastalıkları', 6);
INSERT INTO public.branslar VALUES (53, 'Kulak Burun Boğaz', 6);
INSERT INTO public.branslar VALUES (54, 'Genel Cerrahi', 6);
INSERT INTO public.branslar VALUES (55, 'Nöroloji', 6);
INSERT INTO public.branslar VALUES (56, 'Kardiyoloji', 6);
INSERT INTO public.branslar VALUES (57, 'Ortopedi', 6);
INSERT INTO public.branslar VALUES (58, 'Cildiye', 6);
INSERT INTO public.branslar VALUES (59, 'Psikiyatri', 6);
INSERT INTO public.branslar VALUES (60, 'Üroloji', 6);
INSERT INTO public.branslar VALUES (61, 'Dahiliye', 7);
INSERT INTO public.branslar VALUES (62, 'Göz Hastalıkları', 7);
INSERT INTO public.branslar VALUES (63, 'Kulak Burun Boğaz', 7);
INSERT INTO public.branslar VALUES (64, 'Genel Cerrahi', 7);
INSERT INTO public.branslar VALUES (65, 'Nöroloji', 7);
INSERT INTO public.branslar VALUES (66, 'Kardiyoloji', 7);
INSERT INTO public.branslar VALUES (67, 'Ortopedi', 7);
INSERT INTO public.branslar VALUES (68, 'Cildiye', 7);
INSERT INTO public.branslar VALUES (69, 'Psikiyatri', 7);
INSERT INTO public.branslar VALUES (70, 'Üroloji', 7);
INSERT INTO public.branslar VALUES (71, 'Dahiliye', 8);
INSERT INTO public.branslar VALUES (72, 'Göz Hastalıkları', 8);
INSERT INTO public.branslar VALUES (73, 'Kulak Burun Boğaz', 8);
INSERT INTO public.branslar VALUES (74, 'Genel Cerrahi', 8);
INSERT INTO public.branslar VALUES (75, 'Nöroloji', 8);
INSERT INTO public.branslar VALUES (76, 'Kardiyoloji', 8);
INSERT INTO public.branslar VALUES (77, 'Ortopedi', 8);
INSERT INTO public.branslar VALUES (78, 'Cildiye', 8);
INSERT INTO public.branslar VALUES (79, 'Psikiyatri', 8);
INSERT INTO public.branslar VALUES (80, 'Üroloji', 8);
INSERT INTO public.branslar VALUES (81, 'Dahiliye', 9);
INSERT INTO public.branslar VALUES (82, 'Göz Hastalıkları', 9);
INSERT INTO public.branslar VALUES (83, 'Kulak Burun Boğaz', 9);
INSERT INTO public.branslar VALUES (84, 'Genel Cerrahi', 9);
INSERT INTO public.branslar VALUES (85, 'Nöroloji', 9);
INSERT INTO public.branslar VALUES (86, 'Kardiyoloji', 9);
INSERT INTO public.branslar VALUES (87, 'Ortopedi', 9);
INSERT INTO public.branslar VALUES (88, 'Cildiye', 9);
INSERT INTO public.branslar VALUES (89, 'Psikiyatri', 9);
INSERT INTO public.branslar VALUES (90, 'Üroloji', 9);
INSERT INTO public.branslar VALUES (91, 'Dahiliye', 10);
INSERT INTO public.branslar VALUES (92, 'Göz Hastalıkları', 10);
INSERT INTO public.branslar VALUES (93, 'Kulak Burun Boğaz', 10);
INSERT INTO public.branslar VALUES (94, 'Genel Cerrahi', 10);
INSERT INTO public.branslar VALUES (95, 'Nöroloji', 10);
INSERT INTO public.branslar VALUES (96, 'Kardiyoloji', 10);
INSERT INTO public.branslar VALUES (97, 'Ortopedi', 10);
INSERT INTO public.branslar VALUES (98, 'Cildiye', 10);
INSERT INTO public.branslar VALUES (99, 'Psikiyatri', 10);
INSERT INTO public.branslar VALUES (100, 'Üroloji', 10);
INSERT INTO public.branslar VALUES (101, 'Çocuk Hastalıkları', 1);


--
-- TOC entry 5057 (class 0 OID 17679)
-- Dependencies: 226
-- Data for Name: doktorlar; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.doktorlar VALUES ('2a453a7d-ef11-4c51-a262-ef3b478f1c48', 'Ahmet', 'Yılmaz', 1, '547144e40284a880fe0f41ea2d7a0410e943134c74104d5314536492e942c0bd', 'WEl8IfPaM9gSK5j6EXX1Yfj44ttu1qMuEWKaRNsMfFs=', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 'N5QYbwgqhQsr8aXFH8dBia112Ixo4QbELiWsOY+Nhj0=', 'ERKEK', '298dfd15-d66b-4e06-9e87-4fc84bcf5b73', 1);
INSERT INTO public.doktorlar VALUES ('0ee62493-861f-472e-852e-83c19b5de7a8', 'Zeynep', 'Karanfil', 3, 'd09ade9542dad8cc3b2f76b61273eb405dd63b677e34d836916d302b8d1fa6cb', 'TC51U5q7UNBUrpJnRXaokqQ6z+ox7E5hdnoFBRcCPOo=', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 'J2xIp3/ZbJkTVStA8ztkD7bwxMxSMWdW/2V7mA85ZRw=', 'KADIN', '298dfd15-d66b-4e06-9e87-4fc84bcf5b73', 1);


--
-- TOC entry 5062 (class 0 OID 17741)
-- Dependencies: 231
-- Data for Name: duyurular; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 5053 (class 0 OID 17633)
-- Dependencies: 222
-- Data for Name: hastalar; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.hastalar VALUES ('dac180a8-ca77-468e-b650-e23f4b750289', 'Furkan', 'İşeri', '857f5d35caebe70abd5f4d38f1c8f084e5299eb0beeb5ae5d799c40e8464f4fc', 'K396ojao6gGD/h+2KVWURVQsJml9G/Ay4PP/bIGLrOA=', '5555555555 ', '6121c9e0602c9f33792d4f9c5c631005fd7c17938d3e6e0684f46449d733eddc', 'gXP2MYUgonF+wqnYFEfO/19+6Y/V3I6Z9ERVXe7/ovM=', 'ERKEK');
INSERT INTO public.hastalar VALUES ('710a756f-92c9-4cbf-8b48-12dac52cc9c8', 'Kemal', 'Yılmaz', '1172daf655a07e57c44f8aae58879469ff2dc95d922c02c2ad615f4836dcb505', 'bPlkDH+0S3aZzZCmrDK6tEQllMTZ6QACo794n/2v/9M=', '5788888888 ', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 'x0pbzEldmJJPrbSAfwvRc34m8Ek4/X5v3/0O/zrqzXY=', 'ERKEK');


--
-- TOC entry 5068 (class 0 OID 17789)
-- Dependencies: 237
-- Data for Name: hastalar_log; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.hastalar_log VALUES (1, 'dac180a8-ca77-468e-b650-e23f4b750289', 'KAYIT', 'postgres', '2025-12-19 17:23:10.896178', NULL, NULL, NULL);
INSERT INTO public.hastalar_log VALUES (2, 'dac180a8-ca77-468e-b650-e23f4b750289', 'GUNCELLEME', 'postgres', '2025-12-19 17:40:40.827174', 'Furkan', 'İşeri', '5555555555 ');
INSERT INTO public.hastalar_log VALUES (3, 'dac180a8-ca77-468e-b650-e23f4b750289', 'GUNCELLEME', 'postgres', '2025-12-19 17:41:11.08892', 'Furkan', 'İşer', '5555555555 ');
INSERT INTO public.hastalar_log VALUES (4, '710a756f-92c9-4cbf-8b48-12dac52cc9c8', 'KAYIT', 'postgres', '2025-12-20 12:50:11.851699', NULL, NULL, NULL);


--
-- TOC entry 5050 (class 0 OID 17593)
-- Dependencies: 219
-- Data for Name: hastaneler; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.hastaneler VALUES (1, 'Akdeniz Üniversitesi Hastanesi', 'Antalya', '02422274343');
INSERT INTO public.hastaneler VALUES (2, 'Antalya Eğitim ve Araştırma Hastanesi', 'Antalya', NULL);
INSERT INTO public.hastaneler VALUES (3, 'Ege Üniversitesi Tıp Fakültesi Hastanesi', 'İzmir', '02323903939');
INSERT INTO public.hastaneler VALUES (4, 'İzmir Bayraklı Şehir Hastanesi', 'İzmir', NULL);
INSERT INTO public.hastaneler VALUES (5, 'Hacettepe Üniversitesi Hastanesi', 'Ankara', '03123055000');
INSERT INTO public.hastaneler VALUES (6, 'Ankara Bilkent Şehir Hastanesi', 'Ankara', '03125526000');
INSERT INTO public.hastaneler VALUES (7, 'İstanbul Cerrahpaşa Tıp Fakültesi Hastanesi', 'İstanbul', '02124143000');
INSERT INTO public.hastaneler VALUES (8, 'Başakşehir Çam ve Sakura Şehir Hastanesi', 'İstanbul', NULL);
INSERT INTO public.hastaneler VALUES (9, 'Konya Şehir Hastanesi', 'Konya', '03323105000');
INSERT INTO public.hastaneler VALUES (10, 'Necmettin Erbakan Üniversitesi Meram Tıp Fakültesi', 'Konya', NULL);


--
-- TOC entry 5052 (class 0 OID 17614)
-- Dependencies: 221
-- Data for Name: ilaclar; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.ilaclar VALUES (1, 'PAROL', 98, 1);
INSERT INTO public.ilaclar VALUES (2, 'OTIPAX', 38, 1);


--
-- TOC entry 5064 (class 0 OID 17764)
-- Dependencies: 233
-- Data for Name: ilaclar_log; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 5066 (class 0 OID 17777)
-- Dependencies: 235
-- Data for Name: randevu_loglari; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.randevu_loglari VALUES (1, 1, 'dac180a8-ca77-468e-b650-e23f4b750289', '2a453a7d-ef11-4c51-a262-ef3b478f1c48', 'DURUM_DEGISTI', 'postgres', '2025-12-19 19:41:21.866542', false, true);
INSERT INTO public.randevu_loglari VALUES (2, 2, NULL, '0ee62493-861f-472e-852e-83c19b5de7a8', 'SILME', 'postgres', '2025-12-20 13:33:40.317529', false, NULL);
INSERT INTO public.randevu_loglari VALUES (3, 3, NULL, '0ee62493-861f-472e-852e-83c19b5de7a8', 'SILME', 'postgres', '2025-12-20 13:48:57.737663', false, NULL);
INSERT INTO public.randevu_loglari VALUES (4, 4, 'dac180a8-ca77-468e-b650-e23f4b750289', '0ee62493-861f-472e-852e-83c19b5de7a8', 'DURUM_DEGISTI', 'postgres', '2025-12-20 13:50:43.142369', false, true);


--
-- TOC entry 5060 (class 0 OID 17711)
-- Dependencies: 229
-- Data for Name: randevular; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.randevular VALUES (1, '2026-01-01', '16:00:00', 1, '2a453a7d-ef11-4c51-a262-ef3b478f1c48', true, 'dac180a8-ca77-468e-b650-e23f4b750289', 'Boğazım Ağrıyor.');
INSERT INTO public.randevular VALUES (4, '2026-01-02', '08:00:00', 3, '0ee62493-861f-472e-852e-83c19b5de7a8', true, 'dac180a8-ca77-468e-b650-e23f4b750289', 'Kulağımda Bir sıkıntı var.');


--
-- TOC entry 5072 (class 0 OID 17825)
-- Dependencies: 241
-- Data for Name: recetedetay; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.recetedetay VALUES (1, 'SABAH-AKŞAM 1 tane tok karnı', 2, 1, 1);
INSERT INTO public.recetedetay VALUES (2, 'SABAH-AKSAM 1 TANE TOK KARNI', 2, 2, 2);


--
-- TOC entry 5070 (class 0 OID 17801)
-- Dependencies: 239
-- Data for Name: receteler; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.receteler VALUES (1, 'Boğazın ağrıyor olabilir ama ben sana ağrı kesici vericem.', '2025-12-19', 1, 'dac180a8-ca77-468e-b650-e23f4b750289', '2a453a7d-ef11-4c51-a262-ef3b478f1c48');
INSERT INTO public.receteler VALUES (2, 'Kulak iltihabın var.', '2025-12-20', 4, 'dac180a8-ca77-468e-b650-e23f4b750289', '0ee62493-861f-472e-852e-83c19b5de7a8');


--
-- TOC entry 5054 (class 0 OID 17648)
-- Dependencies: 223
-- Data for Name: sekreterler; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.sekreterler VALUES ('96b2ccdb-355a-4c05-9bdb-17ffa01f1893', 'Mehmet', 'Demir', '5797e80d338224b49e036b8786c483e2e8fa8410ad5455ba280263d34653d315', 'ZinnUew1//c0nwMo7VxU1EVc02XrqTycWytfjLZz3dc=', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', '4bLujZJSmMpzozA+BZhM7Q7w+k4wlXI+KMTwm9h3bzM=', 'ERKEK', 2);
INSERT INTO public.sekreterler VALUES ('580abc2f-6c4c-4e2c-877f-5a2617a099ca', 'Fatma', 'Kaya', '02b264525869a7b0a8d12bfb850d964d12be53c83b76b4167397abd25a56c9bc', 'xyOxMMwO52+w/g054ZIzilvtdfk3GMHvlcRve6Z84eg=', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 'wPmT+mmQrgE5p7qc77yVkzp9xYgHTREQM7wjjcxQ6+Y=', 'KADIN', 3);
INSERT INTO public.sekreterler VALUES ('09b394ab-9671-466a-a769-8f7b2c57a3a5', 'Ali', 'Çelik', 'd5996b25e580c95b90cfc8a69898b31ee8edb66bea003ac99801b8cab34c2bb4', 'DtbbkoMCaGDJ19w5IcDqkTNfoGCuoOHdHTfKEg6XX1M=', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', '7AhPqFIzKiF5Sn4zIiv/tJtc5R4/mKed6j31/oNoWB4=', 'ERKEK', 4);
INSERT INTO public.sekreterler VALUES ('0acdd4fc-e0d8-4aae-b353-845fb4018997', 'Zeynep', 'Şahin', 'b0fd14685addee924623fcdfc54ced6a6a4504f1d5b5c19690e7c945ae2e0cb6', 'Vtd5uU7cBWiponyEAuPC7nW9gGrqCS57KADUN+4U3ns=', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', '2buLrUWs2ObWGjlXVzf0PCVjeIHIBAAuVvqlaYwtP7Q=', 'KADIN', 5);
INSERT INTO public.sekreterler VALUES ('95496848-49ed-4d25-8e40-c9c9b8f72468', 'Mustafa', 'Yıldız', '954fba0d8cd4e91621d9809c9c6b670f87417d355fb4b6fe55ca32a301652465', '13nRNFXHtGrUv7X+dSFLAYc/regYlyBvCshCSA9vz5k=', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 'a+iVxiWWyJURwOPJCggo0onuEwlyLou13e42BUlp8jU=', 'ERKEK', 6);
INSERT INTO public.sekreterler VALUES ('6e4b0092-7166-44be-848a-69f4338467d0', 'Esra', 'Öztürk', '2c61ca5fd60080e55ca81bd82ec04a14a385a5fa231eccd9b3cd59a91b11206b', 'O007DMEPIpL/ps+Ji8dFQeWlCj3y63TLzo3xDnxpDTw=', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 'ZO/6WpIA8W9+v1hFou7nNObl3cVSbFx4IsbmKO9cMlk=', 'KADIN', 7);
INSERT INTO public.sekreterler VALUES ('e914e161-2493-4487-9e72-9c46f0ebca45', 'Burak', 'Aydın', '88e7bcf1a118f81c02bc3aee401d3f0da4ee34140a40749c2079a9790cd15c30', 'g4BvcZwiRhPTW/90lMAXFlJuG1F1O22GhpqSogaScIE=', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 'OSCkS2MEXB8f0FdXXeVHFVgobhqcy/uZnlYa2ubYQ1o=', 'ERKEK', 8);
INSERT INTO public.sekreterler VALUES ('3304cc2c-bc26-44b6-8eb5-3d537a69f3d9', 'Elif', 'Arslan', '0f10d10d47359235e44bf7a1e4598a3844e7df0403885ba3ce8d8f931db8f7fd', 'eng6NTNoSrDrJDW+ejlYE383tdTyLd3JSg/l1IdTs04=', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', '3QeOhckagvNpP1f5TtjuGQQaJTcSkV0kJ1/wT4V3200=', 'KADIN', 9);
INSERT INTO public.sekreterler VALUES ('b273daa1-1965-447a-b76e-611d88e76b3f', 'Emre', 'Doğan', '3d9d5b78079c684a2f94e0e82d2bcaf49f3d2673a5004ff5e37a9413a7b05648', 'vlVSaOu8R1UfNrLsePk+i3LUDyedWuuFbU/C4Sya3Ig=', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 'tCPi5Nh+6DQxhtJxLBGcdDMeKG8dnxBdlCKiI1A9Lso=', 'ERKEK', 10);
INSERT INTO public.sekreterler VALUES ('298dfd15-d66b-4e06-9e87-4fc84bcf5b73', 'Ayşe', 'Yılmaz', '534a4a8eafcd8489af32356d5a7a25f88c70cfe0448539a7c42964c1b897a359', 'lKrxGiD1fDaXDRG4wti33dSrbXlTzXl66qJnVhzFIYU=', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 'zlUPmISejWJXLNtNi7mPIRCUUjRhbU+Flcg/9JRKCBo=', 'KADIN', 1);


--
-- TOC entry 5097 (class 0 OID 0)
-- Dependencies: 224
-- Name: branslar_brans_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.branslar_brans_id_seq', 101, true);


--
-- TOC entry 5098 (class 0 OID 0)
-- Dependencies: 230
-- Name: duyurular_duyurular_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.duyurular_duyurular_id_seq', 1, true);


--
-- TOC entry 5099 (class 0 OID 0)
-- Dependencies: 236
-- Name: hastalar_log_log_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.hastalar_log_log_id_seq', 4, true);


--
-- TOC entry 5100 (class 0 OID 0)
-- Dependencies: 242
-- Name: hastaneler_hastane_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.hastaneler_hastane_id_seq', 1, false);


--
-- TOC entry 5101 (class 0 OID 0)
-- Dependencies: 220
-- Name: ilaclar_ilac_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.ilaclar_ilac_id_seq', 2, true);


--
-- TOC entry 5102 (class 0 OID 0)
-- Dependencies: 232
-- Name: ilaclar_log_log_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.ilaclar_log_log_id_seq', 1, false);


--
-- TOC entry 5103 (class 0 OID 0)
-- Dependencies: 234
-- Name: randevu_loglari_log_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.randevu_loglari_log_id_seq', 4, true);


--
-- TOC entry 5104 (class 0 OID 0)
-- Dependencies: 228
-- Name: randevular_brans_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.randevular_brans_id_seq', 1, false);


--
-- TOC entry 5105 (class 0 OID 0)
-- Dependencies: 227
-- Name: randevular_randevu_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.randevular_randevu_id_seq', 4, true);


--
-- TOC entry 5106 (class 0 OID 0)
-- Dependencies: 240
-- Name: recetedetay_detay_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.recetedetay_detay_id_seq', 2, true);


--
-- TOC entry 5107 (class 0 OID 0)
-- Dependencies: 238
-- Name: receteler_recete_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.receteler_recete_id_seq', 2, true);


--
-- TOC entry 4860 (class 2606 OID 17676)
-- Name: branslar branslar_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.branslar
    ADD CONSTRAINT branslar_pkey PRIMARY KEY (brans_id);


--
-- TOC entry 4864 (class 2606 OID 17693)
-- Name: doktorlar doktorlar_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.doktorlar
    ADD CONSTRAINT doktorlar_pkey PRIMARY KEY (doktor_id);


--
-- TOC entry 4868 (class 2606 OID 17747)
-- Name: duyurular duyurular_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.duyurular
    ADD CONSTRAINT duyurular_pkey PRIMARY KEY (duyurular_id);


--
-- TOC entry 4874 (class 2606 OID 17797)
-- Name: hastalar_log hastalar_log_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.hastalar_log
    ADD CONSTRAINT hastalar_log_pkey PRIMARY KEY (log_id);


--
-- TOC entry 4856 (class 2606 OID 17647)
-- Name: hastalar hastalar_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.hastalar
    ADD CONSTRAINT hastalar_pkey PRIMARY KEY (hasta_id);


--
-- TOC entry 4852 (class 2606 OID 17601)
-- Name: hastaneler hastaneler_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.hastaneler
    ADD CONSTRAINT hastaneler_pkey PRIMARY KEY (hastane_id);


--
-- TOC entry 4870 (class 2606 OID 17773)
-- Name: ilaclar_log ilaclar_log_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ilaclar_log
    ADD CONSTRAINT ilaclar_log_pkey PRIMARY KEY (log_id);


--
-- TOC entry 4854 (class 2606 OID 17622)
-- Name: ilaclar ilaclar_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ilaclar
    ADD CONSTRAINT ilaclar_pkey PRIMARY KEY (ilac_id);


--
-- TOC entry 4872 (class 2606 OID 17785)
-- Name: randevu_loglari randevu_loglari_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.randevu_loglari
    ADD CONSTRAINT randevu_loglari_pkey PRIMARY KEY (log_id);


--
-- TOC entry 4866 (class 2606 OID 17722)
-- Name: randevular randevular_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.randevular
    ADD CONSTRAINT randevular_pkey PRIMARY KEY (randevu_id);


--
-- TOC entry 4880 (class 2606 OID 17832)
-- Name: recetedetay recetedetay_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.recetedetay
    ADD CONSTRAINT recetedetay_pkey PRIMARY KEY (detay_id);


--
-- TOC entry 4876 (class 2606 OID 17808)
-- Name: receteler receteler_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.receteler
    ADD CONSTRAINT receteler_pkey PRIMARY KEY (recete_id);


--
-- TOC entry 4858 (class 2606 OID 17662)
-- Name: sekreterler sekreterler_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sekreterler
    ADD CONSTRAINT sekreterler_pkey PRIMARY KEY (sekreter_id);


--
-- TOC entry 4862 (class 2606 OID 17871)
-- Name: branslar uq_hastane_brans; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.branslar
    ADD CONSTRAINT uq_hastane_brans UNIQUE (brans_ad, hastane_id);


--
-- TOC entry 4878 (class 2606 OID 17893)
-- Name: receteler uq_randevu_recete; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.receteler
    ADD CONSTRAINT uq_randevu_recete UNIQUE (randevu_id);


--
-- TOC entry 4898 (class 2620 OID 17799)
-- Name: hastalar trg_hasta_log; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER trg_hasta_log AFTER INSERT OR DELETE OR UPDATE ON public.hastalar FOR EACH ROW EXECUTE FUNCTION public.func_hasta_log();


--
-- TOC entry 4896 (class 2620 OID 17612)
-- Name: hastaneler trg_hastane_log; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER trg_hastane_log AFTER UPDATE ON public.hastaneler FOR EACH ROW EXECUTE FUNCTION public.trg_hastane_log_func();


--
-- TOC entry 4902 (class 2620 OID 17775)
-- Name: ilaclar_log trg_ilaclog; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER trg_ilaclog BEFORE INSERT OR UPDATE ON public.ilaclar_log FOR EACH ROW EXECUTE FUNCTION public.ilaclog();


--
-- TOC entry 4897 (class 2620 OID 17756)
-- Name: ilaclar trg_ilacstokkontrol; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER trg_ilacstokkontrol BEFORE INSERT OR UPDATE ON public.ilaclar FOR EACH ROW EXECUTE FUNCTION public.ilacstokkontrol();


--
-- TOC entry 4899 (class 2620 OID 17851)
-- Name: doktorlar trg_limit_doktor; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER trg_limit_doktor BEFORE INSERT ON public.doktorlar FOR EACH ROW EXECUTE FUNCTION public.check_doktor_limit();


--
-- TOC entry 4900 (class 2620 OID 17787)
-- Name: randevular trg_randevu_log; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER trg_randevu_log AFTER DELETE OR UPDATE ON public.randevular FOR EACH ROW EXECUTE FUNCTION public.func_randevu_log();


--
-- TOC entry 4901 (class 2620 OID 17846)
-- Name: randevular trg_randevueklemeoncesi; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER trg_randevueklemeoncesi BEFORE INSERT ON public.randevular FOR EACH ROW EXECUTE FUNCTION public.trg_randevukontrol();


--
-- TOC entry 4884 (class 2606 OID 17694)
-- Name: doktorlar doktorlar_brans_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.doktorlar
    ADD CONSTRAINT doktorlar_brans_id_fkey FOREIGN KEY (brans_id) REFERENCES public.branslar(brans_id);


--
-- TOC entry 4885 (class 2606 OID 17704)
-- Name: doktorlar doktorlar_hastane_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.doktorlar
    ADD CONSTRAINT doktorlar_hastane_id_fkey FOREIGN KEY (hastane_id) REFERENCES public.hastaneler(hastane_id);


--
-- TOC entry 4886 (class 2606 OID 17699)
-- Name: doktorlar doktorlar_sekreter_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.doktorlar
    ADD CONSTRAINT doktorlar_sekreter_id_fkey FOREIGN KEY (sekreter_id) REFERENCES public.sekreterler(sekreter_id);


--
-- TOC entry 4890 (class 2606 OID 17748)
-- Name: duyurular duyurular_sekreter_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.duyurular
    ADD CONSTRAINT duyurular_sekreter_id_fkey FOREIGN KEY (sekreter_id) REFERENCES public.sekreterler(sekreter_id);


--
-- TOC entry 4883 (class 2606 OID 17882)
-- Name: branslar fk_hastane_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.branslar
    ADD CONSTRAINT fk_hastane_id FOREIGN KEY (hastane_id) REFERENCES public.hastaneler(hastane_id) NOT VALID;


--
-- TOC entry 4881 (class 2606 OID 17623)
-- Name: ilaclar ilaclar_hastane_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ilaclar
    ADD CONSTRAINT ilaclar_hastane_id_fkey FOREIGN KEY (hastane_id) REFERENCES public.hastaneler(hastane_id);


--
-- TOC entry 4887 (class 2606 OID 17723)
-- Name: randevular randevular_brans_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.randevular
    ADD CONSTRAINT randevular_brans_id_fkey FOREIGN KEY (brans_id) REFERENCES public.branslar(brans_id);


--
-- TOC entry 4888 (class 2606 OID 17728)
-- Name: randevular randevular_doktor_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.randevular
    ADD CONSTRAINT randevular_doktor_id_fkey FOREIGN KEY (doktor_id) REFERENCES public.doktorlar(doktor_id);


--
-- TOC entry 4889 (class 2606 OID 17733)
-- Name: randevular randevular_hasta_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.randevular
    ADD CONSTRAINT randevular_hasta_id_fkey FOREIGN KEY (hasta_id) REFERENCES public.hastalar(hasta_id);


--
-- TOC entry 4894 (class 2606 OID 17838)
-- Name: recetedetay recetedetay_ilac_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.recetedetay
    ADD CONSTRAINT recetedetay_ilac_id_fkey FOREIGN KEY (ilac_id) REFERENCES public.ilaclar(ilac_id);


--
-- TOC entry 4895 (class 2606 OID 17833)
-- Name: recetedetay recetedetay_recete_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.recetedetay
    ADD CONSTRAINT recetedetay_recete_id_fkey FOREIGN KEY (recete_id) REFERENCES public.receteler(recete_id);


--
-- TOC entry 4891 (class 2606 OID 17819)
-- Name: receteler receteler_doktor_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.receteler
    ADD CONSTRAINT receteler_doktor_id_fkey FOREIGN KEY (doktor_id) REFERENCES public.doktorlar(doktor_id);


--
-- TOC entry 4892 (class 2606 OID 17814)
-- Name: receteler receteler_hasta_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.receteler
    ADD CONSTRAINT receteler_hasta_id_fkey FOREIGN KEY (hasta_id) REFERENCES public.hastalar(hasta_id);


--
-- TOC entry 4893 (class 2606 OID 17809)
-- Name: receteler receteler_randevu_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.receteler
    ADD CONSTRAINT receteler_randevu_id_fkey FOREIGN KEY (randevu_id) REFERENCES public.randevular(randevu_id);


--
-- TOC entry 4882 (class 2606 OID 17663)
-- Name: sekreterler sekreterler_hastane_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sekreterler
    ADD CONSTRAINT sekreterler_hastane_id_fkey FOREIGN KEY (hastane_id) REFERENCES public.hastaneler(hastane_id);


--
-- TOC entry 5079 (class 0 OID 0)
-- Dependencies: 225
-- Name: TABLE branslar; Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON TABLE public.branslar TO sekreter_rolu;


--
-- TOC entry 5081 (class 0 OID 0)
-- Dependencies: 226
-- Name: TABLE doktorlar; Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON TABLE public.doktorlar TO sekreter_rolu;
GRANT UPDATE ON TABLE public.doktorlar TO doktor_rolu;


--
-- TOC entry 5082 (class 0 OID 0)
-- Dependencies: 231
-- Name: TABLE duyurular; Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON TABLE public.duyurular TO sekreter_rolu;


--
-- TOC entry 5084 (class 0 OID 0)
-- Dependencies: 222
-- Name: TABLE hastalar; Type: ACL; Schema: public; Owner: postgres
--

GRANT INSERT,UPDATE ON TABLE public.hastalar TO hasta_rolu;


--
-- TOC entry 5086 (class 0 OID 0)
-- Dependencies: 221
-- Name: TABLE ilaclar; Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON TABLE public.ilaclar TO sekreter_rolu;


--
-- TOC entry 5092 (class 0 OID 0)
-- Dependencies: 241
-- Name: TABLE recetedetay; Type: ACL; Schema: public; Owner: postgres
--

GRANT INSERT ON TABLE public.recetedetay TO sekreter_rolu;
GRANT INSERT,UPDATE ON TABLE public.recetedetay TO doktor_rolu;
GRANT SELECT ON TABLE public.recetedetay TO hasta_rolu;


--
-- TOC entry 5094 (class 0 OID 0)
-- Dependencies: 239
-- Name: TABLE receteler; Type: ACL; Schema: public; Owner: postgres
--

GRANT INSERT ON TABLE public.receteler TO sekreter_rolu;
GRANT INSERT,UPDATE ON TABLE public.receteler TO doktor_rolu;
GRANT SELECT ON TABLE public.receteler TO hasta_rolu;


--
-- TOC entry 5096 (class 0 OID 0)
-- Dependencies: 223
-- Name: TABLE sekreterler; Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON TABLE public.sekreterler TO sekreter_rolu;


-- Completed on 2025-12-21 15:50:43

--
-- PostgreSQL database dump complete
--

\unrestrict lYp2tP6thRP8dJnSpN7KK4XTy238DDmfbWA3OXHdznfViT7Vabah1w9PbrMYNaX


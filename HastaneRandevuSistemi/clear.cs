using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastaneRandevuSistemi
{
    internal class clear
    {
        public static void Temizle(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is TextBox || control is MaskedTextBox || control is RichTextBox || control is ComboBox)
                {
                    control.Text = string.Empty;
                }
                else if (control.HasChildren)
                {
                    Temizle(control.Controls);
                }
            }
        }
    }
}

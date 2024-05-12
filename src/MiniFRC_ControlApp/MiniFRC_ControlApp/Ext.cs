using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_ControlApp
{
    public static class Ext
    {
        public static void AppendText(this RichTextBox box, string text, Color color, Font f = null)
        {

            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.SelectionFont = f ?? box.Font;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}

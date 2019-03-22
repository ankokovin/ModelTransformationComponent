using System;
using System.Drawing;
using System.Windows.Forms;

namespace ModelTranformerExample
{
    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        private static int SepLen = 50;
        public static void AppendException(this RichTextBox box, Exception ex, int depth=0)
        {
            if (ex != null)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message + " " + ex.GetType());
                System.Diagnostics.Debug.WriteLine("depth = " + depth);
                if (depth == 0)
                {
                    var datestr = DateTime.Now.ToString();
                    box.AppendText(new string('-', (SepLen - datestr.Length - 2) / 2) 
                        + "[" + datestr + "]"
                        + new string('-', (SepLen - datestr.Length - 2) / 2) + 
                        "\n");
                }
                box.AppendText(new string(' ', depth));
                if (ex is ModelTransformationComponent.TransformComponentException)
                {
                    if (ex is ModelTransformationComponent.SyntaxErrorPlaced se)
                    {
                        box.AppendText("[", Color.Black);
                        box.AppendText(se.Line.ToString(), Color.Blue);
                        box.AppendText(",", Color.Black);
                        box.AppendText(se.Symbol.ToString(), Color.Blue);
                        box.AppendText("] ", Color.Black);
                        box.AppendText(se.TrimedMsg, Color.Red);
                    }
                    else
                    {
                        box.AppendText(ex.Message, Color.Red);
                    }
                }
                else
                {
                    box.AppendText("Unexpected exception\n",Color.Red);
                    box.AppendText(new string(' ', depth));
                    box.AppendText(ex.Message, Color.Red);
                }
                box.AppendText("\n");
                box.AppendException(ex.InnerException, depth+4);
            }
            else
            {
                box.AppendText(new string('-', SepLen ) + "\n");
            }
        }
    }
}

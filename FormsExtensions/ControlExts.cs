using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NUnit.Framework;

namespace FormsExtensions
{
    public static class ControlExts
    {
        public static void Set(this TextBoxBase textbox, string text)
        {
            if (textbox.InvokeRequired)
            {
                textbox.Invoke(new Action<string>(textbox.Set), new object[] { text });
                return;
            }
            if (textbox.Text == text) return;
            textbox.Text = text;
        }

        /*
        public static Observable<string> Observ(this TextBox textbox)
        {
            var observ = new Observable<string>(textbox.Text);
            observ.Subscribe(textbox.Set);

            EventHandler textChanged = (x, y) => { observ.Set(textbox.Text); };
            EventHandler disposed = (x, y) => { observ.Dispose(); };

            textbox.TextChanged += textChanged;
            textbox.Disposed += disposed;

            observ.OnDispose(() =>
            {
                textbox.TextChanged -= textChanged;
                textbox.Disposed -= disposed;
            });

            return observ;
        }
        */
    }
}

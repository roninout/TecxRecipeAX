using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TecxRecipeAX
{
    public static class ControlExtensions
    {
        public static void SafeInvoke(this Control control, Action action)
        {
            if (control.InvokeRequired)
                control.Invoke(action);
            else
                action();
        }

        public static Task InvokeAsync(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                var tcs = new TaskCompletionSource<object>();
                control.BeginInvoke(new Action(() =>
                {
                    try
                    {
                        action();
                        tcs.SetResult(null);
                    }
                    catch (Exception ex)
                    {
                        tcs.SetException(ex);
                    }
                }));
                return tcs.Task;
            }
            else
            {
                action();
                return Task.CompletedTask;
            }
        }
    }
}

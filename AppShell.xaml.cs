using System.Diagnostics;

namespace ZXingTest;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        try
        {
            Routing.RegisterRoute(nameof(SecondPage), typeof(SecondPage));
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex);
        }
    }
}

using System.Diagnostics;
using ZXing.Net.Maui;

namespace ZXingTest;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnBtnClicked(object sender, EventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync($"//SecondPage");
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex);
            //throw;
        }
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            _ = await CheckAndRequestCameraPermission();
            qrscanner.BarcodesDetected += OnBarcodeScanned;
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex);
        }

    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);

        qrscanner.BarcodesDetected -= OnBarcodeScanned;
        qrscanner.IsDetecting = false;
        qrscanner.IsEnabled = false;
        qrscanner.IsVisible = false;
        qrscanner.Parent.RemoveLogicalChild(qrscanner);

        GC.Collect();
    }
    public static async Task<PermissionStatus> CheckAndRequestCameraPermission()
    {
        try
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.Camera>();

            if (status == PermissionStatus.Granted)
            {
                return status;
            }

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                return status;
            }

            status = await Permissions.RequestAsync<Permissions.Camera>();
            return status;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return PermissionStatus.Denied;
        }
    }

    private void OnBarcodeScanned(object? result, BarcodeDetectionEventArgs e)
    {
        string barcode = e.Results.FirstOrDefault()?.Value ?? string.Empty;
        ScannedBarCode.Text = barcode;
    }
}
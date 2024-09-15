using Blazored.SessionStorage;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace MauiBlazor
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });
            // builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://10.0.2.2:7175/") });
            builder.Services.AddScoped(sp => new HttpClient(new CustomHttpClientHandler())
            {
                 BaseAddress = new Uri("https://10.0.2.2:7177/")
               // BaseAddress = new Uri("https://minimalapi202409-egecf3egbkcpcab2.eastus-01.azurewebsites.net/")
            });
            builder.Services.AddBlazoredSessionStorage();
            builder.Services.AddMudServices();
            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }

    public class CustomHttpClientHandler : HttpClientHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;
            return base.SendAsync(request, cancellationToken);
        }
    }
}

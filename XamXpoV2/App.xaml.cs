using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamXpoV2.Views;
using XamXpoV2.ViewModels;
using BIT.Xpo.XPOWebApi.Client;
using OrmV2;
using System;
using System.IO;
using DevExpress.Xpo.DB;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamXpoV2
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            //var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            //var filePath = Path.Combine(documentsPath, "xpoXamarinDemo.db");
            // string connectionString = SQLiteConnectionProvider.GetConnectionString(filePath) + ";Cache=Shared;";
            XPOWebApi.Register();
            var connectionString = XPOWebApi.GetConnectionString("http://10.0.2.2/BitServer", string.Empty, "db1");
            ////string connectionString = MSSqlConnectionProvider.GetConnectionString("XAFERPC", "hector", "root", "XafBackend");
            XpoHelper.InitXpo(connectionString);

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
        }
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var a = e.ExceptionObject.ToString();
        }
    }
}

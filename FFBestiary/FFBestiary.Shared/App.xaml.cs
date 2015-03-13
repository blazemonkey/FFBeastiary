using FFBestiary.Services.NavigationService;
using FFBestiary.Services.SQLiteService;
using Microsoft.Practices.Prism.Mvvm;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace FFBestiary
{

    public sealed partial class App : MvvmAppBase
    {
        public static readonly Container _container = new Container();

        public App()
        {
            this.InitializeComponent();
        }        

        protected override async Task OnInitializeAsync(IActivatedEventArgs args)
        {
            _container.RegisterSingle(NavigationService);
            _container.Register<ISqlLiteService, SqlLiteService>();
            _container.Register<INavigationService, NavigationService>();
            _container.Verify();

            await _container.GetInstance<SqlLiteService>().ClearLocalDb();
            return;
        }

        protected override object Resolve(Type type)
        {
            return _container.GetInstance(type);
        }
        
        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs e)
        {            
            NavigationService.Navigate(Experiences.Main.ToString(), null);
            return Task.FromResult<object>(null);
        }

    }
}
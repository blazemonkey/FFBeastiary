using FFBestiary.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FFBestiary.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EnemiesPage : PageBase
    {
        private ContentControl _statsContentControl;
        private int _gameId;

        public EnemiesPage()
        {
            this.InitializeComponent();            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _gameId = int.Parse(e.Parameter.ToString());            
            base.OnNavigatedTo(e);
        }

        private void StatsContentControl_Loaded(object sender, RoutedEventArgs e)
        {
            _statsContentControl = (ContentControl)sender;
            _statsContentControl.Style = (Style)this.Resources["FFVIIEnemies"];
        }
    }
}

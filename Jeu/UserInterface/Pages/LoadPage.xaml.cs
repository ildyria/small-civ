using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserInterface.Pages
{
    /// <summary>
    /// Logique d'interaction pour LoadPage.xaml
    /// </summary>
    public partial class LoadPage : UserControl, ISwitchable
    {
        public LoadPage()
        {
            InitializeComponent();
        }
        private void startLoadGame_clicked(object sender, RoutedEventArgs e)
        {
            SmallWorld.GameMakerLoad gmn = new SmallWorld.GameMakerLoad();
            //GManager = gmn.makeGame();
        }
        private void deleteSaveButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Pages.MainMenu());
        }

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        #endregion

      
    }
}

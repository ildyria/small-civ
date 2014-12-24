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
    /// Logique d'interaction pour EndGameMenu.xaml
    /// </summary>
    public partial class EndGameMenu : UserControl, ISwitchable
    {
        public EndGameMenu()
        {
            InitializeComponent();
            recapJ1.DataContext = Data.Instance.GManager.Players[0];
            recapJ2.DataContext = Data.Instance.GManager.Players[1];
        }

        private void returnToMainMenuButton_Click(object sender, RoutedEventArgs e)
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

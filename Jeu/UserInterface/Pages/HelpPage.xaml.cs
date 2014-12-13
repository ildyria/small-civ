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
    /// Logique d'interaction pour Help.xaml
    /// </summary>
    public partial class HelpPage: UserControl, ISwitchable
    {
        public HelpPage()
        {
            InitializeComponent();
        }
        private void returnToMainMenuButton_Click(object sender, System.Windows.RoutedEventArgs e)
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

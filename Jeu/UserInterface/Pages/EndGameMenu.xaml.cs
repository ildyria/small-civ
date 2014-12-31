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
using SmallWorld;

namespace UserInterface.Pages
{
    /// <summary>
    /// Logique d'interaction pour EndGameMenu.xaml
    /// </summary>
    public partial class EndGameMenu : UserControl, ISwitchable
    {
        public string Winner
        {
            get 
            { 
                int max = 0;
                string name = "";
                bool exAequo = false;
                foreach (Player p in Data.Instance.GManager.Players)
                {
                    if (p.Points > max)
                    {
                        max = p.Points;
                        name = p.Name;
                        exAequo = false;
                    }
                    else if (p.Points == max)
                    {
                        exAequo = true;
                        name += " et " + p.Name;
                    }
                }
                if (exAequo)
                {
                    return "Ex Aequo : " + name;
                }
                else
                {
                    return "Vainqueur : " + name; 
                }
                 
            }
        }
        public EndGameMenu()
        {
            InitializeComponent();
            this.DataContext = this;
            listPlayer.DataContext = Data.Instance.GManager;

            //recapJ1.DataContext = Data.Instance.GManager.Players[0];
            //recapJ2.DataContext = Data.Instance.GManager.Players[1];
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

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
    /// Logique d'interaction pour NewGameMenu.xaml
    /// </summary>
    public partial class NewGameMenu : UserControl, ISwitchable
    {
        public NewGameMenu()
        {
            InitializeComponent();
            this.DataContext = Data.Instance;
        }

        private void startNewGame_Click(object sender, RoutedEventArgs e)
        {
            UnitType[] listTribes = new UnitType[2];

            Dictionary<RadioButton, UnitType> tribeJ1Radio = new Dictionary<RadioButton, UnitType>()
            {
               {tribeJ1_dwarf, UnitType.DWARF},
               {tribeJ1_orc, UnitType.ORC},
               {tribeJ1_elf, UnitType.ELF}
            };

            Dictionary<RadioButton, UnitType> tribeJ2Radio = new Dictionary<RadioButton, UnitType>()
            {
               {tribeJ2_dwarf, UnitType.DWARF},
               {tribeJ2_orc, UnitType.ORC},
               {tribeJ2_elf, UnitType.ELF}
            };

            Dictionary<RadioButton, MapSize> mapSizeRadio = new Dictionary<RadioButton, MapSize>()
            {
               {mapSize_demo, MapSize.DEMO},
               {mapSize_small, MapSize.SMALL},
               {mapSize_classic, MapSize.CLASSIC}
            };

            listTribes[0] = tribeJ1Radio[tribeJ1Radio.Keys.ToList().First(u => u.IsChecked == true)];
            listTribes[1] = tribeJ2Radio[tribeJ2Radio.Keys.ToList().First(u => u.IsChecked == true)];
            MapSize mapSize = mapSizeRadio[mapSizeRadio.Keys.ToList().First(u => u.IsChecked == true)];

            SmallWorld.GameMakerNew gmn = new SmallWorld.GameMakerNew();
            gmn.Names = new string[2] { p1Name.Text, p2Name.Text };
            gmn.Tribes = listTribes;
            gmn.setMapSize(mapSize);
            //gmn.setNbUnit(new int)
            gmn.makeGame();
            //Should start game instead
            Data.Instance.GManager.getPlayer(1).play();

            Switcher.Switch(new Pages.InGame());

        }

        

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void cancelNewButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Pages.MainMenu());
        }
        #endregion
    }
}

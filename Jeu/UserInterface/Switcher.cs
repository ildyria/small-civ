using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

// Credit go to https://azerdark.wordpress.com

namespace UserInterface
{
    public static class Switcher
    {
        public static MainWindow pageSwitcher;

        public static void Switch(UserControl newPage)
        {
            pageSwitcher.Navigate(newPage);
        }

        public static void Switch(UserControl newPage, object state)
        {
            pageSwitcher.Navigate(newPage, state);
        }
    }
}

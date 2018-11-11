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

namespace millionaire.pages
{
    /// <summary>
    /// Interakční logika pro Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        Frame frame;
        props.FileManager fileManager = new props.FileManager();

        public Menu()
        {
            InitializeComponent();
        }
        public Menu(Frame mainFrame) : this()
        {
            frame = mainFrame;
            // disable continue button if no save found
            if (fileManager.GetProgressData().IsEmpty())
            {
                WPF_continueButton.IsEnabled = false;
            }
        }


        public void NewGame(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new Game(frame));
        }
        public void Continue(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new Game(frame, true));
        }
        public void Scoreboard(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new Scoreboard(frame));
        }
        public void Exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

    }
}

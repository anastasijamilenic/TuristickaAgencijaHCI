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
using System.Windows.Shapes;
using TuristickaAgencija.ViewModels;

namespace TuristickaAgencija.Views
{
    /// <summary>
    /// Interaction logic for ZaposleniView.xaml
    /// </summary>
    public partial class ZaposleniView : UserControl
    {
        public ZaposleniView()
        {
            InitializeComponent();
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is ZaposleniViewModel viewModel)
            {
                var passwordBox = sender as PasswordBox;
                if (passwordBox != null)
                {
                    viewModel.NewZaposleni.Lozinka = passwordBox.Password;
                }
            }
        }
    }
}

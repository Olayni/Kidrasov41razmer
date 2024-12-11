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

namespace Kidrasov_Trade
{
    /// <summary>
    /// Логика взаимодействия для TradePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        string CountObjectAll;
        int curKol = 0;
        public ServicePage()
        {
            InitializeComponent();
            var currentTrade = Kidrasov_TradeEntities.GetContext().Product.ToList();
            curKol=currentTrade.Count;
            OrderListView.ItemsSource = currentTrade;
            CountObjectAll = Convert.ToString(currentTrade.Count);
            CountObject.Text = "Количество " + currentTrade.Count.ToString() + " из " + CountObjectAll;

            ComboType.SelectedIndex = 0;
            UpdateServices();
        }
        public void UpdateServices()
        {

            var currentTrade = Kidrasov_TradeEntities.GetContext().Product.ToList();


            if (ComboType.SelectedIndex == 1)
            {
                currentTrade = currentTrade.Where(p => p.ProductDiscountAmount < 10).ToList();
            }

            if (ComboType.SelectedIndex == 2)
            {
                currentTrade = currentTrade.Where(p => p.ProductDiscountAmount >= 10 && p.ProductDiscountAmount < 15).ToList();
            }

            if (ComboType.SelectedIndex == 3)
            {
                currentTrade = currentTrade.Where(p => p.ProductDiscountAmount >= 15).ToList();
            }

            if (RButtonDown.IsChecked.Value)
            {
                currentTrade = currentTrade.OrderByDescending(p => p.ProductCost).ToList();
            }

            if (RButtonUp.IsChecked.Value)
            {
                currentTrade = currentTrade.OrderBy(p => p.ProductCost).ToList();
            }

            currentTrade = currentTrade.Where(p => p.ProductName.ToLower().Contains(Search.Text.ToLower())).ToList();
            CountObject.Text = "Количество " + currentTrade.Count.ToString() + " из " + CountObjectAll;
            
            OrderListView.ItemsSource = currentTrade;
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateServices();
        }

        private void ComboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateServices();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateServices();
        }

        private void OrderListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateServices();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateServices();
        }

        private void RButtonUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdateServices();
        }
    }
}
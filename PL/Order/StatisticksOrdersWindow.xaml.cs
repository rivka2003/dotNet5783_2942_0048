using BlImplementation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for StatisticksOrdersWindow.xaml
    /// </summary>
    public partial class StatisticksOrdersWindow : Page
    {
        private BlApi.IBl? bl = BlApi.Factory.Get();

        private string groupName = "MonthName";
        PropertyGroupDescription propertyGroupDescription;



        public ICollectionView CollectionViewblStatisticksOrderByMonths
        {
            get { return (ICollectionView)GetValue(CollectionViewblStatisticksOrderByMonthsProperty); }
            set { SetValue(CollectionViewblStatisticksOrderByMonthsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CollectionViewblStatisticksOrderByMonths.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CollectionViewblStatisticksOrderByMonthsProperty =
            DependencyProperty.Register("CollectionViewblStatisticksOrderByMonths", typeof(ICollectionView), typeof(StatisticksOrdersWindow));


        private IEnumerable<StatisticksOrderByMonth> statisticksOrderByMonths
        { get => (IEnumerable<StatisticksOrderByMonth>)GetValue(statisticksOrderByMonthsDep); set => SetValue(statisticksOrderByMonthsDep, value); }

        private static DependencyProperty statisticksOrderByMonthsDep = DependencyProperty.Register(nameof(statisticksOrderByMonths),
            typeof(IEnumerable<StatisticksOrderByMonth>), typeof(StatisticksOrdersWindow));

 
        public StatisticksOrdersWindow()

        { 
             statisticksOrderByMonths = bl.Order.GetStatisticksOrderByMonths()!;
            CollectionViewblStatisticksOrderByMonths = CollectionViewSource.GetDefaultView(statisticksOrderByMonths);

            propertyGroupDescription = new PropertyGroupDescription(groupName);
        CollectionViewblStatisticksOrderByMonths.GroupDescriptions.Add(propertyGroupDescription);
            InitializeComponent();
           
           
        }
    }
}

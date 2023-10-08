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
using LiveChartsCore.SkiaSharpView.VisualElements;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore.Defaults;
using System.Collections.ObjectModel;


namespace HoMMSpell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //public class NameList : ObservableCollection<Variable>
        //{
        //    public NameList() : base() 
        //    {
        //        Add(new Variable(0, "SpellPower", "SP", 1));
        //        Add(new Variable(1, "Knowledge", "KN", 1));

        //    }
        //}
        public ObservableCollection<Variable> variables;
        public MainWindow()
        {
            InitializeComponent();

            variables = new ObservableCollection<Variable>()
            {
                new Variable(0,"SpellPower","SP",1),
                new Variable(1, "Knowledge", "KN", 1)
            };
            //Добавляем ItemSource
            Test.ItemsSource = variables;
            Variables_Table.ItemsSource = variables;
            AllVarList.ItemsSource = variables;
            //    ObservableCollection<Variable> variables = new ObservableCollection<Variable>()
            //{
            //    new Variable {Id=0, Name="SpellPower", Syn="SP", Value=1},
            //    new Variable {Id=1, Name="Knowledge", Syn="KN", Value=1}
            //};
            //Variables_Table.ItemsSource = variables;

            //Variables_Table.ItemsSource = variables;

        }
        
        
        
        
        private void OpenSidebar_Button_Click(object sender, RoutedEventArgs e)
        {
            LeftBar.Visibility = Visibility.Visible;
            OpenSidebar_Button.Visibility = Visibility.Collapsed;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            LeftBar.Visibility = Visibility.Collapsed;


            OpenSidebar_Button.Visibility = Visibility.Visible;
        }

        private void Var_Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Выберите уже имеющуюся переменную или добавьте свою.", "Переменные", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Create_Variable create_Variable = new Create_Variable(variables);
            create_Variable.Owner = this;
            create_Variable.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //var f = e.
            //string t = PutBlock.Text+"+" + f;
            //PutBlock.Text = t;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (AllVarList.SelectedIndex== -1)
            {
                int x = 0;
                var eps = variables[x].Syn;
                string t = PutBlock.Text + eps;
                PutBlock.Text = t;
            }
            else
            {
                int x = AllVarList.SelectedIndex;
                var eps = variables[x].Syn;
                string t = PutBlock.Text + eps;
                PutBlock.Text = t;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PutBlock.Text = null;
        }

        private void AddPlus_Click(object sender, RoutedEventArgs e)
        {
            string t = PutBlock.Text+"+";
            PutBlock.Text = t;
        }

        private void AddMinus_Click(object sender, RoutedEventArgs e)
        {
            string t = PutBlock.Text+"-";
            PutBlock.Text = t;
        }

        private void AddMult_Click(object sender, RoutedEventArgs e)
        {
            string t = PutBlock.Text+"*";
            PutBlock.Text = t;
        }

        private void AddDivision_Click(object sender, RoutedEventArgs e)
        {
            string t = PutBlock.Text+"/";
            PutBlock.Text = t;
            
        }

       
    }

    public partial class ViewModel : ObservableObject
    {
        private readonly Random _random = new(); 
        public readonly ObservableCollection<ObservableValue> _observableValues;

        public ViewModel()
        {
            // Use ObservableCollections to let the chart listen for changes (or any INotifyCollectionChanged). 
            _observableValues = new ObservableCollection<ObservableValue>
        {
            // Use the ObservableValue or ObservablePoint types to let the chart listen for property changes 
            // or use any INotifyPropertyChanged implementation 
            new ObservableValue(2),
            new(5), // the ObservableValue type is redundant and inferred by the compiler (C# 9 and above)
            new(4),
            new(5),
            new(2),
            new(6),
            new(6),
            new(6),
            new(4),
            new(2),
            new(3),
            new(4),
            new(3)
        };

            Series = new ObservableCollection<ISeries>
        {
            new LineSeries<ObservableValue>
            {
                Values = _observableValues,
                Fill = null
            }
        };

            // in the following sample notice that the type int does not implement INotifyPropertyChanged
            // and our Series.Values property is of type List<T>
            // List<T> does not implement INotifyCollectionChanged
            // this means the following series is not listening for changes.
            // Series.Add(new ColumnSeries<int> { Values = new List<int> { 2, 4, 6, 1, 7, -2 } }); 
        }

        public ObservableCollection<ISeries> Series { get; set; }

        [RelayCommand]
        public void AddItem()
        {
            var randomValue = _random.Next(1, 10);
            _observableValues.Add(new(randomValue));
        }

        [RelayCommand]
        public void RemoveItem()
        {
            if (_observableValues.Count == 0) return;
            _observableValues.RemoveAt(0);
        }

        [RelayCommand]
        public void UpdateItem()
        {
            var randomValue = _random.Next(1, 10);

            // we grab the last instance in our collection
            var lastInstance = _observableValues[_observableValues.Count - 1];

            // finally modify the value property and the chart is updated!
            lastInstance.Value = randomValue;
        }

        [RelayCommand]
        public void ReplaceItem()
        {
            var randomValue = _random.Next(1, 10);
            var randomIndex = _random.Next(0, _observableValues.Count - 1);
            _observableValues[randomIndex] = new(randomValue);
        }

        [RelayCommand]
        public void AddSeries()
        {
            //  for this sample only 5 series are supported.
            if (Series.Count == 5) return;

            Series.Add(
                new LineSeries<int>
                {
                    Values = new List<int>
                    {
                    _random.Next(0, 10),
                    _random.Next(0, 10),
                    _random.Next(0, 10)
                    }
                });
        }

        [RelayCommand]
        public void RemoveSeries()
        {
            if (Series.Count == 1) return;

            Series.RemoveAt(Series.Count - 1);
        }
        [RelayCommand]
        public void BuiltChartCommand()
        {
            if(_observableValues.Count == 0) return;
        }
    }
}
    //public class ViewModel : ObservableObject
    //{

//    public ISeries[] Series { get; set; }
//        = new ISeries[]
//    {
//        new LineSeries<double>
//        {
//            Values = new double[] { 2, 1, 3, 5, 3, 4, 6 },
//            Fill = new SolidColorPaint(SKColors.Coral)
//        }
//    };
//public LabelVisual Title { get; set; } =
//new LabelVisual
//{
//    Text = "My chart title",
//    TextSize = 25,
//    Padding = new LiveChartsCore.Drawing.Padding(15),
//    Paint = new SolidColorPaint(SKColors.DarkSlateGray)
//};
//    }
//}

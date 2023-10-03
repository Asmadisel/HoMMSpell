﻿using System;
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
        public MainWindow()
        {
            InitializeComponent();
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
    }

    public partial class ViewModel : ObservableObject
    {
        private readonly Random _random = new();
        private readonly ObservableCollection<ObservableValue> _observableValues;

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

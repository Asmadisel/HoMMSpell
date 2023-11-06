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
using System.Data;
using System.Text.RegularExpressions;
using System.ComponentModel;

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

            //Дататаба для чтения формулы, к нему конектим object value
            //DataTable dt = new DataTable();

            //    ObservableCollection<Variable> variables = new ObservableCollection<Variable>()
            //{
            //    new Variable {Id=0, Name="SpellPower", Syn="SP", Value=1},
            //    new Variable {Id=1, Name="Knowledge", Syn="KN", Value=1}
            //};
            //Variables_Table.ItemsSource = variables;

            //Variables_Table.ItemsSource = variables;

            //Инициализируем базовыеуровни в построении графика
            //LvlTextBox.Text = "1, 5, 10, 15, 20";

            //Инициализируем значение для невидимого элемента
            DecipheredBlock.Text = "0";

            //Создадим новую коллекцию, в которой находятся только val из variables
             
                

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


        //Сейчас патблок выводит данные для формулы, необходимо заменить variablex.value на variables.syn
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
            string t = PutBlock.Text+" + ";
            PutBlock.Text = t;
        }

        private void AddMinus_Click(object sender, RoutedEventArgs e)
        {
            string t = PutBlock.Text+" - ";
            PutBlock.Text = t;
        }

        private void AddMult_Click(object sender, RoutedEventArgs e)
        {
            string t = PutBlock.Text+" * ";
            PutBlock.Text = t;
        }

        private void AddDivision_Click(object sender, RoutedEventArgs e)
        {
            string t = PutBlock.Text+" / ";
            PutBlock.Text = t;
            
        }
        
        private void AddDLvl_Click(object sender, RoutedEventArgs e)
        {
            string t = PutBlock.Text+"LvL";
            PutBlock.Text = t;
        }
        //Метод расчета из патблока, необходимо сопоставить в формуле slider.value к lvl
        //private void Button_Click_3(object sender, RoutedEventArgs e)
        //{
        //    if (PutBlock.Text == "" & PutBlock.Text == null) { return; }
        //    else
        //    {
        //        string formula = PutBlock.Text;
        //        DataTable dataTable = new DataTable();
        //        int m = (int)SlideLvl.Value;
        //        int[] values = new int[m];
        //        string answer = "";
        //        int lvl = 1;

        //        foreach (int i in values)
        //        {
        //            object result = dataTable.Compute(formula, "");
        //            answer= result.ToString();
        //            lvl++;
        //        }
        //        MessageBox.Show(answer);
        //    }
        //}

        private void LvlTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9,]+").IsMatch(e.Text);

        }

        private void LvlTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        //Метод ниже изменяет текст, обязательно наличие пробела между элементами - метод создает массив строк, которые уже потом конвертируются.
        private void PutBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            //string value = PutBlock.Text.Replace("\r\n", "").Replace(" ", "");
            var valueArray = PutBlock.Text.ToUpper().Split(' ');
            var i = 0;

            //Создаем коллекцию syn 
            var synonims = from s in variables select s.Syn;
            //Сопоставляем значения
            
            
            foreach (var value in valueArray)
            {
                var str = value;

                if (synonims.Contains(value))
                {

                    int index = synonims.Select((number, index) => new { number, index }).FirstOrDefault(item => item.number == value)?.index??-1;
                    //str = variables[index].Value.ToString();
                    str =  variables[index].Value.ToString() ;
                    
                    //MessageBox.Show($"Есть совпадение {index}");
                    valueArray[i]=str;
                }
                else { 
                    str = value;
                    valueArray[i]=str;
                    
                }
                i++;


            }
           

            //Возможно, в конце стоит ставить [*][lvl], зависит от архитектуры
            DecipheredBlock.Text = string.Join("", valueArray) ;
        }
    }

    public partial class ViewModel : ObservableObject, INotifyPropertyChanged
    {
        private readonly Random _random = new(); 
        public readonly ObservableCollection<ObservableValue> _observableValues;
        //public int ChartSize { get; set; }
        public string? ChartLvl { get; set; } = "1, 5, 10, 15, 20";

        //текст
        public string? PutBlockText { get; set; }
        //Здесь хранится введенная формула формата [][][][] - необходимо написать метод, который создает массив
        public string? DecipheredFormula 
        {
            get; set;
        } 
        //Почему в значение не попадает text? 




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
        //Метод, который принимает DecipheredFormula и убирает всё, кроме 0-9,+,-,/,* (string numberOnly = Regex.Replace(s, "[^0-9.]", ""))
        public string mathFormula(string? value)
        {
            var output = Regex.Replace(value, "[^0-9.]", "");
            return output;
        }


        //Отложенная команда формирует новый график, сейчас - просто рандомное значение относительно выбранного на слайдере диапазона
        [RelayCommand]
        public void BuiltChart()
        {
            //Получаем формулу
           //string? formula =  mathFormula(DecipheredFormula);
            MessageBox.Show($"{DecipheredFormula}");
            //string arr = ChartLvl;
            bool hasLetters = DecipheredFormula.AsEnumerable().Any(ch => char.IsLetter(ch));
            if(hasLetters == true)
            {
                MessageBox.Show("В выражении есть буквы, проверь");
                return;
            }
            //Используем DataTable
            DataTable dataTable= new DataTable();
            //Создаем первую колонку
            DataColumn column = new DataColumn("DecipheredFormula", typeof(double), DecipheredFormula);
            dataTable.Columns.Add(column);
            dataTable.Rows.Add(0);
            double result = (double)(dataTable.Rows[0]["DecipheredFormula"]);

            MessageBox.Show(result.ToString());

            if (ChartLvl == null) return;
            string form = ChartLvl.Replace(" ", "");
            
            double[] array = form.Split(new[] {',', ' '}).Select(n => Convert.ToDouble(n)).ToArray();
            if (array.Length == 0) { MessageBox.Show("Введите значение"); }
            else if (array.Length == _observableValues.Count) return;
            _observableValues.Clear();

            foreach (double i in array)
            {
                var value = result * i;
                _observableValues.Add(new(value));
            }
           // //if(_observableValues.Count == ChartSize) return;
           // _observableValues.Clear();
            
           //for(int count =0; count <= ChartSize; count++)
           // {
           //     var randomValue = _random.Next(1, 10);
           //     _observableValues.Add(new(randomValue));
           // }
        }

        [RelayCommand]
        public void ClearChart()
        { 
            
            _observableValues.Clear(); 
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

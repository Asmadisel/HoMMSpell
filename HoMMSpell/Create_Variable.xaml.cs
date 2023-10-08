using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace HoMMSpell
{
    /// <summary>
    /// Логика взаимодействия для Create_Variable.xaml
    /// </summary>
    public partial class Create_Variable : Window
    {
        private ObservableCollection<Variable> _variables;
        public Create_Variable(ObservableCollection<Variable> variables)
        {
            
            _variables = variables;
            InitializeComponent();
           
            
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text == null | Name.Text == "" ) 
            {
                MessageBox.Show("Ты че? Заполни поля");
            }
            else if (Syn.Text == null | Syn.Text == "")
            {
                MessageBox.Show("Ты че? Заполни поля");
            }
            else if (Value.Text == null | Value.Text == "")
            {
                MessageBox.Show("Ты че? Заполни поля");
            }
            else {
                int id = _variables.Count + 1;
                string? n = Name.Text;
                string? s = Syn.Text;
                int? v = Int32.Parse(Value.Text);
                //if (!(v.HasValue)) { new Exception("f"); }
                //else { int f = (int)v; }
                Variable vares = new Variable(id, n, s, (int)v);
                _variables.Add(vares);
                //Добавляем новую кнопку вниз
                
                this.Close();
            }
        }
    }
}

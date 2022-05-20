using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Olympiada_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try//Находим ошибки ввода
            {
                string path = @textBox1.Text;
                string[] lines = File.ReadAllLines(path);//Читаем все строки в файле и записываем их в массив
                int kolvo = 0;
                Int32.TryParse(lines[0], out kolvo);//Строка с числом свободного места на диске и числом пользователей           
                char[] numbers = lines[0].ToCharArray();
                int space = 0;
                for (int k = 0; k < numbers.Length; k++)//Ищем пробел в строке и записываем его номер в переменную
                {
                    if (numbers[k] == ' ')
                    {
                        space = k;
                    }
                }
                string volume = "";
                for (int l = 0; l < space; l++)// Записываем числа в строку до пробела
                {
                    volume += numbers[l];
                }
                string users_kol = "";
                for (int j = space; j < numbers.Length; j++)// Записываем числа в строку после пробела
                {
                    users_kol += numbers[j];
                }
                int users_kolvo = Int32.Parse(users_kol);//Кол-во пользователей
                int[] users_volume = new int[users_kolvo+1];//Создаём двухмерный массив рядов и мест
                int volume_int = Int32.Parse(volume);//Свободное место на диске

                for (int i = 1; i < users_volume.Length; i++)//Считываем строки из файла и записываем в массив
                {
                    users_volume[i] = Int32.Parse(lines[i]);
                }

                for (int i = 0; i < users_volume.Length; i++)
                {
                    for (int c = 0; c < users_volume.Length-1; c++)//Сортировка методом пузырька
                    {
                        if (users_volume[c] > users_volume[c + 1])
                        {
                            var temp = users_volume[c + 1];
                            users_volume[c + 1] = users_volume[c];
                            users_volume[c] = temp;
                        }
                    }
                }
                
                int max_users_volume = 0;
                int last= 0;
                for (int i = 0; i < users_volume.Length; i++)//Цикл для нахождения максимального количества пользователей,
                                                             //которые влезут в свободное место диска
                {
                    if (max_users_volume + users_volume[i] <= volume_int)
                    {
                        max_users_volume += users_volume[i];
                        last = i;//Последний пользователь
                    }
                }
                int last_user_volume = users_volume[last];//Объём последнего пользователя
                max_users_volume = max_users_volume - last_user_volume;
                for (int i = last + 1; i < users_kolvo; i++)//Цикл для нахождения максимального объёма файла
                {
                    if (max_users_volume + users_volume[i] <= volume_int)
                    {
                        last_user_volume = users_volume[i];
                    }
                }

                textBox2.Text = $"{last}, {last_user_volume}";//Вывод ответов
            }
            catch (IOException)//Если введён неправильный путь, выведет ошибку
            {
                MessageBox.Show("Неправильно введён путь файла");
            }
            catch (Exception)//Если в файле первая строка неправильного типа, выдаст ошибку
            {
                MessageBox.Show("Неправильный тип входных данных");
            }


        }
    }
}

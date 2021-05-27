using System;
using System.Windows.Forms;
using System.IO;

namespace laba6._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class Quadrangle
        {
            MyPoint A, B, C, D;
            Vector AB, BC, CD, DA;
            Vector d1, d2;

            public Quadrangle(double Ax, double Ay, double Bx, double By, double Cx, double Cy, double Dx, double Dy)//четырехугольник
            {
                A = new MyPoint(Convert.ToDouble(Ax), Convert.ToDouble(Ay));
                B = new MyPoint(Convert.ToDouble(Bx), Convert.ToDouble(By));
                C = new MyPoint(Convert.ToDouble(Cx), Convert.ToDouble(Cy));
                D = new MyPoint(Convert.ToDouble(Dx), Convert.ToDouble(Dy));

                AB = new Vector(A, B);
                BC = new Vector(B, C);
                CD = new Vector(C, D);
                DA = new Vector(D, A);
                d1 = new Vector(A, C);
                d2 = new Vector(B, D);
            }
            public class MyPoint
            {
                public double X;
                public double Y;
                public MyPoint(double X, double Y)
                {
                    this.X = X;
                    this.Y = Y;
                }
                public static bool operator !=(MyPoint p1, MyPoint p2)
                {
                    if (p1.X == p2.X && p1.Y == p2.Y) return false;
                    else return true;
                }
                public static bool operator ==(MyPoint p1, MyPoint p2)
                {
                    if (p1.X == p2.X && p1.Y == p2.Y) return true;
                    else return false;
                }
            }
            public class Vector
            {
                public double l;//dlina
                public MyPoint coordinates;
                public Vector(MyPoint p1, MyPoint p2)
                {
                    coordinates = new MyPoint(p2.X - p1.X, p2.Y - p1.Y);
                    l = Math.Sqrt(Math.Pow(coordinates.X, 2) + Math.Pow(coordinates.Y, 2));
                }
            }
            public string Process()
            {
                if (A == B || A == C || A == D || B == C || B == D || C == D) return "Ошибка";
                if (AB.l == BC.l && BC.l == CD.l && CD.l == DA.l)
                {
                    if (d1.l == d2.l)
                        return "Квадрат";
                    else
                        return "Ромб";
                }
                else if (AB.l == CD.l && BC.l == DA.l)
                {
                    if (d1.l == d2.l)
                        return "Прямоугольник";
                    else
                        return "Параллелограмм";
                }
                else if (parallel(AB, CD) || parallel(BC, DA))
                {
                    if (pryamoyugol(AB, DA) || pryamoyugol(BC, CD))
                        return "Прямоугольнаятрапеция";
                    else if (AB.l == CD.l || BC.l == DA.l)
                        return "Равнобедренная трапеция";
                    else
                        return "Трапеция общего вида";
                }
                else
                    return "Четырехугольник общего вида";
            }
            private bool parallel(Vector one, Vector two)
            {
                if (two.coordinates.X * one.coordinates.Y == two.coordinates.Y * one.coordinates.X)
                    return true;
                else return false;
            }

            private bool pryamoyugol(Vector one, Vector two)
            {
                if ((one.coordinates.X * two.coordinates.X + one.coordinates.Y * two.coordinates.X) == 0)
                    return true;
                else return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)//загрузка файла, считывание данных для тестирования
        {
            dataGridView1.Rows.Clear();
            FileStream file;
            try
            {
                file = new FileStream(textBox1.Text, FileMode.Open, FileAccess.Read); //открытие существующего файла для чтения
            }
            catch (Exception)
            {
                textBox1.Text = "Ошибкаоткрытияфайла";
                return;
            }
            StreamReader reader = new StreamReader(file); // создаем «потоковый читатель» и связываем его с файловым потоком 
            int i = 0;
            string[] lines = new string[10];
            while (!reader.EndOfStream)
            {
                i++;
                lines = reader.ReadLine().Split(new char[] { ' ', '\n' });
                dataGridView1.Rows.Add(i, lines[0], lines[1], lines[2], lines[3], lines[4], lines[5], lines[6], lines[7], lines[8]);
                switch (lines[9])
                {
                    case "1":
                        dataGridView1["ORezult", dataGridView1.Rows.Count - 1].Value = "Квадрат";
                        break;

                    case "2":
                        dataGridView1["ORezult", dataGridView1.Rows.Count - 1].Value = "Ромб";
                        break;

                    case "3":
                        dataGridView1["ORezult", dataGridView1.Rows.Count - 1].Value = "Прямоугольник";
                        break;

                    case "4":
                        dataGridView1["ORezult", dataGridView1.Rows.Count - 1].Value = "Параллелограмм";
                        break;

                    case "5":
                        dataGridView1["ORezult", dataGridView1.Rows.Count - 1].Value = "Прямоугольная трапеция";
                        break;

                    case "6":
                        dataGridView1["ORezult", dataGridView1.Rows.Count - 1].Value = "Равнобедренная трапеция";
                        break;
                    case "7":
                        dataGridView1["ORezult", dataGridView1.Rows.Count - 1].Value = "Трапеция общего вида";
                        break;
                    case "8":
                        dataGridView1["ORezult", dataGridView1.Rows.Count - 1].Value = "Четырехугольник общего вида";
                        break;
                    default:
                        dataGridView1["ORezult", dataGridView1.Rows.Count - 1].Value = "Ошибка";
                        break;
                }
            }
            reader.Close();
            dataGridView1.Rows[0].Cells[0].Selected = false;
            return;
        }

        private void Form1_Load(object sender, EventArgs e)//выполняется при запуске программы, создание столбцов таблицы
        {
            var column0 = new DataGridViewColumn();
            column0.HeaderText = "Номер теста"; //текст в шапке
            column0.Width = 50; //ширина колонки
            column0.ReadOnly = true; //значение в этой колонке нельзя править
            column0.Name = "number"; //текстовое имя колонки, его можно использовать вместо обращений по индексу
            column0.CellTemplate = new DataGridViewTextBoxCell(); //тип нашей колонки

            var column1 = new DataGridViewColumn();
            column1.HeaderText = "Назначение теста";
            column1.Width = 70;
            column1.Name = "Purpose";
            column1.CellTemplate = new DataGridViewTextBoxCell();

            var column2 = new DataGridViewColumn();
            column2.HeaderText = "Ax";
            column2.Width = 30;
            column2.Name = "Ax";
            column2.CellTemplate = new DataGridViewTextBoxCell();

            var column3 = new DataGridViewColumn();
            column3.HeaderText = "Ay";
            column3.Width = 30;
            column3.Name = "Ay";
            column3.CellTemplate = new DataGridViewTextBoxCell();

            var column4 = new DataGridViewColumn();
            column4.HeaderText = "Bx";
            column4.Width = 30;
            column4.Name = "Bx";
            column4.CellTemplate = new DataGridViewTextBoxCell();

            var column5 = new DataGridViewColumn();
            column5.HeaderText = "By";
            column5.Width = 30;
            column5.Name = "By";
            column5.CellTemplate = new DataGridViewTextBoxCell();

            var column6 = new DataGridViewColumn();
            column6.HeaderText = "Сx";
            column6.Width = 30;
            column6.Name = "Сx";
            column6.CellTemplate = new DataGridViewTextBoxCell();

            var column7 = new DataGridViewColumn();
            column7.HeaderText = "Сy";
            column7.Width = 30;
            column7.Name = "Сy";
            column7.CellTemplate = new DataGridViewTextBoxCell();

            var column8 = new DataGridViewColumn();
            column8.HeaderText = "Dx";
            column8.Width = 30;
            column8.Name = "Dx";
            column8.CellTemplate = new DataGridViewTextBoxCell();

            var column9 = new DataGridViewColumn();
            column9.HeaderText = "Dy";
            column9.Width = 30;
            column9.Name = "Dy";
            column9.CellTemplate = new DataGridViewTextBoxCell();

            var column10 = new DataGridViewColumn();
            column10.HeaderText = "Ожидаемый результат";
            column10.Width = 170; //ширина колонки
            column10.Name = "ORezult";
            column10.CellTemplate = new DataGridViewTextBoxCell();

            var column11 = new DataGridViewColumn();
            column11.HeaderText = "Реакция программы";
            column11.Width = 170; //ширина колонки
            column11.Name = "Rezult";
            column11.CellTemplate = new DataGridViewTextBoxCell();

            var column12 = new DataGridViewColumn();
            column12.HeaderText = "Bывод";
            column12.Width = 50; //ширина колонки
            column12.Name = "Decision";
            column12.CellTemplate = new DataGridViewTextBoxCell();

            dataGridView1.Columns.Add(column0);
            dataGridView1.Columns.Add(column1);
            dataGridView1.Columns.Add(column2);
            dataGridView1.Columns.Add(column3);
            dataGridView1.Columns.Add(column4);
            dataGridView1.Columns.Add(column5);
            dataGridView1.Columns.Add(column6);
            dataGridView1.Columns.Add(column7);
            dataGridView1.Columns.Add(column8);
            dataGridView1.Columns.Add(column9);
            dataGridView1.Columns.Add(column10);
            dataGridView1.Columns.Add(column11);
            dataGridView1.Columns.Add(column12);

            dataGridView1.AllowUserToAddRows = false; //запрешаем пользователю самому добавлять строки

            return;
        }

        private void button2_Click(object sender, EventArgs e)//тестирование, сравнение результата с ожидаемым
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                Quadrangle tek = new Quadrangle(Convert.ToDouble(dataGridView1[2, i].Value), Convert.ToDouble(dataGridView1[3, i].Value),
                Convert.ToDouble(dataGridView1[4, i].Value), Convert.ToDouble(dataGridView1[5, i].Value),
                Convert.ToDouble(dataGridView1[6, i].Value), Convert.ToDouble(dataGridView1[7, i].Value),
                Convert.ToDouble(dataGridView1[8, i].Value), Convert.ToDouble(dataGridView1[9, i].Value));
                dataGridView1["Rezult", i].Value = tek.Process();

                if (Convert.ToString(dataGridView1[10, i].Value) == Convert.ToString(dataGridView1[11, i].Value)) dataGridView1["Decision", i].Value = "+";
                else dataGridView1["Decision", i].Value = "-";
            }
            return;
        }
    }
}

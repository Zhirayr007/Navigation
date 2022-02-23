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

namespace Navigation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        double k = 10; //шаг 
        const double c = 300000;// скорость света
        int calc;
        double R1, R2, R3, R4;
        double[] Mass_koor = new double[3];
        //===Испорченный координаты
       // double X1_, X2_, X3_, X4_, Y1_, Y2_, Y3_, Y4_, Z1_, Z2_, Z3_, Z4_=0 ;

        //Спутники
        KoorSput[] sput = new KoorSput[4];

        //Параметры
        double delta_t12, delta_t23, delta_t34, delta_t41;
        double delta_R12, delta_R23, delta_R34, delta_R41;


        //Испорченные параметры
        double delta_t12_1, delta_t23_1, delta_t34_1, delta_t41_1;
        double delta_R12_1, delta_R23_1, delta_R34_1, delta_R41_1;

        //double X, Y, Z, X1,Y1, Z1, X2, Y2, Z2, X3, Y3, Z3, X4, Y4, Z4;
        double X, Y, Z;
        void Intilizate()//Функция для инцилизации данных
        {
            for (int i = 0; i < 4; i++)
            {
                sput[i] = new KoorSput();
            }
            // координаты источника
            X = Convert.ToDouble(textBox_Z.Text);
            Y = Convert.ToDouble(textBox_Y.Text);
            Z = Convert.ToDouble(textBox_X.Text);
            ///=====================================

            sput[0].X = Convert.ToDouble(textBox_X1.Text);
            sput[0].Y = Convert.ToDouble(textBox_Y1.Text);
            sput[0].Z = Convert.ToDouble(textBox_Z1.Text);
            sput[1].X = Convert.ToDouble(textBox_X2.Text);
            sput[1].Y = Convert.ToDouble(textBox_Y2.Text);
            sput[1].Z = Convert.ToDouble(textBox_Z2.Text);
            sput[2].X = Convert.ToDouble(textBox_X3.Text);
            sput[2].Y = Convert.ToDouble(textBox_Z3.Text);
            sput[2].Z = Convert.ToDouble(textBox_Y3.Text);
            sput[3].X = Convert.ToDouble(textBox_X4.Text);
            sput[3].Y = Convert.ToDouble(textBox_Z4.Text);
            sput[3].Z = Convert.ToDouble(textBox_Y4.Text);

            //sput[0].R(X, Y, Z);
            //sput[1].R(X, Y, Z);
            //sput[2].R(X, Y, Z);
            //sput[3].R(X, Y, Z);
        }
        
        void deltaR(double X, double X1, double X2, double X3, double X4,
                    double Y, double Y1, double Y2, double Y3, double Y4,
                    double Z, double Z1, double Z2, double Z3, double Z4)
        {
             R1 = Math.Sqrt((X - X1) * (X - X1) + (Y - Y1) * (Y - Y1) + (Z - Z1) * (Z - Z1));
             R2 = Math.Sqrt((X - X2) * (X - X2) + (Y - Y2) * (Y - Y2) + (Z - Z2) * (Z - Z2));
             R3 = Math.Sqrt((X - X3) * (X - X3) + (Y - Y3) * (Y - Y3) + (Z - Z3) * (Z - Z3));
             R4 = Math.Sqrt((X - X4) * (X - X4) + (Y - Y4) * (Y - Y4) + (Z - Z4) * (Z - Z4));

             delta_R12 = R1 - R2;
             delta_R23 = R2 - R3;
             delta_R34 = R3 - R4;
             delta_R41 = R4 - R1;
        }


        
        private void START_Click(object sender, EventArgs e)
        {
             Intilizate();
            delta_R12 = sput[0].R(X, Y, Z) - sput[1].R(X, Y, Z); 
            delta_R23 = sput[1].R(X, Y, Z) - sput[2].R(X, Y, Z); 
            delta_R34 = sput[2].R(X, Y, Z) - sput[3].R(X, Y, Z) ;
            delta_R41 = sput[3].R(X, Y, Z) - sput[0].R(X, Y, Z); 

            //Находим дельта t
            delta_t12 = delta_R12 / c;
            delta_t23 = delta_R23 / c;
            delta_t34 = delta_R34 / c;
            delta_t41 = delta_R41 / c;

          

            //Портим параметры
            Random random = new Random();
   
            delta_t12_1 = delta_t12 + random.Next(-50,50) / 100000;
            delta_t23_1 = delta_t23 + random.Next(-50, 50) / 100000;
            delta_t34_1 = delta_t34 + random.Next(-50, 50) / 100000;
            delta_t41_1 = delta_t41 + random.Next(-50, 50) / 100000;
            delta_R12_1 = delta_R12;// + random.NextDouble();
            delta_R23_1 = delta_R23;// + random.NextDouble();
            delta_R34_1 = delta_R34;// + random.NextDouble();
            delta_R41_1 = delta_R41;// + random.NextDouble();


            double Functional = 0;

            double [] Mass_koor1 = { X, Y, Z };

            Functional = MHJ(3,Mass_koor1);

            textBox_Xnew.Text= Convert.ToString(Mass_koor1[0]);
            textBox_Ynew.Text = Convert.ToString(Mass_koor1[1]);
            textBox_Znew.Text = Convert.ToString(Mass_koor1[2]);
            textBox_Func.Text = Convert.ToString(Functional);
        }


        private void buttonGraff_Click(object sender, EventArgs e)
        {
            Intilizate();

            //deltaR(X, X1, X2, X3, X4, Y, Y1, Y2, Y3, Y4, Z, Z1, Z2, Z3, Z4);
            //chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            //chart1.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;

            ////Находим дельта t
            //delta_t12 = delta_R12 / c;
            //delta_t23 = delta_R23 / c;
            //delta_t34 = delta_R34 / c;
            //delta_t41 = delta_R41 / c;

           
            //delta_t12_1 = delta_t12;
            //delta_t23_1 = delta_t23;
            //delta_t34_1 = delta_t34;
            //delta_t41_1 = delta_t41;
            ////соохраняем истенные координаты
            //X1_ = X1; Y1_ = Y1; Z1_ = Z1; X2_ = X2; Y2_ = Y2; Z2_ = Z2;
            //X3_ = X3; Y3_ = Y3; Z3_ = Z3; X4_ = X4; Y4_ = Y4; Z4_ = Z4;


            //X1 = Y1 = Z1 = X2 = Y2 = Z2 = X3 = Y3 = Z3 = X4 = Y4 = Z4 = 0;


            //double SKO = 0;
            //double xSred = 0;
            //double ySred = 0;
            //double zSred = 0;
            //int N = 100;
            //double[] koor_x = new double[N];
            //double[] koor_y = new double[N];
            //double[] koor_z = new double[N];
            //double Functional = 0;
            //double mastab = 1000;
            //chart1.Series[0].Points.Clear();
            //// Настройка области рисования
            //chart1.ChartAreas[0].AxisX.Minimum = 0;
            //chart1.ChartAreas[0].AxisX.Maximum = 100;
            //chart1.ChartAreas[0].AxisX.Interval = 10;
            ////chart.ChartAreas[0].AxisY.Minimum = minY;
            ////chart.ChartAreas[0].AxisY.Maximum = maxY;
            ////chart.ChartAreas[0].AxisY.Interval = (maxY - minY) / 8;

            ////chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            ////chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            ////chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            ////chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

            ////chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
            ////chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            ////chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            ////chart1.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;

           
            //double[] list = new double[N];
            //Random random = new Random();
            //for (int p = 0; p < 100; p++)
            //{             
            //    xSred = 0;
            //    ySred = 0;
            //    zSred = 0;
            //    for (int i = 0; i < N; i++)
            //    {
            //        //шумим координаты спутников
            //        X1 = X1_ + (double)random.Next(-100, 100) * p / mastab; 
            //        Y1 = Y1_ + (double)random.Next(-100, 100) * p / mastab;
            //        Z1 = Z1_ + (double)random.Next(-100, 100) * p / mastab;
            //        X2 = X2_ + (double)random.Next(-100, 100) * p / mastab;
            //        Y2 = Y2_ + (double)random.Next(-100, 100) * p / mastab;
            //        Z2 = Z2_ + (double)random.Next(-100, 100) * p / mastab;
            //        X3 = X3_ + (double)random.Next(-100, 100) * p / mastab;
            //        Y3 = Y3_ + (double)random.Next(-100, 100) * p / mastab;
            //        Z3 = Z3_ + (double)random.Next(-100, 100) * p / mastab;
            //        X4 = X4_ + (double)random.Next(-100, 100) * p / mastab;
            //        Y4 = Y4_ + (double)random.Next(-100, 100) * p / mastab;
            //        Z4 = Z4_ + (double)random.Next(-100, 100) * p / mastab;

            //        Mass_koor[0] = 0;//X;
            //        Mass_koor[1] = 0;//Y;
            //        Mass_koor[2] = 0;//Z;
            //        Functional = MHJ(3, Mass_koor);
            //        koor_x[i] = Mass_koor[0];
            //        koor_y[i] = Mass_koor[1];
            //        koor_z[i] = Mass_koor[2];
            //        xSred += (Mass_koor[0] / N);//считаем среднее
            //        ySred += (Mass_koor[1] / N);
            //        zSred += (Mass_koor[2] / N);
            //    }

            //    double R123 = 0;
            //    for (int k = 0; k < koor_x.Length; k++)
            //    {
            //        R123 += ((koor_x[k] - xSred) * (koor_x[k] - xSred)) +
            //        ((koor_y[k] - ySred) * (koor_y[k] - ySred)) +
            //        ((koor_z[k] - zSred) * (koor_z[k] - zSred));
            //    }

            //    SKO = Math.Sqrt(R123 / (double)N);
            //    list[p] = SKO;
            //    chart1.Series[0].Points.AddXY(p, SKO);
                
                
            //}
            //StreamWriter print = new StreamWriter("in.txt", false); // перезапись в файл
            //print.Write("Print massiv:"); // запись в файл строки
            //for (int i = 0; i < N; i++)
            //{
            //    print.WriteLine();
            //    print.Write(list[i]); // запись в файл массива
                
            //}
            //print.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Intilizate();
            //double mass_per = 1;
            //X = X / mass_per; Y = Y / mass_per; Z = Z / mass_per;
            //X1 = X1 / mass_per; Y1 = Y1 / mass_per; Z1 = Z1 / mass_per; X2 = X2 / mass_per; Y2 = Y2 / mass_per; Z2 = Z2 / mass_per;
            //X3 = X3 / mass_per; Y3 = Y3 / mass_per; Z3 = Z3 / mass_per; X4 = X4 / mass_per; Y4 = Y4 / mass_per; Z4 = Z4 / mass_per;
            //deltaR(X, X1, X2, X3, X4, Y, Y1, Y2, Y3, Y4, Z, Z1, Z2, Z3, Z4);
            //chart2.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            //chart2.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;

            ////Находим дельта t
            //delta_t12 = delta_R12 / c;
            //delta_t23 = delta_R23 / c;
            //delta_t34 = delta_R34 / c;
            //delta_t41 = delta_R41 / c;
            //chart2.ChartAreas[0].AxisX.Minimum = 0;
            //chart2.ChartAreas[0].AxisX.Maximum = 10;
            //chart2.ChartAreas[0].AxisX.Interval = 1;


            ////X = X / mass_per; Y = Y / mass_per; Z = Z / mass_per;
            ////X1 = X1/ mass_per; Y1 = Y1/ mass_per; Z1 = Z1/ mass_per; X2 = X2/ mass_per; Y2 = Y2/ mass_per; Z2 = Z2/ mass_per;
            ////X3 = X3/ mass_per; Y3 = Y3/ mass_per; Z3 = Z3/ mass_per; X4 = X4/ mass_per; Y4 = Y4/ mass_per; Z4 = Z4/ mass_per;
            //double SKO = 0;
            //double xSred = 0;
            //double ySred = 0;
            //double zSred = 0;
            //int N = 100;
            //double[] koor_x = new double[N];
            //double[] koor_y = new double[N];
            //double[] koor_z = new double[N];
            //double micro = 100000;
            //double Functional = 0;
            //chart2.Series[0].Points.Clear();
            //Random random = new Random();
            //double[] list = new double[N];
            //double[] list2 = new double[N];
            //for (int p = 0; p < 100; p++)
            //{
            //    xSred = 0;
            //    ySred = 0;
            //    zSred = 0;
            //    for (int i = 0; i < N; i++)
            //    {
            //        //шумим координаты спутников
            //        //delta_t12_1 = delta_t12 + (double)random.Next(-50, 50) * p / micro;
            //        //delta_t23_1 = delta_t23 + (double)random.Next(-50, 50) * p / micro;
            //        //delta_t34_1 = delta_t34 + (double)random.Next(-50, 50) * p / micro;
            //        //delta_t41_1 = delta_t41 + (double)random.Next(-50, 50) * p / micro;
            //        //delta_t12_1 = delta_t12 + random.NextDouble() * p / micro;
            //        delta_t12_1 = delta_t12 + (double)random.Next(0, p) / micro;
            //        delta_t23_1 = delta_t23 + (double)random.Next(0, p) / micro;
            //        delta_t34_1 = delta_t34 + (double)random.Next(0, p) / micro;
            //        delta_t41_1 = delta_t41 + (double)random.Next(0, p) / micro;

            //        Mass_koor[0] = 0;//X;
            //        Mass_koor[1] = 0;//Y;
            //        Mass_koor[2] = 0;//Z;
            //        Functional = MHJ(3, Mass_koor);
            //        koor_x[i] = Mass_koor[0];
            //        koor_y[i] = Mass_koor[1];
            //        koor_z[i] = Mass_koor[2];
            //        xSred += (Mass_koor[0] / N);//считаем среднее
            //        ySred += (Mass_koor[1] / N);
            //        zSred += (Mass_koor[2] / N);
            //    }

            //    double R123 = 0;
            //    for (int k = 0; k < koor_x.Length; k++)
            //    {
            //        R123 += ((koor_x[k] - xSred) * (koor_x[k] - xSred)) +
            //        ((koor_y[k] - ySred) * (koor_y[k] - ySred)) +
            //        ((koor_z[k] - zSred) * (koor_z[k] - zSred));
            //    }

            //    SKO = Math.Sqrt(R123 / (double)N);
            //    list[p] = SKO;
            //    list2[p] = (double)p / 10;
            //    chart2.Series[0].Points.AddXY((double)p/10, SKO);

            //}
            //StreamWriter print = new StreamWriter("intwo.txt", false); // перезапись в файл
            //print.Write("Print massiv:"); // запись в файл строки
            //for (int i = 0; i < N; i++)
            //{
            //    print.WriteLine();
            //    print.Write(list[i]); // запись в файл массива

            //}
            //print.Close();
            //StreamWriter print1 = new StreamWriter("intwo2.txt", false); // перезапись в файл
            //print1.Write("Print massiv:"); // запись в файл строки
            //for (int i = 0; i < N; i++)
            //{
            //    print1.WriteLine();
            //    print1.Write(list2[i]); // запись в файл массива

            //}
            //print1.Close();
        }

        /// <summary>
        /// Реализует оптимизируемую функцию
        /// Возвращает значение функции
        /// </summary>
        public double function(double[] x)
        {
            //количество параметров является членом класса, в противном случае изменить сигнатуру функции

            delta_R12 = sput[0].R(x[0], x[1], x[2]) - sput[1].R(x[0], x[1], x[2]);
            delta_R23 = sput[1].R(x[0], x[1], x[2]) - sput[2].R(x[0], x[1], x[2]);
            delta_R34 = sput[2].R(x[0], x[1], x[2]) - sput[3].R(x[0], x[1], x[2]);
            delta_R41 = sput[3].R(x[0], x[1], x[2]) - sput[0].R(x[0], x[1], x[2]);
            double[] delta_R = { delta_R12, delta_R23, delta_R34, delta_R41 };
            double[] delta_t = { delta_t12_1, delta_t23_1, delta_t34_1, delta_t41_1 };
            double Func = 0;

            for (int i = 0; i < delta_R.Length; i++)
            {
                Func += (c * delta_t[i] - delta_R[i]) * (c * delta_t[i] - delta_R[i]);
            }
            return Func;

        }

        public double MHJ(int kk, double[] x)
        {
            // kk - количество параметров; x - массив параметров
            // float TAU = 1.e - 6f; // Точность вычислений
            double TAU = 0.000001F;
            int i, j, bs, ps;
            double z, h, k, fi, fb;
            double[] b = new double[kk];
            double[] y = new double[kk];
            double[] p = new double[kk];


            Random random = new Random();

            h = 10F;
            x[0] = 1F;
            for (i = 1; i < kk; i++) x[i] = (double)random.Next(0, 100) / 100; ; // Задается начальное приближение

            k = h;
            for (i = 0; i < kk; i++) y[i] = p[i] = b[i] = x[i];
            fi = function(x);
            ps = 0; bs = 1; fb = fi;

            j = 0;
            while (true)
            {
                calc++; // Счетчик итераций. Можно игнорировать

                x[j] = y[j] + k;
                z = function(x);
                if (z >= fi)
                {
                    x[j] = y[j] - k;
                    z = function(x);
                    if (z < fi) y[j] = x[j];
                    else x[j] = y[j];
                }
                else y[j] = x[j];
                fi = function(x);

                if (j < kk - 1) { j++; continue; }
                if (fi + 1e-8 >= fb)
                {
                    if (ps == 1 && bs == 0)
                    {
                        for (i = 0; i < kk; i++)
                        {
                            p[i] = y[i] = x[i] = b[i];
                        }
                        z = function(x);
                        bs = 1; ps = 0; fi = z; fb = z; j = 0;
                        continue;
                    }
                    k /= 10F;
                    if (k < TAU) break;
                    j = 0;
                    continue;
                }

                for (i = 0; i < kk; i++)
                {
                    p[i] = 2 * y[i] - b[i];
                    b[i] = y[i];
                    x[i] = p[i];
                    y[i] = x[i];
                }
                z = function(x);
                fb = fi; ps = 1; bs = 0; fi = z; j = 0;
            } //  end of while(1)

            for (i = 0; i < kk; i++) x[i] = p[i];

            return fb;
        }
    }
}

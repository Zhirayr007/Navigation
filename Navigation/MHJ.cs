using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigation
{
    class MHJ
    {
        // Переменные восстановителя:
        Int32 calc;
        Double TAU;
        Int32 j, bs, ps;
        Double z, h, k, fi, fb;
        Double[] b, y, p;

        Double[] Lambda_data;
        Double[] Im_h_data;
        Double[] OutSignal_data;

        public int GetCalc()
        {
            return calc;
        }
        //Свертка
        public static Double[] Convol(Double[] Data_Left, Double[] Data_Right)
        {
            Double[] Result = new Double[Data_Left.Length];
            // Свертка
            double iter;
            for (int k = 0; k < Result.Length; k++)
            {
                iter = 0;
                for (int p = 0; p < Result.Length; p++)
                {
                    int m = k - p;
                    if (m < 0)
                    {
                        m += Result.Length;
                    }
                    iter += Data_Right[m] * Data_Left[p];
                }
                Result[k] = iter;

            }


            return Result;
        }

        // Вычисление значений функции по параметрам максимума энтропии:
        public static Double[] RecSignal(Double[] Lambda_data, Double[] Im_h_data)
        {
            Double[] Recovery_data = new Double[Im_h_data.Length];

            double[] Svertk = new Double[Im_h_data.Length];

            for (UInt16 i = 0; i < Recovery_data.Length; i++)
            {

                // Свертка

                double iter;
                for (int k = 0; k < Recovery_data.Length; k++)
                {
                    iter = 0;
                    for (int p = 0; p < Recovery_data.Length; p++)
                    {
                        int m = k - p;
                        if (m < 0)
                        {
                            m += Recovery_data.Length;
                        }
                        iter += Im_h_data[m] * Lambda_data[p];
                    }
                    Svertk[k] = iter;

                }
                Recovery_data[i] = Math.Exp(-1.0 - Svertk[i]);

            }
            return Recovery_data;
        }

        // Вычисление значение функционала наименьших квадратов:
        private Double GenFunct(Double[] Lambda_data, Double[] Im_h_data, Double[] OutSignalData)
        {
            Double Functional = 0.0;

            Double[] RecoveryData = MHJ.RecSignal(Lambda_data, Im_h_data);
            RecoveryData = Convol(RecoveryData, Im_h_data);

            for (UInt16 i = 0; i < OutSignalData.Length; i++)
            {
                Functional += (RecoveryData[i] - OutSignalData[i]) * (RecoveryData[i] - OutSignalData[i]);
            }

            return Functional;
        }

        // Создание начального приближения:
        public Double[] InitRec(Double[] SystemInput, Double[] OutSignalInput, UInt16 Prec)
        {
            var rand = new Random();

            TAU = Math.Pow(10.0, -((double)Prec));  // Точность вычислений
            UInt16 LengthData = (ushort)(OutSignalInput.Length);

            calc = 0;
            b = new Double[LengthData];
            y = new Double[LengthData];
            p = new Double[LengthData];
            Lambda_data = new Double[LengthData];
            Im_h_data = SystemInput;
            OutSignal_data = OutSignalInput;

            h = 1.0;
            // Начальное приближение:
            Lambda_data[0] = 1.0;
            for (Int32 i = 1; i < LengthData; i++) { Lambda_data[i] = rand.NextDouble(); }

            k = h;
            for (Int32 i = 0; i < LengthData; i++) { y[i] = p[i] = b[i] = Lambda_data[i]; }
            fi = GenFunct(Lambda_data, Im_h_data, OutSignal_data);

            ps = 0; bs = 1; fb = fi;

            j = 0;

            return RecSignal(Lambda_data, Im_h_data);
        }

        // Итерация приближения:
        public bool Iterate(ref Double[] RecoveryOutput)
        {
            calc++; // Счетчик итераций

            Lambda_data[j] = y[j] + k;
            z = GenFunct(Lambda_data, Im_h_data, OutSignal_data);
            if (z >= fi)
            {
                Lambda_data[j] = y[j] - k;
                z = GenFunct(Lambda_data, Im_h_data, OutSignal_data);
                if (z < fi)
                {
                    y[j] = Lambda_data[j];
                }
                else
                {
                    Lambda_data[j] = y[j];
                }
            }
            else
            {
                y[j] = Lambda_data[j];
            }
            fi = GenFunct(Lambda_data, Im_h_data, OutSignal_data);

            if (j < Lambda_data.Length - 1)
            {
                RecoveryOutput = RecSignal(Lambda_data, Im_h_data);
                j++;
                return false;
            }
            if (fi + 1e-8 >= fb)
            {
                if (ps == 1 && bs == 0)
                {
                    for (Int32 i = 0; i < Lambda_data.Length; i++)
                    {
                        p[i] = y[i] = Lambda_data[i] = b[i];
                    }
                    z = GenFunct(Lambda_data, Im_h_data, OutSignal_data);
                    bs = 1; ps = 0; fi = z; fb = z; j = 0;

                    RecoveryOutput = RecSignal(Lambda_data, Im_h_data);
                    return false;
                }
                k /= 10.0;
                if (k < TAU)
                {
                    for (Int32 i = 0; i < Lambda_data.Length; i++) { Lambda_data[i] = p[i]; }
                    RecoveryOutput = RecSignal(Lambda_data, Im_h_data);
                    return true;
                }
                j = 0;
                RecoveryOutput = RecSignal(Lambda_data, Im_h_data);
                return false;
            }

            for (Int32 i = 0; i < Lambda_data.Length; i++)
            {
                p[i] = 2 * y[i] - b[i];
                b[i] = y[i];
                Lambda_data[i] = p[i];
                y[i] = Lambda_data[i];
            }
            z = GenFunct(Lambda_data, Im_h_data, OutSignal_data);
            fb = fi; ps = 1; bs = 0; fi = z; j = 0;

            RecoveryOutput = RecSignal(Lambda_data, Im_h_data);
            return false;
        }
    }
}

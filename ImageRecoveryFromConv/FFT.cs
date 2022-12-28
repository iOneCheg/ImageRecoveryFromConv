using System;
using System.Numerics;

namespace ImageRecoveryFromConv
{
    /// <summary>
    /// Быстрое преобразование Фурье.
    /// </summary>
    public static class FFT
    {
        public const double DoublePi = 2 * Math.PI;
        /// <summary>
        /// Децимация по частоте.
        /// </summary>
        /// <param name="frame">Массив комлексных чисел.</param>
        /// <param name="direct">Прямой ход?</param>
        /// <returns></returns>
        public static Complex[] FourierTransform(Complex[] frame, bool direct)
        {
            if (frame.Length == 1) return frame;
            int halfSampleSize = frame.Length >> 1; // frame.Length/2;
            int fullSampleSize = frame.Length;

            double arg = direct ? -DoublePi / fullSampleSize : DoublePi / fullSampleSize;
            Complex omegaPowBase = new Complex(Math.Cos(arg), Math.Sin(arg));
            Complex omega = Complex.One;
            Complex[] spectrum = new Complex[fullSampleSize];

            for (int j = 0; j < halfSampleSize; j++)
            {
                spectrum[j] = frame[j] + frame[j + halfSampleSize];
                spectrum[j + halfSampleSize] = omega * (frame[j] - frame[j + halfSampleSize]);
                omega *= omegaPowBase;
            }

            Complex[] yTop = new Complex[halfSampleSize];
            Complex[] yBottom = new Complex[halfSampleSize];
            for (int i = 0; i < halfSampleSize; i++)
            {
                yTop[i] = spectrum[i];
                yBottom[i] = spectrum[i + halfSampleSize];
            }

            yTop = FourierTransform(yTop, direct);
            yBottom = FourierTransform(yBottom, direct);
            for (int i = 0; i < halfSampleSize; i++)
            {
                int j = i << 1; // i = 2*j;
                spectrum[j] = yTop[i];
                spectrum[j + 1] = yBottom[i];
            }

            return spectrum;
        }
        public static Complex[][] transform(Complex[][] a)
        {
            int row = a.Length;
            int column = a[0].Length;
            Complex[][] result = new Complex[column][];
            for (int i = 0; i < column; i++)
            {
                result[i] = new Complex[row];
                for (int j = 0; j < row; j++)
                    result[i][j] = a[j][i];
            }
            return result;
        }
        public static double[][] RecoveryPoints(Complex[][] massSpectrComplex)
        {
            int row = massSpectrComplex.Length;
            int column = massSpectrComplex[0].Length;
            int size = row * column;
            double[][] massSpectrPoints = new double[row][];
            int counter = 0;
            for (int i = 0; i < row; i++)
            {
                massSpectrPoints[i] = new double[column];
                for (int j = 0; j < column; j++)
                {
                    massSpectrPoints[i][j] = massSpectrComplex[i][j].Real;
                    if (massSpectrPoints[i][j] < 0) counter++;
                    massSpectrPoints[i][j] = massSpectrPoints[i][j] / (double)size;
                };
            }

            return massSpectrPoints;
        }
        public static double[] SpectrumPoints(Complex[] massSpectrComplex)
        {
            int lenght = massSpectrComplex.Length;
            double[] massSpectrPoints = new double[lenght];
            for (int i = 0; i < lenght; i++)
            {
                massSpectrPoints[i] = massSpectrComplex[i].Magnitude;
                massSpectrPoints[i] = massSpectrPoints[i] / lenght;
            }

            return massSpectrPoints;
        }
        public static void AngularTransform(ref Complex[][] compArray)
        {
            for (int i = 0; i < compArray.Length / 2; i++)
            {
                for (int j = 0; j < compArray[i].Length / 2; j++)
                {
                    Complex temp = compArray[i][j];
                    compArray[i][j] = compArray[i + compArray.Length / 2][j + compArray[i].Length / 2];
                    compArray[i + compArray.Length / 2][j + compArray[i].Length / 2] = temp;
                }
            }
            for (int i = compArray.Length / 2; i < compArray.Length; i++)
            {
                for (int j = 0; j < compArray[i].Length / 2; j++)
                {
                    Complex temp = compArray[i][j];
                    compArray[i][j] = compArray[i - compArray.Length / 2][j + compArray[i].Length / 2];
                    compArray[i - compArray.Length / 2][j + compArray[i].Length / 2] = temp;
                }
            }

        }

        public static Complex[][] TwoDimensionalTransform(Complex[][] complexArrays, bool direct)
        {
            if (direct == false)
            {
                AngularTransform(ref complexArrays);
            }
            // Выполнение одномерного ДПФ для каждой строки
            for (int i = 0; i < complexArrays.Length; i++)
            {
                complexArrays[i] = FourierTransform(complexArrays[i], direct);
            }
            // Инвертировать, то есть поменять местами строки и столбцы
            complexArrays = transform(complexArrays);
            // Одномерное ДПФ для каждой строки, которое на самом деле является одномерным ДПФ для столбцов массива перед инверсией
            for (int i = 0; i < complexArrays.Length; i++)
            {
                complexArrays[i] = FourierTransform(complexArrays[i], direct);
            }
            // Инвертируем обратно
            // Здесь должно быть перемещение углов

            complexArrays = transform(complexArrays);
            if (direct == true)
            {
                AngularTransform(ref complexArrays);
            }
            //AngularTransform(complexArrays);

            return complexArrays;
        }

    }
}

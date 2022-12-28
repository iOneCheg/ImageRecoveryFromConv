using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageRecoveryFromConv
{
    internal class HardwareFunc
    {
        private double Sigma,
                       Slice,
                       Amplitude;

        public HardwareFunc(double sigma, double slice, double amplitude)
        {
            Sigma = sigma; Slice = slice; Amplitude = amplitude;
        }
        
        public static int[][] Conv(int[][] input, double[][] kernel)
        {

            int[][] result = new int[input.Length][];
            for (var i = 0; i < input.Length; i++)
            {
                result[i] = new int[input[i].Length];
            }
            for (int i = kernel.Length / 2; i < input.Length - kernel.Length / 2; i++)
            {
                for (int j = kernel[0].Length / 2; j < input.Length - kernel[0].Length / 2; j++)
                {
                    double[][] tmp = new double[kernel.Length][];
                    double sum = 0;
                    for (var n = 0; n < tmp.Length; n++)
                    {
                        tmp[n] = new double[kernel[n].Length];
                        for (var m = 0; m < tmp.Length; m++)
                        {
                            tmp[n][m] = input[i - kernel.Length / 2 + n][j - kernel[n].Length / 2 + m] * kernel[n][m];
                            sum += tmp[n][m];
                        }
                    }

                    if (sum < 0)
                    {
                        sum = 0;
                    }
                    //else if (sum > 1)
                    //{
                    //    sum = 1;
                    //}

                    result[i][j] = (int)sum;
                }
            }
            return result;
        }
        public static int[][] CutZero(int[][] input, int kernelSize)
        {
            int[][] buf = new int[input.Length - kernelSize + 1][];
            for (var i = 0; i < buf.Length; i++)
                buf[i] = new int[input.Length - kernelSize + 1];

            for (var i = 0; i < input.Length - kernelSize + 1; i++)
            {
                for (var j = 0; j < input[i].Length - kernelSize + 1; j++)
                {
                    buf[i][j] = input[i+kernelSize/2][j+kernelSize/2];
                }
            } 
            return Resample(input.Length, input[0].Length, buf);
        }
        public static int[][] Norm(double[][] input, double amplitude)
        {
            double Max = 0,Min=1;
            int[][] res = new int[input.Length][];
            double[][] res2 = new double[input.Length][];
            for (int i = 0; i < input.Length; i++)
            {
                if (Max < input[i].Max())
                {
                    Max = input[i].Max();
                }
            }
            for (int i = 0; i < input.Length; i++)
            {
                if (Min > input[i].Min())
                {
                    Min = input[i].Min();
                }
            }
            for (int i = 0; i < input.Length; i++)
            {
                res[i] = new int[input[i].Length];
                res2[i] = new double[input[i].Length];
                for (int j = 0; j < input[i].Length; j++)
                {
                    double a = (input[i][j] - Min) / (Max - Min);
                    if(a>amplitude) a = amplitude;
                    res[i][j] =(int)(a*255);
                }
            }
            return res;
        }
        public static double[][] GaussianBlur(int lenght, double weight)
        {
            double[][] kernel = new double[lenght][];
            for (var i = 0; i < lenght; i++)
            {
                kernel[i] = new double[lenght];
            }
            double kernelSum = 0;
            int foff = (lenght - 1) / 2;
            double distance = 0;
            double constant = 1d / (2 * Math.PI * weight * weight);
            for (int y = -foff; y <= foff; y++)
            {
                for (int x = -foff; x <= foff; x++)
                {
                    distance = ((y * y) + (x * x)) / (2 * weight * weight);
                    kernel[y + foff][x + foff] = constant * Math.Exp(-distance);
                    kernelSum += kernel[y + foff][x + foff];
                }
            }
            for (int y = 0; y < lenght; y++)
            {
                for (int x = 0; x < lenght; x++)
                {
                    kernel[y][x] = kernel[y][x] * 1d / kernelSum;
                }
            }
            return kernel;
        }
        public static int[][] Resample(int newWidth, int newHeight, int[][] oldImage)
        {
            int[][] outputImage = new int[newHeight][];
            int oldWidth = oldImage[0].Length;
            int oldHeight = oldImage.Length;

            for (var j = 0; j < newHeight; j++)
            {
                outputImage[j] = new int[newWidth];
                float tmp = (float)(j) / (float)(newHeight - 1) * (oldHeight - 1);
                int h = (int)Math.Floor(tmp);
                h = h < 0 ? 0 : h >= oldHeight - 1 ? oldHeight - 2 : h;
                float u = tmp - h;

                for (var i = 0; i < newWidth; i++)
                {
                    tmp = (float)(i) / (float)(newWidth - 1) * (oldWidth - 1);
                    int w = (int)Math.Floor(tmp);
                    w = w < 0 ? 0 : w >= oldWidth - 1 ? oldWidth - 2 : w;
                    float t = tmp - w;

                    /* Коэффициенты */
                    float d1 = (1 - t) * (1 - u);
                    float d2 = t * (1 - u);
                    float d3 = t * u;
                    float d4 = (1 - t) * u;

                    /* Окрестные пиксели: a[i][j] */
                    int p1 = oldImage[h][w];
                    int p2 = oldImage[h][w + 1];
                    int p3 = oldImage[h + 1][w + 1];
                    int p4 = oldImage[h + 1][w];

                    outputImage[j][i] = (int)(p1 * d1) + (int)(p2 * d2) + (int)(p3 * d3) + (int)(p4 * d4);
                }
            }
            return outputImage;
        }
        
    }
}

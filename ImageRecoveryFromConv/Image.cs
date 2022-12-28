using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ImageRecoveryFromConv
{
    public partial class Image : Form
    {
        public Image(int N, int M)
        {
            InitializeComponent();
            pictureBoxImage.Image = new Bitmap(N, M);
        }

        public static int[][] GetBrightnessArray(Bitmap srcImage)
        {
            Debug.Assert(srcImage != null);

            var result = new int[srcImage.Width][];


            for (var x = 0; x < srcImage.Width; x++)
            {
                result[x] = new int[srcImage.Height];
                for (var y = 0; y < srcImage.Height; y++)
                {
                    var srcPixel = srcImage.GetPixel(x, y);
                    result[x][y] = (int)(0.299 * srcPixel.R + 0.587 * srcPixel.G + 0.114 * srcPixel.B);

                }
            }
            return result;
        }
        public void FillImage(int[][] pixels)
        {
            pictureBoxImage.Image = new Bitmap(pixels.Length, pixels[0].Length);
            for (int i = 0; i < pixels.Length; i++)
            {
                for (int j = 0; j < pixels[i].Length; j++)
                {
                    if (i > 550 && j > 550)
                    {
                        ((Bitmap)pictureBoxImage.Image).
                    SetPixel(i, j, Color.AliceBlue);
                    }
                    else
                    {
                        ((Bitmap)pictureBoxImage.Image).
                    SetPixel(i, j, Color.FromArgb(pixels[i][j], pixels[i][j], pixels[i][j]));
                    }
                }
            }
            pictureBoxImage.Refresh();
        }
        public static int[][] ConvertToIntArray2Dim(double[][] pixels, bool spectr)
        {
            int[][] pixelInt = new int[pixels.Length][];
            if (spectr) pixels = Norm(pixels);
            for (int i = 0; i < pixelInt.Length; i++)
            {
                pixelInt[i] = new int[pixels[i].Length];
                for (int j = 0; j < pixelInt[i].Length; j++)
                {
                    pixelInt[i][j] = Convert.ToInt32(pixels[i][j] * 255);
                }
            }
            return pixelInt;
        }
        public static double[][] Norm(double[][] input)
        {
            double Max = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (Max < input[i].Max())
                {
                    Max = input[i].Max();
                }
            }

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    input[i][j] /= Max;
                }
            }
            return input;
        }
        public static int[][] Apply(int[][] input, double[,] kernel)
        {
            //Получаем байты изображения
            //byte[] inputBytes = BitmapBytes.GetBytes(input);
            int[][] output = new int[input.Length][];
            for (var i = 0; i < output.Length; i++)
            {
                output[i] = new int[input[i].Length];
            }
            //byte[] outputBytes = new byte[inputBytes.Length];

            int width = input.Length;
            int height = input[0].Length;

            int kernelWidth = kernel.GetLength(0);
            int kernelHeight = kernel.GetLength(1);

            //Производим вычисления
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    double rSum = 0, gSum = 0, bSum = 0, kSum = 0;

                    for (int i = 0; i < kernelWidth; i++)
                    {
                        for (int j = 0; j < kernelHeight; j++)
                        {
                            int pixelPosX = x + (i - (kernelWidth / 2));
                            int pixelPosY = y + (j - (kernelHeight / 2));
                            if ((pixelPosX < 0) ||
                              (pixelPosX >= width) ||
                              (pixelPosY < 0) ||
                              (pixelPosY >= height)) continue;

                            //byte r = inputBytes[3 * (width * pixelPosY + pixelPosX) + 0];
                            int val = input[x][y];
                            ///byte g = inputBytes[3 * (width * pixelPosY + pixelPosX) + 1];
                            //byte b = inputBytes[3 * (width * pixelPosY + pixelPosX) + 2];

                            double kernelVal = kernel[i, j];

                            //rSum += r * kernelVal;
                            //gSum += g * kernelVal;
                            //bSum += b * kernelVal;

                            kSum += kernelVal;
                        }
                    }

                    if (kSum <= 0) kSum = 1;

                    //Контролируем переполнения переменных
                    rSum /= kSum;
                    if (rSum < 0) rSum = 0;
                    if (rSum > 255) rSum = 255;

                    gSum /= kSum;
                    if (gSum < 0) gSum = 0;
                    if (gSum > 255) gSum = 255;

                    bSum /= kSum;
                    if (bSum < 0) bSum = 0;
                    if (bSum > 255) bSum = 255;

                    //Записываем значения в результирующее изображение
                    output[x][y] = (byte)rSum;
                    //outputBytes[3 * (width * y + x) + 1] = (byte)gSum;
                    //outputBytes[3 * (width * y + x) + 2] = (byte)bSum;
                }
            }
            //Возвращаем отфильтрованное изображение
            return output;
        }
    }
}

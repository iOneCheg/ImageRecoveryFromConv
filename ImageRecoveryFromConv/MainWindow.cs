using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Resources.ResXFileRef;

namespace ImageRecoveryFromConv
{
    public partial class MainWindow : Form
    {
        private int[][] _inputPixels, _gaussPix, _bluredPix, _result;
        public static List<Point> del = new List<Point>();

        public double SigmaGauss, SliceValue, ProcentEpsSignal;
        public int DiameterGauss;
        public MainWindow()
        {
            InitializeComponent();
        }
        private double GetLengthForPower2(int length)
        {
            for (double x = 2; ; x *= 2)
                if (x > length) return x;
        }
        public static int[][] ReverseConversion(Complex[][] spectrRecovery)
        {
            Complex[][] recoveryCompl = FFT.TwoDimensionalTransform(spectrRecovery, false);
            double[][] pixelRecovery = FFT.RecoveryPoints(recoveryCompl);
            int counter = 0;
            for (int i = 0; i < pixelRecovery.Length; i++)
            {
                for (int j = 0; j < pixelRecovery[i].Length; j++)
                {
                    if (pixelRecovery[i][j] < 0)
                    {
                        counter++;
                        pixelRecovery[i][j] = 0;
                    }

                }
            }
            return ConvertToIntArray2Dim(pixelRecovery, true);
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
        private void button_Start_Click(object sender, EventArgs e)
        {
            int N = _inputPixels.Length; int M = _inputPixels[0].Length;
            Complex[][] spectrGauss = DirectFourier(_gaussPix, N, M);
            Complex[][] spectrImage = DirectFourier(_bluredPix, N, M);
            Complex[][] res = Delenie(spectrImage, spectrGauss, N, M, ProcentEpsSignal, true); //Eps - доля сигнала в %, отсекаемая при делении

            #region Вывод спектров
            //double[][] pixelsSpectrRes = new double[res.GetLength(0)][];
            //for (int i = 0; i < res.GetLength(0); i++)
            //{
            //    pixelsSpectrRes[i] = new double[res[i].Length];
            //    pixelsSpectrRes[i] = FFT.SpectrumPoints(res[i]);
            //}
            //pixelsSpectrRes[N / 2][M / 2] = 0;
            //int[][] spectrPixelsToImageRes = ConvertToIntArray2Dim(pixelsSpectrRes, true);

            //Image spImageRes = new Image(spectrPixelsToImageRes.Length, spectrPixelsToImageRes[0].Length);
            //spImageRes.Size = new Size(spectrPixelsToImageRes.Length + 40, spectrPixelsToImageRes[0].Length + 60);
            //spImageRes.Text = "Спектр результата деления";
            //spImageRes.FillImage(spectrPixelsToImageRes);
            //spImageRes.Show();

            //double[][] pixelsSpectr = new double[spectrImage.GetLength(0)][];
            //for (int i = 0; i < spectrImage.GetLength(0); i++)
            //{
            //    pixelsSpectr[i] = new double[spectrImage[i].Length];
            //    pixelsSpectr[i] = FFT.SpectrumPoints(spectrImage[i]);
            //}
            //pixelsSpectr[N / 2][M / 2] = 0;
            //int[][] spectrPixelsToImage = ConvertToIntArray2Dim(pixelsSpectr, true);

            //Image spImage = new Image(spectrPixelsToImage.Length, spectrPixelsToImage[0].Length);
            //spImage.Size = new Size(spectrPixelsToImage.Length + 40, spectrPixelsToImage[0].Length + 60);
            //spImage.Text = "Спектр испорченного изображения";
            //spImage.FillImage(spectrPixelsToImage);
            //spImage.Show();

            //double[][] gaussSpectr = new double[spectrGauss.GetLength(0)][];
            //for (int i = 0; i < spectrGauss.GetLength(0); i++)
            //{
            //    gaussSpectr[i] = new double[spectrGauss[i].Length];
            //    gaussSpectr[i] = FFT.SpectrumPoints(spectrGauss[i]);
            //}
            //gaussSpectr[N / 2][M / 2] = 0;
            //int[][] spectrGaussToImage = ConvertToIntArray2Dim(gaussSpectr, true);

            //Image gImage = new Image(spectrGaussToImage.Length, spectrGaussToImage[0].Length);
            //gImage.Text = "Спектр аппаратной функции";
            //gImage.Size = new Size(spectrGaussToImage.Length + 40, spectrGaussToImage[0].Length + 60);
            //gImage.FillImage(spectrGaussToImage);
            //gImage.Show();

            #endregion
            _result = ReverseConversion(res);

            var sync = SynchronizationContext.Current;
            int maxIt = Convert.ToInt32(numUpDownMaxIt.Value);
            double epsRecovery = Convert.ToDouble(numUpDownEpsRecovery.Value);
            progressBarImageRecovery.Maximum = maxIt - 1;

            Thread th = new Thread(_ =>
             {
                 int recIt = 0;
                 for (int i = 0; i < maxIt; i++)
                 {
                     //_result = Iter(_result, spectrGauss, N, M, ProcentEpsSignal); // Eps - доля сигнала в %, отсекаемая при делении
                     spectrImage = DirectFourier(_result, N, M);
                     res = Delenie(spectrImage, spectrGauss, N, M, ProcentEpsSignal, false);
                     double Max = MaxSpectr(res);
                     double Min = MinSpectr(res);
                     double eps = Delta(spectrImage, res);
                     double epsProc = (100 * eps) / (Max - Min);
                     if (epsProc < epsRecovery)
                     {
                         recIt = i;
                         _result = ReverseConversion(res);
                         break;
                     }
                     _result = ReverseConversion(res);
                     sync.Send(__ =>
                     {
                         progressBarImageRecovery.Value = i;
                     }, null);
                 }
                 AngularTransform(ref _result);
                 MessageBox.Show("Кол-во итериций " + recIt+1);
             });
            th.IsBackground = true;
            th.Start();
        }
        public double Delta(Complex[][] first, Complex[][] second)
        {
            double delta = 0;
            for (int i = 0; i < first.Length; i++)
            {
                for (int j = 0; j < first[i].Length; j++)
                {
                    delta += Math.Abs(first[i][j].Magnitude - second[i][j].Magnitude);
                }
            }
            return delta;
        }
        public static int[][] Iter(int[][] inputSignal, Complex[][] spectrGauss, int N, int M, double Eps)
        {
            Complex[][] spectrImage = DirectFourier(inputSignal, N, M);
            Complex[][] res = Delenie(spectrImage, spectrGauss, N, M, Eps, false);
            return ReverseConversion(res);
        }
        public static double MinSpectr(Complex[][] spectr)
        {
            double Min = MaxSpectr(spectr);
            for (int i = 0; i < spectr.Length; i++)
            {
                for (int j = 0; j < spectr[i].Length; j++)
                {
                    if (spectr[i][j].Magnitude < Min)
                    {
                        Min = spectr[i][j].Magnitude;
                    }
                }
            }
            return Min;
        }
        public static double MaxSpectr(Complex[][] spectr)
        {
            double Max = 0;
            for (int i = 0; i < spectr.Length; i++)
            {
                for (int j = 0; j < spectr[i].Length; j++)
                {
                    if (spectr[i][j].Magnitude > Max)
                    {
                        Max = spectr[i][j].Magnitude;
                    }
                }
            }
            return Max;
        }

        private void buttonShowRecoveryImage_Click(object sender, EventArgs e)
        {
            Image resImage = new Image(_result.Length, _result[0].Length);
            resImage.Size = new Size(_result.Length + 40, _result[0].Length + 60);
            resImage.Text = "Результат восстановления";
            resImage.FillImage(_result);
            resImage.Show();
        }

        private void groupBoxGaussFuncParameters_Enter(object sender, EventArgs e)
        {

        }

        public static Complex[][] Delenie(Complex[][] signal, Complex[][] Denominator, int N, int M, double Eps, bool first)
        {
            Complex[][] result = new Complex[N][];
            double MaxSignal = MaxSpectr(signal), MaxGauss = MaxSpectr(Denominator);
            double MinSignal = MinSpectr(signal), MinGauss = MinSpectr(Denominator);
            double EpsSignal = MaxSignal / 100 * Eps, EpsGauss = MaxGauss / 100 * Eps;
            //int counter = 0;
            int counter = 0;
            //double EpsToUse = (EpsSignal > EpsGauss) ? EpsGauss : EpsSignal;
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new Complex[M];
                for (int j = 0; j < result[i].Length; j++)
                {
                    if (first)
                    {
                        if (Denominator[i][j].Magnitude < EpsGauss || signal[i][j].Magnitude < EpsSignal)
                        {
                            result[i][j] = 0;
                        }
                        else
                        {
                            del.Add(new Point(i, j));
                            result[i][j] = signal[i][j] / Denominator[i][j];
                        }

                    }
                    else
                    {
                        for (var k = 0; k < del.Count; k++)
                        {
                            // int counter = 0;
                            if (del[k].Equals(new Point(i, j)))
                            {
                                result[i][j] = signal[i][j] / Denominator[i][j];
                                counter++;

                            }
                            else result[i][j] = signal[i][j];
                        }
                        // counter++;
                    }

                }
            }
            return result;
        }
        public static void AngularTransform(ref int[][] compArray)
        {
            for (int i = 0; i < compArray.Length / 2; i++)
            {
                for (int j = 0; j < compArray[i].Length / 2; j++)
                {
                    int temp = compArray[i][j];
                    compArray[i][j] = compArray[i + compArray.Length / 2][j + compArray[i].Length / 2];
                    compArray[i + compArray.Length / 2][j + compArray[i].Length / 2] = temp;
                }
            }
            for (int i = compArray.Length / 2; i < compArray.Length; i++)
            {
                for (int j = 0; j < compArray[i].Length / 2; j++)
                {
                    int temp = compArray[i][j];
                    compArray[i][j] = compArray[i - compArray.Length / 2][j + compArray[i].Length / 2];
                    compArray[i - compArray.Length / 2][j + compArray[i].Length / 2] = temp;
                }
            }

        }
        public static Complex[][] DirectFourier(int[][] input, int N, int M)
        {
            Complex[][] complexPixels = new Complex[N][];

            for (int i = 0; i < complexPixels.Length; i++)
            {
                complexPixels[i] = new Complex[M];
                for (int j = 0; j < complexPixels[i].Length; j++)
                {
                    complexPixels[i][j] = new Complex(input[i][j], 0);
                }
            }

            return FFT.TwoDimensionalTransform(complexPixels, true);
        }
        public void InitData()
        {
            DiameterGauss = Convert.ToInt32(numUpDownDiameter.Value);
            SigmaGauss = Convert.ToDouble(numUpDownSigma.Value);
            SliceValue = Convert.ToDouble(numUpDownSlice.Value);
            ProcentEpsSignal = Convert.ToDouble(numUpDownEpsSignal.Value);
        }
        private void button_LoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image Files(*.BMP;*.PNG)|*.BMP;*.PNG|All files (*.*)|*.*";
            openFile.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = new Bitmap(openFile.FileName);
                _inputPixels = Image.GetBrightnessArray(bmp);

                double scaleX = GetLengthForPower2(_inputPixels.Length),
               scaleY = GetLengthForPower2(_inputPixels[0].Length);
                _inputPixels = HardwareFunc.Resample((int)scaleX, (int)scaleY, _inputPixels);
                Image inputImage = new Image(_inputPixels.Length, _inputPixels[0].Length);
                inputImage.Size = new Size(_inputPixels.Length + 40, _inputPixels[0].Length + 60);
                inputImage.Text = "Исходное изображение";
                inputImage.FillImage(_inputPixels);
                inputImage.Show();
            }
            InitData();
            double[][] pixelInputImage = HardwareFunc.GaussianBlur(DiameterGauss, SigmaGauss);
            _gaussPix = new int[_inputPixels.Length][];
            for (var i = 0; i < _inputPixels.Length; i++)
            {
                _gaussPix[i] = new int[_inputPixels[i].Length];
            }
            int[][] buf = HardwareFunc.Norm(pixelInputImage, SliceValue);
            for (var i = 0; i < buf.Length; i++)
            {
                for (var j = 0; j < buf[i].Length; j++)
                {
                    _gaussPix[i + _inputPixels.Length / 2 - pixelInputImage.Length / 2][j + _inputPixels[0].Length / 2 - pixelInputImage[0].Length / 2] =
                        buf[i][j];
                }
            }

            //Image gImage = new Image(_gaussPix.Length, _gaussPix[0].Length);
            //gImage.Size = new Size(_gaussPix.Length + 40, _gaussPix[0].Length + 60);
            //gImage.Text = "Гауссов купол";
            //gImage.FillImage(_gaussPix);
            //gImage.Show();

            //int[][] res= new int[_inputPixels.Length][];
            //for(var i=0;i<_inputPixels.Length;i++)
            //{
            //    res[i] = new int[_inputPixels[i].Length];
            //    for(var j = 0; j < _inputPixels[i].Length; j++)
            //    {
            //        res[i][j] = _inputPixels[i][j] + _gaussPix[i][j];
            //        if (res[i][j]>255) res[i][j] = 255;
            //    }
            //}
            //_bluredPix = res;
            int[][] res = HardwareFunc.Conv(_inputPixels, pixelInputImage); //Свёртка изображения с гауссовым куполом
            _bluredPix = HardwareFunc.CutZero(res, DiameterGauss);   //Восстановление изображения до исходного размера

            Image gaussImage = new Image(_bluredPix.Length, _bluredPix[0].Length);
            gaussImage.Size = new Size(_bluredPix.Length + 40, _bluredPix[0].Length + 60);
            gaussImage.Text = "Испорченное изображение";
            gaussImage.FillImage(_bluredPix);
            gaussImage.Show();
            button_Recovery.Enabled = true;
        }
    }
}

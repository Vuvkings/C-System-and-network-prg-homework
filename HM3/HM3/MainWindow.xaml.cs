using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace HM3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource _tokenSource;
        private Thread _thread;

        private int _min;
        private int _max;
        private bool _isBusy;

        private int _factorialCounter;
        private int _maxFactorial;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void SquaresOfNumbers(object sender)
        {
            var token = (CancellationToken)sender;

            while (!token.IsCancellationRequested)
            {
                for (int i = _min; i <= _max; i++)
                {
                    _min = i + 1;
                    // т.к. к UI элементоам можно обращаться только из главного потока, необходимо делать так
                    Dispatcher.Invoke(() => txbk_Squares.Text += $"{Math.Pow(i, 2)} ");

                    Thread.Sleep(500);

                    if (i == _max)
                    {
                        Dispatcher.Invoke(() => txbk_Squares.Text += " Расчёт окончен!");
                        Dispatcher.Invoke(() => bt_Pause.IsEnabled = false);
                    }

                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                }
            }
        }

        private void FactorialOfNumber(object sender)
        {
            var token = (CancellationToken)sender;

            while (!token.IsCancellationRequested)
            {
                double result = 1;
                _factorialCounter++;

                for (int i = 2; i <= _factorialCounter; i++)
                {
                    result *= i;
                }

                if (_factorialCounter == 0 || _factorialCounter == 1)
                {
                    Dispatcher.Invoke(() => txbk_Factorials.Text += $"!{_factorialCounter} = 1\n");
                }
                else
                {
                    Dispatcher.Invoke(() => txbk_Factorials.Text += $"!{_factorialCounter} = {result}\n");
                }

                Thread.Sleep(500);

                if (_factorialCounter == _maxFactorial)
                {
                    Dispatcher.Invoke(() => txbk_Factorials.Text += "Расчёт окончен!");
                    Dispatcher.Invoke(() => bt_FactorialPause.IsEnabled = false);
                    break;
                }

                if (token.IsCancellationRequested)
                {
                    break;
                }
            }
        }

        private void bt_Start_Click(object sender, RoutedEventArgs e)
        {
            bt_Pause.IsEnabled = true;
            bt_Stop.IsEnabled = true;
            bt_Start.IsEnabled = false;
            bt_FactorialStart.IsEnabled = false;

            iud_Min.IsEnabled = false;
            iud_Max.IsEnabled = false;


            int.TryParse(iud_Max.Text, out _max);
            if (_isBusy)
            {
                int.TryParse(iud_Min.Text, out _min);
            }





            _tokenSource = new CancellationTokenSource();
            _thread = new Thread(SquaresOfNumbers)
            {
                IsBackground = true
            };

            _thread.Start(_tokenSource.Token);
        }

        private void bt_Pause_Click(object sender, RoutedEventArgs e)
        {
            bt_Start.IsEnabled = true;
            bt_Pause.IsEnabled = false;
            bt_Stop.IsEnabled = true;
            bt_FactorialStart.IsEnabled = true;

            _isBusy = false;

            if (_thread != null && _tokenSource != null)
            {
                _tokenSource.Cancel();
                _thread = null;
                _tokenSource = null;
            }
        }

        private void bt_Stop_Click(object sender, RoutedEventArgs e)
        {
            bt_Start.IsEnabled = true;
            bt_Pause.IsEnabled = false;
            bt_Stop.IsEnabled = false;
            bt_FactorialStart.IsEnabled = true;

            iud_Min.IsEnabled = true;
            iud_Max.IsEnabled = true;


            if (_thread != null && _tokenSource != null)
            {
                _tokenSource.Cancel();
                _thread = null;
                _tokenSource = null;
            }

            txbk_Squares.Text = string.Empty;
            _isBusy = true;
        }

        private void bt_FactorialStart_Click(object sender, RoutedEventArgs e)
        {
            bt_FactorialPause.IsEnabled = true;
            bt_FactorialStop.IsEnabled = true;
            bt_FactorialStart.IsEnabled = false;
            bt_Start.IsEnabled = false;


            iud_Total.IsEnabled = false;

            int.TryParse(iud_Total.Text, out _maxFactorial);


            _tokenSource = new CancellationTokenSource();
            _thread = new Thread(FactorialOfNumber)
            {
                IsBackground = true
            };

            _thread.Start(_tokenSource.Token);
        }

        private void bt_FactorialPause_Click(object sender, RoutedEventArgs e)
        {
            bt_FactorialStart.IsEnabled = true;
            bt_FactorialPause.IsEnabled = false;
            bt_FactorialStop.IsEnabled = true;
            bt_Start.IsEnabled = true;

            if (_thread != null && _tokenSource != null)
            {
                _tokenSource.Cancel();
                _thread = null;
                _tokenSource = null;
            }
        }

        private void bt_FactorialStop_Click(object sender, RoutedEventArgs e)
        {
            bt_FactorialStart.IsEnabled = true;
            bt_FactorialPause.IsEnabled = false;
            bt_FactorialStop.IsEnabled = false;
            bt_Start.IsEnabled = true;

            iud_Total.IsEnabled = true;

            if (_thread != null && _tokenSource != null)
            {
                _tokenSource.Cancel();
                _thread = null;
                _tokenSource = null;
            }

            txbk_Factorials.Text = string.Empty;
            _factorialCounter = 0;
        }
    }
    
}

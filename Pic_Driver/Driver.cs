using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

/* Serial COM library */
using System.IO.Ports;

namespace Pic_Driver
{
    class Driver
    {
        public short b_val = 170;
        // Create the serial port with basic settings 
        SerialPort port = new SerialPort("COM1",
          9600, Parity.None, 8, StopBits.One);
        [STAThread]

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        IntPtr hMonitor = new IntPtr(Screen.PrimaryScreen.GetHashCode());
        private void SerialPortProgram()
        {
            Console.WriteLine("Datos enviados desde el PIC:");
            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);

            // Open port
            port.Open();
            // Enter an application loop to keep this thread alive 
            Console.ReadLine();
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Show all the incoming data in the port's buffer
            string output = port.ReadExisting();
            Console.WriteLine(output);

            int result;


            try
            {
                result = Int32.Parse(output);

                switch (result)
                {
                    case 0:
                        {
                            Console.WriteLine("Bajar volumen\n");
                            Volume.VolumeDown();
                            break;
                        }
                    case 1:
                        {
                            Console.WriteLine("Subir volumen\n");
                            Volume.VolumeUp();
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Subir brillo\n");

                            if (b_val < 230) {
                                b_val += 20;
                                //Console.WriteLine(b_val);
                                Brightness.SetBrightness(b_val);
                            }
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Bajar brillo\n");

                            if (b_val > 50)
                            {
                                b_val -= 20;
                                //Console.WriteLine(b_val);
                                Brightness.SetBrightness(b_val);
                            }

                            break;
                        }
                    default:
                        {
                            Console.Write("El driver no reconoce esta entrada: ");
                            Console.WriteLine(result);
                            break;
                        }
                }

            } catch (FormatException err)
            {
                Console.WriteLine(err.Message);
            }
        }

        static void Main(string[] args)
        {
            Console.Title = "PIC DRIVER";
            Driver d = new Driver();
            d.SerialPortProgram();
        }
    }
}

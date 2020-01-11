using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Serial COM library */
using System.IO.Ports;

namespace Pic_Driver
{
    class Driver
    {
        // Create the serial port with basic settings 
        SerialPort port = new SerialPort("COM1",
          9600, Parity.None, 8, StopBits.One);
        [STAThread]

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

            Volume v = new Volume();

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
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Bajar brillo\n");
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
            Driver d = new Driver();
            d.SerialPortProgram();
        }
    }
}

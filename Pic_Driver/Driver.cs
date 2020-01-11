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
            Console.WriteLine(port.ReadExisting());
        }

        static void Main(string[] args)
        {
            Driver d = new Driver();
            d.SerialPortProgram();
        }
    }
}

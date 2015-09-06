using System;
using System.IO;

namespace HW_IO.Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            var fstream = new FileStream("bytes.dat", FileMode.Create, FileAccess.Write);
           
            for (int i = 0; i < 256; i++)
            {
                fstream.WriteByte((byte)i);
            }

            fstream.Close();

            fstream = new FileStream("bytes.dat", FileMode.Open, FileAccess.Read);

            for (int i = 0; i < 256; i++)
            {
                Console.Write(fstream.ReadByte() + " ");
            }
            Console.ReadKey();
        }
    }
}

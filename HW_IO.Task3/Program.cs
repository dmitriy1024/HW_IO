using System;
using System.IO;
using System.IO.Compression;

namespace HW_IO.Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            var filestream = File.Create("file.txt");
            var writer = new StreamWriter(filestream);
            writer.WriteLine("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            Console.WriteLine("Init string:\n\"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\"");
            writer.Close();

            filestream = File.Open("file.txt", FileMode.Open);

            var memStream = new MemoryStream();
            var copressWriter = new DeflateStream(memStream, CompressionMode.Compress);

            int readbyte = filestream.ReadByte();
            while (readbyte != -1)
            {
                copressWriter.WriteByte((byte)readbyte);
                readbyte = filestream.ReadByte();
            }

            copressWriter.Close();
            filestream.Close();
            File.Delete("file.txt");

            filestream = File.Open("file.txt", FileMode.Create);
            filestream.Write(memStream.ToArray(), 0, memStream.ToArray().Length);
            filestream.Close();

            var reader = File.OpenText("file.txt");
            string readStr = reader.ReadToEnd();
            reader.Close();

            Console.WriteLine("\nString after archivation:");
            Console.WriteLine(readStr);
            Console.ReadKey();
        }
    }
}

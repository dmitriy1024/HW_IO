using System;
using System.IO;

namespace HW_IO.Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            InitFolders();
            Console.WriteLine("All txt files with substring \"ipsum\"");
            ShowFiles(@".", "ipsum");
            Console.WriteLine("---------------------------------------");
            RenameFiles(@".", "ipsum", "blabla");

            Console.WriteLine("All substrings ipsum replased to \"blabla\"");

            Console.WriteLine("---------------------------------------");
            Console.WriteLine("All txt files with substring \"ipsum\"");
            ShowFiles(@".", "ipsum");
            Console.ReadKey();
        }

        static void InitFolders()
        {
            var currentDirectory = new DirectoryInfo(@".");

            for (int i = 0; i < 5; i++)
            {
                currentDirectory.CreateSubdirectory("folder" + i);
            }

            DirectoryInfo[] subDirectories = currentDirectory.GetDirectories();
            for (int i = 0; i < subDirectories.Length; i++)
            {
                var filestream = File.Create(String.Format(@"{0}\file{1}.txt", subDirectories[i].FullName, i));
                var writer = new StreamWriter(filestream);

                writer.WriteLine("Lorem ipsum dolor sit amet");
                writer.WriteLine("Lorem ipsum dolor sit amet");

                writer.Close();

                for (int j = 0; j < 5; j++)
                {
                    subDirectories[i].CreateSubdirectory(String.Format(@"folder{0}-{1}", i, j));

                    var filestream2 = File.Create(String.Format(@"{0}\file{1}-{2}.txt", subDirectories[i].GetDirectories()[j].FullName, i, j));
                    var writer2 = new StreamWriter(filestream2);

                    writer2.WriteLine("Lorem ipsum dolor sit amet");
                    writer2.WriteLine("Lorem ipsum dolor sit amet");

                    writer2.Close();
                }
            }            
        }

        static void ShowFiles(string path, string findSubStr)
        {   

            var directory = new DirectoryInfo(path);
            var files = directory.GetFiles("*.txt");

            foreach (var item in files)
            {
                string line;
                var reader = item.OpenText();

                while((line = reader.ReadLine()) != null)
                {
                    if(line.Contains(findSubStr))
                    {
                        Console.WriteLine(item.FullName);
                        break;
                    }
                }

                reader.Close();
            }

            var subDirectories = directory.GetDirectories();
            if (subDirectories.Length == 0)
            {
                return;
            }
            else
            {
                foreach (var item in subDirectories)
                {
                    ShowFiles(item.FullName, findSubStr);
                }
            }
        }

        static void RenameFiles(string path, string findSubStr, string newSubStr)
        {

            var directory = new DirectoryInfo(path);
            foreach (var item in directory.GetFiles("*.txt"))
            {
                string str;
                                
                var reader = item.OpenText();
                str = reader.ReadToEnd();
                
                reader.Close();

                if (str.Contains(findSubStr))
                {
                    string replacedStr = str.Replace(findSubStr, newSubStr);
                    var stream = item.Open(FileMode.OpenOrCreate);
                    var writer = new StreamWriter(stream);

                    writer.Write(replacedStr);
                    writer.Close();
                }                            
            }

            var subDirectories = directory.GetDirectories();
            if (subDirectories.Length == 0)
            {
                return;
            }
            else
            {
                foreach (var item in subDirectories)
                {
                    RenameFiles(item.FullName, findSubStr, newSubStr);
                }
            }
        }
    }
}
                
       
            



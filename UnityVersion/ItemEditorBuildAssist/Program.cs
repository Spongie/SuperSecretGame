using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ItemEditorBuildAssist
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine(string.Format("Moving files from {0} to {1}", args[0], args[1]));

            try
            {
                string path = Directory.GetParent(Directory.GetParent(Directory.GetParent(args[1]).FullName).FullName).FullName + "\\ItemEditor";

                Console.WriteLine("Deleting old files");
                foreach (var file in Directory.GetFiles(path))
                {
                    if (Path.GetExtension(file) == ".ini")
                        continue;

                    File.Delete(file);
                }

                Console.WriteLine("Copying new files");
                foreach (var file in Directory.GetFiles(args[0]))
                {
                    if (Path.GetExtension(file) == ".ini")
                        continue;

                    File.Copy(file, path + "\\" + Path.GetFileName(file));
                }
            }
            catch
            {
                return 99;
            }

            return 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using SerializzazioneFwf;

namespace FwfTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var item = new List<Record1>
            {
                new Record1
                {
                    TRA_CAMPO_1 = "Primo campo",
                    TRA_CAMPO_2 = new List<R11>
                    {
                        new R11 {
                            TRA_CAMPO_2_1 = "123",
                            TRA_CAMPO_2_2 = "22%",
                        },
                        new R11
                        {
                            TRA_CAMPO_2_1 = "345",
                            TRA_CAMPO_2_2 = "22%",
                        }
                    },
                    TRA_CAMPO_3 = "P",
                }
            };

            for (int i = 0; i < 1; i++)
            {
                item.AddRange(item);
            }

                var sw = new Stopwatch();
            sw.Start();

            var str = SerializzatoreFwf.Serializza(item);

            sw.Stop();

            Console.WriteLine("\n\n" + item.Count + " " + ((double)str.Length).ToString("N") + " - " + (double)sw.ElapsedMilliseconds / 1000 + "s" + "\n\n.");

            Console.WriteLine(str);

            Console.ReadLine();
        }
    }
}

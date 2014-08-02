//Michael Parker
//6/16/2014
//HTML Markup Generator
//http://www.reddit.com/r/dailyprogrammer/comments/289png/6162014_challenge_167_easy_html_markup_generator/

//Visual Studio 2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace HTMLGenerator
{
    class HTMLGenerator
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your title: ");
            string title = Console.ReadLine();

            Console.Write("Enter your paragraph: ");
            string paragraph = Console.ReadLine();

            Console.Write("Enter the filename: ");
            string fileName = Console.ReadLine();

            string ext = Path.GetExtension(fileName);
            if (ext == "")
                fileName += ".HTML";
            else if (ext.ToUpper() != ".HTML")
                Path.ChangeExtension(fileName, ".HTML");

            string htmlOutput = "<!DOCTYPE html>\n" +
                                "<html>\n" +
                                    "\t<head>\n" +
                                        "\t\t<title>" + title + "</title>\n" +
                                    "\t</head>\n\n" +

                                    "\t<body>\n" +
                                        "\t\t<p>" + paragraph + "<p>\n" +
                                    "\t</body>\n" +
                                "</html>";

            File.WriteAllText(fileName, htmlOutput);

            Process.Start(fileName);
        }
    }
}

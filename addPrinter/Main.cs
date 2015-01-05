using System.Management;
using System.IO;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

using addPrinters;


class ninjaPrinters
{
    public static void Main()
    {

       

       Console.ReadKey();
    }


    public static void checkPrinterDb()
    {
        Dictionary<string, string> dic = new Dictionary<string, string>(); //hash table for fast query
        String[] links = {"http://www.columbia.edu/acis/facilities/printers/ninja/acis/js/printers.js",
"http://www.columbia.edu/acis/facilities/printers/ninja/barnard/js/printers.js",
"http://www.columbia.edu/acis/facilities/printers/ninja/cait/js/printers.js",
"http://www.columbia.edu/acis/facilities/printers/ninja/lso/js/printers.js",
"http://www.columbia.edu/acis/facilities/printers/ninja/meche/js/printers.js",
"http://www.columbia.edu/acis/facilities/printers/ninja/old_pcd/js/printers.js",
"http://www.columbia.edu/acis/facilities/printers/ninja/soa/js/printers.js",
"http://www.columbia.edu/acis/facilities/printers/ninja/ssw/js/printers.js"};

        foreach (string link in links)
        {
            StreamReader sr = openURL(link);
            while (!sr.EndOfStream)
            {
                string anEntry=sr.ReadLine();
                if (anEntry.StartsWith("//")|| anEntry.Length<3) //i can't believe this has a startsWith method!
                {
                    continue; //skip comments and empty lines
                }

                


            }
            
            


        }
        
        
        
        

    }

    public static StreamReader openURL(String str)
    {
        WebClient client = new WebClient();
        Stream stream = client.OpenRead(str);
        StreamReader reader = new StreamReader(stream);
        return reader;
    }
}
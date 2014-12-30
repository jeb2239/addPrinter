using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.ComponentModel;

namespace addPrinters
{
    class PrintPort
    {

        public ManagementScope scope { get; private set; }

        public PrintPort(string name,string computer=".")
        {
            ConnectionOptions connOptions = new ConnectionOptions();
            connOptions.Impersonation = ImpersonationLevel.Impersonate;
            
            scope = new ManagementScope(@"\\" + computer + @"\root\cimv2", connOptions);
            scope.Connect();
            ObjectGetOptions portGetOptions = new ObjectGetOptions(null, TimeSpan.MaxValue, true);
            ManagementPath portPath = new ManagementPath("Win32_TCPIPPrinterPort");
            ManagementClass portClass = new ManagementClass(scope, portPath, portGetOptions);
            ManagementObject newPort = portClass.CreateInstance();
            
            newPort["Name"] = "IP_" + name;
            newPort["Protocol"] = 1;
            newPort["HostAddress"] = name;
            newPort["PortNumber"] = 9100;
            newPort["SNMPEnabled"] = false;
            PutOptions portPutOps = new PutOptions();
            portPutOps.UseAmendedQualifiers = true;
            portPutOps.Type = PutType.UpdateOrCreate;

            newPort.Put(portPutOps);

      





        }

    }



    class Printer
    {



        public Printer(string name, PrintPort port)
        {

            ObjectGetOptions printerGetOptions = new ObjectGetOptions(null, TimeSpan.MaxValue, true);
            ManagementPath printerPath = new ManagementPath("Win32_Printer");
            ManagementClass printerClass = new ManagementClass(port.scope, printerPath, printerGetOptions);
            ManagementObject printer = printerClass.CreateInstance();
            printer["DriverName"] = "Brother MFC-7840W";
            printer["Portname"] = "IP_" + name;
            printer["DeviceId"] = name;
            printer["Network"] = true;
            PutOptions printerPutOptions = new PutOptions();
            printerPutOptions.UseAmendedQualifiers = true;
            printerPutOptions.Type = PutType.UpdateOrCreate;
            printer.Put(printerPutOptions);



        }



    }
}
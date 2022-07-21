// http://www.java2s.com/Tutorial/CSharp/0580__Network/ReportinformationfromNetworkInterfacenetworkstatistics.htm
if (!NetworkInterface.GetIsNetworkAvailable())
   return;

NetworkInterface[] interfaces 
    = NetworkInterface.GetAllNetworkInterfaces();

foreach (NetworkInterface ni in interfaces)
{                
    Console.WriteLine("    Bytes Sent: {0}", 
        ni.GetIPv4Statistics().BytesSent);
    Console.WriteLine("    Bytes Received: {0}",
        ni.GetIPv4Statistics().BytesReceived);
}

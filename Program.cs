// See https://aka.ms/new-console-template for more information
using System.Reflection;
using System.Text;
using UCNLDrivers;

Console.WriteLine(string.Format("{0} v{1} (C) UC&NL, unavlab.com", Assembly.GetExecutingAssembly().GetName().Name, Assembly.GetExecutingAssembly().GetName().Version));

UInt16 inport = 28128;

if (args.Length == 0)
{
    Console.WriteLine("Usage: AzimuthUDPListener [in_port]");
    Console.WriteLine($"Enter a port number (Press Enter to use default value {inport}): ");

    bool ok = false;
    while (!ok)
    {
        string tstr = Console.ReadLine();

        if (!string.IsNullOrEmpty(tstr))
        {
            ok = UInt16.TryParse(tstr, out inport);
        }
        else
            ok = true;
    }
}
else
{
    if (args.Length >= 1)
    {
        if (!UInt16.TryParse(args[0], out inport))
            Console.WriteLine("Error parsing argument 1");
    }
}


Console.WriteLine("Starting listening to {0}", inport);
Console.WriteLine("Press [ENTER] to exit...");

UDPListener lr = new UDPListener(inport);

lr.DataReceivedHandler += (o, e) =>
{
    Console.WriteLine(Encoding.ASCII.GetString(e.Data));
};
lr.StartListen();

Console.ReadLine();
lr.StopListen();
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using FunctionalWay.Extensions;

namespace UdpDog
{
    class Program
    {
        static void Main(string[] args)
        {
            var port = 8125;
            var listener = port.Pipe(p => new UdpClient(p));
            var groupEp = new IPEndPoint(IPAddress.Any, port);

            try
            {
                $"Listening on {port}\r\n".Pipe(Console.WriteLine);

                while (true)
                {
                    var bytes = listener.Receive(ref groupEp);
                    bytes
                        .Pipe(bs => Encoding.ASCII.GetString(bytes, 0, bytes.Length))
                        .Pipe(bsString => $"From {groupEp} on {DateTime.Now}:\r\n{bsString}\r\n")
                        .Pipe(Console.WriteLine);
                }
            }
            catch (Exception e)
            {
                e.Pipe(Console.WriteLine);
            }
            finally
            {
                listener.Close();
            }
        }
    }
}

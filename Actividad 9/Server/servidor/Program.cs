using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor
{
	class Program
	{
		public static void Main(string[] args)
		{
			Conectar();
		}
		
		public static void Conectar() 
		{
			byte[] ByRec;
			Socket miPrimerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			IPEndPoint miDireccion = new IPEndPoint(IPAddress.Any, 1234);
			
			byte[] textoEnviar;
			
			string numero, fibo1, fibo2 = "1";
			
			try {
				miPrimerSocket.Bind(miDireccion);
				miPrimerSocket.Listen(1);
				Console.WriteLine("Escuchando cliente...");
				Socket Escuchar = miPrimerSocket.Accept();
				Console.WriteLine("Conectado con exito.");
				
				// Recibe cantidad de series Fibonacci
				ByRec = new byte[255];
				int a  = Escuchar.Receive(ByRec, 0, ByRec.Length, 0);
				Array.Resize(ref ByRec, a);
				numero = Encoding.Default.GetString(ByRec);
				
				int n = Int32.Parse(numero);
				
				// Recibe numeros
				do{
				ByRec = new byte[255];
				int b  = Escuchar.Receive(ByRec, 0, ByRec.Length, 0);
				Array.Resize(ref ByRec, b);
				fibo1 = Encoding.Default.GetString(ByRec);
				
				Console.WriteLine(fibo2);
				
				int x = Int32.Parse(fibo1);
				int y = Int32.Parse(fibo2);
				int z = y + x;
				
				fibo2 = z.ToString();
				// Enviar numero
				textoEnviar = Encoding.Default.GetBytes(fibo2);
				Escuchar.Send(textoEnviar, 0, textoEnviar.Length, 0);
				n--;
				
				}while(n != 0);

				
				miPrimerSocket.Close();
				
			} catch (Exception error) {
				Console.WriteLine("Error: {0}", error.ToString());
			}
			
			Console.WriteLine("Presiona cualquier tecla para cerrar");
			Console.ReadLine();
		}
	}
}
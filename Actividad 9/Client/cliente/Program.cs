using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
	class Program
	{
		public static void Main(string[] args)
		{
			Conectar();
			
		}
		
		public static void Conectar() 
		{
			Socket miPrimerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			IPEndPoint miDireccion = new IPEndPoint(IPAddress.Parse("177.237.24.253"), 1234);
			
			byte[] textoAEnviar;
			byte[] ByRec;
			
			string numero, fibo1 = "1", fibo2;
			
			try {
	
				miPrimerSocket.Connect(miDireccion);
				Console.WriteLine("Succesfull conection");
				
				// capturar numero de fibonacci
				Console.WriteLine("Ingresa la cantidad de numeros Fibonacci a imprimir: ");
				numero = Console.ReadLine();
				textoAEnviar = Encoding.Default.GetBytes(numero);
				miPrimerSocket.Send(textoAEnviar, 0, textoAEnviar.Length, 0);
				
				int n = Int32.Parse(numero);
				
				Console.Clear();
				
				Console.WriteLine(fibo1);
					//Enviar numero
					do {
				textoAEnviar = Encoding.Default.GetBytes(fibo1);
				miPrimerSocket.Send(textoAEnviar, 0, textoAEnviar.Length, 0);
				
					//Recibe numero
				ByRec = new byte[255];
				int a  = miPrimerSocket.Receive(ByRec, 0, ByRec.Length, 0);
				Array.Resize(ref ByRec, a);
				fibo2 = Encoding.Default.GetString(ByRec);
				
				int x = Int32.Parse(fibo1);
				int y = Int32.Parse(fibo2);
				int z = x + y;
				
				fibo1 = z.ToString();
				
				Console.WriteLine(fibo1);
				n--;
					}while(n != 0);
				miPrimerSocket.Close();
				
			} catch (Exception error) {
				Console.WriteLine("Error: {0}", error.ToString());
			}
			
			Console.WriteLine("press any key");
			Console.ReadLine();
		}
	}
}
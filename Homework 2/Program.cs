using System.Text.RegularExpressions;

namespace Progr2_tarea_2
//Leandro Leguisamo Garcia 2024=2580
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Enter a number: ");
                String Entrance = Console.ReadLine(); //Esta linea garda lo que el usuario escribio como texto (String)


                int numero = int.Parse(Entrance); //Esta linea cambia lo que escribio el usuario a entero (Int)

                if (numero % 2 == 0) //Si el residuo de 2 = 0 es par.
                {
                    Console.WriteLine("The number entered is even");
                }
                else
                {
                    Console.WriteLine("The number entered is odd");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
    }
}
//El programa pide un numero al usuario, y si el numero es par imprime "The number entered is even" y si es impar imprime "The number entered is odd".
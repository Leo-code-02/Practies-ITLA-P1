namespace Progr1_tarea1
//leandro Leguisamo Garcia 2024-2580
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Pregunta 1 Variables

            int number = 1000;
            double PI = 3.1416;
            string text = "Dios, Patria y Libertad";
            char character = 'L';
            float weight = 200.34f;
            bool isMale = true;
            decimal myDecimal = 4337593543950335m;
            long myLong = -9223372036854775808;   // Valor muy grande negativo
            ulong myULong = 18446744073709551615; // Valor muy grande positivo


            Console.WriteLine(number);
            Console.WriteLine(PI);
            Console.WriteLine(text);
            Console.WriteLine(character);
            Console.WriteLine(weight);
            Console.WriteLine(isMale);
            Console.WriteLine(myDecimal);
            Console.WriteLine(myLong);
            Console.WriteLine(myULong);



            //Pregunta 2 buscar como se declara una cosntante

            const string nombre = "Leandro";
            Console.WriteLine(nombre);


            //Pregunta 3 Operaciones con entero

            int num = 3;
            Console.WriteLine(++num);
            Console.WriteLine(--num);
            Console.WriteLine();
            Console.WriteLine(num + 1);
            Console.WriteLine(num - 1);
            Console.WriteLine(num * 5);
            Console.WriteLine(num / 1);


            //Pregunta 4 Ejercicio con float

            float valor = 10152466.25f;
            byte numero = 5;

            float suma = num + valor;
            Console.WriteLine(suma);


            //Pregunta 5 Ejercicio en la hora y fecha


            Console.WriteLine(DateTime.Now);

        }

    }

}

            
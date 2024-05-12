using System.Security.Cryptography.X509Certificates;

namespace Maturita
{
    public class Prorgam
    {
        public static void Main(string[] args)
        {
            // Lambda - metoda bez názvu, na vstup veme parametry a na výstupu něco vráti
            // Func - delegát co má hodně parametru a jeden který vrací
            // Action - delegát co má hodně parametru a děla nějakou akci

            // expression lambda        
            Func<int, int, int, int> volume = (a, b, c) => a * b * c;
            Console.WriteLine(volume(10, 10, 10));


            int[] array = new int[] { 9, 3, 6, 12, 24, 44, 77, 100 };

            var divisibleVar = array.Where(x => (x % 2) == 0);

            foreach (var i in divisibleVar)
            {
                Console.WriteLine(i);
            }

            // statement lambda
            Action<int, int> expression = (a, b) =>
            {
                int result = a + b;
                Console.WriteLine($"Result is {result}");
            };

            expression(93, 32);

            Action<int, string> cyklus = (iterace, slovo) =>
            {
                for (int i = 0; i < iterace; i++)
                {
                    Console.WriteLine(slovo);
                }
            };

            cyklus(10, "ahoj");

            // staticke metody volame bez instance, pouze ukazeme tridu
            Console.WriteLine(Mathematic.Calculate(10, 10, 10));
            Mathematic m = new Mathematic();

            // staticke metody nesmime volat pres instanci
            //Console.WriteLine(m.Calculate(10,10,10));

            // delegaty
            // jsou to ukazatele na metodu

            // pouze jedna metoda
            NumberChanger ncAdd = new NumberChanger(AddNumber);
            NumberChanger ncSub = new NumberChanger(SubstractNumber);

            // dve metody v invoke listu
            NumberChanger multicast = ncAdd + ncSub;

            Console.WriteLine(DelegateUser(ncAdd, 10, 5));
            Console.WriteLine(DelegateUser(ncSub, 10, 5));
            Console.WriteLine("AAAAAA");
            Console.WriteLine(DelegateUser(multicast, 10, 5));

            List<int> list = new List<int>();

            // call invoke list
            foreach (NumberChanger del in multicast.GetInvocationList())
            {
                list.Add(del(10, 10));
            }

            foreach (int i in list)
            {
                Console.WriteLine(i);
            }

            // use delegate as callback function
            array = new int[] { 1, -2, -3, 4, -5, 6, 7, 8, 9 };

            CheckNumber check = (number) => number % 3 == 0;
            CheckNumber positive = Positive;

            List<int> result = MethodToCheck((x) => (x % 2) == 0, array);
            foreach (int i in result)
            {
                Console.WriteLine("Divisible by 2: " + i);
            }

            result = MethodToCheck(check, array);
            foreach (int i in result)
            {
                Console.WriteLine("Divisible by 3: " + i);
            }

            result = MethodToCheck(positive, array);
            foreach (int i in result)
            {
                Console.WriteLine("Positive: " + i);
            }

        }

        public delegate bool CheckNumber(int x);

        public static bool Positive(int x)
        {
            return x > 0;
        }

        public static List<int> MethodToCheck(CheckNumber callback, int[] arr)
        {
            List<int> list = new List<int>();
            foreach (int i in arr)
            {
                if (callback(i))
                {
                    list.Add(i);
                }
            }

            return list;
        }

        public delegate int NumberChanger(int origin, int change);

        public static int DelegateUser(NumberChanger callback, int origin, int number)
        {
            Console.WriteLine(origin);

            int result = callback(origin, number);

            Console.WriteLine(number);
            return result;
        }


        public static int AddNumber(int origin, int add)
        {
            return origin + add;
        }

        public static int SubstractNumber(int origin, int sub)
        {
            return origin - sub;
        }

        public class Mathematic
        {
            public Mathematic() { }
            public static int Calculate(int a, int b, int c)
            {
                return a * b * c;
            }
        }

    }
}
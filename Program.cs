using System;
using System.Threading;

namespace Змейка
{
    class Program
    {
        public static char[,] mass = new char[10, 10];
        public static int x, y, q, z, n = 0;
        public static ConsoleKeyInfo memory;
        public static int[] snakeX = new int[128];
        public static int[] snakeY = new int[128];
        public static bool crash = false;

        static void Main(string[] args)
        {
            int slojnost;
            Console.WriteLine("Введите уровень сложности (1-3)");
            slojnost = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            CreateMass(slojnost);
            Outmass();
            Zmeyka();
        }
        public static ConsoleKeyInfo move()
        {
            ConsoleKeyInfo cki;
            cki = Console.ReadKey();
            memory = cki;
            return cki;
        }

        public static char[,] Zmeyka()
        {
            while (x != 0 && y != 0 && x != 9 && y != 9 && crash == false)
            {
                if (Console.KeyAvailable == true)
                {
                    ConsoleKeyInfo cki = move();
                    if (cki.Key.ToString() == "A")
                    {
                        Movehvost();
                        mass[x, y - 1] = mass[x, y];
                        if (n == 0)
                            mass[x, y] = ' ';
                        y -= 1;
                        mass[x, y] = 'X';
                    }
                    else if (cki.Key.ToString() == "W")
                    {
                        Movehvost();
                        mass[x - 1, y] = mass[x, y];
                        if (n == 0)
                            mass[x, y] = ' ';
                        x -= 1;
                        mass[x, y] = 'X';
                    }
                    else if (cki.Key.ToString() == "S")
                    {
                        Movehvost();
                        mass[x + 1, y] = mass[x, y];
                        if (n == 0)
                            mass[x, y] = ' ';
                        x += 1;
                        mass[x, y] = 'X';
                    }
                    else if (cki.Key.ToString() == "D")
                    {
                        Movehvost();
                        mass[x, y + 1] = mass[x, y];
                        if (n == 0)
                            mass[x, y] = ' ';
                        y += 1;
                        mass[x, y] = 'X';
                    }

                }
                else if (Console.KeyAvailable == false)
                {
                    if (memory.Key.ToString() == "A")
                    {
                        Movehvost();
                        mass[x, y - 1] = mass[x, y];
                        if (n == 0)
                            mass[x, y] = ' ';
                        y -= 1;
                        mass[x, y] = 'X';
                    }
                    else if (memory.Key.ToString() == "W")
                    {
                        Movehvost();
                        mass[x - 1, y] = mass[x, y];
                        if (n == 0) 
                            mass[x, y] = ' ';
                        x -= 1;
                        mass[x, y] = 'X';
                    }
                    else if (memory.Key.ToString() == "S")
                    {
                        Movehvost();
                        mass[x + 1, y] = mass[x, y];
                        if (n == 0) 
                            mass[x, y] = ' ';
                        x += 1;
                        mass[x, y] = 'X';
                    }
                    else if (memory.Key.ToString() == "D")
                    {
                        Movehvost();
                        mass[x, y + 1] = mass[x, y];
                        if (n == 0) 
                            mass[x, y] = ' ';
                        y += 1;
                        mass[x, y] = 'X';
                    }
                }

                if (x == q && y == z)
                {
                    CreateEda(mass);
                    Plushvost();
                }

                Console.SetCursorPosition(0, 0);
                Outmass();

                for (int i = 1; i < n - 1; i++)
                {
                    if (x == snakeX[i] && y == snakeY[i])
                    {
                        crash = true;
                    }
                }

                Thread.Sleep(300);
            }
            Console.WriteLine("Defeat");
            return mass;
        }
        public static void Movehvost() 
        {
            for (int i = 0; i < n; i++)
            {
                mass[snakeX[i], snakeY[i]] = ' ';
            }

            for (int i = n; i >= 1; i--)
            {
                snakeX[i] = snakeX[i - 1];
                snakeY[i] = snakeY[i - 1];
            }

            snakeX[0] = x;
            snakeY[0] = y;

            for (int i = 0; i < n; i++)
            {
                mass[snakeX[i], snakeY[i]] = '*';
            }
        }
        public static void Plushvost() 
        {
            snakeX[n] = x;
            snakeY[n] = y;
            n++;
        }
        public static char[,] CreateEda(char[,] mass)
        {
            Random rand = new Random();
        A:
            q = rand.Next(1, 9);
            z = rand.Next(1, 9);

            for (int i = 0; i < n; i++)
            {
                if(q == snakeX[i] && z == snakeY[i])               
                    goto A;                
            }

            if (x == q && y == z )
            {
                goto A;
            }

            else
            {
                mass[q, z] = 'O';
            }
            return mass;
        }
        public static char[,] CreateMass(int sloj)
        {
            //int pole;
            //switch(slo)
            //{
            //    default:
            //        break;
            //}
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == 0 || j == 0 || i == 9 || j == 9)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        mass[i, j] = '#';
                    }
                    else
                        mass[i, j] = ' ';
                }
            }

            Random rand = new Random();
        A:
            x = rand.Next(2, 8);
            y = rand.Next(2, 8);
            q = rand.Next(2, 8);
            z = rand.Next(2, 8);
            if (x == q && y == z)
            {
                goto A;
            }
            else
            {
                mass[x, y] = 'X';
                mass[q, z] = 'O';
            }
            return mass;
        }
        public static char[,] Outmass()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(mass[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("n - " + n);
            Console.WriteLine(x + " " + y);
            for (int i = 0; i < n; i++)
                Console.WriteLine(snakeX[i] + " " + snakeY[i]);         
            
            

            return mass;
        }
    }
}

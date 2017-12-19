using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DiretoX
{
    class Program
    {
       
        public static Cell[,] Celulas;
        public static int posX;
        public static int posY;
        static bool Vitoria = false;
        static eTipo Ganhou = eTipo.Vazio;

        static void Main(string[] args)
        {
            Setup();
            new Thread(tRotinaKey).Start();
            new Thread(tGame).Start();
        }
        static void Setup()
        {
            Celulas = new Cell[3,3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    Celulas[i, j] = new Cell();
        }
        static bool CheckCell(int x, int y)
        {
            int count = 0;
            // Células Verticais
            for (int i = -2; i <= 2; i++)
            {
                if ((x + i) > -1 && (x + i) < 3)
                {
                    if (Celulas[x + i, y].Selecionado && Celulas[x + i, y].Tipo == Celulas[x, y].Tipo)
                    {                    
                        count++;
                    }         
                    
                }
            }
            if (count == 3)
                return true;
            count = 0;
            //Células Horizontais
            for (int j = -2; j <= 2; j++)
            {
                if ((y + j) > -1 && (y + j) < 3)
                {
                    if (Celulas[x , y + j].Selecionado && Celulas[x , y + j].Tipo == Celulas[x, y].Tipo)
                    {
                        count++;
                    }
                }
            }
            if (count == 3)
                return true;
            count = 0;
            //Células Diagonais
            for (int j = -2; j <= 2; j++)
            {
                if ((x + j) > -1 && (x + j) < 3 && (y + j) > -1 && (y + j) < 3)
                {
                    if (Celulas[x +j, y + j].Selecionado && Celulas[x +j, y + j].Tipo == Celulas[x, y].Tipo)
                    {
                        count++;
                    }
                }
            }
            if (count == 3)
                return true;
            
           return false;
        }
     
        static void tGame()
        {
            while (!Vitoria)
            {
                for(int i = 0; i < 3; i++)
                {
                    for(int j = 0; j < 3; j++)
                    {
                        if (CheckCell(i, j))
                            Vitoria = true;

                        if (i == posX && j == posY)
                            Console.ForegroundColor = ConsoleColor.Green;
                        else
                            Console.ForegroundColor = ConsoleColor.Gray;

                        Celulas[i, j].Draw(i, j);
                    }
                }
                Thread.Sleep(100);
            }
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Green;
            if (Ganhou == eTipo.O)
                Console.WriteLine("Computador ganhou!");
            else
                Console.WriteLine("Voce foi o ganhador!");

        }
        static void BotSelect() {
            bool Sel = false;
            while (!Sel)
            {
                int RandX = new Random().Next(0, 3);
                Thread.Sleep(15);
                int RandY = new Random().Next(0, 3);
                if(!Celulas[RandX, RandY].Selecionado)
                {
                    Celulas[RandX, RandY].Selecionar(eTipo.O);
                    Sel = true;
                }
            }
        }
        static void tRotinaKey()
        {
            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (posX > 0)
                            posX--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (posX < 2)
                            posX++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (posY > 0)
                            posY--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (posY < 2)
                            posY++;
                        break;
                    case ConsoleKey.Enter:
                        Celulas[posX, posY].Selecionar(eTipo.X);
                       BotSelect();
                        break;                    
                    default:
                        break;
                }
            }
        }
    }
    enum eTipo
    {
        Vazio,
        X,
        O
    }
    class Cell
    {
        public bool Selecionado { get; set; }
        public eTipo Tipo { get; set; }
        public Cell()
        {
            this.Selecionado = false;
            Tipo = eTipo.Vazio;
        }
        public void Selecionar(eTipo x)
        {
            Tipo = x;
            Selecionado = true;
        }
        public void Draw(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            switch (Tipo)
            {
                case eTipo.O:
                    Console.Write("O");
                    break;
                case eTipo.X:
                    Console.Write("X");
                    break;
                default:
                    Console.Write("_");
                    break;
            }
            
        }
    }
}

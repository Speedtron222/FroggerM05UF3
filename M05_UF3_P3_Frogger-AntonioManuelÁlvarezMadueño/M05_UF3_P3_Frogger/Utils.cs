using System;
using System.Collections.Generic;
using System.Text;

namespace M05_UF3_P3_Frogger
{
    public class Vector2Int // 2 Warnings¿?
    {
        public int x;
        public int y;
        public Vector2Int(int x = 0, int y = 0) { this.x = x; this.y = y; }

        public static Vector2Int zero { get { return new Vector2Int(); } }
        public static Vector2Int right { get { return new Vector2Int(x: 1); } }
        public static Vector2Int left { get { return new Vector2Int(x: -1); } }
        public static Vector2Int up { get { return new Vector2Int(y: -1); } }
        public static Vector2Int down { get { return new Vector2Int(y: 1); } }

        public override bool Equals(object obj) //Compara un objeto con las coordenadas del vector ¿?
        {
            if(obj is Vector2Int)
            {
                return this == (Vector2Int)obj;
            }
            return base.Equals(obj);
        }

        public override string ToString() //Pasa un vector a string.
        {
            return "(" + x + ", " + y + ")";
        }

        //Comparadores para vectores.
        public static Vector2Int operator +(Vector2Int a, Vector2Int b) => new Vector2Int(a.x + b.x, a.y + b.y);
        public static bool operator ==(Vector2Int a, Vector2Int b) => a.x == b.x && a.y == b.y;
        public static bool operator !=(Vector2Int a, Vector2Int b) => a.x != b.x || a.y != b.y;
    }
    public static class Utils
    {
        public static Random rnd = new Random();
        public const int MAP_WIDTH = 20; //ancho
        public static int MAP_HEIGHT = 13; //alto

        public const char charCars = '╫'; //"Sprite" y colores de los coches.
        public static readonly ConsoleColor[] colorsCars = { ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.Red, ConsoleColor.Red };
        public const char charLogs = '='; //"Sprite" y colores de los troncos.
        public static readonly ConsoleColor[] colorsLogs = { ConsoleColor.DarkYellow, ConsoleColor.Yellow};

        public enum GAME_STATE { RUNNING, WIN, LOOSE };

        public static Vector2Int Input() //Modifica los vectores según los inputs de jugador.
        {
            if(Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        return Vector2Int.left;
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        return Vector2Int.up;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        return Vector2Int.right;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        return Vector2Int.down;
                }
            }
            return Vector2Int.zero;
        }
    }
} 
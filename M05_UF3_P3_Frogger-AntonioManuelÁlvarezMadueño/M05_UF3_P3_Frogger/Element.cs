using System;
using System.Collections.Generic;

namespace M05_UF3_P3_Frogger
{
    public abstract class Element
    {
        public Vector2Int pos { get; protected set; }
        public char character { get; protected set; }
        public readonly ConsoleColor foreground;

        public Element(Vector2Int pos, char character = ' ', ConsoleColor foreground = ConsoleColor.White)
        {// Constructor de elementos, vacío y blanco predeterminados.
            this.pos = pos;
            this.character = character;
            this.foreground = foreground;
        }

        public virtual void Draw() //Dibuja un caracter en específico (posición, color y caracter).
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.ForegroundColor = foreground;
            Console.Write(character);
        }
        public virtual void Draw(ConsoleColor background) //Establece el color del fondo y después llama al Draw() de arriba.
        {
            Console.BackgroundColor = background;
            Draw();
        }
        public abstract void Update(); // ¿?
    }

    public class DynamicElement : Element // Hereda de Element.
    {

        public Vector2Int speed { get; protected set; }
        public DynamicElement(Vector2Int speed, Vector2Int pos, char character = ' ', ConsoleColor foreground = ConsoleColor.White) : base(pos, character, foreground)
        {
            this.speed = speed; //Constructor nuevo para elemntos dinámicos, añade Speed.
        }

        public override void Update() //Evita las salidas de la pantalla calculando la posición en el siguiente frame (pos += speed).
        {
            pos += speed;
            if(pos.x >= Utils.MAP_WIDTH)
            {
                pos.x = 0;
            }
            else if (pos.x < 0)
            {
                pos.x = Utils.MAP_WIDTH - 1;
            }
            if(pos.y >= Utils.MAP_HEIGHT)
            {
                pos.y = 0;
            }
            else if (pos.y < 0)
            {
                pos.y = Utils.MAP_HEIGHT - 1;
            }
        }
        public virtual void Update(Vector2Int dir) //Añade una velocidad y llama al Update() de arriba.
        {
            speed = dir;
            Update();
        }
    }

    public class Player : DynamicElement
    {
        public const char characterForward = '╧';
        public const char characterBackwards = '╤';
        public const char characterLeft = '╢';
        public const char characterRight = '╟';

        public Player() : base(Vector2Int.zero, new Vector2Int(Utils.MAP_WIDTH / 2, Utils.MAP_HEIGHT - 1), characterForward, ConsoleColor.Green)
        {//Crea al jugador abajo en el centro de la pantalla.
        }

        public Utils.GAME_STATE Update(Vector2Int dir, List<Lane> lanes)
        { //Calcula el estado de la partida con las lineas y una dirección
            speed = dir;

            if(dir.y < 0)
            { character = characterForward; }
            else if (dir.y > 0)
            { character = characterBackwards;}
            else if (dir.x > 0)
            { character = characterRight; }
            else if (dir.x < 0)
            { character = characterLeft; }

            pos += speed;
            if (pos.y <= 0) //Comprueba si en el siguiente frame gana, muere o sigue.
            {
                return Utils.GAME_STATE.WIN;
            }
            else if (pos.y >= Utils.MAP_HEIGHT)
            {
                pos.y = Utils.MAP_HEIGHT - 1;
            }
            foreach (Lane lane in lanes)
            {
                if (lane.posY == pos.y)
                {
                    if (lane.speedPlayer) {
                        pos.x += lane.speedElements;
                    }
                    if (pos.x >= Utils.MAP_WIDTH)
                    {
                        pos.x = 0;
                    }
                    else if (pos.x < 0)
                    {
                        pos.x = Utils.MAP_WIDTH - 1;
                    }
                    if (lane.ElementAtPosition(pos) == null)
                    {
                        if (lane.damageBackground)
                        {
                            return Utils.GAME_STATE.LOOSE;
                        }
                        else
                        {
                            return Utils.GAME_STATE.RUNNING;
                        }
                    }
                    else
                    {
                        if (lane.damageElements)
                        {
                            return Utils.GAME_STATE.LOOSE;
                        }
                        else
                        {
                            return Utils.GAME_STATE.RUNNING;
                        }
                    }
                }
            }
            return Utils.GAME_STATE.RUNNING;
        }

        public void Draw(List<Lane> lanes)
        {  // Pone de color de fondo el color de cada línea y despues llama al Draw() de arriba (el de caracteres específicos).
            foreach (Lane lane in lanes)
            {
                if (lane.posY == pos.y)
                {
                    Console.BackgroundColor = lane.background;
                }
            }
            base.Draw();
        }
    }
}

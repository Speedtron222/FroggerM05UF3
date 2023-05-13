using System;
using System.Collections.Generic;
using System.Linq;

namespace M05_UF3_P3_Frogger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            List<Lane> lanes = new List<Lane>();
            Utils.GAME_STATE gameState = Utils.GAME_STATE.RUNNING;
            TimeManager.timer.Start();
            bool speedPlayer;
            ConsoleColor background;
            bool damageElements;
            bool damageBackground;
            float elementsPercent;
            char elementsChar;
            List<ConsoleColor> colorsElements = Utils.colorsLogs.ToList();
            for (int i = 1; i < Utils.MAP_HEIGHT-1; i++)
            {
                if (i<Utils.MAP_HEIGHT/2)
                {
                    speedPlayer = true;
                    background = ConsoleColor.Cyan;
                    damageElements = false;
                    damageBackground = true;
                    elementsPercent = 0.5f; //Spawn rate troncos
                    elementsChar = Utils.charLogs;
                    colorsElements = Utils.colorsLogs.ToList();
                }
                else if(i > Utils.MAP_HEIGHT / 2)
                {
                    speedPlayer = false;
                    background = ConsoleColor.Black;
                    damageElements = true;
                    damageBackground = false;
                    elementsPercent = 0.3f; //Spawn rate coches
                    elementsChar = Utils.charCars;
                    colorsElements = Utils.colorsCars.ToList();
                } else {
                    speedPlayer = false;
                    background = ConsoleColor.DarkGreen;
                    damageElements = false;
                    damageBackground = false;
                    elementsPercent = 0f;
                    elementsChar = ' ';
                    colorsElements = Utils.colorsCars.ToList();
                }
                Lane lane = new Lane(i, speedPlayer, background, damageElements, damageBackground, elementsPercent, elementsChar, colorsElements);
                lanes.Add(lane);
            }

            Lane lane0 = new Lane(0, false, ConsoleColor.DarkGreen, false, false, 0f, ' ', colorsElements);
            Lane laneEnd = new Lane(Utils.MAP_HEIGHT-1, false, ConsoleColor.Black, false, false, 0f, ' ', colorsElements);
            lanes.Add(lane0);
            lanes.Add(laneEnd);

            while (gameState == Utils.GAME_STATE.RUNNING)
            {
                Console.Clear();

                foreach (Lane lane in lanes)
                {
                    lane.Update();
                    lane.Draw();
                }
                player.Draw(lanes);

                Vector2Int input = Utils.Input();

                gameState = player.Update(input, lanes);

                if (gameState == Utils.GAME_STATE.WIN)
                {
                    Console.WriteLine("You Win!");
                    break;
                }
                else if (gameState == Utils.GAME_STATE.LOOSE)
                {
                    Console.WriteLine("You lose...");
                    break;
                }
                TimeManager.NextFrame();
            }
            TimeManager.timer.Stop();
        }
    }
}

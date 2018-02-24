using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.GameMenu();            
            Console.Clear();                      
            game.GameInit();                     
            while (game.gameover)
            {
                
                if (Game.snake.EatFood())
                {
                    Game.food.FoodGenerate();
                    Game.food.Draw();
                    Game.snake.Grow();
                    game.score++;
                    if(game.score % 3 == 0)
                    {
                        Game.walls.GenerateWalls(++game.level);
                        Game.walls.Draw();
                    }
                }
                if(Game.snake.EatBody() || Game.snake.EatWalls())
                {
                    game.GameEnd();                    
                }
                if (Console.KeyAvailable)
                {                    
                    ConsoleKeyInfo btn = Console.ReadKey();
                    if (btn.Key == ConsoleKey.Enter)
                    {
                        game.GamePause();
                    }
                    Game.snake.HandleDirection(btn);                                        
                }
                Game.snake.Move();
                Game.snake.Draw();                
                Thread.Sleep(150);
            }

            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    [Serializable]
    public class Food
    {
        public Point food;
        public Random random;
        public Food()
        {
            food = new Point();
            random = new Random();            
            food.sym = '$';
            food.color = ConsoleColor.DarkBlue;
            FoodGenerate();
        }
        public bool FoodIntersection()
        {
            foreach (Point p in Game.snake.body)
            {
                if (p.x == food.x && p.y == food.y)
                    return false;
            }
            foreach (Point p in Game.walls.body)
            {
                if (p.x == food.x && p.y == food.y)
                    return false;
            }
            return true;
        }
        public void FoodGenerate()
        {
            do
            {
                food.x = random.Next(3, 66);
                food.y = random.Next(2, 20);
            } while (!FoodIntersection());
        }
        public void Draw()
        {
            food.Draw();
    }
    }
    
         
 }

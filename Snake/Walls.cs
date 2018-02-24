using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    [Serializable]
    public class Walls
    {
        public List<Point> body;
        public Walls()
        {
            body = new List<Point>();
        }
        public void GenerateWalls(int level = 1)
        {
            body.Clear();

            string path = string.Format(@"level{0}.txt", level);
            string map = File.ReadAllText(path);
            int x = 0, y = 0;
            for (int it = 0; it < map.Length; it++)
            {
                Console.SetCursorPosition(x, y);
                if (map[it] == '#')
                    body.Add(new Point(x, y, '#', ConsoleColor.DarkYellow));
                x++;
                if (map[it] == '!')
                {
                    y++;
                    x = 0;
                }
            }
        }
        public void Draw()
        {
            foreach(Point p in body)
            {
                p.Draw();
                //Console.SetCursorPosition(p.x, p.y);
                //Console.ForegroundColor = p.color;
                //Console.Write(p.sym);
            }
        }
        
    }
}

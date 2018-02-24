using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Snake
{
    [Serializable]
    class Game
    {
        public string name;
        static public Snake snake;
        static public Walls walls;
        static public Food food;
        public static string text;
        public int level;
        public int score;
        public bool gameover;
      //  FileStream fs;

        public Game()
        {
           // fs = new FileStream(@"C:\Users\Admin\Desktop\log.txt", FileMode.Open, FileAccess.Write);                        
                     
            level = 1;
            score = 0;
            gameover = true;
            }
        public void CreateObjects()
        {
            snake = new Snake();
            walls = new Walls();
            food = new Food();
            walls.GenerateWalls(level);
            food.FoodGenerate();

        }
        public void GetName()
        {
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
            Console.WriteLine("Enter your name: ");
            name = Console.ReadLine();
            CreateObjects();
            Game.snake.AddElements();
        }
        public void InfoMenu()
        {
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
            Console.Write("1.НАЧАТЬ НОВУЮ ИГРУ?");
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + 1);
            Console.Write("2.ЗАПУСТИТЬ ПОСЛЕДНЮЮ СОХРАНЕННУЮ");
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + 2);
            Console.Write("3.ЛИДЕР БОРД");
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 + 3);
        }
        public void GameMenu()
        {
            Console.Clear();
            InfoMenu();           
            int choise = 0;                       
                InfoMenu();
                choise = int.Parse(Console.ReadLine());
                switch (choise)
                {
                    case 1:
                        GetName();
                        break;
                    case 2:
                        GameResume();
                        break;
                    case 3:
                        ShowLog();
                        ConsoleKeyInfo btn = Console.ReadKey(); ;
                    GameMenu();
                    break;
                
            }
        }
        public void ShowLog()
        {
            StreamReader sr = new StreamReader(@"C:\Users\Admin\Desktop\log.txt");
            Console.Clear();
            Console.Write(sr.ReadToEnd());                        
            sr.Close();
        }
        public void GamePause()
        {
            File.WriteAllText(@"C:\Users\Admin\Desktop\food.xml", string.Empty);
            File.WriteAllText(@"C:\Users\Admin\Desktop\walls.xml", string.Empty);
            File.WriteAllText(@"C:\Users\Admin\Desktop\snake.xml", string.Empty);
            FileStream snake_stream = new FileStream(@"C:\Users\Admin\Desktop\snake.xml", FileMode.Open, FileAccess.Write);
            FileStream walls_stream = new FileStream(@"C:\Users\Admin\Desktop\walls.xml", FileMode.Open, FileAccess.Write);
            FileStream food_stream = new FileStream(@"C:\Users\Admin\Desktop\food.xml", FileMode.Open, FileAccess.Write);

            XmlSerializer snake_xml = new XmlSerializer(typeof(Snake));
            XmlSerializer walls_xml = new XmlSerializer(typeof(Walls));
            XmlSerializer food_xml = new XmlSerializer(typeof(Food));
            try
            {
                //xs.Serialize(fs, s);
                snake_xml.Serialize(snake_stream, Game.snake);
                walls_xml.Serialize(walls_stream, Game.walls);
                food_xml.Serialize(food_stream, Game.food);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                food_stream.Close();
                walls_stream.Close();
                snake_stream.Close();
            }
            Environment.Exit(0);

        }
        public void GameResume()
        {
            
                FileStream snake_stream = new FileStream(@"C:\Users\Admin\Desktop\snake.xml", FileMode.Open, FileAccess.Read);
                FileStream walls_stream = new FileStream(@"C:\Users\Admin\Desktop\walls.xml", FileMode.Open, FileAccess.Read);
                FileStream food_stream = new FileStream(@"C:\Users\Admin\Desktop\food.xml", FileMode.Open, FileAccess.Read);
                XmlSerializer snake_xml = new XmlSerializer(typeof(Snake));
                XmlSerializer walls_xml = new XmlSerializer(typeof(Walls));
                XmlSerializer food_xml = new XmlSerializer(typeof(Food));
                try
                {
                    //Game.snake = new Snake();
                    //xs.Serialize(fs, s);
                    //Game.snake = new Snake();

                    Game.snake = snake_xml.Deserialize(snake_stream) as Snake;
                    //Game.snake.body.RemoveAt(0);
                    //Game.snake.body.RemoveAt(1);
                    // Game.snake.body.RemoveAt(2);
                    Game.walls = new Walls();
                    Game.walls = walls_xml.Deserialize(walls_stream) as Walls;
                    Game.food = food_xml.Deserialize(food_stream) as Food;

                    //Game.food = new Food();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    snake_stream.Close();
                    walls_stream.Close();
                    food_stream.Close();
                }
            
        }
        public void GetLastGameProccess()
        {
          /*  FileStream xml = new FileStream(@"data.xml", FileMode.Open, FileAccess.Read);

           /* XmlSerializer xs = new XmlSerializer(typeof(Game));
            try
            {
                this = xs.Deserialize(fs) as Game;

                Console.WriteLine(s);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                fs.Close();
            }*/
        }
        public void GameInit()
        {
            Console.CursorVisible = false;
            Console.Clear();
            snake.Draw();
            walls.Draw();
            food.FoodGenerate();
            food.Draw();
        }
        public void GameEnd()
        {
            GameLog();
            gameover = false;
        }
        public void GameLog()
        {
            char nl = '\n';
            text =  string.Format("\nname: {0}, score: {1}", name, score);
            text += nl;
            StreamWriter sw = new StreamWriter(@"C:\Users\Admin\Desktop\log.txt", true);
            sw.WriteLine(text);
            sw.Close();            
        }
            

    }
}

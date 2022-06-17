using System; 
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;



namespace simon { 

     [Serializable()]
     class Score {
        public string name;
        public int scoreValue;

        public Score() {

            name = "kein Name";
            scoreValue = 1; //scoreValue = 1;
        }


    }
    

    class Konsolenraser
    {

        static char c = 'a';
        static int links = 10;
        static int rechts = 50;
        static int auto = 30;
        static int score;
        static int highscore;
    
        static string name = "kein Name";
        static Score[] scores = new Score[10]; 
 
        static void Main(string[] args)
        {
            Stream stream;

            IFormatter formatter = new BinaryFormatter();  
            try {
                stream = new FileStream(@"MyFile.bin",FileMode.Open,FileAccess.Read);
                
                scores = (Score[])formatter.Deserialize(stream);
                stream.Close();
            } catch(FileNotFoundException exception)
            {
                for (int i=0; i<10; i++) {
                    scores[i] = new Score();
                }
                System.IO.FileStream fs = System.IO.File.Create("MyFile.bin");
                fs.Close();
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.Write("     Dein Name: ");
            name = Console.ReadLine();

            do
            {
                c = showMenue();
                if (c == 's') run();

            } while (c != 'b');

            Console.WriteLine("speichern");
            stream = new FileStream("MyFile.bin", FileMode.Open, FileAccess.Write, FileShare.None);  
            formatter.Serialize(stream, scores);  
            stream.Close(); 

            return;
        }

        static char showMenue()
        {

            Console.WriteLine("");
            Console.WriteLine("             Hauptmenü!");
            Console.WriteLine("");
            Console.WriteLine("     s: Spiel starten");
            Console.WriteLine("     a: im Spiel Auto nach links bewegen");
            Console.WriteLine("     d: im Spiel Auto nach rechts bewegen");
            Console.WriteLine("     q: Spiel abbrechen");
            Console.WriteLine("     b: das Spiel beenden");
            Console.WriteLine("     h: Highscoretabelle zeigen");
            Console.WriteLine("");

            c = Console.ReadKey(true).KeyChar;

            if (c == 'h') {

                showHighscoretabelle();
                c = 's';
            }

            if (c == 's' || c == 'b')
            {
                return c;
            }
            else
            {
                Console.WriteLine("     Der Befehl " + c + " ist nicht bekannt!");
                return c;
            }

        }

        static void run()
        {

            Random rnd = new Random();
            int richtung = 0;
            links = 10;
            rechts = 35;
            auto = 20;
            score = 0;
            int speed = 80;


            while (c != 'q')
            {
                int r = rnd.Next(1, 30);
                if (r == 1) richtung = -1;
                if (r == 2) richtung = 1;
                if (r == 3) richtung = 0;
                Thread.Sleep(speed);

                rechts += richtung;
                links += richtung;
                if (links == 0)
                {
                    links = 1;
                    richtung = 1;
                }
                if (rechts == 121)
                {
                    rechts = 120;
                    richtung = -1;
                }

                if (rechts <= auto || links >= auto) //|| 
                {
                    Console.Beep();
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("     Loading....");
                    Thread.Sleep(3000);

                    break;
                }

                if (Console.KeyAvailable == true)
                {
                    c = Console.ReadKey(true).KeyChar;

                    if (c == 'a')
                    {
                        auto--;
                    }

                    if (c == 'd')
                    {
                        auto++;
                    }
                }

                for (int i = 0; i < links; i++)
                {
                    Console.Write(" ");     //linker Abstand zwischen Stkraße und Konsolenfensterende
                }

                Console.Write("#");            //Streckenlinie

                for (int i = 0; i < (auto - links); i++)
                {
                    Console.Write(" ");     //linker Abstand zwischen Stkraße und Konsolenfensterende
                }

                Console.Write("!"); //Auto 

                for (int i = 0; i < (rechts - auto); i++)
                {
                    Console.Write(" ");     //linker Abstand zwischen Stkraße und Konsolenfensterende
                }


                Console.Write("#");            //Streckenlinie
                Console.WriteLine(""); //freier Platz bis zum Konsolenfensterende (2x Tab)
                score++;




            }

            if (score > highscore)
            {
                highscore = score;
                Console.WriteLine("");
                Console.WriteLine("     Neuer HIGHSCORE!!!!!!!!!!!");
            }


            showScore();
            saveScore();


        }

        static void showScore()
        {


            if (score > 200) {
                
                Console.WriteLine("");
                Console.WriteLine("     Super " + name + ", du bist echt gut");
                Console.WriteLine("");
            }    

            Console.WriteLine("");
            Console.WriteLine("     Score: " + score);
            Console.WriteLine("     Highscore: " + highscore);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("     Weiter mit RETURN");

            Console.ReadLine();


        }

        static void showHighscoretabelle() {
            
                Console.WriteLine("");
                Console.WriteLine("     Highscoretabelle: ");
                Console.WriteLine("");

                for (int i=0; i<10; i++) Console.WriteLine("     " + scores[i].scoreValue + "       von     " + scores[i].name);

                Console.ReadLine();
        }

        static void saveScore() {

            int scorestelle = -1;

            for (int i=0; i<10; i++) {

                if (score > scores[i].scoreValue) {

                    for (int ii=9; ii>i; ii--) {
                        scores[ii].scoreValue = scores[ii-1].scoreValue;
                        scores[ii].name = scores[ii-1].name;

                    }
                    scorestelle = i;
                    
                    break;
                }
            }
            if (scorestelle != -1)
            {
                scores[scorestelle].scoreValue = score;
                scores[scorestelle].name = name;
            }
        }

    

    }
}

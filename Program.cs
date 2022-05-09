using System;


namespace _Практика__муравьи_
{

    static class Templates
    {
        public static class Green
        {
            public static class Queen
            {
                public const int healthpoints = 20;
                public const int armor = 8;
                public const int damage = 16;
                public const int min_count_of_larvas = 14;
                public const int max_count_of_larvas = 26;
                public const int first_day_born = 1;
                public const int last_day_born = 5;
                public const int min_count_of_queens = 2;
                public const int max_count_of_queens = 5;
            }
            public static class Elite_Worker
            {
                public const string type = "Elite";
                public const int healthpoints = 8;
                public const int armor = 4;

                public static void Show_Info()
                {
                    Console.WriteLine("Type: {0}", type);
                    Console.WriteLine("HP = {0}  Armor = {1}", healthpoints, armor);
                    Console.WriteLine();
                }
            }
            public static class Elite_Sprinter_Worker
            {
                public const string type = "Elite Sprinter";
                public const int healthpoints = 8;
                public const int armor = 4;

                public static void Show_Info()
                {
                    Console.WriteLine("Type: {0}", type);
                    Console.WriteLine("HP = {0}  Armor = {1}", healthpoints, armor);
                    Console.WriteLine("Modifier: {0}", "Не может быть атакован первым");
                    Console.WriteLine();
                }
            }
            public static class Older_Warrior
            {
                public const string type = "Older";
                public const int healthpoints = 2;
                public const int armor = 1;
                public const int damage = 2;

                public static void Show_Info()
                {
                    Console.WriteLine("Type: {0}", type);
                    Console.WriteLine("HP = {0}  Armor = {1}  Damage = {2}", healthpoints, armor, damage);
                    Console.WriteLine();
                }
            }
            public static class Legendary_Inspired_Warrior
            {
                public const string type = "Legendary Inspired";
                public const int healthpoints = 10;
                public const int armor = 6;
                public const int damage = 6;

                public static void Show_Info()
                {
                    Console.WriteLine("Type: {0}", type);
                    Console.WriteLine("HP = {0}  Armor = {1}  Damage = {2}", healthpoints, armor, damage);
                    Console.WriteLine("Modifier: {0}", "Может атаковать 3 цели за раз и наносит 1 укус");
                    Console.WriteLine("          Если убил врага становится неуязвимый до конца похода");
                    Console.WriteLine();
                }
            }
            public static class Butterfly
            {
                public const string type = "Hardworking Invulnerable Aggressive Mythical Unlucky -- Butterfly";
                public const int healthpoints = 21;
                public const int armor = 8;
                public const int damage = 8;

                public static void Show_Info()
                {
                    Console.WriteLine("Type: {0}", type);
                    Console.WriteLine("HP = {0}  Armor = {1}  Damage = {2}", healthpoints, armor, damage);
                    Console.WriteLine("Modifier: {0}", "Полностью неуязвим для всех атак (даже смертельных для неуязвимых)");
                    Console.WriteLine("          Отменяет все модификаторы вражеских воинов");
                    Console.WriteLine("          Убивает любого с 1 укуса (даже неуязвимых)");
                    Console.WriteLine("          Может потерять ресурс по пути в колонию");

                    Console.WriteLine();
                }
            }
        }
        static class Red
        {

        }
        static class Black
        {

        }

    }

    /// <summary>
    /// ///////////////////////////////////////
    /// </summary>
    /// 

    abstract class Insect
    {
        protected string name;
        protected string type;
        protected int healthpoints;
        protected int armor;

        protected int num_of_colony;
        protected string colour_of_colony;

        public Insect(string name, string type, int hp, int armor, int num, string colour)
        {
            this.name = name;
            this.type = type;
            this.healthpoints = hp;
            this.armor = armor;
            this.num_of_colony = num;
            this.colour_of_colony = colour;
        }
        public virtual void Show_info()
        {
            Console.WriteLine("Colony: {0}-{1}", colour_of_colony, num_of_colony);
            Console.WriteLine("Type: {0}", type);
            Console.WriteLine("HP = {0}  Armor = {1}", healthpoints, armor);
            Console.WriteLine("Serves the Queen: {0}", name);
            Console.WriteLine();
        }
        public virtual void Take_Damage(int damage) { if (armor < damage) healthpoints -= damage - armor; }

        public void Is_Alive()
        {

        }
        public void Kill()
        {
            /////////// Как убить муравья?
        }
    }
    ////////////////////////////////////////////////////////////
    class Queen : Insect
    {
        private int damage;
        private int larvas = 0;
        private int cooldown = 0;
        private int queens_in_pack = 0;

        private int[] consts_for_creation = new int[6];

        public Queen(string name, int hp, int armor, int dm, int a, int b, int c, int d, int e, int f,                      
            int num, string colour) : base(name, "Queen", hp, armor, num, colour)                           // переименуй нормально
        {
            this.damage = dm;
            consts_for_creation[0] = a;
            consts_for_creation[1] = b;
            consts_for_creation[2] = c;
            consts_for_creation[3] = d;
            consts_for_creation[4] = e;
            consts_for_creation[5] = f;
        }
        public override void Show_info()
        {
            Console.WriteLine("Colony: {0}-{1}", colour_of_colony, num_of_colony);
            Console.WriteLine("Name of Queen: {0}", name);
            Console.WriteLine("HP = {0}  Armor = {1}  Damage = {2}", healthpoints, armor, damage);
            Console.WriteLine("Count of Larvas = {0}  Cooldown = {1}", larvas, cooldown);
            Console.WriteLine();
        }
        public void Create_larvas()
        {
            if (larvas == 0)
            {
                Random random = new Random();

                larvas = random.Next(consts_for_creation[0], consts_for_creation[1] + 1);
                cooldown = random.Next(consts_for_creation[2], consts_for_creation[3] + 1);
                if (num_of_colony == 0) queens_in_pack = random.Next(consts_for_creation[4], consts_for_creation[5] + 1);
            }
        }
        public void Create_Insects(Field field)
        {
            if (cooldown == 0)
            {
                Random random = new Random();
                larvas -= queens_in_pack;
                field.Add_Anthills(queens_in_pack, colour_of_colony);

                int count_of_workers_1 = random.Next(0, larvas + 1);
                larvas -= count_of_workers_1;
                int count_of_workers_2 = random.Next(0, larvas + 1);
                larvas -= count_of_workers_2;
                int count_of_warriors_1 = random.Next(0, larvas + 1);
                larvas -= count_of_warriors_1;
                int count_of_warriors_2 = random.Next(0, larvas + 1);
                larvas -= count_of_warriors_2; 
                
                field.Add_Insects(num_of_colony, count_of_workers_1, count_of_workers_2, count_of_warriors_1, count_of_warriors_2, larvas, colour_of_colony);
                Create_larvas();
            } else { cooldown--; }
        }
    }

    class Green_Queen : Queen
    {
        public Green_Queen(string name, int num, string colour) : base(name, Templates.Green.Queen.healthpoints, Templates.Green.Queen.armor, 
            Templates.Green.Queen.damage, Templates.Green.Queen.min_count_of_larvas, Templates.Green.Queen.max_count_of_larvas, 
            Templates.Green.Queen.first_day_born, Templates.Green.Queen.last_day_born, 
            Templates.Green.Queen.min_count_of_queens, Templates.Green.Queen.max_count_of_queens, num, colour) { }
    }
    ////////////////////////////////////////////////////////////
    abstract class Worker : Insect
    {
        public Worker(string name, string type, int hp, int armor, int num, string colour) : base(name, type, hp, armor, num, colour) { }

        public abstract void Take_Resourses(Anthill home, Resourse_Base resourse_b);
        public override void Take_Damage(int damage) { Kill(); }
    }

    class Elite_Worker : Worker
    {
        private static int count = 0;

        public Elite_Worker(int num) : base("Rogneda", Templates.Green.Elite_Worker.type, Templates.Green.Elite_Worker.healthpoints, 
            Templates.Green.Elite_Worker.armor, num, "Green") { count++; }            // цвет в шаблон
        public override void Take_Resourses(Anthill home, Resourse_Base resourse_b)     // взаимодействие с i-тым зелёным муравейником и выбранной ресурсной базой
        {
            if (resourse_b.Take_A_Stone()) { home.Add_A_Stone(); }              // добавить домой камень, если есть
            if (resourse_b.Take_A_Dew()) { home.Add_A_Dew(); }                  // добавить домой росу, если есть
        }
    }
    class Elite_Sprinter_Worker : Worker
    {
        private static int count = 0;

        public Elite_Sprinter_Worker(int num) : base("Rogneda", Templates.Green.Elite_Sprinter_Worker.type, 
            Templates.Green.Elite_Sprinter_Worker.healthpoints, Templates.Green.Elite_Sprinter_Worker.armor , num, "Green") { count++; }
        
        public override void Take_Damage(int damage) { }                        // особенность элитного (метод)
        public override void Take_Resourses(Anthill home, Resourse_Base resourse_b)
        {
            if (resourse_b.Take_A_Stone()) { home.Add_A_Stone(); }              // добавить домой камень, если есть
            if (resourse_b.Take_A_Dew()) { home.Add_A_Dew(); }                  // добавить домой росу, если есть
        }
    }
    ////////////////////////////////////////////////////////////
    abstract class Warrior : Insect
    {
        protected int damage;
        protected bool mod_switch = true;
        protected static int count_of_enemies = 1;

        public Warrior(string name, string type, int hp, int armor, int dm, int num, string colour) : base(name, type, hp, armor, num, colour) { this.damage = dm; }

        public override void Show_info()
        {
            Console.WriteLine("Colony: {0}-{1}", colour_of_colony, num_of_colony);
            Console.WriteLine("Type: {0}", type);
            Console.WriteLine("HP = {0}  Armor = {1}  Damage = {2}", healthpoints, armor, damage);
            Console.WriteLine("Serves the Queen: {0}", name);
            Console.WriteLine();
        }
        public void Set_Mod_Switch(bool new_mod_switch) { mod_switch = new_mod_switch; }          // костыль для спецов
        public virtual void Deal_Damage(Insect[] enemies) 
        { 
            for (int i = 0; i < Math.Min(count_of_enemies, enemies.Length); i++) enemies[i].Take_Damage(damage); 
        }    // стандартный кусь
    }

    class Older_Warrior : Warrior
    {
        private static int count = 0;

        public Older_Warrior(int num) : base("Rogneda", Templates.Green.Older_Warrior.type, Templates.Green.Older_Warrior.healthpoints, 
            Templates.Green.Older_Warrior.armor, Templates.Green.Older_Warrior.damage, num, "Green") { count++; }
    }
    class Legendary_Inspired_Warrior : Warrior
    {
        private static int count = 0;
        private bool invincibility = false;
        public Legendary_Inspired_Warrior(int num) : base("Rogneda", Templates.Green.Legendary_Inspired_Warrior.type, Templates.Green.Legendary_Inspired_Warrior.healthpoints,
            Templates.Green.Legendary_Inspired_Warrior.armor, Templates.Green.Legendary_Inspired_Warrior.damage, num, "Green") { count++; count_of_enemies = 3; }

        public override void Take_Damage(int damage) { if (!invincibility || !mod_switch) base.Take_Damage(damage); }
        public void Set_Invincible(bool new_invincibility) { invincibility = new_invincibility; }           // особенность элитного (метод) // переписать атаку с проверкой 
        // особенность элитного (метод)
    }
    ////////////////////////////////////////////////////////////
    class Butterfly : Warrior
    {
        private static int count = 0;

        public Butterfly(int num) : base("Rogneda", Templates.Green.Butterfly.type, Templates.Green.Butterfly.healthpoints,
            Templates.Green.Butterfly.armor, Templates.Green.Butterfly.damage, num, "Green") { count++; count_of_enemies = 3; }

        public void Take_Resourses(Anthill home, Resourse_Base resourse_b)
        {
            int i = 0;
            while (i < 3)
            {
                Random random = new Random();
                if (resourse_b.Take_A_Stone()) { if (random.Next(0, 2) > 0) home.Add_A_Stone(); i++; }                   // добавить домой камень, если есть
                else if (resourse_b.Take_A_Dew()) { if (random.Next(0, 2) > 0) home.Add_A_Dew(); i++; }                  // добавить домой росу, если есть
                else if (resourse_b.Take_A_Branch()) { if (random.Next(0, 2) > 0) home.Add_A_Branch(); i++; }            // добавить домой ветку, если есть
                else if (resourse_b.Take_A_Leaf()) { if (random.Next(0, 2) > 0) home.Add_A_Leaf(); i++; }                // добавить домой лист, если есть
                else { break; }
            }
        }
        public override void Take_Damage(int damage) { }
        public override void Deal_Damage(Insect[] enemies) { for (int i = 0; i < enemies.Length; i++) enemies[i].Kill(); }
    }
    ////////////////////////////////////////////////////////////                    разделить наведение и атаку
    class Resourse_Base
    {
        protected int num;

        protected int branch;
        protected int stone;
        protected int leaf;
        protected int dew;

        public Resourse_Base(int num, int branch, int stone, int leaf, int dew)
        {
            this.num = num;
            this.branch = branch;
            this.stone = stone;
            this.leaf = leaf;
            this.dew = dew;
        }

        public virtual void Show_info()
        {
            Console.WriteLine("Куча номер {0} ресурсы: ", num);
            Console.WriteLine("веточка: {0}; листик: {1}; камушек: {2}; росинка: {3};", branch, leaf, stone, dew);
        }
        public bool Take_A_Branch() { if (branch-- >= 0) return true; return false; }
        public bool Take_A_Stone() { if (stone-- >= 0) return true; return false; }
        public bool Take_A_Leaf() { if (leaf-- >= 0) return true; return false; }
        public bool Take_A_Dew() { if (dew-- >= 0) return true; return false; }
    }

    class Anthill : Resourse_Base
    {
        private string colour;
        private Queen queen;

        private Worker[] workers_1;
        private Worker[] workers_2;

        private Warrior[] warriors_1;
        private Warrior[] warriors_2;

        private Warrior[] specials;

        public Anthill(int num, string colour) : base(num, 0, 0, 0, 0) // передвавать шаблоны муравьёв?
        { 
            this.colour = colour;

            if (colour == "Green")
            {
                if (num == 0)
                {
                    queen = new Green_Queen("Rogneda", num, "Green");

                    Random random = new Random();
                    int count_of_insects = random.Next(0, 16);
                    workers_1 = new Worker[count_of_insects];
                    workers_2 = new Worker[16 - count_of_insects];
                    for (int i = 0; i < count_of_insects; i++) { workers_1[i] = new Elite_Worker(num); }
                    for (int i = 0; i < 16 - count_of_insects; i++) { workers_2[i] = new Elite_Sprinter_Worker(num); }

                    count_of_insects = random.Next(0, 9);
                    warriors_1 = new Warrior[count_of_insects];
                    warriors_2 = new Warrior[9 - count_of_insects];
                    for (int i = 0; i < count_of_insects; i++) { warriors_1[i] = new Older_Warrior(num); }
                    for (int i = 0; i < 9 - count_of_insects; i++) { warriors_2[i] = new Legendary_Inspired_Warrior(num); }

                    count_of_insects = 1;                                           // оптимизировать для всех. Процедура по сути одинаковая
                    specials = new Butterfly[count_of_insects];
                    for (int i = 0; i < count_of_insects; i++) { specials[i] = new Butterfly(num); }
                } 
                else
                {
                    queen = new Green_Queen("(not a) Rogneda", num, "Green");
                    workers_1 = new Worker[0];
                    workers_2 = new Worker[0];
                    warriors_1 = new Warrior[0];
                    warriors_2 = new Warrior[0];
                    specials = new Butterfly[0];
                }
            }
        }

        public void Create_Larvas() { queen.Create_larvas(); }
        
        // поход для всех > 0 hp

        // метод для королевы... ссылка?
        //
        // добавить\убрать муравья

        public override void Show_info()
        {
            Console.WriteLine("----------------------------------");
            queen.Show_info();
            Console.WriteLine("Ресурсы колнии: ");
            Console.WriteLine("веточка: {0}; листик: {1}; камушек: {2}; росинка: {3};", branch, leaf, stone, dew);
            Console.WriteLine();
            Console.WriteLine("<<<<<<<<<<<<< Рабочие >>>>>>>>>>>>>");
            
            if (colour == "Green") { Templates.Green.Elite_Worker.Show_Info(); }                         //
            
            Console.WriteLine("Count of Workers: {0}", workers_1.Length);
            Console.WriteLine();
            
            if (colour == "Green") { Templates.Green.Elite_Sprinter_Worker.Show_Info(); }                //
            
            Console.WriteLine("Count of Workers: {0}", workers_2.Length);
            Console.WriteLine();
            Console.WriteLine("<<<<<<<<<<<<< Воины >>>>>>>>>>>>>");
            if (colour == "Green") { Templates.Green.Older_Warrior.Show_Info(); }

            Console.WriteLine("Count of Warriors: {0}", warriors_1.Length);
            Console.WriteLine();
            
            if (colour == "Green") { Templates.Green.Legendary_Inspired_Warrior.Show_Info(); }           //
            
            Console.WriteLine("Count of Warriors: {0}", warriors_2.Length);
            Console.WriteLine();
            Console.WriteLine("<<<<<<<<<<<<< Особые >>>>>>>>>>>>>");
            
            if (colour == "Green") { Templates.Green.Butterfly.Show_Info(); }                            //
            
            Console.WriteLine("Count of Specials: {0}", specials.Length);
            Console.WriteLine("----------------------------------");
        }
        public void Add_A_Branch() { branch++; }
        public void Add_A_Stone() { stone++; }
        public void Add_A_Leaf() { leaf++; }
        public void Add_A_Dew() { dew++; }
        public void Create_insects(Field field) { queen.Create_Insects(field); }
        public void Add_Insects(int add_workers_1_count, int add_workers_2_count, 
            int add_warriors_1_count, int add_warriors_2_count, int add_specials_count)
        {
            if (colour == "Green")
            {
                int i = workers_1.Length;
                Array.Resize(ref workers_1, i + add_workers_1_count);
                for (int j = i; j < i + add_workers_1_count; j++) workers_1[j] = new Elite_Worker(num);
                i = workers_2.Length;
                Array.Resize(ref workers_2, i + add_workers_2_count);
                for (int j = i; j < i + add_workers_2_count; j++) workers_2[j] = new Elite_Sprinter_Worker(num);
                i = warriors_1.Length;
                Array.Resize(ref warriors_1, i + add_warriors_1_count);
                for (int j = i; j < i + add_warriors_1_count; j++) warriors_1[j] = new Older_Warrior(num);
                i = warriors_2.Length;
                Array.Resize(ref warriors_2, i + add_warriors_2_count);
                for (int j = i; j < i + add_warriors_2_count; j++) warriors_2[j] = new Legendary_Inspired_Warrior(num);
                i = specials.Length;
                Array.Resize(ref specials, i + add_specials_count);
                for (int j = i; j < i + add_specials_count; j++) specials[j] = new Butterfly(num);
            }
        }
    }

    class Field
    {
        public Anthill[] green_anthills;                              // NEED TO PRIVATE!!!
        //public Anthill[] red_anthills;                              // NEED TO PRIVATE!!!
        //public Anthill[] black_anthills;                            // NEED TO PRIVATE!!!

        private Resourse_Base[] resourse_bases;


        public Field()
        {
            Random random = new Random();
            green_anthills = new Anthill[1];
            green_anthills[0] = new Anthill(0, "Green");
            //red_anthills = new Anthill[1];
            //black_anthills = new Anthill[1];

            resourse_bases = new Resourse_Base[5];
            for (int i = 0; i < 5; i++)
            {
                resourse_bases[i] = new Resourse_Base(i, random.Next(0, 101), random.Next(0, 101), random.Next(0, 101), random.Next(0, 101));
            }  // считывать ресурсы?  для ресурсов есть определённый сетап!!!
        }

        public void Add_Anthills(int add_count, string colour)
        {
            if (colour == "Green")
            {
                int i = green_anthills.Length;
                Array.Resize(ref green_anthills, i + add_count);
                for (int j = i; j < i + add_count; j++) green_anthills[j] = new Anthill(j, colour);
            }
        }
        public void Create_Insects(Field field)
        {
            for (int i = 0; i < green_anthills.Length; i++) { green_anthills[i].Create_insects(field); }        //
        }
        public void Add_Insects(int num, int a, int b, int c, int d, int e, string colour)                  // переименуй нормально
        {
            if (colour == "Green")
            {
                green_anthills[num].Add_Insects(a, b, c, d, e);
            }
        }
        public void Show_Info() { Console.WriteLine("There are {0} Green Anthills and {1} Resourse Bases on the BattleField", green_anthills.Length, resourse_bases.Length); }
    }

    class Game
    {
        
    }
    ////////////////////////////////////////////////////////////    
    ////////////////////////////////////////////////////////////
    class Program
    {
        static void Main(string[] args)                             // язык вывода
        {
            Test();

            Console.ReadKey();
        }

        //static void Class_Test_1()
        //{
        //    Elite_Worker[] ants = new Elite_Worker[Convert.ToInt32(Console.ReadLine())];

        //    for (int i = 0; i < ants.Length; i++)
        //    {
        //        ants[i] = new Elite_Worker();
        //        ants[i].Show_info();
        //    }

        //    Elite_Sprinter_Worker[] ants1 = new Elite_Sprinter_Worker[Convert.ToInt32(Console.ReadLine())];

        //    for (int i = 0; i < ants1.Length; i++)
        //    {
        //        ants1[i] = new Elite_Sprinter_Worker();
        //        ants1[i].Show_info();
        //    }

        //    Older_Warrior[] ants2 = new Older_Warrior[Convert.ToInt32(Console.ReadLine())];

        //    for (int i = 0; i < ants2.Length; i++)
        //    {
        //        ants2[i] = new Older_Warrior();
        //        ants2[i].Show_info();
        //    }

        //    Legendary_Inspired_Warrior[] ants3 = new Legendary_Inspired_Warrior[Convert.ToInt32(Console.ReadLine())];

        //    for (int i = 0; i < ants3.Length; i++)
        //    {
        //        ants3[i] = new Legendary_Inspired_Warrior();
        //        ants3[i].Show_info();
        //    }
        //}                               // тест классов s

        static void Test()
        {
            Field test_field = new Field();
            test_field.Show_Info();

            test_field.green_anthills[0].Create_Larvas();
            test_field.green_anthills[0].Show_info();

            
            test_field.Show_Info();

            Console.WriteLine("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF");
            Console.WriteLine("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF");

            test_field.Create_Insects(test_field);
            test_field.Create_Insects(test_field);
            test_field.green_anthills[0].Show_info();
            test_field.Show_Info();



            //A.wr();
            //Console.WriteLine(A.h);
        }
    }
    class A
    {
        public const int h = 0;
        public static void wr() { Console.WriteLine("FF"); }
    }
}






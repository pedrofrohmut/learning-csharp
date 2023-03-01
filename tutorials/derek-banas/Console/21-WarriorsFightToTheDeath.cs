// dotnet run -v quiet

namespace ns21;

/*
   Thor Attacks Hulk and Deals 74 Damage
   Maximus Has 69 Health

   Hulk Attacks Thor and Deals 6 Damage
   Bob Has 6 Health

   Thor Attacks Hulk and Deals 48 Damage
   Maximus Has 21 Health

   Hulk Attacks Thor and Deals 48 Damage
   Bob Has -42 Health

   Thor has Died and Hulk is Victorious

   Game Over
*/

class Warrior
{
    public string Name      { get; set; } = "NO_NAME";
    public double Health    { get; set; }
    public double AttackMax { get; set; }
    public double BlockMax  { get; set; }

    private Random rand = new Random();

    public Warrior(string name, double health, double attackMax, double blockMax)
    {
        Name      = name;
        Health    = health;
        AttackMax = attackMax;
        BlockMax  = blockMax;
    }

    public double Attack() => rand.Next(1, (int) AttackMax);

    public virtual double Block() => rand.Next(1, (int) BlockMax);
}

class Battle
{
    public static void StartFight(Warrior w1, Warrior w2)
    {
        double demage = 0;
        uint counter = 0;
        while (true) {
            counter++;
            Console.WriteLine("Round " + counter + "\n");

            // Warrior 1 move
            demage = MakeAttack(w1, w2);
            PrintAttackResult(w1, w2, demage);
            if (IsGameOver(w1, w2)) break;

            // Warrior 2 move
            demage = MakeAttack(w2, w1);
            PrintAttackResult(w2, w1, demage);
            if (IsGameOver(w1, w2)) break;
        }
    }

    public static double GetDemage(Warrior w1, Warrior w2)
    {
        var dmg = w1.Attack() - w2.Block();
        return dmg > 0 ? dmg : 0;
    }

    public static void PrintAttackResult(Warrior w1, Warrior w2, double demage)
    {
        Console.WriteLine(w1.Name + " attacks " + w2.Name + " and deals " +  demage + " demage");
        Console.WriteLine(w2.Name + " has " + w2.Health + " health\n");
        if (w2.Health <= 0) {
            Console.WriteLine(w2.Name + " has died and " + w1.Name + " is victorious\n");
            Console.WriteLine("Game Over");
        }
    }

    public static double MakeAttack(Warrior w1, Warrior w2)
    {
        var demage = GetDemage(w1, w2);
        w2.Health -= demage;
        return demage;
    }

    public static bool IsGameOver(Warrior w1, Warrior w2) => w1.Health <= 0 || w2.Health <= 0;
}

interface ITeleportable
{
    string Teleport();
}

class CanTeleport : ITeleportable
{
    public string Teleport() => "Teleported away";
}

class CantTeleport : ITeleportable
{
    public string Teleport() => "Fails at teleporting";
}

class MagicWarrior : Warrior
{
    int teleportChance = 0;
    CanTeleport teleportType = new CanTeleport();

    public MagicWarrior(string name,
                        double health,
                        double attackMax,
                        double blockMax,
                        int teleportChance)
        :base (name, health, attackMax, blockMax)
    {
        this.teleportChance = teleportChance;
    }

    public override double Block()
    {
        var rand = new Random();
        int randDodge = rand.Next(1, 100);
        if (randDodge < this.teleportChance) {
            Console.WriteLine(Name + " " + teleportType.Teleport());
            return double.MaxValue;
        }
        // Block of the parent class (Warrior)
        return base.Block();
    }
}

class WarriorsFightToTheDeath
{
    static void __Main(string[] args)
    {
        var thor = new Warrior("Thor", 100, 26, 10);
        var loki = new MagicWarrior("Loki", 75, 20, 10, 50);
        Battle.StartFight(thor, loki);
    }
}

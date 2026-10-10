using System;
using System.Collections.Generic;
using System.Threading;

namespace CyberDefender
{
    class Program
    {
        // Player Stats
        static int baseHealth = 100;
        static int coins = 50;
        static int score = 0;
        static int wave = 1;

        // Upgrades
        static int numberOfTurrets = 1;
        static int turretDamage = 20;

        static void Main(string[] args)
        {
            Console.Title = "Cyber Defender - Tower Defense";
            Console.CursorVisible = false;

            ShowWelcomeScreen();

            // Main Game Loop
            while (baseHealth > 0)
            {
                ShopPhase();
                if (baseHealth <= 0) break; // Exited game

                CombatPhase();
                wave++;
            }

            GameOverScreen();
        }

        static void ShowWelcomeScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==================================================");
            Console.WriteLine("              CYBER DEFENDER v1.0                 ");
            Console.WriteLine("==================================================");
            Console.ResetColor();
            Console.WriteLine("\nDefend your mainframe from incoming virus waves.");
            Console.WriteLine("Buy turrets and upgrade damage using coins.");
            Console.WriteLine("\nPress ENTER to begin your defense...");
            Console.ReadLine();
        }

        static void ShopPhase()
        {
            bool shopping = true;
            while (shopping)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"--- WAVE {wave} PREPARATION ---");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Health: {baseHealth} HP | Coins: {coins} | Score: {score}");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Current Defense: {numberOfTurrets} Turrets (Damage: {turretDamage} each)");
                Console.ResetColor();
                Console.WriteLine("\n--- UPGRADE SHOP ---");
                Console.WriteLine("1. Buy New Turret (Cost: 30 Coins)");
                Console.WriteLine("2. Upgrade Turret Damage (Cost: 40 Coins)");
                Console.WriteLine("3. Repair Base +20 HP (Cost: 20 Coins)");
                Console.WriteLine("4. START NEXT WAVE");
                Console.WriteLine("5. Surrender (Quit Game)");
                Console.Write("\nSelect an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        if (coins >= 30) { coins -= 30; numberOfTurrets++; Console.WriteLine("Turret Purchased!"); }
                        else Console.WriteLine("Not enough coins!");
                        break;
                    case "2":
                        if (coins >= 40) { coins -= 40; turretDamage += 15; Console.WriteLine("Damage Upgraded!"); }
                        else Console.WriteLine("Not enough coins!");
                        break;
                    case "3":
                        if (coins >= 20) { coins -= 20; baseHealth += 20; Console.WriteLine("Base Repaired!"); }
                        else Console.WriteLine("Not enough coins!");
                        break;
                    case "4":
                        shopping = false;
                        break;
                    case "5":
                        baseHealth = 0;
                        shopping = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
                if (shopping) Thread.Sleep(1000);
            }
        }

        static void CombatPhase()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"!!! WARNING: VIRUS WAVE {wave} INCOMING !!!\n");
            Console.ResetColor();
            Thread.Sleep(1500);

            int enemies = 3 + (wave * 2);
            int enemyHealth = 20 + (wave * 10);
            int enemiesDefeated = 0;

            for (int i = 1; i <= enemies; i++)
            {
                int currentEnemyHealth = enemyHealth;
                Console.Write($"Virus {i}/{enemies} approaching... ");

                // Turrets fire
                int totalDamage = numberOfTurrets * turretDamage;
                currentEnemyHealth -= totalDamage;

                if (currentEnemyHealth <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("DESTROYED!");
                    Console.ResetColor();
                    coins += 10;
                    score += 50;
                    enemiesDefeated++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"SURVIVED! It hit your base for {currentEnemyHealth} damage.");
                    Console.ResetColor();
                    baseHealth -= currentEnemyHealth;
                }

                Thread.Sleep(800);

                if (baseHealth <= 0)
                {
                    Console.WriteLine("\nCRITICAL SYSTEM FAILURE! Mainframe breached.");
                    Thread.Sleep(2000);
                    break;
                }
            }

            if (baseHealth > 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nWave {wave} cleared! Bonus coins awarded.");
                coins += 20;
                Console.ResetColor();
                Thread.Sleep(2000);
            }
        }

        static void GameOverScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("==================================================");
            Console.WriteLine("                   GAME OVER                      ");
            Console.WriteLine("==================================================");
            Console.ResetColor();
            Console.WriteLine($"Waves Survived: {wave - 1}");
            Console.WriteLine($"Final Score: {score}");
            Console.WriteLine("\nThank you for playing Cyber Defender!");
        }
    }
}
﻿namespace Bankomat
{
    // Handles user input and output
    internal class ATM
    {
        private readonly Bank bank;

        public enum Menu
        {
            Main = 0,
            Deposit = 1,
            Withdraw = 2,
            Balance = 3,
            List = 4,
            Exit = 5
        }

        public ATM()
        {
            bank = new Bank();
        }

        public void Run()
        {
            var menuChoice = Menu.Main;
            bool run = true;

            while (run)
            {
                MainMenu();
                menuChoice = SelectChoice(GetInput());

                switch (menuChoice)
                {
                    case Menu.Main:
                        MainMenu();
                        break;
                    case Menu.Deposit:
                        DepositMenu();
                        break;
                    case Menu.Withdraw:
                        WithdrawMenu();
                        break;
                    case Menu.Balance:
                        BalanceMenu();
                        break;
                    case Menu.List:
                        ListMenu();
                        break;
                    case Menu.Exit:
                        ExitMenu();
                        run = false;
                        break;
                    default:
                        Console.Clear();
                        continue;
                }

                menuChoice = Menu.Main;

                Console.Clear();
            }
        }


        /************************
         * Menus
         ************************/

        private void MainMenu()
        {
            Console.WriteLine("    Bankomat2000");
            Console.WriteLine("-----------------------");
            Console.WriteLine("1. Gör insättning.");
            Console.WriteLine("2. Gör uttag.");
            Console.WriteLine("3. Visa saldo.");
            Console.WriteLine("4. Lista alla konton.");
            Console.WriteLine("5. Avsluta.");
        }

        private void DepositMenu()
        {
            Console.Clear();
            int accountNr = GetAccount();

            while (true)
            {
                var amount = GetInput("Summa att sätta in: ");

                try
                {
                    bank.Deposit(accountNr, amount);
                    Console.WriteLine($"{amount} SEK har satts in på ert konto.");
                    Halt();
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Halt();
                }
            }
        }

        private void WithdrawMenu()
        {
            Console.Clear();

            int accountNr = GetAccount();

            var amount = GetInput("Summa att ta ut: ");

            if (bank.Withdraw(accountNr, amount))
            {
                Console.WriteLine($"{amount} SEK har tagits ut från ert konto.");
            }
            else
            {
                Console.WriteLine("Ett fel har inträffat, försök igen.");
            }

            Halt();
        }

        private void BalanceMenu()
        {
            Console.Clear();

            int accountNr = GetAccount();

            try
            {
                var balance = bank.GetBalance(accountNr);

                Console.WriteLine($"Det finns {balance} SEK på ert konto.");
                Halt();
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Halt();
            }
        }

        private void ListMenu()
        {
            Console.Clear();
            Console.WriteLine("----------------------");
            Console.WriteLine("Kontonr \tBalans");
            Console.WriteLine("----------------------");

            string[] accountList = bank.GetAccountList();
            for (int i = 0; i < accountList.Length; i++)
            {
                Console.WriteLine(accountList[i]);
            }

            Console.WriteLine("----------------------");

            Halt();
        }

        private void ExitMenu()
        {
            Console.WriteLine("Tack för att ni använder Bankomat2000");
            Halt();
        }


        /************************
        * Helpers
        ************************/

        private static void Halt()
        {
            Console.WriteLine("Tryck på valfri knapp för att fortsätta");
            Console.ReadKey();
        }


        private int GetAccount()
        {
            while (true)
            {
                var accountNr = GetInput("Ange kontonummer: ");
                if (bank.AccountWithNumberExists(accountNr))
                {
                    return accountNr;
                }
                else
                {
                    Console.WriteLine("Ogiltigt kontonummer, försök igen.");
                }
            }
        }


        private Menu SelectChoice(int choice)
        {
            return (Menu)choice;
        }

        private static int GetInput(string message = "")
        {

            int input;
            while (true)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    Console.Write(message);
                }

                if (!int.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine("Ogiltig input, försök igen!");
                }
                else
                {
                    break;
                }
            }
            return input;
        }
    }
}
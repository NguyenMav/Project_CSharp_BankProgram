using System;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using SplashKitSDK;

namespace BankProgram
{
    public enum MenuOption
    {
        Withdraw,
        Deposit,
        Transfer,
        Print,
        PrintTransactionHistory,
        NewAccount,
        Quit,
    }
    public class Program
    {
        public static void Main()
        {   
            Bank bank = new Bank();

            Account account = new Account ("Maverick", 1000);
            bank.AddAccount(account);
            
            MenuOption userSelection;
                do
                {
                    userSelection = ReadUserOption();
                    switch(userSelection)
                {
                    case MenuOption.Withdraw:
                        DoWithdraw(bank);
                        break;
                    case MenuOption.Deposit:
                        DoDeposit(bank);
                        break;    
                    case MenuOption.Transfer:
                        DoTransfer(bank, bank);
                        break;
                    case MenuOption.Print:
                        DoPrint(account);
                        break;
                    case MenuOption.PrintTransactionHistory:
                        bank.PrintTransactionHistory();
                        break;
                    case MenuOption.NewAccount:
                        DoNewAccount(bank);
                        break;
                    case MenuOption.Quit:
                        Console.WriteLine("You have chosen Quit");
                        break;
                }
                }while (userSelection != MenuOption.Quit);
        }
        public static MenuOption ReadUserOption()
        {
            int option;
            Console.WriteLine("********************************");
            Console.WriteLine("Choose an option [1-7]");
            Console.WriteLine("1. Withdraw");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Transfer");
            Console.WriteLine("4. Print");
            Console.WriteLine("5. Transaction History");
            Console.WriteLine("6. New Account");
            Console.WriteLine("7. Quit");
            Console.WriteLine("********************************");
        
            while (true)
            {
                try
                {
                    Console.Write("Enter your chosen option [1-7]: ");
                    option=Convert.ToInt32(Console.ReadLine());
                    if (option >= 1 && option <= 7)
                    {
                        return (MenuOption)(option - 1);
                    }
                    else
                    {
                        Console.WriteLine("Invalid integer input. Please enter [1-7]");
                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadLine();
                    }
                }
                catch (System.Exception)
                {
                    Console.WriteLine("Invalid string input. Please enter [1-7]");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                }
            }
        }
        private static void DoWithdraw(Bank fromBank)
        {
            Account fromAccount = FindAccount(fromBank);
            if (fromAccount == null) return;

            try
            {
                Console.Write("Please enter the amount you would like to withdraw: ");
                decimal amount = Convert.ToDecimal(Console.ReadLine());
                WithdrawTransaction withdrawTransaction = new WithdrawTransaction(fromAccount, amount);
                fromBank.ExecuteTransaction(withdrawTransaction);
                if (withdrawTransaction.Success)
                {
                    withdrawTransaction.Print();
                    fromAccount.Print();
                }
                else
                {
                    Console.WriteLine("Invalid Withdrawal");
                }
            }
            catch (System.Exception)
            {
                Console.WriteLine("Invalid input");
            }
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
        private static void DoDeposit(Bank toBank)
        {
            Account toAccount = FindAccount(toBank);
            if (toAccount == null) return;

            try
            {
                Console.Write("Please enter the amount you would like to deposit: ");
                decimal amount = Convert.ToDecimal(Console.ReadLine());
                DepositTransaction depositTransaction = new DepositTransaction(toAccount, amount);
                toBank.ExecuteTransaction(depositTransaction);
                if (depositTransaction.Success)
                {
                    depositTransaction.Print();
                    toAccount.Print();
                }
                else
                {
                    Console.WriteLine("Invalid Deposit");
                }
            }
            catch (System.Exception)
            {
                Console.WriteLine("Invalid input");
            }
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
        private static void DoTransfer(Bank fromBank, Bank toBank)
        {
            Console.Write("Enter fromAccount name: ");
            string fromAccountName = Console.ReadLine();
            Account fromAccount = fromBank.GetAccount(fromAccountName);
            if (fromAccount == null) 
            {
                Console.WriteLine("Source account not found.");
                return;
            }
            Console.Write("Enter toAccount name: ");
            string toAccountName = Console.ReadLine();
            Account toAccount = toBank.GetAccount(toAccountName);
            if (toAccount == null) 
            {
                Console.WriteLine("Destination account not found.");
                return;
            }
            try
            {
                Console.Write("Please enter the amount you want to transfer from " + fromAccount.Name + " to " + toAccount.Name + ": ");
                decimal amount = Convert.ToDecimal(Console.ReadLine());
                if (amount <= 0)
                {
                    Console.WriteLine("Invalid amount. Transfer failed");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    return;
                }
                if (amount > fromAccount.Balance)
                {
                    Console.WriteLine("Insufficient funds in " + fromAccount.Name + ".");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    return;
                }
                TransferTransaction transferTransaction = new TransferTransaction(fromAccount, toAccount, amount);
                transferTransaction.Execute();
                if (transferTransaction.Success)
                {
                    Console.WriteLine("Transfer of " + amount + " from " + fromAccount.Name + " to " + toAccount.Name + " successful");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    fromAccount.Print();
                    toAccount.Print();
                }
            }
            catch (System.Exception)
            {
                Console.WriteLine("Transfer failed");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
        private static void DoPrint(Account account)
        {
            account.Print();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
        private static void DoNewAccount(Bank bank)
        {
            try
            {
                Console.Write("Enter the name of the new account: ");
                string name = Console.ReadLine();
                
                Console.Write("Enter the starting balance of the new account: ");
                decimal balance = Convert.ToDecimal(Console.ReadLine());

                if (balance < 0)
                {
                    Console.WriteLine("Invalid input: Starting balance cannot be negative.");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    return;
                }

                Account newAccount = new Account(name, balance);
                bank.AddAccount(newAccount); 
                Console.WriteLine("New account added successfully.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input: Please enter a valid decimal number for the balance.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Invalid input: The balance is too large.");
            }
            catch (System.Exception)
            {
                Console.WriteLine("Invalid input");
                Console.ReadLine();
            }
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
        private static Account FindAccount(Bank fromBank)
        {
            Console.Write("Enter account name: ");
            String name = Console.ReadLine();
            Account result = fromBank.GetAccount(name);
            if ( result == null )
            {
                Console.WriteLine($"No account found with name {name}");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
            return result;
        }
    }
}

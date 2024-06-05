using System;
using System.Collections.Generic;

namespace BankProgram
{
    public class Bank
    {
        private List<Account> _accounts;
        private List<Transaction> _transactions;

        public Bank()
        {
            _accounts = new List<Account>();
            _transactions = new List<Transaction>();
        }

        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }

        public Account GetAccount(string name)
        {
            foreach (var account in _accounts)
            {
                if (account.Name == name)
                {
                    return account;
                }
            }
            return null;
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
            transaction.Execute();
        }

        public void PrintTransactionHistory()
        {
            Console.WriteLine("Transaction History: ");
            foreach (var account in _accounts)
            {
                account.PrintTransactionHistory();
            }
        }
    }
}
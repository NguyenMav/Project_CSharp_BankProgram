using System;
using System.Collections.Generic;
using BankProgram;

public class Account
{
    private decimal _balance;
    private string _name;
    private List<Transaction> _transactions;

    public Account(string name, decimal startingBalance)
    {
        _name = name;
        _balance = startingBalance;
        _transactions = new List<Transaction>();
    }

    public decimal Balance
    {
        get { return _balance; }
        set { _balance = value; }
    }

    public bool Deposit(decimal amountToDeposit)
    {
        if (amountToDeposit > 0)
        {
            _balance += amountToDeposit;
            _transactions.Add(new DepositTransaction(this, amountToDeposit));
            return true;
        }
        return false;
    }

    public bool Withdraw(decimal amountToWithdraw)
    {
        if (amountToWithdraw > 0 && amountToWithdraw <= _balance)
        {
            _balance -= amountToWithdraw;
            _transactions.Add(new WithdrawTransaction(this, amountToWithdraw));
            return true;
        }
        return false;
    }

    public string Name
    {
        get { return _name; }
    }

    public void Print()
    {
        Console.WriteLine("Account name: " + _name);
        Console.WriteLine("Balance: " + _balance);
    }

    public void PrintTransactionHistory()
    {
        Console.WriteLine($"Transaction history for account {_name}:");
        foreach (var transaction in _transactions)
        {
            Console.WriteLine($"Account: {_name}, Transaction: {transaction.GetType().Name}, Amount: {transaction.Amount}, Date: {transaction.DateStamp}");
        }
    }
}
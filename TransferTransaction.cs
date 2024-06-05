using System;

namespace BankProgram
{
    public class TransferTransaction : Transaction
    {
        private Account _fromAccount;
        private Account _toAccount;

        public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
        {
            _fromAccount = fromAccount;
            _toAccount = toAccount;
        }

        public override bool Success
        {
            get { return Executed; }
        }

        public override void Execute()
        {
            base.Execute();
            _fromAccount.Withdraw(Amount);
            _toAccount.Deposit(Amount);
        }

        public override void Rollback()
        {
            base.Rollback();
            _toAccount.Withdraw(Amount);
            _fromAccount.Deposit(Amount);
        }

        public override void Print()
        {
            Console.WriteLine("Transfer of " + Amount + " from account " + _fromAccount.Name + " to account " + _toAccount.Name + " on " + DateStamp);
        }
    }
}
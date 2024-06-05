using System;

namespace BankProgram
{
    public class DepositTransaction : Transaction
    {
        private Account _account;

        public DepositTransaction(Account account, decimal amount) : base(amount)
        {
            _account = account;
        }

        public override bool Success
        {
            get { return Executed; }
        }

        public override void Execute()
        {
            base.Execute();
            _account.Deposit(Amount);
        }

        public override void Rollback()
        {
            base.Rollback();
            _account.Withdraw(Amount);
        }

        public override void Print()
        {
            Console.WriteLine("Deposit of " + Amount + " to account " + _account.Name + " on " + DateStamp);
        }
    }
}
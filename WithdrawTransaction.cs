using System;

namespace BankProgram
{
    public class WithdrawTransaction : Transaction
    {
        private Account _account;

        public WithdrawTransaction(Account account, decimal amount) : base(amount)
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
            _account.Withdraw(Amount);
        }

        public override void Rollback()
        {
            base.Rollback();
            _account.Deposit(Amount);
        }

        public override void Print()
        {
            Console.WriteLine("Withdrawal of " + Amount + " from account " + _account.Name + " on " + DateStamp);
        }
    }
}
using System;

namespace BankProgram
{
    public abstract class Transaction
    {
        protected decimal _amount;
        private DateTime _dateStamp;
        private bool _executed = false;
        private bool _reversed = false;

        public decimal Amount
        {
            get { return _amount; }
        }

        public bool Executed
        {
            get { return _executed; }
        }

        public abstract bool Success { get; }

        public bool Reversed
        {
            get { return _reversed; }
        }

        public DateTime DateStamp
        {
            get { return _dateStamp; }
        }

        public Transaction(decimal amount)
        {
            _amount = amount;
            _dateStamp = DateTime.Now;
        }

        public abstract void Print();

        public virtual void Execute()
        {
            if (_executed)
            {
                throw new InvalidOperationException("Transaction has already been executed.");
            }

            _executed = true;
            _dateStamp = DateTime.Now;
        }

        public virtual void Rollback()
        {
            if (!_executed)
            {
                throw new InvalidOperationException("Transaction has not been executed yet.");
            }

            if (_reversed)
            {
                throw new InvalidOperationException("Transaction has already been reversed.");
            }

            _reversed = true;
            _dateStamp = DateTime.Now;
        }
    }
}
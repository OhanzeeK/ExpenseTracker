using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker
{
    public class Expense: Transaction
    {
        private bool necessary;

        public Expense(string desc, double amount, bool rec, bool nec): base(desc, amount, rec)
        {
            setAmount(amount);
            necessary = nec;
        }


        public override void setAmount(double amount)
        {
            if (amount > 0)
            {
                amount *= -1;
            }
            this.amount = amount;

        }

        public bool getNeces()
        {
            return necessary;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nNecessary: {necessary}";
        }
    }
}

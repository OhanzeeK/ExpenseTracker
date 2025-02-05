using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker
{
    public abstract class Transaction
    {
        protected string description;
        protected double amount;
        protected bool recurring;
        
        public Transaction(string desc, double amount, bool rec)
        {
            this.amount = amount;
            recurring = rec;
            description = desc;
        }

        public override string ToString()
        {
            return $"Description: {description}\nAmount: {amount}\nRecurring: {recurring}";
        }

        public void setDesc(string desc)
        {
            description = desc;
        }

        public void setRec(bool rec)
        {
            recurring = rec;
        }

        public virtual void setAmount(double amount)
        {
            this.amount = amount;
        }

        public string getDesc()
        {
            return description;
        }

        public double getAmount()
        {
            return amount;
        }

        public bool getRec()
        {
            return recurring;
        }
    }
}

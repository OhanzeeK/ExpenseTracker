using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker
{
    public class Income: Transaction
    {
        private double hours;
        private double rate;

        public Income(string desc, double amount, bool rec): base(desc, amount, rec)
        {
            setAmount(amount);
        }

        public Income(string desc, bool rec, double hours, double rate) : base(desc, 0 , rec)
        {
            this.hours = hours;
            this.rate = rate;
            setAmount(hours * rate);
        }

        public override void setAmount(double amount)
        {
            if (amount < 0)
            {
                amount *= -1;
            }
            this.amount = amount;
            
        }

        public void setRate(double rate)
        {
            if (rate < 0)
            {
                rate = 0;
            }
            this.rate = rate;
        }

        public void setHours(double hours)
        {
            if (hours < 0)
            {
                hours = 0;
            }
            this.hours = hours;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

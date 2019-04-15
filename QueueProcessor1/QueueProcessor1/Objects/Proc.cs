using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueProcessor1.Objects
{
    public class Proc
    {   
        public string Name { get; set; }
        public int Priority { get; set; }
        public int Burst { get; set; }
        public int Arrival { get; set; }

        public bool Validate() 
        {
            if (this.Priority >= 0 && this.Burst >= 1 && this.Arrival >= 0) 
            {
                return true;
            }

            return false;
        }
    }

    public enum Color {
        white,
        blue,
        red,
        green,
        purple,
        orange,
        gray
    };
}

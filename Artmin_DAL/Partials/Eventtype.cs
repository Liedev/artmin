using Artmin_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artmin_DAL
{
    public partial class Eventtype : BasisKlasse
    {
        public override string this[string columnName]
        {
            get
            {
                return "";
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Eventtype eventtype && EventtypeID == eventtype.EventtypeID;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Artmin_Models;

namespace Artmin_DAL
{
    public partial class Event: BasisKlasse
    {
        public override string this[string veldnaam]
        {
            get
            {
                if (veldnaam == "Eventnaam" && string.IsNullOrWhiteSpace(Eventnaam))
                {
                    return "Gelieve het event een naam te geven";
                }
                if (veldnaam == "Startuur" && string.IsNullOrWhiteSpace(Startuur))
                {
                    return "Gelieve een startuur te selecteren";
                }
                if (veldnaam == "Einduur" && string.IsNullOrWhiteSpace(Einduur))
                {
                    return "Gelieve een einduur te selecteren";
                }
                return "";
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Event _event && EventID == _event.EventID;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

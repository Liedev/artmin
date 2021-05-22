using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Artmin_Models;
using System.Text.RegularExpressions;

namespace Artmin_DAL
{
    public partial class Notitie : BasisKlasse
    {   //Gemaakt door: Jarno Peeters - R0670336
        public override string this[string veldnaam]
        {
            get
            {
                if (veldnaam == "Titel" && string.IsNullOrWhiteSpace(Titel))
                {
                    return "Titel moet ingevuld zijn!";
                }
                if (veldnaam == "Omschrijving" && string.IsNullOrWhiteSpace(Omschrijving))
                {
                    return "De omschrijving moet ingevuld zijn!";
                }
                return "";
            }
        }

    }
}

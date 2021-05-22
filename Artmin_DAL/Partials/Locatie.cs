using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Artmin_Models;
using System.Text.RegularExpressions;

namespace Artmin_DAL
{//Stijn Beckers - r0795483
    public partial class Locatie : BasisKlasse
    {
        public override string this[string veldnaam]
        {
            get
            {
                if(veldnaam == "Naam" && string.IsNullOrWhiteSpace(Naam))
                {
                    return "Gelieve een naam van de locatie te geven.";
                }
                if (veldnaam == "Straat" && string.IsNullOrWhiteSpace(Straat)) 
                {
                    return "Gelieve de straat van de locatie in te geven." + Environment.NewLine;
                }
                if (veldnaam == "Gemeente" && string.IsNullOrWhiteSpace(Gemeente))
                {
                    return "Gelieve de gemeente van de locatie in te geven." + Environment.NewLine; 
                }
                if (veldnaam == "Manager" && string.IsNullOrWhiteSpace(Manager))
                {
                    return "Gelieve de manager van de locatie in te geven." + Environment.NewLine; 
                }
                if (veldnaam == "Email" && !(IsEenValideEmailAdres(Email)))
                {
                    return "Gelieve een valide e-mail adres in te geven." + Environment.NewLine; 
                }
                if (veldnaam == "Telefoon" && string.IsNullOrWhiteSpace(Telefoon))
                {
                    return "Gelieve een telefoon in te geven." + Environment.NewLine;
                }
                return "";
            }
            
        }

        static bool IsEenValideEmailAdres(string emailadres)
        {
            return Regex.IsMatch(emailadres, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }

        public override bool Equals(object obj)
        {
            return obj is Locatie locatie &&
                LocatieID == locatie.LocatieID;
                   
        }

        public override int GetHashCode()
        {
            return -519215521 + LocatieID.GetHashCode();
        }

        
    }
}

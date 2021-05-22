using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using System.Net.NetworkInformation;

namespace Artmin_DAL
{
    public static class DatabaseOperations
    {
        public static List<Locatie> OphalenLocaties()
        {//Stijn Beckers - r0795483
            using (ArtminEntities artminEntities = new ArtminEntities())
            {
                var query = artminEntities.Locatie 
                    .OrderBy(x => x.Naam);
                return query.ToList();
            }
        }
        public static Locatie OphalenLocatieViaId(int locatieID)
        {//Stijn Beckers - r0795483
            using (ArtminEntities artminEntities = new ArtminEntities())
            {
                return artminEntities.Locatie
                    .Include(x => x.Events)
                    .Where(x => x.LocatieID == locatieID)
                    .SingleOrDefault();
            }
        }
        public static int ToevoegenLocatie(Locatie locatie)
        {//Stijn Beckers - r0795483
            try
            {
                using (ArtminEntities artminEntities = new ArtminEntities())
                {
                    artminEntities.Locatie.Add(locatie);
                    return artminEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.Foutloggen(ex);
                return 0;
            }
        }
        public static List<Event> OphalenEvents()//Lieven
        {
            using (ArtminEntities artminEntities = new ArtminEntities())
            {
                return artminEntities.Event
                    .Include(x => x.Eventtype)
                    .ToList();
            }
        }

        public static Event OphalenEventViaId(int eventId)//Lieven
        {
            using (ArtminEntities artminEntities = new ArtminEntities())
            {
                return artminEntities.Event
                    .Include(x => x.ToDos)
                    .Include(x => x.Eventtype)
                    .Include(x => x.Locatie)
                    .Include(x => x.Notities)
                    .Include(x => x.Artiesten)
                    .Include(x => x.Klant)
                    .Where(x => x.EventID == eventId)
                    .SingleOrDefault();
            }
        }
        public static List<ToDo> OphalenToDosViaEventId(int eventId)//Lieven
        {
            using (ArtminEntities artminEntities = new ArtminEntities())
            {
                return artminEntities.ToDo
                    .Where(x => x.EventID == eventId)
                    .ToList();
            }
        }

        public static List<Eventtype> OphalenEventTypes()//Lieven
        {
            using (ArtminEntities artminEntities = new ArtminEntities())
            {
                return artminEntities.Eventtype
                    .ToList();
            }
        }

        public static int UpdateEvent(Event _event)//Lieven//Stijn
        {
            try
            {
                using (ArtminEntities artminEntities = new ArtminEntities())
                {
                    artminEntities.Entry(_event).State = EntityState.Modified;
                    return artminEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.Foutloggen(ex);
                return 0;
            }
        }

        public static int ToevoegenEvent(Event _event)//Lieven
        {
            try
            {
                using (ArtminEntities artminEntities = new ArtminEntities())
                {
                    artminEntities.Event.Add(_event);
                    return artminEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.Foutloggen(ex);
                return 0;
            }
        }

        public static int VerwijderenEvent(Event _event)//Lieven
        {
            try
            {
                using (ArtminEntities artminEntities = new ArtminEntities())
                {
                    artminEntities.Entry(_event).State = EntityState.Deleted;
                    return artminEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.Foutloggen(ex);
                return 0;
            }
        }
        public static List<Notitie> OphalenNotitiesViaEventId(int eventId)
        {     //Gemaakt door: Jarno Peeters - R0670336
            using (ArtminEntities artminEntities = new ArtminEntities())
            {
                var query = artminEntities.Notitie
                .Where(x => x.EventID == eventId); 
                return query.ToList();
            }
        }
        public static int VerwijderenNotitie(Notitie notitie)
        {    //Gemaakt door: Jarno Peeters - R0670336
            try
            {
                using (ArtminEntities artminEntities = new ArtminEntities())
                {
                    artminEntities.Entry(notitie).State = EntityState.Deleted;
                    return artminEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.Foutloggen(ex);
                return 0;
            }
        }
        public static int AanpassenNotitie(Notitie notitie)
        {   //Gemaakt door: Jarno Peeters - R0670336
            try
            {
                using (ArtminEntities artminEntities = new ArtminEntities())
                {
                    artminEntities.Entry(notitie).State = EntityState.Modified;
                    return artminEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.Foutloggen(ex);
                return 0;
            }
        }
        public static int ToevoegenNotitie(Notitie notitie)
        {   //Gemaakt door: Jarno Peeters - R0670336
            try
            {
                using (ArtminEntities artminEntities = new ArtminEntities())
                {
                    artminEntities.Notitie.Add(notitie);
                    return artminEntities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                FileOperations.Foutloggen(ex);
                return 0;
            }
        }
        public static Notitie OphalenNotitieViaId(int notitieId)
        {   //Gemaakt door: Jarno Peeters - R0670336
            using (ArtminEntities artminEntities = new ArtminEntities())
            {
                var query = artminEntities.Notitie.Where(x => x.NotitieID == notitieId);
                return query.SingleOrDefault();
            }
        }
    }
}

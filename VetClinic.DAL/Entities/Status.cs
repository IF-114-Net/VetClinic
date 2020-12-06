using System.Collections.Generic;

namespace VetClinic.DAL.Entities
{
    public class Status : IBaseEntity
    {
        public int Id { get; set; }
        public StatusName StatusName { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}

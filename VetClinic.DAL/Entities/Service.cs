﻿using System.Collections.Generic;

namespace VetClinic.DAL.Entities
{
    public class Service : IBaseEntity
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
﻿namespace VetClinic.API.DTO.ProcedureDTO
{
    public class UpdateProcedureDTO
    {
        public string ProcedureName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsSelectable { get; set;}
    }
}
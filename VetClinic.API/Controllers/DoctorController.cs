﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.API.DTO.Doctor;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;

namespace VetClinic.API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;

        public DoctorController(IDoctorService doctorService, IMapper mapper)
        {
            _doctorService = doctorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ReadDoctorDto>>> Index()
        {
            var doctors = await _doctorService.GetDoctorAsync();
            var doctorsDto = _mapper.Map<ICollection<ReadDoctorDto>>(doctors);
            return Ok(doctorsDto);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ReadDoctorDto>> Show(int id)
        {
            Doctor doctor = await _doctorService.GetDoctorAsync(id);
            ReadDoctorDto doctorDto = _mapper.Map<ReadDoctorDto>(doctor);
            return Ok(doctorDto);
        }

        [HttpPost]
        public async Task<ActionResult<ReadDoctorDto>> Create(ReadDoctorDto doctorDto)
        {
            var doctor = _mapper.Map<Doctor>(doctorDto);
            var createdDoctor = await _doctorService.AddDoctorAsync(doctor, doctor.User);
            var readDoctorDto = _mapper.Map<ReadDoctorDto>(createdDoctor);

            return CreatedAtAction(nameof(Show), new { id = doctor.Id }, readDoctorDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CreateDoctorDto>> Update(CreateDoctorDto doctorDto, int id)
        {
            var doctor = _mapper.Map<Doctor>(doctorDto);
            var successUpdate = await _doctorService.UpdateDoctorAsync(doctor, doctor.User , id);
            if (successUpdate)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Destroy(int id)
        {
            var successDelete = await _doctorService.RemoveDoctorAsync(id);
            if (successDelete)
                return NoContent();

            return NotFound();
        }
    }
}

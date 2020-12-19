﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using VetClinic.API.DTO.Error;

namespace VetClinic.API.Filters
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid) return;

            var errorsDictionary =
                context.ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage));


            var errors = new List<FieldErrorModel>();
            foreach (var (fieldName, errorList) in errorsDictionary)
            {
                var fieldErrorModel = new FieldErrorModel { FieldName = fieldName, Messages = errorList.ToArray() };

                errors.Add(fieldErrorModel);
            }

            var errorDto = new ErrorDto { Status = 400, Title = "Model is invalid", Errors = errors.ToArray() };

            context.Result = new BadRequestObjectResult(errorDto);
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
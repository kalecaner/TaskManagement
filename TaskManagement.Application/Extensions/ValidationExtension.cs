using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;

namespace TaskManagement.Application.Extensions
{
	public static class ValidationExtension
	{
		public static List<ValidationError> ToMap(this List<ValidationFailure> errors)
		{
			var errorList = new List<ValidationError>();
			foreach (var error in errors)
			{
				errorList.Add(new ValidationError(error.PropertyName, error.ErrorMessage));

			}
			return errorList;
		}
	}
}

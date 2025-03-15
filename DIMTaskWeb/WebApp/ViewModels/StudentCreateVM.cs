using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
namespace WebApp.ViewModels
{


	public class StudentCreateVM
	{
		public int? Id { get; set; }

		[Required(ErrorMessage = "Name is required.")]
		[MaxLength(30, ErrorMessage = "Name cannot exceed 30 characters.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Surname is required.")]
		[MaxLength(30, ErrorMessage = "Surname cannot exceed 30 characters.")]
		public string Surname { get; set; }

		[Required(ErrorMessage = "Father's name is required.")]
		[MaxLength(30, ErrorMessage = "Father's name cannot exceed 30 characters.")]
		public string Father { get; set; }

		[Required(ErrorMessage = "Mother's name is required.")]
		[MaxLength(30, ErrorMessage = "Mother's name cannot exceed 30 characters.")]
		public string Mother { get; set; }

		[Required(ErrorMessage = "FIN is required.")]
		[MaxLength(15, ErrorMessage = "FIN cannot exceed 15 characters.")]
		public string FIN { get; set; }

		[Required(ErrorMessage = "Address is required.")]
		[MaxLength(250, ErrorMessage = "Address cannot exceed 250 characters.")]
		public string Address { get; set; }

		[Required(ErrorMessage = "Gender is required.")]
		public byte Gender { get; set; }

		[Required(ErrorMessage = "Mobile telephone number is required.")]
		[MaxLength(15, ErrorMessage = "Mobile telephone number cannot exceed 15 characters.")]
		public string MobileTel { get; set; }

		[Required(ErrorMessage = "Photo is required.")]
		public byte[]? Foto { get; set; }

		[NotMapped]  // This ensures that the Photo property is not mapped to the database
		public IFormFile? Photo { get; set; }
	}


}

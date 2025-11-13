using System;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Domain.Entities
{
	public class AppUser : BaseEntity
	{
		public string Username { get; set; } = null!;
		public string Password { get; set; } = null!;
		public string Name { get; set; } = null!;
		public string Surname { get; set; } = null!;

        public string NormalizedUsername { get; set; }        

		public string Email { get; set; }
        public int AppRoleId { get; set; }

		#region Navigation property
		public AppRole? Role { get; set; }
		public List<AppTask>? Tasks { get; set; }

		public List<Notification>? Notifications { get; set; }

		#endregion

		public static AppUser Create (string username, string password, string name, string surname, int appRoleId, string email,IUsernameNormalizer normalizer)
		{
			if(string.IsNullOrWhiteSpace(username))
			{
				throw new ArgumentException("Username cannot be null or empty", nameof(username));
            }	
			var trimed=username.Trim();
			return new AppUser
			{
				Username = trimed,
				Password = password,
				Name = name,
				Surname = surname,
				NormalizedUsername = normalizer.Normalize(trimed),
				AppRoleId = appRoleId,
				Email = email

			};

        }

    }
}

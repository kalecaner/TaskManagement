using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Enums;

namespace TaskManagement.Application.Dtos
{
	public record LoginResponseDto(string Name,string Surname, RoleType Role);
	
	public record RegisterDto(string Username, string Password, string Name, string Surname,string Email);
	public record MemberDto(int Id, string Name, string Surname, string Username, RoleType Role);
    public record UserDto(int Id, string Name, string Surname, string Username, string Password, RoleType Role);
    public record UpdateDto(int Id,string Name,string Surname,string Username,RoleType Role,string Email);

}
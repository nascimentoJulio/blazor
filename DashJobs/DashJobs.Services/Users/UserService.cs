using Dashclass.Constants;
using Dashclass.Model;
using DashJobs.Repository.Sessions;
using DashJobs.Repository.Users;
using DashJobs.Services.Dto;
using Sodium;


namespace DashJobs.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ISessionRepository _sessionRepository;
        public UserService(IUsersRepository usersRepository, ISessionRepository sessionRepository)
        {
            _usersRepository = usersRepository;
            _sessionRepository = sessionRepository;
        }

        public async Task CreateUser(CreateUserDto createUserDto)
        {
            try
            {
                var newPassword = hashPassword(createUserDto.Password);
                var role = createUserDto.IsAdmin ? Roles.ADMINISTRATIVE : Roles.RECRUITER;

                await _usersRepository.Insert(new User
                {
                    Name = createUserDto.Username,
                    Password = newPassword,
                    Email = createUserDto.Email,
                    Roles = role
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<Guid> Login(string email, string password)
        {
            try
            {
                var user = await _usersRepository.GetUserByEmail(email);
                if (user == null)
                    throw new Exception("Usuário ou senha incorreta");

                if (!ComparePassword(user.Password, password))
                    throw new Exception("Usuário ou senha incorreta");

                await _sessionRepository.ExpiressSessions(user.Id);
                var sessionCode = Guid.NewGuid();
                await _sessionRepository.CreateSession(user.Id, sessionCode);

                return sessionCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string hashPassword(string password)
        {
            return PasswordHash.ArgonHashString(password, PasswordHash.StrengthArgon.Medium);
        }

        private bool ComparePassword(string userPassword, string password)
        {
            return PasswordHash.ArgonHashStringVerify(userPassword, password);
        }

    }
}

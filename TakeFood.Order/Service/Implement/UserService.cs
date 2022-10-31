using StoreService.Model.Entities.Role;
using StoreService.Model.Entities.User;
using StoreService.Model.Repository;
using StoreService.ViewModel.Dtos;
using StoreService.ViewModel.Dtos.User;
using MongoDB.Bson;
using BC = BCrypt.Net.BCrypt;

namespace Order.Service.Implement;

public class UserService : IUserService
{
    /// <summary>
    /// UserRepository
    /// </summary>
    private readonly IMongoRepository<User> userRepository;

    /// <summary>
    /// AccountRepository
    /// </summary>
    private readonly IMongoRepository<Account> accountRepository;

    /// <summary>
    /// RoleRepository
    /// </summary>
    private readonly IMongoRepository<Role> roleRepository;

    /// <summary>
    /// UserRefreshTokenRepository
    /// </summary>
    private readonly IMongoRepository<UserRefreshToken> userRefreshTokenRepository;

    /// <summary>
    /// User service constructor
    /// </summary>
    /// <param name="userRepository"></param>
    /// <param name="roleRepository"></param>
    public UserService(IMongoRepository<User> userRepository,
                       IMongoRepository<UserRefreshToken> userRefreshTokenRepository,
                       IMongoRepository<Role> roleRepository,
                       IMongoRepository<Account> accountRepository)
    {
        this.userRepository = userRepository;
        this.roleRepository = roleRepository;
        this.userRefreshTokenRepository = userRefreshTokenRepository;
        this.accountRepository = accountRepository;
    }

    /// <summary>
    /// Get user by user id
    /// </summary>
    /// <returns></returns>
    public async Task<UserViewDto> GetUserByIdAsync(string id)
    {
        var user = await userRepository.FindByIdAsync(id);
        var account = await accountRepository.FindOneAsync(x => x.UserId == id);

        var view = new UserViewDto()
        {
            Name = user.Name,
            Email = account.Email,
            Photo = user.Avatar,
            Id = user.Id,
            Phone = user.PhoneNumber

        };
        var role = new List<String>();
        var listRole = await roleRepository.GetAllAsync();
        foreach (var i in listRole)
        {
            if (user.RoleIds.Contains(i.Id))
            {
                role.Add(i.Name!);
            }
        }
        view.Roles = role;
        return view;
    }

    /// <summary>
    /// Create new user
    /// </summary>
    /// <returns></returns>
    public async Task<UserViewDto> CreateUserAsync(CreateUserDto createUserDto)
    {
        var users = await userRepository.FindAsync(x => x.PhoneNumber == createUserDto.PhoneNumber);
        if (users.Count != 0)
        {
            return null;
        }
        var accounts = await accountRepository.FindAsync(x => x.Email == createUserDto.Email);
        if (accounts.Count != 0)
        {
            return null;
        }

        var user = new User()
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = createUserDto.Name,
            PhoneNumber = createUserDto.PhoneNumber,
            Avatar = createUserDto.Avatar,
            RoleIds = new List<String>() { "2" },
            State = "Active",
            UpdatedDate = DateTime.Now,
            CreatedDate = DateTime.Now,
        };
        var rs = await userRepository.InsertAsync(user);

        var account = new Account()
        {
            Password = BC.HashPassword(createUserDto.Password),
            Email = createUserDto.Email,
            UserId = rs.Id
        };

        var rsAcount = await accountRepository.InsertAsync(account);

        return await GetUserByIdAsync(rs.Id);
    }

    /// <summary>
    /// Sign in by phone number or email
    /// </summary>
    /// <returns></returns>
    public async Task<UserViewDto> SignIn(LoginDto loginDto)
    {
        var account = await accountRepository.FindOneAsync(x => x.Email.Equals(loginDto.UserName));
        if (account == null)
        {
            return null;
        }

        var user = await userRepository.FindByIdAsync(account.UserId);
        if (BC.Verify(loginDto.Password, account.Password) && user.State == "Active")
        {
            return await GetUserByIdAsync(user.Id);
        }
        else
        {
            return null;
        }
    }

    public Task Active(string token)
    {
        return null;
    }
}

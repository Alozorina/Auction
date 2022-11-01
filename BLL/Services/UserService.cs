using AutoMapper;
using BLL;
using BLL.Extensions;
using BLL.Models;
using BLL.Validation;
using DAL.Entities;
using DAL.Entities.Configuration;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HashHandler = BCrypt.Net.BCrypt;

namespace Auction.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all users from the database and maps them to <UserPulicInfo>
        /// </summary>
        /// <returns>
        /// Async Task. Task result contains List<UserPulicInfo> that contains elements from the input sequence
        /// </returns>
        public async Task<IEnumerable<UserPulicInfo>> GetPublicInfoAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllWithDetailsAsync();
            return users.Select(u => _mapper.Map<UserPulicInfo>(u));
        }

        /// <summary>
        /// Gets info about User with all details
        /// </summary>
        /// <remarks> 
        /// Navigation properties included
        /// </remarks>
        /// <returns>
        /// Async Task. Task result contains the single User with the given ID, or throws an Argument Exception if User is null
        /// </returns>
        public async Task<User> GetUserWithDetailsByIdAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdWithDetailsAsync(id);
            UserValidation.ThrowArgumentExceptionIfUserIsNull(user);

            return user;
        }

        /// <summary>
        /// Gets info about user without details
        /// </summary>
        /// <remarks> 
        /// Navigation properties will be null
        /// </remarks>
        /// <returns>
        /// Async Task. Task result contains the single user with the given ID, or throws an Argument Exception if user is null
        /// </returns>
        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            UserValidation.ThrowArgumentExceptionIfUserIsNull(user);

            return user;
        }
        public UserPersonalInfoModel MapToUserPersonalInfoFromUser(User user) => _mapper.Map<User, UserPersonalInfoModel>(user);

        /// <summary>
        /// Gets info about User if given email and password are correct. 
        /// Throws an Argument Exception if User with given email wasn't found. 
        /// </summary>
        /// <remarks> 
        /// Role navigation property included.
        /// </remarks>
        /// <returns>
        /// Async Task. Task result contains the single User with the given email and password, 
        /// or null, if given password is not correct.
        /// </returns>
        public async Task<User> LoginAsync(string email, string password)
        {
            var user = await _unitOfWork.UserRepository.GetUserWithRoleAsync(u => u.Email == email);
            UserValidation.ThrowArgumentExceptionIfUserIsNull(user);

            bool isPasswordCorrect = HashHandler.Verify(password, user.Password);
            return isPasswordCorrect ? user : null;
        }

        /// <summary>
        /// Adds new User to database. User's Role by default is "User".
        /// </summary>
        /// <returns>
        /// Async Task. Task result contains User that was added to database
        /// </returns>
        public async Task<User> AddAsync(UserRegistrationModel model)
        {
            var users = _unitOfWork.UserRepository.GetAll();
            if (UserValidation.IsEmailExists(users, model.Email))
                throw new AuctionException("This email is being used by another user");

            Role role = await _unitOfWork.RoleRepository
                .FirstOrDefaultAsync(r => r.Id == (int)UserRoles.User);
            var user = _mapper.Map<User>(model);
            user.RoleId = (int)UserRoles.User;
            user.Role = role;
            user.Password = HashHandler.HashPassword(user.Password);
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();

            return user;
        }

        /// <summary>
        /// Updates user's info with given password. If user is null, throws an exception.
        /// If current user's password in database is not equal to "OldPassword" from the UserPassword model,
        /// throws an Auction Exception.
        /// </summary>
        public async Task UpdatePassword(User user, UserPassword userPassword)
        {
            UserValidation.ThrowArgumentExceptionIfUserIsNull(user, "User wasn't found");

            if (user != null && HashHandler.Verify(userPassword.OldPassword, user.Password))
                user.Password = HashHandler.HashPassword(userPassword.NewPassword);
            else
                throw new AuctionException("Wrong password");

            await _unitOfWork.SaveAsync();
        }

        /// <summary>
        /// Updates user's info with given info from <UserPersonalInfoModel>. If user is null, throws an exception.
        /// Throws an Auction Exception if email from given info already exists in database
        /// </summary>
        public async Task UpdatePersonalInfo(int id, UserPersonalInfoModel updateModel)
        {
            var user = await GetUserByIdAsync(id);
            var users = _unitOfWork.UserRepository.GetAll();

            UserValidation.IsEmailCouldBeUpdated(users, user.Email, updateModel.Email);

            var update = _mapper.Map(updateModel, user);
            update.BirthDate = DataConverter.ConvertDateFromClient(updateModel.BirthDate);

            await _unitOfWork.SaveAsync();
        }

        /// <summary>
        /// Updates user's Role by given Role Id. If user is null, throws an exception.
        /// Throws an Auction Exception if role wasn't found.
        /// </summary>
        public async Task UpdateRoleAsync(int userId, int roleId)
        {
            var role = await _unitOfWork.RoleRepository.GetByIdAsync(roleId);
            if (role == null)
                throw new AuctionException("Wrong role id");

            var existingUser = await _unitOfWork.UserRepository.GetUserWithRoleAsync(u => u.Id == userId);
            UserValidation.ThrowArgumentExceptionIfUserIsNull(existingUser);

            existingUser.RoleId = role.Id;
            existingUser.Role = role;

            await _unitOfWork.SaveAsync();
        }

        /// <summary>
        /// Updates user with id from given User model, If user is null, throws an exception.
        /// Throws an Auction Exception if email from given info already exists in database or model has null property
        /// </summary>
        public async Task UpdateAsync(User model)
        {
            var existingUser = await GetUserByIdAsync(model.Id);
            var users = _unitOfWork.UserRepository.GetAll();

            UserValidation.IsEmailCouldBeUpdated(users, existingUser.Email, model.Email);
            UserValidation.IsModelHasNullProperty(model);

            existingUser.FirstName = model.FirstName;
            existingUser.LastName = model.LastName;
            existingUser.Password = HashHandler.HashPassword(model.Password);
            existingUser.BirthDate = model.BirthDate;
            existingUser.Email = model.Email;
            existingUser.RoleId = model.RoleId;
        }

        /// <summary>
        /// Removes user from the database, 
        /// If user wasn't found, throws an exception.
        /// </summary>
        public async Task DeleteByIdAsync(int id)
        {
            var user = await GetUserWithDetailsByIdAsync(id);
            _unitOfWork.UserRepository.Delete(user);
            await _unitOfWork.SaveAsync();
        }
    }
}

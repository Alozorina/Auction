using AutoMapper;
using BLL;
using BLL.Extensions;
using BLL.Models;
using BLL.Validation;
using DAL.Entities;
using DAL.Entities.Configuration;
using DAL.Interfaces;
using System.Collections.Generic;
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

        public async Task<IEnumerable<UserPulicInfo>> GetPublicInfoAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllWithDetailsAsync();
            List<UserPulicInfo> usersPulicInfo = new List<UserPulicInfo>();
            foreach (var user in users)
            {
                usersPulicInfo.Add(_mapper.Map<UserPulicInfo>(user));
            }
            return usersPulicInfo;
        }

        public async Task<User> GetUserWithDetailsByIdAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdWithDetailsAsync(id);
            UserValidation.ThrowArgumentExceptionIfUserIsNull(user);

            return user;
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            UserValidation.ThrowArgumentExceptionIfUserIsNull(user);

            return user;
        }
        public UserPersonalInfoModel MapToUserPersonalInfoFromUser(User user) => _mapper.Map<User, UserPersonalInfoModel>(user);
        public async Task<User> LoginAsync(string email, string password)
        {
            var user = await _unitOfWork.UserRepository.GetUserWithRoleAsync(u => u.Email == email);
            UserValidation.ThrowArgumentExceptionIfUserIsNull(user);

            bool isPasswordCorrect = HashHandler.Verify(password, user.Password);
            return isPasswordCorrect ? user : null;
        }

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
        public async Task UpdatePassword(User user, UserPassword userPassword)
        {
            if (user != null && HashHandler.Verify(userPassword.OldPassword, user.Password))
                user.Password = HashHandler.HashPassword(userPassword.NewPassword);
            else
                throw new AuctionException("Wrong password");

            await _unitOfWork.SaveAsync();
        }

        public async Task UpdatePersonalInfo(int id, UserPersonalInfoModel updateModel)
        {
            var user = await GetUserByIdAsync(id);
            var users = _unitOfWork.UserRepository.GetAll();

            UserValidation.IsEmailCouldBeUpdated(users, user.Email, updateModel.Email);

            var update = _mapper.Map(updateModel, user);
            update.BirthDate = DataConverter.ConvertDateFromClient(updateModel.BirthDate);

            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateRoleAsync(int userId, int roleId)
        {
            var role = await _unitOfWork.RoleRepository.GetByIdAsync(roleId);
            var existingUser = await _unitOfWork.UserRepository.GetUserWithRoleAsync(x => x.Id == userId);

            UserValidation.ThrowArgumentExceptionIfUserIsNull(existingUser);

            existingUser.RoleId = role.Id;
            existingUser.Role = role;

            await _unitOfWork.SaveAsync();
        }

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
        /// Removes an entity from the database if that entity Id exists in the database
        /// </summary>
        /// <returns>
        /// Remove operation from EF Core for the entity with given Id 
        /// </returns>
        public async Task DeleteByIdAsync(int id)
        {
            var user = await GetUserWithDetailsByIdAsync(id);
            UserValidation.ThrowArgumentExceptionIfUserIsNull(user);

            _unitOfWork.UserRepository.Delete(user);
            await _unitOfWork.SaveAsync();
        }
    }
}

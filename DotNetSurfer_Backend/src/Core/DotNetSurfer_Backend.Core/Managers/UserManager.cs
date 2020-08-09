using DotNetSurfer_Backend.Core.Models;
using DotNetSurfer_Backend.Core.Interfaces.Managers;
using DotNetSurfer.Core.TokenGenerators;
using DotNetSurfer_Backend.Core.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using DotNetSurfer_Backend.Core.Interfaces.Encryptors;
using DotNetSurfer_Backend.Core.Exceptions;
using DotNetSurfer_Backend.Core.Interfaces.Caches;

namespace DotNetSurfer_Backend.Core.Managers
{
    public class UserManager : BaseManager<UserManager>, IUserManager
    {
        private readonly IEncryptor _encryptor;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IConfiguration _configuration;

        public UserManager(
            IEncryptor encryptor, 
            ITokenGenerator tokenGenerator,
            IConfiguration configuration,
            IUnitOfWork unitOfWork,
            ICacheDataProvider cacheDataProvider,
            ILogger<UserManager> logger
            ) : base(unitOfWork, cacheDataProvider, logger)
        {
            this._encryptor = encryptor;
            this._tokenGenerator = tokenGenerator;
            this._configuration = configuration;
        }

        public async Task SignUp(User model)
        {
            try
            {
                bool isUserEmailExist = await this._unitOfWork.UserRepository.IsEmailExistAsync(model.Email);
                if (isUserEmailExist)
                {
                    throw new BaseCustomException("User email already exists");
                }

                string encryptedPassword = _encryptor.Encrypt(model.Password);
                var entityModel = new User()
                {
                    Email = model.Email,
                    Name = model.Name,
                    Password = encryptedPassword,
                    Birthdate = model.Birthdate,
                    Picture = model.Picture,
                    PictureMimeType = model.PictureMimeType,
                    PermissionId = (int)PermissionType.User // default
                };

                this._unitOfWork.UserRepository.Create(entityModel);
                await this._unitOfWork.UserRepository.SaveAsync();
            }
            catch (BaseCustomException ex)
            {
                this._logger.LogInformation(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
                throw new BaseCustomException();
            } 
        }

        public async Task<SignIn> SignIn(User model)
        {
            SignIn signIn = null;

            try
            {
                var user = await this._unitOfWork.UserRepository.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    throw new CustomNotFoundException("Please check that your email addresses");
                }

                if (!this._encryptor.IsEqual(model.Password, user.Password))
                {
                    throw new BaseCustomException("Wrong type of the password, please enter it again");
                }

                user.Password = null; // Clear password to be secure
                var jwt = this._tokenGenerator.GetToken(user);
                var domainUser = user;
                signIn = new SignIn
                {
                    Auth_Token = jwt,
                    User = domainUser
                };
            }
            catch (BaseCustomException ex)
            {
                this._logger.LogInformation(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
                throw new BaseCustomException();
            }

            return signIn;
        }
    }
}

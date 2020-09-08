namespace ApiRest
{
    using AutoMapper;
    using Models;
    using Repository.Entities;

    /// <summary>
    /// Class to manage the profile for automapper.
    /// </summary>
    public class AutoMapperModelProfile : Profile
    {
        /// <summary>
        /// Creates a new instance of <see cref="AutoMapperModelProfile"/>.
        /// </summary>
        public AutoMapperModelProfile()
        {
            CreateMap<User, UserModel>()
                .ReverseMap();

            CreateMap<User, CreateUserModel>()
                .ReverseMap();

            CreateMap<Role, RoleModel>()
                .ReverseMap();

            CreateMap<BalanceSheet, BalanceSheetModel>()
                .ReverseMap();
        }
    }
}
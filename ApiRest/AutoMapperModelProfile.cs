namespace ApiRest
{
    using AutoMapper;
    using Models;
    using Repository.Entities;

    public class AutoMapperModelProfile : Profile
    {
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
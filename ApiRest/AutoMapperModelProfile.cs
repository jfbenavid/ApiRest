namespace ApiRest
{
    using AutoMapper;
    using Models;
    using Repository.Entities;

    public class AutoMapperModelProfile : Profile
    {
        public AutoMapperModelProfile()
        {
            CreateMap<AuthUser, AuthModel>()
                .ReverseMap();

            CreateMap<AuthUser, CreateAuthModel>()
                .ReverseMap();

            CreateMap<Account, AccountModel>()
                .ReverseMap();

            CreateMap<Role, RoleModel>()
                .ReverseMap();

            CreateMap<User, UserModel>()
                .ReverseMap();

            CreateMap<BalanceSheet, BalanceSheetModel>()
                .ReverseMap();
        }
    }
}
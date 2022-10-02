using AutoMapper;
using PostgreSQL.Demo.API.Entities;
using PostgreSQL.Demo.API.Models.Authors;
using PostgreSQL.Demo.API.Models.Books;

namespace PostgreSQL.Demo.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // CreateAuthorRequest => Author
            CreateMap<CreateAuthorRequest, Author>();

            // UpdateAuthorRequest => Author
            CreateMap<UpdateAuthorRequest, Author>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // let's ignore null and empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        // Ignore null gender
                        if (x.DestinationMember.Name == "Gender" && src.Gender == null) return false;

                        return true;
                    }
                  ));

            // CreateBookRequest => Book
            CreateMap<CreateBookRequest, Book>();

            // UpdateBookRequest => Book
            CreateMap<UpdateBookRequest, Book>();
        }
    }
}

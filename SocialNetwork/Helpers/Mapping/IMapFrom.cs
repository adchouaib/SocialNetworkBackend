using AutoMapper;

namespace SocialNetwork.Helpers.Mapping
{
    public interface IMapFrom<T>
    {
        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(T), GetType());
        }
    }
}

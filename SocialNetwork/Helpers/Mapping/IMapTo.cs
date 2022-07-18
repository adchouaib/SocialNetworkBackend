using AutoMapper;

namespace SocialNetwork.Helpers.Mapping
{
    public interface IMapTo<T>
    {
        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(T), GetType());
        }
    }
}

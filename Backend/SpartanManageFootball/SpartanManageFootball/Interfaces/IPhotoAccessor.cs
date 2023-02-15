using SpartanManageFootball.Application.Teams;

namespace SpartanManageFootball.Interfaces
{
    public interface IPhotoAccessor
    {
        Task<PhotoResult> AddPhoto(IFormFile file);
        Task<string> DeletePhoto(string PublicNum);
    }
}

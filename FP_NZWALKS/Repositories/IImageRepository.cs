using FP_NZWALKS.Models.Domain;

namespace FP_NZWALKS.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}

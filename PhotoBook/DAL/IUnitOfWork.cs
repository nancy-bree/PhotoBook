using System;
namespace PhotoBook.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        AlbumRepository AlbumRepository { get; }

        PhotoRepository PhotoRepository { get; }

        RatingRepository RatingRepository { get; }

        TagRepository TagRepository { get; }

        void Save();
    }
}

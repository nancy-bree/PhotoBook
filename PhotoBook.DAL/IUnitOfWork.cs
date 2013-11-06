using System;
namespace PhotoBook.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        PhotoRepository PhotoRepository { get; }

        RatingRepository RatingRepository { get; }

        TagRepository TagRepository { get; }

        UserRepository UserRepository { get; }

        void Save();
    }
}

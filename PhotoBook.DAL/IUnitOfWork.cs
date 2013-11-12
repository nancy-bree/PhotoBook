using System;
using PhotoBook.Entities;

namespace PhotoBook.DAL
{
    /// <summary>
    /// Defines interface of Unit of Work operations.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        PhotoRepository PhotoRepository { get; }

        RatingRepository RatingRepository { get; }

        TagRepository TagRepository { get; }

        UserRepository UserRepository { get; }

        void Save();
    }
}

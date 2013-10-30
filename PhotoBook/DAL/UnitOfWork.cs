using System;
using PhotoBook.Models;

namespace PhotoBook.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Fields
        private PhotoBookContext _context = new PhotoBookContext();

        private AlbumRepository _albumRepository;

        private PhotoRepository _photoRepository;

        private RatingRepository _ratingRepository;

        private TagRepository _tagRepository;
        #endregion

        #region Properties
        public AlbumRepository AlbumRepository
        {
            get
            {
                if (_albumRepository == null)
                {
                    _albumRepository = new AlbumRepository(_context);
                }
                return _albumRepository;
            }
        }

        public PhotoRepository PhotoRepository
        {
            get
            {
                if (_photoRepository == null)
                {
                    _photoRepository = new PhotoRepository(_context);
                }
                return _photoRepository;
            }
        }

        public RatingRepository RatingRepository
        {
            get
            {
                if (_ratingRepository == null)
                {
                    _ratingRepository = new RatingRepository(_context);
                }
                return _ratingRepository;
            }
        }

        public TagRepository TagRepository
        {
            get
            {
                if (_tagRepository == null)
                {
                    _tagRepository = new TagRepository(_context);
                }
                return _tagRepository;
            }
        }
        #endregion

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

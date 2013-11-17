using System;

namespace PhotoBook.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Fields
        private readonly PhotoBookContext _context = new PhotoBookContext();

        private PhotoRepository _photoRepository;

        private RatingRepository _ratingRepository;

        private TagRepository _tagRepository;

        private UserRepository _userRepository;
        #endregion

        #region Properties
        public PhotoRepository PhotoRepository
        {
            get { return _photoRepository ?? (_photoRepository = new PhotoRepository(_context)); }
        }

        public RatingRepository RatingRepository
        {
            get { return _ratingRepository ?? (_ratingRepository = new RatingRepository(_context)); }
        }

        public TagRepository TagRepository
        {
            get { return _tagRepository ?? (_tagRepository = new TagRepository(_context)); }
        }

        public UserRepository UserRepository
        {
            get { return _userRepository ?? (_userRepository = new UserRepository(_context)); }
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

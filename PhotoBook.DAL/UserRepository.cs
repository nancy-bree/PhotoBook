using PhotoBook.Entities;

namespace PhotoBook.DAL
{
    /// <summary>
    /// Defines data access operations for User entity.
    /// </summary>
    public class UserRepository : Repository<User>
    {
        public UserRepository(PhotoBookContext context) : base(context) { }
    }
}
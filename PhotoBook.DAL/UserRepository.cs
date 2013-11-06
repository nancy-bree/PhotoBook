using PhotoBook.Entities;

namespace PhotoBook.DAL
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(PhotoBookContext context) : base(context) { }
    }
}
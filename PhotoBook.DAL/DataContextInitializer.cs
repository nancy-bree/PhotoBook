using PhotoBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace PhotoBook.DAL
{
    public class DataContextInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<PhotoBookContext>
    {
        protected override void Seed(PhotoBookContext context)
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection",
               "User", "UserID", "UserName", autoCreateTables: true);
            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            if (!roles.RoleExists("User"))
            {
                roles.CreateRole("User");
            }
            if (membership.GetUser("test", false) == null)
            {
                membership.CreateUserAndAccount("test", "test");
            }
            if (!roles.GetRolesForUser("test").Contains("User"))
            {
                roles.AddUsersToRoles(new[] { "test" }, new[] { "user" });
            }

            var photo = new Photo
            {
                Filename = "b1bb1e7e-2a89-401d-be3b-0612df061e44.jpg",
                UserID = membership.GetUserId("test"),
                Description = "just a photo"
            };
            context.Photo.Add(photo);
            context.SaveChanges();

            var rating = new Rating
            {
                UserID = membership.GetUserId("test"),
                PhotoID = photo.ID,
                Vote = 1
            };
            context.Rating.Add(rating);
            context.SaveChanges();

            var tag = new Tag
            {
                Name = "nature"
            };
            context.Tag.Add(tag);
            context.SaveChanges();
        }
    }
}
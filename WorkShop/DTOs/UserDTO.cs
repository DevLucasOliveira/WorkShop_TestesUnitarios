using WorkShop.Entities;

namespace WorkShop.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }


        public static implicit operator UserDTO(User entity)
        {
            return new UserDTO
            {
                Id = entity.Id.ToString(),
                Name = entity?.Name,
                Email = entity?.Email,
            };
        }

    }
}

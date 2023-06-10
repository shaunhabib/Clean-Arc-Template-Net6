using System.Collections.Generic;

namespace Core.Domain.Shared.Contacts
{
    public interface IAuthenticatedUser
    {
        string UserId { get; }
        string UserName { get; }
        List<string> Roles { get; }
    }
}

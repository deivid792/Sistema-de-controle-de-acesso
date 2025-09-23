using System;
using System.Collections.Generic;

namespace VisitorService.Domain.Entities
{
    public sealed class ValidationToken
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Token { get; private set; } = default!;
        public DateTime Expiration { get; private set; }

        public User User { get; private set; } = default!;

        private ValidationToken() { }

        public ValidationToken(User user, string token, DateTime expiration)
        {
            Id = Guid.NewGuid();
            User = user;
            UserId = user.Id;
            Token = token;
            Expiration = expiration;
        }
    }
}

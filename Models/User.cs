using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] PasswordSalt { get; set; } = new byte[0];

        public List<TinyUrl>? Urls { get; set;}
        public List<QandA>? QandAs { get; set; }

        public List<Bill>? Bills {get; set;}

        public SubscriptionTypeClass subscriptionType {get; set; } = SubscriptionTypeClass.Normal;

    }
}
using System;

namespace SocialMegazord2._0.Models
{
    internal class ForeigntKeyAttribute : Attribute
    {
        private string v;

        public ForeigntKeyAttribute(string v)
        {
            this.v = v;
        }
    }
}
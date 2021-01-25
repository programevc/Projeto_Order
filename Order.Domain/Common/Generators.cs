using System;

namespace Order.Domain.Common
{
    public class Generators : IGenerators
    {
        public string Generate() => Guid.NewGuid().ToString("N");
    }
}

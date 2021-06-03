using System;

namespace Contracts.Domain.Base
{
    public interface IDomainAppUserId : IDomainAppUserId<int>
    {
    }

    public interface IDomainAppUserId<TKey>
        where TKey : IEquatable<TKey>
    {
        TKey AppUserId { get; set; }
    }
}
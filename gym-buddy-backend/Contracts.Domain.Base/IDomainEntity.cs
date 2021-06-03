using System;

namespace Contracts.Domain.Base
{
    public interface IDomainEntity : IDomainEntity<int>
    {
    }

    public interface IDomainEntity<TKey> : IDomainEntityId<TKey>, IDomainEntityMeta
        where TKey : IEquatable<TKey>
    {
    }
}
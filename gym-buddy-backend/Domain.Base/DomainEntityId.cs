﻿using System;
using Contracts.Domain.Base;

namespace Domain.Base
{
    public abstract class DomainEntityId : DomainEntityId<int>, IDomainEntityId
    {
    }

    public abstract class DomainEntityId<TKey> : IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; } = default!;
    }
}
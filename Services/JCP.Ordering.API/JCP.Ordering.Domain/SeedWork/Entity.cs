﻿using System;

namespace JCP.Ordering.Domain.SeedWork
{
    public abstract class Entity
    {
        Guid _Id;
        public virtual Guid Id
        {
            get
            {
                return _Id;
            }
            protected set
            {
                _Id = value;
            }
        }
    }
}

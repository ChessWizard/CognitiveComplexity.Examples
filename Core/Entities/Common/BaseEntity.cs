﻿using Core.Entities.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Common
{
    public class BaseEntity<T> : IEntity<T>
    {
        public T Id { get; set; }
    }
}

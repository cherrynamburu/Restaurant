﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.ViewModel
{
    public class ModelStateTransferValue
    {
        public string Key { get; set; }
        public string AttemptedValue { get; set; }
        public object RawValue { get; set; }
        public ICollection<string> ErrorMessages { get; set; } = new List<string>();
    }
}

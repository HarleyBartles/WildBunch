using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WildBunch.Models
{
    public class UiResponse
    {
        public bool Success { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
        public object Result { get; set; }
    }
}

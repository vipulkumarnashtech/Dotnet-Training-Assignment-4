using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rpgAPI.Model
{
    public class ServiceResponse<T>
    {

        public T? Data { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;
        
    }
}
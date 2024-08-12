using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Response
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public String? Message { get; set; }
        public Object? Data { get; set; }
    }
}

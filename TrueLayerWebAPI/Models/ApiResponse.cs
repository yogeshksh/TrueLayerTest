using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrueLayerWebAPI.Models
{
    public class ApiResponse
    {
        public bool Status { get; set; }
        public Post Post { get; set; }
        public ModelStateDictionary ModelState { get; set; }
    }
}

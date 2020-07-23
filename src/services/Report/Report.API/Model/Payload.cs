using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Report.API.Model
{

    public partial class Payload
    {
        [Key]
        public int Id { get; set; }
        public string Image { get; set; }

        public string Link { get; set; }
    }

}

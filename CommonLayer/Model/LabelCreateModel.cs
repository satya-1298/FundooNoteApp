using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class LabelCreateModel
    {
        [Required]
        public string LabelName { get; set; }
    }
}

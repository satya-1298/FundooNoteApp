using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class NoteCreateModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Reminder { get; set; }
        public string BackGround { get; set; }
        public string Image { get; set; }
        public bool IsArchive { get; set; }
        public bool IsPin { get; set; }
        public bool IsTrash { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepoLayer.Entity
{
    public class CollabratorEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollabId { get; set; }
        public string Email { get; set; }
       
        [ForeignKey("Notes")]
        public long NoteId { get; set; }
        [JsonIgnore]
        public NotesEntity Notes { get; set; }
       
        [ForeignKey("User")]
        public long UserId { get; set; }
        [JsonIgnore]
        public UserEntity User { get; set; }
    }
}

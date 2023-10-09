using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Interface
{
    public interface INoteRepo
    {
        public NotesEntity CreateNote(NoteCreateModel model,long UserId);
        public NotesEntity UpdateNote(NoteCreateModel noteUpdate, long NoteID,long userId);
        public List<NotesEntity> GetAllNotes(long UserId);
        public NotesEntity DeleteNote(long noteID, long userId);
        public NotesEntity IsArchiev(long noteID);
        public NotesEntity IsPin(long noteID) ;
        public NotesEntity IsTrash(long noteID);
        public string UploadImage(string image,long userId,long noteID);
        public List<NotesEntity> SearchQuery(long userId,string word);
        public Task<Tuple<int, string>> Image(long id, long usedId, IFormFile imageFile);
        public  NotesEntity CopyNote(long userId,long noteID);
    }
}

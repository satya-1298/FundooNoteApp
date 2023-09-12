using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepoLayer.Entity;
using RepoLayer.Interface;
using RepoLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class NoteBusiness:INoteBusiness
    {
        private readonly INoteRepo noteRepo;
        public NoteBusiness(INoteRepo noteRepo)
        {
            this.noteRepo = noteRepo;
        }
        public NotesEntity CreateNote(NoteCreateModel model, long UserId)
        {
            try
            {
                return noteRepo.CreateNote(model, UserId);

            }

            catch (Exception ex)
            {
                throw;
            }

        }
        public NotesEntity UpdateNote(NoteCreateModel noteUpdate, long NoteID, long userId)
        {
            try
            {
                return noteRepo.UpdateNote(noteUpdate, NoteID,userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<NotesEntity> GetAllNotes(long UserId)
        {
            try
            {
                return noteRepo.GetAllNotes(UserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
      
        public bool DeleteNote(long noteID, long userId)
        {
            try
            {
                return noteRepo.DeleteNote(noteID, userId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool IsArchiev(long noteID)
        {
            try
            {
                return noteRepo.IsArchiev(noteID);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool IsPin(long noteID)
        {
            try
            {
                return noteRepo.IsPin(noteID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool IsTrash(long noteID)
        {
            try
            {
                return noteRepo.IsTrash(noteID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string UploadImage(string image, long userId, long noteID)
        {
            try
            {
                return noteRepo.UploadImage(image, userId, noteID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<NotesEntity> SearchQuery(long userId, string word)
        {
            try
            {
                return noteRepo.SearchQuery(userId, word);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Tuple<int, string>> Image(long id, long usedId, IFormFile imageFile)
        {
            try
            {
                return await noteRepo.Image(id, usedId, imageFile);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public NotesEntity CopyNote(long userId, long noteId)
        {
            try
            {
                return noteRepo.CopyNote(userId, noteId);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    }

}

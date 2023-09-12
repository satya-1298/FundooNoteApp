using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace RepoLayer.Service
{
    public class NoteRepo:INoteRepo
    {
        private readonly FundooContext _fundooContext;
        private readonly FileService fileService;
        private readonly Cloudinary cloudinary;

        public NoteRepo(FundooContext fundooContext, FileService fileService, Cloudinary cloudinary)
        {
           this. _fundooContext = fundooContext;
            this.fileService = fileService;
            this.cloudinary = cloudinary;

        }
        public NotesEntity CreateNote(NoteCreateModel model, long UserId)
        {
            NotesEntity notes = new NotesEntity();  
            notes.UserId = UserId;
            notes.Title = model.Title;
            notes.Description = model.Description;
            notes.Reminder = DateTime.Now;
            notes.BackGround = model.BackGround;
            notes.Image = model.Image;
            notes.IsArchive = model.IsArchive;
            notes.IsPin = model.IsPin;
            notes.IsTrash = model.IsTrash;
            _fundooContext.Notes.Add(notes);
            _fundooContext.SaveChanges();
            if(notes!=null)
            {
                return notes;
            }
            return null;
        }
        public NotesEntity UpdateNote(NoteCreateModel noteUpdate, long NoteID, long userId  )
        {
            try
            {
                var result = _fundooContext.Notes.FirstOrDefault(x => x.NoteID == NoteID);
                if (result != null)
                {
                    result.Title = noteUpdate.Title;
                    result.Description = noteUpdate.Description;
                    result.Reminder = DateTime.Now;
                    result.BackGround = noteUpdate.BackGround;
                    result.Image = noteUpdate.Image;
                    result.IsArchive = noteUpdate.IsArchive;
                    result.IsPin = noteUpdate.IsPin;
                    result.IsTrash = noteUpdate.IsTrash;
                    _fundooContext.SaveChanges();
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NotesEntity> GetAllNotes(long UserId)
        {
            var result = _fundooContext.Notes.Where(x => x.UserId == UserId).ToList();
            if (result.Count != 0)
            {
                return result;
            }
            return null;
        }
        public bool DeleteNote(long noteID, long userId)
        {
            try
            {
                var noteDelete = _fundooContext.Notes.Where(x => x.NoteID == noteID).FirstOrDefault();
                
                if (noteDelete != null)
                {
                    _fundooContext.Notes.Remove(noteDelete);
                    _fundooContext.SaveChanges();
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool IsArchiev(long noteID)
        {
            var result = _fundooContext.Notes.Where(x => x.NoteID == noteID).FirstOrDefault();
            if (result != null)
            {
                result.IsArchive = !result.IsArchive;
                _fundooContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsPin(long noteID)
        {
            var result = _fundooContext.Notes.Where(x => x.NoteID == noteID).FirstOrDefault();
            if (result != null)
            {
                result.IsPin = !result.IsPin;
                _fundooContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsTrash(long noteID)
        {
            var result=_fundooContext.Notes.Where(x=>x.NoteID==noteID).FirstOrDefault();
            if (result != null)
            {
                result.IsTrash= !result.IsTrash;
                _fundooContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public string UploadImage(string image, long userId, long noteID)
        {
            var result = _fundooContext.Notes.Where(x => x.NoteID == noteID).FirstOrDefault();
            if (result != null)
            {
                result.Image = image;
                _fundooContext.SaveChanges();
                return result.Image;
            }
            else
            {
                return image;
            }
        }
        public List<NotesEntity> SearchQuery(long userId, string word)
        {
            try
            {
                var result = _fundooContext.Notes.Where(x => x.UserId == userId && x.Title.Contains(word)||x.Description.Contains(word)).ToList();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // IMAGE  UPLOAD ON CLOUDINARY:-
        public async Task<Tuple<int, string>> Image(long id, long usedId, IFormFile imageFile)
        {
            var result = _fundooContext.Notes.FirstOrDefault(x => x.NoteID == id && x.UserId == usedId);
            if (result != null)
            {
                try
                {
                    var data = await fileService.SaveImage(imageFile);
                    if (data.Item1 == 0)
                    {
                        return new Tuple<int, string>(0, data.Item2);
                    }

                    var UploadImage = new ImageUploadParams
                    {
                        File = new CloudinaryDotNet.FileDescription(imageFile.FileName, imageFile.OpenReadStream())
                    };

                    ImageUploadResult uploadResult = await cloudinary.UploadAsync(UploadImage);
                    string imageUrl = uploadResult.SecureUrl.AbsoluteUri;
                    result.Image = imageUrl;

                    _fundooContext.Notes.Update(result);
                    _fundooContext.SaveChanges();

                    return new Tuple<int, string>(1, "Image Uploaded Successfully");
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return null;
        }
        public NotesEntity CopyNote(long userId, long noteId)
        {
            try
            {
                var copy = _fundooContext.Notes.FirstOrDefault(x => x.UserId == userId && x.NoteID == noteId);
                if (copy != null)
                {
                    NotesEntity notes = new NotesEntity();
                    notes.UserId = userId;
                    notes.Title = copy.Title;
                    notes.Description = copy.Description;
                    notes.Reminder = copy.Reminder;
                    notes.BackGround = copy.BackGround;
                    notes.Image=copy.Image;
                    notes.IsArchive = copy.IsArchive;
                    notes.IsPin = copy.IsPin;
                    notes.IsTrash = copy.IsTrash;
                    _fundooContext.Notes.Add(notes);
                    _fundooContext.SaveChanges();
                    return notes;
                }
                else
                {
                    return null;
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}

using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelBusiness:ILabelBusiness
    {
        private readonly ILabelRepo _labelRepo;
        public LabelBusiness(ILabelRepo labelRepo)
        {
            this._labelRepo = labelRepo;
        }
        public LabelEntity CreateLabel(LabelCreateModel model, long? NoteID, long UserId)
        {
            try
            {
                return _labelRepo.CreateLabel(model, NoteID, UserId);
            }
            catch
            {
                return null;
            }

        }
        public LabelEntity AddNote(long noteId, long userId, long labelId)
        {
            try
            {
                return _labelRepo.AddNote(noteId, userId,labelId);
            }
            catch
            {
                throw ;
            }
        }
        public List<LabelEntity> UpdateLabel(string newName, long UserId, string labelName)
        {
            try
            {
                return _labelRepo.UpdateLabel(newName, UserId,labelName);
            }
            catch
            {
                return null;
            }
        }
        public List<LabelEntity> DeleteLabel(string labelName, long userId)
        {
            try
            {
                return _labelRepo.DeleteLabel(labelName, userId);
            }
            catch 
            {
                return null;
            }
        }
        public List<LabelEntity> Retrieve(long userId)
        {
            try
            {
                return _labelRepo.Retrieve(userId);
            }
            catch
            {
                return null;
            }
        }
    }
}

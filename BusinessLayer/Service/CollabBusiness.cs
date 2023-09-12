using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CollabBusiness:ICollabBusiness
    {
        private readonly ICollabRepo _collabRepo;
        public CollabBusiness(ICollabRepo collabRepo)
        {
            this._collabRepo = collabRepo;
            
        }
        public CollabratorEntity CreateCollab(CollabCreateModel collabCreateModel, long noteId, long userId, string email)
        {
            try
            {
                return _collabRepo.CreateCollab(collabCreateModel, userId, noteId,email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CollabratorEntity> AllCollabs(long NoteId, long userId)
        {
            try
            {
                return _collabRepo.AllCollabs(NoteId,userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteCollab(long collabId)
        {
            try
            {
                _collabRepo.DeleteCollab(collabId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

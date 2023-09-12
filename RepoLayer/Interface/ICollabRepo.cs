using CommonLayer.Model;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface ICollabRepo
    {
        public CollabratorEntity CreateCollab(CollabCreateModel collabCreateModel, long noteId, long userId,string email);
        public List<CollabratorEntity> AllCollabs(long NoteId,long userId);
        public void DeleteCollab(long collabId);
    }
}

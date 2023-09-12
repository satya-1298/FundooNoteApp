using CommonLayer.Model;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoLayer.Service
{
    public class LabelRepo : ILabelRepo
    {
        private readonly FundooContext fundooContext;

        public LabelRepo(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }



        // CREATE LABEL IMPLEMENTATION LOGIC:-
        public LabelEntity CreateLabel(LabelCreateModel model, long? NoteID, long UserId)
        {
            try
            {
                var user = fundooContext.User.FirstOrDefault(x => x.UserId == UserId);
                if ((user != null && NoteID != null) || (NoteID == null))
                {
                    LabelEntity label = new LabelEntity();
                    label.LabelName = model.LabelName;
                    label.UserId = UserId;
                    label.NoteId = NoteID;

                    fundooContext.Label.Add(label);
                    fundooContext.SaveChanges();

                    if (label != null)
                    {
                        return label;
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public LabelEntity AddNote(long noteId, long userId, long labelId)
        {
            try
            {

                LabelEntity user = fundooContext.Label.FirstOrDefault(x => x.UserId == userId && x.LabelId == labelId);
                if (user != null)
                {
                    user.NoteId = noteId;

                    fundooContext.SaveChanges();

                    return user;
                }
                return null;
            }
            catch
            {
                throw;
            }
        }
        //public LabelEntity UpdateLabel(string labelName, long userId)
        //{
        //    try
        //    {
        //        LabelEntity user = fundooContext.Label.FirstOrDefault(x => x.UserId == userId);
        //        if (user != null)
        //        {
        //            user.LabelName = labelName;
        //            fundooContext.SaveChanges();
        //            return user;
        //        }
        //        return null;

        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //}
        public List<LabelEntity> UpdateLabel(string newName, long UserId, string labelName)
        {
            try
            {
                var user = fundooContext.Label.Where(x => x.UserId == UserId && x.LabelName == labelName).ToList();
                if (user != null)
                {
                    foreach (var item in user)
                    {
                        item.LabelName = newName;
                    }
                    fundooContext.SaveChanges();
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch { throw; }
        }
        public List<LabelEntity> DeleteLabel(string labelName, long userId)
        {
            try
            {
                var user = fundooContext.Label.Where(x => x.UserId == userId && x.LabelName == labelName).ToList();
                if (user != null)
                {
                    foreach (var item in user)
                    {
                        fundooContext.Label.Remove(item);
                    }
                    fundooContext.SaveChanges();
                    return user;
                }
                else
                {
                    return null;
                }

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
                var user = fundooContext.Label.Where(x => x.UserId == userId).ToList();
                if (user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch 
            { 
                return null;
            }
        }
    }
}

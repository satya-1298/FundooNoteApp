using CommonLayer.Model;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBusiness
    {
        public LabelEntity CreateLabel(LabelCreateModel model, long? NoteID, long UserId);
        public LabelEntity AddNote(long noteId, long userId, long labelId);
        public List<LabelEntity> UpdateLabel(string newName, long UserId, string labelName);

        public List<LabelEntity> DeleteLabel(string labelName, long userId);
        public List<LabelEntity> Retrieve(long userId);

    }
}

using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;

namespace RepoLayer.Service
{
    public class CollabRepo : ICollabRepo
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;
        public CollabRepo(FundooContext fundooContext, IConfiguration configuration)
        {

            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }
        public CollabratorEntity CreateCollab(CollabCreateModel collabCreateModel, long noteId, long userId,string email)
        {
            try
            {
                var existingCollaborator = fundooContext.Collabrator
                        .FirstOrDefault(c => c.Email == collabCreateModel.Email && c.NoteId == noteId);
                if (existingCollaborator == null&&email!=collabCreateModel.Email)
                {

                    CollabratorEntity collabrator = new CollabratorEntity();
                    collabrator.Email = collabCreateModel.Email;
                    collabrator.UserId = userId;
                    collabrator.NoteId = noteId;
                    fundooContext.Collabrator.Add(collabrator);
                    fundooContext.SaveChanges();
                    return collabrator;
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
        public List<CollabratorEntity> AllCollabs(long NoteId, long userId)
        {
            var result=fundooContext.Collabrator.Where(x=>x.UserId==userId&&x.NoteId==NoteId).ToList();
            if(result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }
        public void DeleteCollab(long collabId)
        {
            var result=fundooContext.Collabrator.FirstOrDefault(x=>x.CollabId==collabId);
            if(result != null)
            {
                fundooContext.Collabrator.Remove(result);
                fundooContext.SaveChanges();
            }
            else
            {
                throw null;
            }
        }
    }
}

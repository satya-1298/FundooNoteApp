using Microsoft.EntityFrameworkCore;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Context
{
    public class FundooContext :DbContext
    {
        public FundooContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<UserEntity>User{ get; set; }
        public DbSet<NotesEntity> Notes { get; set; }
        public DbSet<CollabratorEntity> Collabrator { get; set;}
        public DbSet<LabelEntity> Label { get; set; }   
    }

}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AudioDevices
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    
    public partial class SoundSharpDBEntities : DbContext
    {
        public SoundSharpDBEntities()
            : base("name=SoundSharpDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AudioDevice> AudioDevice { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CdDiscMan> CdDiscMan { get; set; }
        public virtual DbSet<MemoRecorder> MemoRecorder { get; set; }
        public virtual DbSet<Mp3Player> Mp3Player { get; set; }
        public virtual DbSet<Track> Track { get; set; }
        public virtual DbSet<TrackList> TrackList { get; set; }

    }
}
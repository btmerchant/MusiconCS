using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Musicon.Models;

namespace Musicon.DAL
{
    public class MusiconRepository
    {
        public MusiconContext context { get; set; }
        public IDbSet<ApplicationUser> Users { get { return context.Users; } }

        public MusiconRepository()
        {
            context = new MusiconContext();
        }

        public MusiconRepository(MusiconContext _context)
        {
            context = _context;
        }

        public ApplicationUser GetUser(string user_id)
        {
            return context.Users.FirstOrDefault(i => i.Id == user_id);
        }

        public int GetSongCount()
        {
            return context.Songs.Count();
        }

        public List<Song> GetSongs()
        {
            return context.Songs.ToList<Song>();
        }

        public void AddSong(string title, string artist, string composer, string key, string tempo, int length, string status, string vocal, DateTime entryDate)
        {
            Song new_song = new Song { Title = title, Artist = artist, Composer =  composer, Key =  key, Tempo =  tempo, Length = length, Status =  status, Vocal =  vocal, EntryDate  = entryDate};
            context.Songs.Add(new_song);
            context.SaveChanges();
        }

        public Song GetSongOrNull(int _song_id)
        {
            return context.Songs.FirstOrDefault(i => i.SongId == _song_id);
        }

        public void EditSong(Song song_to_edit)
        {
            context.Entry(song_to_edit).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
    }

  
}
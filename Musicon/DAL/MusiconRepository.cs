using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using Musicon.Models;
using Microsoft.AspNet.Identity;
using System.Security.Principal;

namespace Musicon.DAL
{
    
    public class MusiconRepository : Controller
    {

        public MusiconContext context { get; set; }
        public IDbSet<ApplicationUser> Users { get { return context.Users; } }

        public MusiconRepository()
        {
            context = new MusiconContext();
        }
        // Allows us to isolate Repo from context during testing
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

        public List<Song> GetAllSongs()
        {
            return context.Songs.ToList<Song>();
        }

        public List<Song> GetUserSongs(ApplicationUser user)
        {
            //List<Song> userList = context.Songs.ToList();
            //IEnumerable<Song> userQuery = context.Songs.Where(s => s.Member == user);
            //List<Song> userList = userQuery.ToList();

            //foreach (var songList in context.Songs)
            //{
            //    if (context.Songs.Any()
            //    {

            //    }
            //}
            List<Song> songs = context.Songs.ToList<Song>();
            List<Song> userList = new List<Song>();
     
            foreach (var item in songs)
            {
                if (item.Member == user)
                {
                    userList.Add(item);
                }
            }
            return userList;
        }

        public void AddSong(string title, string artist, string composer, string key, string tempo, double length, string status, string vocal, DateTime entryDate, string genre, ApplicationUser member)
        {
            Song new_song = new Song{Title = title, Artist = artist, Composer = composer,Key = key,Tempo = tempo,Length = length, Status = status, Vocal = vocal,EntryDate = entryDate, Genre = genre, Member = member };
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
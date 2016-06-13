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

        public Song GetSong(int id)
        {
            return context.Songs.Find(id); ;
        }

        public List<Song> GetAllSongs()
        {
            return context.Songs.ToList<Song>();
        }
        
        public List<Song> GetUserSongs(ApplicationUser user)
        {
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

        public List<Song> GetGroupSongs(string group_Id) // Work in Progress
        {
            List<Song> groupSongList = new List<Song>();
            return groupSongList;
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

        public IEnumerable<SelectListItem> GetStatusList()
        {
            IEnumerable<SelectListItem> statusList = context.Statuses.Select(s => new SelectListItem { Value = s.StatusType, Text = s.StatusType });
            return statusList;
        }

        public IEnumerable<SelectListItem> GetTempoList()
        {
            IEnumerable<SelectListItem> tempoList = context.Tempos.Select(s => new SelectListItem { Value = s.TempoType, Text = s.TempoType });
            return tempoList;
        }

        public string GetSelectedStatus()
        {
            string statusSelected = context.Songs.Select(s => new SelectListItem { Value = s.Status, Text = s.Status }).ToString();
            return statusSelected;
        }

        public string GetSelectedTempo()
        {
            string tempoSelected = context.Songs.Select(s => new SelectListItem { Value = s.Tempo, Text = s.Tempo }).ToString();
            return tempoSelected;
        }

        public void DeleteSelectedSong(int id)
        {
            Song song = context.Songs.Find(id);
            context.Songs.Remove(song);
            context.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
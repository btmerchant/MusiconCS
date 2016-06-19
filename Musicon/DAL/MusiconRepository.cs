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

        // MethodRepo   MusiconRepository
        public MusiconRepository()
        {
            context = new MusiconContext();
        }

        // Allows us to isolate Repo from context during testing
        // Method   MusiconRepository-Overload - Repo
        public MusiconRepository(MusiconContext _context)
        {
            context = _context;
        }

        // MethodRepo   GetUser        
        public ApplicationUser GetUser(string user_id)
        {
            return context.Users.FirstOrDefault(i => i.Id == user_id);
        }

        // MethodRepo   GetSongCount
        public int GetSongCount()
        {
            return context.Songs.Count();
        }

        //MethodRepo    GetSong
        public Song GetSong(int id)
        {
            return context.Songs.Find(id); ;
        }

        // MethodRepo   GetAllSongs
        public List<Song> GetAllSongs()
        {
            return context.Songs.ToList<Song>();
        }

        // MethodRepo   GetUserSongs
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

        // MethodRepo   AddSong
        public void AddSong(string title, string artist, string composer, string key, string tempo, double length, string status, string vocal, DateTime entryDate, string genre, ApplicationUser member)
        {
            Song new_song = new Song{Title = title, Artist = artist, Composer = composer,Key = key,Tempo = tempo,Length = length, Status = status, Vocal = vocal,EntryDate = entryDate, Genre = genre, Member = member };
            context.Songs.Add(new_song);
            context.SaveChanges();   
        }


        // MethodRepo   GetSongOrNull
        public Song GetSongOrNull(int _song_id)
        {
            return context.Songs.FirstOrDefault(i => i.SongId == _song_id);
        }


        //MethodRepo    EditSong
        public void EditSong(Song song_to_edit)
        {
            context.Entry(song_to_edit).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        // MethodRepo   GetStatusList
        public IEnumerable<SelectListItem> GetStatusList()
        {
            IEnumerable<SelectListItem> statusList = context.Statuses.Select(s => new SelectListItem { Value = s.StatusType, Text = s.StatusType });
            return statusList;
        }

        // Method   GetTempoList
        public IEnumerable<SelectListItem> GetTempoList()
        {
            IEnumerable<SelectListItem> tempoList = context.Tempos.Select(s => new SelectListItem { Value = s.TempoType, Text = s.TempoType });
            return tempoList;
        }

        // MethodRepo   GetSelectedStatus
        public string GetSelectedStatus()
        {
            string statusSelected = context.Songs.Select(s => new SelectListItem { Value = s.Status, Text = s.Status }).ToString();
            return statusSelected;
        }

        // MethodRepo   GetSelectedTempo
        public string GetSelectedTempo()
        {
            string tempoSelected = context.Songs.Select(s => new SelectListItem { Value = s.Tempo, Text = s.Tempo }).ToString();
            return tempoSelected;
        }

        //MethodRepo    DeleteSelectedSong
        public void DeleteSelectedSong(int id)
        {
            Song song = context.Songs.Find(id);
            context.Songs.Remove(song);
            context.SaveChanges();
        }

        // MethodRepo    GetAllGroups
        public List<Group> GetAllGroups()
        {
            return context.Groups.ToList<Group>();
        }

        //MethodRepo    GetListOfUsersGroupsOrNull
        public List<int> GetAListOfUsersGroupsOrNull(ApplicationUser user) //Return a list of group ids that the user is a member of
        {
            List<GroupMember> groupMembers = context.GroupMemberRelations.ToList<GroupMember>();
            List<int> users_list = new List<int>();

            foreach (var item in groupMembers)
            {
                if (item.User == user)
                {
                    users_list.Add(item.Group.GroupId);
                }
                else
                {
                    users_list = null;
                }
            }
            return users_list;
        }

        // MethodRepo   GetGroupSongs
        public List<Song> GetGroupSongs(string group_Id) // Work in Progress
        {
            List<Song> groupSongList = new List<Song>();
            return groupSongList;
        }

        // MethodRepo   GetGroupCount
        public int GetGroupCount()
        {
            return context.Groups.Count();
        }

        // MethodRepo   GetGroupOrNull
        public Group GetGroupOrNull(int _group_id)
        {
            return context.Groups.FirstOrDefault(i => i.GroupId == _group_id);
        }

        // MethodRepo   AddGroup
        public void AddGroup(string name, DateTime dateFormed, string style)
        {
            Group new_group = new Group { Name = name, DateFormed = dateFormed, Style = style};
            context.Groups.Add(new_group);
            context.SaveChanges();
        }

        // MethodRepo   GetGroupByNameOrNull
        public Group GetGroupByNameOrNull(string name)
        {
            Group group;
            try
            {
                group = context.Groups.First(i => i.Name == name);
            }
            catch (Exception)
            {
                group = null;
            }
            return group; // ConnectMockstoDatastore made this possible
        }

        // MethodRepo   GetGroupByIdOrNull
        public Group GetGroupByIdOrNull(int id)
        {
            //return context.Polls.Find(_group_id); // Requires explicit mocking of the DbSet.Find method
            Group group;
            try
            {
                group = context.Groups.First(i => i.GroupId == id);
            }
            catch (Exception)
            {
                group = null;
            }
            return group; // ConnectMockstoDatastore made this possible
        }

        // MethodRepo   JoinGroupByName
        public bool JoinGroupByName(string name, ApplicationUser user)
        {
            bool result = false;
            Group found_group = GetGroupByNameOrNull(name);

            if (found_group == null)
            {
                result = false;
            }
            else
            {
                context.GroupMemberRelations.Add(new GroupMember { User = user, Group = found_group  });
                result = true;
            }
            return result;
        }

        // MethodRepo   JoinGroupById
        public bool JoinGroupById(int id, ApplicationUser user)
        {
            bool result = false;
            Group found_group = GetGroupByIdOrNull(id);

            if (found_group == null)
            {
                result = false;
            }
            else
            {
                context.GroupMemberRelations.Add(new GroupMember { User = user, Group = found_group });
                context.SaveChanges();
                result = true;
            }
            return result;
        }

        // MethodRepo   IsUserAMember
        public bool IsUserAMember(string group_name, ApplicationUser user)
        {
            bool result = false;
            //List<GroupMember> groupList = context.GroupMemberRelations.ToList(); //error here

            foreach (var item in context.GroupMemberRelations.ToList())
            {
                if (item.Group.Name == group_name && item.User == user)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        //MethodRepo GetGroupMemberList
        public List<ApplicationUser> GetGroupMemberList(int group_id, ApplicationUser user)
        {
            List<ApplicationUser> members = new List<ApplicationUser>();

            foreach (var item in context.GroupMemberRelations.ToList())
            {
                if (item.Group.GroupId == group_id)
                {
                    members.Add(user);
                }
            }
            return members;
        }

        //MethodRepo GetGroupMemberRelationById
        public GroupMember GetGroupMemberRelationById(int? id)
        {
            GroupMember groupMember;
            try
            {
                groupMember = context.GroupMemberRelations.First(i => i.Group.GroupId == id);
            }
            catch (Exception)
            {
                groupMember = null;
            }
            return groupMember; // ConnectMockstoDatastore made this possible
        }

        //MethodRepo QuitGroupById
        public bool QuitGroupById(int id, ApplicationUser user)
        {
            bool result = false;
            GroupMember found_group_member = GetGroupMemberRelationById(id);

            if (found_group_member == null)
            {
                result = false;
            }
            else
            {
                context.GroupMemberRelations.Remove(found_group_member);
                context.SaveChanges();
                result = true;
            }
            return result;
        }


        // MethodRepo   Dispose
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
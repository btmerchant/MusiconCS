﻿using System;
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

        // MethodRepo   GetGroupSongCount
        public int GetGroupSongCount()
        {
            return context.GroupSongs.Count();
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

        public List<GroupSong> GetGroupSongs()
        {
            List<GroupSong> groupSongs = context.GroupSongs.ToList<GroupSong>();
            List<GroupSong> groupSongList = new List<GroupSong>();
            int currentGroupId = Convert.ToInt32(System.Web.HttpContext.Current.Session["currentGroupId"]);

            foreach (var item in groupSongs)
            {
                if (item.GroupId == currentGroupId)
                {
                    groupSongList.Add(item);
                }
            }
            return groupSongList;
        }

        // MethodRepo   AddSong
        public void AddSong(string title, string artist, string composer, string key, string tempo, double length, string status, string vocal, DateTime entryDate, string genre, ApplicationUser member , string arrangement, string lyric)
        {
            Song new_song = new Song{Title = title, Artist = artist, Composer = composer,Key = key,Tempo = tempo,Length = length, Status = status, Vocal = vocal,EntryDate = entryDate, Genre = genre, Member = member, Arrangement = arrangement, Lyric = lyric };
            context.Songs.Add(new_song);
            context.SaveChanges();   
        }

        // MethodRepo   AddGroupSong
        public void AddGroupSong(string title, string artist, string composer, string key, string tempo, double length, string status, string vocal, DateTime entryDate, string genre, int groupId, string arrangement, string lyric)
        {
            GroupSong new_song = new GroupSong { Title = title, Artist = artist, Composer = composer, Key = key, Tempo = tempo, Length = length, Status = status, Vocal = vocal, EntryDate = entryDate, Genre = genre, GroupId = groupId, Arrangement = arrangement, Lyric = lyric };
            context.GroupSongs.Add(new_song);
            context.SaveChanges();
        }

        // MethodRepo   GetSongOrNull
        public Song GetSongOrNull(int _song_id)
        {
            return context.Songs.FirstOrDefault(i => i.SongId == _song_id);
        }

        // MethodRepo   GetSongOrNull
        public GroupSong GetGroupSongOrNull(int _group_song_id)
        {
            return context.GroupSongs.FirstOrDefault(i => i.GroupSongId == _group_song_id);
        }

        //MethodRepo    EditSong
        public void EditSong(Song song_to_edit)
        {
            context.Entry(song_to_edit).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        //MethodRepo    EditGroupSong
        public void EditGroupSong(GroupSong group_song_to_edit)
        {
            context.Entry(group_song_to_edit).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }






        //******************************************

        // MethodRepo   GetArrangementOrNull
        public string GetArrangementOrNull(int _song_id)
        {
            Song tempSong = context.Songs.FirstOrDefault(i => i.SongId == _song_id);
            return tempSong.Arrangement;
        }


        //MethodRepo    EditArrangement
        public void EditArrangement(Song song_to_edit)
        {
            context.Entry(song_to_edit).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }


        // MethodRepo   GetLyricOrNull
        public string GetLyricOrNull(int _song_id)
        {
            Song tempSong = context.Songs.FirstOrDefault(i => i.SongId == _song_id);
            return tempSong.Lyric.ToString();
        }


        //MethodRepo    EditLyric
        public void EditLyric(Song song_to_edit)
        {
            context.Entry(song_to_edit).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        //************************************************











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
            return group;
        }

        // MethodRepo   GetGroupByIdOrNull
        public Group GetGroupByIdOrNull(int id)
        { 
            Group group;
            try
            {
                group = context.Groups.First(i => i.GroupId == id);
            }
            catch (Exception)
            {
                group = null;
            }
            return group;
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
            
            Group group = GetGroupByNameOrNull(group_name);

            foreach (var item in context.GroupMemberRelations.ToList())
            {
                if (item.Group.GroupId == group.GroupId && item.User == user)
                {
                    result = true;
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
                    members.Add(item.User);
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
            return groupMember;
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
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Musicon.DAL;
using System.Collections.Generic;
using Musicon.Models;
using Moq;
using System.Linq;
using System.Data.Entity;

namespace Musicon.Tests.DAL
{
    [TestClass]
    public class MusiconRepositoryTest
    {
        private DateTime myNow = DateTime.Now;

        Mock<MusiconContext> mock_context { get; set; }
        MusiconRepository repo { get; set; }

        // Songs
        List<Song> songs_datasource { get; set; } 
        Mock<DbSet<Song>> mock_songs_table { get; set; } // Fake Songs table
        IQueryable<Song> song_data { get; set; }// Turns List<Song> into something we can query with LINQ

        // Statuses
        List<Status> statuses_datasource { get; set; }
        Mock<DbSet<Status>> mock_statuses_table { get; set; } // Fake Statuses table
        IQueryable<Status> status_data { get; set; }

        // Tempos
        List<Tempo> tempos_datasource { get; set; }
        Mock<DbSet<Tempo>> mock_tempos_table { get; set; } // Fake Tempos table
        IQueryable<Tempo> tempo_data { get; set; }

        // Groups
        List<Group> groups_datasource { get; set; }
        Mock<DbSet<Group>> mock_groups_table { get; set; } // Fake Groups table
        IQueryable<Group> group_data { get; set; }

        // GroupMembers
        List<GroupMember> groupMembers_datasource { get; set; }
        Mock<DbSet<GroupMember>> mock_groupMembers_table { get; set; } // Fake Groups table
        IQueryable<GroupMember> groupMember_data { get; set; }

        // MethodTests Initialize
        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<MusiconContext> { CallBase = true };

            songs_datasource = new List<Song>();
            statuses_datasource = new List<Status>();
            tempos_datasource = new List<Tempo>();
            groups_datasource = new List<Group>();
            groupMembers_datasource = new List<GroupMember>();

            mock_songs_table = new Mock<DbSet<Song>>(); // Fake Songs table
            mock_statuses_table = new Mock<DbSet<Status>>();
            mock_tempos_table = new Mock<DbSet<Tempo>>();
            mock_groups_table = new Mock<DbSet<Group>>();
            mock_groupMembers_table = new Mock<DbSet<GroupMember>>();

            repo = new MusiconRepository(mock_context.Object); // Injects mocked (fake) MusiconContext
            song_data = songs_datasource.AsQueryable(); // Turns List<Song> into something we can query with LINQ
            status_data = statuses_datasource.AsQueryable();
            tempo_data = tempos_datasource.AsQueryable();
            group_data = groups_datasource.AsQueryable();
            groupMember_data = groupMembers_datasource.AsQueryable();
        }

        // MethodTests Cleanup
        [TestCleanup]
        public void Cleanup()
        {
            songs_datasource = null;
            statuses_datasource = null;
            tempos_datasource = null;
            groups_datasource = null;
            groupMembers_datasource = null;
        }

        // MethodTests ConnectMocksToDatastore
        void ConnectMocksToDatastore() // Utility method
        {
            // Telling our fake DbSet to use our datasource like something Queryable
            mock_songs_table.As<IQueryable<Song>>().Setup(m => m.GetEnumerator()).Returns(song_data.GetEnumerator());
            mock_songs_table.As<IQueryable<Song>>().Setup(m => m.ElementType).Returns(song_data.ElementType);
            mock_songs_table.As<IQueryable<Song>>().Setup(m => m.Expression).Returns(song_data.Expression);
            mock_songs_table.As<IQueryable<Song>>().Setup(m => m.Provider).Returns(song_data.Provider);

            // Tell our mocked MusiconContext to use our fully mocked Datasource. (List<Song>)
            mock_context.Setup(m => m.Songs).Returns(mock_songs_table.Object);


            // Telling our fake DbSet to use our datasource like something Queryable
            mock_statuses_table.As<IQueryable<Status>>().Setup(m => m.GetEnumerator()).Returns(status_data.GetEnumerator());
            mock_statuses_table.As<IQueryable<Status>>().Setup(m => m.ElementType).Returns(status_data.ElementType);
            mock_statuses_table.As<IQueryable<Status>>().Setup(m => m.Expression).Returns(status_data.Expression);
            mock_statuses_table.As<IQueryable<Status>>().Setup(m => m.Provider).Returns(status_data.Provider);

            // Tell our mocked MusiconContext to use our fully mocked Datasource. (List<Status>)
            mock_context.Setup(m => m.Statuses).Returns(mock_statuses_table.Object);

            // Telling our fake DbSet to use our datasource like something Queryable
            mock_tempos_table.As<IQueryable<Tempo>>().Setup(m => m.GetEnumerator()).Returns(tempo_data.GetEnumerator());
            mock_tempos_table.As<IQueryable<Tempo>>().Setup(m => m.ElementType).Returns(tempo_data.ElementType);
            mock_tempos_table.As<IQueryable<Tempo>>().Setup(m => m.Expression).Returns(tempo_data.Expression);
            mock_tempos_table.As<IQueryable<Tempo>>().Setup(m => m.Provider).Returns(tempo_data.Provider);

            // Tell our mocked MusiconContext to use our fully mocked Datasource. (List<Tempo>)
            mock_context.Setup(m => m.Tempos).Returns(mock_tempos_table.Object);

            // Telling our fake DbSet to use our datasource like something Queryable
            mock_groups_table.As<IQueryable<Group>>().Setup(m => m.GetEnumerator()).Returns(group_data.GetEnumerator());
            mock_groups_table.As<IQueryable<Group>>().Setup(m => m.ElementType).Returns(group_data.ElementType);
            mock_groups_table.As<IQueryable<Group>>().Setup(m => m.Expression).Returns(group_data.Expression);
            mock_groups_table.As<IQueryable<Group>>().Setup(m => m.Provider).Returns(group_data.Provider);

            // Tell our mocked MusiconContext to use our fully mocked Datasource. (List<Group>)
            mock_context.Setup(m => m.Groups).Returns(mock_groups_table.Object);

            // Telling our fake DbSet to use our datasource like something Queryable
            mock_groupMembers_table.As<IQueryable<GroupMember>>().Setup(m => m.GetEnumerator()).Returns(groupMember_data.GetEnumerator());
            mock_groupMembers_table.As<IQueryable<GroupMember>>().Setup(m => m.ElementType).Returns(groupMember_data.ElementType);
            mock_groupMembers_table.As<IQueryable<GroupMember>>().Setup(m => m.Expression).Returns(groupMember_data.Expression);
            mock_groupMembers_table.As<IQueryable<GroupMember>>().Setup(m => m.Provider).Returns(groupMember_data.Provider);

            // Tell our mocked MusiconContext to use our fully mocked Datasource. (List<GroupMember>)
            mock_context.Setup(m => m.GroupMemberRelations).Returns(mock_groupMembers_table.Object);


            // Hijack the call to the Add methods and put it the list using the List's Add method.
            mock_songs_table.Setup(m => m.Add(It.IsAny<Song>())).Callback((Song song) => songs_datasource.Add(song));
            mock_statuses_table.Setup(m => m.Add(It.IsAny<Status>())).Callback((Status status) => statuses_datasource.Add(status));
            mock_tempos_table.Setup(m => m.Add(It.IsAny<Tempo>())).Callback((Tempo tempo) => tempos_datasource.Add(tempo));
            mock_groups_table.Setup(m => m.Add(It.IsAny<Group>())).Callback((Group group) => groups_datasource.Add(group));
            mock_groupMembers_table.Setup(m => m.Add(It.IsAny<GroupMember>())).Callback((GroupMember groupMember) => groupMembers_datasource.Add(groupMember));

        }

        // MethodTests RepoEnsureICanCreateInstance
        [TestMethod]
        public void RepoEnsureICanCreateInstance()
        {
            //Repo is setup at top;
            Assert.IsNotNull(repo);
        }

        // MethodTests RepoEnsureIsUsingContext
        [TestMethod]
        public void RepoEnsureIsUsingContext()
        {
            // Arrange 

            // Act

            // Assert
            Assert.IsNotNull(repo.context);
        }

        // MethodTests RepoEnsureThereAreNoSongs
        [TestMethod]
        public void RepoEnsureThereAreNoSongs()
        {
            // Arrange 
            ConnectMocksToDatastore();

            // Act
            List<Song> list_of_songs = repo.GetAllSongs();
            List<Song> expected = new List<Song>();

            // Assert
            Assert.AreEqual(expected.Count, list_of_songs.Count);
        }

        // MethodTests RepoEnsureSongCountIsZero
        [TestMethod]
        public void RepoEnsureSongCountIsZero()
        {
            // Arrange 
            ConnectMocksToDatastore();

            // Act
            int expected = 0;
            int actual = repo.GetSongCount();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        // MethodTests RepoEnsureICanNotFindOrNull
        [TestMethod]
        public void RepoEnsureICanNotFindOrNull()
        {
            // Arrange
            Song song_in_db = new Song { SongId = 1, Title = "Some Title", Artist = "An Artist", Composer = "A Composer", Key = "A", Tempo = "Moderate", Length = 2.00, Status = "Active", Vocal = "Some Singer", EntryDate = DateTime.Now };
            Song song_in_db_2 = new Song { SongId = 2, Title = "Some Title", Artist = "An Artist", Composer = "A Composer", Key = "A", Tempo = "Moderate", Length = 2.00, Status = "Active", Vocal = "Some Singer", EntryDate = DateTime.Now };
            songs_datasource.Add(song_in_db);
            songs_datasource.Add(song_in_db_2);

            songs_datasource.Remove(song_in_db_2);

            ConnectMocksToDatastore();

            // Act
            Song found_song = repo.GetSongOrNull(5);

            // Assert
            Assert.IsNull(found_song);
        }

        // MethodTests RepoEnsureICanCreateASong
        [TestMethod]
        public void RepoEnsureICanCreateASong()
        {
            // Arrange
            Song song_in_db = new Song { SongId = 1, Title = "Some Title", Artist = "An Artist", Composer = "A Composer", Key = "A", Tempo = "Moderate", Length = 2.00, Status = "Active", Vocal = "Some Singer", EntryDate = DateTime.Now };
            songs_datasource.Add(song_in_db);
            ConnectMocksToDatastore();

            // Act
            Song found_song = repo.GetSongOrNull(5);

            // Assert
            Assert.IsNull(found_song);
        }

        // MethodTests RepoEnsureThereAreNoGroups
        [TestMethod]
        public void RepoEnsureThereAreNoGroups()
        {
            // Arrange 
            ConnectMocksToDatastore();

            // Act
            List<Group> list_of_groups = repo.GetAllGroups();
            List<Group> expected = new List<Group>();

            // Assert
            Assert.AreEqual(expected.Count, list_of_groups.Count);
        }

        // MethodTests RepoEnsureGroupCountIsZero
        [TestMethod]
        public void RepoEnsureGroupCountIsZero()
        {
            // Arrange 
            ConnectMocksToDatastore();

            // Act
            int expected = 0;
            int actual = repo.GetGroupCount();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        // MethodTests RepoEnsureICanCreateAGroup
        [TestMethod]
        public void RepoEnsureICanCreateAGroup()
        {
            // Arrange
            Group group_in_db = new Group { GroupId = 1, Name = "Some Name", DateFormed = DateTime.Now, Style = "A Style"};
            groups_datasource.Add(group_in_db);
            ConnectMocksToDatastore();

            // Act
            Group found_group = repo.GetGroupOrNull(5);

            // Assert
            Assert.IsNull(found_group);
        }

        // MethodTests RepoEnsureICanNotFindOrNullGroup
        [TestMethod]
        public void RepoEnsureICanNotFindOrNullGroup()
        {
            // Arrange
            Group group_in_db = new Group { GroupId = 1, Name = "Some Name", DateFormed = DateTime.Now, Style = "A Style"};
            Group group_in_db_2 = new Group { GroupId = 2, Name = "Some Name", DateFormed = DateTime.Now, Style = "A Style"};
            groups_datasource.Add(group_in_db);
            groups_datasource.Add(group_in_db_2);

            groups_datasource.Remove(group_in_db_2);

            ConnectMocksToDatastore();

            // Act
            Group found_group = repo.GetGroupOrNull(5);

            // Assert
            Assert.IsNull(found_group);
        }

        // MethodTests RepoEnsureICanGetGroupByNameOrNull
        [TestMethod]
        public void RepoEnsureICanGetGroupByNameOrNull()
        {
            // Arrange
            Group group_in_db = new Group { GroupId = 0, Name = "Some Group", DateFormed = myNow, Style = "Some Style"};
            groups_datasource.Add(group_in_db);
            Group expected = new Group();
            expected.Name = "Some Group";
            expected.DateFormed = myNow;
            expected.Style = "Some Style";
            expected.GroupId = 0;

            ConnectMocksToDatastore();
            // Act
            Group actual = repo.GetGroupByNameOrNull("Some Group");
            Group actualNot = repo.GetGroupByNameOrNull("A Non Group");

            // Assert
            //Assert.AreEqual(expected, actual); // use this to compare 2 db items

            Assert.AreEqual(expected.GroupId, actual.GroupId); // Use multiple asserts to compare a db object to an actual memory object
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.DateFormed, actual.DateFormed);
            Assert.AreEqual(expected.Style, actual.Style);

            Assert.IsNull(actualNot);
        }

        // MethodTests RepoEnsureICanGetGroupByIdOrNull
        [TestMethod]
        public void RepoEnsureICanGetGroupByIdOrNull()
        {
            // Arrange
            Group group_in_db = new Group { GroupId = 1, Name = "Some Group", DateFormed = myNow, Style = "Some Style" };
            groups_datasource.Add(group_in_db);
            Group expected = new Group();
            expected.Name = "Some Group";
            expected.DateFormed = myNow;
            expected.Style = "Some Style";
            expected.GroupId = 1;

            ConnectMocksToDatastore();
            // Act
            Group actual = repo.GetGroupByIdOrNull(1);
            Group actualNot = repo.GetGroupByIdOrNull(0);

            // Assert
            //Assert.AreEqual(expected, actual); // use this to compare 2 db items

            Assert.AreEqual(expected.GroupId, actual.GroupId); // Use multiple asserts to compare a db object to an actual memory object
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.DateFormed, actual.DateFormed);
            Assert.AreEqual(expected.Style, actual.Style);

            Assert.IsNull(actualNot);
        }

        // MethodTests RepoEnsureICanJoinAGroup
        [TestMethod]
        public void RepoEnsureICanJoinAGroup()
        {
            // Arrange
            Group group_in_db = new Group { GroupId = 1, Name = "Group_A", DateFormed = myNow, Style = "Style_A" };
            groups_datasource.Add(group_in_db);
            ApplicationUser user = new ApplicationUser();
            ConnectMocksToDatastore();

            // Act
            bool sucess = repo.JoinGroupByName("Group_A", user);
            bool member = repo.IsUserAMember("Group_A", user);

            // Assert
            Assert.IsTrue(sucess);
            Assert.IsTrue(member);
        }
    }
}

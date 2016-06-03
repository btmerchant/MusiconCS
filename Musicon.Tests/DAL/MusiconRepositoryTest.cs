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

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<MusiconContext> { CallBase = true };

            songs_datasource = new List<Song>();
            statuses_datasource = new List<Status>();
            tempos_datasource = new List<Tempo>();

            mock_songs_table = new Mock<DbSet<Song>>(); // Fake Songs table
            mock_statuses_table = new Mock<DbSet<Status>>();
            mock_tempos_table = new Mock<DbSet<Tempo>>();

            repo = new MusiconRepository(mock_context.Object); // Injects mocked (fake) MusiconContext
            song_data = songs_datasource.AsQueryable(); // Turns List<Song> into something we can query with LINQ
            status_data = statuses_datasource.AsQueryable();
            tempo_data = tempos_datasource.AsQueryable();
        }

        [TestCleanup]
        public void Cleanup()
        {
            songs_datasource = null;
            statuses_datasource = null;
            tempos_datasource = null;
        }

        void ConnectMocksToDatastore() // Utility method
        {
            // Telling our fake DbSet to use our datasource like something Queryable
            mock_songs_table.As<IQueryable<Song>>().Setup(m => m.GetEnumerator()).Returns(song_data.GetEnumerator());
            mock_songs_table.As<IQueryable<Song>>().Setup(m => m.ElementType).Returns(song_data.ElementType);
            mock_songs_table.As<IQueryable<Song>>().Setup(m => m.Expression).Returns(song_data.Expression);
            mock_songs_table.As<IQueryable<Song>>().Setup(m => m.Provider).Returns(song_data.Provider);

            // Tell our mocked VotrContext to use our fully mocked Datasource. (List<Song>)
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

            // Hijack the call to the Add methods and put it the list using the List's Add method.
            mock_songs_table.Setup(m => m.Add(It.IsAny<Song>())).Callback((Song song) => songs_datasource.Add(song));
            mock_statuses_table.Setup(m => m.Add(It.IsAny<Status>())).Callback((Status status) => statuses_datasource.Add(status));
            mock_tempos_table.Setup(m => m.Add(It.IsAny<Tempo>())).Callback((Tempo tempo) => tempos_datasource.Add(tempo));

        }

        [TestMethod]
        public void RepoEnsureICanCreateInstance()
        {
            //Repo is setup at top;
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void RepoEnsureIsUsingContext()
        {
            // Arrange 

            // Act

            // Assert
            Assert.IsNotNull(repo.context);
        }

        [TestMethod]
        public void RepoEnsureThereAreNoSongs()
        {
            // Arrange 
            ConnectMocksToDatastore();

            // Act
            List<Song> list_of_songs = repo.GetSongs();
            List<Song> expected = new List<Song>();

            // Assert
            Assert.AreEqual(expected.Count, list_of_songs.Count);
        }

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
    }
}

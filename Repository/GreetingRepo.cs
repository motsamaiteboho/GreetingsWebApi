using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GreetingsAPI.Interfaces;
using GreetingsAPI.Models;

namespace GreetingsAPI.Repository
{
    public class GreetingRepo:IGreeting
    {
    private string _connectionString;
    public GreetingRepo(string connectionString)
    {
        _connectionString = connectionString;

        using (var connection = new SQLiteConnection(_connectionString))
        {
            string CREATE_USER_TABLE = @"create table if not exists user (
	            Id integer primary key AUTOINCREMENT,
	            Name text,
	            Count integer,
	            Language text,
                ImageFile text
            );";
            connection.Execute(CREATE_USER_TABLE);
        }
    }
    //"Data Source=./trying_dapper.db";
    public IEnumerable<Friend> GetGreetedFriends()
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            var Friends = connection.Query<Friend>(@"select * from user");
            return Friends;
        }
    }
    public Friend GetFriend(string Name)
    {
        var template = new Friend { Name = Name };
        var parameters = new DynamicParameters(template);
        var sql = @"select * from user where Name = @Name";
        using (var connection = new SQLiteConnection(_connectionString))
        {
            var user = connection.QueryFirstOrDefault<Friend>(sql, parameters);
            return user;
        }
    }

    public void GreetFriend(Friend user)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            var sql = @" insert into  user (Name, Count, Language, ImageFile)
	                    values (@Name, @Count, @Language, @ImageFile);";
            var parameters = new Friend()
            {
                Name = user.Name,
                Count = user.Count,
                Language = user.Language,
                ImageFile = user.ImageFile
            };

            connection.Execute(sql, parameters);
        }
    }

    public void UpdateFriend(Friend user)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            var template = new Friend { Name = user.Name, Count = user.Count + 1, Language = user.Language, ImageFile = user.ImageFile };
            var parameters = new DynamicParameters(template);
            var sql = @" update user
	                     set Count= @Count, Language = @Language, ImageFile = @ImageFile
                         where Name = @Name;";
            connection.Execute(sql, parameters);
        }
    }
    public void RemoveFriend(Friend user)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            if (user.Count > 1)
            {
                var template = new Friend { Name = user.Name, Count = user.Count - 1 };
                var parameters = new DynamicParameters(template);
                var sql = @"update user
                          set Count= @Count
                          where Name = @Name;";
                connection.Execute(sql, parameters);
            }
            else
            {
                var template = new Friend { Name = user.Name };
                var parameters = new DynamicParameters(template);
                var sql = @"delete from user
                        where Name = @Name;";
                connection.Execute(sql, parameters);
            }

        }
    }

    public void ClearAllFriends()
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            var sql = @"delete from user;";
            connection.Execute(sql);
        }
    }
    }
}
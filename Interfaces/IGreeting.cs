using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreetingsAPI.Models;

namespace GreetingsAPI.Interfaces
{
    public interface IGreeting
    {
    IEnumerable<Friend> GetGreetedFriends();
    Friend GetFriend(string Name);
    void GreetFriend(Friend friend);
    void UpdateFriend(Friend friend);
    void  RemoveFriend(Friend friend);
    void ClearAllFriends();
    }
}
using GreetingsAPI.Interfaces;
using GreetingsAPI.Models;
using Microsoft.AspNetCore.Mvc;
namespace GreetingsAPI.Controllers;

[ApiController]
 [Route( "api/greetings" )]
public class GreetingsController : ControllerBase
{
    private readonly ILogger<GreetingsController> _logger;

    private IGreeting greetings;
    public GreetingsController(ILogger<GreetingsController> logger, IGreeting pGreetings)
    {
        _logger = logger;
        greetings = pGreetings;
    }

    [HttpGet]
    public IActionResult GetGreetedFriends()
    {
       IEnumerable<Friend> friends  =  greetings.GetGreetedFriends();
       return Ok(friends.ToList());
    }

    [HttpGet]
    [Route("{name}")]
    public IActionResult GetGreetedFriend(string name)
    {
       Friend friend  =  greetings.GetFriend(name);
       return Ok(friend);
    }

    [HttpPost]
    public IActionResult GreetFriendPost(Friend friend)
    {
       greetings.GreetFriend(friend);
       return Ok("Greeted friend");
    }

    [HttpDelete]
    public IActionResult RemoveFriend(Friend friend)
    {
       greetings.RemoveFriend(friend);
       return Ok("removed friend");
    }
}

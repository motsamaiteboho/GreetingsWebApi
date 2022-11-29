using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreetingsAPI.Models
{
    public class Friend
    {
         public int Id {get; set;}
    [Required]
    public string? Name {get; set;}
    public int Count {get; set;}
    public string Language {get; set;} = string.Empty;
    public string? ImageFile { get; set; }
    public IFormFile? Upload { get; set; } 
    
    public string GreetingMessage { 
        get
        {
            Dictionary<string, string> Languages = new(){
            {"English", "Hello"},  {"Afrikaans", "Hallo"},  {"Sesotho", "Dumelang"}
            };
            if(Name != null)
            {
                foreach (var lang in Languages)
                {
                    if (lang.Key == Language)
                    {
                        return $"{lang.Value}, {Name}";
                    }
                }
            }
            return "null";
        }
    }
    public string ProfilePic { 
        get
        {
            if(ImageFile != null)
            {
                return ImageFile;
            }
             return "";
        }
       
    }

    public string GetName { 
        get
        {
            if(Name != null)
            {
                return Name;
            }
            return "";
        }
        
    }
    }
}
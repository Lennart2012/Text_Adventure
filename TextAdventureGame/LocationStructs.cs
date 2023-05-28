using System;
using Dialogue;
namespace LocationStructs
{
    public class Location
    {
        public int id{get;set;}
        public Location[] GoTo {get;set;}
        public string Description{get;set;}
        public bool LivePenalty {get;set;}
        public string name {get;set;}
        public Location(string d, int i, Location[] l, bool u, string n)
        {
            GoTo = l;
            Description = d;
            id = i;
            LivePenalty = u;
            name = n;
        }
    }
    class Choice
    {
        public string label {get;set;}
	public string name;
    }
    public  class Locations
    {
        public static Location Cave = new Location("You are in a Cave. ", 1, new Location[3], false, "Return");
        public static Location HelpCame = new Location("You wait.................................................You hear someone talking. Another person is answering. ", 6, new Location[1],false, "Wait");
        public static Location OutsideOfCave = new Location("After what seems like an eternity, you can finally see a light in the distance. After your eyes finished adjusting to the life-giving light of the sun, you look around. You can see a Village in the distance and hear the rushing of a river. we didnt have the time to add more story so here you go you won yay ", 5, new Location[2], false, "Escape");
        public static Location BleedingCave = new Location("You start to crawl in a random direction. But you cant see the walls in the Darkness, so you accidentally hurt your hand trying to move around. ", 4, new Location[3],true, "Escape" );
        public static Location InFrontOfVillage = new Location("They dont seem keen on letting you in.", 9,new Location[1],false, "Village");

        public static Location TalkToThem = new Location("You introduce yourself to them, and they take you outside. ", 9,new Location[1],false, "Talk");
	
        public static void SetUpGoTo()
        {
            Cave.GoTo = new Location[3]{HelpCame, BleedingCave, Cave};
            BleedingCave.GoTo = new Location[3]{OutsideOfCave,HelpCame, Cave};
            OutsideOfCave.GoTo = new Location[2]{OutsideOfCave, InFrontOfVillage};
            HelpCame.GoTo = new Location[1]{TalkToThem};
            InFrontOfVillage.GoTo = new Location[1]{Cave};
            TalkToThem.GoTo = new Location[1]{OutsideOfCave};
        }    
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VK
{
    
   
        public class Audio
        {
            public int id { get; set; }
            public int owner_id { get; set; }
            public string artist { get; set; }
            public string title { get; set; }
            public int duration { get; set; }
            public string url { get; set; }
            public string lyrics_id { get; set; }
            public int genre_id { get; set; }
            //id: 232745053,
            //owner_id: 34,
            //artist: 'Ambassadeurs',
            //title: 'Sparks',
            //duration: 274,
            //url: 'http://cs6164.vk....M_lGEJhqRK8d5OQZngI',
            //lyrics_id: 120266970,
            //genre_id: 18
        }
    
}

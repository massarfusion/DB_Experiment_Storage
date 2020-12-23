using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHotel.Models
{
    public class Guest
    {
        String Nickname { get; set; }
        String Realname { get; set; }
        String GuestId { get; set; }
        String LanguageId { get; set; }
        public Guest(string nick, string real, string id, string lang) {
            this.Nickname = nick;
            this.Realname = real;
            this.GuestId = id;
            this.LanguageId = lang;
        }
        public Guest() {

        }
        public String Realn(){
            return this.Realname;
        }
        public string gid() {
            return GuestId;
        }
        public string nickN()
        {
            return Nickname;
        }
        
    }
}

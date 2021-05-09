using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAnnouncer.Models
{
    public class Config
    {
        public double Interval { get; set; }
        public List<AnnouncerMessage> Messages { get; set; }
    }
}

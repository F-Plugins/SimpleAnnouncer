using CitizenFX.Core;
using System.Timers;
using CitizenFX.Core.Native;
using System.IO;
using Newtonsoft.Json;
using SimpleAnnouncer.Models;
using System.Collections.Generic;

namespace SimpleAnnouncer
{
    public class Script : BaseScript
    {
        private Config _Configuration;
        private int index = 0;

        public Script()
        {
            if (!File.Exists($"{API.GetResourcePath("SimpleAnnouncer")}//config.json"))
            {
                File.WriteAllText($"{API.GetResourcePath("SimpleAnnouncer")}//config.json", JsonConvert.SerializeObject(new Config
                {
                    Interval = 5000,
                    Messages = new List<AnnouncerMessage>
                    {
                        new AnnouncerMessage
                        {
                            Author = "^3 [My server] ^7",
                            Message = "Remember to kill your dad"
                        },
                        new AnnouncerMessage
                        {
                            Author = "^5 [AHHHHHH] ^1",
                            Message = "Aliens came here"
                        }
                    }
                }, Formatting.Indented));
            }

            _Configuration = JsonConvert.DeserializeObject<Config>(File.ReadAllText($"{API.GetResourcePath("SimpleAnnouncer")}//config.json"));


            Timer timer = new Timer(_Configuration.Interval);
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if(index >= _Configuration.Messages.Count)
            {
                index = 0;
            }

            TriggerClientEvent("chat:addMessage", new
            {
                args = new [] {_Configuration.Messages[index].Author, _Configuration.Messages[index].Message}
            });

            index++;
        }
    }
}

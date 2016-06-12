using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaApp
{

    interface HowMany
    {
        void countHowMany();
    }

    enum STATE { CONNECTED = 0, DISCONNECTED = 1 };
    public abstract class ConnectionClass : HowMany
    {

        public string username { get; set; }
        public string Status { get; }
        public string password { get; set; }
        public string serverName { get; set; }
        public abstract int getConnectionStatus();
        public abstract string getConnectionInfo();
        public abstract void countHowMany();



    }
}

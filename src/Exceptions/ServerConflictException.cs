using System;

namespace ServerING.Exceptions {
    public class ServerConflictException: Exception {
        public ServerConflictException(int id) : 
            base(string.Format("Server conflict with id = {0}", id)) {}
    }
}

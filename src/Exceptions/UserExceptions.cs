using System;

namespace ServerING.Exceptions
{
    public class UserException : Exception
    {
        public UserException() : base() { }
        public UserException(string message) : base(message) { }
    }
    
    public class UserAlreadyExistsException : UserException
    {
        public UserAlreadyExistsException() : base() { }
        public UserAlreadyExistsException(string message) : base(message) { }
    }

    public class UserNotExistsException : UserException
    {
        public UserNotExistsException() : base() { }
        public UserNotExistsException(string message) : base(message) { }
    }
}
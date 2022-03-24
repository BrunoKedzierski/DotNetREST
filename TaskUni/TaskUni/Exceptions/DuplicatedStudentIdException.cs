using System;

namespace TaskUni.Exceptions
{
    public class DuplicatedStudentIdException : Exception
    {
        public DuplicatedStudentIdException(string message) : base(message)
        {
        }
    }
}

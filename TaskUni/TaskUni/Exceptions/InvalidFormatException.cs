﻿using System;

namespace TaskUni.Exceptions
{
    public class InvalidFormatException : Exception
    {
        public InvalidFormatException(string message) : base(message)
        {
        }
    }
}

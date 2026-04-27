using System;

namespace AnimalSimulation.Events
{
    public class AnimalEventArgs : EventArgs
    {
        public string Message { get; }

        public AnimalEventArgs(string message)
        {
            Message = message;
        }
    }
}
namespace Events
{
    using System;
    using System.Text;

    //// build solution with f6, to install Wintellect.PowerCollections
    using Wintellect.PowerCollections;

    public class Program
    {
        private static readonly StringBuilder Output = new StringBuilder();

        private static readonly EventHolder Events = new EventHolder();

        public static void Main()
        {
            while (ExecuteNextCommand())
            {
                //// empty
            }

            Console.WriteLine(Output);
        }

        private static bool ExecuteNextCommand()
        {
            string command = Console.ReadLine();
            if (string.IsNullOrEmpty(command))
            {
                return false;
            }

            if (command[0] == 'A')
            {
                AddEvent(command);
                return true;
            }

            if (command[0] == 'D')
            {
                DeleteEvents(command);
                return true;
            }

            if (command[0] == 'L')
            {
                ListEvents(command);
                return true;
            }

            return false;
        }

        private static void ListEvents(string command)
        {
            int pipeIndex = command.IndexOf('|');
            DateTime date = GetDate(command, "ListEvents");
            string countString = command.Substring(pipeIndex + 1);
            int count = int.Parse(countString);
            Events.ListEvents(date, count);
        }

        private static void DeleteEvents(string command)
        {
            string title = command.Substring("DeleteEvents".Length + 1);
            Events.DeleteEvents(title);
        }

        private static void AddEvent(string command)
        {
            DateTime date;
            string title, location;
            GetParameters(command, "AddEvent", out date, out title, out location);
            Events.AddEvent(date, title, location);
        }

        private static void GetParameters(string commandForExecution, string commandType, out DateTime dateAndTime, out string eventTitle, out string eventLocation)
        {
            dateAndTime = GetDate(commandForExecution, commandType);
            int firstPipeIndex = commandForExecution.IndexOf('|');
            int lastPipeIndex = commandForExecution.LastIndexOf('|');
            if (firstPipeIndex == lastPipeIndex)
            {
                eventTitle = commandForExecution.Substring(firstPipeIndex + 1).Trim();
                eventLocation = string.Empty;
            }
            else
            {
                eventTitle = commandForExecution
                    .Substring(firstPipeIndex + 1, lastPipeIndex - firstPipeIndex - 1)
                    .Trim();
                eventLocation = commandForExecution.Substring(lastPipeIndex + 1).Trim();
            }
        }

        private static DateTime GetDate(string command, string commandType)
        {
            var date = DateTime.Parse(command.Substring(commandType.Length + 1, 20));
            return date;
        }

        private static class Messages
        {
            public static void EventAdded()
            {
                Output.Append("Event added\n");
            }

            public static void EventDeleted(int x)
            {
                if (x == 0)
                {
                    NoEventsFound();
                }
                else
                {
                    Output.AppendFormat("{0} events deleted\n", x);
                }
            }

            public static void NoEventsFound()
            {
                Output.Append("No events found\n");
            }

            public static void PrintEvent(Event eventToPrint)
            {
                if (eventToPrint != null)
                {
                    Output.Append(eventToPrint + "\n");
                }
            }
        }

        private class EventHolder
        {
            private MultiDictionary<string, Event> ByTitle { get; set; }

            private OrderedBag<Event> ByDate { get; set; }

            public void AddEvent(DateTime date, string title, string location)
            {
                this.ByTitle = new MultiDictionary<string, Event>(true);
                this.ByDate = new OrderedBag<Event>();
                var newEvent = new Event(date, title, location);
                this.ByTitle.Add(title.ToLower(), newEvent);
                this.ByDate.Add(newEvent);
                Messages.EventAdded();
            }

            public void DeleteEvents(string titleToDelete)
            {
                string title = titleToDelete.ToLower();
                int removed = 0;
                foreach (var eventToRemove in this.ByTitle[title])
                {
                    removed++;
                    this.ByDate.Remove(eventToRemove);
                }

                this.ByTitle.Remove(title);
                Messages.EventDeleted(removed);
            }

            public void ListEvents(DateTime date, int count)
            {
                OrderedBag<Event>.View eventsToShow = this.ByDate.RangeFrom(new Event(date, string.Empty, string.Empty), true);
                int showed = 0;
                foreach (var eventToShow in eventsToShow)
                {
                    if (showed == count)
                    {
                        break;
                    }

                    Messages.PrintEvent(eventToShow);

                    showed++;
                }

                if (showed == 0)
                {
                    Messages.NoEventsFound();
                }
            }
        }
    }
}
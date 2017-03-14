namespace Events
{
    using System;
    using System.Text;

    //// build solution with f6, to install Wintellect.PowerCollections

    public class Event : IComparable
    {
        public Event(DateTime date, string title, string location)
        {
            this.Date = date;
            this.Title = title;
            this.Location = location;
        }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public string Location { get; set; }

        public int CompareTo(object obj)
        {
            var other = obj as Event;
            int byDate = this.Date.CompareTo(other.Date);
            int byTitle = this.Title.CompareTo(other.Title);
            int byLocation = this.Location.CompareTo(other.Location);
            if (byDate == 0)
            {
                if (byTitle == 0)
                {
                    return byLocation;
                }

                return byTitle;
            }

            return byDate;
        }

        public override string ToString()
        {
            var toString = new StringBuilder();
            toString.Append(this.Date.ToString("yyyy-MM-ddTHH:mm:ss"));
            toString.Append(" | " + this.Title);
            if (!string.IsNullOrEmpty(this.Location))
            {
                toString.Append(" | " + this.Location);
            }

            return toString.ToString();
        }
    }
}
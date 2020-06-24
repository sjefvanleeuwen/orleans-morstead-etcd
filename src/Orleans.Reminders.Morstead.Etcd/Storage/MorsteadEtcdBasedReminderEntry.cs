using Orleans.Runtime;
using System;

namespace Orleans.Reminders.Morstead.Etcd.Storage
{
    /// <summary>
    /// Data transfer object for a Morstead etcd reminders entry
    /// </summary>
    public class MorsteadEtcdBasedReminderEntry
    {
        //
        // Summary:
        //     The grain reference of the grain that created the reminder. Forms the reminder
        //     primary key together with Orleans.ReminderEntry.ReminderName.
        public GrainReference GrainRef { get; set; }
        //
        // Summary:
        //     The name of the reminder. Forms the reminder primary key together with Orleans.ReminderEntry.GrainRef.
        public string ReminderName { get; set; }
        //
        // Summary:
        //     the time when the reminder was supposed to tick in the first time
        public DateTime StartAt { get; set; }
        //
        // Summary:
        //     the time period for the reminder
        public TimeSpan Period { get; set; }
        public string ETag { get; set; }
    }
}

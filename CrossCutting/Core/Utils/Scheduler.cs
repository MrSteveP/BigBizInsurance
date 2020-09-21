// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Scheduler.cs" company="Usama Nada">
//   No Copyright .. Copy, Share, and Evolve.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CrossCutting.Core.Utils
{
    #region usings

    using NCrontab;
    using System;

    #endregion

    /// <summary>
    ///     The scheduler.
    /// </summary>
    public static class Scheduler
    {
        // This uses something called CRON expression: https://en.wikipedia.org/wiki/Cron#CRON_expression
        // THis site might help: http://shawnchin.github.io/jquery-cron/
        /*
        @yearly     (or @annually) Run once a year at midnight of 1 January         0 0 1 1 * 
        @monthly    Run once a month at midnight of the first day of the month      0 0 1 * * 
        @weekly     Run once a week at midnight on Sunday morning                   0 0 * * 0 
        @daily      Run once a day at midnight                                      0 0 * * * 
        @hourly     Run once an hour at the beginning of the hour                   0 * * * * 
        @reboot     Run at startup N/A 

         # ┌───────────── min (0 - 59)
         # │ ┌────────────── hour (0 - 23)
         # │ │ ┌─────────────── day of month (1 - 31)
         # │ │ │ ┌──────────────── month (1 - 12)
         # │ │ │ │ ┌───────────────── day of week (0 - 6) (0 to 6 are Sunday to
         # │ │ │ │ │                  Saturday, or use names; 7 is also Sunday)
         # │ │ │ │ │
         # │ │ │ │ │
         # * * * * *  command to execute
         ========================================
         ========================================
         # Kafalah Committee will meeting every Wednesday on 2:00 PM                0 14 * * 3
         ========================================
         ========================================
        */

        /// <summary>
        /// The get next occurrence.
        /// </summary>
        /// <param name="cronExpression">
        /// The cron expression.
        ///     "0 14 * * 3";  //every Wednesday on 2:00 PM
        /// </param>
        /// <param name="baseTime">
        /// The base Time.
        /// </param>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        public static DateTime GetNextOccurrence(string cronExpression, DateTime? baseTime = null)
        {
            var schedule = CrontabSchedule.TryParse(cronExpression);
            baseTime = baseTime ?? DateTime.Now.AddMinutes(1);
            var meetingTime = schedule.GetNextOccurrence(baseTime.Value);
            return meetingTime;
        }
    }
}
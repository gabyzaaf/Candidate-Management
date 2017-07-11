using System;

namespace Candidate_Management.CORE.Remind
{
    public interface Iremind
    {    

        /// <summary>
        /// add the remind inside the database
        /// </summary>
        /// <param name="id">Candiate Id </param>
        /// <param name="date">Current Date</param>
         void add(int id,DateTime date);
         /// <summary>
         /// Update the remind job inside the Database.
         /// </summary>
         /// <param name="id"></param>
         /// <param name="date"></param>
         void update(int id,DateTime date);
         /// <summary>
         /// Execute the command out of the System with AT command.
         /// </summary>
         /// <param name="token"></param>
         /// <param name="meeting"></param>
         void exec(string token,DateTime meeting);
    }
}
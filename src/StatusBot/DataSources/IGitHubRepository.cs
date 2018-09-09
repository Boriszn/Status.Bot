using System.Threading.Tasks;

namespace StatusBot.DataSources
{
    public interface IGitHubRepository
    {
        /// <summary>
        /// Gets the user information.
        /// </summary>
        /// <returns>User info string</returns>
        Task<string> GetUserInfo();

        /// <summary>
        /// Gets the current user issuesinfo string.
        /// </summary>
        /// <returns></returns>
        Task<string> GetCurrentUserIssuesInfoString();

        /// <summary>
        /// Gets the user issuesinfo string.
        /// </summary>
        /// <returns></returns>
        Task<string> GetUserIssuesInfoString();
    }
}
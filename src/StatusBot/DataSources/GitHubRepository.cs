using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Octokit;
using StatusBot.Configuration;
using StatusBot.Constants;

namespace StatusBot.DataSources
{
    public class GitHubRepository : IGitHubRepository
    {
        private readonly GitHubClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="GitHubRepository"/> class.
        /// </summary>
        public GitHubRepository(IOptions<AuthenticationOption> authenticationOption)
        {
            client = new GitHubClient(new ProductHeaderValue(authenticationOption.Value.ProductHeaderValue));

            var basicAuth = new Credentials(authenticationOption.Value.ApiToken);
            client.Credentials = basicAuth;
        }

        /// <summary>
        /// Gets the user information.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetUserInfo()
        {
            User user = await client.User.Current();

            return $"{user.Name} has {user.PublicRepos} public repositories, Has {user.Followers} Followers and " +
                   $" folowing {user.Following} - Blog URL {user.Blog} - GitHub URL: {user.HtmlUrl}. Has {user.PublicGists} public gists";
        }

        /// <summary>
        /// Gets the user information.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetCurrentUserIssuesInfoString()
        {
            string issuesString = string.Empty;

            var recently = new IssueRequest
            {
                Filter = IssueFilter.All,
                State = ItemStateFilter.All,
                Since = DateTimeOffset.Now.Subtract(TimeSpan.FromDays(GeneralConstants.IssuesByDays))
            };

            IReadOnlyList<Issue> issues = await client.Issue.GetAllForCurrent(recently);

            return issues.Aggregate(issuesString, (current, issue) => 
            current + $" | Title: {issue.Title} , Comments: {issue.Comments} , " +
            $"Issue URL: {issue.HtmlUrl}, Created: {issue.CreatedAt}, Status: {issue.State} \n\n\n\n");
        }

        /// <summary>
        /// Gets the user information.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetUserIssuesInfoString()
        {
            string issuesString = string.Empty;

            var recently = new IssueRequest
            {
                Filter = IssueFilter.All,
                State = ItemStateFilter.All,
                Since = DateTimeOffset.Now.Subtract(TimeSpan.FromDays(GeneralConstants.IssuesByDays))
            };

            IReadOnlyList<Issue> issues = await client.Issue.GetAllForOwnedAndMemberRepositories(recently);

            return issues.Aggregate(issuesString, (current, issue) =>
                current + $" | Title: {issue.Title} , Comments: {issue.Comments} , Issue URL: {issue.HtmlUrl} " +
                $"Created: {issue.CreatedAt}, Status: {issue.State} \n\n\n\n");
        }
    }
}

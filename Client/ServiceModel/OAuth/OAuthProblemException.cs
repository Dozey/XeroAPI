using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XeroAPI.Client.ServiceModel.OAuth
{
    public class OAuthProblemException : Exception
    {
        public OAuthProblemException(string problem, string problemAdvice) : base(problemAdvice)
        {
            Problem = problem;
            ProblemAdvice = problemAdvice;
        }

        public string Problem { get; private set; }
        public string ProblemAdvice { get; private set; }
    }
}

﻿using System.Linq;
using Raven.Client.Documents.Indexes;
using TestingEnvironment.Common;
using TestingEnvironment.Common.OrchestratorReporting;

namespace TestingEnvironment.Client
{
    public class FailTests : AbstractIndexCreationTask<TestInfo>
    {
        public FailTests()
        {
            Map = tests => from test in tests
                           where (test.Events.All(x => x.Type != EventInfoWithExceptionAsString.EventType.TestSuccess) ||
                                  test.Events.Any(x => x.Type == EventInfoWithExceptionAsString.EventType.TestFailure))
                           select new
                           {
                               test.Id,
                               test.Start,
                               test.Name,
                               test.Round,
                               test.Config,
                               test.Author,
                               test.ExtendedName,
                               test.TestClassName,
                               test.End,
                               test.Events
                           };
        }
    }
}

using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;

namespace CognitiveLocator.Functions
{
    public static class DocumentCleaner
    {
        [FunctionName("DocumentCleaner")]
        public static void Run(
            [CosmosDBTrigger("CognitiveLocator", "Person", ConnectionStringSetting = "PersonTrigger_ConnectionString")]IReadOnlyList<Document> input,
        //   // [QueueTrigger("myqueue-items", Connection = "PersonTrigger_ConnectionString")] string myQueueItem,
        //    [DocumentDB("CognitiveLocator", "Person", SqlQuery = "SELECT * FROM Person p where p._pending_to_be_deleted=true", ConnectionStringSetting = "PersonTrigger_ConnectionString")] out dynamic document,
            TraceWriter log)
        {
            log.Verbose("Document count " + input.Count);
        }
    }
}
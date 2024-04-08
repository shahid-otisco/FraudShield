using FraudShield.Models;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework.Internal;

namespace FraudShield.Test
{
    public class TransactionMonitorOrchestratorTests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void TestValidTransaction()
        {
            // Arrange
            var mockContext = new Mock<IDurableOrchestrationContext>();
            var mockLogger = new Mock<ILogger>();

            var jsonData = @"{
                ""correlationId"": ""0EC1D320-3FDD-43A0-84B8-3CF8972CDCD8"",
                ""tenantId"": ""345"",
                ""transactionId"": ""eyJpZCI6ImE2NDUzYTZlLTk1NjYtNDFmOC05ZjAzLTg3ZDVmMWQ3YTgxNSIsImlzIjoiU3RhcmxpbmciLCJydCI6InBheW1lbnQifQ"",
                ""transactionDate"": ""2024-02-15 11:36:22"",
                ""direction"": ""Credit"",
                ""amount"": ""345.87"",
                ""currency"": ""EUR"",
                ""description"": ""Mr C A Woods"",
                ""sourceaccount"": {
                    ""accountno"": ""44421232"",
                    ""sortcode"": ""30-23-20"",
                    ""countrycode"": ""GBR""
                },
                ""destinationaccount"": {
                    ""accountno"": ""87285552"",
                    ""sortcode"": ""10-33-12"",
                    ""countrycode"": ""HKG""
                }
            }";

            var transactionEvent = JsonConvert.DeserializeObject<TransactionEvent>(jsonData);

            var serializedTransactionEvent = JsonConvert.SerializeObject(transactionEvent);
            mockContext.Setup(c => c.GetInput<string>()).Returns(serializedTransactionEvent);

            //TransactionMonitorOrchestrator.RunOrchestrator(mockContext.Object).GetAwaiter().GetResult();

            // Assert
            // Verify that the payment is sent to processing queue
        }
    }

}

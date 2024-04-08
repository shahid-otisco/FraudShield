
Thsi is a test microservice-based fraud detection system designed to monitor transactions for a financial system. It utilizes Azure Durable Functions, Azure Service Bus, and other Azure services for event-driven processing and detection of potentially fraudulent activities.

1. TransactionMonitorOrchestrator (Azure Function): This Azure Function orchestrates the processing of transaction events. It assesses incoming transaction messages against tenant-specific settings, including velocity limits, payment thresholds, and sanctions. If a transaction violates any restrictions, it raises an event and sends the payment to a holding queue for assessment.

2. TransactionValidator (Class Library): Contains logic for validating transaction events based on tenant settings. It checks velocity limits, payment thresholds, and sanctions to determine if a transaction is allowed.

3. TenantSettings (Class Library): Defines data structures for representing tenant-specific settings, including velocity limits, payment thresholds, and sanctions.

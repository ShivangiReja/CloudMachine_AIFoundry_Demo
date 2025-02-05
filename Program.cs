using Azure.CloudMachine;
using Azure.CloudMachine.AIFoundry;
using Azure.CloudMachine.OpenAI;

// ------------- Demo - 1 -----------------------------------

var connectionString = Environment.GetEnvironmentVariable("PROJECT_CONNECTION_STRING");
AIFoundryClient client = new(connectionString);

var openAIClient = client.GetOpenAIChatClient("gpt-4o-mini");
var result = openAIClient.CompleteChat("Tell me the most luxurious handbag brand");
Console.WriteLine(result.AsText());

var agentClient = client.GetAgentsClient();
var searchClient = client.GetSearchClient("index");
var chatCompletionsClient = client.GetChatCompletionsClient(); // Inference chat client
var embeddClient = client.GetEmbeddingsClient(); // Inference embeddings client

// -------------- Demo - 2 ----------------------------------

ProjectInfrastructure infrastructure = new();

infrastructure.AddFeature(new AIFoundryFeature(connectionString));
if (infrastructure.TryExecuteCommand(args)) return;

var projectClient = infrastructure.GetClient();
var chatClient = projectClient.GetOpenAIChatClient("gpt-4o-mini");
Console.WriteLine(chatClient.CompleteChat("List all the rainbow colors").AsText());
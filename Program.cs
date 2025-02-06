using Azure.AI.Inference;
using Azure.AI.Projects;
using Azure.CloudMachine;
using Azure.CloudMachine.AIFoundry;
using Azure.CloudMachine.KeyVault;
using Azure.CloudMachine.OpenAI;
using Azure.Search.Documents;
using Azure.Security.KeyVault.Secrets;
using OpenAI.Chat;

// ------------- Demo - 1 -----------------------------------

var connectionString = Environment.GetEnvironmentVariable("PROJECT_CONNECTION_STRING");
AIFoundryClient client = new(connectionString);

ChatClient openAIClient = client.GetOpenAIChatClient("gpt-4o-mini");
Console.WriteLine(openAIClient.CompleteChat("Which is the largest continent?").AsText());

AgentsClient agentClient = client.GetAgentsClient();
SearchClient searchClient = client.GetSearchClient("index");
ChatCompletionsClient chatCompletionsClient = client.GetChatCompletionsClient(); // Inference chat client
EmbeddingsClient embeddingsClient = client.GetEmbeddingsClient(); // Inference embeddings client


// -------------- Demo - 2 ----------------------------------


ProjectInfrastructure infrastructure = new();

infrastructure.AddFeature(new AIFoundryFeature(connectionString));
infrastructure.AddFeature(new KeyVaultFeature());
if (infrastructure.TryExecuteCommand(args)) return;

ProjectClient projectClient = infrastructure.GetClient();

ChatClient chatClient = projectClient.GetOpenAIChatClient("gpt-4o-mini");
Console.WriteLine(chatClient.CompleteChat("List all the rainbow colors").AsText());

SecretClient secretClient = projectClient.GetKeyVaultSecretsClient();
KeyVaultSecret secret = secretClient.SetSecret("mysecret", "please don't tell anyone");
Console.WriteLine($"Shhh it's a secret: {secret.Name}: {secret.Value}");
using System.Text.Json.Serialization;

namespace TagzApp.Providers.DiscordChat;

public class DiscordChatConfiguration : IProviderConfiguration
{
    [JsonPropertyOrder(1)]
    public string? BotName { get; set; }
    [JsonPropertyOrder(2)]
    public string? channelId { get; set; } //needs to be converted to ulong to be used in the discord client
    [JsonPropertyOrder(3)]
    public string? OAuthToken { get; set; }
		[JsonPropertyOrder(4)]
		public string CommandPrefix { get; set; } = "!";
		[JsonPropertyOrder(5)]
	  public string Command { get; set; } = "tagz";
    
    public static DiscordChatConfiguration Empty => new()
    {
      BotName = string.Empty,
      channelId = string.Empty,
      OAuthToken = string.Empty,
			CommandPrefix = string.Empty,
			Command = string.Empty
    };

    public string Name => "DiscordChat";
    public string Description => "Read all messages from a specified Discord channel";
    public bool Enabled { get; set; }
    public string[] Keys => ["BotName", "channelId", "OAuthToken","CommandPrefix","Command"];

    public string GetConfigurationByKey(string key)
    {

		return key switch
        {
            "BotName" => BotName,
            "channelId" => channelId,
            "OAuthToken" => OAuthToken,
						"CommandPrefix" => CommandPrefix,
						"Command" => Command,
            "Enabled" => Enabled.ToString(),
            _ => string.Empty
        };

	}

    public void SetConfigurationByKey(string key, string value)
    {
        switch (key)
        {
            case "BotName":
                BotName = value;
                break;
            case "channelId":
                channelId = value;
                break;
            case "OAuthToken":
                OAuthToken = value;
                break;
						case "CommandPrefix":
								CommandPrefix = value;
								break;
						case "Command":
								Command = value;
								break;
            default:
                throw new NotImplementedException($"Unable to set value for key {key} in DiscordChatConfiguration.");    
        }
    }


}

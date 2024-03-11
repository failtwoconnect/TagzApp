using System.Text.Json.Serialization;

namespace TagzApp.Providers.Discord;

public class DiscordChatConfiguration : IProviderConfiguration
{
    [JsonPropertyOrder(1)]
    public string BotName { get; set; }
    [JsonPropertyOrder(2)]
    public string ChannelId { get; set; } //needs to be converted to ulong to be used in the discord client
    [JsonPropertyOrder(3)]
    public string OAuthToken { get; set; }
		public string CommandPrefix { get; set; } = "!";
	  public string Command { get; set; } = "tagz";
    
    public static DiscordChatConfiguration Empty => new()
    {
      BotName = string.Empty,
      ChannelId = string.Empty,
      OAuthToken = string.Empty,
			CommandPrefix = string.Empty,
			Command = string.Empty
    };

    public string Name => "DiscordChat";
    public string Description => "Read all messages from a specified Discord channel";
    public bool Enabled { get; set; }
    public string[] Keys => ["BotName", "channelId", "OAuthToken"];

		public string GetConfigurationByKey(string key)
		{
			
		}
    public void SetConfigurationByKey(string key, string value)
    {
        switch (key)
        {
            case "BotName":
                BotName = value;
                break;
            case "channelId":
                ChannelId = value;
                break;
            case "OAuthToken":
                OAuthToken = value;
                break;
            default:
                throw new NotImplementedException($"Unable to set value for key {key} in DiscordChatConfiguration.");    
        }
    }


}

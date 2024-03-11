using DSharpPlus;
using Microsoft.Extensions.Logging;

namespace TagzApp.Providers.DiscordChat;

public class DiscordChatProvider : ISocialMediaProvider, IDisposable
{
	public string Id => "DISCORD";

	public string DisplayName => "DiscordChat";

	public string Description {get; init;} = "Discord is a voice, video and text communication service to talk and hang out with your friends and communities.";

	public bool Enabled { get; }
    private string _StatusMessage = "Not started";
	public TimeSpan NewContentRetrievalFrequency => TimeSpan.FromSeconds(1);

    private DiscordChatConfiguration _Settings;
    private readonly ILogger<DiscordChatProvider> _Logger;
    private DiscordClient _Client;
    private SocialMediaStatus _Status = SocialMediaStatus.Unhealthy;
	public void Dispose()
	{
		_Client.Dispose();
	}

	public Task<IProviderConfiguration> GetConfiguration(IConfigureTagzApp configure)
	{
		return Task.FromResult<IProviderConfiguration>(_Settings);
	}

	public Task<(SocialMediaStatus Status, string Message)> GetHealth()
	{
		return Task.FromResult((_Status, _StatusMessage));
	}

	public Task SaveConfiguration(IConfigureTagzApp configure, IProviderConfiguration providerConfiguration)
	{
		_Settings = (DiscordChatConfiguration)providerConfiguration;
        return Task.CompletedTask;
	}

	public Task StartAsync()
	{
		if(string.IsNullOrEmpty(_Settings.OAuthToken))
        {
            _Status = SocialMediaStatus.Unhealthy;
            _Logger.LogError("Discord token is not set");
            return Task.CompletedTask;
        }
        _Client.ConnectAsync();
        return Task.CompletedTask;
	}

	public Task StopAsync()
	{
		_Client.DisconnectAsync();
        return Task.CompletedTask;
	}

    private async Task ListenForMessages()
    {
        _Client = new DiscordClient(new DiscordConfiguration
        {
            Token = _Settings.OAuthToken,
            TokenType = TokenType.Bot
        });

        _Client.MessageCreated += async (s,e) =>
        {
            if (!e.Author.IsBot && e.Channel.Id.ToString().Equals(_Settings.channelId))
                await e.Message.RespondAsync("pong!");
        };

        await _Client.ConnectAsync();
    }

	public Task<IEnumerable<Content>> GetContentForHashtag(Hashtag tag, DateTimeOffset since)
	{
		throw new NotImplementedException();
	}
}

using Microsoft.Extensions.DependencyInjection;
using TagzApp.Communication.Configuration;
using TagzApp.Communication.Extensions;

namespace TagzApp.Providers.DiscordChat;
public class StartDiscordChat : IConfigureProvider
{
	private const string ConfigurationKey = "providers:discordchat";
	private const string _DisplayName = "DiscordChat";
	private DiscordChatConfiguration? _DiscordChatConfiguration;

	public async Task<IServiceCollection> RegisterServices(IServiceCollection services, CancellationToken cancellationToken = default)
	{

		_DiscordChatConfiguration = await ConfigureTagzAppFactory.Current.GetConfigurationById<DiscordChatConfiguration>(ConfigurationKey);

		services.AddSingleton(_DiscordChatConfiguration ?? DiscordChatConfiguration.Empty);
		services.AddHttpClient<ISocialMediaProvider, DiscordChatProvider, HttpClientOptions>(new());
		services.AddSingleton<ISocialMediaProvider, DiscordChatProvider>();

		return services;
	}
}


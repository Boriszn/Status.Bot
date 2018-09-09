# Status.Bot

Bot interface allows to send and receive statuses about GIT repositories and Pull Requests to Skype, Facebook, etc.

## Installation

1. Clone repository
2. Create Bot Service in Azure. Instructions can be found in [Wiki page](https://github.com/Boriszn/Status.Bot/wiki).
3. Update in `appsettings.json` your _MicrosoftAppId_ and _MicrosoftAppPassword_
4. Build Or Run project
5. Testing. Download and Run [Bot Emulator](https://github.com/Microsoft/BotFramework-Emulator). Follow instructions for [local development](https://github.com/Microsoft/BotFramework-Emulator/wiki/Getting-Started#connect-to-a-bot-running-on-localhost). Example below:

![alt text](https://raw.githubusercontent.com/Boriszn/Status.Bot/feature/add-github-integration/assets/img/BotLocalEmulator.png "Logo Title Text 1")

6.Deploy to azure

## Dependencies info

Current solution dependent to project which was retrieved from [Microsoft BotBuilder library. (_NetCore2_ branch)](https://github.com/Microsoft/BotBuilder/tree/NetCore2)

## Integrations

* *Telegramm* - integrated/worked
* *Facebook* - currently impossible (Facebook closed access to the development API)
* *Skype* - In progress

## Contributing

1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request

## History

All changes can be easily found in [RELEASENOTES](ReleaseNotes.md)

## License

This project is licensed under the MIT License

This is a sample bot app that demonstrates Panviva's API and the Microsoft Bot Framework.  It uses Panviva's Natural Language Search API available at https://dev.panviva.com.  The Microsoft Bot Framework is documented at https://dev.botframework.com.

[![Build Status](https://cadanz.visualstudio.com/PanvivaBot/_apis/build/status/panvivabot20181114021140%20-%20CI)](https://cadanz.visualstudio.com/PanvivaBot/_build/latest?definitionId=17)

## Environment Variables
The app requires three environment variables to be set:
* Ocp-Apim-Subscription-Key - your API subscription key from Panviva's API
* PanvivaAPIEndpoint - your Panviva API endpoint - eg api.panviva.com/v3/demo-orchestrator
* botFileSecret - a key used to encrypt the BotConfiguration.bot settings file
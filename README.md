# Simple SignalR Chat Application
Sample live support chat application, based on .Net 6 and SignalR.

## Getting Started
Make sure you have installed .Net 6 in your enviroment. After that, you can run the below command from the **/src/** directory an get started the application immediately.
```
dotnet run
```

## Basic scenario
On the main page, a dialog box opens for chatting with the support, through which the user can send messages.
If the user opens several different tabs in a browser or even if he closes and reopens the browser, he can see his active messages for a certain period of time.        	
If the user's network connection is interrupted or slowed down, the program will try to reconnect and the messages will not be lost.

A page has also been created for the support agent so that it can respond to the users's messages.
This page can be accessed through the **/agent** address with the any username and password: **password**

## Technical Review
Cookies are used to tracking anonymouse users and keep the history of messages
An interface for persisting and retrieving the history of messages has been created, which is implemented as a In-memory for the demo purpose.

The following guide has been used to understanding lifetime events in SignalR and disconnection and reconnection scenarios.
[Understanding and Handling Connection Lifetime Events in SignalR](https://docs.microsoft.com/en-us/aspnet/signalr/overview/guide-to-the-api/handling-connection-lifetime-events/)

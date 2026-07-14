# Event sorter
Find events in your area and sort them by date, location, and availability.

## Assumptions

This program assumes the user is in the United States

## Setup
You must modify the config.json file in the root directory of the project.


## Required APIs

### Google Places API
https://developers.google.com/maps/billing-and-pricing/pricing
Requires you to setup billing, You stay within the free tier if you limit requests to 10,000 / month

### Setup an API key with your google calendar
https://console.cloud.google.com/?pli=1


## My events arent showing summary / description / location

### Your Sharing Dropdown is restricted
If all your events are showing up as "Untitled Event" even though they have clear titles on your personal screen, it means the Google Calendar API is intentionally redacting the text before it reaches your code.

Because you are using a standard API Key on a public calendar link, Google applies a strict privacy filter to the payload based on your sharing settings:

When you made the calendar public, the permission dropdown in your Google Calendar settings was likely left on the default: "See only free/busy (hide details)".

When this happens:
* Google considers the existence of the event public (so your code successfully finds a block of time).
* Google considers the details (like the Title, Description, and Location) strictly private.
* To protect your privacy, the API scrubs the Summary field completely, which causes your C# code to fall back to "Untitled Event".



### Setup API key with serp api
https://serpapi.com/dashboard
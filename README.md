# MoreAuctions

A web application to create Auctions and lot items for auction.

## Getting Started

Run MoreAuction project in visual studio to create local database.

### Prerequisites

Visual Studio 2017 or VS Code
.Net Core 2.0
A local smtp server - e.g [papercut](https://github.com/ChangemakerStudios/Papercut/releases)

### Configuration

The following can be configured

```
PriceWarning: Warning on lot item maximum start price - current value of 100

MaxAuctionItems: Maximum lot items per auction - currenlty set to 3 for development, 10 for other
```

SMTP Settings

```
SmtpServer

SmtpPort

User

Pwd

SourceEmailAddress

DestinationEmailAddress
```


## Assumptions

Auction description must be unique

Lot Item title must be unique within each auction

Lot item start price cannot be negative

## To Do

Use a repository in controllers, thus removing dbContext dependancy.

Use JQuery datetime picker for date entry, because html5 `<input type="datetime-local" />` is a bad user experience.

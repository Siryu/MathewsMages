CREATE TABLE [dbo].[Users] (
    [UserName]           VARCHAR (50) NOT NULL,
    [CurrentHealth]      INT          NOT NULL,
    [MonstersKilled]     INT          NOT NULL,
    [RoomsTraveled]      INT          NOT NULL,
    [Attack]             INT          DEFAULT ((1)) NOT NULL,
    [Defense]            INT          DEFAULT ((1)) NOT NULL,
    [AttackSpeed]        INT          DEFAULT ((1)) NOT NULL,
    [AttackRange]        INT          DEFAULT ((1)) NOT NULL,
    [MoveSpeed]          INT          DEFAULT ((1)) NOT NULL,
    [HighMonstersKilled] INT          DEFAULT ((0)) NOT NULL,
    [HighRoomsTraveled]  INT          DEFAULT ((0)) NOT NULL,
    [HighAttack]         INT          DEFAULT ((0)) NOT NULL,
    [HighDefense]        INT          DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([UserName] ASC)
);

CREATE TABLE [dbo].[Roles] (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (40) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[LogUsers] (
    [UserName] VARCHAR (50)  NOT NULL,
    [Password] VARCHAR (500) NOT NULL,
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[UserRoles] (
    [LogUserId] INT NOT NULL,
    [RoleId]    INT NOT NULL,
    PRIMARY KEY CLUSTERED ([LogUserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_LogUserId] FOREIGN KEY ([LogUserId]) REFERENCES [dbo].[LogUsers] ([Id]),
    CONSTRAINT [FK_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id])
);

CREATE TABLE [dbo].[Message] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Sender]    VARCHAR (50)  NOT NULL,
    [Receiver]  VARCHAR (50)  NOT NULL,
    [Message]   VARCHAR (MAX) NOT NULL,
    [TimeStamp] ROWVERSION    NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Message_Sender] FOREIGN KEY ([Sender]) REFERENCES [dbo].[Users] ([UserName]),
    CONSTRAINT [FK_Message_Receiver] FOREIGN KEY ([Receiver]) REFERENCES [dbo].[Users] ([UserName])
);

CREATE TABLE [dbo].[Friend] (
    [User]   VARCHAR (50) NOT NULL,
    [Friend] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([User] ASC, [Friend] ASC),
    CONSTRAINT [FK_Friend_User] FOREIGN KEY ([User]) REFERENCES [dbo].[Users] ([UserName]),
    CONSTRAINT [FK_Friend_Friend] FOREIGN KEY ([Friend]) REFERENCES [dbo].[Users] ([UserName])
);

CREATE TABLE [dbo].[FriendRequest] (
    [Sender]   VARCHAR (50) NOT NULL,
    [Receiver] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Receiver] ASC, [Sender] ASC),
    CONSTRAINT [FK_FriendRequest_Sender] FOREIGN KEY ([Sender]) REFERENCES [dbo].[Users] ([UserName]),
    CONSTRAINT [FK_FriendRequest_Reciever] FOREIGN KEY ([Receiver]) REFERENCES [dbo].[Users] ([UserName])
);

GO
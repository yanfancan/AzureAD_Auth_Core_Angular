CREATE TABLE [dbo].[Sessions] (
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    [Status]              INT           NOT NULL,
    [StartDateTime]       DATETIME      CONSTRAINT [DF_Sessions_StartDateTime] DEFAULT (getutcdate()) NOT NULL,
    [EndDateTime]         DATETIME      NULL,
    [ServiceAccountId]    INT           NOT NULL,
    [LastRequestDateTime] DATETIME      CONSTRAINT [DF_Sessions_LastRequestDateTime] DEFAULT (getutcdate()) NOT NULL,
    [ClientIp]            VARCHAR (50)  NOT NULL,
    [AddedBy]             VARCHAR (150) NOT NULL,
    [ModifiedBy]          VARCHAR (150) NULL,
    [ModifiedDateTime]    DATETIME      NULL,
    CONSTRAINT [PK_Sessions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Sessions_Users] FOREIGN KEY ([ServiceAccountId]) REFERENCES [dbo].[ServiceAccounts] ([Id])
);
















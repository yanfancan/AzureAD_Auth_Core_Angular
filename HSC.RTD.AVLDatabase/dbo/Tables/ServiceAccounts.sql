CREATE TABLE [dbo].[ServiceAccounts] (
    [Id]               INT               IDENTITY (1, 1) NOT NULL,
    [LoginName]        VARCHAR (15)      NOT NULL,
    [Password]         VARCHAR (50)      NOT NULL,
    [Name]             VARCHAR (250)     NULL,
    [Roles]            INT               NOT NULL,
    [Status]           INT               NOT NULL,
    [TimeZone]         VARCHAR (150)     NOT NULL,
    [GeoZone]          [sys].[geography] NULL,
    [AddedDateTime]    DATETIME          CONSTRAINT [DF_ServiceAccounts_AddedDateTime] DEFAULT (getutcdate()) NOT NULL,
    [AddedBy]          VARCHAR (150)     NOT NULL,
    [ModifiedDateTime] DATETIME          NULL,
    [ModifiedBy]       VARCHAR (150)     NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [IX_Users] UNIQUE NONCLUSTERED ([LoginName] ASC)
);










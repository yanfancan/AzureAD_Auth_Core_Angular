CREATE TABLE [dbo].[ServiceAccounts_Services] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [ServiceAccountId] INT           NOT NULL,
    [ServiceId]        INT           NOT NULL,
    [AddedDateTime]    DATETIME      CONSTRAINT [DF_User_Service_AddedDateTime] DEFAULT (getutcdate()) NOT NULL,
    [AddedBy]          VARCHAR (150) NOT NULL,
    CONSTRAINT [PK_User_Service] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_User_EmsService_Services] FOREIGN KEY ([ServiceId]) REFERENCES [dbo].[Services] ([Id]),
    CONSTRAINT [FK_User_EmsService_Users] FOREIGN KEY ([ServiceAccountId]) REFERENCES [dbo].[ServiceAccounts] ([Id])
);






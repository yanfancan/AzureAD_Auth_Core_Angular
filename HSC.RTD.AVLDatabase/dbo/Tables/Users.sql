CREATE TABLE [dbo].[Users] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]        VARCHAR (150) NULL,
    [LastName]         VARCHAR (150) NULL,
    [Department]       VARCHAR (150) NULL,
    [Email]            VARCHAR (250) NOT NULL,
    [Position]         VARCHAR (50)  NULL,
    [AccessLevel]      INT           NOT NULL,
    [AccountLocked]    BIT           NOT NULL,
    [AccountDisabled]  BIT           NOT NULL,
    [Password]         VARCHAR (150) NULL,
    [AddedBy]          VARCHAR (150) NOT NULL,
    [AddedDateTime]    DATETIME      CONSTRAINT [DF_Users_AddedDateTime] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]       VARCHAR (150) NULL,
    [ModifiedDateTime] DATETIME      NULL,
    CONSTRAINT [email_unique] UNIQUE NONCLUSTERED ([Email] ASC)
);






CREATE TABLE [dbo].[Consumers] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [Name]            VARCHAR (150) NOT NULL,
    [Type]            INT           NOT NULL,
    [AddedBy]         VARCHAR (150) NOT NULL,
    [AddedDateTime]   DATETIME      CONSTRAINT [DF_Consumers_AddedDateTime] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]      VARCHAR (150) NULL,
    [ModifieDateTime] DATETIME      NULL,
    CONSTRAINT [PK_Consumers] PRIMARY KEY CLUSTERED ([Id] ASC)
);


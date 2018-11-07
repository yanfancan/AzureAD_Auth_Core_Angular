CREATE TABLE [dbo].[Services] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [ServiceName]   VARCHAR (150) NOT NULL,
    [Description]   VARCHAR (500) NULL,
    [AddedDateTime] DATETIME      CONSTRAINT [DF_Services_AddedDateTime] DEFAULT (getutcdate()) NOT NULL,
    [AddedBy]       VARCHAR (150) NOT NULL,
    CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED ([Id] ASC)
);




CREATE TABLE [dbo].[AvlConfiguration] (
    [ConfigurationId]     INT            IDENTITY (1, 1) NOT NULL,
    [ConfigurationKey]    NVARCHAR (50)  NOT NULL,
    [ConfigurationValue]  VARCHAR (150)  NOT NULL,
    [Description]         VARCHAR (250)  NULL,
    [LastChangedDateTime] DATETIME       CONSTRAINT [DF_AvlConfiguration_LastChangedDateTime] DEFAULT (getutcdate()) NOT NULL,
    [Component]           NVARCHAR (50)  NULL,
    [ComponentName]       NVARCHAR (150) NULL,
    CONSTRAINT [PK_CIPConfiguration] PRIMARY KEY CLUSTERED ([ConfigurationId] ASC)
);




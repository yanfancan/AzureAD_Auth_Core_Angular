CREATE TABLE [dbo].[DeviceStats] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [Address]            VARCHAR (150) NOT NULL,
    [DeviceId]           INT           NULL,
    [LastReportDateTime] DATETIME      NULL,
    [LastUpdateDateTime] DATETIME      NULL,
    CONSTRAINT [PK_PositionsStats] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DeviceStats_Positions] FOREIGN KEY ([Address]) REFERENCES [dbo].[Positions] ([Address])
);




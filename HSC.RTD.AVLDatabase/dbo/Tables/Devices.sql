CREATE TABLE [dbo].[Devices] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Address]       VARCHAR (150) NOT NULL,
    [ServiceId]     INT           NULL,
    [Description]   VARCHAR (500) NULL,
    [DeviceType]    INT           NOT NULL,
    [VehicleName]   VARCHAR (50)  NOT NULL,
    [VehicleId]     VARCHAR (50)  NOT NULL,
    [AddedDateTime] DATETIME      CONSTRAINT [DF_Devices_AddedDateTime] DEFAULT (getutcdate()) NOT NULL,
    [AddedBy]       VARCHAR (150) NOT NULL,
    CONSTRAINT [PK_Devices] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Vehicles_EmsServices] FOREIGN KEY ([ServiceId]) REFERENCES [dbo].[Services] ([Id]),
    CONSTRAINT [IX_Devices] UNIQUE NONCLUSTERED ([Address] ASC)
);






CREATE TABLE [dbo].[Positions] (
    [Id]               INT               IDENTITY (1, 1) NOT NULL,
    [Address]          VARCHAR (150)     NOT NULL,
    [Latitude]         DECIMAL (18, 6)   NOT NULL,
    [Longitude]        DECIMAL (18, 6)   NOT NULL,
    [Velocity]         INT               NULL,
    [Direction]        INT               NULL,
    [Point]            [sys].[geography] NULL,
    [AvlDateTime]      DATETIME          NOT NULL,
    [ModifiedDateTime] DATETIME          CONSTRAINT [DF_Reports_AddedDateTime] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]       VARCHAR (150)     NOT NULL,
    CONSTRAINT [PK_Reports] PRIMARY KEY CLUSTERED ([Id] ASC)
);










GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Positions]
    ON [dbo].[Positions]([Address] ASC);


GO
CREATE TRIGGER [dbo].[INSTEADOF_TR_Positions_Insert] 
   ON [dbo].[Positions] 
   INSTEAD OF INSERT
AS 
BEGIN
	SET NOCOUNT ON;
declare @timeNow datetime = getutcdate();
declare @cnt int = 0;
UPDATE p SET 
	Latitude = i.Latitude, 
	Longitude = i.Longitude, 
	Velocity = i.Velocity,
	Direction = i.Direction,
	Point = geography::Point(i.Latitude, i.Longitude,4955),
	AvlDateTime = i.AvlDateTime, 
	ModifiedDateTime = @timeNow,
	ModifiedBy = i.ModifiedBy
FROM Positions p, inserted i WHERE p.Address = i.Address

SET @cnt = @@ROWCOUNT;
IF (@cnt = 0)
BEGIN
	INSERT INTO Positions (Address,Latitude,Longitude,Velocity,Direction,Point,AvlDateTime,ModifiedDateTime,ModifiedBy)
        SELECT Address, Latitude,Longitude,Velocity,Direction,
			geography::Point(Latitude, Longitude, 4955),
		AvlDateTime,@timeNow,ModifiedBy
		FROM inserted
END

UPDATE DeviceStats SET LastUpdateDateTime = @timeNow WHERE [Address] = (SELECT [Address] from inserted);

IF (@@ROWCOUNT = 0) --no DeviceStats rows updated
BEGIN
	INSERT INTO DeviceStats ([Address],[DeviceId],[LastUpdateDateTime])
	SELECT ins.Address, d.Id, @timeNow 
	FROM inserted ins
	LEFT JOIN Devices d (nolock) ON d.Address = ins.Address;
END

END
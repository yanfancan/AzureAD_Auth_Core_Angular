CREATE TABLE [dbo].[ResponseAreaSegments] (
    [ID]        INT             IDENTITY (1, 1) NOT NULL,
    [DrawOrder] INT             NOT NULL,
    [AreaID]    INT             NOT NULL,
    [Lat]       DECIMAL (18, 6) NOT NULL,
    [Lon]       DECIMAL (18, 6) NOT NULL
);


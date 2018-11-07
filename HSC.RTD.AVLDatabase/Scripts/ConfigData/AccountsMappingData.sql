DELETE FROM [dbo].[Devices]
GO
DELETE FROM [dbo].[ServiceAccounts_Services]
GO
DELETE FROM [dbo].[Services]
GO
DELETE FROM [dbo].[ServiceAccounts]
GO
SET IDENTITY_INSERT [dbo].[ServiceAccounts] ON 
GO
INSERT [dbo].[ServiceAccounts] ([Id], [Name], [Password], [Roles], [Status], [TimeZone], [AddedDateTime], [AddedBy], [ModifiedDateTime], [ModifiedBy]) VALUES (2, N'provider', N'gps', 1, 1, N'Canada Central Standard Time', CAST(N'2018-09-08T00:00:00.000' AS DateTime), N'Andrey', NULL, NULL)
GO
INSERT [dbo].[ServiceAccounts] ([Id], [Name], [Password], [Roles], [Status], [TimeZone], [AddedDateTime], [AddedBy], [ModifiedDateTime], [ModifiedBy]) VALUES (12, N'consumer', N'gps', 2, 2, N'Canada Central Standard Time', CAST(N'2018-09-26T15:11:29.957' AS DateTime), N'Andrey', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[ServiceAccounts] OFF
GO
SET IDENTITY_INSERT [dbo].[Services] ON 
GO
INSERT [dbo].[Services] ([Id], [ServiceName], [Description], [AddedDateTime], [AddedBy]) VALUES (4, N'CAD1', N'Test CAD', CAST(N'2018-08-13T12:54:17.573' AS DateTime), N'Andrey')
GO
INSERT [dbo].[Services] ([Id], [ServiceName], [Description], [AddedDateTime], [AddedBy]) VALUES (5, N'CAD2', N'Test CAD', CAST(N'2018-08-13T12:54:53.110' AS DateTime), N'Andrey')
GO
SET IDENTITY_INSERT [dbo].[Services] OFF
GO
SET IDENTITY_INSERT [dbo].[ServiceAccounts_Services] ON 
GO
INSERT [dbo].[ServiceAccounts_Services] ([Id], [UserId], [ServiceId], [AddedDateTime], [AddedBy]) VALUES (3, 12, 4, CAST(N'2018-08-13T12:59:38.493' AS DateTime), N'Andrey')
GO
SET IDENTITY_INSERT [dbo].[ServiceAccounts_Services] OFF
GO
SET IDENTITY_INSERT [dbo].[Devices] ON 
GO
INSERT [dbo].[Devices] ([Id], [Address], [ServiceId], [Description], [DeviceType], [VehicleName], [VehicleId], [AddedDateTime], [AddedBy]) VALUES (4, N'2945900', 4, NULL, 1, N'V1', N'1', CAST(N'2018-08-13T12:56:47.923' AS DateTime), N'Andrey')
GO
INSERT [dbo].[Devices] ([Id], [Address], [ServiceId], [Description], [DeviceType], [VehicleName], [VehicleId], [AddedDateTime], [AddedBy]) VALUES (5, N'2945901', 4, NULL, 1, N'V2', N'2', CAST(N'2018-08-13T12:57:08.783' AS DateTime), N'Andrey')
GO
INSERT [dbo].[Devices] ([Id], [Address], [ServiceId], [Description], [DeviceType], [VehicleName], [VehicleId], [AddedDateTime], [AddedBy]) VALUES (6, N'2945902', 4, NULL, 1, N'V3', N'3', CAST(N'2018-08-13T12:57:29.547' AS DateTime), N'Andrey')
GO
INSERT [dbo].[Devices] ([Id], [Address], [ServiceId], [Description], [DeviceType], [VehicleName], [VehicleId], [AddedDateTime], [AddedBy]) VALUES (7, N'2945903', 4, NULL, 1, N'V4', N'4', CAST(N'2018-08-13T12:57:47.410' AS DateTime), N'Andrey')
GO
SET IDENTITY_INSERT [dbo].[Devices] OFF
GO
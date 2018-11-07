DELETE FROM [dbo].[AvlConfiguration]
GO
SET IDENTITY_INSERT [dbo].[AvlConfiguration] ON 
GO
INSERT [dbo].[AvlConfiguration] ([ConfigurationId], [ConfigurationKey], [ConfigurationValue], [Description], [LastChangedDateTime], [Component], [ComponentName]) VALUES (1, N'SessionExpireMinutes', N'20', N'Session expiration time if no requests received', CAST(N'2018-08-09T10:55:31.570' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[AvlConfiguration] ([ConfigurationId], [ConfigurationKey], [ConfigurationValue], [Description], [LastChangedDateTime], [Component], [ComponentName]) VALUES (2, N'Datum', N'NAD83', NULL, CAST(N'2018-08-09T10:57:00.860' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[AvlConfiguration] ([ConfigurationId], [ConfigurationKey], [ConfigurationValue], [Description], [LastChangedDateTime], [Component], [ComponentName]) VALUES (5, N'LoginExpirationDays', N'1', NULL, CAST(N'2018-08-10T10:23:39.057' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[AvlConfiguration] ([ConfigurationId], [ConfigurationKey], [ConfigurationValue], [Description], [LastChangedDateTime], [Component], [ComponentName]) VALUES (6, N'MaxFeedAgeSec', N'60', N'Maximum number of secons for feed to become stalled', CAST(N'2018-10-16T20:16:22.470' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[AvlConfiguration] OFF
GO
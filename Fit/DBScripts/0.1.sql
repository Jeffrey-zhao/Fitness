/****** Object:  Table [dbo].[TFit_Permissions]    Script Date: 06/12/2017 22:26:21 ******/
SET IDENTITY_INSERT [dbo].[TFit_Permissions] ON
INSERT [dbo].[TFit_Permissions] ([ID], [Name], [Description], [IsDeleted], [CreatedDateTime]) VALUES (1, N'Permission.List', NULL, 0, CAST(0x0000A790016D0BE7 AS DateTime))
INSERT [dbo].[TFit_Permissions] ([ID], [Name], [Description], [IsDeleted], [CreatedDateTime]) VALUES (2, N'Permission.Add', NULL, 0, CAST(0x0000A790016D1C69 AS DateTime))
INSERT [dbo].[TFit_Permissions] ([ID], [Name], [Description], [IsDeleted], [CreatedDateTime]) VALUES (3, N'Permission.Edit', NULL, 0, CAST(0x0000A790016D2E71 AS DateTime))
INSERT [dbo].[TFit_Permissions] ([ID], [Name], [Description], [IsDeleted], [CreatedDateTime]) VALUES (4, N'Permission.Delete', NULL, 0, CAST(0x0000A790016D3F09 AS DateTime))
INSERT [dbo].[TFit_Permissions] ([ID], [Name], [Description], [IsDeleted], [CreatedDateTime]) VALUES (5, N'Role.List', NULL, 0, CAST(0x0000A790016D6920 AS DateTime))
INSERT [dbo].[TFit_Permissions] ([ID], [Name], [Description], [IsDeleted], [CreatedDateTime]) VALUES (6, N'Role.Add', NULL, 0, CAST(0x0000A790016D740A AS DateTime))
INSERT [dbo].[TFit_Permissions] ([ID], [Name], [Description], [IsDeleted], [CreatedDateTime]) VALUES (7, N'Role.Edit', NULL, 0, CAST(0x0000A790016D7D55 AS DateTime))
INSERT [dbo].[TFit_Permissions] ([ID], [Name], [Description], [IsDeleted], [CreatedDateTime]) VALUES (8, N'Role.Delete', NULL, 0, CAST(0x0000A790016D87EE AS DateTime))
INSERT [dbo].[TFit_Permissions] ([ID], [Name], [Description], [IsDeleted], [CreatedDateTime]) VALUES (9, N'AdminUser.List', NULL, 0, CAST(0x0000A790016D9A76 AS DateTime))
INSERT [dbo].[TFit_Permissions] ([ID], [Name], [Description], [IsDeleted], [CreatedDateTime]) VALUES (10, N'AdminUser.Add', NULL, 0, CAST(0x0000A790016DA349 AS DateTime))
INSERT [dbo].[TFit_Permissions] ([ID], [Name], [Description], [IsDeleted], [CreatedDateTime]) VALUES (11, N'AdminUser.Edit', NULL, 0, CAST(0x0000A790016DAE2F AS DateTime))
INSERT [dbo].[TFit_Permissions] ([ID], [Name], [Description], [IsDeleted], [CreatedDateTime]) VALUES (12, N'AdminUser.Delete', NULL, 0, CAST(0x0000A790016DB9AE AS DateTime))
SET IDENTITY_INSERT [dbo].[TFit_Permissions] OFF
/****** Object:  Table [dbo].[TFit_AdminUsers]    Script Date: 06/12/2017 22:26:21 ******/
SET IDENTITY_INSERT [dbo].[TFit_AdminUsers] ON
INSERT [dbo].[TFit_AdminUsers] ([ID], [Name], [PhoneNum], [Email], [PasswordSalt], [PasswordHash], [LoginErrorTimes], [LastLoginErrorDateTime], [IsDeleted], [CreatedDateTime]) VALUES (1, N'zhixin9001', N'15891655417', N'zhixin9001@126.com', N'4tsrf', N'577D23E45615BC3BF4F7A4F5A9E6FD8A', 0, NULL, 0, CAST(0x0000A790016E6E42 AS DateTime))
SET IDENTITY_INSERT [dbo].[TFit_AdminUsers] OFF
/****** Object:  Table [dbo].[TFit_Roles]    Script Date: 06/12/2017 22:26:21 ******/
SET IDENTITY_INSERT [dbo].[TFit_Roles] ON
INSERT [dbo].[TFit_Roles] ([ID], [Name], [Description], [IsDeleted], [CreatedDateTime]) VALUES (1, N'SuperAdmin', N'Has All Permission', 0, CAST(0x0000A790016E2947 AS DateTime))
SET IDENTITY_INSERT [dbo].[TFit_Roles] OFF
/****** Object:  Table [dbo].[TFit_RolePermissions]    Script Date: 06/12/2017 22:26:21 ******/
INSERT [dbo].[TFit_RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 1)
INSERT [dbo].[TFit_RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 2)
INSERT [dbo].[TFit_RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 3)
INSERT [dbo].[TFit_RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 4)
INSERT [dbo].[TFit_RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 5)
INSERT [dbo].[TFit_RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 6)
INSERT [dbo].[TFit_RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 7)
INSERT [dbo].[TFit_RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 8)
INSERT [dbo].[TFit_RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 9)
INSERT [dbo].[TFit_RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 10)
INSERT [dbo].[TFit_RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 11)
INSERT [dbo].[TFit_RolePermissions] ([RoleId], [PermissionId]) VALUES (1, 12)
/****** Object:  Table [dbo].[TFit_AdminUserRole]    Script Date: 06/12/2017 22:26:21 ******/
INSERT [dbo].[TFit_AdminUserRole] ([AdminUserId], [RoleId]) VALUES (1, 1)

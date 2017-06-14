USE [qds174986397_db]
GO
/****** Object:  Table [dbo].[TFit_MuscleGroups]    Script Date: 06/15/2017 06:53:24 ******/
SET IDENTITY_INSERT [dbo].[TFit_MuscleGroups] ON
INSERT [dbo].[TFit_MuscleGroups] ([ID], [Name], [Description], [IsDeleted], [CreatedDateTime]) VALUES (1, N'前臂', NULL, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_MuscleGroups] ([ID], [Name], [Description], [IsDeleted], [CreatedDateTime]) VALUES (2, N'上臂', NULL, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_MuscleGroups] ([ID], [Name], [Description], [IsDeleted], [CreatedDateTime]) VALUES (3, N'三角肌臂', NULL, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_MuscleGroups] ([ID], [Name], [Description], [IsDeleted], [CreatedDateTime]) VALUES (4, N'胸肌', NULL, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_MuscleGroups] ([ID], [Name], [Description], [IsDeleted], [CreatedDateTime]) VALUES (5, N'腹肌', NULL, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_MuscleGroups] ([ID], [Name], [Description], [IsDeleted], [CreatedDateTime]) VALUES (6, N'背部', NULL, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_MuscleGroups] ([ID], [Name], [Description], [IsDeleted], [CreatedDateTime]) VALUES (7, N'臀部', NULL, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_MuscleGroups] ([ID], [Name], [Description], [IsDeleted], [CreatedDateTime]) VALUES (8, N'腿部', NULL, 0, CAST(0x0000A79100000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[TFit_MuscleGroups] OFF
/****** Object:  Table [dbo].[TFit_AdminUsers]    Script Date: 06/15/2017 06:53:24 ******/
SET IDENTITY_INSERT [dbo].[TFit_AdminUsers] ON
INSERT [dbo].[TFit_AdminUsers] ([ID], [Name], [PhoneNum], [Email], [PasswordSalt], [PasswordHash], [LoginErrorTimes], [LastLoginErrorDateTime], [IsDeleted], [CreatedDateTime]) VALUES (1, N'zhixin9001', N'15891655417', N'zhixin9001@126.com', N'4tsrf', N'577D23E45615BC3BF4F7A4F5A9E6FD8A', 0, NULL, 0, CAST(0x0000A790016E6E42 AS DateTime))
SET IDENTITY_INSERT [dbo].[TFit_AdminUsers] OFF
/****** Object:  Table [dbo].[TFit_Permissions]    Script Date: 06/15/2017 06:53:24 ******/
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
/****** Object:  Table [dbo].[TFit_Roles]    Script Date: 06/15/2017 06:53:24 ******/
SET IDENTITY_INSERT [dbo].[TFit_Roles] ON
INSERT [dbo].[TFit_Roles] ([ID], [Name], [Description], [IsDeleted], [CreatedDateTime]) VALUES (1, N'SuperAdmin', N'Has All Permission', 0, CAST(0x0000A790016E2947 AS DateTime))
SET IDENTITY_INSERT [dbo].[TFit_Roles] OFF
/****** Object:  Table [dbo].[TFit_RolePermissions]    Script Date: 06/15/2017 06:53:24 ******/
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
/****** Object:  Table [dbo].[TFit_Muscles]    Script Date: 06/15/2017 06:53:24 ******/
SET IDENTITY_INSERT [dbo].[TFit_Muscles] ON
INSERT [dbo].[TFit_Muscles] ([ID], [Name], [Description], [MuscleGroupID], [IsDeleted], [CreatedDateTime]) VALUES (1, N'前臂肌群', NULL, 1, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_Muscles] ([ID], [Name], [Description], [MuscleGroupID], [IsDeleted], [CreatedDateTime]) VALUES (2, N'肱二头肌', NULL, 2, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_Muscles] ([ID], [Name], [Description], [MuscleGroupID], [IsDeleted], [CreatedDateTime]) VALUES (5, N'肱三头肌', NULL, 2, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_Muscles] ([ID], [Name], [Description], [MuscleGroupID], [IsDeleted], [CreatedDateTime]) VALUES (6, N'肱肌', NULL, 2, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_Muscles] ([ID], [Name], [Description], [MuscleGroupID], [IsDeleted], [CreatedDateTime]) VALUES (7, N'三角肌前束', NULL, 3, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_Muscles] ([ID], [Name], [Description], [MuscleGroupID], [IsDeleted], [CreatedDateTime]) VALUES (8, N'三角肌中束', NULL, 3, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_Muscles] ([ID], [Name], [Description], [MuscleGroupID], [IsDeleted], [CreatedDateTime]) VALUES (9, N'三角肌后束', NULL, 3, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_Muscles] ([ID], [Name], [Description], [MuscleGroupID], [IsDeleted], [CreatedDateTime]) VALUES (10, N'胸大肌', NULL, 4, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_Muscles] ([ID], [Name], [Description], [MuscleGroupID], [IsDeleted], [CreatedDateTime]) VALUES (11, N'腹横肌', NULL, 5, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_Muscles] ([ID], [Name], [Description], [MuscleGroupID], [IsDeleted], [CreatedDateTime]) VALUES (12, N'腹外斜肌', NULL, 5, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_Muscles] ([ID], [Name], [Description], [MuscleGroupID], [IsDeleted], [CreatedDateTime]) VALUES (13, N'腹直肌上部', NULL, 5, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_Muscles] ([ID], [Name], [Description], [MuscleGroupID], [IsDeleted], [CreatedDateTime]) VALUES (14, N'腹直肌整体', NULL, 5, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_Muscles] ([ID], [Name], [Description], [MuscleGroupID], [IsDeleted], [CreatedDateTime]) VALUES (15, N'腹肌综合', NULL, 5, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_Muscles] ([ID], [Name], [Description], [MuscleGroupID], [IsDeleted], [CreatedDateTime]) VALUES (16, N'背阔肌', NULL, 6, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_Muscles] ([ID], [Name], [Description], [MuscleGroupID], [IsDeleted], [CreatedDateTime]) VALUES (17, N'竖脊肌', NULL, 6, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_Muscles] ([ID], [Name], [Description], [MuscleGroupID], [IsDeleted], [CreatedDateTime]) VALUES (18, N'斜方肌', NULL, 6, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_Muscles] ([ID], [Name], [Description], [MuscleGroupID], [IsDeleted], [CreatedDateTime]) VALUES (19, N'臀大肌', NULL, 7, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_Muscles] ([ID], [Name], [Description], [MuscleGroupID], [IsDeleted], [CreatedDateTime]) VALUES (21, N'髋外展肌', NULL, 7, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_Muscles] ([ID], [Name], [Description], [MuscleGroupID], [IsDeleted], [CreatedDateTime]) VALUES (22, N'大腿内收肌', NULL, 8, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_Muscles] ([ID], [Name], [Description], [MuscleGroupID], [IsDeleted], [CreatedDateTime]) VALUES (23, N'股二头肌', NULL, 8, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_Muscles] ([ID], [Name], [Description], [MuscleGroupID], [IsDeleted], [CreatedDateTime]) VALUES (24, N'股四头肌', NULL, 8, 0, CAST(0x0000A79100000000 AS DateTime))
INSERT [dbo].[TFit_Muscles] ([ID], [Name], [Description], [MuscleGroupID], [IsDeleted], [CreatedDateTime]) VALUES (25, N'小腿三头肌', NULL, 8, 0, CAST(0x0000A79100000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[TFit_Muscles] OFF
/****** Object:  Table [dbo].[TFit_AdminUserRole]    Script Date: 06/15/2017 06:53:23 ******/
INSERT [dbo].[TFit_AdminUserRole] ([AdminUserId], [RoleId]) VALUES (1, 1)
/****** Object:  Table [dbo].[TFit_Motions]    Script Date: 06/15/2017 06:53:24 ******/

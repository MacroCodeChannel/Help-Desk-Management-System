IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(128) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'00000000000000_CreateIdentitySchema', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240519121823_InitialMigration', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240519122627_ExtendedUserTable', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AspNetUsers] ADD [City] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [AspNetUsers] ADD [Country] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [AspNetUsers] ADD [FirstName] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [AspNetUsers] ADD [Gender] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [AspNetUsers] ADD [LastName] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [AspNetUsers] ADD [MiddleName] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240519122744_ExtendedUserTableUpdated', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUserTokens]') AND [c].[name] = N'Name');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUserTokens] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [AspNetUserTokens] ALTER COLUMN [Name] nvarchar(450) NOT NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUserTokens]') AND [c].[name] = N'LoginProvider');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUserTokens] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [AspNetUserTokens] ALTER COLUMN [LoginProvider] nvarchar(450) NOT NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUserLogins]') AND [c].[name] = N'ProviderKey');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUserLogins] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [AspNetUserLogins] ALTER COLUMN [ProviderKey] nvarchar(450) NOT NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUserLogins]') AND [c].[name] = N'LoginProvider');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUserLogins] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [AspNetUserLogins] ALTER COLUMN [LoginProvider] nvarchar(450) NOT NULL;
GO

CREATE TABLE [Tickets] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [Priority] nvarchar(max) NOT NULL,
    [CreatedById] nvarchar(450) NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    CONSTRAINT [PK_Tickets] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Tickets_AspNetUsers_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Tickets_CreatedById] ON [Tickets] ([CreatedById]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240525125437_Tickets', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Comments] (
    [Id] int NOT NULL IDENTITY,
    [Description] nvarchar(max) NOT NULL,
    [TicketId] int NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [CreatedById] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Comments_AspNetUsers_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Comments_Tickets_TicketId] FOREIGN KEY ([TicketId]) REFERENCES [Tickets] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Comments_CreatedById] ON [Comments] ([CreatedById]);
GO

CREATE INDEX [IX_Comments_TicketId] ON [Comments] ([TicketId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240526080936_Comments', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AuditTrails] (
    [Id] int NOT NULL IDENTITY,
    [Action] nvarchar(max) NOT NULL,
    [Module] nvarchar(max) NOT NULL,
    [AffectedTable] nvarchar(max) NOT NULL,
    [TimeStamp] datetime2 NOT NULL,
    [IpAddress] nvarchar(max) NOT NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AuditTrails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AuditTrails_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AuditTrails_UserId] ON [AuditTrails] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240602161348_AuditTrails', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [TicketCategories] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [CreatedById] nvarchar(450) NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [ModifiedById] nvarchar(450) NOT NULL,
    [ModifiedOn] datetime2 NOT NULL,
    CONSTRAINT [PK_TicketCategories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TicketCategories_AspNetUsers_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TicketCategories_AspNetUsers_ModifiedById] FOREIGN KEY ([ModifiedById]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_TicketCategories_CreatedById] ON [TicketCategories] ([CreatedById]);
GO

CREATE INDEX [IX_TicketCategories_ModifiedById] ON [TicketCategories] ([ModifiedById]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240608170543_TicketCategory', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TicketCategories]') AND [c].[name] = N'ModifiedOn');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [TicketCategories] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [TicketCategories] ALTER COLUMN [ModifiedOn] datetime2 NULL;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TicketCategories]') AND [c].[name] = N'ModifiedById');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [TicketCategories] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [TicketCategories] ALTER COLUMN [ModifiedById] nvarchar(450) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240608171648_ModifiedUserActivities', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [TicketSubCategories] (
    [Id] int NOT NULL IDENTITY,
    [CategoryId] int NOT NULL,
    [Code] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [CreatedById] nvarchar(450) NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [ModifiedById] nvarchar(450) NULL,
    [ModifiedOn] datetime2 NULL,
    CONSTRAINT [PK_TicketSubCategories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TicketSubCategories_AspNetUsers_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_TicketSubCategories_AspNetUsers_ModifiedById] FOREIGN KEY ([ModifiedById]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_TicketSubCategories_TicketCategories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [TicketCategories] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_TicketSubCategories_CategoryId] ON [TicketSubCategories] ([CategoryId]);
GO

CREATE INDEX [IX_TicketSubCategories_CreatedById] ON [TicketSubCategories] ([CreatedById]);
GO

CREATE INDEX [IX_TicketSubCategories_ModifiedById] ON [TicketSubCategories] ([ModifiedById]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240610165104_AddedTicketSubCategories', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Tickets] ADD [SubCategoryId] int NULL;
GO

CREATE INDEX [IX_Tickets_SubCategoryId] ON [Tickets] ([SubCategoryId]);
GO

ALTER TABLE [Tickets] ADD CONSTRAINT [FK_Tickets_TicketSubCategories_SubCategoryId] FOREIGN KEY ([SubCategoryId]) REFERENCES [TicketSubCategories] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240616102228_SUbCategoryDetails', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [SystemCodes] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_SystemCodes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SystemCodeDetails] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [OrderNo] int NULL,
    [SystemCodeId] int NOT NULL,
    CONSTRAINT [PK_SystemCodeDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SystemCodeDetails_SystemCodes_SystemCodeId] FOREIGN KEY ([SystemCodeId]) REFERENCES [SystemCodes] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_SystemCodeDetails_SystemCodeId] ON [SystemCodeDetails] ([SystemCodeId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240616140537_SystemCodes', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SystemCodeDetails] DROP CONSTRAINT [FK_SystemCodeDetails_SystemCodes_SystemCodeId];
GO

ALTER TABLE [SystemCodes] ADD [CreatedById] nvarchar(450) NOT NULL DEFAULT N'';
GO

ALTER TABLE [SystemCodes] ADD [CreatedOn] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [SystemCodes] ADD [ModifiedById] nvarchar(450) NULL;
GO

ALTER TABLE [SystemCodes] ADD [ModifiedOn] datetime2 NULL;
GO

ALTER TABLE [SystemCodeDetails] ADD [CreatedById] nvarchar(450) NOT NULL DEFAULT N'';
GO

ALTER TABLE [SystemCodeDetails] ADD [CreatedOn] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [SystemCodeDetails] ADD [ModifiedById] nvarchar(450) NULL;
GO

ALTER TABLE [SystemCodeDetails] ADD [ModifiedOn] datetime2 NULL;
GO

CREATE INDEX [IX_SystemCodes_CreatedById] ON [SystemCodes] ([CreatedById]);
GO

CREATE INDEX [IX_SystemCodes_ModifiedById] ON [SystemCodes] ([ModifiedById]);
GO

CREATE INDEX [IX_SystemCodeDetails_CreatedById] ON [SystemCodeDetails] ([CreatedById]);
GO

CREATE INDEX [IX_SystemCodeDetails_ModifiedById] ON [SystemCodeDetails] ([ModifiedById]);
GO

ALTER TABLE [SystemCodeDetails] ADD CONSTRAINT [FK_SystemCodeDetails_AspNetUsers_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [SystemCodeDetails] ADD CONSTRAINT [FK_SystemCodeDetails_AspNetUsers_ModifiedById] FOREIGN KEY ([ModifiedById]) REFERENCES [AspNetUsers] ([Id]);
GO

ALTER TABLE [SystemCodeDetails] ADD CONSTRAINT [FK_SystemCodeDetails_SystemCodes_SystemCodeId] FOREIGN KEY ([SystemCodeId]) REFERENCES [SystemCodes] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [SystemCodes] ADD CONSTRAINT [FK_SystemCodes_AspNetUsers_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [SystemCodes] ADD CONSTRAINT [FK_SystemCodes_AspNetUsers_ModifiedById] FOREIGN KEY ([ModifiedById]) REFERENCES [AspNetUsers] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240616141510_SystemCodesActvity', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tickets]') AND [c].[name] = N'Priority');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Tickets] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Tickets] DROP COLUMN [Priority];
GO

ALTER TABLE [Tickets] ADD [PriorityId] int NOT NULL DEFAULT 0;
GO

CREATE INDEX [IX_Tickets_PriorityId] ON [Tickets] ([PriorityId]);
GO

ALTER TABLE [Tickets] ADD CONSTRAINT [FK_Tickets_SystemCodeDetails_PriorityId] FOREIGN KEY ([PriorityId]) REFERENCES [SystemCodeDetails] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240616170121_TicketPriorityDetails', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tickets]') AND [c].[name] = N'Status');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Tickets] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Tickets] DROP COLUMN [Status];
GO

ALTER TABLE [Tickets] ADD [StatusId] int NOT NULL DEFAULT 0;
GO

CREATE INDEX [IX_Tickets_StatusId] ON [Tickets] ([StatusId]);
GO

ALTER TABLE [Tickets] ADD CONSTRAINT [FK_Tickets_SystemCodeDetails_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [SystemCodeDetails] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240616171446_TicketStatusDetails', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Tickets] ADD [Attachment] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240617083851_DocumentsAttachment', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Departments] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [CreatedById] nvarchar(450) NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [ModifiedById] nvarchar(450) NULL,
    [ModifiedOn] datetime2 NULL,
    CONSTRAINT [PK_Departments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Departments_AspNetUsers_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Departments_AspNetUsers_ModifiedById] FOREIGN KEY ([ModifiedById]) REFERENCES [AspNetUsers] ([Id])
);
GO

CREATE INDEX [IX_Departments_CreatedById] ON [Departments] ([CreatedById]);
GO

CREATE INDEX [IX_Departments_ModifiedById] ON [Departments] ([ModifiedById]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240617142228_Departments', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [TicketResolutions] (
    [Id] int NOT NULL IDENTITY,
    [TicketId] int NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [StatusId] int NOT NULL,
    [CreatedById] nvarchar(450) NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [ModifiedById] nvarchar(450) NULL,
    [ModifiedOn] datetime2 NULL,
    CONSTRAINT [PK_TicketResolutions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TicketResolutions_AspNetUsers_CreatedById] FOREIGN KEY ([CreatedById]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_TicketResolutions_AspNetUsers_ModifiedById] FOREIGN KEY ([ModifiedById]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_TicketResolutions_SystemCodeDetails_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [SystemCodeDetails] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TicketResolutions_Tickets_TicketId] FOREIGN KEY ([TicketId]) REFERENCES [Tickets] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_TicketResolutions_CreatedById] ON [TicketResolutions] ([CreatedById]);
GO

CREATE INDEX [IX_TicketResolutions_ModifiedById] ON [TicketResolutions] ([ModifiedById]);
GO

CREATE INDEX [IX_TicketResolutions_StatusId] ON [TicketResolutions] ([StatusId]);
GO

CREATE INDEX [IX_TicketResolutions_TicketId] ON [TicketResolutions] ([TicketId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240707093304_ResolutionStatus', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Tickets] ADD [AssignedOn] datetime2 NULL;
GO

ALTER TABLE [Tickets] ADD [AssignedToId] int NULL;
GO

ALTER TABLE [Tickets] ADD [AssignedToId1] nvarchar(450) NULL;
GO

CREATE INDEX [IX_Tickets_AssignedToId1] ON [Tickets] ([AssignedToId1]);
GO

ALTER TABLE [Tickets] ADD CONSTRAINT [FK_Tickets_AspNetUsers_AssignedToId1] FOREIGN KEY ([AssignedToId1]) REFERENCES [AspNetUsers] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240707163847_Assignment', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Tickets] DROP CONSTRAINT [FK_Tickets_AspNetUsers_AssignedToId1];
GO

DROP INDEX [IX_Tickets_AssignedToId1] ON [Tickets];
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tickets]') AND [c].[name] = N'AssignedToId1');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Tickets] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Tickets] DROP COLUMN [AssignedToId1];
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tickets]') AND [c].[name] = N'AssignedToId');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Tickets] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Tickets] ALTER COLUMN [AssignedToId] nvarchar(450) NULL;
GO

CREATE INDEX [IX_Tickets_AssignedToId] ON [Tickets] ([AssignedToId]);
GO

ALTER TABLE [Tickets] ADD CONSTRAINT [FK_Tickets_AspNetUsers_AssignedToId] FOREIGN KEY ([AssignedToId]) REFERENCES [AspNetUsers] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240707165657_AssignmentUpdates', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tickets]') AND [c].[name] = N'Attachment');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Tickets] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [Tickets] ALTER COLUMN [Attachment] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240709175346_AttachmentFieldUpdates', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [SystemTasks] (
    [Id] int NOT NULL IDENTITY,
    [Code] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [ParentId] int NULL,
    [OrderNumber] int NULL,
    CONSTRAINT [PK_SystemTasks] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SystemTasks_SystemTasks_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [SystemTasks] ([Id])
);
GO

CREATE INDEX [IX_SystemTasks_ParentId] ON [SystemTasks] ([ParentId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240720112150_SystemTasks', N'8.0.4');
GO

COMMIT;
GO


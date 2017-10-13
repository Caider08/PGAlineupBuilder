IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170529082938_InitialCreatePGA')
BEGIN
    CREATE TABLE [DKS] (
        [ID] int NOT NULL IDENTITY,
        [Name] nvarchar(max),
        CONSTRAINT [PK_DKS] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170529082938_InitialCreatePGA')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170529082938_InitialCreatePGA', N'1.1.2');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170606054728_DKcsv')
BEGIN
    CREATE TABLE [DKup] (
        [ID] int NOT NULL IDENTITY,
        [Name] nvarchar(max),
        [csvUpload] varbinary(max),
        CONSTRAINT [PK_DKup] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170606054728_DKcsv')
BEGIN
    CREATE TABLE [Golfer] (
        [ID] int NOT NULL IDENTITY,
        [DKsalarysID] int,
        [Name] nvarchar(max),
        [Salary] int NOT NULL,
        CONSTRAINT [PK_Golfer] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_Golfer_DKS_DKsalarysID] FOREIGN KEY ([DKsalarysID]) REFERENCES [DKS] ([ID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170606054728_DKcsv')
BEGIN
    CREATE INDEX [IX_Golfer_DKsalarysID] ON [Golfer] ([DKsalarysID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170606054728_DKcsv')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170606054728_DKcsv', N'1.1.2');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170607044232_csvUploadGONE')
BEGIN
    DROP TABLE [DKup];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170607044232_csvUploadGONE')
BEGIN
    ALTER TABLE [DKS] ADD [csvUpload] varbinary(max);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170607044232_csvUploadGONE')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170607044232_csvUploadGONE', N'1.1.2');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170628043810_DroppedDKS')
BEGIN
    ALTER TABLE [Golfer] DROP CONSTRAINT [FK_Golfer_DKS_DKsalarysID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170628043810_DroppedDKS')
BEGIN
    DROP TABLE [DKS];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170628043810_DroppedDKS')
BEGIN
    ALTER TABLE [Golfer] DROP CONSTRAINT [PK_Golfer];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170628043810_DroppedDKS')
BEGIN
    EXEC sp_rename N'Golfer', N'GOLFER';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170628043810_DroppedDKS')
BEGIN
    EXEC sp_rename N'GOLFER.DKsalarysID', N'DkTourneyID', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170628043810_DroppedDKS')
BEGIN
    EXEC sp_rename N'GOLFER.IX_Golfer_DKsalarysID', N'IX_GOLFER_DkTourneyID', N'INDEX';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170628043810_DroppedDKS')
BEGIN
    ALTER TABLE [GOLFER] ADD [GameInfo] nvarchar(max);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170628043810_DroppedDKS')
BEGIN
    ALTER TABLE [GOLFER] ADD CONSTRAINT [PK_GOLFER] PRIMARY KEY ([ID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170628043810_DroppedDKS')
BEGIN
    CREATE TABLE [DKT] (
        [ID] int NOT NULL IDENTITY,
        [Name] nvarchar(max),
        CONSTRAINT [PK_DKT] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170628043810_DroppedDKS')
BEGIN
    ALTER TABLE [GOLFER] ADD CONSTRAINT [FK_GOLFER_DKT_DkTourneyID] FOREIGN KEY ([DkTourneyID]) REFERENCES [DKT] ([ID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170628043810_DroppedDKS')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170628043810_DroppedDKS', N'1.1.2');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170702084324_databaseSavedGolfers')
BEGIN
    ALTER TABLE [GOLFER] ADD [Playerid] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170702084324_databaseSavedGolfers')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170702084324_databaseSavedGolfers', N'1.1.2');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170704081125_ClassesUpdated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170704081125_ClassesUpdated', N'1.1.2');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170705023022_ClassesChangedLot')
BEGIN
    ALTER TABLE [GOLFER] ADD [Website] nvarchar(max);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170705023022_ClassesChangedLot')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170705023022_ClassesChangedLot', N'1.1.2');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170705063117_BigClassChanges')
BEGIN
    ALTER TABLE [GOLFER] DROP CONSTRAINT [FK_GOLFER_DKT_DkTourneyID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170705063117_BigClassChanges')
BEGIN
    DROP INDEX [IX_GOLFER_DkTourneyID] ON [GOLFER];
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'GOLFER') AND [c].[name] = N'DkTourneyID');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [GOLFER] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [GOLFER] ALTER COLUMN [DkTourneyID] int NOT NULL;
    CREATE INDEX [IX_GOLFER_DkTourneyID] ON [GOLFER] ([DkTourneyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170705063117_BigClassChanges')
BEGIN
    ALTER TABLE [GOLFER] ADD [Exposure] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170705063117_BigClassChanges')
BEGIN
    ALTER TABLE [GOLFER] ADD [YearCreated] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170705063117_BigClassChanges')
BEGIN
    ALTER TABLE [GOLFER] ADD CONSTRAINT [FK_GOLFER_DKT_DkTourneyID] FOREIGN KEY ([DkTourneyID]) REFERENCES [DKT] ([ID]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170705063117_BigClassChanges')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170705063117_BigClassChanges', N'1.1.2');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170719030846_ChangedGolferClass')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'GOLFER') AND [c].[name] = N'Exposure');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [GOLFER] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [GOLFER] ALTER COLUMN [Exposure] float NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170719030846_ChangedGolferClass')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170719030846_ChangedGolferClass', N'1.1.2');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170720021000_Debugging5032')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170720021000_Debugging5032', N'1.1.2');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170721035849_More502Debugging')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170721035849_More502Debugging', N'1.1.2');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170727044814_GolferChanged')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170727044814_GolferChanged', N'1.1.2');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170809053849_FanDuelClass')
BEGIN
    CREATE TABLE [FDT] (
        [ID] int NOT NULL IDENTITY,
        [Name] nvarchar(max),
        CONSTRAINT [PK_FDT] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170809053849_FanDuelClass')
BEGIN
    CREATE TABLE [FDGOLFER] (
        [ID] int NOT NULL IDENTITY,
        [Exposure] float NOT NULL,
        [FDtourneyID] int NOT NULL,
        [GameInfo] nvarchar(max),
        [Name] nvarchar(max),
        [Playerid] int NOT NULL,
        [Salary] int NOT NULL,
        [Website] nvarchar(max),
        [YearCreated] int NOT NULL,
        CONSTRAINT [PK_FDGOLFER] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_FDGOLFER_FDT_FDtourneyID] FOREIGN KEY ([FDtourneyID]) REFERENCES [FDT] ([ID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170809053849_FanDuelClass')
BEGIN
    CREATE INDEX [IX_FDGOLFER_FDtourneyID] ON [FDGOLFER] ([FDtourneyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170809053849_FanDuelClass')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170809053849_FanDuelClass', N'1.1.2');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170809055433_FDclassChanged')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'FDGOLFER') AND [c].[name] = N'Playerid');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [FDGOLFER] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [FDGOLFER] ALTER COLUMN [Playerid] nvarchar(max);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170809055433_FDclassChanged')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170809055433_FDclassChanged', N'1.1.2');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170820070059_BlogPostClasses')
BEGIN
    CREATE TABLE [BPCAT] (
        [ID] int NOT NULL IDENTITY,
        [Description] nvarchar(max),
        [Name] nvarchar(max),
        [URLslug] nvarchar(max),
        CONSTRAINT [PK_BPCAT] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170820070059_BlogPostClasses')
BEGIN
    CREATE TABLE [BPTag] (
        [ID] int NOT NULL IDENTITY,
        [Description] nvarchar(max),
        [Name] nvarchar(max),
        [URLslug] nvarchar(max),
        CONSTRAINT [PK_BPTag] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170820070059_BlogPostClasses')
BEGIN
    CREATE TABLE [BP] (
        [ID] int NOT NULL IDENTITY,
        [CategoryID] int,
        [Content] nvarchar(max),
        [Meta] nvarchar(max),
        [Modified] datetime2 NOT NULL,
        [Name] nvarchar(max),
        [PublishedDate] datetime2 NOT NULL,
        [URLslug] nvarchar(max),
        CONSTRAINT [PK_BP] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_BP_BPCAT_CategoryID] FOREIGN KEY ([CategoryID]) REFERENCES [BPCAT] ([ID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170820070059_BlogPostClasses')
BEGIN
    CREATE TABLE [BlogPostTag] (
        [BlogPostID] int NOT NULL,
        [TagID] int NOT NULL,
        CONSTRAINT [PK_BlogPostTag] PRIMARY KEY ([BlogPostID], [TagID]),
        CONSTRAINT [FK_BlogPostTag_BP_BlogPostID] FOREIGN KEY ([BlogPostID]) REFERENCES [BP] ([ID]) ON DELETE CASCADE,
        CONSTRAINT [FK_BlogPostTag_BPTag_TagID] FOREIGN KEY ([TagID]) REFERENCES [BPTag] ([ID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170820070059_BlogPostClasses')
BEGIN
    CREATE TABLE [Comment] (
        [ID] int NOT NULL IDENTITY,
        [BlogPostID] int,
        [CommentDate] datetime2 NOT NULL,
        [Content] nvarchar(max),
        [Meta] nvarchar(max),
        [Modified] datetime2 NOT NULL,
        [URLslug] nvarchar(max),
        CONSTRAINT [PK_Comment] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_Comment_BP_BlogPostID] FOREIGN KEY ([BlogPostID]) REFERENCES [BP] ([ID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170820070059_BlogPostClasses')
BEGIN
    CREATE INDEX [IX_BP_CategoryID] ON [BP] ([CategoryID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170820070059_BlogPostClasses')
BEGIN
    CREATE INDEX [IX_BlogPostTag_TagID] ON [BlogPostTag] ([TagID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170820070059_BlogPostClasses')
BEGIN
    CREATE INDEX [IX_Comment_BlogPostID] ON [Comment] ([BlogPostID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170820070059_BlogPostClasses')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170820070059_BlogPostClasses', N'1.1.2');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170821021852_BlogPostClassesModified')
BEGIN
    ALTER TABLE [BlogPostTag] DROP CONSTRAINT [FK_BlogPostTag_BP_BlogPostID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170821021852_BlogPostClassesModified')
BEGIN
    ALTER TABLE [BlogPostTag] DROP CONSTRAINT [FK_BlogPostTag_BPTag_TagID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170821021852_BlogPostClassesModified')
BEGIN
    ALTER TABLE [BlogPostTag] DROP CONSTRAINT [PK_BlogPostTag];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170821021852_BlogPostClassesModified')
BEGIN
    EXEC sp_rename N'BlogPostTag', N'BPostTag';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170821021852_BlogPostClassesModified')
BEGIN
    EXEC sp_rename N'BPostTag.IX_BlogPostTag_TagID', N'IX_BPostTag_TagID', N'INDEX';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170821021852_BlogPostClassesModified')
BEGIN
    ALTER TABLE [BP] ADD [TagID] int;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170821021852_BlogPostClassesModified')
BEGIN
    ALTER TABLE [BPostTag] ADD CONSTRAINT [PK_BPostTag] PRIMARY KEY ([BlogPostID], [TagID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170821021852_BlogPostClassesModified')
BEGIN
    CREATE INDEX [IX_BP_TagID] ON [BP] ([TagID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170821021852_BlogPostClassesModified')
BEGIN
    ALTER TABLE [BP] ADD CONSTRAINT [FK_BP_BPTag_TagID] FOREIGN KEY ([TagID]) REFERENCES [BPTag] ([ID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170821021852_BlogPostClassesModified')
BEGIN
    ALTER TABLE [BPostTag] ADD CONSTRAINT [FK_BPostTag_BP_BlogPostID] FOREIGN KEY ([BlogPostID]) REFERENCES [BP] ([ID]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170821021852_BlogPostClassesModified')
BEGIN
    ALTER TABLE [BPostTag] ADD CONSTRAINT [FK_BPostTag_BPTag_TagID] FOREIGN KEY ([TagID]) REFERENCES [BPTag] ([ID]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170821021852_BlogPostClassesModified')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170821021852_BlogPostClassesModified', N'1.1.2');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170904050236_SEO')
BEGIN
    CREATE TABLE [SEO] (
        [ID] int NOT NULL IDENTITY,
        [Email] nvarchar(max) NOT NULL,
        [FirstName] nvarchar(max),
        [LastName] nvarchar(max),
        CONSTRAINT [PK_SEO] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170904050236_SEO')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170904050236_SEO', N'1.1.2');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170904085229_FDraft')
BEGIN
    CREATE TABLE [FDraftT] (
        [ID] int NOT NULL IDENTITY,
        [Name] nvarchar(max),
        CONSTRAINT [PK_FDraftT] PRIMARY KEY ([ID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170904085229_FDraft')
BEGIN
    CREATE TABLE [FDraftG] (
        [ID] int NOT NULL IDENTITY,
        [Exposure] float NOT NULL,
        [FDraftTourneyID] int NOT NULL,
        [GameInfo] nvarchar(max),
        [Name] nvarchar(max),
        [Playerid] nvarchar(max),
        [Salary] int NOT NULL,
        [Website] nvarchar(max),
        [YearCreated] int NOT NULL,
        CONSTRAINT [PK_FDraftG] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_FDraftG_FDraftT_FDraftTourneyID] FOREIGN KEY ([FDraftTourneyID]) REFERENCES [FDraftT] ([ID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170904085229_FDraft')
BEGIN
    CREATE INDEX [IX_FDraftG_FDraftTourneyID] ON [FDraftG] ([FDraftTourneyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170904085229_FDraft')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170904085229_FDraft', N'1.1.2');
END;

GO


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

CREATE TABLE [Language] (
    [Id] bigint NOT NULL IDENTITY,
    [Area] nvarchar(max) NULL,
    [StartDate] datetime2 NULL,
    [EndDate] datetime2 NULL,
    [Features] nvarchar(max) NULL,
    [AdditionalDetails] nvarchar(max) NULL,
    CONSTRAINT [PK_Language] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210205122434_Initial', N'5.0.2');
GO

COMMIT;
GO


CREATE TABLE [Audit].[Category] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [CategoryName] NVARCHAR (64) NOT NULL,
    [TimeSpam]     BIGINT        NULL,
    CONSTRAINT [PK_CATEGORIES] PRIMARY KEY CLUSTERED ([Id] ASC)
);


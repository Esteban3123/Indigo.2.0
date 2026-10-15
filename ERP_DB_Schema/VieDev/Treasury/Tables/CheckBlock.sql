CREATE TABLE [Treasury].[CheckBlock] (
    [Id]          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdCheckbook] INT          NOT NULL,
    [CheckNumber] BIGINT       NOT NULL,
    [CodUser]     VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_CheckBlock] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CheckBlock_Checkbooks] FOREIGN KEY ([IdCheckbook]) REFERENCES [Treasury].[Checkbooks] ([Id])
);


GO
ALTER TABLE [Treasury].[CheckBlock] NOCHECK CONSTRAINT [FK_CheckBlock_Checkbooks];


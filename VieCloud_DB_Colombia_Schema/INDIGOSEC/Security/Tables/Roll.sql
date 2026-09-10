CREATE TABLE [Security].[Roll] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [RollCode]    VARCHAR (10) NOT NULL,
    [Description] VARCHAR (60) NOT NULL,
    [TimeStamp]   ROWVERSION   NOT NULL,
    [RollType]    TINYINT      NULL,
    CONSTRAINT [PK_Roll] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [IX_Roll] UNIQUE NONCLUSTERED ([RollCode] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de rol: 1:Global, 2:Por tenant', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Roll', @level2type = N'COLUMN', @level2name = N'RollType';


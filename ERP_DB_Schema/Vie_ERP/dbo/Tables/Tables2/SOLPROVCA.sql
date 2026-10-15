CREATE TABLE [dbo].[SOLPROVCA] (
    [PROCATAUT] TINYINT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODPROVE]  NUMERIC (18) NOT NULL,
    [CODCATE]   INT          NOT NULL,
    CONSTRAINT [PK_SOLProvCat] PRIMARY KEY CLUSTERED ([PROCATAUT] ASC),
    CONSTRAINT [FK_SOLPROVCAT_SOLCAT] FOREIGN KEY ([CODCATE]) REFERENCES [dbo].[SOLCATEGO] ([CATEAUTON]),
    CONSTRAINT [FK_SOLPROVCAT_SOLPROVE] FOREIGN KEY ([CODPROVE]) REFERENCES [dbo].[SOLPROVEE] ([PROVAUTO])
);


GO
ALTER TABLE [dbo].[SOLPROVCA] NOCHECK CONSTRAINT [FK_SOLPROVCAT_SOLCAT];


GO
ALTER TABLE [dbo].[SOLPROVCA] NOCHECK CONSTRAINT [FK_SOLPROVCAT_SOLPROVE];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo categoría', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVCA', @level2type = N'COLUMN', @level2name = N'CODCATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVCA', @level2type = N'COLUMN', @level2name = N'CODPROVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPROVCA', @level2type = N'COLUMN', @level2name = N'PROCATAUT';


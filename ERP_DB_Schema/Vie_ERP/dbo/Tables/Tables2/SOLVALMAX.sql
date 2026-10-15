CREATE TABLE [dbo].[SOLVALMAX] (
    [Auto]      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CATEAUTON] INT             NOT NULL,
    [CATDESCRI] VARCHAR (100)   NOT NULL,
    [UFUCODIGO] CHAR (10)       NOT NULL,
    [ValMAx]    NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_SOLVALMAX] PRIMARY KEY CLUSTERED ([Auto] ASC),
    CONSTRAINT [FK_SOLVALMAX_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_SOLVALMAX_SOLCATEGO] FOREIGN KEY ([CATEAUTON]) REFERENCES [dbo].[SOLCATEGO] ([CATEAUTON])
);


GO
ALTER TABLE [dbo].[SOLVALMAX] NOCHECK CONSTRAINT [FK_SOLVALMAX_INUNIFUNC];


GO
ALTER TABLE [dbo].[SOLVALMAX] NOCHECK CONSTRAINT [FK_SOLVALMAX_SOLCATEGO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor maximo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLVALMAX', @level2type = N'COLUMN', @level2name = N'ValMAx';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLVALMAX', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de categorias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLVALMAX', @level2type = N'COLUMN', @level2name = N'CATDESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de las categorias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLVALMAX', @level2type = N'COLUMN', @level2name = N'CATEAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLVALMAX', @level2type = N'COLUMN', @level2name = N'Auto';


CREATE TABLE [dbo].[SOLDETALL] (
    [DETAAUTON] INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [COMAUTON]  INT          NOT NULL,
    [PROAUTON]  INT          NOT NULL,
    [DETCANSOL] INT          NOT NULL,
    [CATEAUTON] INT          NOT NULL,
    [DETCANCOM] INT          NULL,
    [ORDCONSEC] VARCHAR (50) NULL,
    [AUTOCOTI]  INT          NULL,
    [DETCANAUT] INT          NULL,
    CONSTRAINT [PK_SOLDETALL] PRIMARY KEY CLUSTERED ([DETAAUTON] ASC),
    CONSTRAINT [FK_SOLDETALL_SOLCATEGO] FOREIGN KEY ([CATEAUTON]) REFERENCES [dbo].[SOLCATEGO] ([CATEAUTON]),
    CONSTRAINT [FK_SOLDETALL_SOLCOMPRA] FOREIGN KEY ([COMAUTON]) REFERENCES [dbo].[SOLCOMPRA] ([COMAUTON]),
    CONSTRAINT [FK_SOLDETALL_SOLDETALL] FOREIGN KEY ([DETAAUTON]) REFERENCES [dbo].[SOLDETALL] ([DETAAUTON]),
    CONSTRAINT [FK_SOLDETALL_SOLPRODUC] FOREIGN KEY ([PROAUTON]) REFERENCES [dbo].[SOLPRODUC] ([PRODAUTON])
);


GO
ALTER TABLE [dbo].[SOLDETALL] NOCHECK CONSTRAINT [FK_SOLDETALL_SOLCATEGO];


GO
ALTER TABLE [dbo].[SOLDETALL] NOCHECK CONSTRAINT [FK_SOLDETALL_SOLCOMPRA];


GO
ALTER TABLE [dbo].[SOLDETALL] NOCHECK CONSTRAINT [FK_SOLDETALL_SOLDETALL];


GO
ALTER TABLE [dbo].[SOLDETALL] NOCHECK CONSTRAINT [FK_SOLDETALL_SOLPRODUC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la cantidad autorizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'DETCANAUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el Autonumerico de la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'AUTOCOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el consecutivo de la orden de compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'ORDCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la cantidad comprada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'DETCANCOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico de la categoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'CATEAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'contiene  la cantidad de la solicitud del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'DETCANSOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'contiene el autonumerico del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'PROAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'contiene el Autonumerico de la solicitud de compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'COMAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDETALL', @level2type = N'COLUMN', @level2name = N'DETAAUTON';


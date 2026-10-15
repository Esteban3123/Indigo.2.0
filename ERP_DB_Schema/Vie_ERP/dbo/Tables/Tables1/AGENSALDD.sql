CREATE TABLE [dbo].[AGENSALDD] (
    [AUTONUMER] INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC] INT       NOT NULL,
    [CODSERIPS] CHAR (20) NOT NULL,
    [TIPOPARAM] NCHAR (1) NOT NULL,
    [PRODAUTON] INT       NULL,
    [CONCECREC] INT       NULL,
    CONSTRAINT [PK_AGENSALDD_1] PRIMARY KEY CLUSTERED ([AUTONUMER] ASC),
    CONSTRAINT [FK_AGENSALDD_AGERECURS] FOREIGN KEY ([CONCECREC]) REFERENCES [dbo].[AGERECURS] ([CODCONCEC]),
    CONSTRAINT [FK_AGENSALDD_SOLPRODUC] FOREIGN KEY ([PRODAUTON]) REFERENCES [dbo].[SOLPRODUC] ([PRODAUTON])
);


GO
ALTER TABLE [dbo].[AGENSALDD] NOCHECK CONSTRAINT [FK_AGENSALDD_AGERECURS];


GO
ALTER TABLE [dbo].[AGENSALDD] NOCHECK CONSTRAINT [FK_AGENSALDD_SOLPRODUC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'consecutivo del recurso, ya sea fisico o humano', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALDD', @level2type = N'COLUMN', @level2name = N'CONCECREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'consecutivo del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALDD', @level2type = N'COLUMN', @level2name = N'PRODAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TIPO PARAMETRO (H- HUMANO, F- FISICO, P- PRODUCTO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALDD', @level2type = N'COLUMN', @level2name = N'TIPOPARAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CÓDIGO SERVICIO IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALDD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código Consecutivo AGENSALAD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALDD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALDD', @level2type = N'COLUMN', @level2name = N'AUTONUMER';


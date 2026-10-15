CREATE TABLE [dbo].[ADEMPRESA] (
    [CODEMPRES] CHAR (5)     NOT NULL,
    [DESEMPRES] CHAR (80)    NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_ADEmpresa] PRIMARY KEY CLUSTERED ([CODEMPRES] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMPRESA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la Empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMPRESA', @level2type = N'COLUMN', @level2name = N'DESEMPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMPRESA', @level2type = N'COLUMN', @level2name = N'CODEMPRES';


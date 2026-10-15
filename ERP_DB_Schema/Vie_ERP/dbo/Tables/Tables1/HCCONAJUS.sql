CREATE TABLE [dbo].[HCCONAJUS] (
    [CODCONAJU] CHAR (3)     NOT NULL,
    [DESCODAJU] VARCHAR (50) NOT NULL,
    [ESTADOCOD] BIT          NOT NULL,
    CONSTRAINT [PK_HCCONAJUS] PRIMARY KEY CLUSTERED ([CODCONAJU] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del concepto de ajuste', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONAJUS', @level2type = N'COLUMN', @level2name = N'ESTADOCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del concepto de ajuste', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONAJUS', @level2type = N'COLUMN', @level2name = N'DESCODAJU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Concepto de Ajuste', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONAJUS', @level2type = N'COLUMN', @level2name = N'CODCONAJU';


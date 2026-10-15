CREATE TABLE [dbo].[INCAUMUER] (
    [CODCAUMUE] CHAR (3)     NOT NULL,
    [DESCAUMUE] CHAR (40)    NOT NULL,
    [EXICERDEF] BIT          NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    [CAMUERASO] CHAR (1)     NULL,
    CONSTRAINT [PK_INCAUMUER] PRIMARY KEY CLUSTERED ([CODCAUMUE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Causa de muerte asociada  - ERP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCAUMUER', @level2type = N'COLUMN', @level2name = N'CAMUERASO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo Reservado Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCAUMUER', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exigir certificado de defuncion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCAUMUER', @level2type = N'COLUMN', @level2name = N'EXICERDEF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la Causa de Muerte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCAUMUER', @level2type = N'COLUMN', @level2name = N'DESCAUMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Causa de Muerte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCAUMUER', @level2type = N'COLUMN', @level2name = N'CODCAUMUE';


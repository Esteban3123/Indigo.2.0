CREATE TABLE [dbo].[INUNIMEDI] (
    [CODUNIMED] VARCHAR (20)  NOT NULL,
    [DESUNIMED] VARCHAR (100) NOT NULL,
    [ABRUNIMED] CHAR (10)     NULL,
    [TIPUNIDAD] CHAR (1)      NULL,
    [INDAUDFOR] NUMERIC (18)  NOT NULL,
    [CODHOMHV]  VARCHAR (20)  NULL,
    CONSTRAINT [PK_INUNIMEDI] PRIMARY KEY CLUSTERED ([CODUNIMED] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo que pasa a health vault en la integración con smarth health', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIMEDI', @level2type = N'COLUMN', @level2name = N'CODHOMHV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIMEDI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1- Peso 2- Volumen 3- Unidad Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIMEDI', @level2type = N'COLUMN', @level2name = N'TIPUNIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Abreviatura Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIMEDI', @level2type = N'COLUMN', @level2name = N'ABRUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIMEDI', @level2type = N'COLUMN', @level2name = N'DESUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INUNIMEDI', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


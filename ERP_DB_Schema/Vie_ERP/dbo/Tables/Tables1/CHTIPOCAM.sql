CREATE TABLE [dbo].[CHTIPOCAM] (
    [CODTIPCAM] CHAR (3)     NOT NULL,
    [DESTIPCAM] CHAR (40)    NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_CHTIPOCAM] PRIMARY KEY CLUSTERED ([CODTIPCAM] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOCAM', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Tipo de Cama    Nota: Este campo permite identificar el tipo de cama, ejemplo Cama Estandar, Cama con Control, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOCAM', @level2type = N'COLUMN', @level2name = N'DESTIPCAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Tipo de Cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPOCAM', @level2type = N'COLUMN', @level2name = N'CODTIPCAM';


CREATE TABLE [dbo].[HCCATDOCU] (
    [CODCATEGO] CHAR (3)      NOT NULL,
    [DESCATEGO] VARCHAR (100) NOT NULL,
    [TIPDOCUME] CHAR (1)      NULL,
    [ESTCATDOC] BIT           NULL,
    CONSTRAINT [PK_HCCATDOCU] PRIMARY KEY CLUSTERED ([CODCATEGO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCATDOCU', @level2type = N'COLUMN', @level2name = N'ESTCATDOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Documento 0:Administrativo;1:Asistencial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCATDOCU', @level2type = N'COLUMN', @level2name = N'TIPDOCUME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la categoria del documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCATDOCU', @level2type = N'COLUMN', @level2name = N'DESCATEGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la categoria del documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCATDOCU', @level2type = N'COLUMN', @level2name = N'CODCATEGO';


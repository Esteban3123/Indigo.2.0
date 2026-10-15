CREATE TABLE [dbo].[ADCONEXCE] (
    [CODCONCEC] INT        NOT NULL,
    [CODENTIDA] CHAR (9)   NOT NULL,
    [NOMENTIDA] CHAR (150) NOT NULL,
    CONSTRAINT [PK_ADCONEXCE] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [CODENTIDA] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONEXCE', @level2type = N'COLUMN', @level2name = N'NOMENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONEXCE', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla de configuracion servicios suceptibles', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONEXCE', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


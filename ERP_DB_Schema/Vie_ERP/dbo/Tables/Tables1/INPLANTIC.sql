CREATE TABLE [dbo].[INPLANTIC] (
    [CODPLANTI] CHAR (5)     NOT NULL,
    [DESPLANTI] CHAR (30)    NOT NULL,
    [INDIDMENU] CHAR (3)     NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_INPLANTIL] PRIMARY KEY CLUSTERED ([CODPLANTI] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTIC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Menu en INDIGO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTIC', @level2type = N'COLUMN', @level2name = N'INDIDMENU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la Plantilla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTIC', @level2type = N'COLUMN', @level2name = N'DESPLANTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Plantilla para Indigo Crystal.Net', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANTIC', @level2type = N'COLUMN', @level2name = N'CODPLANTI';


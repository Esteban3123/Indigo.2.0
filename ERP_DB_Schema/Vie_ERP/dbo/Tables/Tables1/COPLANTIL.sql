CREATE TABLE [dbo].[COPLANTIL] (
    [CODPLANTI] CHAR (3)   NOT NULL,
    [DESPLANTI] CHAR (120) NOT NULL,
    [TIPPLANTI] TINYINT    NOT NULL,
    CONSTRAINT [PK_COPLANTIL] PRIMARY KEY CLUSTERED ([CODPLANTI] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Plantilla:  1: Plantilla de Servicios IPS  2: Plantilla de Productos  3: Plantilla de Hospitalizacion  4: Plantilla de Requerimientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANTIL', @level2type = N'COLUMN', @level2name = N'TIPPLANTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de Plantillas - Modulo de Contratos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANTIL', @level2type = N'COLUMN', @level2name = N'DESPLANTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Plantillas - Modulo de Contratos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANTIL', @level2type = N'COLUMN', @level2name = N'CODPLANTI';


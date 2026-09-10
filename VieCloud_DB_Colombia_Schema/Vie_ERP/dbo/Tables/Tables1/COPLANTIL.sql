CREATE TABLE [dbo].[COPLANTIL] (
    [CODPLANTI] CHAR (3)   NOT NULL,
    [DESPLANTI] CHAR (120) NOT NULL,
    [TIPPLANTI] TINYINT    NOT NULL,
    CONSTRAINT [PK_COPLANTIL] PRIMARY KEY CLUSTERED ([CODPLANTI] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Plantilla (TINYINT): 1=Servicios IPS, 2=Productos, 3=Hospitalización, 4=Requerimientos. Clasificación del modelo de contrato o atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANTIL', @level2type = N'COLUMN', @level2name = N'TIPPLANTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Plantilla:  1: Plantilla de Servicios IPS  2: Plantilla de Productos  3: Plantilla de Hospitalizacion  4: Plantilla de Requerimientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANTIL', @level2type = N'COLUMN', @level2name = N'TIPPLANTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANTIL', @level2type = N'COLUMN', @level2name = N'TIPPLANTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de Plantillas (VARCHAR 120) - Módulo de Contratos. Nombre o detalle del template de servicios, productos, hospitalización o requerimientos para gestión de acuerdos comerciales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANTIL', @level2type = N'COLUMN', @level2name = N'DESPLANTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de Plantillas - Modulo de Contratos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANTIL', @level2type = N'COLUMN', @level2name = N'DESPLANTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANTIL', @level2type = N'COLUMN', @level2name = N'DESPLANTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Plantilla (CHAR 3, PK) - Módulo de Contratos. Identificador único del template de contrato, servicios o productos. Clave primaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANTIL', @level2type = N'COLUMN', @level2name = N'CODPLANTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Plantillas - Modulo de Contratos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANTIL', @level2type = N'COLUMN', @level2name = N'CODPLANTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANTIL', @level2type = N'COLUMN', @level2name = N'CODPLANTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de plantillas del sistema. Registra los tipos de plantillas disponibles (por ejemplo, plantillas de documentos clínicos, formularios o reportes), con su código, descripción y clasificación por tipo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANTIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COPLANTIL';

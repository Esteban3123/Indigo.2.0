CREATE TABLE [dbo].[CHOMUECUL] (
    [CODMUECUL] CHAR (3)  NOT NULL,
    [DESMUECUL] CHAR (40) NOT NULL,
    CONSTRAINT [PK_CHOMUECUL] PRIMARY KEY CLUSTERED ([CODMUECUL] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la muestra cultivada para diagnóstico de infección intrahospitalaria. Texto identificador del tipo de cultivo, sitio de procedencia (sangre, orina, herida, secreción) y microorganismo detectado. Almacenado como CHAR(40). Utilizado en laboratorio clínico, microbiología y control de infecciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOMUECUL', @level2type = N'COLUMN', @level2name = N'DESMUECUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Muestra Cultivada - Infeccion Intrahospitalaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOMUECUL', @level2type = N'COLUMN', @level2name = N'DESMUECUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOMUECUL', @level2type = N'COLUMN', @level2name = N'DESMUECUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de tres caracteres (CHAR 3) que identifica la muestra cultivada en estudios de infección intrahospitalaria. Clave primaria. Sinónimos: código de cultivo, identificador de muestra, referencia de aislamiento. Utilizado en órdenes de laboratorio, RIPS y reportes epidemiológicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOMUECUL', @level2type = N'COLUMN', @level2name = N'CODMUECUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Muestra Cultivada - Infeccion Intrahospitalaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOMUECUL', @level2type = N'COLUMN', @level2name = N'CODMUECUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOMUECUL', @level2type = N'COLUMN', @level2name = N'CODMUECUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos o cultivos de muestras clínicas (por ejemplo: sangre, orina, esputo). Permite clasificar las muestras tomadas en laboratorio clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOMUECUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOMUECUL';

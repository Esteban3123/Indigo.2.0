CREATE TABLE [dbo].[HCTIPENCC] (
    [AUTO]       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NOMENCENF]  VARCHAR (200) NOT NULL,
    [GENEROAPLI] INT           NULL,
    [UNIEDADMI]  INT           NULL,
    [EDADMINIM]  INT           NULL,
    [UNIEDADMA]  INT           NULL,
    [EDADMAXIM]  INT           NULL,
    [ESTADOENC]  INT           NULL,
    CONSTRAINT [PK_HCTIPENCC] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro: 1=Activo (encuesta vigente), 2=Inactivo (encuesta descontinuada); controla disponibilidad operativa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'ESTADOENC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado  1 activo -  2 inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'ESTADOENC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'ESTADOENC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad máxima permitida para aplicar la lista de chequeo; umbral superior de rango etario (válido en poblaciones específicas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'EDADMAXIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Maxima en la que aplica  la lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'EDADMAXIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'EDADMAXIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida para edad máxima de aplicación: 1=Años, 2=Meses, 3=Días; dimensión temporal que complementa EDADMAXIM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'UNIEDADMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de Medida para Edad Maxima: 1: Años    2: Meses    3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'UNIEDADMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'UNIEDADMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad mínima requerida para aplicar la lista de chequeo de enfermería; umbral inferior de rango etario (pediatría/adultos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'EDADMINIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Minima en la que aplica la lista de Chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'EDADMINIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'EDADMINIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida para edad mínima de aplicación: 1=Años, 2=Meses, 3=Días; dimensión temporal que complementa EDADMINIM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'UNIEDADMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de Medida para Edad Minima:  1: Años    2: Meses    3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'UNIEDADMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'UNIEDADMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sexo/género al cual aplica la encuesta: 1=Masculino, 2=Femenino, 3=Aplica para ambos; filtro demográfico de la lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'GENEROAPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sexo Aplicacion Encuesta 1 Masculino  - 2 femenino - 3  Aplica para Ambas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'GENEROAPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'GENEROAPLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción de la lista de chequeo de enfermería (VARCHAR 200), etiqueta que identifica el tipo de encuesta clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'NOMENCENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Lista chekeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'NOMENCENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'NOMENCENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumerado (identity) que funciona como clave primaria de la tabla de tipos de encuestas/listas de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna autonumerica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipos de encuestas o formularios de enfermería clínica, con sus criterios de aplicación según género y rango de edad del paciente, y su estado de activación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC';

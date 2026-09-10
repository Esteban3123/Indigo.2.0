CREATE TABLE [dbo].[DIM_GRUPO_ETAREO] (
    [ID_GRUPO_ETAREO]          INT            IDENTITY (0, 1) NOT FOR REPLICATION NOT NULL,
    [GRUPO_ETAREO_EDAD]        FLOAT (53)     NOT NULL,
    [GRUPO_ETAREO_RES_5268]    NVARCHAR (100) NOT NULL,
    [GRUPO_ETAREO_UPC]         NVARCHAR (100) NULL,
    [GRUPO_ETAREO_CICLO_VITAL] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_GRUPO_ETAREO] PRIMARY KEY CLUSTERED ([ID_GRUPO_ETAREO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciclo vital del paciente (NVARCHAR 100): etapa del curso de vida (infancia, adolescencia, adultez, vejez, etc.) según clasificación demográfica y epidemiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DIM_GRUPO_ETAREO', @level2type = N'COLUMN', @level2name = N'GRUPO_ETAREO_CICLO_VITAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el ciclo vital', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DIM_GRUPO_ETAREO', @level2type = N'COLUMN', @level2name = N'GRUPO_ETAREO_CICLO_VITAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DIM_GRUPO_ETAREO', @level2type = N'COLUMN', @level2name = N'GRUPO_ETAREO_CICLO_VITAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de Pago por Capacitación (NVARCHAR 100, nullable): clasificación de grupo etáreo según normativa UPC para facturación y gestión de recursos en salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DIM_GRUPO_ETAREO', @level2type = N'COLUMN', @level2name = N'GRUPO_ETAREO_UPC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la unidad de pago por capacitación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DIM_GRUPO_ETAREO', @level2type = N'COLUMN', @level2name = N'GRUPO_ETAREO_UPC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DIM_GRUPO_ETAREO', @level2type = N'COLUMN', @level2name = N'GRUPO_ETAREO_UPC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resolución 5268 - Grupo etáreo (NVARCHAR 100): clasificación oficial de rango de edad según la Resolución 5268 del Ministerio de Salud, requerida para reportes RIPS y gestión sanitaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DIM_GRUPO_ETAREO', @level2type = N'COLUMN', @level2name = N'GRUPO_ETAREO_RES_5268';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la resolución 5268', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DIM_GRUPO_ETAREO', @level2type = N'COLUMN', @level2name = N'GRUPO_ETAREO_RES_5268';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DIM_GRUPO_ETAREO', @level2type = N'COLUMN', @level2name = N'GRUPO_ETAREO_RES_5268';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad representativa del grupo etáreo (FLOAT): valor numérico en años que identifica o promedia el rango de edad para categorización de pacientes en atenciones, diagnósticos y procedimientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DIM_GRUPO_ETAREO', @level2type = N'COLUMN', @level2name = N'GRUPO_ETAREO_EDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la edad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DIM_GRUPO_ETAREO', @level2type = N'COLUMN', @level2name = N'GRUPO_ETAREO_EDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DIM_GRUPO_ETAREO', @level2type = N'COLUMN', @level2name = N'GRUPO_ETAREO_EDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo del grupo etáreo (INT IDENTITY 0,1): clave primaria única que identifica cada categoría de rango de edad en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DIM_GRUPO_ETAREO', @level2type = N'COLUMN', @level2name = N'ID_GRUPO_ETAREO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DIM_GRUPO_ETAREO', @level2type = N'COLUMN', @level2name = N'ID_GRUPO_ETAREO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DIM_GRUPO_ETAREO', @level2type = N'COLUMN', @level2name = N'ID_GRUPO_ETAREO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de grupos etarios o rangos de edad utilizados para segmentar pacientes según edad, ciclo vital (niño, joven, adulto, adulto mayor) y las categorías definidas por la Resolución 5268 y la UPC (Unidad de Pago por Capitación) del sistema de salud colombiano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DIM_GRUPO_ETAREO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DIM_GRUPO_ETAREO';

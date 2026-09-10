CREATE TABLE [dbo].[HCCUSTOMI] (
    [AUTO]            INT                                      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODTIPHIS]       CHAR (3)                                 NOT NULL,
    [DESTIPHIS]       CHAR (60)                                NOT NULL,
    [IDETIPHIS]       CHAR (9)                                 NOT NULL,
    [FORTIPHIS]       CHAR (1)                                 NOT NULL,
    [ANALISIS]        BIT MASKED WITH (FUNCTION = 'default()') NOT NULL,
    [SUBJETIVO]       BIT                                      NOT NULL,
    [DIAGNOSTICOS]    BIT                                      NOT NULL,
    [EXAMENFISICO]    BIT MASKED WITH (FUNCTION = 'default()') NOT NULL,
    [ORDENESMEDICAS]  BIT                                      NOT NULL,
    [ANTECEDENTES]    BIT                                      NOT NULL,
    [PANALISIS]       INT MASKED WITH (FUNCTION = 'default()') NULL,
    [PSUBJETIVO]      INT                                      NULL,
    [PDIAGNOSTICOS]   INT                                      NULL,
    [PEXAMENFISICO]   INT MASKED WITH (FUNCTION = 'default()') NULL,
    [PORDENESMEDICAS] INT                                      NULL,
    [PANTECEDENTES]   INT                                      NULL,
    CONSTRAINT [PK_HCCUSTOMI] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCCUSTOMI].[ANALISIS]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCCUSTOMI].[EXAMENFISICO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCCUSTOMI].[PANALISIS]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCCUSTOMI].[PEXAMENFISICO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de página o sección para antecedentes en historia clínica. INT, configuración de layout de formulario personalizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'PANTECEDENTES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'page antecedentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'PANTECEDENTES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'PANTECEDENTES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de página o sección para órdenes médicas en historia clínica. INT, configuración de layout de formulario personalizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'PORDENESMEDICAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'page d eordenes medicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'PORDENESMEDICAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'PORDENESMEDICAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de página o sección para examen físico en historia clínica. INT, enmascarado, configuración de layout.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'PEXAMENFISICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'page examen fisico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'PEXAMENFISICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'PEXAMENFISICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de página o sección para diagnósticos en historia clínica. INT, configuración de layout de formulario personalizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'PDIAGNOSTICOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'page de diagnosticos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'PDIAGNOSTICOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'PDIAGNOSTICOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de página o sección para síntomas/relato subjetivo del paciente en historia clínica. INT, configuración de layout.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'PSUBJETIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'page subjetivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'PSUBJETIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'PSUBJETIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de página o sección para análisis clínicos/resultados de laboratorio en historia clínica. INT, enmascarado, configuración de layout.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'PANALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'page de análisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'PANALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'PANALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) de inclusión de sección antecedentes personales y médicos en historia clínica personalizada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'ANTECEDENTES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'antecedentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'ANTECEDENTES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'ANTECEDENTES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) de inclusión de sección órdenes médicas, prescripciones y procedimientos en historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'ORDENESMEDICAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ordenes médicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'ORDENESMEDICAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'ORDENESMEDICAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) de inclusión de sección examen físico en historia clínica personalizada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'EXAMENFISICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'examen fisico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'EXAMENFISICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'EXAMENFISICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) de inclusión de sección diagnósticos (impresión clínica, CIE10) en historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'DIAGNOSTICOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'diagnosticos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'DIAGNOSTICOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'DIAGNOSTICOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) de inclusión de sección subjetivo (motivo de consulta, síntomas, antecedente enfermedad actual) en historia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'SUBJETIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'subjetivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'SUBJETIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'SUBJETIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT), enmascarado, de inclusión de sección análisis clínicos y resultados de laboratorio en historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'ANALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Análisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'ANALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'ANALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código formato tipo de historia clínica: I=Ingreso, E=Evolución, O=Otros formatos de apoyo. CHAR(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'FORTIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Formato Tipo de Historia Clinica  I: Ingreso  E: Evolucion  O: Otros formatos de apoyo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'FORTIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'FORTIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno único del tipo de historia clínica (ingreso, evolución, valoración especializada). CHAR(9).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual del tipo de historia clínica (ej: Historia de Ingreso, Evolución Diaria, Nota de Urgencia). CHAR(60).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'DESTIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion Tipo de Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'DESTIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'DESTIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del tipo de historia clínica personalizado. CHAR(3), clave primaria conceptual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'CODTIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Tipo de Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'CODTIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'CODTIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único (PRIMARY KEY CLUSTERED). INT IDENTITY(1,1), generado automáticamente por SQL Server.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración personalizada de los tipos de historia clínica: define qué secciones (análisis, subjetivo, diagnósticos, examen físico, órdenes médicas, antecedentes) están habilitadas para cada tipo de historia, y el orden de presentación de cada sección en la interfaz clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCUSTOMI';

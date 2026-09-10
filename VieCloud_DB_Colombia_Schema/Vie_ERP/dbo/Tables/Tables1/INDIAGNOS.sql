CREATE TABLE [dbo].[INDIAGNOS] (
    [CODDIAGNO]        CHAR (4)     NOT NULL,
    [NOMDIAGNO]        CHAR (350)   NULL,
    [UNIEDADMI]        INT          NOT NULL,
    [EDADMINIM]        NUMERIC (3)  NOT NULL,
    [UNIEDADMA]        INT          NOT NULL,
    [EDADMAXIM]        NUMERIC (3)  NOT NULL,
    [APLICAMAS]        INT          NOT NULL,
    [APLICAFEM]        INT          NOT NULL,
    [EXIGENOTI]        BIT          NOT NULL,
    [CODICIE10]        CHAR (4)     NULL,
    [INDAUDFOR]        NUMERIC (18) NOT NULL,
    [EGREDIAGNO]       BIT          NOT NULL,
    [DIAGNOANT]        BIT          NULL,
    [ESTADO]           BIT          NULL,
    [DIASINCAMAX]      NUMERIC (3)  NULL,
    [DiagnosticType]   INT          DEFAULT ('1') NOT NULL,
    [DiagnosticFather] VARCHAR (20) NULL,
    CONSTRAINT [PK_INDIAGNOS] PRIMARY KEY CLUSTERED ([CODDIAGNO] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_INDIAGNOS_CODDIAGNO_NOMDIAGNO]
    ON [dbo].[INDIAGNOS]([CODDIAGNO] ASC, [NOMDIAGNO] ASC);


GO
CREATE NONCLUSTERED INDEX [_dta_index_INDIAGNOS_8_1803153469__K1_2]
    ON [dbo].[INDIAGNOS]([CODDIAGNO] ASC)
    INCLUDE([NOMDIAGNO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días de incapacidad máxima permitida para el diagnóstico (NUMERIC(3), 0-999 días). Usado para validar licencias médicas y períodos de restricción laboral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'DIASINCAMAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dias de incapacidad maxima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'DIASINCAMAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'DIASINCAMAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo del diagnóstico en el catálogo (BIT: 1=Activo, 0=Inactivo). Controla disponibilidad para nuevos registros clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de diagnóstico antecedente o condición preexistente del paciente (BIT: 1=Sí es antecedente, 0=No). Distingue diagnósticos históricos de activos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'DIAGNOANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diagnostico antecedente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'DIAGNOANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'DIAGNOANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prohibición de egreso con este diagnóstico como principal (BIT: 1=No permite egreso, 0=Permite egreso). Validación de coherencia clínica en cierre de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'EGREDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'No permitir egreso como diagnostico principal 1: No lo permite , 0: Si lo permite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'EGREDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'EGREDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o índice de auditoría y control de registro (NUMERIC(18)). Trazabilidad de creación/modificación para auditoría clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CIE-10 (Clasificación Internacional de Enfermedades v10) para el diagnóstico (CHAR(4)). Mapeo a estándares internacionales y RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'CODICIE10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo CIE10', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'CODICIE10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'CODICIE10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Exige notificación obligatoria a salud pública (BIT: 1=Sí, 0=No). Enfermedades de reporte obligatorio, vigilancia epidemiológica, notificación SIVIGILA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'EXIGENOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Exige Notificacion Obligatoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'EXIGENOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'EXIGENOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aplicabilidad del diagnóstico al sexo femenino (INT: 1=Aplica, 0=No aplica). Validación de coherencia clínica por género.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'APLICAFEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aplica al Sexo Femenino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'APLICAFEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'APLICAFEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aplicabilidad del diagnóstico al sexo masculino (INT: 1=Aplica, 0=No aplica). Validación de coherencia clínica por género.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'APLICAMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aplica al Sexo Masculino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'APLICAMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'APLICAMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad máxima permitida para aplicar este diagnóstico (NUMERIC(3)). Parámetro superior de rango etario clínico (unidad en UNIEDADMA).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'EDADMAXIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Minima en la que aplica el Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'EDADMAXIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'EDADMAXIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida para edad máxima (INT: 1=Años, 2=Meses, 3=Días). Define escala temporal para EDADMAXIM.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'UNIEDADMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de Medida para Edad Minima:  1: Años  2: Meses  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'UNIEDADMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'UNIEDADMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad mínima permitida para aplicar este diagnóstico (NUMERIC(3)). Parámetro inferior de rango etario clínico (unidad en UNIEDADMI).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'EDADMINIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Minima en la que aplica el Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'EDADMINIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'EDADMINIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida para edad mínima (INT: 1=Años, 2=Meses, 3=Días). Define escala temporal para EDADMINIM.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'UNIEDADMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de Medida para Edad Minima:  1: Años  2: Meses  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'UNIEDADMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'UNIEDADMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción clínica del diagnóstico (CHAR(350)). Denominación médica, síndrome, enfermedad o condición de salud registrada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'NOMDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'NOMDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'NOMDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del diagnóstico en el catálogo (CHAR(4), PK). Identificador del diagnóstico dentro del ERP/EHR Indigo Vie Cloud, correlaciona con CIE-10.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo maestro de diagnósticos clínicos (CIE-10) habilitados en el sistema. Contiene los códigos y nombres de diagnósticos, restricciones por edad y sexo, configuraciones de notificación obligatoria y jerarquía diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de diagnóstico: indica la categoría o clasificación del diagnóstico (por ejemplo, principal, secundario, complicación). Valor por defecto 1.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'DiagnosticType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'DiagnosticType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico padre o diagnóstico superior en la jerarquía; permite agrupar diagnósticos bajo un concepto clínico común.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'DiagnosticFather';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOS', @level2type = N'COLUMN', @level2name = N'DiagnosticFather';

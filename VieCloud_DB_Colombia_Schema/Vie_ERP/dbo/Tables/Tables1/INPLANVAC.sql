CREATE TABLE [dbo].[INPLANVAC] (
    [IDPLAVACU]    CHAR (4)     NOT NULL,
    [EDAREGVAC]    CHAR (80)    NOT NULL,
    [TIPVACUNA]    CHAR (120)   NOT NULL,
    [NUMDOSISV]    CHAR (60)    NOT NULL,
    [ESTPLAVAC]    CHAR (1)     NOT NULL,
    [INDAUDFOR]    NUMERIC (18) NOT NULL,
    [NUMPOSGRU]    SMALLINT     NOT NULL,
    [ESTRUCNUEVA]  BIT          CONSTRAINT [DF_INPLANVAC_ESTRUCNUEVA] DEFAULT ((0)) NOT NULL,
    [ESTADOVACUNA] BIT          NULL,
    CONSTRAINT [PK_INPLANVAC] PRIMARY KEY CLUSTERED ([IDPLAVACU] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la vacuna aplicada; BIT (0=Inactivo/No aplicado, 1=Activo/Aplicado). Bandera que indica si la dosis fue administrada al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'ESTADOVACUNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda estado de vacuna  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'ESTADOVACUNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'ESTADOVACUNA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Esquema de vacunación; 1=Nuevo esquema, 0=Esquema antiguo. Identifica la versión del protocolo de inmunización utilizado en el plan.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'ESTRUCNUEVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Esquema de vacunas 1=Nuevo, 0=Antiguo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'ESTRUCNUEVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'ESTRUCNUEVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de posición dentro del grupo de vacunación; SMALLINT. Orden secuencial de la dosis en el esquema de vacunación del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'NUMPOSGRU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Posicion del Grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'NUMPOSGRU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'NUMPOSGRU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de auditoría y formato; NUMERIC(18). Campo de control y trazabilidad para seguimiento administrativo y cumplimiento normativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CAmpo Auditoria  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del plan de vacunación; CHAR(1): A=Anulado, C=Confirmado. Indica si el plan fue ejecutado o descartado en la atención del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'ESTPLAVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Plan de Vacunacion  A: Anulado  C: Confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'ESTPLAVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'ESTPLAVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de dosis de vacuna requeridas; CHAR(60). Cantidad de aplicaciones programadas en el plan de inmunización (ej: dosis 1, dosis 2, refuerzo).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'NUMDOSISV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'NUMDOSISV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'NUMDOSISV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de vacuna aplicada; CHAR(120). Nombre o clasificación del biológico inmunizante (ej: Polio, BCG, DPT, COVID-19, hepatitis, influenza, neumococo).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'TIPVACUNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Vacuna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'TIPVACUNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'TIPVACUNA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad recomendada para aplicar la vacuna; CHAR(80). Rango etario del paciente en el que debe administrarse la inmunización según esquema nacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'EDAREGVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad en la que se debio aplicar la vacuna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'EDAREGVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'EDAREGVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del plan de vacunas; CHAR(4), PK. Clave primaria que referencia cada programa de inmunización del paciente en el historial de vacunación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'IDPLAVACU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Plan de Vacunas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'IDPLAVACU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC', @level2type = N'COLUMN', @level2name = N'IDPLAVACU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plan de vacunación: catálogo maestro de vacunas con sus rangos de edad, tipo de vacuna, número de dosis y estado de vigencia. Se usa para configurar el esquema de vacunación institucional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPLANVAC';

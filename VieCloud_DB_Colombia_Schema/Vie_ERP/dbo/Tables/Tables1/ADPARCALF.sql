CREATE TABLE [dbo].[ADPARCALF] (
    [CODCENATE] CHAR (10)    NOT NULL,
    [CODENTIDA] CHAR (9)     NOT NULL,
    [APLFESTR3] BIT          NOT NULL,
    [APLFESTR4] BIT          NOT NULL,
    [CONHORTRI] INT          NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_ADCALFEST] PRIMARY KEY CLUSTERED ([CODCENATE] ASC, [CODENTIDA] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría (NUMERIC 18), identificador único del registro de auditoría o trazabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control de horario para triage III y IV (INT): 1=Control por paciente, 2=Registro triage. Valida apertura de ingreso según calendario de no atenciones y horarios de festivos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF', @level2type = N'COLUMN', @level2name = N'CONHORTRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite establecer el parametro con el que se controla los parametros de apertura de Ingresos segun el calendario de no atenciones en TRIAGE III y IV.    1: Control Paciente  2: Registro TRIAGE    Nota: Este parametro tomara la hora segun la seleccion y la comparara contra el calendario para decidir si realiza apertura de ingreso o no.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF', @level2type = N'COLUMN', @level2name = N'CONHORTRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF', @level2type = N'COLUMN', @level2name = N'CONHORTRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aplicar horario festivo en triage IV (BIT): indicador si se aplica control horario a festivos en triaje nivel 4.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF', @level2type = N'COLUMN', @level2name = N'APLFESTR4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aplica horario a Festivos en TRIAGE 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF', @level2type = N'COLUMN', @level2name = N'APLFESTR4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF', @level2type = N'COLUMN', @level2name = N'APLFESTR4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aplicar horario festivo en triage III (BIT): indicador si se aplica control horario a festivos en triaje nivel 3.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF', @level2type = N'COLUMN', @level2name = N'APLFESTR3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aplica horario a Festivos en TRIAGE 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF', @level2type = N'COLUMN', @level2name = N'APLFESTR3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF', @level2type = N'COLUMN', @level2name = N'APLFESTR3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de entidad EAPB (CHAR 9), referencia a aseguradora/prestador. FK→INENTIDAD.CODENTIDA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Entidad EAPB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de centro de atención (CHAR 10): identifica la sede, clínica o punto de servicio donde ingresa el paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Centron de Atencion en donde Ingresa el Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de parámetros de facturación y auditoría por centro de atención y entidad aseguradora: controla la aplicación de estructuras tarifarias especiales y el horario de triage.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF';

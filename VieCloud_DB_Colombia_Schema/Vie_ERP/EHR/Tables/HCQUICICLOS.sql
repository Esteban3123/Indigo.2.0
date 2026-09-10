CREATE TABLE [EHR].[HCQUICICLOS] (
    [ID]            INT                                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCQUIORDENC] INT                                                                           NOT NULL,
    [CICLO]         INT                                                                           NOT NULL,
    [SEMANA]        INT                                                                           NOT NULL,
    [DIA]           INT                                                                           NOT NULL,
    [ESTADOCICLO]   INT                                                                           NOT NULL,
    [NUMEFOLIO]     NCHAR (10)                                                                    NOT NULL,
    [IPCODPACI]     CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    CONSTRAINT [PK_HCQUICICLOS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [EHR].[HCQUICICLOS].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciclos de tratamiento de quimioterapia asignados a órdenes médicas oncológicas. Registra cada ciclo, semana y día de aplicación de un esquema de quimioterapia para un paciente.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUICICLOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUICICLOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno único del registro de ciclo de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUICICLOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUICICLOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la orden de quimioterapia (esquema oncológico) a la que pertenece este ciclo.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUICICLOS', @level2type = N'COLUMN', @level2name = N'IDHCQUIORDENC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUICICLOS', @level2type = N'COLUMN', @level2name = N'IDHCQUIORDENC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ciclo de quimioterapia dentro del esquema de tratamiento (ej: ciclo 1, ciclo 2).', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUICICLOS', @level2type = N'COLUMN', @level2name = N'CICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUICICLOS', @level2type = N'COLUMN', @level2name = N'CICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de semana dentro del ciclo de quimioterapia en que se aplica el tratamiento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUICICLOS', @level2type = N'COLUMN', @level2name = N'SEMANA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUICICLOS', @level2type = N'COLUMN', @level2name = N'SEMANA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día específico dentro de la semana del ciclo en que se administra la quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUICICLOS', @level2type = N'COLUMN', @level2name = N'DIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUICICLOS', @level2type = N'COLUMN', @level2name = N'DIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual del ciclo de quimioterapia (ej: pendiente, en curso, completado, suspendido).', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUICICLOS', @level2type = N'COLUMN', @level2name = N'ESTADOCICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUICICLOS', @level2type = N'COLUMN', @level2name = N'ESTADOCICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio o consecutivo del documento asociado al ciclo de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUICICLOS', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUICICLOS', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cédula del paciente, identificación o documento de identidad del paciente oncológico al que pertenece el ciclo de tratamiento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUICICLOS', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUICICLOS', @level2type = N'COLUMN', @level2name = N'IPCODPACI';

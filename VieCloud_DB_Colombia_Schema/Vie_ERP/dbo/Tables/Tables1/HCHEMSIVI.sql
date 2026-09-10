CREATE TABLE [dbo].[HCHEMSIVI] (
    [CONSECUTI]  NUMERIC (18)                                                             NOT NULL,
    [REGREGIST]  DATETIME                                                                 NOT NULL,
    [VALORPSPD]  CHAR (10)                                                                NOT NULL,
    [VALFRECARD] CHAR (10) MASKED WITH (FUNCTION = 'partial(0, "HeartRate_Ofuscado", 0)') NOT NULL,
    [PMCONDUTI]  CHAR (10)                                                                NOT NULL,
    [PMFLUJBOM]  CHAR (10)                                                                NOT NULL,
    [NUVALORPA]  CHAR (10)                                                                NOT NULL,
    [NUVALORPV]  CHAR (10)                                                                NOT NULL,
    [NVALORPTM]  CHAR (10)                                                                NOT NULL,
    [OBSERVACI]  CHAR (400)                                                               NULL,
    CONSTRAINT [PK_HCHEMSIVI] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC, [REGREGIST] ASC),
    CONSTRAINT [FK_HCHEMSIVI_HCHEMODIA] FOREIGN KEY ([CONSECUTI]) REFERENCES [dbo].[HCHEMODIA] ([CONSECUTI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCHEMSIVI].[VALFRECARD]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas o notas adicionales del registro de hemodiálisis, texto libre (CHAR 400)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PTM - Presión Transmembrana, parámetro de diálisis (CHAR 10, numérico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'NVALORPTM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'PTM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'NVALORPTM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'NVALORPTM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PV - Presión Venosa, medición de presión en acceso vascular durante hemodiálisis (CHAR 10, numérico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'NUVALORPV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'PV', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'NUVALORPV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'NUVALORPV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PA - Presión Arterial, medición de presión en línea arterial de acceso vascular (CHAR 10, numérico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'NUVALORPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'PA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'NUVALORPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'NUVALORPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flujo de Bomba, velocidad de flujo sanguíneo en mL/min durante sesión de hemodiálisis (CHAR 10, numérico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'PMFLUJBOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Flujo de Bomba', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'PMFLUJBOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'PMFLUJBOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conductividad, conductancia del dialisado en sesión de hemodiálisis (CHAR 10, numérico, mS/cm)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'PMCONDUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Conductividad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'PMCONDUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'PMCONDUTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia Cardiaca, pulso del paciente en latidos por minuto durante hemodiálisis (CHAR 10, PII - Identificación_Ofuscado, DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'VALFRECARD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia Cardiaca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'VALFRECARD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'VALFRECARD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PS/PD - Presión Sistólica/Presión Diastólica, mediciones de tensión arterial del paciente (CHAR 10, numérico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'VALORPSPD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'PS/PD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'VALORPSPD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'VALORPSPD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del Registro, timestamp de captura de datos en sesión de hemodiálisis (DATETIME, PK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'REGREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'REGREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'REGREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de la Cabecera, identificador único enlazado a HCHEMODIA para seguimiento de sesión (NUMERIC 18, FK, PK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la Cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI', @level2type = N'COLUMN', @level2name = N'CONSECUTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de monitoreo hemodinámico y signos vitales de soporte invasivo (EMSI/VI) para pacientes críticos: guarda valores de presiones vasculares, frecuencia cardíaca, condición de la bomba y flujos, usados en seguimiento de pacientes con soporte circulatorio o monitoreo invasivo en historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMSIVI';

CREATE TABLE [dbo].[HCORHEMSV] (
    [ID]           INT                                                                            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCORHEMBOLID] INT                                                                            NOT NULL,
    [TIPO]         TINYINT                                                                        NOT NULL,
    [TA]           VARCHAR (7) MASKED WITH (FUNCTION = 'partial(0, "BloodPressure_Ofuscado", 0)') NOT NULL,
    [FC]           INT MASKED WITH (FUNCTION = 'default()')                                       NOT NULL,
    [T]            NUMERIC (18, 1) MASKED WITH (FUNCTION = 'default()')                           NOT NULL,
    [FECREGISTRO]  DATETIME                                                                       NOT NULL,
    [USUREGISTRO]  CHAR (20)                                                                      NOT NULL,
    [FR]           INT                                                                            NULL,
    [SO2]          INT                                                                            NULL,
    CONSTRAINT [PK_HCORHEMSV] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORHEMSV_HCORHEMBOL] FOREIGN KEY ([HCORHEMBOLID]) REFERENCES [dbo].[HCORHEMBOL] ([ID])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORHEMSV].[TA]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORHEMSV].[FC]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORHEMSV].[T]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia Respiratoria (respiraciones por minuto). Signo vital registrado durante monitoreo de transfusión de sangre. Valores normales 12-20 rpm.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'FR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia Respiratoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'FR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'FR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que registra. Identificación del profesional de salud (enfermero, médico, técnico) que documenta los signos vitales. PII sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'USUREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que registra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'USUREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'USUREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del registro. Timestamp DATETIME de cuando se capturan los signos vitales durante la transfusión hemática. Auditoría y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Feha del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Temperatura corporal en grados Celsius. Signo vital monitorizado durante transfusión de sangre. NUMERIC(18,1) enmascarado. Valores normales 36.5-37.5°C.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'T';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Temperatura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'T';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'T';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia Cardiaca (latidos por minuto). Signo vital clave en monitoreo de transfusión. INT enmascarado. Valores normales 60-100 lpm.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'FC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia Cardiaca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'FC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'FC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tensión Arterial (sistólica/diastólica en mmHg). Signo vital crítico durante hemotransfusión. VARCHAR(7) formato ''''XXX/XX''''. BloodPressure_Ofuscado. PII sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'TA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tensión Arterial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'TA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'TA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Registro: 1=Al iniciar (baseline pretransfusional), 2=Monitoreo (durante), 3=Al finalizar (posttransfusión). Clasificación de momento de captura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Registro: 1-> Al iniciar, 2-> Monitoreo, 3-> Al Finalizar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de bolsa de sangre. FK a tabla HCORHEMBOL. Trazabilidad de transfusión, compatibilidad con producto hemático, lote y vencimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'HCORHEMBOLID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de bolsa de sangre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'HCORHEMBOLID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'HCORHEMBOLID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autonumérico INT IDENTITY). Clave primaria de registro de signos vitales. Permite referenciar cada captura de constantes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumérico tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Signos vitales registrados durante la monitorización hemodinámica del paciente: presión arterial, frecuencia cardíaca, temperatura, frecuencia respiratoria y saturación de oxígeno, asociados a un evento hemático o de urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saturación de oxígeno en sangre (SpO2), porcentaje de oxigenación del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'SO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSV', @level2type = N'COLUMN', @level2name = N'SO2';

CREATE TABLE [dbo].[PostpartumCareFollowUpDetail] (
    [ID]                             INT             IDENTITY (1, 1) NOT NULL,
    [IdPostpartumCareFollowUp]       INT             NOT NULL,
    [DiastolicBloodPressure]         INT             NOT NULL,
    [SistolicBloodPressure]          INT             NOT NULL,
    [HeartRate]                      CHAR (10)       NOT NULL,
    [RespiratoryRate]                CHAR (10)       NOT NULL,
    [Temperature]                    CHAR (10)       NOT NULL,
    [OxygenSaturation]               CHAR (10)       NOT NULL,
    [Pain]                           INT             NOT NULL,
    [Size]                           NUMERIC (4, 1)  NOT NULL,
    [Weight]                         NUMERIC (18, 3) NOT NULL,
    [ConscienceState]                INT             NULL,
    [Breasts]                        INT             NULL,
    [UterineTone]                    BIT             NULL,
    [Bleeding]                       INT             NULL,
    [SurgicalWound]                  INT             NULL,
    [VulvarSutures]                  BIT             NULL,
    [EarlyAmbulation]                BIT             NULL,
    [ProperNutrition]                BIT             NULL,
    [Observations]                   VARCHAR (500)   NULL,
    [AdviceWasProvided]              BIT             NULL,
    [ContraceptiveWasProvided]       BIT             NULL,
    [HCTIPPLANCode]                  CHAR (2)        NULL,
    [ReceiveBreastfeedingCounseling] BIT             NULL,
    [SurveillanceObservations]       VARCHAR (1000)  NULL,
    CONSTRAINT [PK_PostpartumCareFollowUpDetail] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PostpartumCareFollowUpDetail_PostpartumCareFollowUp] FOREIGN KEY ([IdPostpartumCareFollowUp]) REFERENCES [dbo].[PostpartumCareFollowUp] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones del agrupador de vigilancia, notas de seguimiento y monitoreo epidemiológico posparto según protocolos RIPS o vigilancia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'SurveillanceObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena las observaciones del agrupador de vigilancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'SurveillanceObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'SurveillanceObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Recibe consejería en lactancia materna? (Verdadero=Sí, Falso=No), educación sobre amamantamiento en período posparto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'ReceiveBreastfeedingCounseling';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Recibe consejería en lactancia materna del agrupador de vigilancia
True - Si
False - No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'ReceiveBreastfeedingCounseling';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'ReceiveBreastfeedingCounseling';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del método anticonceptivo (FK a tabla HCTIPPLAN), tipo de contraceptivo seleccionado en seguimiento puerperal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'HCTIPPLANCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el código del método anticonceptivo seleccionado de la tabla HCTIPPLAN del agrupador de vigilancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'HCTIPPLANCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'HCTIPPLANCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Se suministró método anticonceptivo post evento obstétrico? (Verdadero=Sí, Falso=No), registro de entrega de anticonceptivo posparto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'ContraceptiveWasProvided';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se almacena el campo  ¿Se suministró anticonceptivo para el post evento obstétrico? del agrupador de vigilancia
True - Si
False - No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'ContraceptiveWasProvided';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'ContraceptiveWasProvided';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Se brindó asesoría anticonceptiva post evento obstétrico? (Verdadero=Sí, Falso=No), registro de consejería contraceptiva posparto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'AdviceWasProvided';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se almacena el campo ¿Se brindó asesoría anticonceptiva post evento obstétrico? del agrupador de vigilancia
True - Si
False - No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'AdviceWasProvided';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'AdviceWasProvided';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas generales, notas libres del examen o hallazgos relevantes en seguimiento puerperal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Alimentación adecuada (binario: sí/no), evaluación de ingesta nutricional materna posparto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'ProperNutrition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Alimentacion adecuada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'ProperNutrition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'ProperNutrition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deambulación temprana (binario: sí/no), indicador de movilización materna precoz en puerperio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'EarlyAmbulation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Deambulación temprana', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'EarlyAmbulation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'EarlyAmbulation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Suturas vulvares o perineales (presencia/ausencia), evaluación de trauma o reparación perineal postparto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'VulvarSutures';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Suturas vulvares', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'VulvarSutures';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'VulvarSutures';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Herida quirúrgica (si aplica cesárea o legrado), estado de cicatrización o infección en sitio quirúrgico posparto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'SurgicalWound';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Herida quirurgica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'SurgicalWound';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'SurgicalWound';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sangrado o hemorragia (evaluación clínica), registro de pérdida hemática vaginal en puerperio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Bleeding';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sangrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Bleeding';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Bleeding';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tono uterino (binario: presente/ausente), valoración de contratilidad y firmeza uterina postparto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'UterineTone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tono uterino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'UterineTone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'UterineTone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evaluación de mamas o senos (condición, sensibilidad, lactancia), hallazgos clínicos en período puerperal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Breasts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Senos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Breasts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Breasts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de conciencia o nivel de consciencia (1=consciente, 2=somnoliento, 3=inconsciente), valoración neurológica posparto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'ConscienceState';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de conciencia (1. conciente, 2. somnoliente, 3. inconsciente)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'ConscienceState';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'ConscienceState';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso (kg), registro de masa corporal materna en evaluación posparto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Weight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Weight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Weight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tamaño o estatura (cm), medida antropométrica materna durante seguimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Size';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tamaño', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Size';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Size';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice o escala de dolor (0-10), evaluación del dolor posparto o postquirúrgico en la puérpera.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Pain';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indice de dolor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Pain';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Pain';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saturación de oxígeno (SPO2, %), parámetro de oxigenación materna en puerperio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'OxygenSaturation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saturación de oxigeno (SPO2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'OxygenSaturation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'OxygenSaturation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Temperatura corporal (°C), signo vital registrado en evaluación posparto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Temperature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Temperatura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Temperature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Temperature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia respiratoria (respiraciones por minuto, rpm), parámetro vital en seguimiento puerperal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'RespiratoryRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia respiratoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'RespiratoryRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'RespiratoryRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia cardíaca (latidos por minuto, lpm), registro vital durante evaluación posparto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'HeartRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia cardíaca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'HeartRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'HeartRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presión arterial sistólica (mmHg), valor superior del registro de tensión durante seguimiento posparto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'SistolicBloodPressure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presion arterial sistolica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'SistolicBloodPressure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'SistolicBloodPressure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presión arterial diastólica (mmHg), valor inferior del registro de tensión durante seguimiento posparto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'DiastolicBloodPressure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presion arterial diastolica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'DiastolicBloodPressure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'DiastolicBloodPressure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) hacia tabla cabecera PostpartumCareFollowUp; vincula el detalle al seguimiento asistencial del puerperio o postparto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'IdPostpartumCareFollowUp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del seguimiento asistencial puerperio (tabla cabecera)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'IdPostpartumCareFollowUp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'IdPostpartumCareFollowUp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (consecutivo) de la tabla de detalle del seguimiento posparto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle clínico de cada control de seguimiento posparto: registra los signos vitales, hallazgos físicos, evolución de la herida quirúrgica, lactancia, planificación familiar y observaciones de vigilancia de la madre durante las visitas de seguimiento después del parto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail';

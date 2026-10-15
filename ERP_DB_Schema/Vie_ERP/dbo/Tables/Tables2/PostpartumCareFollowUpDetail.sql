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
ALTER TABLE [dbo].[PostpartumCareFollowUpDetail] NOCHECK CONSTRAINT [FK_PostpartumCareFollowUpDetail_PostpartumCareFollowUp];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena las observaciones del agrupador de vigilancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'SurveillanceObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el campo Recibe consejería en lactancia materna del agrupador de vigilancia
True - Si
False - No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'ReceiveBreastfeedingCounseling';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el código del método anticonceptivo seleccionado de la tabla HCTIPPLAN del agrupador de vigilancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'HCTIPPLANCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se almacena el campo  ¿Se suministró anticonceptivo para el post evento obstétrico? del agrupador de vigilancia
True - Si
False - No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'ContraceptiveWasProvided';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se almacena el campo ¿Se brindó asesoría anticonceptiva post evento obstétrico? del agrupador de vigilancia
True - Si
False - No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'AdviceWasProvided';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Alimentacion adecuada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'ProperNutrition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deambulación temprana', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'EarlyAmbulation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Suturas vulvares', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'VulvarSutures';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Herida quirurgica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'SurgicalWound';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sangrado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Bleeding';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tono uterino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'UterineTone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Senos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Breasts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de conciencia (1. conciente, 2. somnoliente, 3. inconsciente)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'ConscienceState';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Weight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tamaño', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Size';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indice de dolor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Pain';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saturación de oxigeno (SPO2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'OxygenSaturation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Temperatura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'Temperature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Frecuencia respiratoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'RespiratoryRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Frecuencia cardíaca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'HeartRate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presion arterial sistolica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'SistolicBloodPressure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presion arterial diastolica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'DiastolicBloodPressure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del seguimiento asistencial puerperio (tabla cabecera)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'IdPostpartumCareFollowUp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PostpartumCareFollowUpDetail', @level2type = N'COLUMN', @level2name = N'ID';


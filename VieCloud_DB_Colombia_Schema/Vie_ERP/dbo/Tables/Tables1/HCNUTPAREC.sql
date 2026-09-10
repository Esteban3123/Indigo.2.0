CREATE TABLE [dbo].[HCNUTPAREC] (
    [ID]                           INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCPARNUTC]                  INT                                                                              NOT NULL,
    [IDHCHISPACA]                  INT                                                                              NOT NULL,
    [CODPROSAL]                    CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [IPCODPACI]                    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                    CHAR (10)                                                                        NOT NULL,
    [FOLIORDEN]                    NCHAR (10)                                                                       NOT NULL,
    [FOLIOFIN]                     NCHAR (10)                                                                       NULL,
    [FECHAORDEN]                   DATETIME                                                                         NOT NULL,
    [PESOPACIE]                    NUMERIC (18, 2)                                                                  NOT NULL,
    [VOLUTOTAL]                    NUMERIC (18, 2)                                                                  NOT NULL,
    [VOLUADM]                      NUMERIC (18, 2)                                                                  NOT NULL,
    [TEMPOADMIN]                   INT                                                                              NOT NULL,
    [VELINFUSION]                  NUMERIC (18, 2)                                                                  NOT NULL,
    [VIADMIN]                      VARCHAR (20)                                                                     NOT NULL,
    [STATUS]                       INT                                                                              NOT NULL,
    [USERSUS]                      CHAR (20)                                                                        NULL,
    [SUSDATE]                      DATETIME                                                                         NULL,
    [AGRUPAQUETE]                  UNIQUEIDENTIFIER                                                                 NULL,
    [FECHAVERIFICA]                DATETIME                                                                         NULL,
    [CODPROSALVERIFICA]            CHAR (20)                                                                        NULL,
    [OBSERVERIFICA]                VARCHAR (5000)                                                                   NULL,
    [IDUNITDOSETYPE]               INT                                                                              NULL,
    [ReasonDiscontinuationOfDrug]  INT                                                                              NULL,
    [PatientRiskLevel]             INT                                                                              NULL,
    [PatientRiskLevelObservations] VARCHAR (1000)                                                                   NULL,
    [MOTSUSMED]                    VARCHAR (2000)                                                                   NULL,
    [IDTypesInfusionPumps]         INT                                                                              NULL,
    [TALLAPACIE]                   NUMERIC (18, 2)                                                                  NULL,
    [CODDIAGNO]                    CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    [NUMDIAS]                      INT                                                                              NULL,
    [LIQUIDOS]                     NUMERIC (18, 2)                                                                  NULL,
    [AGUAESTERIL]                  NUMERIC (18, 2)                                                                  NULL,
    [TIPOINFUSION]                 INT                                                                              NULL,
    [INDICACIONADM]                VARCHAR (300)                                                                    NULL,
    [INDICACIONADI]                VARCHAR (500)                                                                    NULL,
    [ValidityPrescription]         INT                                                                              NULL,
    [Justificacion]                VARCHAR (200)                                                                    NULL,
    [DateOfNutritionApplication]   DATETIME                                                                         NULL,
    [ApplyingProfessional]         CHAR (20)                                                                        NULL,
    [DateOfDiscardApplication]     DATETIME                                                                         NULL,
    [DiscardProfessional]          CHAR (20)                                                                        NULL,
    CONSTRAINT [PK__HCNUTPAR__3214EC27B6642FC2] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADINGRESO_NUTPAREC] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_CODPROSALVERIFICA] FOREIGN KEY ([CODPROSALVERIFICA]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCPARNUTC_NUTPAREC] FOREIGN KEY ([IDHCPARNUTC]) REFERENCES [dbo].[HCPARNUTC] ([ID]),
    CONSTRAINT [FK_HISPACA_NUTPAREC] FOREIGN KEY ([IDHCHISPACA]) REFERENCES [dbo].[HCHISPACA] ([ID]),
    CONSTRAINT [FK_IDUNITDOSETYPE] FOREIGN KEY ([IDUNITDOSETYPE]) REFERENCES [MixingStation].[UnitDoseType] ([Id]),
    CONSTRAINT [FK_INPACIENT_NUTPAREC] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_INPROFSAL_NUTPAREC] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[HCNUTPAREC] NOCHECK CONSTRAINT [FK_ADINGRESO_NUTPAREC];


GO
ALTER TABLE [dbo].[HCNUTPAREC] NOCHECK CONSTRAINT [FK_CODPROSALVERIFICA];


GO
ALTER TABLE [dbo].[HCNUTPAREC] NOCHECK CONSTRAINT [FK_HCPARNUTC_NUTPAREC];


GO
ALTER TABLE [dbo].[HCNUTPAREC] NOCHECK CONSTRAINT [FK_HISPACA_NUTPAREC];


GO
ALTER TABLE [dbo].[HCNUTPAREC] NOCHECK CONSTRAINT [FK_IDUNITDOSETYPE];


GO
ALTER TABLE [dbo].[HCNUTPAREC] NOCHECK CONSTRAINT [FK_INPACIENT_NUTPAREC];


GO
ALTER TABLE [dbo].[HCNUTPAREC] NOCHECK CONSTRAINT [FK_INPROFSAL_NUTPAREC];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCNUTPAREC].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCNUTPAREC].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCNUTPAREC].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO
ALTER TABLE [dbo].[HCNUTPAREC] NOCHECK CONSTRAINT [FK_ADINGRESO_NUTPAREC];


GO
ALTER TABLE [dbo].[HCNUTPAREC] NOCHECK CONSTRAINT [FK_CODPROSALVERIFICA];


GO
ALTER TABLE [dbo].[HCNUTPAREC] NOCHECK CONSTRAINT [FK_HCPARNUTC_NUTPAREC];


GO
ALTER TABLE [dbo].[HCNUTPAREC] NOCHECK CONSTRAINT [FK_HISPACA_NUTPAREC];


GO
ALTER TABLE [dbo].[HCNUTPAREC] NOCHECK CONSTRAINT [FK_IDUNITDOSETYPE];


GO
ALTER TABLE [dbo].[HCNUTPAREC] NOCHECK CONSTRAINT [FK_INPACIENT_NUTPAREC];


GO
ALTER TABLE [dbo].[HCNUTPAREC] NOCHECK CONSTRAINT [FK_INPROFSAL_NUTPAREC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código profesional de salud (CHAR 20) que descarta/rechaza la aplicación de dosis de nutrición parenteral (NPT); vinculado a tabla INPROFSAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'DiscardProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que descarta la aplicación de la dosis de la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'DiscardProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'DiscardProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de descarte o rechazo de la aplicación de nutrición parenteral; marca cuándo se anula la administración de NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'DateOfDiscardApplication';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de descarte de la aplicación de la dosis de la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'DateOfDiscardApplication';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'DateOfDiscardApplication';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código profesional de salud (CHAR 20) que administra/aplica la dosis prescrita de nutrición parenteral; vinculado a tabla INPROFSAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'ApplyingProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que aplica la dosis de la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'ApplyingProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'ApplyingProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de administración efectiva de la nutrición parenteral; registra cuándo se inicia la infusión de NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'DateOfNutritionApplication';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la aplicación de la dosis de la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'DateOfNutritionApplication';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'DateOfNutritionApplication';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vigencia de prescripción en horas (INT): 0=24h, 1=48h, 2=72h; determina duración de validez de la orden de nutrición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'ValidityPrescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la Vigencia de la prescripción en horas (0 ->24, 1->48, 2 -> 72)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'ValidityPrescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'ValidityPrescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones adicionales de nutrición (VARCHAR 500); notas clínicas complementarias para administración de NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'INDICACIONADI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicación adicional nutrición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'INDICACIONADI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'INDICACIONADI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones de administración (VARCHAR 300); instrucciones técnicas para infundir nutrición parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'INDICACIONADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones de administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'INDICACIONADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'INDICACIONADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de infusión (INT): 1=Continua, 2=Ciclada; modalidad de administración de la nutrición parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'TIPOINFUSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de infusion: 1:Continúa 2:Ciclada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'TIPOINFUSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'TIPOINFUSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen agua estéril calculada (NUMERIC 18,2); resultado: volumen total menos sumatoria de volúmenes a administrar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'AGUAESTERIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado de la formula (Volumen total - Sumatoria de columna volúmenes administrar)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'AGUAESTERIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'AGUAESTERIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice de líquidos (NUMERIC 18,2); resultado: volumen total dividido entre peso del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'LIQUIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado de la formula (Volumen total / Peso)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'LIQUIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'LIQUIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de días de NPT (INT); duración total del tratamiento nutricional parenteral prescrito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'NUMDIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero dias NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'NUMDIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'NUMDIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico (CHAR 4, ofuscado); diagnóstico CIE-10 o equivalente que justifica la nutrición parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Talla/estatura del paciente (NUMERIC 18,2); altura en cm, parámetro antropométrico para cálculo nutricional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'TALLAPACIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Talla del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'TALLAPACIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'TALLAPACIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador tipo bomba infusión (INT, FK MixingStation.TypesInfusionPumps); equipo o dispositivo para administración de NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'IDTypesInfusionPumps';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la pametrizacion de tipos de bombas de infusion (MedicalHistory.TypesInfusionPumps)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'IDTypesInfusionPumps';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'IDTypesInfusionPumps';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo suspensión nutrición (VARCHAR 2000); razón clínica por la cual se interrumpe o suspende la terapia de NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'MOTSUSMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de suspensión del la nutrición parenteral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'MOTSUSMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'MOTSUSMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones nivel riesgo paciente (VARCHAR 1000); notas adicionales sobre reacciones alérgicas, adversas o intolerancias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'PatientRiskLevelObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de los niveles de riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'PatientRiskLevelObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'PatientRiskLevelObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel riesgo paciente (INT): 1=Reacción alérgica, 2=Reacción adversa, 3=Intolerancia; clasificación de riesgo clínico en NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'PatientRiskLevel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel de riesgo del paciente 1. Reacción alérgica 2. Reacción adversa 3. Intolerancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'PatientRiskLevel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'PatientRiskLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Razón descontinuación (INT): 1=Riesgos/reacciones adversas, 2=Otra razón; motivo para suspender o eliminar nutrición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'ReasonDiscontinuationOfDrug';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Razón descontinuación de medicamento 1. Riesgos y reacciones adversas de medicamentos 2. Otra Razón o motivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'ReasonDiscontinuationOfDrug';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'ReasonDiscontinuationOfDrug';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID tipo dosis unitaria (INT, FK MixingStation.UnitDoseType); configuración de unidad de dosificación en preparación de NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'IDUNITDOSETYPE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla de vie erp Tipo de dosis unitaria: [MixingStation].[UnitDoseType]     ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'IDUNITDOSETYPE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'IDUNITDOSETYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones verificación (VARCHAR 5000); notas del químico farmacéutico al validar esquema nutricional desde dashboard de atención farmacéutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'OBSERVERIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuando el quimico confirma ó verifica el esquema de Nutricion, esto se hace desde el dashboard atención farmaceutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'OBSERVERIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'OBSERVERIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profesional verificación (CHAR 20, FK INPROFSAL); químico farmacéutico que confirma/verifica esquema de NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'CODPROSALVERIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuando el quimico confirma ó verifica el esquema de Nutricion, esto se hace desde el dashboard atención farmaceutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'CODPROSALVERIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'CODPROSALVERIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha verificación (DATETIME); cuándo el químico farmacéutico valida el esquema de nutrición parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'FECHAVERIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuando el quimico confirma ó verifica el esquema de Nutricion, esto se hace desde el dashboard atención farmaceutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'FECHAVERIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'FECHAVERIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agrupador paquete NPT (UNIQUEIDENTIFIER); relación bidireccional con HCFARMACD para trazabilidad de preparación farmacéutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'AGRUPAQUETE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuando el quimico en el dashboard atención farmaceutica verfica el esquema de nutricion y guarda se actualiza este campo que es una relacion con HCFARMACD ya que alla tambien queda dicho agrupador.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'AGRUPAQUETE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'AGRUPAQUETE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha suspensión (DATETIME); cuándo se suspende o interrumpe la nutrición parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'SUSDATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la suspension de la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'SUSDATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'SUSDATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario suspensión (CHAR 20); código usuario que registra la suspensión de NPT en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'USERSUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que suspende nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'USERSUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'USERSUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado prescripción (INT): 1=Solicitado, 2=Anulado, 3=Confirmado/Verificado, 4=Programado, 5=Aplicado; flujo operativo de la orden de NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'STATUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la prescripción:  1. Solicitado   2.Anulado  3. Confirmado ó verficado por el quimico desde dashboard atenciòn farmeceutico.  4. Programada. 5. Aplicado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'STATUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vía administración (VARCHAR 20): 1=Catéter central, 2=Catéter periférico/lateral; ruta de acceso vascular para infusión de NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'VIADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Via de administracion.  1- Cateter Central  2- Cateter lateral  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'VIADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'VIADMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Velocidad infusión (NUMERIC 18,2); flujo ml/h de administración de la nutrición parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'VELINFUSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Velocidad de infusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'VELINFUSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'VELINFUSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo administración (INT); duración en horas de la infusión de NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'TEMPOADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo de administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'TEMPOADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'TEMPOADMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen purga/purgado (NUMERIC 18,2); volumen suministrado para llenar catéter antes de infusión principal de NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'VOLUADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Volumen purga sumistrado para la prescripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'VOLUADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'VOLUADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen total (NUMERIC 18,2); volumen total en ml de solución nutritiva parenteral prescrita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'VOLUTOTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Volumen total sumistrado para la prescripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'VOLUTOTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'VOLUTOTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso paciente (NUMERIC 18,2); peso corporal en kg, parámetro crítico para cálculo dosis de nutrición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'PESOPACIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'PESOPACIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'PESOPACIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha prescripción NPT (DATETIME); cuándo el profesional ordena la nutrición parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'FECHAORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la prescripción de la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'FECHAORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'FECHAORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Folio finalización (NCHAR 10); número de folio donde se registra finalización/cierre de la administración de NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'FOLIOFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Folio desde el cual se finalizo la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'FOLIOFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'FOLIOFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Folio orden (NCHAR 10); número de folio inicial desde donde se ordena/autoriza la nutrición parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'FOLIORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Folio desde el cual se ordeno la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'FOLIORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'FOLIORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número ingreso (CHAR 10, FK ADINGRESO); identificador único del ingreso hospitalario asociado a la prescripción de NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de ingreso del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código paciente (VARCHAR 25, PII ofuscado, FK INPACIENT); identificación del paciente: cédula/documento/identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código profesional prescriptor (CHAR 20, PII ofuscado, FK INPROFSAL); médico o especialista que prescribe nutrición parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del profesional que prescribio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID historia clínica paciente (INT, FK HCHISPACA); referencia a historia clínica del paciente en atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de HISPACA ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID parametrización NPT (INT, FK HCPARNUTC); configuración/template de nutrición parenteral utilizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'IDHCPARNUTC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la pametrizacion de nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'IDHCPARNUTC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'IDHCPARNUTC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador registro (INT, PK); clave primaria única de la orden/prescripción de nutrición parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Órdenes de nutrición parenteral o enteral prescritas a pacientes hospitalizados. Registra la fórmula nutricional indicada por el profesional de salud, incluyendo volúmenes, velocidad de infusión, vía de administración, peso, talla, diagnóstico y estado de la orden (activa, suspendida, verificada, aplicada o descartada).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación clínica o motivo por el cual se indica la terapia nutricional al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'Justificacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'Justificacion';

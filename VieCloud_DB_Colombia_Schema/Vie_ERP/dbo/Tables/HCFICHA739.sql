CREATE TABLE [dbo].[HCFICHA739] (
    [ID]                             INT           IDENTITY (1, 1) NOT NULL,
    [IDFICHANOTIFICACION]            INT           NOT NULL,
    [CODDIAGNO]                      CHAR (4)      NULL,
    [RtPcrPositive]                  BIT           NOT NULL,
    [AcIgmIggPositive]               BIT           NOT NULL,
    [EpidemiologicalLinkToCovidCase] BIT           NOT NULL,
    [InitialSymptoms]                VARCHAR (MAX) NULL,
    [OtherSymptoms]                  VARCHAR (200) NULL,
    [FeverStartDate]                 DATE          NOT NULL,
    [HasFibrinogenoAlteration]       BIT           NOT NULL,
    [FibrinogenoValue]               VARCHAR (30)  NULL,
    [HasCReactiveProteinAlteration]  BIT           NOT NULL,
    [CReactiveProteinValue]          VARCHAR (30)  NULL,
    [HasFerritinaAlteration]         BIT           NOT NULL,
    [FerritinaValue]                 VARCHAR (30)  NULL,
    [HasDDimeroAlteration]           BIT           NOT NULL,
    [DDimeroValue]                   VARCHAR (30)  NULL,
    [HasLinfopenia]                  BIT           NOT NULL,
    [LinfopeniaValue]                VARCHAR (30)  NULL,
    [HasTroponinaElevation]          BIT           NOT NULL,
    [TroponinaValue]                 VARCHAR (30)  NULL,
    [VERSION]                        VARCHAR (20)  NULL,
    [JSON]                           VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA739] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA739_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA739_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA739] NOCHECK CONSTRAINT [CK_HCFICHA739_JSON];




GO
ALTER TABLE [dbo].[HCFICHA739] NOCHECK CONSTRAINT [CK_HCFICHA739_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos adicionales en formato JSON válido; almacena nuevas columnas y extensiones de la ficha de notificación COVID-19 (VARCHAR MAX, validado con CHECK isjson)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de control de la ficha de notificación; valor nulo indica primera versión, incrementa en actualizaciones posteriores (VARCHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor cuantitativo de troponina cardíaca obtenido en laboratorio, incluye unidad de medida (ng/mL, pg/mL); indicador de daño miocárdico (VARCHAR 30)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'TroponinaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de troponina obtneido en el laboratorio (con unidad si aplica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'TroponinaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'TroponinaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario de elevación o alteración de troponina cardíaca: 1=Sí presenta elevación, 0=No; marcador de gravedad COVID-19 (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'HasTroponinaElevation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Presenta alteracion de troponina? (1. Sí, 0. No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'HasTroponinaElevation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'HasTroponinaElevation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor o porcentaje de linfocitos obtenido en análisis de laboratorio, incluye unidad (células/µL, %); parámetro hematológico de respuesta inmune (VARCHAR 30)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'LinfopeniaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor/porcentaje de linfocitos obtenido en el laboratio (con unidad si aplica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'LinfopeniaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'LinfopeniaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario de linfopenia (recuento bajo de linfocitos): 1=Sí presenta, 0=No; hallazgo frecuente en COVID-19 severo (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'HasLinfopenia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Presenta linfopenia? (1. Sí, 0. No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'HasLinfopenia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'HasLinfopenia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de D-Dímero obtenido en laboratorio, incluye unidad (ng/mL FEU, µg/mL); marcador de coagulación intravascular (VARCHAR 30)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'DDimeroValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de D-Dímero obenido en el laboratorio (con unidad si aplica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'DDimeroValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'DDimeroValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario de alteración o elevación de D-Dímero: 1=Sí alterado, 0=No; asociado a riesgo trombótico en COVID-19 (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'HasDDimeroAlteration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Presenta alteración de D-Dímero? (1. Sí, 0. No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'HasDDimeroAlteration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'HasDDimeroAlteration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de ferritina sérica obtenido en laboratorio, incluye unidad (ng/mL, µg/L); marcador de inflamación sistémica (VARCHAR 30)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'FerritinaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de ferritina obtenida en el laboratorio (con unidad si aplica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'FerritinaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'FerritinaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario de alteración o elevación de ferritina: 1=Sí alterada, 0=No; refleja tormenta inflamatoria en COVID-19 (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'HasFerritinaAlteration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Presenta alteración de ferritina? (1. Sí, 0. No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'HasFerritinaAlteration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'HasFerritinaAlteration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de proteína C reactiva (PCR) obtenido en laboratorio, incluye unidad (mg/L, mg/dL); marcador de inflamación sistémica (VARCHAR 30)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'CReactiveProteinValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de proteína C reactiva (PCR) obtenido en el laboratorio (con unidad si aplica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'CReactiveProteinValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'CReactiveProteinValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario de alteración o elevación de proteína C reactiva (PCR): 1=Sí alterada, 0=No; predictor de severidad COVID-19 (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'HasCReactiveProteinAlteration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Presenta alteración de proteína C reactiva (PCR)? (1. Sí, 0. No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'HasCReactiveProteinAlteration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'HasCReactiveProteinAlteration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de fibrinógeno plasmático obtenido en laboratorio, incluye unidad (mg/dL, g/L); parámetro de coagulación (VARCHAR 30)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'FibrinogenoValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de fibrinógeno obtenido en el laboratorio (con unidad si aplica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'FibrinogenoValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'FibrinogenoValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario de alteración de fibrinógeno: 1=Sí alterado, 0=No; asociado a coagulopatía en COVID-19 severo (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'HasFibrinogenoAlteration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Presenta alteración del fibrinógeno? (1. Sí, 0. No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'HasFibrinogenoAlteration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'HasFibrinogenoAlteration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio de fiebre del paciente; referencia temporal para cronología clínica de síntomas COVID-19 (DATE, formato YYYY-MM-DD)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'FeverStartDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de inicio de la fiebre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'FeverStartDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'FeverStartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de síntomas adicionales o secundarios presentes en el paciente más allá de los iniciales; complementa manifestaciones clínicas (VARCHAR 200)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'OtherSymptoms';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otros síntomas que presenta el paciete', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'OtherSymptoms';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'OtherSymptoms';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Síntomas iniciales o presentación clínica al momento de notificación/sospecha COVID-19; describes manifestaciones tempranas del paciente (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'InitialSymptoms';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Síntomas iniciales del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'InitialSymptoms';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'InitialSymptoms';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario de exposición epidemiológica a caso confirmado COVID-19: 1=Sí tiene vínculo, 0=No; criterio de caso sospechoso (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'EpidemiologicalLinkToCovidCase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vinculo epidemiologico con caso confirmado de COVID-19 (1. Sí, 0. No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'EpidemiologicalLinkToCovidCase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'EpidemiologicalLinkToCovidCase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario de presencia de anticuerpos IgM/IgG positivos contra SARS-CoV-2: 1=Sí positivo, 0=No; confirmación serológica (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'AcIgmIggPositive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presencia de anticuerpos IgM/IgG positivos para SARS-CoV-2 (1. Sí, 0. No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'AcIgmIggPositive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'AcIgmIggPositive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario de resultado RT-PCR positivo para SARS-CoV-2 en últimas 4 semanas: 1=Sí positivo, 0=No; confirmación molecular (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'RtPcrPositive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado RT-PCR positivo para SARS-CoV-2 en las ultimas 4 semanas (1. Sí, 0. No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'RtPcrPositive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'RtPcrPositive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico (CHAR 4) asociado a la ficha de notificación; referencia a diagnóstico confirmado o sospechoso de COVID-19 (CHAR 4, ej: U071)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diagnostico asociado a la ficha de notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico único de la ficha de notificación COVID-19; llave foránea que relaciona con tabla HCFICHANOTIFICACION (INT, FK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único de la ficha clínica detallada COVID-19; clave primaria de HCFICHA739 (INT IDENTITY 1,1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha epidemiológica 739 para notificación de casos sospechosos o confirmados de COVID-19 (Síndrome Inflamatorio Multisistémico y similares). Registra resultados de pruebas diagnósticas, síntomas, nexo epidemiológico y marcadores inflamatorios de laboratorio asociados a cada notificación de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA739';

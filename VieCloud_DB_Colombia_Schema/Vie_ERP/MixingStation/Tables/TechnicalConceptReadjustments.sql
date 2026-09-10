CREATE TABLE [MixingStation].[TechnicalConceptReadjustments] (
    [Id]                           INT           IDENTITY (1, 1) NOT NULL,
    [RequestPackageDetailStatusId] INT           NOT NULL,
    [TechnicalConceptDate]         DATETIME      NOT NULL,
    [PreviouslyReadjustment]       BIT           NOT NULL,
    [TechnicalConceptDateExpired]  DATETIME      NULL,
    [Temperature]                  VARCHAR (50)  NULL,
    [TechnicalConcept]             VARCHAR (500) NULL,
    CONSTRAINT [PK_TechnicalConceptReadjusments] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que registra si el concepto técnico ya fue adecuado, ajustado o reenviado previamente. Permite identificar reajustes sucesivos del mismo concepto técnico en el ciclo de validación o aprobación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'TechnicalConceptReadjustments', @level2type = N'COLUMN', @level2name = N'PreviouslyReadjustment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena si anteriormente ya se ha adecuado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'TechnicalConceptReadjustments', @level2type = N'COLUMN', @level2name = N'PreviouslyReadjustment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'TechnicalConceptReadjustments', @level2type = N'COLUMN', @level2name = N'PreviouslyReadjustment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de reajustes de conceptos técnicos asociados al detalle de paquetes solicitados en la estación de mezcla. Guarda la evaluación técnica (concepto, fecha, temperatura y vencimiento) de cada reajuste realizado sobre un estado de detalle de solicitud.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'TechnicalConceptReadjustments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'TechnicalConceptReadjustments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del reajuste de concepto técnico.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'TechnicalConceptReadjustments', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'TechnicalConceptReadjustments', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al estado del detalle del paquete de solicitud al que pertenece este concepto técnico.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'TechnicalConceptReadjustments', @level2type = N'COLUMN', @level2name = N'RequestPackageDetailStatusId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'TechnicalConceptReadjustments', @level2type = N'COLUMN', @level2name = N'RequestPackageDetailStatusId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se emitió el concepto técnico del reajuste.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'TechnicalConceptReadjustments', @level2type = N'COLUMN', @level2name = N'TechnicalConceptDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'TechnicalConceptReadjustments', @level2type = N'COLUMN', @level2name = N'TechnicalConceptDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de vencimiento o expiración del concepto técnico; indica hasta cuándo es válido.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'TechnicalConceptReadjustments', @level2type = N'COLUMN', @level2name = N'TechnicalConceptDateExpired';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'TechnicalConceptReadjustments', @level2type = N'COLUMN', @level2name = N'TechnicalConceptDateExpired';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Temperatura registrada al momento de la evaluación técnica, relevante para el proceso de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'TechnicalConceptReadjustments', @level2type = N'COLUMN', @level2name = N'Temperature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'TechnicalConceptReadjustments', @level2type = N'COLUMN', @level2name = N'Temperature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o texto del concepto técnico emitido por el evaluador, detallando el resultado o justificación del reajuste.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'TechnicalConceptReadjustments', @level2type = N'COLUMN', @level2name = N'TechnicalConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'TechnicalConceptReadjustments', @level2type = N'COLUMN', @level2name = N'TechnicalConcept';

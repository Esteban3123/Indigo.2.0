CREATE TABLE [MixingStation].[RequestPackageDetailStatusDefectClassification] (
    [Id]                           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RequestPackageDetailStatusId] INT             NOT NULL,
    [Observation]                  VARCHAR (500)   NOT NULL,
    [CreationUser]                 VARCHAR (20)    NOT NULL,
    [CreationDate]                 DATETIME        NOT NULL,
    [ModificationUser]             VARCHAR (20)    NULL,
    [ModificationDate]             DATETIME        NULL,
    [ValidateWeigthNPT]            BIT             NULL,
    [ActualWeight]                 DECIMAL (18, 2) NULL,
    [IndicationSize]               INT             NULL,
    CONSTRAINT [PK_RequestPackageDetailStatusDefectClassification] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RequestPackageDetailStatusDefectClassification_RequestPackageDetailStatus] FOREIGN KEY ([RequestPackageDetailStatusId]) REFERENCES [MixingStation].[RequestPackageDetailStatus] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_DefectClassification_RequestPackageDetailStatusId]
    ON [MixingStation].[RequestPackageDetailStatusDefectClassification] ([RequestPackageDetailStatusId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT) que valida si la preparación/adecuación NPT (Nutrición Parenteral Total) cumple con tolerancia de peso: 0=defecto detectado, 1=válido/conforme. Usado para control de calidad en mezcla de fórmulas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'ValidateWeigthNPT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si la adecuacion NPT tiene defectos en el peso real: 0 - Tiene defectos,  1 - Valida', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'ValidateWeigthNPT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'ValidateWeigthNPT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) del último cambio o actualización del registro de clasificación de defecto en la solicitud de paquete, para auditoría y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o ID del usuario (VARCHAR 20) que realizó la última modificación del registro; NULL si no hubo cambios posteriores a creación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro de clasificación de defecto; marca inicio de la evaluación de calidad.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o ID del usuario (VARCHAR 20) que registró inicialmente la clasificación de defecto en la solicitud de paquete NPT; trazabilidad de responsable.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de defectos detectados en el estado de detalle de un paquete de solicitud en la estación de mezclas. Registra observaciones, peso real y tamaño de indicación cuando se identifica un problema o no conformidad en la preparación de una mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de clasificación de defecto.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al estado del detalle del paquete de solicitud al que pertenece este defecto o no conformidad.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'RequestPackageDetailStatusId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'RequestPackageDetailStatusId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción u observación del defecto encontrado en la preparación o verificación de la mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso real medido del componente o mezcla al momento de detectar el defecto, en la unidad de peso configurada.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'ActualWeight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'ActualWeight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tamaño o volumen de la indicación médica asociada al paquete en el momento del registro del defecto.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'IndicationSize';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestPackageDetailStatusDefectClassification', @level2type = N'COLUMN', @level2name = N'IndicationSize';

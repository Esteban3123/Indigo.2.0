CREATE TABLE [MixingStation].[DilutionFactors] (
    [Id]                   INT             IDENTITY (1, 1) NOT NULL,
    [Code]                 VARCHAR (20)    NOT NULL,
    [ATCId]                INT             NOT NULL,
    [MeasurementUnitId]    INT             NOT NULL,
    [PreMedic]             VARCHAR (20)    NOT NULL,
    [PharmaceuticalFormId] INT             NOT NULL,
    [WeightStandar]        DECIMAL (18, 2) NOT NULL,
    [CreationUser]         VARCHAR (20)    NOT NULL,
    [CreationDate]         DATETIME        NOT NULL,
    [ModificationUser]     VARCHAR (20)    NULL,
    [ModificationDate]     DATETIME        NULL,
    [TimeStamp]            ROWVERSION      NOT NULL,
    CONSTRAINT [PK_DilutionFactors] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DilutionFactors_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_DilutionFactors_MeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_DilutionFactors_PharmaceuticalForm] FOREIGN KEY ([PharmaceuticalFormId]) REFERENCES [Inventory].[PharmaceuticalForm] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del factor de dilución; DATETIME, auditoría de cambios en farmacotecnia', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de modificacion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del registro de factor de dilución; VARCHAR(20), trazabilidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario de modificacion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del factor de dilución; DATETIME, registro inicial en sistema de preparación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de creacion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro del factor de dilución; VARCHAR(20), autor original', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso estándar de dilución en unidades métricas; DECIMAL(18,2), parámetro farmacotécnico para cálculo de concentración', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'WeightStandar';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso estandar', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'WeightStandar';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'WeightStandar';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de forma farmacéutica (tableta, cápsula, solución, polvo, etc.); INT FK→[Inventory].[PharmaceuticalForm], referencia de presentación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'PharmaceuticalFormId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la forma farmaceutica', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'PharmaceuticalFormId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'PharmaceuticalFormId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presentación del medicamento (dosis, concentración, envase); VARCHAR(20), código de presentación comercial o farmacotécnica', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'PreMedic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presentacion del medicamento', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'PreMedic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'PreMedic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de unidad de medida (mg, ml, mcg, g, etc.); INT FK→[Inventory].[InventoryMeasurementUnit], normalización de cantidades', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de medida', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de clasificación ATC (Anatomical Therapeutic Chemical) del fármaco; INT FK→[Inventory].[ATC], estandarización farmacológica', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla ATC', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código parametrizable único del factor de dilución; VARCHAR(20), identificador interno para preparación y mezcla farmacotécnica', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Parametrizable del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factores de dilución para la estación de mezcla de medicamentos. Registra los parámetros de dilución de cada medicamento (identificado por código ATC y forma farmacéutica) utilizados en la preparación y dispensación de mezclas en farmacia hospitalaria.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno único del registro de factor de dilución.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo automática del sistema para control de concurrencia y auditoría de cambios en el registro.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'TimeStamp';

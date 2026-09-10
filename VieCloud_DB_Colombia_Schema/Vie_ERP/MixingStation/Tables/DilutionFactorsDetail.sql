CREATE TABLE [MixingStation].[DilutionFactorsDetail] (
    [Id]                 INT             IDENTITY (1, 1) NOT NULL,
    [DilutionFactorsId]  INT             NOT NULL,
    [AtcId]              INT             NULL,
    [Volume]             DECIMAL (18, 2) NULL,
    [Dilution]           DECIMAL (18, 4) NULL,
    [Concentration]      DECIMAL (18, 4) NULL,
    [AmountTime]         INT             NOT NULL,
    [VolumeMeasureUnit]  INT             NOT NULL,
    [TimeUnit]           TINYINT         NOT NULL,
    [DisplacementVolume] DECIMAL (18, 2) NULL,
    [RequiredVolume]     DECIMAL (18, 2) NULL,
    [ByDefault]          BIT             NULL,
    CONSTRAINT [PK_DilutionFactorsDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DilutionFactorsDetail_ATC] FOREIGN KEY ([AtcId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_DilutionFactorsDetail_DilutionFactors] FOREIGN KEY ([DilutionFactorsId]) REFERENCES [MixingStation].[DilutionFactors] ([Id]),
    CONSTRAINT [FK_DilutionFactorsDetail_UnitMeasure_Volume] FOREIGN KEY ([VolumeMeasureUnit]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para la dilución: 1=Horas, 2=Días. Tipo TINYINT, determina la escala temporal del factor de dilución aplicado al medicamento.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'TimeUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de tiempo : 1-Horas, 2-Dias', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'TimeUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'TimeUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida del volumen (mL, L, etc.). FK a [Inventory].[InventoryMeasurementUnit]. Define la escala volumétrica del medicamento diluido.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'VolumeMeasureUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida del Volumen', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'VolumeMeasureUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'VolumeMeasureUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de tiempo, expresado en la unidad especificada en TimeUnit. Representa el intervalo o duración aplicable al factor de dilución.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'AmountTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de tiempo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'AmountTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'AmountTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración del medicamento tras dilución. Decimal (18,4), expresada en unidades farmacológicas. Resultado de la relación volumen/dilución.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'Concentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concentracion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'Concentration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'Concentration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de dilución aplicado al medicamento. Decimal (18,4). Relación matemática que ajusta la concentración original del fármaco ATC.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'Dilution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Factor de dilucion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'Dilution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'Dilution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen del medicamento para preparar la dilución. Decimal (18,2), medido en la unidad especificada en VolumeMeasureUnit. Base para el cálculo.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'Volume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'volumen del medicamento', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'Volume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'Volume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del medicamento tipo dilución (código ATC). FK a [Inventory].[ATC]. Referencia al fármaco base sometido a proceso de dilución en estación de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'AtcId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del medicamento tipo dilucion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'AtcId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'AtcId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla cabecera [MixingStation].[DilutionFactors]. FK obligatoria. Agrupa múltiples detalles de dilución bajo un factor principal.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'DilutionFactorsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla cabecera', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'DilutionFactorsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'DilutionFactorsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los factores de dilución utilizados en la estación de mezclas: especifica los volúmenes, concentraciones y tiempos asociados a cada medicamento o componente (ATC) para preparar diluciones farmacéuticas en farmacia hospitalaria.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de detalle de factor de dilución.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen de desplazamiento del medicamento; cantidad de líquido que el fármaco desplaza al ser reconstituido o diluido, utilizado para ajustar el volumen final de la preparación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'DisplacementVolume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'DisplacementVolume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen requerido o necesario para completar la dilución según la prescripción o protocolo de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'RequiredVolume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'RequiredVolume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si este detalle de dilución es la opción predeterminada o por defecto para el factor de dilución asociado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'ByDefault';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'ByDefault';

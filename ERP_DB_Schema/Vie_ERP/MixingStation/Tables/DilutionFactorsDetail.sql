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
ALTER TABLE [MixingStation].[DilutionFactorsDetail] NOCHECK CONSTRAINT [FK_DilutionFactorsDetail_ATC];


GO
ALTER TABLE [MixingStation].[DilutionFactorsDetail] NOCHECK CONSTRAINT [FK_DilutionFactorsDetail_DilutionFactors];


GO
ALTER TABLE [MixingStation].[DilutionFactorsDetail] NOCHECK CONSTRAINT [FK_DilutionFactorsDetail_UnitMeasure_Volume];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de tiempo : 1-Horas, 2-Dias', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'TimeUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de medida del Volumen', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'VolumeMeasureUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad de tiempo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'AmountTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concentracion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'Concentration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Factor de dilucion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'Dilution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'volumen del medicamento', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'Volume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del medicamento tipo dilucion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'AtcId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla cabecera', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactorsDetail', @level2type = N'COLUMN', @level2name = N'DilutionFactorsId';


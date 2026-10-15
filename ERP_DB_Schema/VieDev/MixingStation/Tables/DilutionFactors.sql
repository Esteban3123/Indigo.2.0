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
ALTER TABLE [MixingStation].[DilutionFactors] NOCHECK CONSTRAINT [FK_DilutionFactors_ATC];


GO
ALTER TABLE [MixingStation].[DilutionFactors] NOCHECK CONSTRAINT [FK_DilutionFactors_MeasurementUnit];


GO
ALTER TABLE [MixingStation].[DilutionFactors] NOCHECK CONSTRAINT [FK_DilutionFactors_PharmaceuticalForm];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de modificacion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'usuario de modificacion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de creacion', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peso estandar', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'WeightStandar';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la forma farmaceutica', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'PharmaceuticalFormId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presentacion del medicamento', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'PreMedic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de medida', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla ATC', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Parametrizable del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'DilutionFactors', @level2type = N'COLUMN', @level2name = N'Code';


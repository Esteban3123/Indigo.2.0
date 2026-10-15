CREATE TABLE [MixingStation].[RequestUnitDoseInventoryDetail] (
    [Id]                         INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RequestUnitDoseInventoryId] INT NOT NULL,
    [UnitDoseTypeId]             INT NOT NULL,
    [ATCId]                      INT NULL,
    [PackageId]                  INT NULL,
    [Quantity]                   INT NOT NULL,
    [AdministrationRouteId]      INT NULL,
    CONSTRAINT [PK_RequestUnitDoseInventoryDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RequestUnitDoseInventoryDetail_AdministrationRoute] FOREIGN KEY ([AdministrationRouteId]) REFERENCES [Inventory].[AdministrationRoute] ([Id]),
    CONSTRAINT [FK_RequestUnitDoseInventoryDetail_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_RequestUnitDoseInventoryDetail_Package] FOREIGN KEY ([PackageId]) REFERENCES [MixingStation].[Package] ([Id]),
    CONSTRAINT [FK_RequestUnitDoseInventoryDetail_RequestUnitDoseInventory] FOREIGN KEY ([RequestUnitDoseInventoryId]) REFERENCES [MixingStation].[RequestUnitDoseInventory] ([Id]),
    CONSTRAINT [FK_RequestUnitDoseInventoryDetail_UnitDoseType] FOREIGN KEY ([UnitDoseTypeId]) REFERENCES [MixingStation].[UnitDoseType] ([Id])
);


GO
ALTER TABLE [MixingStation].[RequestUnitDoseInventoryDetail] NOCHECK CONSTRAINT [FK_RequestUnitDoseInventoryDetail_ATC];


GO
ALTER TABLE [MixingStation].[RequestUnitDoseInventoryDetail] NOCHECK CONSTRAINT [FK_RequestUnitDoseInventoryDetail_Package];


GO
ALTER TABLE [MixingStation].[RequestUnitDoseInventoryDetail] NOCHECK CONSTRAINT [FK_RequestUnitDoseInventoryDetail_RequestUnitDoseInventory];


GO
ALTER TABLE [MixingStation].[RequestUnitDoseInventoryDetail] NOCHECK CONSTRAINT [FK_RequestUnitDoseInventoryDetail_UnitDoseType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseInventoryDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del paquete, se asigna cuando la clase del tipo de dosis unitaria es: mezclas, npt, antibioticoterapia, citostatico, unguento, magistral', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseInventoryDetail', @level2type = N'COLUMN', @level2name = N'PackageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del medicamento, se asigna cuando la clase del tipo de dosis unitaria es: reempaque, reetiquetado, reenvase', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseInventoryDetail', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de dosis unitaria', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseInventoryDetail', @level2type = N'COLUMN', @level2name = N'UnitDoseTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseInventoryDetail', @level2type = N'COLUMN', @level2name = N'RequestUnitDoseInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'RequestUnitDoseInventoryDetail', @level2type = N'COLUMN', @level2name = N'Id';


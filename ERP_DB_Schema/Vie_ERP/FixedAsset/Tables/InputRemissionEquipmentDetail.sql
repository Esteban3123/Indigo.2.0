CREATE TABLE [FixedAsset].[InputRemissionEquipmentDetail] (
    [Id]                        INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdInputRemissionEquipment] INT          NOT NULL,
    [IdEquipment]               INT          NOT NULL,
    [LicensePlate]              VARCHAR (50) NOT NULL,
    [Serie]                     VARCHAR (50) NOT NULL,
    [IdResponsible]             INT          NOT NULL,
    [IdFunctionalUnit]          INT          NOT NULL,
    [IdLocation]                INT          NOT NULL,
    [AdquisitionDate]           DATE         NOT NULL,
    [Depreciate]                BIT          NOT NULL,
    [ComponentDepreciate]       BIT          NULL,
    CONSTRAINT [PK_InputRemissionEquipmentDetail_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InputRemissionEquipmentDetail_FunctionalUnit] FOREIGN KEY ([IdFunctionalUnit]) REFERENCES [Payroll].[FunctionalUnit] ([Id])
);


GO
ALTER TABLE [FixedAsset].[InputRemissionEquipmentDetail] NOCHECK CONSTRAINT [FK_InputRemissionEquipmentDetail_FunctionalUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deprecia x Componente. 1 - SI, 0 - NO. Solo se llena si el campo LOAN es 1. ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'ComponentDepreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deprecia (1 - SI, 0 - NO)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Depreciate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Adquisición', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'AdquisitionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Localización', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdLocation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdFunctionalUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Responsable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdResponsible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de serie', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Serie';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Placa', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'LicensePlate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdEquipment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Equipo de remisión de entrada', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'IdInputRemissionEquipment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'InputRemissionEquipmentDetail', @level2type = N'COLUMN', @level2name = N'Id';


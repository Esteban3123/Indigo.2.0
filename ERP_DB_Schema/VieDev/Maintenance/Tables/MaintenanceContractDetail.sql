CREATE TABLE [Maintenance].[MaintenanceContractDetail] (
    [Id]                    INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MaintenanceContractId] INT NOT NULL,
    [PhysicalAssetId]       INT NOT NULL,
    CONSTRAINT [PK_MaintenanceContractDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MaintenanceContractDetail_MaintenanceContract] FOREIGN KEY ([MaintenanceContractId]) REFERENCES [Maintenance].[MaintenanceContract] ([Id]),
    CONSTRAINT [FK_MaintenanceContractDetail_PhysicalAssetId] FOREIGN KEY ([PhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id])
);


GO
ALTER TABLE [Maintenance].[MaintenanceContractDetail] NOCHECK CONSTRAINT [FK_MaintenanceContractDetail_MaintenanceContract];


GO
ALTER TABLE [Maintenance].[MaintenanceContractDetail] NOCHECK CONSTRAINT [FK_MaintenanceContractDetail_PhysicalAssetId];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de las placas de activos fijos', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContractDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del contrato', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContractDetail', @level2type = N'COLUMN', @level2name = N'MaintenanceContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContractDetail', @level2type = N'COLUMN', @level2name = N'Id';


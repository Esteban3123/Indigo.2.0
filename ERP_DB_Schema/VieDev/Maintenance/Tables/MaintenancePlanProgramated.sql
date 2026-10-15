CREATE TABLE [Maintenance].[MaintenancePlanProgramated] (
    [Id]                            INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MaintenancePlanAndMetrologyId] INT      NOT NULL,
    [FixedAssetPhysicalId]          INT      NOT NULL,
    [DateProgramated]               DATETIME NOT NULL,
    [ProgramType]                   TINYINT  NOT NULL,
    [Notificated]                   BIT      NOT NULL,
    [State]                         TINYINT  NULL,
    CONSTRAINT [PK_MaintenancePlanProgramated__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MaintenancePlanProgramated_FixedAssetPhysicalAsset] FOREIGN KEY ([FixedAssetPhysicalId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id]),
    CONSTRAINT [FK_MaintenancePlanProgramated_MaintenancePlanAndMetrology] FOREIGN KEY ([MaintenancePlanAndMetrologyId]) REFERENCES [Maintenance].[MaintenancePlanAndMetrology] ([Id]),
    CONSTRAINT [UQ_MaintenancePlanProgramated__DateProgramated__MaintenancePlanAndMetrologyId__ProgramType__FixedAssetPhysicalId] UNIQUE NONCLUSTERED ([DateProgramated] ASC, [MaintenancePlanAndMetrologyId] ASC, [ProgramType] ASC, [FixedAssetPhysicalId] ASC)
);


GO
ALTER TABLE [Maintenance].[MaintenancePlanProgramated] NOCHECK CONSTRAINT [FK_MaintenancePlanProgramated_FixedAssetPhysicalAsset];


GO
ALTER TABLE [Maintenance].[MaintenancePlanProgramated] NOCHECK CONSTRAINT [FK_MaintenancePlanProgramated_MaintenancePlanAndMetrology];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado:  1 - Programado  2 - Con Orden de Trabajo  3 - Anulado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si ya se ha notificado via mail', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'Notificated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Programación:  1 - Mantenimiento  2 - Metrología', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'ProgramType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha programada', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'DateProgramated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Activo fijo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del plan de mantenimiento y metrología', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'MaintenancePlanAndMetrologyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlanProgramated', @level2type = N'COLUMN', @level2name = N'Id';


CREATE TABLE [Maintenance].[MaintenancePlan] (
    [Id]                INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]              VARCHAR (20)  NOT NULL,
    [Name]              VARCHAR (200) NOT NULL,
    [InitialDate]       DATETIME      NOT NULL,
    [RegimeType]        TINYINT       NOT NULL,
    [MeasurementUnitId] INT           NULL,
    [EquipmentTypeId]   INT           NOT NULL,
    [Status]            TINYINT       NOT NULL,
    [CreationUser]      VARCHAR (20)  CONSTRAINT [DF_MaintenancePlan_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]      DATETIME      CONSTRAINT [DF_MaintenancePlan_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]  VARCHAR (20)  NULL,
    [ModificationDate]  DATETIME      NULL,
    [ConfirmationUser]  VARCHAR (20)  NULL,
    [ConfirmationDate]  DATETIME      NULL,
    [AnnulmentUser]     VARCHAR (20)  NULL,
    [AnnulmentDate]     DATETIME      NULL,
    [TimeStamp]         ROWVERSION    NOT NULL,
    CONSTRAINT [PK_MaintenancePlan__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MaintenancePlan_EquipmentType] FOREIGN KEY ([EquipmentTypeId]) REFERENCES [FixedAsset].[FixedAssetItemType] ([Id]),
    CONSTRAINT [FK_MaintenancePlan_MeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Maintenance].[MeasurementUnit] ([Id])
);


GO
ALTER TABLE [Maintenance].[MaintenancePlan] NOCHECK CONSTRAINT [FK_MaintenancePlan_EquipmentType];


GO
ALTER TABLE [Maintenance].[MaintenancePlan] NOCHECK CONSTRAINT [FK_MaintenancePlan_MeasurementUnit];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_MaintenancePlan__Code]
    ON [Maintenance].[MaintenancePlan]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlan', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlan', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlan', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlan', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlan', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlan', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlan', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlan', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlan', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Documento (Registrado = 1, Confirmado = 2, Anulado = 3)', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlan', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de equipo asociado al registro del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlan', @level2type = N'COLUMN', @level2name = N'EquipmentTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la unidad de medida', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlan', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de regimen  1 - Fechas  2 - Lecturas', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlan', @level2type = N'COLUMN', @level2name = N'RegimeType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlan', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del plan de mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlan', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del plan de mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlan', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del plan del mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenancePlan', @level2type = N'COLUMN', @level2name = N'Id';


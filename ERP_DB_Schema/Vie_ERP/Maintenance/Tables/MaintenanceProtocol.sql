CREATE TABLE [Maintenance].[MaintenanceProtocol] (
    [Id]                   INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                 VARCHAR (20)   NOT NULL,
    [Name]                 VARCHAR (255)  NOT NULL,
    [Type]                 TINYINT        NOT NULL,
    [FixedAssetItemId]     INT            NOT NULL,
    [ResponsibleTypeId]    INT            NOT NULL,
    [Color]                INT            NOT NULL,
    [ConsumabeDescription] VARCHAR (1000) NOT NULL,
    [SupplyDescription]    VARCHAR (1000) NOT NULL,
    [ToolsDescription]     VARCHAR (1000) NOT NULL,
    [State]                BIT            NOT NULL,
    [CreationUser]         VARCHAR (20)   NOT NULL,
    [CreationDate]         DATETIME       NOT NULL,
    [ModificationUser]     VARCHAR (20)   NULL,
    [ModificationDate]     DATETIME       NULL,
    [TimeStamp]            ROWVERSION     NOT NULL,
    CONSTRAINT [PK_MaintenanceProtocol__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MaintenanceProtocol_FixedAssetItem] FOREIGN KEY ([FixedAssetItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [FK_MaintenanceProtocol_ResponsibleType] FOREIGN KEY ([ResponsibleTypeId]) REFERENCES [FixedAsset].[ResponsibleType] ([Id]),
    CONSTRAINT [UQ_MaintenanceProtocol__Code] UNIQUE NONCLUSTERED ([Code] ASC),
    CONSTRAINT [UQ_MaintenanceProtocol__FixedAssetItemId] UNIQUE NONCLUSTERED ([FixedAssetItemId] ASC)
);


GO
ALTER TABLE [Maintenance].[MaintenanceProtocol] NOCHECK CONSTRAINT [FK_MaintenanceProtocol_FixedAssetItem];


GO
ALTER TABLE [Maintenance].[MaintenanceProtocol] NOCHECK CONSTRAINT [FK_MaintenanceProtocol_ResponsibleType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceProtocol', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceProtocol', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceProtocol', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceProtocol', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceProtocol', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del protocolo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceProtocol', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción de las herramientas a utilizar', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceProtocol', @level2type = N'COLUMN', @level2name = N'ToolsDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción de los insumos a utilizar en el protocolo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceProtocol', @level2type = N'COLUMN', @level2name = N'SupplyDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consumibles a utilizar en el protocolo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceProtocol', @level2type = N'COLUMN', @level2name = N'ConsumabeDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'color  ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceProtocol', @level2type = N'COLUMN', @level2name = N'Color';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de responsable del protocolo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceProtocol', @level2type = N'COLUMN', @level2name = N'ResponsibleTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceProtocol', @level2type = N'COLUMN', @level2name = N'FixedAssetItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo:  1 - Pruebas de seguridad  2 - Verificación y Calibración  3 - Matenimiento Preventivo  4 - Mantenimiento Correctivo  5 - Otros', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceProtocol', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceProtocol', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceProtocol', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceProtocol', @level2type = N'COLUMN', @level2name = N'Id';


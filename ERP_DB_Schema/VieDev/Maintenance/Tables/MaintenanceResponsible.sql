CREATE TABLE [Maintenance].[MaintenanceResponsible] (
    [Id]                INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]              VARCHAR (20) NOT NULL,
    [ThirdPartyId]      INT          NOT NULL,
    [VinculationTypeId] INT          NOT NULL,
    [ReponsibleTypeId]  INT          NOT NULL,
    [ResponsibleRole]   TINYINT      NOT NULL,
    [Status]            BIT          NOT NULL,
    [CreationUser]      VARCHAR (20) NOT NULL,
    [CreationDate]      DATETIME     NOT NULL,
    [ModificationUser]  VARCHAR (20) NULL,
    [ModificationDate]  DATETIME     NULL,
    [TimeStamp]         ROWVERSION   NOT NULL,
    [Capacity]          TINYINT      NULL,
    CONSTRAINT [PK_MaintenanceResponsible__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Responsible_ResponsibleType] FOREIGN KEY ([ReponsibleTypeId]) REFERENCES [FixedAsset].[ResponsibleType] ([Id]),
    CONSTRAINT [FK_Responsible_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_Responsible_VinculationType] FOREIGN KEY ([VinculationTypeId]) REFERENCES [FixedAsset].[FixedAssetVinculationType] ([Id])
);


GO
ALTER TABLE [Maintenance].[MaintenanceResponsible] NOCHECK CONSTRAINT [FK_Responsible_ResponsibleType];


GO
ALTER TABLE [Maintenance].[MaintenanceResponsible] NOCHECK CONSTRAINT [FK_Responsible_ThirdParty];


GO
ALTER TABLE [Maintenance].[MaintenanceResponsible] NOCHECK CONSTRAINT [FK_Responsible_VinculationType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Capacidad laboral diarias en horas', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceResponsible', @level2type = N'COLUMN', @level2name = N'Capacity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceResponsible', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Modificación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceResponsible', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Modificación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceResponsible', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceResponsible', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Creación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceResponsible', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado (1 - Activo, 0 - Inactivo)', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceResponsible', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Rol del responsable:  1 - Administrador  2 - Encargado  3 - Coordinador  4 - Operario-Tecnico  5 - Externo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceResponsible', @level2type = N'COLUMN', @level2name = N'ResponsibleRole';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Tipo de Responsable', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceResponsible', @level2type = N'COLUMN', @level2name = N'ReponsibleTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Tipo de Vinculación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceResponsible', @level2type = N'COLUMN', @level2name = N'VinculationTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Tercero', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceResponsible', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del Responsable', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceResponsible', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Responsable', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceResponsible', @level2type = N'COLUMN', @level2name = N'Id';


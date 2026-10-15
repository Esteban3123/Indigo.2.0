CREATE TABLE [Maintenance].[TemplateEquipmentType] (
    [Id]                   INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                 VARCHAR (20)   NOT NULL,
    [IdEquipmentType]      INT            NOT NULL,
    [Terms]                VARCHAR (8000) NOT NULL,
    [GeneralData]          VARCHAR (8000) NOT NULL,
    [OperationDescription] VARCHAR (8000) NOT NULL,
    [HandlingPrecautions]  VARCHAR (8000) NOT NULL,
    [Cleaning]             VARCHAR (8000) NOT NULL,
    [State]                BIT            NOT NULL,
    [TimeStamp]            ROWVERSION     NOT NULL,
    CONSTRAINT [PK_TemplateEquipmentType__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TemplateEquipmentType_EquipmentType] FOREIGN KEY ([IdEquipmentType]) REFERENCES [FixedAsset].[FixedAssetItemType] ([Id])
);


GO
ALTER TABLE [Maintenance].[TemplateEquipmentType] NOCHECK CONSTRAINT [FK_TemplateEquipmentType_EquipmentType];


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_TemplateEquipmentType]
    ON [Maintenance].[TemplateEquipmentType]([IdEquipmentType] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TemplateEquipmentType__State]
    ON [Maintenance].[TemplateEquipmentType]([State] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_TemplateEquipmentType_1]
    ON [Maintenance].[TemplateEquipmentType]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control de Concurrencia', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del area 1-activo 0-inactivo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Limpieza del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'Cleaning';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Preucuaciones de manejo del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'HandlingPrecautions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de Funcionamiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'OperationDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Datos generales del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'GeneralData';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Condiciones Del entorno del almacenamiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'Terms';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de equipo relacionado a la plantilla creada', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'IdEquipmentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la plantilla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de plantillas de tipos de equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TemplateEquipmentType', @level2type = N'COLUMN', @level2name = N'Id';


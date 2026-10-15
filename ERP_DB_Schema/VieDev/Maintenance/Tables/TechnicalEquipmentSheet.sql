CREATE TABLE [Maintenance].[TechnicalEquipmentSheet] (
    [Id]                   INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Terms]                VARCHAR (8000) NOT NULL,
    [GeneralData]          VARCHAR (8000) NOT NULL,
    [OperationDescription] VARCHAR (8000) NOT NULL,
    [HandlingPrecautions]  VARCHAR (8000) NOT NULL,
    [Cleaning]             VARCHAR (8000) NOT NULL,
    [IdEquipmentReception] INT            NULL,
    CONSTRAINT [PK_TénicaEquipmentSheet] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TechnicalEquipmentSheet_EquipmentRegistration] FOREIGN KEY ([IdEquipmentReception]) REFERENCES [Maintenance].[EquipmentRegistration] ([Id])
);


GO
ALTER TABLE [Maintenance].[TechnicalEquipmentSheet] NOCHECK CONSTRAINT [FK_TechnicalEquipmentSheet_EquipmentRegistration];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la recepcion del equipo detalle', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Limpieza del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'Cleaning';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Preucuaciones de manejo del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'HandlingPrecautions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de Funcionamiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'OperationDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Datos generales del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'GeneralData';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Condiciones Del entorno del almacenamiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'Terms';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'TechnicalEquipmentSheet', @level2type = N'COLUMN', @level2name = N'Id';


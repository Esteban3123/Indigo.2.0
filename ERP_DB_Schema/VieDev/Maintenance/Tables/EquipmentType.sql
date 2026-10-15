CREATE TABLE [Maintenance].[EquipmentType] (
    [Id]            TINYINT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdParent]      TINYINT      NULL,
    [Code]          VARCHAR (20) NOT NULL,
    [Name]          VARCHAR (60) NOT NULL,
    [InventoryType] CHAR (1)     NOT NULL,
    [TimeStamp]     ROWVERSION   NOT NULL,
    [State]         BIT          NOT NULL,
    CONSTRAINT [PK_EquipmentType__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentType_EquipmentType] FOREIGN KEY ([IdParent]) REFERENCES [Maintenance].[EquipmentType] ([Id])
);


GO
ALTER TABLE [Maintenance].[EquipmentType] NOCHECK CONSTRAINT [FK_EquipmentType_EquipmentType];


GO
CREATE NONCLUSTERED INDEX [IX_EquipmentType__State]
    ON [Maintenance].[EquipmentType]([State] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado el tipo de equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentType', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'columna para controla la concurrencia ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentType', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Inventario 1 - Biomedico 2- Infraestructura 3 - Industrial 4-Muebles y Enseres 5 - Equipos de Computo y Comunicaciones 6 - Otros.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentType', @level2type = N'COLUMN', @level2name = N'InventoryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desripcion del tipo de equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentType', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del tipo del equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentType', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro padre.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentType', @level2type = N'COLUMN', @level2name = N'IdParent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de tipos de equipos', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentType', @level2type = N'COLUMN', @level2name = N'Id';


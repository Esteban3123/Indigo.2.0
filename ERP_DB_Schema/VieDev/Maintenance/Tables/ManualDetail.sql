CREATE TABLE [Maintenance].[ManualDetail] (
    [Id]                   INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdEquipmentReception] INT      NOT NULL,
    [Manual]               CHAR (1) NOT NULL,
    CONSTRAINT [PK_ManualDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ManualDetail_EquipmentRegistration] FOREIGN KEY ([IdEquipmentReception]) REFERENCES [Maintenance].[EquipmentRegistration] ([Id])
);


GO
ALTER TABLE [Maintenance].[ManualDetail] NOCHECK CONSTRAINT [FK_ManualDetail_EquipmentRegistration];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Manual Seleccionado 1-Operacion 2-Mantenimiento 3-Partes 4-Despieces', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ManualDetail', @level2type = N'COLUMN', @level2name = N'Manual';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Recepción del equipo ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ManualDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ManualDetail', @level2type = N'COLUMN', @level2name = N'Id';


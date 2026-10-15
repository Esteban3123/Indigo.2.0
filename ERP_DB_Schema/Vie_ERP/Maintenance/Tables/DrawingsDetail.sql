CREATE TABLE [Maintenance].[DrawingsDetail] (
    [Id]                   INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdEquipmentReception] INT       NOT NULL,
    [Drawings]             CHAR (10) NOT NULL,
    CONSTRAINT [PK_DrawingsDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DrawingsDetail_EquipmentRegistration] FOREIGN KEY ([IdEquipmentReception]) REFERENCES [Maintenance].[EquipmentRegistration] ([Id])
);


GO
ALTER TABLE [Maintenance].[DrawingsDetail] NOCHECK CONSTRAINT [FK_DrawingsDetail_EquipmentRegistration];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plano Seleccionado 1-Electrico 2- Electronicos 3-Hidraulicos 4-Neumaticos 5-Mecanicos', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'DrawingsDetail', @level2type = N'COLUMN', @level2name = N'Drawings';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la recepcion del equipo detalle', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'DrawingsDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de detalle de planos', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'DrawingsDetail', @level2type = N'COLUMN', @level2name = N'Id';


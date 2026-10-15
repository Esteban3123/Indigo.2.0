CREATE TABLE [Maintenance].[EquipmentInvima] (
    [Id]                      SMALLINT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EquipmentRegistrationId] INT           NOT NULL,
    [DateInit]                DATE          NOT NULL,
    [NumberRegister]          VARCHAR (100) NOT NULL,
    [ExpirationDate]          DATE          NOT NULL,
    [State]                   BIT           NOT NULL,
    CONSTRAINT [PK_EquipmentInvima] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentRegistration] FOREIGN KEY ([EquipmentRegistrationId]) REFERENCES [Maintenance].[EquipmentRegistration] ([Id])
);


GO
ALTER TABLE [Maintenance].[EquipmentInvima] NOCHECK CONSTRAINT [FK_EquipmentRegistration];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado | 1 = Activo | 0 = Inactivo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentInvima', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de vencimiento  ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentInvima', @level2type = N'COLUMN', @level2name = N'ExpirationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de registro ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentInvima', @level2type = N'COLUMN', @level2name = N'NumberRegister';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite carga la fecha ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentInvima', @level2type = N'COLUMN', @level2name = N'DateInit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Registro de equipo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentInvima', @level2type = N'COLUMN', @level2name = N'EquipmentRegistrationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EquipmentInvima', @level2type = N'COLUMN', @level2name = N'Id';


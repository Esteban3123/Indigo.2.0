CREATE TABLE [Authorization].[ConfigurationServicesAmbulatoryExceptions] (
    [Id]                                INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ConfigurationServicesAmbulatoryId] INT     NOT NULL,
    [CareGroupId]                       INT     NOT NULL,
    [SusceptibleAuthorization]          BIT     NOT NULL,
    [Assignment]                        INT     NULL,
    [AssignmentUnit]                    TINYINT NULL,
    [Request]                           INT     NULL,
    [RequestUnit]                       TINYINT NULL,
    [Radicated]                         INT     NULL,
    [RadicatedUnit]                     TINYINT NULL,
    [DeliveryService]                   INT     NULL,
    [DeliveryServiceUnit]               TINYINT NULL,
    CONSTRAINT [PK_ConfigurationServicesAmbulatoryExceptions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConfigurationServicesAmbulatoryExceptions_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_ConfigurationServicesAmbulatoryExceptions_ConfigurationServicesAmbulatory] FOREIGN KEY ([ConfigurationServicesAmbulatoryId]) REFERENCES [Authorization].[ConfigurationServicesAmbulatory] ([Id])
);


GO
ALTER TABLE [Authorization].[ConfigurationServicesAmbulatoryExceptions] NOCHECK CONSTRAINT [FK_ConfigurationServicesAmbulatoryExceptions_CareGroup];


GO
ALTER TABLE [Authorization].[ConfigurationServicesAmbulatoryExceptions] NOCHECK CONSTRAINT [FK_ConfigurationServicesAmbulatoryExceptions_ConfigurationServicesAmbulatory];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de entrega al servicio:   1 - Minutos  2 - Horas  3 - Días', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'DeliveryServiceUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de radicado:   1 - Minutos  2 - Horas  3 - Días', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'RadicatedUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de solicitud:   1 - Minutos  2 - Horas  3 - Días', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'RequestUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de asignación:   1 - Minutos  2 - Horas  3 - Días', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'AssignmentUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Representa el tiempo de asignación al turno del Autorizador para iniciar la gestión.   Solicitud: Representa el tiempo máximo que se permite estar en este estado (Afecta barra de estado Dashboard Autorizaciones Ambulatorio)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'Assignment';


CREATE TABLE [Authorization].[ConfigurationServicesAmbulatory] (
    [Id]                                       INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AuthorizationPortfolioCUPSEntityId]       INT     NULL,
    [AuthorizationPortfolioInventoryProductId] INT     NULL,
    [Assignment]                               INT     NOT NULL,
    [AssignmentUnit]                           TINYINT NOT NULL,
    [Request]                                  INT     NOT NULL,
    [RequestUnit]                              TINYINT NOT NULL,
    [Radicated]                                INT     NOT NULL,
    [RadicatedUnit]                            TINYINT NOT NULL,
    [DeliveryService]                          INT     NOT NULL,
    [DeliveryServiceUnit]                      TINYINT NOT NULL,
    CONSTRAINT [PK_ConfigurationServicesAmbulatory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConfigurationServicesAmbulatory_AuthorizationPortfolioCUPSEntity] FOREIGN KEY ([AuthorizationPortfolioCUPSEntityId]) REFERENCES [Authorization].[AuthorizationPortfolioCUPSEntity] ([Id]),
    CONSTRAINT [FK_ConfigurationServicesAmbulatory_AuthorizationPortfolioInventoryProduct] FOREIGN KEY ([AuthorizationPortfolioInventoryProductId]) REFERENCES [Authorization].[AuthorizationPortfolioInventoryProduct] ([Id])
);


GO
ALTER TABLE [Authorization].[ConfigurationServicesAmbulatory] NOCHECK CONSTRAINT [FK_ConfigurationServicesAmbulatory_AuthorizationPortfolioCUPSEntity];


GO
ALTER TABLE [Authorization].[ConfigurationServicesAmbulatory] NOCHECK CONSTRAINT [FK_ConfigurationServicesAmbulatory_AuthorizationPortfolioInventoryProduct];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ConfigurationServicesAmbulatory]
    ON [Authorization].[ConfigurationServicesAmbulatory]([AuthorizationPortfolioCUPSEntityId] ASC, [AuthorizationPortfolioInventoryProductId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de entrega al servicio:   1 - Minutos  2 - Horas  3 - Días', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'DeliveryServiceUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Representa el tiempo máximo que se permite estar en este estado (Afecta barra de estado Dashboard Autorizaciones Ambulatorio)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'DeliveryService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de radicado:   1 - Minutos  2 - Horas  3 - Días', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'RadicatedUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Representa el tiempo máximo que se permite estar en este estado (Afecta barra de estado Dashboard Autorizaciones Ambulatorio)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'Radicated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de solicitud:   1 - Minutos  2 - Horas  3 - Días', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'RequestUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Representa el tiempo máximo que se permite estar en este estado (Afecta barra de estado Dashboard Autorizaciones Ambulatorio)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'Request';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de asignación:   1 - Minutos  2 - Horas  3 - Días', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'AssignmentUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Representa el tiempo de asignación al turno del Autorizador para iniciar la gestión.   Solicitud: Representa el tiempo máximo que se permite estar en este estado (Afecta barra de estado Dashboard Autorizaciones Ambulatorio)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'Assignment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de productos del portafolio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'AuthorizationPortfolioInventoryProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de servicios del portafolio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'AuthorizationPortfolioCUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'Id';


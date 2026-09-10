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



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ConfigurationServicesAmbulatory]
    ON [Authorization].[ConfigurationServicesAmbulatory]([AuthorizationPortfolioCUPSEntityId] ASC, [AuthorizationPortfolioInventoryProductId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para entrega del servicio ambulatorio: 1=Minutos, 2=Horas, 3=Días. Define granularidad temporal del plazo de entrega.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'DeliveryServiceUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de entrega al servicio:   1 - Minutos  2 - Horas  3 - Días', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'DeliveryServiceUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'DeliveryServiceUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo máximo permitido en estado de entrega/prestación del servicio ambulatorio. Impacta visualización barra de progreso en Dashboard de Autorizaciones Ambulatorio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'DeliveryService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Representa el tiempo máximo que se permite estar en este estado (Afecta barra de estado Dashboard Autorizaciones Ambulatorio)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'DeliveryService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'DeliveryService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para radicado de autorización: 1=Minutos, 2=Horas, 3=Días. Define granularidad temporal del plazo de radicación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'RadicatedUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de radicado:   1 - Minutos  2 - Horas  3 - Días', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'RadicatedUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'RadicatedUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo máximo permitido en estado radicado de la autorización ambulatoria. Impacta visualización barra de progreso en Dashboard de Autorizaciones Ambulatorio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'Radicated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Representa el tiempo máximo que se permite estar en este estado (Afecta barra de estado Dashboard Autorizaciones Ambulatorio)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'Radicated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'Radicated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para solicitud de autorización: 1=Minutos, 2=Horas, 3=Días. Define granularidad temporal del plazo de solicitud.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'RequestUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de solicitud:   1 - Minutos  2 - Horas  3 - Días', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'RequestUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'RequestUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo máximo permitido en estado de solicitud de autorización ambulatoria. Impacta visualización barra de progreso en Dashboard de Autorizaciones Ambulatorio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'Request';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Representa el tiempo máximo que se permite estar en este estado (Afecta barra de estado Dashboard Autorizaciones Ambulatorio)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'Request';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'Request';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para asignación al autorizador: 1=Minutos, 2=Horas, 3=Días. Define granularidad temporal del plazo de asignación de turno.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'AssignmentUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de asignación:   1 - Minutos  2 - Horas  3 - Días', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'AssignmentUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'AssignmentUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo máximo para asignación de turno al autorizador e inicio de gestión de autorización ambulatoria. Impacta visualización barra de progreso en Dashboard de Autorizaciones Ambulatorio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'Assignment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Representa el tiempo de asignación al turno del Autorizador para iniciar la gestión.   Solicitud: Representa el tiempo máximo que se permite estar en este estado (Afecta barra de estado Dashboard Autorizaciones Ambulatorio)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'Assignment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'Assignment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del detalle de producto de inventario asociado al portafolio de autorización. Referencia a AuthorizationPortfolioInventoryProduct.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'AuthorizationPortfolioInventoryProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de productos del portafolio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'AuthorizationPortfolioInventoryProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'AuthorizationPortfolioInventoryProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del detalle de servicio CUPS asociado al portafolio de autorización. Referencia a AuthorizationPortfolioCUPSEntity.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'AuthorizationPortfolioCUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de servicios del portafolio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'AuthorizationPortfolioCUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'AuthorizationPortfolioCUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) del registro de configuración de servicios ambulatorios.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory', @level2type = N'COLUMN', @level2name = N'Id';


GO
CREATE NONCLUSTERED INDEX [IDX_ConfigServicesAmbulatory_Service]
    ON [Authorization].[ConfigurationServicesAmbulatory]([AuthorizationPortfolioCUPSEntityId] ASC, [Id] ASC)
    INCLUDE([Request], [RequestUnit], [Radicated], [RadicatedUnit], [DeliveryService], [DeliveryServiceUnit]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de tiempos y unidades permitidos para los servicios ambulatorios en el proceso de autorización. Define los plazos máximos para asignación, solicitud, radicación y entrega de servicios, tanto para procedimientos CUPS como para productos de inventario.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatory';

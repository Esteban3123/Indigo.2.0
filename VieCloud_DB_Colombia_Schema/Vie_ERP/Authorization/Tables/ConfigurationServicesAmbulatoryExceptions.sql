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

CREATE NONCLUSTERED INDEX [IX_ConfigurationServicesAmbulatoryExceptions_ConfigurationServicesAmbulatoryId_CareGroupId]
    ON [Authorization].[ConfigurationServicesAmbulatoryExceptions]([ConfigurationServicesAmbulatoryId] ASC, [CareGroupId] ASC)
    INCLUDE([SusceptibleAuthorization]);

GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para entrega del servicio (1=Minutos, 2=Horas, 3=Días). Define la granularidad de la métrica DeliveryService en configuración de excepciones ambulatorias.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'DeliveryServiceUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de entrega al servicio:   1 - Minutos  2 - Horas  3 - Días', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'DeliveryServiceUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'DeliveryServiceUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo máximo permitido en estado de entrega del servicio (INT, valor numérico). Afecta barra de progreso y alertas en Dashboard de Autorizaciones Ambulatorio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'DeliveryService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Representa el tiempo máximo que se permite estar en este estado (Afecta barra de estado Dashboard Autorizaciones Ambulatorio)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'DeliveryService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'DeliveryService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para radicado de solicitud (1=Minutos, 2=Horas, 3=Días). Define la granularidad de la métrica Radicated en excepciones de autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'RadicatedUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de radicado:   1 - Minutos  2 - Horas  3 - Días', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'RadicatedUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'RadicatedUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo máximo permitido en estado de radicado/presentación de solicitud (INT, valor numérico). Impacta visualización de estado en Dashboard Autorizaciones Ambulatorio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'Radicated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Representa el tiempo máximo que se permite estar en este estado (Afecta barra de estado Dashboard Autorizaciones Ambulatorio)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'Radicated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'Radicated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para solicitud de autorización (1=Minutos, 2=Horas, 3=Días). Define granularidad de la métrica Request en configuración de excepciones.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'RequestUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de solicitud:   1 - Minutos  2 - Horas  3 - Días', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'RequestUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'RequestUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo máximo permitido en estado de solicitud/gestión de autorización (INT, valor numérico). Controla barra de progreso en Dashboard Autorizaciones Ambulatorio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'Request';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Representa el tiempo máximo que se permite estar en este estado (Afecta barra de estado Dashboard Autorizaciones Ambulatorio)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'Request';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'Request';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para asignación al autorizador (1=Minutos, 2=Horas, 3=Días). Define granularidad del tiempo de asignación a turno del gestor de autorizaciones.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'AssignmentUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de asignación:   1 - Minutos  2 - Horas  3 - Días', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'AssignmentUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'AssignmentUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo máximo de asignación al turno del autorizador para iniciar gestión (INT, valor numérico). Afecta barra de estado en Dashboard Autorizaciones Ambulatorio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'Assignment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Representa el tiempo de asignación al turno del Autorizador para iniciar la gestión.   Solicitud: Representa el tiempo máximo que se permite estar en este estado (Afecta barra de estado Dashboard Autorizaciones Ambulatorio)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'Assignment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'Assignment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que marca si la excepción de servicio ambulatorio está sujeta a proceso de autorización formal.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'SusceptibleAuthorization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Suceptible a autorización', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'SusceptibleAuthorization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'SusceptibleAuthorization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT FK) del grupo de atención/contrato asociado. Referencia a [Contract].[CareGroup] para vinculación de excepciones por entidad.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de atención', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT FK) de la configuración de tiempos ambulatorios base. Referencia a [Authorization].[ConfigurationServicesAmbulatory] para excepciones específicas.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'ConfigurationServicesAmbulatoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tiempo', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'ConfigurationServicesAmbulatoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'ConfigurationServicesAmbulatoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de excepción de servicios ambulatorios. Clave primaria clustered.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Excepciones a la configuración de servicios ambulatorios en el proceso de autorización. Define, por grupo de atención, reglas particulares sobre si un servicio es susceptible de autorización y los tiempos límite (en distintas unidades) para asignación, solicitud, radicación y entrega del servicio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ConfigurationServicesAmbulatoryExceptions';

CREATE TABLE [Authorization].[TraceabilityPaperwork] (
    [Id]                                INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AdmissionNumber]                   VARCHAR (20)                                                                     NULL,
    [Folio]                             VARCHAR (20)                                                                     NULL,
    [ServiceCode]                       VARCHAR (20)                                                                     NOT NULL,
    [Type]                              TINYINT                                                                          NOT NULL,
    [PatientCode]                       VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CareCenterCode]                    VARCHAR (20)                                                                     NOT NULL,
    [RequestDate]                       DATETIME                                                                         NOT NULL,
    [RequestQuantity]                   INT                                                                              NOT NULL,
    [DocumentDate]                      DATETIME                                                                         NOT NULL,
    [FunctionalUnitCode]                VARCHAR (20)                                                                     NOT NULL,
    [EntityId]                          INT                                                                              NULL,
    [EntityName]                        VARCHAR (50)                                                                     NULL,
    [CancellationReasonsId]             INT                                                                              NULL,
    [CancellationReasonsObservations]   VARCHAR (MAX)                                                                    NULL,
    [CancellationUserCode]              VARCHAR (20)                                                                     NULL,
    [CancellationDate]                  DATETIME                                                                         NULL,
    [AssignUserCode]                    VARCHAR (20)                                                                     NULL,
    [AuthorizationSourceId]             INT                                                                              NULL,
    [Status]                            TINYINT                                                                          NOT NULL,
    [IsManual]                          BIT                                                                              NOT NULL,
    [CareGroupId]                       INT                                                                              NOT NULL,
    [HealthAdministratorId]             INT                                                                              NULL,
    [ProfessionalCode]                  VARCHAR (20)                                                                     NOT NULL,
    [CareCenterTargetCode]              VARCHAR (20)                                                                     NULL,
    [FunctionalUnitTargetId]            INT                                                                              NULL,
    [AcceptanceStatus]                  TINYINT                                                                          NULL,
    [RejectionObservations]             VARCHAR (MAX)                                                                    NULL,
    [ServiceId]                         INT                                                                              NOT NULL,
    [ContractDescriptionId]             INT                                                                              NULL,
    [AuthorizationOutsourcedServicesId] INT                                                                              NULL,
    [AuthorizationRejectionId]          INT                                                                              NULL,
    [RejectionUserCode]                 VARCHAR (20)                                                                     NULL,
    [PreviousStatus]                    TINYINT                                                                          NULL,
    CONSTRAINT [PK_TraceabilityPaperwork] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TraceabilityPaperwork_AuthorizationOutsourcedServices] FOREIGN KEY ([AuthorizationOutsourcedServicesId]) REFERENCES [Authorization].[AuthorizationOutsourcedServices] ([Id]),
    CONSTRAINT [FK_TraceabilityPaperwork_AuthorizationRejection] FOREIGN KEY ([AuthorizationRejectionId]) REFERENCES [Authorization].[AuthorizationRejection] ([Id]),
    CONSTRAINT [FK_TraceabilityPaperwork_AuthorizationSource] FOREIGN KEY ([AuthorizationSourceId]) REFERENCES [Authorization].[AuthorizationSource] ([Id]),
    CONSTRAINT [FK_TraceabilityPaperwork_CancellationReasons] FOREIGN KEY ([CancellationReasonsId]) REFERENCES [Authorization].[CancellationReasons] ([Id]),
    CONSTRAINT [FK_TraceabilityPaperwork_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_TraceabilityPaperwork_ContractDescriptions] FOREIGN KEY ([ContractDescriptionId]) REFERENCES [Contract].[ContractDescriptions] ([Id]),
    CONSTRAINT [FK_TraceabilityPaperwork_FunctionalUnit] FOREIGN KEY ([FunctionalUnitTargetId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_TraceabilityPaperwork_HealthAdministrator] FOREIGN KEY ([HealthAdministratorId]) REFERENCES [Contract].[HealthAdministrator] ([Id])
);


GO
ALTER TABLE [Authorization].[TraceabilityPaperwork] NOCHECK CONSTRAINT [FK_TraceabilityPaperwork_AuthorizationOutsourcedServices];


GO
ALTER TABLE [Authorization].[TraceabilityPaperwork] NOCHECK CONSTRAINT [FK_TraceabilityPaperwork_AuthorizationRejection];


GO
ALTER TABLE [Authorization].[TraceabilityPaperwork] NOCHECK CONSTRAINT [FK_TraceabilityPaperwork_AuthorizationSource];


GO
ALTER TABLE [Authorization].[TraceabilityPaperwork] NOCHECK CONSTRAINT [FK_TraceabilityPaperwork_CancellationReasons];


GO
ALTER TABLE [Authorization].[TraceabilityPaperwork] NOCHECK CONSTRAINT [FK_TraceabilityPaperwork_CareGroup];


GO
ALTER TABLE [Authorization].[TraceabilityPaperwork] NOCHECK CONSTRAINT [FK_TraceabilityPaperwork_ContractDescriptions];


GO
ALTER TABLE [Authorization].[TraceabilityPaperwork] NOCHECK CONSTRAINT [FK_TraceabilityPaperwork_FunctionalUnit];


GO
ALTER TABLE [Authorization].[TraceabilityPaperwork] NOCHECK CONSTRAINT [FK_TraceabilityPaperwork_HealthAdministrator];


GO
CREATE NONCLUSTERED INDEX [IX_TraceabilityPaperwork_EntityId_EntityName_ServiceCode]
    ON [Authorization].[TraceabilityPaperwork]([EntityId] ASC, [EntityName] ASC, [ServiceCode] ASC)
    INCLUDE([AssignUserCode], [PreviousStatus], [Status]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado anterior del registro:  1 - Solicitado  2 - Radicado  3 - Radicado Pendiente de Autorizacion  4 - Radicado No Autorizado  5 - Autorizado  6 - Autorizado en Entrega  7 - Autorizado Entregado  8 - Agendado (Ya conecta con el EHR)  9 - Ejecutado (Ya conecta con el EHR)  10 - Facturado (Ya conecta con el Proceso de Facturación)  11 - Cancelado: cuando sufre proceso de cancelación  12 - Solicitado en Cotización  13 - Radicado en Cotización  14 - Autorizado Remitido  15 - Autorizado con Cotización  16 - Postergado', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'PreviousStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de aceptación:  1. Aceptado  2. Rechazado    Este campo se asigna desde el formulario de aceptación de autorización', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'AcceptanceStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro:  1 - Solicitado  2 - Radicado  3 - Radicado Pendiente de Autorizacion  4 - Radicado No Autorizado  5 - Autorizado  6 - Autorizado en Entrega  7 - Autorizado Entregado  8 - Agendado (Ya conecta con el EHR)  9 - Ejecutado (Ya conecta con el EHR)  10 - Facturado (Ya conecta con el Proceso de Facturación)  11 - Cancelado: cuando sufre proceso de cancelación  12 - Solicitado en Cotización  13 - Radicado en Cotización  14 - Autorizado Remitido  15 - Autorizado con Cotización  16 - Postergado', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si es un producto o un servicio:   1 - Servicio  2 - Producto', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'Type';


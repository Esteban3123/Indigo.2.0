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
ADD SENSITIVITY CLASSIFICATION TO
    [Authorization].[TraceabilityPaperwork].[PatientCode]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
CREATE NONCLUSTERED INDEX [IX_TraceabilityPaperwork_EntityId_EntityName_ServiceCode]
    ON [Authorization].[TraceabilityPaperwork]([EntityId] ASC, [EntityName] ASC, [ServiceCode] ASC)
    INCLUDE([AssignUserCode], [PreviousStatus], [Status]);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_TraceabilityPaperwork_Entity_Service]
    ON [Authorization].[TraceabilityPaperwork]
       ([EntityName] ASC, [EntityId] ASC, [ServiceCode] ASC)
    WHERE [EntityName] IS NOT NULL AND [EntityId] IS NOT NULL;

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado anterior del registro (TINYINT): 1=Solicitado, 2=Radicado, 3=Radicado Pendiente Autorización, 4=No Autorizado, 5=Autorizado, 6=Autorizado en Entrega, 7=Autorizado Entregado, 8=Agendado (EHR), 9=Ejecutado (EHR), 10=Facturado, 11=Cancelado, 12=Solicitado Cotización, 13=Radicado Cotización, 14=Autorizado Remitido, 15=Autorizado con Cotización, 16=Postergado. Auditoría de transición de estados.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'PreviousStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado anterior del registro:  1 - Solicitado  2 - Radicado  3 - Radicado Pendiente de Autorizacion  4 - Radicado No Autorizado  5 - Autorizado  6 - Autorizado en Entrega  7 - Autorizado Entregado  8 - Agendado (Ya conecta con el EHR)  9 - Ejecutado (Ya conecta con el EHR)  10 - Facturado (Ya conecta con el Proceso de Facturación)  11 - Cancelado: cuando sufre proceso de cancelación  12 - Solicitado en Cotización  13 - Radicado en Cotización  14 - Autorizado Remitido  15 - Autorizado con Cotización  16 - Postergado', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'PreviousStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'PreviousStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario profesional (VARCHAR 20) que rechaza la cotización en formulario de aceptación de autorización. Auditoría de rechazo. Campo de identificación del rechazante.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'RejectionUserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que realiza el rechazo, , este campo se asigna en el formulario de aceptación de cotización al momento de rechazar una cotización', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'RejectionUserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'RejectionUserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) FK a AuthorizationRejection: relaciona rechazo formal en aceptación de cotización. Trazabilidad de motivos de rechazo.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'AuthorizationRejectionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del rechazo, este campo se asigna en el formulario de aceptación de cotización al momento de rechazar una cotización', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'AuthorizationRejectionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'AuthorizationRejectionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) FK a AuthorizationOutsourcedServices: asocia servicios tercerizados remitidos. Asignado en pestaña autorizados.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'AuthorizationOutsourcedServicesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la autorización de servicios tercerizados, se asigna en la pestaña de autorizados al momento de ejecutar la acción de remitir', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'AuthorizationOutsourcedServicesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'AuthorizationOutsourcedServicesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) FK a ContractDescriptions: descripción del CUPS/producto vinculado al contrato. Detalle contractual.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la descripción relacionada con el CUPS', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del producto CUPS o servicio según Type (1=Servicio, 2=Producto). Clave de prestación sanitaria.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'ServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto o cups asociado, depende del campo tipo', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'ServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'ServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentarios observaciones rechazo (VARCHAR MAX): notas del usuario al rechazar cotización. Campo de anotaciones rechazante.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'RejectionObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentarios de rechazo, este campo se asigna en el formulario de aceptación de cotización al momento de rechazar una cotización', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'RejectionObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'RejectionObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado aceptación (TINYINT): 1=Aceptado, 2=Rechazado. Asignado desde formulario aceptación autorización. Confirmación de cotización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'AcceptanceStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de aceptación:  1. Aceptado  2. Rechazado    Este campo se asigna desde el formulario de aceptación de autorización', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'AcceptanceStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'AcceptanceStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) FK a FunctionalUnit: unidad funcional destino para entrega de servicio. Asignado en proceso de entregar.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'FunctionalUnitTargetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad funcional destino, este campo se asigna cuando se realiza el proceso de entregar al servicio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'FunctionalUnitTargetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'FunctionalUnitTargetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (VARCHAR 20) centro atención destino: lugar donde se entrega/ejecuta servicio. Asignado en entrega a servicio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CareCenterTargetCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de centro atención destino, este campo se asigna cuando se realiza el proceso de entregar al servicio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CareCenterTargetCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CareCenterTargetCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (VARCHAR 20) del profesional de la salud que realiza solicitud. Identificador médico solicitante.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que realiza la solicitud', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) FK a HealthAdministrator: entidad aseguradora/administrador. Asignado desde ingreso o manual. Puede ser nulo si grupo atención es particular.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad. Se asigna desde el ingreso y cuando el registro es manual el usuario lo escoge. Puede ir nulo porque el grupo de atención puede ser tipo particular', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) FK a CareGroup: grupo de atención (aseguradora/plan). Asignado desde ingreso o selecciones manual. Obligatorio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de atención. Se asigna del ingreso o cuando el registro es manual el usuario escoge el grupo de atención', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano (BIT): 1=registro creado manualmente sin ingreso, 0=automático desde ingreso. Bandera de creación manual. Ingreso se asigna posterior en control servicios.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'IsManual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el registro fue creado manualmente. Si fue creado manualmente el ingreso se asigna vacío y posteriormente en control servicios ambulatorios se asigna', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'IsManual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'IsManual';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual registro (TINYINT): 1=Solicitado, 2=Radicado, 3=Pendiente Autorización, 4=No Autorizado, 5=Autorizado, 6=Autorizado Entrega, 7=Entregado, 8=Agendado (EHR), 9=Ejecutado (EHR), 10=Facturado, 11=Cancelado, 12=Solicitado Cotización, 13=Radicado Cotización, 14=Remitido, 15=Autorizado Cotización, 16=Postergado. Estado ciclo vida autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro:  1 - Solicitado  2 - Radicado  3 - Radicado Pendiente de Autorizacion  4 - Radicado No Autorizado  5 - Autorizado  6 - Autorizado en Entrega  7 - Autorizado Entregado  8 - Agendado (Ya conecta con el EHR)  9 - Ejecutado (Ya conecta con el EHR)  10 - Facturado (Ya conecta con el Proceso de Facturación)  11 - Cancelado: cuando sufre proceso de cancelación  12 - Solicitado en Cotización  13 - Radicado en Cotización  14 - Autorizado Remitido  15 - Autorizado con Cotización  16 - Postergado', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) FK a AuthorizationSource: origen de la autorización. Asignado al abrir/agregar servicio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'AuthorizationSourceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del origen, se asigna cuando se agrega un servicio en la opción de abrir', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'AuthorizationSourceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'AuthorizationSourceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (VARCHAR 20) usuario propietario del registro en workflow. Usuario responsable/gestor del trámite.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'AssignUserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de usuario que tiene asignado el registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'AssignUserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'AssignUserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha hora (DATETIME) de cancelación del registro. Timestamp cancelación. Auditoría.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CancellationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de cancelación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CancellationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CancellationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (VARCHAR 20) usuario que realiza cancelación. Identificación del cancelante. Auditoría.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CancellationUserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de usuario que cancela el registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CancellationUserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CancellationUserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones motivo cancelación (VARCHAR MAX). Notas explicativas por qué se cancela.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CancellationReasonsObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de la cancelación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CancellationReasonsObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CancellationReasonsObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) FK a CancellationReasons: motivo formal cancelación. Clasificación de rechazo/anulación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CancellationReasonsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del motivo de cancelación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CancellationReasonsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CancellationReasonsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre tabla Crystal origen del registro (VARCHAR 50). Origen del dato: si nulo = agregado manualmente sin ingreso.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la tabla de Crystal del cual viene el registro, si esta vacío es porque se agrego un servicio desde la opción de abrir', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) tabla Crystal origen: trazabilidad documento fuente. Nulo si servicio agregado manual sin ingreso.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla de Crystal del cual viene el registro, si esta vacío es porque se agrego un servicio desde la opción de abrir', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (VARCHAR 20) unidad funcional solicitante. Área/departamento sanitario.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código unidad funcional', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha hora (DATETIME) de radicación/creación del registro. Timestamp documento.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la cual se realiza el registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad (INT) solicitada de servicio/producto. Número de unidades.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'RequestQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad solicitada', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'RequestQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'RequestQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha hora (DATETIME) solicitud original en tabla Crystal. Timestamp requerimiento.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'RequestDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la cual se realizó la solicitud del registro en las tablas de Crystal', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'RequestDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'RequestDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (VARCHAR 20) centro atención donde nace solicitud. Institución/sucursal.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CareCenterCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código centro atención', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CareCenterCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CareCenterCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación paciente (VARCHAR 25, MASKED PII - Identification_Ofuscado): cédula/documento/usuario. FK paciente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación del paciente', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo prestación (TINYINT): 1=Servicio profesional, 2=Producto/medicamento. Clasificación CUPS vs producto.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si es un producto o un servicio:   1 - Servicio  2 - Producto', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (VARCHAR 20) CUPS servicio o código producto según Type. Nomenclador sanitario.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'ServiceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Representa al código del servicio o al código del producto dependiendo de la tabla de Crystal de donde venga el registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'ServiceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'ServiceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número folio (VARCHAR 20) del documento origen. Nulo si agregado manual sin ingreso. Referencia documental.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'Folio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'No. de folio, puede ir vacío cuando el item que se agrega es manual y no tiene ingreso', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'Folio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'Folio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número admisión (VARCHAR 20) del paciente. Nulo si creado manual; se asigna posterior en control servicios. FK ingreso/atención.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'No. de admisión, se asigna el campo nulo cuando se agrega un servicio manual para posteriormente asociarle un ingreso en control servicios ambulatorios', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, IDENTITY PK): clave primaria trazabilidad papelería autorización. Secuencial.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Trazabilidad y gestión de trámites de autorización de servicios de salud: registra cada solicitud de autorización (manual o automática) para un servicio o procedimiento, incluyendo su ciclo de vida completo desde la solicitud hasta la aprobación, rechazo o anulación, asociada a un paciente, ingreso, profesional, centro de atención y entidad aseguradora.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork';
GO

/*==============================================================================================================================
	Author: Karen Esquivel
	Descripcion: Mantiene sincronizada la caché [Authorization].[BotDashboardAuthorization] con los cambios en vivo de
	             TraceabilityPaperwork (Status, AssignUserCode, PreviousStatus), ya que el bot que puebla la caché
	             corre 1 vez/día y sin esto el dashboard queda desactualizado hasta la próxima corrida.
	Sprint : Optimización Dashboard Autorizaciones (2026-07)
==============================================================================================================================*/
CREATE TRIGGER [Authorization].[TR_TraceabilityPaperwork_SyncBotDashboard]
ON [Authorization].[TraceabilityPaperwork]
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH LatestInserted AS (
        SELECT i.EntityId, i.EntityName, i.ServiceCode, i.Id, i.Status, i.AssignUserCode, i.PreviousStatus,
               ROW_NUMBER() OVER (PARTITION BY i.EntityId, i.EntityName, i.ServiceCode ORDER BY i.Id DESC) AS rn
        FROM inserted i
        WHERE i.EntityId IS NOT NULL AND i.EntityName IS NOT NULL AND i.ServiceCode IS NOT NULL
    )
    UPDATE h
    SET h.TraceabilityPaperworkStatus = COALESCE(li.Status, h.TraceabilityPaperworkStatus),
        h.TraceabilityPaperworkId = COALESCE(li.Id, h.TraceabilityPaperworkId),
        h.AssignUserCode = COALESCE(li.AssignUserCode, h.AssignUserCode),
        h.PreviousStatus = COALESCE(li.PreviousStatus, h.PreviousStatus)
    FROM [Authorization].BotDashboardAuthorization h
    JOIN LatestInserted li ON h.EntityId = li.EntityId
                          AND h.EntityName = li.EntityName
                          AND h.ItemCodeOriginal = li.ServiceCode
    WHERE li.rn = 1;
END
GO

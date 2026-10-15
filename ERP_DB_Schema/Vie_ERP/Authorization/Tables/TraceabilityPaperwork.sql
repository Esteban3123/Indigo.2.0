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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que realiza el rechazo, , este campo se asigna en el formulario de aceptación de cotización al momento de rechazar una cotización', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'RejectionUserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del rechazo, este campo se asigna en el formulario de aceptación de cotización al momento de rechazar una cotización', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'AuthorizationRejectionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la autorización de servicios tercerizados, se asigna en la pestaña de autorizados al momento de ejecutar la acción de remitir', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'AuthorizationOutsourcedServicesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la descripción relacionada con el CUPS', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto o cups asociado, depende del campo tipo', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'ServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comentarios de rechazo, este campo se asigna en el formulario de aceptación de cotización al momento de rechazar una cotización', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'RejectionObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de aceptación:  1. Aceptado  2. Rechazado    Este campo se asigna desde el formulario de aceptación de autorización', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'AcceptanceStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad funcional destino, este campo se asigna cuando se realiza el proceso de entregar al servicio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'FunctionalUnitTargetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de centro atención destino, este campo se asigna cuando se realiza el proceso de entregar al servicio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CareCenterTargetCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profesional que realiza la solicitud', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la entidad. Se asigna desde el ingreso y cuando el registro es manual el usuario lo escoge. Puede ir nulo porque el grupo de atención puede ser tipo particular', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo de atención. Se asigna del ingreso o cuando el registro es manual el usuario escoge el grupo de atención', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el registro fue creado manualmente. Si fue creado manualmente el ingreso se asigna vacío y posteriormente en control servicios ambulatorios se asigna', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'IsManual';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro:  1 - Solicitado  2 - Radicado  3 - Radicado Pendiente de Autorizacion  4 - Radicado No Autorizado  5 - Autorizado  6 - Autorizado en Entrega  7 - Autorizado Entregado  8 - Agendado (Ya conecta con el EHR)  9 - Ejecutado (Ya conecta con el EHR)  10 - Facturado (Ya conecta con el Proceso de Facturación)  11 - Cancelado: cuando sufre proceso de cancelación  12 - Solicitado en Cotización  13 - Radicado en Cotización  14 - Autorizado Remitido  15 - Autorizado con Cotización  16 - Postergado', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del origen, se asigna cuando se agrega un servicio en la opción de abrir', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'AuthorizationSourceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de usuario que tiene asignado el registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'AssignUserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de cancelación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CancellationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de usuario que cancela el registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CancellationUserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de la cancelación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CancellationReasonsObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del motivo de cancelación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CancellationReasonsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la tabla de Crystal del cual viene el registro, si esta vacío es porque se agrego un servicio desde la opción de abrir', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla de Crystal del cual viene el registro, si esta vacío es porque se agrego un servicio desde la opción de abrir', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código unidad funcional', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en la cual se realiza el registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad solicitada', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'RequestQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en la cual se realizó la solicitud del registro en las tablas de Crystal', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'RequestDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código centro atención', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'CareCenterCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificación del paciente', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si es un producto o un servicio:   1 - Servicio  2 - Producto', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Representa al código del servicio o al código del producto dependiendo de la tabla de Crystal de donde venga el registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'ServiceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No. de folio, puede ir vacío cuando el item que se agrega es manual y no tiene ingreso', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'Folio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No. de admisión, se asigna el campo nulo cuando se agrega un servicio manual para posteriormente asociarle un ingreso en control servicios ambulatorios', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperwork', @level2type = N'COLUMN', @level2name = N'Id';


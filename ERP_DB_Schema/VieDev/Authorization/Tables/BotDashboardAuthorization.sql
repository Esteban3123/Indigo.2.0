CREATE TABLE [Authorization].[BotDashboardAuthorization] (
    [Id]                                         INT                                                                              IDENTITY (1, 1) NOT NULL,
    [UniqueCode]                                 VARCHAR (200)                                                                    NOT NULL,
    [EntityName]                                 VARCHAR (21)                                                                     NOT NULL,
    [EntityId]                                   INT                                                                              NOT NULL,
    [CareCenterCode]                             VARCHAR (20)                                                                     NOT NULL,
    [CareCenterCodeName]                         VARCHAR (123)                                                                    NOT NULL,
    [FunctionalUnitCode]                         VARCHAR (20)                                                                     NOT NULL,
    [FunctionalUnitName]                         CHAR (60)                                                                        NOT NULL,
    [FunctionalUnitCodeName]                     VARCHAR (83)                                                                     NOT NULL,
    [AdmissionNumber]                            VARCHAR (20)                                                                     NULL,
    [Folio]                                      NVARCHAR (20)                                                                    NULL,
    [TypeClinicalHistory]                        INT                                                                              NOT NULL,
    [CareGroupId]                                INT                                                                              NULL,
    [CareGroupCode]                              VARCHAR (20)                                                                     NULL,
    [CareGroupName]                              VARCHAR (100)                                                                    NULL,
    [CareGroupCodeName]                          VARCHAR (123)                                                                    NOT NULL,
    [HealthAdministratorId]                      INT                                                                              NULL,
    [HealthAdministratorCode]                    VARCHAR (20)                                                                     NULL,
    [HealthAdministratorName]                    VARCHAR (300)                                                                    NULL,
    [HealthAdministratorCodeName]                VARCHAR (323)                                                                    NOT NULL,
    [PatientCode]                                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [PatientName]                                VARCHAR (250) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)')          NULL,
    [PatientAddress]                             VARCHAR (MAX)                                                                    NOT NULL,
    [PatientPhone]                               VARCHAR (MAX)                                                                    NOT NULL,
    [PatientAge]                                 NVARCHAR (100)                                                                   NULL,
    [RequestDate]                                DATETIME                                                                         NULL,
    [ProfessionalCode]                           VARCHAR (20)                                                                     NOT NULL,
    [Quantity]                                   INT                                                                              NULL,
    [Type]                                       INT                                                                              NOT NULL,
    [ServiceId]                                  INT                                                                              NULL,
    [ServiceCode]                                VARCHAR (20)                                                                     NULL,
    [ItemCodeOriginal]                           VARCHAR (20)                                                                     NULL,
    [ServiceDescription]                         VARCHAR (323)                                                                    NOT NULL,
    [ContractDescriptionId]                      INT                                                                              NULL,
    [ContractDescriptionCodeName]                VARCHAR (273)                                                                    NULL,
    [IsCovered]                                  INT                                                                              NOT NULL,
    [Contracted]                                 INT                                                                              NOT NULL,
    [Quoted]                                     INT                                                                              NOT NULL,
    [AssignUserCode]                             VARCHAR (20)                                                                     NOT NULL,
    [AssignUser]                                 VARCHAR (273)                                                                    NOT NULL,
    [RequestTime]                                INT                                                                              NULL,
    [RequestUnitTime]                            INT                                                                              NULL,
    [RequestElapsedTime]                         INT                                                                              NULL,
    [ColorRequest]                               INT                                                                              NULL,
    [TraceabilityPaperworkId]                    INT                                                                              NULL,
    [TraceabilityPaperworkStatus]                TINYINT                                                                          NOT NULL,
    [TraceabilityPaperworkEventsId]              INT                                                                              NULL,
    [TraceabilityPaperworkEventsStatus]          TINYINT                                                                          NULL,
    [AuthorizationSourceId]                      INT                                                                              NULL,
    [IsManual]                                   BIT                                                                              NOT NULL,
    [Observations]                               VARCHAR (MAX)                                                                    NULL,
    [Alert]                                      INT                                                                              NOT NULL,
    [PatientThirdPartyId]                        INT                                                                              NULL,
    [AuthorizationGroupId]                       INT                                                                              NOT NULL,
    [AuthorizationGroupCodeName]                 VARCHAR (123)                                                                    NOT NULL,
    [ProfessionalCodeName]                       VARCHAR (83)                                                                     NULL,
    [CareCenterTargetCodeName]                   VARCHAR (113)                                                                    NULL,
    [FunctionalUnitTargetCodeName]               VARCHAR (73)                                                                     NULL,
    [DiagnosticCode]                             VARCHAR (4)                                                                      NULL,
    [DiagnosticDescription]                      VARCHAR (357)                                                                    NULL,
    [TraceabilityPaperworkPostponementReasonsId] INT                                                                              NULL,
    [PostponementReasonsId]                      INT                                                                              NULL,
    [PostponementDate]                           DATETIME                                                                         NULL,
    [PostponementCreationDate]                   DATETIME                                                                         NULL,
    [PostponementCreationUser]                   VARCHAR (20)                                                                     NULL,
    [PostponementCodeName]                       VARCHAR (123)                                                                    NULL,
    [PreviousStatus]                             TINYINT                                                                          NULL,
    CONSTRAINT [PK_BotDashboardAuthorization] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UC_UniqueCode] UNIQUE NONCLUSTERED ([UniqueCode] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado anterior del registro de la tabla TraceabilityPaperwork.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PreviousStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concatenación del Código y nombre del motivo de postergación de la tabla PostponementReasons.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PostponementCodeName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación de la tabla TraceabilityPaperworkPostponementReasons.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PostponementCreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación de la tabla TraceabilityPaperworkPostponementReasons.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PostponementCreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la postergación de la tabla TraceabilityPaperworkPostponementReasons.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PostponementDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del motivo de la postergación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PostponementReasonsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la trazabilidad de Motivos de aplazamiento del trámite.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkPostponementReasonsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del diagnostico.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'DiagnosticDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del diagnostico.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'DiagnosticCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concatenación de Código y Nombre de la Unidad Funcional de la tabla FunctionalUnit.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'FunctionalUnitTargetCodeName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención obtenido de la tabla ADCENATEN.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareCenterTargetCodeName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concatenación del Código y Nombre profesional.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concatena el Código y Nombre del Grupo de Autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'AuthorizationGroupCodeName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Grupo de Autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'AuthorizationGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tercero asociado a un paciente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PatientThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hace referencia al estado de una alerta, 1 si se encuentra activa en la tabla TraceabilityPaperworkAlert, 0 si no es asi.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Alert';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contiene las Observaciones.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo llenado de la tabla TraceabilityPaperwork. Especifica si el registro fue creado manualmente. Si fue creado manualmente el ingreso se asigna vacío y posteriormente en control servicios ambulatorios se asigna.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'IsManual';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la Fuente de Autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'AuthorizationSourceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la documentación de trazabilidad de eventos.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la documentación de trazabilidad de eventos.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la documentación de trazabilidad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la documentación de trazabilidad.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referente a la fecha de postergación que se encuentra en la tabla TraceabilityPaperworkPostponementReasons. Esta variable guardará 1 si la fecha es mayor a la actual, 2 si es igual a la actual o 3 si es menor a esta.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ColorRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el tiempo trascurrido de la solicitud. Se llena con el resultado de la función GetRequestTime', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'RequestElapsedTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece la unidad de tiempo de la solicitud. Se llena con el resultado de la función GetRequestUnitTime.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'RequestUnitTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el tiempo de la solicitud. Se llena con el resultado de la función GetRequestTime.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'RequestTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Usuario con el que se ingresa.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'AssignUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Usuario que tiene asignado el registro, obtenido de la tabla TraceabilityPaperwork.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'AssignUserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de la tabla ProcedureCups que permite saber si el servicio está cotizado.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Quoted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Información que permite saber si el producto es contratado de la tabla ProductRateDetail.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Contracted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valida que exista un procedimiento CUPS en la tabla de Contratos ProcedureCups. Donde 1 es que existe y 0 que no.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'IsCovered';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concatenación del Código y Nombre de la descripción del contrato.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ContractDescriptionCodeName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la descripción del contrato.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del servicio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ServiceDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el Código Original del Item, se llena de la vista ViewListRequest.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ItemCodeOriginal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ServiceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del servicio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el tipo asignado en la vista ViewListRequest', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece la cantidad asignada en la vista ViewListRequest.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el código profesional.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece la Fecha de Solicitud.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'RequestDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la edad del paciente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PatientAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el número telefónico del paciente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PatientPhone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la dirección del paciente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PatientAddress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el nombre del paciente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PatientName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el código del paciente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concatenación del Código y Nombre del Administrador de Salud.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'HealthAdministratorCodeName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del Administrador de Salud.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'HealthAdministratorName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Administrador de Salud.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'HealthAdministratorCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Administrador de Salud.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concatenación del Código y Nombre del Grupo de Atención.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareGroupCodeName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del Grupo de Atención.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareGroupName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Grupo de Atención.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareGroupCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Grupo de Atención.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el tipo de historia clinica de la tabla HCHISPACA, donde 1 puede ser el tipo "I","O","PT" o "F", 2 puede ser "E" o "V", 3 es "N", 4 es "T", 5 es "P", 6 es "S", 7 es "NF", 8 es "B", y si no es ninguna es 0.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'TypeClinicalHistory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Este campo hace referencia al Folio y se llena con la vista ViewListRequest.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Folio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numero de ingreso del paciente de la tabla ADINGRESO.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concatenación de Código y Nombre de la Unidad Funcional desde la vista ViewListRequest.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCodeName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la Unidad Funcional desde la vista ViewListRequest.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'FunctionalUnitName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Unidad Funcional desde la vista ViewListRequest.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concatenación del Código y Nombre del Centro de Atención.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareCenterCodeName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareCenterCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la entidad.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la entidad.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el Código Unico.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'UniqueCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Registro.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Id';


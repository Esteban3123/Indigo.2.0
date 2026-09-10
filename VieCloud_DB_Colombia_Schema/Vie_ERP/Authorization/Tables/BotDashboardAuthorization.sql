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
    [PatientNameAge]                             AS (CONCAT([PatientName], ' - ', [PatientAge])) PERSISTED,
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
CREATE NONCLUSTERED INDEX [IX_BotDashboardAuthorization_Dashboard_EntityService]
    ON [Authorization].[BotDashboardAuthorization]
       ([EntityName] ASC, [EntityId] ASC, [ItemCodeOriginal] ASC)
    INCLUDE
       ([Id], [UniqueCode], [CareCenterCode], [Type], [ServiceId],
        [ContractDescriptionId], [RequestDate], [TraceabilityPaperworkStatus]);
GO

CREATE NONCLUSTERED INDEX [IX_BotDashboardAuthorization_Dashboard_Covering]
    ON [Authorization].[BotDashboardAuthorization]
       ([CareCenterCode] ASC, [Type] ASC, [ServiceId] ASC, [ContractDescriptionId] ASC, [TraceabilityPaperworkStatus] ASC)
    INCLUDE
       ([UniqueCode], [EntityName], [EntityId], [CareCenterCodeName], [FunctionalUnitCode], [FunctionalUnitName],
        [FunctionalUnitCodeName], [AdmissionNumber], [Folio], [TypeClinicalHistory], [CareGroupId], [CareGroupCode],
        [CareGroupName], [CareGroupCodeName], [HealthAdministratorId], [HealthAdministratorCode], [HealthAdministratorName],
        [HealthAdministratorCodeName], [PatientCode], [PatientName], [PatientAddress], [PatientPhone], [PatientAge],
        [PatientNameAge], [RequestDate], [ProfessionalCode], [Quantity], [ServiceCode], [ItemCodeOriginal], [ServiceDescription],
        [ContractDescriptionCodeName], [IsCovered], [Contracted], [Quoted], [AssignUserCode], [AssignUser],
        [RequestTime], [RequestUnitTime], [RequestElapsedTime], [TraceabilityPaperworkId], [TraceabilityPaperworkEventsId],
        [TraceabilityPaperworkEventsStatus], [AuthorizationSourceId], [IsManual], [Observations], [Alert],
        [PatientThirdPartyId], [AuthorizationGroupId], [AuthorizationGroupCodeName], [ProfessionalCodeName],
        [CareCenterTargetCodeName], [FunctionalUnitTargetCodeName], [DiagnosticCode], [DiagnosticDescription],
        [TraceabilityPaperworkPostponementReasonsId], [PostponementReasonsId], [PostponementDate],
        [PostponementCreationDate], [PostponementCreationUser], [PostponementCodeName], [PreviousStatus]);
GO

CREATE NONCLUSTERED INDEX [IX_BotDashboardAuthorization_Dashboard_Navigation]
    ON [Authorization].[BotDashboardAuthorization]
       ([TraceabilityPaperworkStatus] ASC, [CareCenterCode] ASC, [PatientNameAge] ASC, [RequestDate] DESC, [Id] DESC);
GO

ADD SENSITIVITY CLASSIFICATION TO
    [Authorization].[BotDashboardAuthorization].[PatientCode]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');

GO
ADD SENSITIVITY CLASSIFICATION TO
    [Authorization].[BotDashboardAuthorization].[PatientName]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado anterior del registro de trazabilidad (TraceabilityPaperwork). TINYINT: 0-255. Auditoría de cambios de estado en autorización/trámite.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PreviousStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado anterior del registro de la tabla TraceabilityPaperwork.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PreviousStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PreviousStatus';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código + nombre del motivo de postergación/aplazamiento (PostponementReasons). VARCHAR(123). Buscar: razón de demora, causa de retraso.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PostponementCodeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concatenación del Código y nombre del motivo de postergación de la tabla PostponementReasons.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PostponementCodeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PostponementCodeName';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de postergación en TraceabilityPaperworkPostponementReasons. VARCHAR(20). Auditoría: profesional/administrativo.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PostponementCreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación de la tabla TraceabilityPaperworkPostponementReasons.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PostponementCreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PostponementCreationUser';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de postergación (TraceabilityPaperworkPostponementReasons). DATETIME. Trazabilidad de aplazamientos.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PostponementCreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación de la tabla TraceabilityPaperworkPostponementReasons.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PostponementCreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PostponementCreationDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha programada de la postergación/aplazamiento del trámite (TraceabilityPaperworkPostponementReasons). DATETIME. Control de vencimientos.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PostponementDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la postergación de la tabla TraceabilityPaperworkPostponementReasons.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PostponementDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PostponementDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del motivo de postergación. INT (FK PostponementReasons). Clasificar aplazamientos.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PostponementReasonsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del motivo de la postergación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PostponementReasonsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PostponementReasonsId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de trazabilidad de motivos de aplazamiento. INT (FK). Auditoría completa de postergaciones.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkPostponementReasonsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la trazabilidad de Motivos de aplazamiento del trámite.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkPostponementReasonsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkPostponementReasonsId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual del diagnóstico CIE-10/médico. VARCHAR(357). Buscar: enfermedad, condición, patología, síntoma.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'DiagnosticDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción del diagnostico.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'DiagnosticDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'DiagnosticDescription';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico (CIE-10 o estándar). VARCHAR(4). PII sensible: diagnóstico médico del paciente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'DiagnosticCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diagnostico.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'DiagnosticCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'DiagnosticCode';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código + nombre de la unidad funcional destino (FunctionalUnit). VARCHAR(73). Buscar: servicio, departamento, área clínica.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'FunctionalUnitTargetCodeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concatenación de Código y Nombre de la Unidad Funcional de la tabla FunctionalUnit.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'FunctionalUnitTargetCodeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'FunctionalUnitTargetCodeName';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código + nombre del centro de atención destino (ADCENATEN). VARCHAR(113). Buscar: clínica, hospital, sede, institución.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareCenterTargetCodeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del Centro de Atención obtenido de la tabla ADCENATEN.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareCenterTargetCodeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareCenterTargetCodeName';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código + nombre del profesional de salud. VARCHAR(83). Buscar: médico, especialista, enfermero, terapeuta.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concatenación del Código y Nombre profesional.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeName';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código + nombre del grupo de autorización/clasificación. VARCHAR(123). Buscar: categoría de autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'AuthorizationGroupCodeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concatena el Código y Nombre del Grupo de Autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'AuthorizationGroupCodeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'AuthorizationGroupCodeName';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de autorización. INT (FK). Clasificación de solicitudes autorizables.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'AuthorizationGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Grupo de Autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'AuthorizationGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'AuthorizationGroupId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero/beneficiario asociado al paciente. INT. PII: persona relacionada en facturación/contrato.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PatientThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero asociado a un paciente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PatientThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PatientThirdPartyId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de alerta activa (TraceabilityPaperworkAlert). INT (0=inactivo, 1=activo). Flag de atención prioritaria.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Alert';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hace referencia al estado de una alerta, 1 si se encuentra activa en la tabla TraceabilityPaperworkAlert, 0 si no es asi.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Alert';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Alert';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de observaciones, notas o comentarios adicionales. VARCHAR(MAX). Búsqueda libre: anotaciones clínicas/administrativas.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene las Observaciones.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Observations';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el registro fue creado manualmente vs. automático. BIT (1=manual, 0=automático). Si manual, ingreso vacío; asignación posterior en servicios ambulatorios.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'IsManual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo llenado de la tabla TraceabilityPaperwork. Especifica si el registro fue creado manualmente. Si fue creado manualmente el ingreso se asigna vacío y posteriormente en control servicios ambulatorios se asigna.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'IsManual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'IsManual';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la fuente/origen de la autorización. INT (FK). Buscar: de dónde viene la solicitud (usuario, sistema, API).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'AuthorizationSourceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Fuente de Autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'AuthorizationSourceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'AuthorizationSourceId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la documentación de eventos de trazabilidad. TINYINT. Validación de cumplimiento de hitos.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la documentación de trazabilidad de eventos.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsStatus';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de eventos en trazabilidad de documentación. INT (FK). Auditoría: registro de acciones y cambios.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la documentación de trazabilidad de eventos.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual de la documentación/trazabilidad del trámite. TINYINT. Buscar: pendiente, en proceso, completado, rechazado.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la documentación de trazabilidad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkStatus';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la documentación de trazabilidad. INT (FK). Núcleo: auditoría completa del trámite.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la documentación de trazabilidad.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador visual del estado de postergación vs. fecha actual. INT (1=futuro, 2=hoy, 3=vencido). Código de color: verde/amarillo/rojo.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ColorRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referente a la fecha de postergación que se encuentra en la tabla TraceabilityPaperworkPostponementReasons. Esta variable guardará 1 si la fecha es mayor a la actual, 2 si es igual a la actual o 3 si es menor a esta.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ColorRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ColorRequest';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo transcurrido desde la solicitud hasta ahora (función GetRequestTime). INT. Métricas: días/horas/minutos de espera.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'RequestElapsedTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el tiempo trascurrido de la solicitud. Se llena con el resultado de la función GetRequestTime', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'RequestElapsedTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'RequestElapsedTime';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo de la solicitud (días/horas/minutos, función GetRequestUnitTime). INT. Complementa RequestTime.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'RequestUnitTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece la unidad de tiempo de la solicitud. Se llena con el resultado de la función GetRequestUnitTime.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'RequestUnitTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'RequestUnitTime';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo de la solicitud calculado por función GetRequestTime. INT. SLA: cumplimiento de tiempos de atención.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'RequestTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el tiempo de la solicitud. Se llena con el resultado de la función GetRequestTime.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'RequestTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'RequestTime';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario operador que ingresó el registro. VARCHAR(273). Auditoría: quién creó la autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'AssignUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del Usuario con el que se ingresa.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'AssignUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'AssignUser';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario asignado al registro (TraceabilityPaperwork). VARCHAR(20). Responsable actual del trámite.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'AssignUserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de Usuario que tiene asignado el registro, obtenido de la tabla TraceabilityPaperwork.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'AssignUserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'AssignUserCode';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de servicio cotizado en CUPS (ProcedureCups). INT (0=no, 1=sí). Validación de valor/precio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Quoted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo de la tabla ProcedureCups que permite saber si el servicio está cotizado.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Quoted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Quoted';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de servicio contratado (ProductRateDetail). INT (0=no contratado, 1=contratado). Cobertura del seguro.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Contracted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Información que permite saber si el producto es contratado de la tabla ProductRateDetail.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Contracted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Contracted';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Validación de cobertura: existe en contrato CUPS (ProcedureCups). INT (0=no cubierto, 1=cubierto). Determinante de responsabilidad de pago.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'IsCovered';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valida que exista un procedimiento CUPS en la tabla de Contratos ProcedureCups. Donde 1 es que existe y 0 que no.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'IsCovered';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'IsCovered';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código + nombre de la descripción/plan del contrato. VARCHAR(273). Buscar: paquete, cobertura, contrato específico.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ContractDescriptionCodeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concatenación del Código y Nombre de la descripción del contrato.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ContractDescriptionCodeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ContractDescriptionCodeName';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de descripción del contrato. INT (FK). Referencias de cobertura y beneficios.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la descripción del contrato.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ContractDescriptionId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual del servicio/procedimiento. VARCHAR(323). Búsqueda libre: qué se solicita.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ServiceDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción del servicio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ServiceDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ServiceDescription';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código original del ítem de la solicitud (ViewListRequest). VARCHAR(20). Trazabilidad de cambios de código.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ItemCodeOriginal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Código Original del Item, se llena de la vista ViewListRequest.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ItemCodeOriginal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ItemCodeOriginal';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio/procedimiento. VARCHAR(20). Buscar: CUPS, código interno, identificador único del servicio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ServiceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del servicio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ServiceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ServiceCode';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del servicio/procedimiento. INT (FK). Referencias cruzadas de servicios.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del servicio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ServiceId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo asignado en ViewListRequest. INT. Clasificación: consulta, procedimiento, examen, internación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el tipo asignado en la vista ViewListRequest', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Type';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad solicitada del servicio/procedimiento (ViewListRequest). INT. Búsqueda: cuántas unidades se requieren.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece la cantidad asignada en la vista ViewListRequest.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Quantity';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud solicitante/prestador. VARCHAR(20). Buscar: cédula profesional, identificación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el código profesional.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la solicitud de autorización. DATETIME. Filtro temporal: cuándo se solicitó.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'RequestDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece la Fecha de Solicitud.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'RequestDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'RequestDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad del paciente. NVARCHAR(100). Demografía: rango etario para análisis epidemiológico.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PatientAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la edad del paciente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PatientAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PatientAge';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número telefónico del paciente. VARCHAR(MAX). PII: contacto, comunicación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PatientPhone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el número telefónico del paciente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PatientPhone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PatientPhone';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección domiciliaria del paciente. VARCHAR(MAX). PII: ubicación geográfica.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PatientAddress';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la dirección del paciente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PatientAddress';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PatientAddress';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del paciente. VARCHAR(250). PII ofuscado. Buscar: identificación de beneficiario.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PatientName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el nombre del paciente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PatientName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PatientName';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/identificación del paciente (cédula, pasaporte, etc.). VARCHAR(25). PII ofuscado: Identification_Ofuscado. Clave primaria de paciente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el código del paciente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'PatientCode';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código + nombre del administrador/asegurador de salud. VARCHAR(323). Buscar: EPS, aseguradora, entidad responsable de pago.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'HealthAdministratorCodeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concatenación del Código y Nombre del Administrador de Salud.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'HealthAdministratorCodeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'HealthAdministratorCodeName';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la entidad administradora de salud (EPS/ARS). VARCHAR(300). Entidad responsable de cobertura.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'HealthAdministratorName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Administrador de Salud.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'HealthAdministratorName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'HealthAdministratorName';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la administradora de salud. VARCHAR(20). Identificación del pagador/asegurador.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'HealthAdministratorCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del Administrador de Salud.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'HealthAdministratorCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'HealthAdministratorCode';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la administradora de salud. INT (FK). Referencias de entidades de pago.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Administrador de Salud.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código + nombre del grupo de atención/línea de servicio. VARCHAR(123). Buscar: medicina general, especialidad, urgencia.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareGroupCodeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concatenación del Código y Nombre del Grupo de Atención.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareGroupCodeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareGroupCodeName';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del grupo de atención/especialidad. VARCHAR(100). Clasificación clínica de servicios.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareGroupName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Grupo de Atención.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareGroupName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareGroupName';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del grupo de atención. VARCHAR(20). Identificación de línea de servicio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareGroupCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del Grupo de Atención.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareGroupCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareGroupCode';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de atención. INT (FK). Organización de servicios clínicos.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Grupo de Atención.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareGroupId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de historia clínica (HCHISPACA: I/O/PT/F=1, E/V=2, N=3, T=4, P=5, S=6, NF=7, B=8, otro=0). INT. Auditoría: documento clínico generado.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'TypeClinicalHistory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el tipo de historia clinica de la tabla HCHISPACA, donde 1 puede ser el tipo "I","O","PT" o "F", 2 puede ser "E" o "V", 3 es "N", 4 es "T", 5 es "P", 6 es "S", 7 es "NF", 8 es "B", y si no es ninguna es 0.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'TypeClinicalHistory';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'TypeClinicalHistory';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio/referencia de la solicitud (ViewListRequest). NVARCHAR(20). Buscar: número de gestión/expediente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Folio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este campo hace referencia al Folio y se llena con la vista ViewListRequest.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Folio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Folio';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/admisión del paciente (ADINGRESO). VARCHAR(20). Buscar: episodio de atención, internación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de ingreso del paciente de la tabla ADINGRESO.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código + nombre de unidad funcional (ViewListRequest). VARCHAR(83). Buscar: servicio, departamento, área asistencial.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCodeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concatenación de Código y Nombre de la Unidad Funcional desde la vista ViewListRequest.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCodeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCodeName';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la unidad funcional/departamento (ViewListRequest). CHAR(60). Búsqueda: dónde se presta el servicio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'FunctionalUnitName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Unidad Funcional desde la vista ViewListRequest.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'FunctionalUnitName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'FunctionalUnitName';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (ViewListRequest). VARCHAR(20). ID de departamento/área clínica.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la Unidad Funcional desde la vista ViewListRequest.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCode';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código + nombre del centro de atención. VARCHAR(123). Buscar: hospital, clínica, sede, institución prestadora.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareCenterCodeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concatenación del Código y Nombre del Centro de Atención.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareCenterCodeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareCenterCodeName';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención/institución. VARCHAR(20). Identificación: dónde se realiza la atención.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareCenterCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del Centro de Atención.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareCenterCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'CareCenterCode';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad (organización padre). INT. Jerarquía: empresa/grupo económico.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'EntityId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la entidad/organización. VARCHAR(21). Clasificación: institución prestadora o administradora.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la entidad.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico único del registro. INT IDENTITY. PK: clave primaria de BotDashboardAuthorization.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Registro.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization', @level2type = N'COLUMN', @level2name = N'Id';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de solicitudes de autorización de servicios de salud gestionadas por el bot de autorizaciones. Consolida información del paciente, el servicio solicitado, el contrato, el profesional, el centro de atención, el estado de trámite y los tiempos de respuesta para el seguimiento y control del proceso de autorización.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'BotDashboardAuthorization';

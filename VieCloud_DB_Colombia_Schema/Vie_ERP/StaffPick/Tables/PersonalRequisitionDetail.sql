CREATE TABLE [StaffPick].[PersonalRequisitionDetail] (
    [Id]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PersonalRequisitionId]   INT           NOT NULL,
    [StudyTypeId]             INT           NULL,
    [VacancyType]             TINYINT       NOT NULL,
    [ReasonForVacancy]        TINYINT       NOT NULL,
    [TypesOfCall]             TINYINT       NULL,
    [ExperienceInPosition]    TINYINT       NULL,
    [ContractTypeId]          INT           NULL,
    [ApplicationDate]         DATE          NOT NULL,
    [NumberEmployeesRequired] TINYINT       NOT NULL,
    [ContractPeriod]          VARCHAR (30)  NULL,
    [Observations]            VARCHAR (300) NULL,
    [ModificationDate]        DATETIME      NOT NULL,
    [UserStatus]              VARCHAR (20)  CONSTRAINT [DF_PersonalRequisitionDetail_UserStatus] DEFAULT ((999)) NOT NULL,
    [StatusRequest]           TINYINT       CONSTRAINT [DF_PersonalRequisitionDetail_StatusRequest] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PersonalRequisitionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonalRequisitionDetail_ContractType] FOREIGN KEY ([ContractTypeId]) REFERENCES [Payroll].[ContractType] ([Id]),
    CONSTRAINT [FK_PersonalRequisitionDetail_PersonalRequisition] FOREIGN KEY ([PersonalRequisitionId]) REFERENCES [StaffPick].[PersonalRequisition] ([Id]),
    CONSTRAINT [FK_PersonalRequisitionDetail_StudyType] FOREIGN KEY ([StudyTypeId]) REFERENCES [Payroll].[StudyType] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la solicitud de personal (0=Pendiente, 1=Aprobada, 2=Rechazada, etc.). Tipo: TINYINT. Indica el estado actual del trámite de requisición.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'StatusRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Solicitud de estado', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'StatusRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'StatusRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del usuario que registra la solicitud (999=Activo). Tipo: VARCHAR(20). Identificador de estado del solicitante en el sistema.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'UserStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del usuario ', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'UserStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'UserStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del detalle de requisición. Tipo: DATETIME. Auditoria de cambios.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas, comentarios o detalles adicionales sobre la requisición de personal (máx. 300 caracteres). Tipo: VARCHAR(300). Campo descriptivo para observaciones de recursos humanos.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones ', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración o período del contrato (ej: 3 meses, 1 año, indefinido). Tipo: VARCHAR(30). Especifica la vigencia del vínculo laboral.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'ContractPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duración del Contrato', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'ContractPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'ContractPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de empleados a contratar para esta vacante. Tipo: TINYINT. Número de posiciones abiertas a cubrir.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'NumberEmployeesRequired';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Empleados Requeridos', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'NumberEmployeesRequired';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'NumberEmployeesRequired';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio solicitada para el empleado en la vacante. Tipo: DATE. Momento esperado de ingreso del nuevo personal.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'ApplicationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicio Solicitada', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'ApplicationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'ApplicationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de contrato (FK a Payroll.ContractType). Tipo: INT. Referencia al régimen: término fijo, indefinido, aprendizaje, prestación de servicios, etc.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'ContractTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Tipo de Contrato', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'ContractTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'ContractTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de experiencia requerido: 1=Cargos similares, 2=En el puesto, 3=En el sector, 4=No requiere. Tipo: TINYINT. Criterio de selección de candidatos.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'ExperienceInPosition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Experiencia en el Cargo:  1. En Cargos Similares.  2. En el Puesto  3. En el Sector  4. No Requiere', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'ExperienceInPosition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'ExperienceInPosition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modalidad de convocatoria: 1=Interna, 2=Externa, 3=Mixta. Tipo: TINYINT. Define si se recluta dentro de la organización, afuera o ambas.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'TypesOfCall';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipos de Convocatoria:  1. Interna.  2. Externa  3. Mixta', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'TypesOfCall';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'TypesOfCall';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de la vacante: 1=Creación de nuevo cargo, 2=Reemplazo de personal. Tipo: TINYINT. Causa del aumento o cambio de personal.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'ReasonForVacancy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de la Vacante:  1. Creación del Cargo  2. Reemplazo', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'ReasonForVacancy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'ReasonForVacancy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de la vacante: 1=Ocasional, 2=Indefinida, 3=Temporal, 4=Prestación de servicios. Tipo: TINYINT. Naturaleza y duración del contrato a ofrecer.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'VacancyType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Vacante:  1. Ocasional  2. Indefinido  3. Temporal  4. Prestación de Servicios', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'VacancyType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'VacancyType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del nivel de estudio requerido (FK a Payroll.StudyType). Tipo: INT. Referencia a educación: primaria, secundaria, técnico, profesional, especialización, etc.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'StudyTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de Tipo de Estudio', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'StudyTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'StudyTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la solicitud de personal padre (FK a StaffPick.PersonalRequisition). Tipo: INT. Agrupa detalles de una misma requisición.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'PersonalRequisitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de solicitud personal', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'PersonalRequisitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'PersonalRequisitionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementado del detalle de requisición. Tipo: INT IDENTITY. Clave primaria para auditoría y relaciones.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de cada requisición de personal: condiciones específicas de la vacante solicitada, tipo de contrato, número de empleados requeridos, fechas y estado del proceso de selección.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail';

CREATE TABLE [HumanTalent].[PersonalRequisition] (
    [Id]                        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [JobBondingTypeId]          TINYINT       NOT NULL,
    [DateRequisition]           DATETIME      NOT NULL,
    [PositionsId]               INT           NOT NULL,
    [Vacancies]                 INT           NOT NULL,
    [CallForStaff]              INT           NOT NULL,
    [PlazaPositionId]           INT           NULL,
    [ReasonRequisitionId]       INT           NOT NULL,
    [Description]               VARCHAR (800) NULL,
    [Observation]               VARCHAR (800) NULL,
    [WorkScheduleId]            INT           NOT NULL,
    [IncorporationDate]         DATETIME      NULL,
    [ResponsibleEmployeeId]     VARCHAR (20)  NULL,
    [ResponsibleAssignmentDate] DATETIME      NULL,
    [Source]                    INT           NOT NULL,
    [State]                     INT           NULL,
    [CreationUser]              VARCHAR (20)  NOT NULL,
    [CreationDate]              DATETIME      NOT NULL,
    [ModificationUser]          VARCHAR (20)  NULL,
    [ModificationDate]          DATETIME      NULL,
    CONSTRAINT [PK__Personal__3214EC071F79D352] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_JobBondingType] FOREIGN KEY ([JobBondingTypeId]) REFERENCES [Payroll].[JobBondingType] ([Id]),
    CONSTRAINT [fk_PlazaPositions_PersonalR] FOREIGN KEY ([PlazaPositionId]) REFERENCES [HumanTalent].[PlazaPosition] ([Id]),
    CONSTRAINT [fk_Position] FOREIGN KEY ([PositionsId]) REFERENCES [HumanTalent].[Positions] ([Id]),
    CONSTRAINT [fk_RequisitionReason] FOREIGN KEY ([ReasonRequisitionId]) REFERENCES [StaffPick].[RequisitionReason] ([Id]),
    CONSTRAINT [fk_WorkSchedule] FOREIGN KEY ([WorkScheduleId]) REFERENCES [StaffPick].[WorkSchedule] ([Id])
);




GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de requisición de personal (DATETIME, auditoría de cambios).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificición del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación del usuario que realizó la última modificación del registro (VARCHAR 20, PII-Ofuscado).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación inicial del registro de requisición de personal (DATETIME, auditoría).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación del usuario que creó el registro de requisición (VARCHAR 20, PII-Ofuscado).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual de la requisición (INT): 1=Solicitada (jefe área/talento humano), 2=Aprobada (jefe selección), 3=Rechazada (jefe selección). Indica etapa del flujo de aprobación.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado: 1-Solicitada, 2-Aprobada, 3-Rechazada   Solicitada=cuando la diligencie el jefe del area o los de talento humano  Aprobada=cuando la jefe de seleccion la apruebe  Rechazada=cuando la jefe de seleccion entre a rechazarla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen o canal de creación del registro (INT): 1=Portal Talento Humano, 2=Portal Autoservicios. Trazabilidad de entrada.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Source';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Origen del registro: 1 - portal Talento Humano. 2- portal Autoservicios  ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Source';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Source';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se asignó el empleado responsable de ejecutar la requisición de personal (DATETIME NULL).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ResponsibleAssignmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de asignación del responsable', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ResponsibleAssignmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ResponsibleAssignmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación o código del empleado responsable de gestionar y ejecutar la requisición de personal (VARCHAR 20, FK indirecto, PII-Ofuscado).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ResponsibleEmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del empliado responsable de la Requisición', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ResponsibleEmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ResponsibleEmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha prevista o real de incorporación del personal seleccionado en la requisición (DATETIME NULL).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'IncorporationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Incorporación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'IncorporationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'IncorporationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del horario o jornada laboral asociado a la requisición (INT, FK→StaffPick.WorkSchedule). Vincula con turnos, jornadas ordinarias/extraordinarias.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'WorkScheduleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id  de Horario de Trabajo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'WorkScheduleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'WorkScheduleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones, notas adicionales o comentarios sobre la solicitud de requisición de personal (VARCHAR 800 NULL).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación de la solicitud', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada de la requisición de personal, justificación o contexto de la solicitud (VARCHAR 800 NULL).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción de la solicitud', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del motivo o causa de la requisición (INT, FK→StaffPick.RequisitionReason). Ej: reemplazo, creación de plaza, proyecto.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ReasonRequisitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Motivo de la Requisición', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ReasonRequisitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ReasonRequisitionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la posición de plaza específica asociada a la requisición (INT NULL, FK→HumanTalent.PlazaPosition). Vincula con estructura presupuestal.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'PlazaPositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la posición de la plaza', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'PlazaPositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'PlazaPositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de convocatoria (INT): 1=Interna (empleados), 2=Externa (mercado laboral), 3=Mixta (ambas). Define estrategia de selección.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'CallForStaff';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Convocatoria: 1-Interna, 2-Externa,3-Mixta', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'CallForStaff';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'CallForStaff';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad o número de vacantes a solicitar en esta requisición de personal (INT).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Vacancies';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de vacantes a solicitar', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Vacancies';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Vacancies';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la posición laboral requerida (INT, FK→HumanTalent.Positions). Vincula con descripción, nivel, departamento.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'PositionsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la posición.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'PositionsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'PositionsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se registra o realiza la solicitud formal de requisición de personal (DATETIME).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'DateRequisition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se realiza la requisición', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'DateRequisition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'DateRequisition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del tipo de vinculación laboral (INT, FK→Payroll.JobBondingType). Ej: contratación directa, outsourcing, temporal.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'JobBondingTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Tipo de Vinculación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'JobBondingTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'JobBondingTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY) de cada registro de requisición de personal en el sistema.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Requisiciones de personal (solicitudes de contratación) generadas por las áreas de la organización, indicando el cargo requerido, número de vacantes, motivo de la solicitud, tipo de vinculación laboral y estado del proceso de selección.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition';

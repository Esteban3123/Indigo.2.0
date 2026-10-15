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
ALTER TABLE [HumanTalent].[PersonalRequisition] NOCHECK CONSTRAINT [fk_JobBondingType];


GO
ALTER TABLE [HumanTalent].[PersonalRequisition] NOCHECK CONSTRAINT [fk_PlazaPositions_PersonalR];


GO
ALTER TABLE [HumanTalent].[PersonalRequisition] NOCHECK CONSTRAINT [fk_Position];


GO
ALTER TABLE [HumanTalent].[PersonalRequisition] NOCHECK CONSTRAINT [fk_RequisitionReason];


GO
ALTER TABLE [HumanTalent].[PersonalRequisition] NOCHECK CONSTRAINT [fk_WorkSchedule];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificición del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado: 1-Solicitada, 2-Aprobada, 3-Rechazada   Solicitada=cuando la diligencie el jefe del area o los de talento humano  Aprobada=cuando la jefe de seleccion la apruebe  Rechazada=cuando la jefe de seleccion entre a rechazarla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Origen del registro: 1 - portal Talento Humano. 2- portal Autoservicios  ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Source';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de asignación del responsable', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ResponsibleAssignmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del empliado responsable de la Requisición', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ResponsibleEmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Incorporación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'IncorporationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id  de Horario de Trabajo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'WorkScheduleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observación de la solicitud', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción de la solicitud', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Motivo de la Requisición', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'ReasonRequisitionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la posición de la plaza', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'PlazaPositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Convocatoria: 1-Interna, 2-Externa,3-Mixta', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'CallForStaff';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de vacantes a solicitar', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Vacancies';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la posición.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'PositionsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se realiza la requisición', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'DateRequisition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Tipo de Vinculación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'JobBondingTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PersonalRequisition', @level2type = N'COLUMN', @level2name = N'Id';


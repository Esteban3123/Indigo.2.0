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
ALTER TABLE [StaffPick].[PersonalRequisitionDetail] NOCHECK CONSTRAINT [FK_PersonalRequisitionDetail_ContractType];


GO
ALTER TABLE [StaffPick].[PersonalRequisitionDetail] NOCHECK CONSTRAINT [FK_PersonalRequisitionDetail_PersonalRequisition];


GO
ALTER TABLE [StaffPick].[PersonalRequisitionDetail] NOCHECK CONSTRAINT [FK_PersonalRequisitionDetail_StudyType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solicitud de estado', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'StatusRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del usuario ', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'UserStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones ', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Duración del Contrato', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'ContractPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Empleados Requeridos', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'NumberEmployeesRequired';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicio Solicitada', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'ApplicationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Tipo de Contrato', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'ContractTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Experiencia en el Cargo:  1. En Cargos Similares.  2. En el Puesto  3. En el Sector  4. No Requiere', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'ExperienceInPosition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipos de Convocatoria:  1. Interna.  2. Externa  3. Mixta', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'TypesOfCall';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de la Vacante:  1. Creación del Cargo  2. Reemplazo', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'ReasonForVacancy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Vacante:  1. Ocasional  2. Indefinido  3. Temporal  4. Prestación de Servicios', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'VacancyType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de Tipo de Estudio', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'StudyTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de solicitud personal', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'PersonalRequisitionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'PersonalRequisitionDetail', @level2type = N'COLUMN', @level2name = N'Id';


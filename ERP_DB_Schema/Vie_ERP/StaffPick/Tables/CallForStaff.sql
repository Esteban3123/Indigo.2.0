CREATE TABLE [StaffPick].[CallForStaff] (
    [Id]                          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                        VARCHAR (20)  NULL,
    [PersonalRequisitionImport]   BIT           NOT NULL,
    [PersonalRequisitionId]       INT           NULL,
    [DateRequest]                 DATETIME      NOT NULL,
    [DateCallForStaff]            DATETIME      NOT NULL,
    [OrganizationChartPositionId] INT           NOT NULL,
    [PositionId]                  INT           NOT NULL,
    [BranchOfficeId]              INT           NOT NULL,
    [FunctionalUnitId]            INT           NOT NULL,
    [CostCenterId]                INT           NOT NULL,
    [PositionImmediateBossId]     INT           NULL,
    [StarTimeWeek]                TIME (7)      NOT NULL,
    [EndTimeWeek]                 TIME (7)      NOT NULL,
    [StarTimeWeekend]             TIME (7)      NOT NULL,
    [EndTimeWeekend]              TIME (7)      NOT NULL,
    [StudyTypeId]                 INT           NULL,
    [VacancyType]                 TINYINT       NOT NULL,
    [ReasonForVacancy]            TINYINT       NOT NULL,
    [TypesOfCall]                 TINYINT       NOT NULL,
    [ExperienceInPosition]        TINYINT       NOT NULL,
    [ContractTypeId]              INT           NOT NULL,
    [ApplicationDate]             DATE          NOT NULL,
    [NumberEmployeesRequired]     TINYINT       NOT NULL,
    [ContractPeriod]              VARCHAR (30)  NOT NULL,
    [Salary]                      NUMERIC (18)  NOT NULL,
    [Observations]                VARCHAR (500) NULL,
    [Status]                      TINYINT       NOT NULL,
    [CreationUser]                VARCHAR (20)  NOT NULL,
    [CreationDate]                DATETIME      NOT NULL,
    [ModificationUser]            VARCHAR (20)  NULL,
    [ModificationDate]            DATETIME      NULL,
    CONSTRAINT [PK_CallForStaff] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CallForStaff_BranchOffice] FOREIGN KEY ([BranchOfficeId]) REFERENCES [Payroll].[BranchOffice] ([Id]),
    CONSTRAINT [FK_CallForStaff_ContractType] FOREIGN KEY ([ContractTypeId]) REFERENCES [Payroll].[ContractType] ([Id]),
    CONSTRAINT [FK_CallForStaff_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CallForStaff_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_CallForStaff_OrganizationChartPosition] FOREIGN KEY ([OrganizationChartPositionId]) REFERENCES [HumanTalent].[OrganizationChartPosition] ([Id]),
    CONSTRAINT [FK_CallForStaff_OrganizationChartPosition1] FOREIGN KEY ([PositionImmediateBossId]) REFERENCES [HumanTalent].[OrganizationChartPosition] ([Id]),
    CONSTRAINT [FK_CallForStaff_PersonalRequisition] FOREIGN KEY ([PersonalRequisitionId]) REFERENCES [StaffPick].[PersonalRequisition] ([Id]),
    CONSTRAINT [FK_CallForStaff_Position] FOREIGN KEY ([PositionId]) REFERENCES [Payroll].[Position] ([Id]),
    CONSTRAINT [FK_CallForStaff_StudyType] FOREIGN KEY ([StudyTypeId]) REFERENCES [Payroll].[StudyType] ([Id])
);


GO
ALTER TABLE [StaffPick].[CallForStaff] NOCHECK CONSTRAINT [FK_CallForStaff_BranchOffice];


GO
ALTER TABLE [StaffPick].[CallForStaff] NOCHECK CONSTRAINT [FK_CallForStaff_ContractType];


GO
ALTER TABLE [StaffPick].[CallForStaff] NOCHECK CONSTRAINT [FK_CallForStaff_CostCenter];


GO
ALTER TABLE [StaffPick].[CallForStaff] NOCHECK CONSTRAINT [FK_CallForStaff_FunctionalUnit];


GO
ALTER TABLE [StaffPick].[CallForStaff] NOCHECK CONSTRAINT [FK_CallForStaff_OrganizationChartPosition];


GO
ALTER TABLE [StaffPick].[CallForStaff] NOCHECK CONSTRAINT [FK_CallForStaff_OrganizationChartPosition1];


GO
ALTER TABLE [StaffPick].[CallForStaff] NOCHECK CONSTRAINT [FK_CallForStaff_PersonalRequisition];


GO
ALTER TABLE [StaffPick].[CallForStaff] NOCHECK CONSTRAINT [FK_CallForStaff_Position];


GO
ALTER TABLE [StaffPick].[CallForStaff] NOCHECK CONSTRAINT [FK_CallForStaff_StudyType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Convocatoria:  1. Activa  2. Inactiva (Rechazada)  3. Finalizada', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Salario', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'Salary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Duración del Contrato', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ContractPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Empleados Requeridos', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'NumberEmployeesRequired';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicio Solicitada', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ApplicationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Tipo de Contrato', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ContractTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Experiencia en el Cargo:  1. En Cargos Similares.  2. En el Puesto  3. En el Sector  4. No Requiere', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ExperienceInPosition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipos de Convocatoria:  1. Interna.  2. Externa  3. Mixta', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'TypesOfCall';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de la Vacante:  1. Creación del Cargo  2. Reemplazo', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ReasonForVacancy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Vacante:  1. Ocasional  2. Indefinido  3. Temporal  4. Prestación de Servicios', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'VacancyType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Tipo de Estudio', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'StudyTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Horario Fin Fin de Semana', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'EndTimeWeekend';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Horario Inicio Fin de Semana', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'StarTimeWeekend';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Horario Fin Entre Semana', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'EndTimeWeek';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Horario Inicio Entre Semana', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'StarTimeWeek';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Posición Jefe Inmediato', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'PositionImmediateBossId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Centro de Costo', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Sucursal', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cargo', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'PositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Posición de Talento Humano', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'OrganizationChartPositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la Convocatoria', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'DateCallForStaff';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Requisición. Si la requisición no es importada, se debe colocar la misma que está en el campo DateCallForStaff', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'DateRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Requisición de Personal. Si el campo PersonalRequisitionImport es cero, debe ir vacía', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'PersonalRequisitionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si es una Requisición Importada. 1. Si, 0. No', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'PersonalRequisitionImport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la Convocatoria', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'Id';


CREATE TABLE [HumanTalent].[AnnualVacationSchedule] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]            INT           NOT NULL,
    [InitialContractNumber] INT           NOT NULL,
    [ContractId]            INT           NOT NULL,
    [VacationPeriodId]      INT           NOT NULL,
    [VacationInitialDate]   DATE          NOT NULL,
    [VacationEndDate]       DATE          NOT NULL,
    [IncorporationDate]     DATE          NOT NULL,
    [RequestDays]           TINYINT       NOT NULL,
    [Comments]              VARCHAR (500) NOT NULL,
    [Status]                TINYINT       NOT NULL,
    [CreationUser]          VARCHAR (20)  NOT NULL,
    [CreationDate]          DATETIME      NOT NULL,
    [ModificationUser]      VARCHAR (20)  NULL,
    [ModificationDate]      DATETIME      NULL,
    CONSTRAINT [PK_AnnualVacationSchedule] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnnualVacationSchedule_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_AnnualVacationSchedule_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_AnnualVacationSchedule_VacationPeriod] FOREIGN KEY ([VacationPeriodId]) REFERENCES [Payroll].[VacationPeriod] ([Id])
);


GO
ALTER TABLE [HumanTalent].[AnnualVacationSchedule] NOCHECK CONSTRAINT [FK_AnnualVacationSchedule_Contract];


GO
ALTER TABLE [HumanTalent].[AnnualVacationSchedule] NOCHECK CONSTRAINT [FK_AnnualVacationSchedule_Employee];


GO
ALTER TABLE [HumanTalent].[AnnualVacationSchedule] NOCHECK CONSTRAINT [FK_AnnualVacationSchedule_VacationPeriod];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado:  1 - Programada  2 - Cancelada', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comentarios', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias Solicitados', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'RequestDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Incoporación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'IncorporationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Fin de Vacaciones', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'VacationEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicio de Vacaciones', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'VacationInitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del período de vacaciones', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'VacationPeriodId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Contrato Inicial', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'InitialContractNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'AnnualVacationSchedule', @level2type = N'COLUMN', @level2name = N'Id';


CREATE TABLE [Payroll].[VacationPeriod] (
    [Id]                INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]        INT          NOT NULL,
    [ContractId]        INT          NOT NULL,
    [InitialDatePeriod] DATE         NOT NULL,
    [EndDatePeriod]     DATE         NOT NULL,
    [VacationDays]      TINYINT      NOT NULL,
    [PendingDays]       TINYINT      NOT NULL,
    [TakenDays]         TINYINT      NOT NULL,
    [CreationUser]      VARCHAR (20) NOT NULL,
    [CreationDate]      DATETIME     NOT NULL,
    [ModificationUser]  VARCHAR (20) NULL,
    [ModificationDate]  DATETIME     NULL,
    [TimeStamp]         ROWVERSION   NOT NULL,
    CONSTRAINT [PK_VacationPeriod__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_VacationPeriod_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_VacationPeriod_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id])
);


GO
ALTER TABLE [Payroll].[VacationPeriod] NOCHECK CONSTRAINT [FK_VacationPeriod_Contract];


GO
ALTER TABLE [Payroll].[VacationPeriod] NOCHECK CONSTRAINT [FK_VacationPeriod_Employee];


GO
CREATE NONCLUSTERED INDEX [IX_VacationPeriod__EmployeeId]
    ON [Payroll].[VacationPeriod]([EmployeeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_VacationPeriod__ContractId]
    ON [Payroll].[VacationPeriod]([ContractId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationPeriod', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationPeriod', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationPeriod', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationPeriod', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationPeriod', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias tomados por el empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationPeriod', @level2type = N'COLUMN', @level2name = N'TakenDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad de dias que se le debe al empleado, es decir la cantidad de dias que puede pedir', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationPeriod', @level2type = N'COLUMN', @level2name = N'PendingDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Días de vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationPeriod', @level2type = N'COLUMN', @level2name = N'VacationDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha final del Período', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationPeriod', @level2type = N'COLUMN', @level2name = N'EndDatePeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha inicial Período', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationPeriod', @level2type = N'COLUMN', @level2name = N'InitialDatePeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FK Contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationPeriod', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FK Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationPeriod', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla de vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'VacationPeriod', @level2type = N'COLUMN', @level2name = N'Id';


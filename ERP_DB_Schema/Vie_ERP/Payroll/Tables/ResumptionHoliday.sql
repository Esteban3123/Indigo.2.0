CREATE TABLE [Payroll].[ResumptionHoliday] (
    [Id]            INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]    INT          NOT NULL,
    [RequestedDays] INT          NOT NULL,
    [InitialDate]   DATE         NOT NULL,
    [EndDate]       DATE         NOT NULL,
    [EntryDate]     DATE         NOT NULL,
    [CreationUser]  VARCHAR (20) NOT NULL,
    [CreationDate]  DATETIME     NOT NULL,
    [TimeStamp]     ROWVERSION   NOT NULL,
    CONSTRAINT [PK_ResumptionHoliday] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ResumptionHoliday_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id])
);


GO
ALTER TABLE [Payroll].[ResumptionHoliday] NOCHECK CONSTRAINT [FK_ResumptionHoliday_Employee];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHoliday', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHoliday', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHoliday', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de ingreso del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHoliday', @level2type = N'COLUMN', @level2name = N'EntryDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dia de finalizacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHoliday', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dia de inicio ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHoliday', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Días solicitados', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHoliday', @level2type = N'COLUMN', @level2name = N'RequestedDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHoliday', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ResumptionHoliday', @level2type = N'COLUMN', @level2name = N'Id';


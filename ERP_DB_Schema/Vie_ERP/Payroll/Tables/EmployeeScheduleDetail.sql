CREATE TABLE [Payroll].[EmployeeScheduleDetail] (
    [Id]                    INT          IDENTITY (1, 1) NOT NULL,
    [IdEmployeeScheduleC]   INT          NOT NULL,
    [EmployeeId]            INT          NOT NULL,
    [InitialContractNumber] INT          NOT NULL,
    [ContractId]            INT          NOT NULL,
    [InitialDate]           DATE         NOT NULL,
    [InitialHourDate]       TIME (7)     NOT NULL,
    [EndDate]               DATE         NOT NULL,
    [EndHourDate]           TIME (7)     NOT NULL,
    [Hours]                 TINYINT      NOT NULL,
    [CreationUser]          VARCHAR (20) NOT NULL,
    [CreationDate]          DATETIME     NOT NULL,
    CONSTRAINT [FK_EmployeeScheduleDetail_EmployeeScheduleC] FOREIGN KEY ([IdEmployeeScheduleC]) REFERENCES [Payroll].[EmployeeScheduleC] ([Id])
);


GO
ALTER TABLE [Payroll].[EmployeeScheduleDetail] NOCHECK CONSTRAINT [FK_EmployeeScheduleDetail_EmployeeScheduleC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'EmployeeScheduleDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'EmployeeScheduleDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Horas laboradas (Diferencia entre la Hora de Salida y la Hora de Entrada)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'EmployeeScheduleDetail', @level2type = N'COLUMN', @level2name = N'Hours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora Fin Turno', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'EmployeeScheduleDetail', @level2type = N'COLUMN', @level2name = N'EndHourDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Fin Turno', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'EmployeeScheduleDetail', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora Inicio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'EmployeeScheduleDetail', @level2type = N'COLUMN', @level2name = N'InitialHourDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'EmployeeScheduleDetail', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Contract', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'EmployeeScheduleDetail', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de contrato inicial', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'EmployeeScheduleDetail', @level2type = N'COLUMN', @level2name = N'InitialContractNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Employee', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'EmployeeScheduleDetail', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Horario de empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'EmployeeScheduleDetail', @level2type = N'COLUMN', @level2name = N'IdEmployeeScheduleC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'EmployeeScheduleDetail', @level2type = N'COLUMN', @level2name = N'Id';


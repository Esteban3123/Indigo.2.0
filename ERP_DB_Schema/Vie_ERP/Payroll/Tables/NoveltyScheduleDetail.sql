CREATE TABLE [Payroll].[NoveltyScheduleDetail] (
    [Id]                       INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GroupId]                  INT          NOT NULL,
    [EmployeeId]               INT          NOT NULL,
    [CompanyId]                INT          NOT NULL,
    [BranchOfficeId]           INT          NOT NULL,
    [FunctionalUnitId]         INT          NOT NULL,
    [CostCenterId]             INT          NOT NULL,
    [ScheduleFunctionalUnitId] INT          NOT NULL,
    [PayrollLiquidationNumber] VARCHAR (50) NULL,
    [Letter]                   VARCHAR (2)  NOT NULL,
    [DateDetail]               DATE         NOT NULL,
    [Period]                   CHAR (7)     NOT NULL,
    [TotalNumberHours]         INT          NOT NULL,
    [ScheduleTemplateId]       INT          NULL,
    [Status]                   INT          NOT NULL,
    [State]                    BIT          NOT NULL,
    [PayrollDate]              DATE         NULL,
    CONSTRAINT [PK_NoveltyScheduleDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NoveltyScheduleDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_NoveltyScheduleDetail_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_NoveltyScheduleDetail_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_NoveltyScheduleDetail_FunctionalUnit1] FOREIGN KEY ([ScheduleFunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id])
);


GO
ALTER TABLE [Payroll].[NoveltyScheduleDetail] NOCHECK CONSTRAINT [FK_NoveltyScheduleDetail_CostCenter];


GO
ALTER TABLE [Payroll].[NoveltyScheduleDetail] NOCHECK CONSTRAINT [FK_NoveltyScheduleDetail_Employee];


GO
ALTER TABLE [Payroll].[NoveltyScheduleDetail] NOCHECK CONSTRAINT [FK_NoveltyScheduleDetail_FunctionalUnit];


GO
ALTER TABLE [Payroll].[NoveltyScheduleDetail] NOCHECK CONSTRAINT [FK_NoveltyScheduleDetail_FunctionalUnit1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Nómina en que se paga', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'PayrollDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1-activo 0-inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del item de la plantilla con la que se liquido el registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'ScheduleTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero total de horas del dia correspondiente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'TotalNumberHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Periodo al cual pertenece el detalle', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'Period';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha donde ocurrio el detalle', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'DateDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Letra utilizada para identificar la plantilla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'Letter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de nomina en donde se liquido el registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'PayrollLiquidationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad funcional donde realizo el turno el empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'ScheduleFunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad funcional (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la sucursal (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la empresa', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'CompanyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'Id';


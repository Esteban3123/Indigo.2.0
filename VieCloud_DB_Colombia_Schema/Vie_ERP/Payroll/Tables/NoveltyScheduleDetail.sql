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



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de nómina en que se paga al empleado. Tipo DATE, fecha de liquidación y desembolso salarial.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'PayrollDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Nómina en que se paga', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'PayrollDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'PayrollDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado binario del registro: 1=activo, 0=inactivo. Bit, controla visibilidad y procesamiento.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado 1-activo 0-inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro de novedad de horario. INT, código de estado del ciclo de vida (pendiente, liquidado, etc.).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la plantilla horaria usada para liquidar el registro. FK a ScheduleTemplate, INT.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'ScheduleTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del item de la plantilla con la que se liquido el registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'ScheduleTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'ScheduleTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de horas laboradas en el día correspondiente. INT, horas del turno del empleado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'TotalNumberHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero total de horas del dia correspondiente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'TotalNumberHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'TotalNumberHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Período al cual pertenece el detalle. CHAR(7), formato YYYYMM o similar, mes de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'Period';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Periodo al cual pertenece el detalle', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'Period';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'Period';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que ocurrió el evento de novedad horaria. DATE, día específico del turno o novedad.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'DateDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha donde ocurrio el detalle', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'DateDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'DateDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Letra identificadora de la plantilla horaria utilizada. VARCHAR(2), código de plantilla abreviado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'Letter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Letra utilizada para identificar la plantilla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'Letter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'Letter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de nómina donde se liquidó el registro. VARCHAR(50), identificador de la liquidación procesada.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'PayrollLiquidationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de nomina en donde se liquido el registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'PayrollLiquidationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'PayrollLiquidationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad funcional donde realizó el turno el empleado. FK a FunctionalUnit, INT, área asignada.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'ScheduleFunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad funcional donde realizo el turno el empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'ScheduleFunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'ScheduleFunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo asociado. FK a CostCenter, INT, para asignación contable.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad funcional del empleado. FK a FunctionalUnit, INT, área administrativa.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad funcional (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la sucursal o sede. INT, FK, ubicación física donde labora.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la sucursal (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la empresa. INT, FK, organización empleadora.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'CompanyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la empresa', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'CompanyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'CompanyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del empleado. FK a Employee, INT, profesional de la salud o personal de plantilla.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de empleados. INT, FK, agrupación administrativa de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico del registro. INT IDENTITY, clave primaria de la tabla NoveltyScheduleDetail.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de novedades de turno o programación de horario por empleado en nómina. Registra, para cada día del período, el turno asignado, las horas programadas, la unidad funcional, el centro de costos y el estado de liquidación de cada novedad de horario laboral.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyScheduleDetail';

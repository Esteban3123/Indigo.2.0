CREATE TABLE [Payroll].[ScheduleDetail] (
    [Id]                       INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GroupId]                  INT            NOT NULL,
    [EmployeeId]               INT            NOT NULL,
    [ContractId]               INT            NOT NULL,
    [CompanyId]                INT            NOT NULL,
    [BranchOfficeId]           INT            NOT NULL,
    [FunctionalUnitId]         INT            NOT NULL,
    [CenterCostId]             INT            NOT NULL,
    [ScheduleFunctionalUnitId] INT            NOT NULL,
    [PayrollLiquidationNumber] VARCHAR (50)   NULL,
    [Letter]                   VARCHAR (2)    NOT NULL,
    [DateDetail]               DATE           NOT NULL,
    [TotalNumberHours]         NUMERIC (5, 2) NOT NULL,
    [ScheduleTemplateId]       INT            NULL,
    [Status]                   INT            NOT NULL,
    [State]                    BIT            NOT NULL,
    [ScheduleId]               INT            NULL,
    CONSTRAINT [PK_HourCalendar] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HourCalendar_CenterCost] FOREIGN KEY ([CenterCostId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_HourCalendar_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_HourCalendar_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_ScheduleDetail_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_ScheduleDetail_FunctionalUnit] FOREIGN KEY ([ScheduleFunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_ScheduleDetail_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id]),
    CONSTRAINT [FK_ScheduleDetail_ScheduleTemplate] FOREIGN KEY ([ScheduleTemplateId]) REFERENCES [Payroll].[ScheduleTemplate] ([Id]),
    CONSTRAINT [FK_ScheduleDetail_Schedule] FOREIGN KEY ([ScheduleId]) REFERENCES [Payroll].[Schedule] ([Id])
);


GO
ALTER TABLE [Payroll].[ScheduleDetail] NOCHECK CONSTRAINT [FK_ScheduleDetail_ScheduleTemplate];


GO
-- Sigue la convencion NOCHECK de las demas FK de esta tabla: evita el scan de validacion sobre la tabla completa al desplegar.
ALTER TABLE [Payroll].[ScheduleDetail] NOCHECK CONSTRAINT [FK_ScheduleDetail_Schedule];




GO



GO



GO



GO



GO



GO



GO
ALTER TABLE [Payroll].[ScheduleDetail] NOCHECK CONSTRAINT [FK_ScheduleDetail_ScheduleTemplate];




GO



GO



GO



GO



GO



GO



GO
ALTER TABLE [Payroll].[ScheduleDetail] NOCHECK CONSTRAINT [FK_ScheduleDetail_ScheduleTemplate];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de registro activo (1) o inactivo (0); estado lógico de disponibilidad del detalle de turno', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado 1-activo 0-inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado operativo del registro (código numérico); indicador de validación o aprobación del detalle', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la plantilla de horario (FK); referencia al patrón de turno usado en liquidación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'ScheduleTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del item de la plantilla con la que se liquido el registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'ScheduleTemplateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'ScheduleTemplateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad total de horas laboradas en el día correspondiente; horas liquidables del turno (NUMERIC 5,2)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'TotalNumberHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero total de horas del dia correspondiente', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'TotalNumberHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'TotalNumberHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de ocurrencia del detalle de turno; día en que se registró la asistencia o turno del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'DateDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha donde ocurrio el detalle', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'DateDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'DateDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Letra identificadora de la plantilla de horario (máx 2 caracteres); código alfabético del patrón de turno', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'Letter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Letra utilizada para identificar la plantilla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'Letter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'Letter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de nómina donde se liquidó el registro; referencia a proceso de pago (VARCHAR 50, nullable)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'PayrollLiquidationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de nomina en donde se liquido el registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'PayrollLiquidationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'PayrollLiquidationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad funcional donde el empleado realizó el turno (FK); centro de atención o área de trabajo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'ScheduleFunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad funcional donde realizo el turno el empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'ScheduleFunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'ScheduleFunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo asignado (FK); referencia contable y presupuestaria del turno', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'CenterCostId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'CenterCostId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'CenterCostId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad funcional del empleado (FK); área organizacional de adscripción laboral', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad funcional (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la sucursal; ubicación física o sede donde se prestó el servicio', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la sucursal (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la empresa empleadora; entidad legal responsable de la nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'CompanyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la empresa', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'CompanyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'CompanyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del contrato laboral del empleado; referencia a términos y condiciones de empleo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del contrato', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del empleado (FK); referencia a profesional de la salud o trabajador en nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de turnos o lote (FK); agrupación para procesamiento conjunto de horarios', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria numérica autoincrementada; identificador único del detalle de turno o asistencia diaria', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
CREATE NONCLUSTERED INDEX [IX_ScheduleDetail_EmployeeId_DateDetail_TotalNumberHours]
    ON [Payroll].[ScheduleDetail]([EmployeeId] ASC, [DateDetail] ASC, [TotalNumberHours] ASC)
    INCLUDE([Id]) WITH (FILLFACTOR = 90, PAD_INDEX = ON);


GO
CREATE NONCLUSTERED INDEX [IX_ScheduleDetail_EmployeeId_DateDetail]
    ON [Payroll].[ScheduleDetail]([EmployeeId] ASC, [DateDetail] ASC)
    INCLUDE ([ScheduleFunctionalUnitId], [FunctionalUnitId], [TotalNumberHours], [Letter], [ScheduleTemplateId]);


GO
CREATE NONCLUSTERED INDEX [IX_ScheduleDetail__ScheduleId]
    ON [Payroll].[ScheduleDetail]([ScheduleId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK al cuadro (Schedule) dueño de este detalle. Normalizacion Fase 3.1 (expand-contract): reemplaza gradualmente la relacion inversa via las 31 columnas D01..D31 de Schedule. Nullable durante la transicion.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail', @level2type = N'COLUMN', @level2name = N'ScheduleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle diario del turno o jornada programada para cada empleado, indicando a qué grupo de horario pertenece, en qué empresa, sucursal, unidad funcional y centro de costo trabaja ese día, cuántas horas tiene asignadas y si el turno está activo o liquidado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ScheduleDetail';

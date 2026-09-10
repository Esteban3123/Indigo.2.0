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



GO



GO



GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo (DATETIME) de la última modificación del registro, puede ser nulo si no ha sido editado', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del último usuario que modificó la convocatoria, tipo VARCHAR(20), puede ser nulo', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo (DATETIME) de cuándo se creó el registro de convocatoria', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario que originó la convocatoria de personal, tipo VARCHAR(20)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la convocatoria: 1=Activa (abierta), 2=Inactiva/Rechazada (no prosigue), 3=Finalizada (selección completada)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Convocatoria:  1. Activa  2. Inactiva (Rechazada)  3. Finalizada', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de notas complementarias sobre la convocatoria, requisitos adicionales o especificaciones', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salario bruto mensual ofrecido para el cargo, tipo NUMERIC(18)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'Salary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Salario', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'Salary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'Salary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración/vigencia del contrato (ej: 12 meses, 6 meses, indefinido), VARCHAR(30)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ContractPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duración del Contrato', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ContractPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ContractPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de empleados/profesionales requeridos para cubrir la vacante', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'NumberEmployeesRequired';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Empleados Requeridos', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'NumberEmployeesRequired';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'NumberEmployeesRequired';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio solicitada para que comience a laborar el personal seleccionado', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ApplicationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicio Solicitada', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ApplicationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ApplicationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de contrato (ej: Indefinido, Fijo, Temporal), referencia FK a Payroll.ContractType', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ContractTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Tipo de Contrato', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ContractTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ContractTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Experiencia requerida: 1=Cargos similares, 2=En el puesto específico, 3=Sector/industria, 4=No requiere', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ExperienceInPosition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Experiencia en el Cargo:  1. En Cargos Similares.  2. En el Puesto  3. En el Sector  4. No Requiere', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ExperienceInPosition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ExperienceInPosition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de convocatoria: 1=Interna (personal actual), 2=Externa (público general), 3=Mixta (ambas)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'TypesOfCall';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipos de Convocatoria:  1. Interna.  2. Externa  3. Mixta', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'TypesOfCall';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'TypesOfCall';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de la vacante: 1=Creación de nuevo cargo, 2=Reemplazo de personal', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ReasonForVacancy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de la Vacante:  1. Creación del Cargo  2. Reemplazo', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ReasonForVacancy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'ReasonForVacancy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de vacante: 1=Ocasional, 2=Indefinido, 3=Temporal, 4=Prestación de Servicios', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'VacancyType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Vacante:  1. Ocasional  2. Indefinido  3. Temporal  4. Prestación de Servicios', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'VacancyType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'VacancyType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del nivel/tipo de estudio requerido (ej: Primaria, Bachillerato, Pregrado, Posgrado), referencia FK a Payroll.StudyType', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'StudyTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Tipo de Estudio', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'StudyTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'StudyTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de finalización de la jornada laboral en fin de semana/festivos, tipo TIME', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'EndTimeWeekend';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Horario Fin Fin de Semana', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'EndTimeWeekend';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'EndTimeWeekend';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de inicio de la jornada laboral en fin de semana/festivos, tipo TIME', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'StarTimeWeekend';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Horario Inicio Fin de Semana', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'StarTimeWeekend';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'StarTimeWeekend';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de finalización de la jornada laboral entre semana (lunes a viernes), tipo TIME', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'EndTimeWeek';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Horario Fin Entre Semana', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'EndTimeWeek';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'EndTimeWeek';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de inicio de la jornada laboral entre semana (lunes a viernes), tipo TIME', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'StarTimeWeek';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Horario Inicio Entre Semana', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'StarTimeWeek';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'StarTimeWeek';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la posición del jefe inmediato del cargo vacante, referencia FK a HumanTalent.OrganizationChartPosition', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'PositionImmediateBossId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Posición Jefe Inmediato', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'PositionImmediateBossId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'PositionImmediateBossId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo que cubre la contratación, referencia FK a Payroll.CostCenter', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Centro de Costo', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad funcional/departamento solicitante, referencia FK a Payroll.FunctionalUnit', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la sucursal/sede donde se requiere personal, referencia FK a Payroll.BranchOffice', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Sucursal', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del cargo a cubrir, referencia FK a Payroll.Position', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'PositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cargo', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'PositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'PositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la posición en el organigrama de talento humano, referencia FK a HumanTalent.OrganizationChartPosition', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'OrganizationChartPositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Posición de Talento Humano', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'OrganizationChartPositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'OrganizationChartPositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha oficial de publicación/apertura de la convocatoria de personal ante candidatos internos o externos', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'DateCallForStaff';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la Convocatoria', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'DateCallForStaff';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'DateCallForStaff';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se originó la requisición de personal; si no es importada, coincide con DateCallForStaff', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'DateRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Requisición. Si la requisición no es importada, se debe colocar la misma que está en el campo DateCallForStaff', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'DateRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'DateRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la requisición de personal asociada; solo se completa si PersonalRequisitionImport=1, referencia FK a StaffPick.PersonalRequisition', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'PersonalRequisitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Requisición de Personal. Si el campo PersonalRequisitionImport es cero, debe ir vacía', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'PersonalRequisitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'PersonalRequisitionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si la convocatoria proviene de requisición importada: 1=Sí (importada), 0=No (creada manualmente)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'PersonalRequisitionImport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si es una Requisición Importada. 1. Si, 0. No', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'PersonalRequisitionImport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'PersonalRequisitionImport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de la convocatoria de personal, referencia externa para búsqueda y trazabilidad', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la Convocatoria', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de la convocatoria de personal (clave primaria)', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Convocatorias de selección de personal: registra cada proceso de llamado o apertura de vacante para cubrir un cargo, incluyendo el cargo solicitado, la unidad organizacional, horarios, tipo de contrato, salario ofrecido, número de vacantes y el estado del proceso de reclutamiento.', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'StaffPick', @level1type = N'TABLE', @level1name = N'CallForStaff';

CREATE TABLE [Payroll].[Position] (
    [Id]                         INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                       VARCHAR (20) NOT NULL,
    [Name]                       VARCHAR (80) NOT NULL,
    [PositionLevelId]            INT          NOT NULL,
    [MinHourAmount]              INT          NOT NULL,
    [MaxHourAmount]              INT          NOT NULL,
    [MinBasicSalary]             INT          NOT NULL,
    [MaxBasicSalary]             INT          NOT NULL,
    [ProfessionalRiskLevelId]    INT          NOT NULL,
    [Simulation]                 BIT          NOT NULL,
    [HandlesTurnsChart]          BIT          NOT NULL,
    [NightlyChargeAuthorization] BIT          NOT NULL,
    [RepresentationCost]         BIT          CONSTRAINT [DF_Position_RepresentationCost] DEFAULT ((0)) NOT NULL,
    [ResolutionNumber]           VARCHAR (50) NULL,
    [BeforeResolutionNumber]     VARCHAR (50) NULL,
    [State]                      BIT          NOT NULL,
    [CreationUser]               VARCHAR (20) NOT NULL,
    [CreationDate]               DATETIME     NOT NULL,
    [ModificationUser]           VARCHAR (20) NULL,
    [ModificationDate]           DATETIME     NULL,
    [TimeStamp]                  ROWVERSION   NOT NULL,
    [INSCode]                    VARCHAR (20) NULL,
    [CCSSCode]                   VARCHAR (20) NULL,
    CONSTRAINT [PK_PositionStructure] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Position_PositionLevel] FOREIGN KEY ([PositionLevelId]) REFERENCES [Payroll].[PositionLevel] ([Id]),
    CONSTRAINT [FK_Position_ProfessionalRisk] FOREIGN KEY ([ProfessionalRiskLevelId]) REFERENCES [Payroll].[ProfessionalRisk] ([Id])
);


GO
ALTER TABLE [Payroll].[Position] NOCHECK CONSTRAINT [FK_Position_PositionLevel];




GO
ALTER TABLE [Payroll].[Position] NOCHECK CONSTRAINT [FK_Position_PositionLevel];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CCSS (VARCHAR 20), identificador de seguridad social ante la Caja Costarricense de Seguro Social para cumplimiento normativo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'CCSSCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Información de seguridad social, Código CCSS', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'CCSSCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'CCSSCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código INS (VARCHAR 20), identificador de seguridad social ante el Instituto Nacional de Salud para clasificación de profesionales de la salud.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'INSCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Información de seguridad social, Código INS', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'INSCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'INSCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP, SQL Server auto-generado) que registra automáticamente el instante exacto de creación o modificación del registro, garantizando control concurrente.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del registro de cargo, trazabilidad de cambios.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que realizó la última modificación del registro de cargo, auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro de la definición de cargo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que creó el registro de la definición de cargo en el sistema.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (BIT: 1=Activo, 0=Inactivo) que indica disponibilidad del cargo para asignar a nuevos empleados en nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado 1 Activo, 0 Inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de resolución (VARCHAR 50) anterior, antecedente normativo previo que regulaba el cargo antes de cambios recientes.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'BeforeResolutionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Resolución Anterior', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'BeforeResolutionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'BeforeResolutionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de resolución (VARCHAR 50) vigente que regula o autoriza la creación/modificación actual del cargo en el sistema.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Resolución Actual', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT: 1=Sí, 0=No) que especifica si el cargo incluye gastos de representación o asignación de representación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'RepresentationCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el cargo tiene gastos de representacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'RepresentationCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'RepresentationCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT: 1=Autorizado, 0=No autorizado) que especifica si el cargo autoriza recargo nocturno o trabajo fuera de horario.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'NightlyChargeAuthorization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autorizacion Recargo Nocturno 1 - si - 0 - no', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'NightlyChargeAuthorization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'NightlyChargeAuthorization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT: 1=Sí, 0=No) que señala si el cargo gestiona cuadro de turnos o horarios especiales.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'HandlesTurnsChart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si Maneja Cuadro de Turno: 1 Si -  0 No', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'HandlesTurnsChart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'HandlesTurnsChart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT: 1=Sí, 0=No) que activa distribución de sueldo por simulación, contabilizando por Centro de Costo y Sub-CC independientemente de horas.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'Simulation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Distribucion de Sueldo por Simulacion  establece si la contabilizacion se realiza por CC y Sub CC asi los conceptos no tengan variable de N° Horas: 1 Si - 0 No', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'Simulation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'Simulation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del nivel de riesgo profesional del cargo. Referencia a [Payroll].[ProfessionalRisk] para clasificación de riesgos ocupacionales.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'ProfessionalRiskLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Nivel de Riesgos profesionales (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'ProfessionalRiskLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'ProfessionalRiskLevelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salario base máximo (INT) permitido para un empleado en este cargo, límite superior de remuneración.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'MaxBasicSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Salario maximo que se puede establecer como Basico del Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'MaxBasicSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'MaxBasicSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salario base mínimo (INT) que se puede asignar a un empleado en este cargo, límite inferior de remuneración.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'MinBasicSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Salario minimo que se puede establecer como Basico para el Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'MinBasicSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'MinBasicSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad máxima de horas (INT) permitidas para un empleado en esta posición por período de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'MaxHourAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad maxima de horas que puede trabajar el empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'MaxHourAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'MaxHourAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad mínima de horas (INT) que debe trabajar un empleado en esta posición por período de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'MinHourAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad minima de horas que puede trabajar el empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'MinHourAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'MinHourAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del nivel jerárquico del cargo. Referencia a [Payroll].[PositionLevel] para clasificar jerarquía organizacional.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'PositionLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Nivel de Cargo  (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'PositionLevelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'PositionLevelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del cargo (VARCHAR 80), título de la posición laboral (ej: Médico, Enfermera, Administrativo, Profesional de la Salud).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de cargo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del cargo (VARCHAR 20), identificador alphanumeric que referencia la posición laboral en reportes, contratos y sistemas integrados.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del cargo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) de la estructura/definición de cargo en el sistema de nómina. Clave primaria que indexa cada posición laboral.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la estructura de cargo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cargos o puestos de trabajo definidos en la nómina. Contiene los parámetros laborales de cada cargo: rango salarial, horas permitidas, nivel de riesgo profesional y configuraciones de turnos, recargos nocturnos y gastos de representación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position';

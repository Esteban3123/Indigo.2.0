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
ALTER TABLE [Payroll].[Position] NOCHECK CONSTRAINT [FK_Position_ProfessionalRisk];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Información de seguridad social, Código CCSS', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'CCSSCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Información de seguridad social, Código INS', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'INSCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1 Activo, 0 Inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Resolución Anterior', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'BeforeResolutionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Resolución Actual', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el cargo tiene gastos de representacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'RepresentationCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autorizacion Recargo Nocturno 1 - si - 0 - no', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'NightlyChargeAuthorization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si Maneja Cuadro de Turno: 1 Si -  0 No', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'HandlesTurnsChart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Distribucion de Sueldo por Simulacion  establece si la contabilizacion se realiza por CC y Sub CC asi los conceptos no tengan variable de N° Horas: 1 Si - 0 No', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'Simulation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Nivel de Riesgos profesionales (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'ProfessionalRiskLevelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Salario maximo que se puede establecer como Basico del Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'MaxBasicSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Salario minimo que se puede establecer como Basico para el Empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'MinBasicSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad maxima de horas que puede trabajar el empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'MaxHourAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad minima de horas que puede trabajar el empleado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'MinHourAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Nivel de Cargo  (FK)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'PositionLevelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de cargo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del cargo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la estructura de cargo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'Id';


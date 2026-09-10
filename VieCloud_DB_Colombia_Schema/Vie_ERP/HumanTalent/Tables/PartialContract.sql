CREATE TABLE [HumanTalent].[PartialContract] (
    [Id]                           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RowType]                      TINYINT       NOT NULL,
    [InitialContractNumber]        INT           NOT NULL,
    [ResolutionNumber]             VARCHAR (50)  NULL,
    [ResolutionDate]               DATE          NULL,
    [PosesionDate]                 DATE          NULL,
    [CertificateOfficeNumber]      VARCHAR (50)  NULL,
    [EmployeeId]                   INT           NOT NULL,
    [PositionId]                   INT           NOT NULL,
    [ContractTypeId]               INT           NOT NULL,
    [JobBondingDate]               DATE          NOT NULL,
    [ContractInitialDate]          DATE          NOT NULL,
    [ContractEndingDate]           DATE          NOT NULL,
    [BasicSalary]                  NUMERIC (18)  NOT NULL,
    [Status]                       TINYINT       NOT NULL,
    [RetirementReasonId]           TINYINT       NULL,
    [RetirementDate]               DATE          NULL,
    [PaymentPeriod]                TINYINT       NOT NULL,
    [PaymentType]                  TINYINT       NOT NULL,
    [TrialPeriod]                  BIT           NOT NULL,
    [TrialPeriodTime]              INT           NULL,
    [TrialPeriodSalaryPercentage]  INT           NULL,
    [ContractCreationDate]         DATETIME      NOT NULL,
    [CreationUserId]               VARCHAR (20)  NOT NULL,
    [ModificationDate]             DATETIME      NOT NULL,
    [TypeOfPensionContribution]    TINYINT       NOT NULL,
    [Valid]                        BIT           NOT NULL,
    [Notes]                        VARCHAR (255) NULL,
    [GroupId]                      INT           NOT NULL,
    [BankId]                       INT           NOT NULL,
    [BankAccountNumber]            VARCHAR (20)  NOT NULL,
    [BankAccountType]              INT           NOT NULL,
    [LiquidationPayroll]           TINYINT       NOT NULL,
    [ContractModificationReasonId] INT           NULL,
    [BaseIncome]                   DECIMAL (18)  NULL,
    [IncomeDailyBase]              INT           NULL,
    [HoursDaily]                   TINYINT       NOT NULL,
    [LastLiquidationDate]          DATE          NULL,
    [ModificationUserId]           VARCHAR (20)  NOT NULL,
    [LastModificationDate]         DATE          NOT NULL,
    [Contingency]                  TINYINT       CONSTRAINT [DF_Contract_Contingency] DEFAULT ((0)) NULL,
    [OrganizationChartPositionId]  INT           NULL,
    [PlazaPositionId]              INT           NOT NULL,
    [OrganizationalUnitId]         INT           NOT NULL,
    [WorkPlaceId]                  INT           NULL,
    [PositionsId]                  INT           NOT NULL,
    [TrialPeriodDate]              DATE          NULL,
    [ProfesionalRiskId]            INT           NULL,
    [ContractId]                   INT           NULL,
    [State]                        TINYINT       NULL,
    [HiringDocumentsId]            INT           NOT NULL,
    CONSTRAINT [PK_Contract__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Contract_Bank] FOREIGN KEY ([BankId]) REFERENCES [Payroll].[Bank] ([Id]),
    CONSTRAINT [FK_Contract_ContractModificationReason] FOREIGN KEY ([ContractModificationReasonId]) REFERENCES [Payroll].[ContractModificationReason] ([Id]),
    CONSTRAINT [FK_Contract_ContractType] FOREIGN KEY ([ContractTypeId]) REFERENCES [Payroll].[ContractType] ([Id]),
    CONSTRAINT [FK_Contract_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_Contract_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id]),
    CONSTRAINT [FK_Contract_OrganizationChartPosition] FOREIGN KEY ([OrganizationChartPositionId]) REFERENCES [HumanTalent].[OrganizationChartPosition] ([Id]),
    CONSTRAINT [FK_Contract_Position] FOREIGN KEY ([PositionId]) REFERENCES [Payroll].[Position] ([Id]),
    CONSTRAINT [FK_Contract_RetirementReason] FOREIGN KEY ([RetirementReasonId]) REFERENCES [Payroll].[RetirementReason] ([Id]),
    CONSTRAINT [FK_PartialContract_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_PartialContract_HiringDocuments] FOREIGN KEY ([HiringDocumentsId]) REFERENCES [HumanTalent].[HiringDocuments] ([Id]),
    CONSTRAINT [FK_PartialContract_OrganizationalUnit] FOREIGN KEY ([OrganizationalUnitId]) REFERENCES [HumanTalent].[OrganizationalUnit] ([Id]),
    CONSTRAINT [FK_PartialContract_PlazaPosition] FOREIGN KEY ([PlazaPositionId]) REFERENCES [HumanTalent].[PlazaPosition] ([Id]),
    CONSTRAINT [FK_PartialContract_Positions] FOREIGN KEY ([PositionsId]) REFERENCES [HumanTalent].[Positions] ([Id]),
    CONSTRAINT [FK_PartialContract_ProfessionalRisk] FOREIGN KEY ([ProfesionalRiskId]) REFERENCES [Payroll].[ProfessionalRisk] ([Id]),
    CONSTRAINT [FK_PartialContract_WorkPlace] FOREIGN KEY ([WorkPlaceId]) REFERENCES [HumanTalent].[WorkPlace] ([Id])
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



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de documentos de contratación, referencias a HiringDocuments; vinculación, antecedentes, certificados de empleabilidad (INT, FK)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'HiringDocumentsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identidicacion de los documentos de contratación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'HiringDocumentsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'HiringDocumentsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del contrato parcial: 1=Parcial, 2=Contrato realizado (sincronizado a Payroll), 3=Anulado; ciclo de vigencia laboral', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del contrato: 1- parcial, 2- Contrato realizado (se crea tambien en payroll), 3- anulado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del contrato en Payroll, se genera al confirmar ingreso del empleado; referencia FK a Contract (INT, nullable)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de l contrato de payroll, se almacena cuando se confirma el ingreso del empleado.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del nivel de riesgo profesional asignado al empleado en Talento Humano; ARL, cobertura sanitaria (INT, FK, nullable)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ProfesionalRiskId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del nivel de riesgo profesional del human talent.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ProfesionalRiskId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ProfesionalRiskId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de finalización del período de prueba del empleado; vencimiento, término de evaluación inicial (DATE, nullable)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'TrialPeriodDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha fin del periodo de prueba al empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'TrialPeriodDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'TrialPeriodDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la posición en Talento Humano; cargo, puesto, rol organizacional (INT, FK, obligatorio)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'PositionsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la posicion de talento humano.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'PositionsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'PositionsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del lugar de trabajo en Talento Humano; sede, oficina, centro laboral, ubicación física (INT, FK, nullable)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'WorkPlaceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del lugar de trabajo, de humantaTalent', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'WorkPlaceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'WorkPlaceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad organizativa en Talento Humano; departamento, división, área funcional (INT, FK, obligatorio)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'OrganizationalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad organizativa de talento humano.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'OrganizationalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'OrganizationalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la plaza de Talento Humano; puesto presupuestado, vacante asignada (INT, FK, obligatorio)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'PlazaPositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la plaza de talento humano', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'PlazaPositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'PlazaPositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la posición en el organigrama; se completa solo cuando hay integración con Talento Humano; estructura jerárquica (INT, FK, nullable)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'OrganizationChartPositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la posición del Organigrama. Únicamente se llena, cuando el sistema está integrado con Talento Humano', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'OrganizationChartPositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'OrganizationChartPositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de contrato de contingencia: 0=Ninguna, 1=Licencias, 2=Vacaciones, 3=Incapacidades; cobertura temporal, reemplazo (TINYINT)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'Contingency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contrato de Contigencia:  0 - Ninguna  1 - Licencias  2 - Vacaciones  3 - Incapacidades', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'Contingency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'Contingency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la última modificación del contrato; actualización más reciente, cambios realizados (DATE)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'LastModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificacion', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'LastModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'LastModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario que realizó la última modificación; usuario editor, auditoría de cambios (VARCHAR(20))', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ModificationUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del usuario que modifico', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ModificationUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ModificationUserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la última liquidación de nómina del empleado; último pago procesado, cierre de período (DATE, nullable)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'LastLiquidationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ultima fecha de liquidacion de nomina del empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'LastLiquidationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'LastLiquidationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de horas laborales diarias; jornada laboral, carga horaria, dedicación (TINYINT)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'HoursDaily';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de horas laborales diarias', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'HoursDaily';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'HoursDaily';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ingreso base de cotización diario; salario cotizable por día, base para aportes (INT, nullable)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'IncomeDailyBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ingreso base cotizacion diario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'IncomeDailyBase';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'IncomeDailyBase';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ingreso base de cotización; salario cotizable, base para cálculo de aportes a pensión y seguridad social (DECIMAL(18), nullable)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'BaseIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ingreso base cotizacion', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'BaseIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'BaseIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la razón de modificación del contrato; FK a ContractModificationReason, causas de cambio (INT, nullable)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ContractModificationReasonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Razones de Modificación del Contrato (FK)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ContractModificationReasonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ContractModificationReasonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si se liquida nómina: 1=Sí, 0=No; procesamiento de pago, generación de recibos (TINYINT)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'LiquidationPayroll';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquidacion nomina 1-Si 0-No', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'LiquidationPayroll';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'LiquidationPayroll';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de cuenta bancaria: 1=Ahorros, 2=Corriente; cuenta de consignación de nómina (INT)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'BankAccountType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de cuenta Ahorros - Corriente', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'BankAccountType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'BankAccountType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de cuenta bancaria del empleado; IBAN, cuenta destino de pago (VARCHAR(20), PII sensible)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'BankAccountNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero cuenta bancaria empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'BankAccountNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'BankAccountNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del banco donde tiene cuenta de nómina; FK a Bank, institución financiera pagadora (INT)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'BankId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del banco donde tiene la cuenta de nomina (FK)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'BankId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'BankId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de nómina; FK a Group, agrupación de empleados para procesamiento (INT)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Grupo (FK)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'GroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas o comentarios del contrato; observaciones, aclaraciones, información complementaria (VARCHAR(255), nullable)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'Notes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Notas o Comentarios del Contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'Notes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'Notes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica vigencia del contrato: 1=Vigente, 0=No vigente; solo un contrato vigente por empleado a pesar de múltiples activos temporales (BIT)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'Valid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica Si un Contrato es Vigente o No. 1-Vigente 0- No Solo debe haber un contrato vigente por empleado asi tenga dos contratos temporalmente activos', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'Valid';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'Valid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de cotizante a pensión: 1=Normal, 2=Especial, 3=Alto riesgo, 4=Servidor público; régimen contributivo (TINYINT)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'TypeOfPensionContribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de cotizante pension  1 - Normal  2 - Especial  3 - Alto riesgo  4 - Servidor publico', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'TypeOfPensionContribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'TypeOfPensionContribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que entra en vigencia la modificación del contrato; efectividad del cambio, fecha de aplicación (DATETIME)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la cual entra en vigencia la modificaion del contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario que creó el contrato; usuario originador, auditoría inicial (VARCHAR(20))', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'CreationUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del usuario que modifico', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'CreationUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'CreationUserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del contrato; timestamp de registro, inicio de documento (DATETIME)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ContractCreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creacion Contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ContractCreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ContractCreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje del sueldo a pagar durante período de prueba; escala salarial temporal (INT, nullable)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'TrialPeriodSalaryPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje del sueldo a pagar durante el periodo de prueba', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'TrialPeriodSalaryPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'TrialPeriodSalaryPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración del período de prueba en días; plazo de evaluación, tiempo de adaptación (INT, nullable)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'TrialPeriodTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo de periodo de pruebas en dias', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'TrialPeriodTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'TrialPeriodTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si está en período de prueba: 1=Sí, 0=No; fase inicial de contrato, evaluación laboral (BIT)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'TrialPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Esta en periodo de prueba 1 - Si 0 - No', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'TrialPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'TrialPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Forma de pago: 1=Cheque, 2=Efectivo, 3=Consignación, 4=Desprendible; método de entrega de nómina (TINYINT)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'PaymentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Forma de pago: 1 - Cheque 2 - Efectivo 3 - Consignacion 4 - Desprendible', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'PaymentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'PaymentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Período de pago: 1=Mensual, 2=Quincenal; frecuencia de liquidación, ciclo de nómina (TINYINT)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'PaymentPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Periodo de Pago: 1 Mensual - 2 Quincenal', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'PaymentPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'PaymentPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de retiro del empleado; desvinculación, término laboral, fin de relación (DATE, nullable)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'RetirementDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de retiro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'RetirementDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'RetirementDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del motivo de retiro; FK a RetirementReason, causal de liquidación (TINYINT, nullable)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'RetirementReasonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Motivo de Retiro (FK). En caso de que el usuario le hayan realizado Liquidación de Contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'RetirementReasonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'RetirementReasonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del contrato: 1=Activo, 2=Liquidado, 3=Anulado, 4=Reemplazado; ciclo de vida del documento (TINYINT)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Contrato:1-Activo  2-Liquidado 3-Anulado 4-Reemplazado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sueldo básico del empleado; remuneración base, salario mensual (NUMERIC(18))', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'BasicSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sueldo Basico', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'BasicSalary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'BasicSalary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final de vigencia del contrato; término, vencimiento, fecha de conclusión (DATE)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ContractEndingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha final de contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ContractEndingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ContractEndingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial de vigencia del contrato; inicio, efectividad, comienzo de relación (DATE)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ContractInitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha inicial de contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ContractInitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ContractInitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vinculación laboral del empleado; ingreso, fecha de asociación a empresa (DATE)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'JobBondingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de vinculacion laboral', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'JobBondingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'JobBondingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de contrato; FK a ContractType, modalidad (indefinido, temporal, aprendizaje, etc.) (INT)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ContractTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FK Id del tipo de contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ContractTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ContractTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del nivel de cargo en Payroll; FK a Position, posición salarial, escala (INT)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'PositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del nivel de cargo (FK)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'PositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'PositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del empleado; FK a Employee, trabajador, profesional de salud (INT)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del empleado (FK)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de Acta de Posesión; acto administrativo de toma de posesión, para entidades públicas (VARCHAR(50), nullable)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'CertificateOfficeNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Acta Posesión', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'CertificateOfficeNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'CertificateOfficeNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Posesión; toma de posesión, solo para Entidades Públicas (DATE, nullable)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'PosesionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Posesión (Únicamente para Entidades Públicas)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'PosesionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'PosesionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la Resolución; fecha de expedición del acto administrativo (DATE, nullable)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ResolutionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Resolución', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ResolutionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ResolutionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de Resolución; acto administrativo, decreto de contratación (VARCHAR(50), nullable)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Resolucion', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'ResolutionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del contrato inicial o base; referencia al primer contrato origen, histórico (INT)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'InitialContractNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nùmero Contrato Inicial o Base - Hace referencia al id del primer contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'InitialContractNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'InitialContractNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de registro: 1=Contrato Base, 2=Novedad de Contrato; clasificación de documento, cambios o adiciones (TINYINT)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'RowType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Registro 1- Contrato Base 2- Novedad Contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'RowType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'RowType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del contrato parcial; clave primaria, PK_Contract__Id (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del contrato', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contratos parciales o adiciones/modificaciones de contratos laborales del talento humano. Registra las condiciones de vinculación de cada empleado: cargo, salario, fechas de contrato, período de prueba, forma de pago, cuenta bancaria, retiro y demás datos de nómina y gestión de personal.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PartialContract';

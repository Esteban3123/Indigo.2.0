CREATE TABLE [Payroll].[Contract] (
    [Id]                           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RowType]                      TINYINT       NOT NULL,
    [InitialContractNumber]        INT           NOT NULL,
    [ResolutionNumber]             VARCHAR (50)  NULL,
    [ResolutionDate]               DATE          NULL,
    [PosesionDate]                 DATE          NULL,
    [CertificateOfficeNumber]      VARCHAR (50)  NULL,
    [EmployeeId]                   INT           NOT NULL,
    [PositionId]                   INT           NOT NULL,
    [FunctionalUnitId]             INT           NOT NULL,
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
    [WorkPlaceId]                  INT           NULL,
    [InsuredCCSSCode]              VARCHAR (25)  NULL,
    [ParameterACCAI]               BIT           DEFAULT ((0)) NULL,
    CONSTRAINT [PK_Contract__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Contract_Bank] FOREIGN KEY ([BankId]) REFERENCES [Payroll].[Bank] ([Id]),
    CONSTRAINT [FK_Contract_ContractModificationReason] FOREIGN KEY ([ContractModificationReasonId]) REFERENCES [Payroll].[ContractModificationReason] ([Id]),
    CONSTRAINT [FK_Contract_ContractType] FOREIGN KEY ([ContractTypeId]) REFERENCES [Payroll].[ContractType] ([Id]),
    CONSTRAINT [FK_Contract_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_Contract_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_Contract_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id]),
    CONSTRAINT [FK_Contract_OrganizationChartPosition] FOREIGN KEY ([OrganizationChartPositionId]) REFERENCES [HumanTalent].[OrganizationChartPosition] ([Id]),
    CONSTRAINT [FK_Contract_Position] FOREIGN KEY ([PositionId]) REFERENCES [Payroll].[Position] ([Id]),
    CONSTRAINT [FK_Contract_RetirementReason] FOREIGN KEY ([RetirementReasonId]) REFERENCES [Payroll].[RetirementReason] ([Id]),
    CONSTRAINT [FK_Contract_WorkPlace] FOREIGN KEY ([WorkPlaceId]) REFERENCES [HumanTalent].[WorkPlace] ([Id])
);


GO
ALTER TABLE [Payroll].[Contract] NOCHECK CONSTRAINT [FK_Contract_Bank];


GO
ALTER TABLE [Payroll].[Contract] NOCHECK CONSTRAINT [FK_Contract_ContractType];


GO
ALTER TABLE [Payroll].[Contract] NOCHECK CONSTRAINT [FK_Contract_Employee];


GO
ALTER TABLE [Payroll].[Contract] NOCHECK CONSTRAINT [FK_Contract_FunctionalUnit];


GO
ALTER TABLE [Payroll].[Contract] NOCHECK CONSTRAINT [FK_Contract_Group];


GO
ALTER TABLE [Payroll].[Contract] NOCHECK CONSTRAINT [FK_Contract_OrganizationChartPosition];


GO
ALTER TABLE [Payroll].[Contract] NOCHECK CONSTRAINT [FK_Contract_Position];


GO
ALTER TABLE [Payroll].[Contract] NOCHECK CONSTRAINT [FK_Contract_RetirementReason];


GO
ALTER TABLE [Payroll].[Contract] NOCHECK CONSTRAINT [FK_Contract_WorkPlace];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contrato de Contigencia:  0 - Ninguna  1 - Licencias  2 - Vacaciones  3 - Incapacidades', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'Contingency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de cotizante pension  1 - Normal  2 - Especial  3 - Alto riesgo  4 - Servidor publico', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Contract', @level2type = N'COLUMN', @level2name = N'TypeOfPensionContribution';


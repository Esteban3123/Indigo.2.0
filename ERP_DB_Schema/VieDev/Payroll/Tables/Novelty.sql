CREATE TABLE [Payroll].[Novelty] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Consecutive]                   INT             NOT NULL,
    [GroupId]                       INT             NOT NULL,
    [EmployeeId]                    INT             NOT NULL,
    [TypeNovelty]                   TINYINT         NOT NULL,
    [Extension]                     TINYINT         NOT NULL,
    [IBC]                           NUMERIC (18)    NOT NULL,
    [EmployeeBaseSalary]            NUMERIC (18)    NOT NULL,
    [RealDate]                      DATE            NOT NULL,
    [Days]                          TINYINT         NOT NULL,
    [EndDate]                       DATE            NOT NULL,
    [VacationInitialDateNovelty]    DATE            NULL,
    [VacationEndDateNovelty]        DATE            NULL,
    [LiquidatedIBCorSalary]         TINYINT         NOT NULL,
    [Reason]                        VARCHAR (500)   NOT NULL,
    [LiquidationBase]               INT             NOT NULL,
    [EPSDays]                       TINYINT         NULL,
    [EmployerDays]                  TINYINT         NULL,
    [LicenseClass]                  TINYINT         NULL,
    [InabilityClass]                TINYINT         NULL,
    [RiskType]                      TINYINT         NULL,
    [NoveltyLiquidate]              BIT             NOT NULL,
    [CalculationType]               TINYINT         NULL,
    [EPSRecognizeValue]             NUMERIC (18, 2) NULL,
    [PaidPayrollValue]              NUMERIC (18, 2) NULL,
    [PaidEmployerValue]             NUMERIC (18, 2) NULL,
    [Value]                         NUMERIC (18, 2) NULL,
    [AutorizationNumber]            VARCHAR (30)    NULL,
    [ResolutionNumber]              VARCHAR (30)    NULL,
    [ResolutionDate]                DATE            NULL,
    [Status]                        TINYINT         CONSTRAINT [DF_Novelty_Status] DEFAULT ((0)) NOT NULL,
    [CreationUser]                  VARCHAR (20)    NOT NULL,
    [CreationDate]                  DATETIME        NOT NULL,
    [ModificationUser]              VARCHAR (20)    NULL,
    [ModificationDate]              DATETIME        NULL,
    [TimeStamp]                     ROWVERSION      NOT NULL,
    [IsCycleDate]                   BIT             DEFAULT ((0)) NOT NULL,
    [LiquidationBasePatrono]        DECIMAL (18, 2) CONSTRAINT [DF_Novelty_LiquidationBasePatrono] DEFAULT ((0)) NOT NULL,
    [IntegralSalaryDisabilityValue] DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_Novelty__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Disability_Employee_EmployeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_Disability_Group_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id])
);


GO
ALTER TABLE [Payroll].[Novelty] NOCHECK CONSTRAINT [FK_Disability_Employee_EmployeId];


GO
ALTER TABLE [Payroll].[Novelty] NOCHECK CONSTRAINT [FK_Disability_Group_GroupId];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la fecha real es el inicio de un ciclo de descuentos de liquidación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Novelty', @level2type = N'COLUMN', @level2name = N'IsCycleDate';


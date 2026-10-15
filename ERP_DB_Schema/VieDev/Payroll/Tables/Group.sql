CREATE TABLE [Payroll].[Group] (
    [Id]                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                   VARCHAR (20)  NOT NULL,
    [CompanyId]              INT           NOT NULL,
    [PayrollParameterId]     INT           NOT NULL,
    [Name]                   VARCHAR (150) NOT NULL,
    [Liquidation]            TINYINT       NOT NULL,
    [LastDateLiquidation]    DATETIME      NOT NULL,
    [NextDateLiquidation]    DATETIME      NOT NULL,
    [Month]                  CHAR (1)      NOT NULL,
    [HandlingProvisions]     BIT           NOT NULL,
    [ContractClass]          TINYINT       NOT NULL,
    [State]                  BIT           NOT NULL,
    [CreationUser]           VARCHAR (20)  NULL,
    [CreationDate]           DATETIME      NULL,
    [ModificationUser]       VARCHAR (20)  NULL,
    [ModificationDate]       DATETIME      NULL,
    [TimeStamp]              ROWVERSION    NOT NULL,
    [AdditionalVacationDays] TINYINT       CONSTRAINT [DF_Group_AdditionalVacationDays] DEFAULT ((0)) NOT NULL,
    [SMMLVAmountExemption]   INT           NULL,
    CONSTRAINT [PK_Group__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Group_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Payroll].[Company] ([Id]),
    CONSTRAINT [FK_Group_PayrollParameter] FOREIGN KEY ([PayrollParameterId]) REFERENCES [Payroll].[PayrollParameter] ([Id]),
    CONSTRAINT [IX_Group] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
ALTER TABLE [Payroll].[Group] NOCHECK CONSTRAINT [FK_Group_Company];


GO
ALTER TABLE [Payroll].[Group] NOCHECK CONSTRAINT [FK_Group_PayrollParameter];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de SMMLV Exonerado SENA, ICBF y Salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'SMMLVAmountExemption';


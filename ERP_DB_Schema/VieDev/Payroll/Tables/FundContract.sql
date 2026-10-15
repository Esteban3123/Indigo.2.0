CREATE TABLE [Payroll].[FundContract] (
    [Id]                         INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FundId]                     INT          NOT NULL,
    [ContractId]                 INT          NOT NULL,
    [FundType]                   TINYINT      NOT NULL,
    [InitialDate]                DATE         NOT NULL,
    [EndingDate]                 DATE         NULL,
    [MembershipNumber]           VARCHAR (15) NOT NULL,
    [VoluntaryContribution]      BIT          NOT NULL,
    [VoluntaryContributionValue] NUMERIC (18) NULL,
    [State]                      BIT          NOT NULL,
    [CreationUser]               VARCHAR (20) CONSTRAINT [DF_FundContract_CreationUser] DEFAULT ((999)) NULL,
    [CreationDate]               DATETIME     CONSTRAINT [DF_FundContract_CreationDate] DEFAULT ([Common].[getdate]()) NULL,
    [ModificationUser]           VARCHAR (20) NULL,
    [ModificationDate]           DATETIME     NULL,
    [ParameterACCAI]             BIT          DEFAULT ((0)) NULL,
    CONSTRAINT [PK_FundContract__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FundContract_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_FundContract_Fund] FOREIGN KEY ([FundId]) REFERENCES [Payroll].[Fund] ([Id])
);


GO
ALTER TABLE [Payroll].[FundContract] NOCHECK CONSTRAINT [FK_FundContract_Contract];


GO
ALTER TABLE [Payroll].[FundContract] NOCHECK CONSTRAINT [FK_FundContract_Fund];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de fondo   1 - Salud   2 - Pension   3 - Cesantias   4 - Riesgo    5 - Caja Compensacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'FundContract', @level2type = N'COLUMN', @level2name = N'FundType';


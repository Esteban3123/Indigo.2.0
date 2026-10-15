CREATE TABLE [Payroll].[LiquidationDetail] (
    [Id]                      INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PayrollId]               INT            NOT NULL,
    [RegisterStatus]          INT            NOT NULL,
    [PayrollDate]             DATE           NOT NULL,
    [LiquidationPeriod]       CHAR (1)       NOT NULL,
    [ConceptClass]            CHAR (3)       NOT NULL,
    [ConceptId]               INT            NOT NULL,
    [ConceptCode]             VARCHAR (4)    NULL,
    [ConceptDetail]           VARCHAR (300)  NULL,
    [ConceptTotalValue]       NUMERIC (18)   NOT NULL,
    [AccruedValue]            NUMERIC (18)   NULL,
    [DeductedValue]           NUMERIC (18)   NULL,
    [RetentionBase]           DECIMAL (18)   NULL,
    [RetentionPercentage]     DECIMAL (6, 3) NULL,
    [InitialBalance]          NUMERIC (18)   NULL,
    [ConceptType]             CHAR (1)       NOT NULL,
    [AgreementsId]            INT            NULL,
    [AgreementsDId]           INT            NULL,
    [ConceptFormulate]        VARCHAR (MAX)  NULL,
    [ReplaceConceptFormulate] VARCHAR (MAX)  NULL,
    [DistribuirGasto]         BIT            NULL,
    [InabilityCollect]        NUMERIC (18)   NULL,
    [SpendingInability]       NUMERIC (18)   NULL,
    [TotalNumberHours]        INT            CONSTRAINT [DF_LiquidationDetail_TotalNumberHours] DEFAULT ((0)) NULL,
    [IdThirdParty]            INT            NULL,
    [TypeArticleRTF]          TINYINT        NULL,
    [RetentionId]             INT            NULL,
    [ErpDays]                 INT            NULL,
    [EmployeerDays]           INT            NULL,
    [LutoDays]                INT            NULL,
    CONSTRAINT [PK_LiquidationDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Liquidation_LiquidationDetail] FOREIGN KEY ([PayrollId]) REFERENCES [Payroll].[Liquidation] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_LiquidationDetail_AgreementsC] FOREIGN KEY ([AgreementsId]) REFERENCES [Payroll].[AgreementsC] ([Id]),
    CONSTRAINT [FK_LiquidationDetail_AgreementsD] FOREIGN KEY ([AgreementsDId]) REFERENCES [Payroll].[AgreementsD] ([Id]),
    CONSTRAINT [FK_LiquidationDetail_RetentionConcept] FOREIGN KEY ([RetentionId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_LiquidationDetail_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_PayrollDetail_PayrollConcept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id])
);


GO
ALTER TABLE [Payroll].[LiquidationDetail] NOCHECK CONSTRAINT [FK_Liquidation_LiquidationDetail];


GO
ALTER TABLE [Payroll].[LiquidationDetail] NOCHECK CONSTRAINT [FK_LiquidationDetail_AgreementsC];


GO
ALTER TABLE [Payroll].[LiquidationDetail] NOCHECK CONSTRAINT [FK_LiquidationDetail_AgreementsD];


GO
ALTER TABLE [Payroll].[LiquidationDetail] NOCHECK CONSTRAINT [FK_LiquidationDetail_RetentionConcept];


GO
ALTER TABLE [Payroll].[LiquidationDetail] NOCHECK CONSTRAINT [FK_LiquidationDetail_ThirdParty];


GO
ALTER TABLE [Payroll].[LiquidationDetail] NOCHECK CONSTRAINT [FK_PayrollDetail_PayrollConcept];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días de luto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'LiquidationDetail', @level2type = N'COLUMN', @level2name = N'LutoDays';


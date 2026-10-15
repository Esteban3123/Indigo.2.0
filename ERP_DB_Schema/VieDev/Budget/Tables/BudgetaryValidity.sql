CREATE TABLE [Budget].[BudgetaryValidity] (
    [Id]                           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Year]                         INT             NOT NULL,
    [BudgetaryEntityId]            INT             NOT NULL,
    [CxPCreateSingleCategory]      BIT             CONSTRAINT [DF_BudgetaryValidity_CxPCreateSingleCategory] DEFAULT ((0)) NOT NULL,
    [CxPSingleCategoryId]          INT             NULL,
    [Status]                       TINYINT         NOT NULL,
    [IncomeMonth]                  INT             NOT NULL,
    [ExpenseMonth]                 INT             NOT NULL,
    [ResolutionNumber]             VARCHAR (20)    NOT NULL,
    [ResolutionValue]              NUMERIC (18, 2) NOT NULL,
    [LegalRepresentativeId]        INT             NULL,
    [ChiefBudgetOfficerId]         INT             NOT NULL,
    [ChiefFinancialOfficerId]      INT             NOT NULL,
    [InitialStateIncome]           INT             NULL,
    [InitialStateExpenses]         INT             NULL,
    [DateIncomeRecord]             DATETIME        NULL,
    [DateExpensesRecord]           DATETIME        NULL,
    [PACControl]                   BIT             NOT NULL,
    [ConsecutiveModPTOIncome]      INT             NULL,
    [ConsecutiveTrasPTOIncome]     INT             NULL,
    [ConsecutiveModPACIncome]      INT             NULL,
    [ConsecutiveTrasPACIncome]     INT             NULL,
    [ConsecutiveRecognition]       INT             NULL,
    [ConsecutiveModRecognition]    INT             NULL,
    [ConsecutiveCollection]        INT             NULL,
    [ConsecutiveModCollection]     INT             NULL,
    [ConsecutiveModPTOExpense]     INT             NULL,
    [ConsecutiveTrasPTOExpense]    INT             NULL,
    [ConsecutiveModPACExpense]     INT             NULL,
    [ConsecutiveTrasPACExpense]    INT             NULL,
    [ConsecutiveCDP]               INT             NULL,
    [ConsecutiveModCDP]            INT             NULL,
    [ConsecutiveRP]                INT             NULL,
    [ConsecutiveModRP]             INT             NULL,
    [ConsecutiveLiabilities]       INT             NULL,
    [ConsecutiveModLiabilities]    INT             NULL,
    [ConsecutiveODP]               INT             NULL,
    [ConsecutiveExtendedCDP]       INT             NULL,
    [ConsecutiveResourceRelease]   INT             NULL,
    [ConsecutiveReinstatement]     INT             NULL,
    [ConsecutiveReservation]       INT             NULL,
    [ConsecutiveCXP]               INT             NULL,
    [ConsecutiveCDPVFT]            INT             NULL,
    [ConsecutiveRPVFT]             INT             NULL,
    [ConsecutiveLiabilitiesVFT]    INT             NULL,
    [ConsecutiveODPVFT]            INT             NULL,
    [ConsecutiveSuspensionPTO]     INT             NULL,
    [ConsecutiveLiftingPTO]        INT             NULL,
    [ControlDateConsecutive]       BIT             NULL,
    [ConsecutiveCXC]               INT             NULL,
    [ConsecutiveDocumentExtension] INT             NULL,
    [AnnualClosureOfIncome]        BIT             NULL,
    [AnnualClosureOfExpenses]      BIT             NULL,
    [CreationUser]                 VARCHAR (20)    CONSTRAINT [DF_BudgetaryValidity_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]                 DATETIME        CONSTRAINT [DF_BudgetaryValidity_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]             VARCHAR (20)    NULL,
    [ModificationDate]             DATETIME        NULL,
    [ActivateUser]                 VARCHAR (20)    NULL,
    [ActivateDate]                 DATETIME        NULL,
    [ClosureUser]                  VARCHAR (20)    NULL,
    [ClosureDate]                  DATETIME        NULL,
    [TimeStamp]                    ROWVERSION      NOT NULL,
    CONSTRAINT [PK_BudgetaryValidity__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BudgetaryValidity_Category] FOREIGN KEY ([CxPSingleCategoryId]) REFERENCES [Budget].[Category] ([Id]),
    CONSTRAINT [FK_Validity_BudgetEntities] FOREIGN KEY ([BudgetaryEntityId]) REFERENCES [Budget].[BudgetaryEntity] ([Id]),
    CONSTRAINT [FK_Validity_Validity_BudgetBoss] FOREIGN KEY ([ChiefBudgetOfficerId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_Validity_Validity_FinancialBoss] FOREIGN KEY ([ChiefFinancialOfficerId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_Validity_Validity_LegalRepresentative] FOREIGN KEY ([LegalRepresentativeId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Budget].[BudgetaryValidity] NOCHECK CONSTRAINT [FK_BudgetaryValidity_Category];


GO
ALTER TABLE [Budget].[BudgetaryValidity] NOCHECK CONSTRAINT [FK_Validity_BudgetEntities];


GO
ALTER TABLE [Budget].[BudgetaryValidity] NOCHECK CONSTRAINT [FK_Validity_Validity_BudgetBoss];


GO
ALTER TABLE [Budget].[BudgetaryValidity] NOCHECK CONSTRAINT [FK_Validity_Validity_FinancialBoss];


GO
ALTER TABLE [Budget].[BudgetaryValidity] NOCHECK CONSTRAINT [FK_Validity_Validity_LegalRepresentative];


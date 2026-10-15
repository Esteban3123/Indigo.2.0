CREATE TABLE [Budget].[Category] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BudgetaryValidityId] INT           NOT NULL,
    [ItemType]            TINYINT       NOT NULL,
    [Code]                VARCHAR (40)  NULL,
    [AlternativeCode]     VARCHAR (40)  NULL,
    [Name]                VARCHAR (300) NOT NULL,
    [CategoryOwnerId]     INT           NULL,
    [Auxiliary]           BIT           NOT NULL,
    [FinancialSourceId]   INT           NULL,
    [PAC]                 BIT           NULL,
    [StatusPAC]           TINYINT       CONSTRAINT [DF_Category_StatusPAC] DEFAULT ((1)) NULL,
    [Used]                BIT           NOT NULL,
    [FutureValidity]      BIT           NULL,
    [InvertionProject]    BIT           NULL,
    [BalanceDeficit]      BIT           NULL,
    [IncomeCxP]           BIT           NULL,
    [ReservePacStatus]    TINYINT       NULL,
    [StatusPacCxP]        TINYINT       NULL,
    [Status]              BIT           NOT NULL,
    [CreationUser]        VARCHAR (20)  CONSTRAINT [DF_Category_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]        DATETIME      CONSTRAINT [DF_Category_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]    VARCHAR (20)  NULL,
    [ModificationDate]    DATETIME      NULL,
    [TimeStamp]           ROWVERSION    NOT NULL,
    [CCPETCodeId]         INT           NULL,
    [CPCCodeId]           INT           NULL,
    [FundSituation]       CHAR (1)      CONSTRAINT [DF_Category_FundSituation] DEFAULT ('C') NOT NULL,
    [Validity]            TINYINT       CONSTRAINT [DF_Category_Validity] DEFAULT ((1)) NOT NULL,
    [PublicPolicyId]      INT           NULL,
    CONSTRAINT [PK_Category__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Budget_CCPETCodeId] FOREIGN KEY ([CCPETCodeId]) REFERENCES [Budget].[CCPET] ([Id]),
    CONSTRAINT [FK_Budget_CPCCodeId] FOREIGN KEY ([CPCCodeId]) REFERENCES [Budget].[CPCCatalog] ([Id]),
    CONSTRAINT [FK_BudgetItem_FinancialSource] FOREIGN KEY ([FinancialSourceId]) REFERENCES [Budget].[FinancialSource] ([Id]),
    CONSTRAINT [FK_BudgetItem_Validity] FOREIGN KEY ([BudgetaryValidityId]) REFERENCES [Budget].[BudgetaryValidity] ([Id]),
    CONSTRAINT [FK_Category_Category] FOREIGN KEY ([CategoryOwnerId]) REFERENCES [Budget].[Category] ([Id]),
    CONSTRAINT [FK_Category_PublicPolicy] FOREIGN KEY ([PublicPolicyId]) REFERENCES [Budget].[PublicPolicy] ([Id])
);


GO
ALTER TABLE [Budget].[Category] NOCHECK CONSTRAINT [FK_Budget_CCPETCodeId];


GO
ALTER TABLE [Budget].[Category] NOCHECK CONSTRAINT [FK_Budget_CPCCodeId];


GO
ALTER TABLE [Budget].[Category] NOCHECK CONSTRAINT [FK_BudgetItem_FinancialSource];


GO
ALTER TABLE [Budget].[Category] NOCHECK CONSTRAINT [FK_BudgetItem_Validity];


GO
ALTER TABLE [Budget].[Category] NOCHECK CONSTRAINT [FK_Category_Category];


GO
ALTER TABLE [Budget].[Category] NOCHECK CONSTRAINT [FK_Category_PublicPolicy];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vigencia:   1 - Vigencia Actual   2 - Reservas  3 - Cuentas por pagar  4 - Vigencias futuras - Vigencia Actual  5 - Vigencias futuras - Reservas  6 - Vigencias futuras - Cuentas por pagar', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'Validity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Situación de Fondos:  C - Con situación de Fondos  S - Sin Situación de Fondos ', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'FundSituation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obtiene o establece el estado del PAC para el Rubro  1 - Registrado  2 - Confirmado  3 - Anulado', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Category', @level2type = N'COLUMN', @level2name = N'StatusPAC';


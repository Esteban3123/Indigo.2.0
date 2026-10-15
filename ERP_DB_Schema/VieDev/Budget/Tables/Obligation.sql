CREATE TABLE [Budget].[Obligation] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                  VARCHAR (20)  NOT NULL,
    [BudgetaryValidityId]   INT           NOT NULL,
    [ThirdPartyId]          INT           NOT NULL,
    [Document]              VARCHAR (100) NOT NULL,
    [DocumentDate]          DATETIME      NOT NULL,
    [ObligationType]        TINYINT       NOT NULL,
    [Observations]          VARCHAR (MAX) NOT NULL,
    [Status]                TINYINT       NOT NULL,
    [CreationUser]          VARCHAR (20)  CONSTRAINT [DF_Obligation_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]          DATETIME      CONSTRAINT [DF_Obligation_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]      VARCHAR (20)  NULL,
    [ModificationDate]      DATETIME      NULL,
    [ConfirmationUser]      VARCHAR (20)  NULL,
    [ConfirmationDate]      DATETIME      NULL,
    [AnnulmentUser]         VARCHAR (20)  NULL,
    [AnnulmentDate]         DATETIME      NULL,
    [TimeStamp]             ROWVERSION    NOT NULL,
    [AnnulmentConceptId]    INT           NULL,
    [AnnulmentDescription]  VARCHAR (MAX) NULL,
    [AutomaticPaymentOrder] BIT           CONSTRAINT [DF_Obligation_AutomaticPaymentOrder] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Obligation__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Obligation_BudgetaryValidity] FOREIGN KEY ([BudgetaryValidityId]) REFERENCES [Budget].[BudgetaryValidity] ([Id]),
    CONSTRAINT [FK_Obligation_Concept] FOREIGN KEY ([AnnulmentConceptId]) REFERENCES [Budget].[Concept] ([Id]),
    CONSTRAINT [FK_Obligation_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Budget].[Obligation] NOCHECK CONSTRAINT [FK_Obligation_BudgetaryValidity];


GO
ALTER TABLE [Budget].[Obligation] NOCHECK CONSTRAINT [FK_Obligation_Concept];


GO
ALTER TABLE [Budget].[Obligation] NOCHECK CONSTRAINT [FK_Obligation_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de obligaciones   Obligacion =1  CuentaXPagar = 2  ', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Obligation', @level2type = N'COLUMN', @level2name = N'ObligationType';


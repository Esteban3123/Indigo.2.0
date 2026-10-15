CREATE TABLE [Budget].[Commitment] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                  VARCHAR (20)  NOT NULL,
    [BudgetaryValidityId]   INT           NOT NULL,
    [ThirdPartyId]          INT           NOT NULL,
    [DocumentSource]        TINYINT       NOT NULL,
    [Document]              VARCHAR (100) NOT NULL,
    [DocumentDate]          DATETIME      NOT NULL,
    [CommitmentType]        TINYINT       NOT NULL,
    [Observations]          VARCHAR (MAX) NULL,
    [Status]                TINYINT       NOT NULL,
    [CreationUser]          VARCHAR (20)  CONSTRAINT [DF_Commitment_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]          DATETIME      CONSTRAINT [DF_Commitment_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]      VARCHAR (20)  NULL,
    [ModificationDate]      DATETIME      NULL,
    [ConfirmationUser]      VARCHAR (20)  NULL,
    [ConfirmationDate]      DATETIME      NULL,
    [AnnulmentUser]         VARCHAR (20)  NULL,
    [AnnulmentDate]         DATETIME      NULL,
    [TimeStamp]             ROWVERSION    NOT NULL,
    [EntityId]              INT           NULL,
    [EntityCode]            VARCHAR (20)  NULL,
    [EntityName]            VARCHAR (250) NULL,
    [AnnulmentConceptId]    INT           NULL,
    [AnnulmentDescription]  VARCHAR (MAX) NULL,
    [AutomaticObligation]   BIT           CONSTRAINT [DF_Commitment_AutomaticObligation] DEFAULT ((0)) NOT NULL,
    [AutomaticPaymentOrder] BIT           CONSTRAINT [DF_Commitment_AutomaticPaymentOrder] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Commitment__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Commitment_BudgetaryValidity] FOREIGN KEY ([BudgetaryValidityId]) REFERENCES [Budget].[BudgetaryValidity] ([Id]),
    CONSTRAINT [FK_Commitment_Concept] FOREIGN KEY ([AnnulmentConceptId]) REFERENCES [Budget].[Concept] ([Id]),
    CONSTRAINT [FK_Commitment_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Budget].[Commitment] NOCHECK CONSTRAINT [FK_Commitment_BudgetaryValidity];


GO
ALTER TABLE [Budget].[Commitment] NOCHECK CONSTRAINT [FK_Commitment_Concept];


GO
ALTER TABLE [Budget].[Commitment] NOCHECK CONSTRAINT [FK_Commitment_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de tiempo. Guarda el instante tiempo de la creación, registro o modificación de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Commitment', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Origen del documento  1 - Otro  2 - Orden de Trabajo  3 - Contrato', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Commitment', @level2type = N'COLUMN', @level2name = N'DocumentSource';


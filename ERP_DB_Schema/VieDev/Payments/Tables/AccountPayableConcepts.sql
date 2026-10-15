CREATE TABLE [Payments].[AccountPayableConcepts] (
    [Id]                                INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                              VARCHAR (20)  NOT NULL,
    [Name]                              VARCHAR (100) NULL,
    [ConceptType]                       TINYINT       CONSTRAINT [DF_AccountPayableConcepts_NoteType] DEFAULT ((2)) NOT NULL,
    [IdAccount]                         INT           NULL,
    [HandlesRetention]                  BIT           CONSTRAINT [DF_PaymentsConcept_IvaConcept] DEFAULT ((0)) NOT NULL,
    [EmployeeCategoryRetention]         BIT           CONSTRAINT [DF_AccountPayableConcepts_EmployeeCategoryRetention] DEFAULT ((0)) NOT NULL,
    [ThreeEightThreeRetentionConceptId] INT           NULL,
    [ThreeEightThreeAccountId]          INT           NULL,
    [ThreeEightFourRetentionConceptId]  INT           NULL,
    [ThreeEightFourAccountId]           INT           NULL,
    [RetentionConceptId]                INT           NULL,
    [DeferredCausation]                 BIT           NOT NULL,
    [AccumulatedBudgetCalculation]      BIT           NOT NULL,
    [FreeResourcesUnexecuted]           BIT           NOT NULL,
    [Status]                            BIT           CONSTRAINT [DF_PaymentsConcept_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]                      VARCHAR (20)  NOT NULL,
    [CreationDate]                      DATETIME      NOT NULL,
    [ModificationUser]                  VARCHAR (20)  NULL,
    [ModificationDate]                  DATETIME      NULL,
    [TimeStamp]                         ROWVERSION    NOT NULL,
    [HandleTaxes]                       BIT           CONSTRAINT [DF__AccountPa__Handl__1067B6E1] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_AccountPayableConcepts__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountPayableConcepts_MainAccounts] FOREIGN KEY ([IdAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountPayableConcepts_MainAccounts383] FOREIGN KEY ([ThreeEightThreeAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountPayableConcepts_MainAccounts384] FOREIGN KEY ([ThreeEightFourAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AccountPayableConcepts_RetentionConcepts] FOREIGN KEY ([RetentionConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_AccountPayableConcepts_RetentionConcepts1] FOREIGN KEY ([ThreeEightThreeRetentionConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_AccountPayableConcepts_RetentionConcepts2] FOREIGN KEY ([ThreeEightFourRetentionConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id])
);


GO
ALTER TABLE [Payments].[AccountPayableConcepts] NOCHECK CONSTRAINT [FK_AccountPayableConcepts_MainAccounts];


GO
ALTER TABLE [Payments].[AccountPayableConcepts] NOCHECK CONSTRAINT [FK_AccountPayableConcepts_MainAccounts383];


GO
ALTER TABLE [Payments].[AccountPayableConcepts] NOCHECK CONSTRAINT [FK_AccountPayableConcepts_MainAccounts384];


GO
ALTER TABLE [Payments].[AccountPayableConcepts] NOCHECK CONSTRAINT [FK_AccountPayableConcepts_RetentionConcepts];


GO
ALTER TABLE [Payments].[AccountPayableConcepts] NOCHECK CONSTRAINT [FK_AccountPayableConcepts_RetentionConcepts1];


GO
ALTER TABLE [Payments].[AccountPayableConcepts] NOCHECK CONSTRAINT [FK_AccountPayableConcepts_RetentionConcepts2];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de concepto   1  - General  2 - Especifico    Cuando es general no se solicita la cuenta contable pero si se solicita cuando esten creando la cuenta por pagar    Cuando es especifico si se obliga a que seleccionen una cuenta contable', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConcepts', @level2type = N'COLUMN', @level2name = N'ConceptType';


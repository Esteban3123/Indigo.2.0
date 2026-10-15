CREATE TABLE [Payments].[AccountPayableConceptNotes] (
    [Id]                             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                           VARCHAR (20)  NOT NULL,
    [Name]                           VARCHAR (100) NOT NULL,
    [ConceptType]                    TINYINT       NOT NULL,
    [IdAccount]                      INT           NULL,
    [AffectBudget]                   BIT           NOT NULL,
    [Behavior]                       TINYINT       NULL,
    [Status]                         BIT           CONSTRAINT [DF_PaymentsNoteConcept_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]                   VARCHAR (20)  NOT NULL,
    [CreationDate]                   DATETIME      NOT NULL,
    [ModificationUser]               VARCHAR (20)  NULL,
    [ModificationDate]               DATETIME      NULL,
    [TimeStamp]                      ROWVERSION    NOT NULL,
    [ManageRetention]                BIT           CONSTRAINT [DF__AccountPa__Manag__4897016B] DEFAULT ((0)) NOT NULL,
    [ManageTax]                      BIT           CONSTRAINT [DF__AccountPa__Manag__498B25A4] DEFAULT ((0)) NOT NULL,
    [TypeCategoryIndependentWorkers] BIT           CONSTRAINT [DF_AccountPayableConceptNotes_TypeCategoryIndependentWorkers] DEFAULT ((0)) NOT NULL,
    [RetentionConcept383Id]          INT           NULL,
    [RetentionMainAccount383Id]      INT           NULL,
    [RetentionConceptId]             INT           NULL,
    CONSTRAINT [PK_AccountPayableConceptNotes__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountPayableConceptNotes_RetentionConcept383Id] FOREIGN KEY ([RetentionConcept383Id]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_AccountPayableConceptNotes_RetentionConceptId] FOREIGN KEY ([RetentionConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_AccountPayableConceptNotes_RetentionMainAccount383Id] FOREIGN KEY ([RetentionMainAccount383Id]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PaymentsNoteConcept_PUC] FOREIGN KEY ([IdAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [Payments].[AccountPayableConceptNotes] NOCHECK CONSTRAINT [FK_AccountPayableConceptNotes_RetentionConcept383Id];


GO
ALTER TABLE [Payments].[AccountPayableConceptNotes] NOCHECK CONSTRAINT [FK_AccountPayableConceptNotes_RetentionConceptId];


GO
ALTER TABLE [Payments].[AccountPayableConceptNotes] NOCHECK CONSTRAINT [FK_AccountPayableConceptNotes_RetentionMainAccount383Id];


GO
ALTER TABLE [Payments].[AccountPayableConceptNotes] NOCHECK CONSTRAINT [FK_PaymentsNoteConcept_PUC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gestionar impuestos', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'ManageTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gestionar retención', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'ManageRetention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de concepto  1 - General  2 - Especifico    Cuando es general no se pide la cuenta contable cuando se esten creando el concepto de nota, pero si se pide cuando se este creando la nota    Cuando es especifico se solicita la cuenta contable cuando se crea el concepto y solo se muestra como ReadOnly cuando se esta creando la nota', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'AccountPayableConceptNotes', @level2type = N'COLUMN', @level2name = N'ConceptType';


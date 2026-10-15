CREATE TABLE [Treasury].[Cards] (
    [Id]                            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                          VARCHAR (20)  NOT NULL,
    [Name]                          VARCHAR (100) NOT NULL,
    [IdThirdParty]                  INT           NOT NULL,
    [GetCostCenter]                 TINYINT       CONSTRAINT [DF_Cards_GetCostCenter] DEFAULT ((1)) NOT NULL,
    [IdCashReceiptConceptCommision] INT           NOT NULL,
    [CommisionCostCenterId]         INT           NULL,
    [IdRetentionConceptCommision]   INT           NOT NULL,
    [IdCashReceiptConceptRTF]       INT           NOT NULL,
    [RTFCostCenterId]               INT           NULL,
    [IdRetentionConceptRTF]         INT           NOT NULL,
    [IdCashReceiptConceptICA]       INT           NOT NULL,
    [ICACostCenterId]               INT           NULL,
    [IdRetentionConceptICA]         INT           NOT NULL,
    [Status]                        BIT           CONSTRAINT [DF_Card_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]                  VARCHAR (20)  NOT NULL,
    [CreationDate]                  DATETIME      NOT NULL,
    [ModificationUser]              VARCHAR (20)  NULL,
    [ModificationDate]              DATETIME      NULL,
    [TimeStamp]                     ROWVERSION    NOT NULL,
    CONSTRAINT [PK_Cards__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Card_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_Cards_CashReceiptConcepts_Commision] FOREIGN KEY ([IdCashReceiptConceptCommision]) REFERENCES [Treasury].[CashReceiptConcepts] ([Id]),
    CONSTRAINT [FK_Cards_CashReceiptConcepts_ICA] FOREIGN KEY ([IdCashReceiptConceptICA]) REFERENCES [Treasury].[CashReceiptConcepts] ([Id]),
    CONSTRAINT [FK_Cards_CashReceiptConcepts_RTF] FOREIGN KEY ([IdCashReceiptConceptRTF]) REFERENCES [Treasury].[CashReceiptConcepts] ([Id]),
    CONSTRAINT [FK_Cards_RetentionConcepts_Commision] FOREIGN KEY ([IdRetentionConceptCommision]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_Cards_RetentionConcepts_ICA] FOREIGN KEY ([IdRetentionConceptICA]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_Cards_RetentionConcepts_RTF] FOREIGN KEY ([IdRetentionConceptRTF]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id])
);


GO
ALTER TABLE [Treasury].[Cards] NOCHECK CONSTRAINT [FK_Card_ThirdParty];


GO
ALTER TABLE [Treasury].[Cards] NOCHECK CONSTRAINT [FK_Cards_CashReceiptConcepts_Commision];


GO
ALTER TABLE [Treasury].[Cards] NOCHECK CONSTRAINT [FK_Cards_CashReceiptConcepts_ICA];


GO
ALTER TABLE [Treasury].[Cards] NOCHECK CONSTRAINT [FK_Cards_CashReceiptConcepts_RTF];


GO
ALTER TABLE [Treasury].[Cards] NOCHECK CONSTRAINT [FK_Cards_RetentionConcepts_Commision];


GO
ALTER TABLE [Treasury].[Cards] NOCHECK CONSTRAINT [FK_Cards_RetentionConcepts_ICA];


GO
ALTER TABLE [Treasury].[Cards] NOCHECK CONSTRAINT [FK_Cards_RetentionConcepts_RTF];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica de donde se obtiene el centro de costo   1 - Centro de costo Unico  2 - Centro de costo Por Unidad Operativa', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'GetCostCenter';


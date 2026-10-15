CREATE TABLE [Payments].[LoadMassiveAccountPayableDetail] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LoadMassiveAccountPayableId] INT             NOT NULL,
    [AccountPayableConceptId]     INT             NOT NULL,
    [ThirdPartyId]                INT             NOT NULL,
    [CostCenterId]                INT             NULL,
    [Detail]                      VARCHAR (500)   NULL,
    [Nature]                      TINYINT         NOT NULL,
    [Value]                       DECIMAL (18, 2) NOT NULL,
    [RetentionConceptId]          INT             NULL,
    [BaseValue]                   DECIMAL (18, 2) CONSTRAINT [DF_LoadMassiveAccountPayableDetail_BaseValue] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Payments_LoadMassiveAccountPayableDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LoadMassiveAccountPayableDetail_AccountPayableConcept] FOREIGN KEY ([AccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_LoadMassiveAccountPayableDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_LoadMassiveAccountPayableDetail_LoadMassiveAccountPayable] FOREIGN KEY ([LoadMassiveAccountPayableId]) REFERENCES [Payments].[LoadMassiveAccountPayable] ([Id]),
    CONSTRAINT [FK_LoadMassiveAccountPayableDetail_RetentionConcept] FOREIGN KEY ([RetentionConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_LoadMassiveAccountPayableDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Payments].[LoadMassiveAccountPayableDetail] NOCHECK CONSTRAINT [FK_LoadMassiveAccountPayableDetail_AccountPayableConcept];


GO
ALTER TABLE [Payments].[LoadMassiveAccountPayableDetail] NOCHECK CONSTRAINT [FK_LoadMassiveAccountPayableDetail_CostCenter];


GO
ALTER TABLE [Payments].[LoadMassiveAccountPayableDetail] NOCHECK CONSTRAINT [FK_LoadMassiveAccountPayableDetail_LoadMassiveAccountPayable];


GO
ALTER TABLE [Payments].[LoadMassiveAccountPayableDetail] NOCHECK CONSTRAINT [FK_LoadMassiveAccountPayableDetail_RetentionConcept];


GO
ALTER TABLE [Payments].[LoadMassiveAccountPayableDetail] NOCHECK CONSTRAINT [FK_LoadMassiveAccountPayableDetail_ThirdParty];


CREATE TABLE [MedicalFees].[SettingMedicalFees] (
    [Id]                      INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AccountPayableConceptId] INT          NOT NULL,
    [JournalVoucherTypeId]    INT          NOT NULL,
    [CreationUser]            VARCHAR (20) NOT NULL,
    [CreationDate]            DATETIME     NOT NULL,
    [ModificationUser]        VARCHAR (20) NULL,
    [ModificationDate]        DATETIME     NULL,
    CONSTRAINT [PK_SettingMedicalFees] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SettingMedicalFees_AccountPayableConcepts] FOREIGN KEY ([AccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_SettingMedicalFees_JournalVoucherTypes] FOREIGN KEY ([JournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id])
);


GO
ALTER TABLE [MedicalFees].[SettingMedicalFees] NOCHECK CONSTRAINT [FK_SettingMedicalFees_AccountPayableConcepts];


GO
ALTER TABLE [MedicalFees].[SettingMedicalFees] NOCHECK CONSTRAINT [FK_SettingMedicalFees_JournalVoucherTypes];


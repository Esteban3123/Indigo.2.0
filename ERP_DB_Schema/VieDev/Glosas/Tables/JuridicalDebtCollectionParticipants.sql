CREATE TABLE [Glosas].[JuridicalDebtCollectionParticipants] (
    [Id]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ConciliationCId] INT           NOT NULL,
    [Type]            CHAR (1)      NOT NULL,
    [FullName]        VARCHAR (200) NOT NULL,
    [Position]        VARCHAR (200) NOT NULL,
    [TimeStamp]       ROWVERSION    NOT NULL,
    CONSTRAINT [PK_JuridicalDebtCollectionParticipants] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_JuridicalDebtCollectionParticipants_ConciliationC] FOREIGN KEY ([ConciliationCId]) REFERENCES [Glosas].[ConciliationC] ([Id])
);


GO
ALTER TABLE [Glosas].[JuridicalDebtCollectionParticipants] NOCHECK CONSTRAINT [FK_JuridicalDebtCollectionParticipants_ConciliationC];


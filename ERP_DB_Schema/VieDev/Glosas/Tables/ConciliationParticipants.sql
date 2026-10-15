CREATE TABLE [Glosas].[ConciliationParticipants] (
    [Id]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ConciliationCId] INT           NOT NULL,
    [Type]            CHAR (1)      NOT NULL,
    [FullName]        VARCHAR (200) NOT NULL,
    [Position]        VARCHAR (200) NOT NULL,
    [TimeStamp]       ROWVERSION    NOT NULL,
    CONSTRAINT [PK_ConciliationParticipants] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConciliationParticipants_ConciliationC] FOREIGN KEY ([ConciliationCId]) REFERENCES [Glosas].[ConciliationC] ([Id])
);


GO
ALTER TABLE [Glosas].[ConciliationParticipants] NOCHECK CONSTRAINT [FK_ConciliationParticipants_ConciliationC];


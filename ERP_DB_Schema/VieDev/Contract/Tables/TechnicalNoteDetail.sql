CREATE TABLE [Contract].[TechnicalNoteDetail] (
    [Id]              INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TechnicalNoteId] INT NOT NULL,
    [GrouperId]       INT NOT NULL,
    CONSTRAINT [PK_TechnicalNoteDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TechnicalNoteDetail_Groupers] FOREIGN KEY ([GrouperId]) REFERENCES [Contract].[Groupers] ([Id]),
    CONSTRAINT [FK_TechnicalNoteDetail_TechnicalNote] FOREIGN KEY ([TechnicalNoteId]) REFERENCES [Contract].[TechnicalNote] ([Id])
);


GO
ALTER TABLE [Contract].[TechnicalNoteDetail] NOCHECK CONSTRAINT [FK_TechnicalNoteDetail_Groupers];


GO
ALTER TABLE [Contract].[TechnicalNoteDetail] NOCHECK CONSTRAINT [FK_TechnicalNoteDetail_TechnicalNote];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_TechnicalNoteDetail]
    ON [Contract].[TechnicalNoteDetail]([TechnicalNoteId] ASC, [GrouperId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Agrupador', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'TechnicalNoteDetail', @level2type = N'COLUMN', @level2name = N'GrouperId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la nota tecnica', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'TechnicalNoteDetail', @level2type = N'COLUMN', @level2name = N'TechnicalNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'TechnicalNoteDetail', @level2type = N'COLUMN', @level2name = N'Id';


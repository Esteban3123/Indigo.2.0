CREATE TABLE [dbo].[EXAVALORES] (
    [ID]            INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCHISPACA]   INT            NOT NULL,
    [IDEXAGRUPO]    INT            NOT NULL,
    [IDEXAVARIABLE] INT            NOT NULL,
    [VALOR]         VARCHAR (5000) NOT NULL,
    [IDITEMLISTA]   INT            NULL,
    [VALOROPCION]   TINYINT        NULL,
    CONSTRAINT [PK_EXAVALORES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_EXAVALORES_EXAGRUPO] FOREIGN KEY ([IDEXAGRUPO]) REFERENCES [dbo].[EXAGRUPO] ([ID]),
    CONSTRAINT [FK_EXAVALORES_EXAVALORES] FOREIGN KEY ([ID]) REFERENCES [dbo].[EXAVALORES] ([ID]),
    CONSTRAINT [FK_EXAVALORES_EXAVARIABLES] FOREIGN KEY ([IDEXAVARIABLE]) REFERENCES [dbo].[EXAVARIABLES] ([ID]),
    CONSTRAINT [FK_EXAVALORES_EXAVARIABLESL] FOREIGN KEY ([IDITEMLISTA]) REFERENCES [dbo].[EXAVARIABLESL] ([ID]),
    CONSTRAINT [FK_EXAVALORES_HCHISPACA] FOREIGN KEY ([IDHCHISPACA]) REFERENCES [dbo].[HCHISPACA] ([ID])
);


GO
ALTER TABLE [dbo].[EXAVALORES] NOCHECK CONSTRAINT [FK_EXAVALORES_EXAGRUPO];


GO
ALTER TABLE [dbo].[EXAVALORES] NOCHECK CONSTRAINT [FK_EXAVALORES_EXAVALORES];


GO
ALTER TABLE [dbo].[EXAVALORES] NOCHECK CONSTRAINT [FK_EXAVALORES_EXAVARIABLES];


GO
ALTER TABLE [dbo].[EXAVALORES] NOCHECK CONSTRAINT [FK_EXAVALORES_EXAVARIABLESL];


GO
ALTER TABLE [dbo].[EXAVALORES] NOCHECK CONSTRAINT [FK_EXAVALORES_HCHISPACA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1= Normal      2= Anormal     3=No valorado  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORES', @level2type = N'COLUMN', @level2name = N'VALOROPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del control o valor del control IDEXAVARIABLE  ejemplo:-- checkedit.editvalue', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORES', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'guarda el tag del control   CheckEdit    MemoEdit    SpinEdit    GridLookUpEdit    DateEdit', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORES', @level2type = N'COLUMN', @level2name = N'IDEXAVARIABLE';


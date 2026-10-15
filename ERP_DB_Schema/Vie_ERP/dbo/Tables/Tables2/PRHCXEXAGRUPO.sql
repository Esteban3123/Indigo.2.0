CREATE TABLE [dbo].[PRHCXEXAGRUPO] (
    [ID]         INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDMODELOHC] INT NOT NULL,
    [IDEXAGRUPO] INT NOT NULL,
    CONSTRAINT [PK_PRHCXEXAGRUPO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRHCXEXAGRUPO_EXAGRUPO] FOREIGN KEY ([IDEXAGRUPO]) REFERENCES [dbo].[EXAGRUPO] ([ID]),
    CONSTRAINT [FK_PRHCXEXAGRUPO_PRMODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID])
);


GO
ALTER TABLE [dbo].[PRHCXEXAGRUPO] NOCHECK CONSTRAINT [FK_PRHCXEXAGRUPO_EXAGRUPO];


GO
ALTER TABLE [dbo].[PRHCXEXAGRUPO] NOCHECK CONSTRAINT [FK_PRHCXEXAGRUPO_PRMODELOHC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo de examen físico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAGRUPO', @level2type = N'COLUMN', @level2name = N'IDEXAGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID modelo de Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAGRUPO', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAGRUPO', @level2type = N'COLUMN', @level2name = N'ID';


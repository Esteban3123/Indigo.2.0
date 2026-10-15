CREATE TABLE [dbo].[PRHCXRSGRUPO] (
    [ID]         INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDMODELOHC] INT NOT NULL,
    [IDRSGRUPO]  INT NOT NULL,
    CONSTRAINT [PK_PRHCXRSGRUPO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRHCXRSGRUPO_PRMODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID]),
    CONSTRAINT [FK_PRHCXRSGRUPO_RSGRUPO] FOREIGN KEY ([IDRSGRUPO]) REFERENCES [dbo].[RSGRUPO] ([ID])
);


GO
ALTER TABLE [dbo].[PRHCXRSGRUPO] NOCHECK CONSTRAINT [FK_PRHCXRSGRUPO_PRMODELOHC];


GO
ALTER TABLE [dbo].[PRHCXRSGRUPO] NOCHECK CONSTRAINT [FK_PRHCXRSGRUPO_RSGRUPO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del grupo de revision por Sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSGRUPO', @level2type = N'COLUMN', @level2name = N'IDRSGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID modelo de Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSGRUPO', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSGRUPO', @level2type = N'COLUMN', @level2name = N'ID';


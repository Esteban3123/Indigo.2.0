CREATE TABLE [dbo].[PRHCXOTGRUPO] (
    [ID]         INT IDENTITY (1, 1) NOT NULL,
    [IDMODELOHC] INT NOT NULL,
    [IDOTGRUPO]  INT NOT NULL,
    CONSTRAINT [PK_PRHCXOTGRUPO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRHCXOTGRUPO_OTGRUPO] FOREIGN KEY ([IDOTGRUPO]) REFERENCES [dbo].[OTGRUPO] ([ID]),
    CONSTRAINT [FK_PRHCXOTGRUPO_PRMODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID])
);


GO
ALTER TABLE [dbo].[PRHCXOTGRUPO] NOCHECK CONSTRAINT [FK_PRHCXOTGRUPO_OTGRUPO];


GO
ALTER TABLE [dbo].[PRHCXOTGRUPO] NOCHECK CONSTRAINT [FK_PRHCXOTGRUPO_PRMODELOHC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del grupo de revision por Sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTGRUPO', @level2type = N'COLUMN', @level2name = N'IDOTGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID modelo de Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTGRUPO', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXOTGRUPO', @level2type = N'COLUMN', @level2name = N'ID';


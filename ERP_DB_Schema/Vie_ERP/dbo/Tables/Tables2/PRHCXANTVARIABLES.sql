CREATE TABLE [dbo].[PRHCXANTVARIABLES] (
    [ID]             INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDMODELOHC]     INT NOT NULL,
    [IDANTVARIABLES] INT NOT NULL,
    [OBLIGATORIO]    BIT NOT NULL,
    [ESTADO]         BIT NOT NULL,
    CONSTRAINT [PK_PRHCXANTVARIABLES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRHCXANTVARIABLES_ANTVARIABLES] FOREIGN KEY ([IDANTVARIABLES]) REFERENCES [dbo].[ANTVARIABLES] ([ID]),
    CONSTRAINT [FK_PRHCXANTVARIABLES_PRMODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID])
);


GO
ALTER TABLE [dbo].[PRHCXANTVARIABLES] NOCHECK CONSTRAINT [FK_PRHCXANTVARIABLES_ANTVARIABLES];


GO
ALTER TABLE [dbo].[PRHCXANTVARIABLES] NOCHECK CONSTRAINT [FK_PRHCXANTVARIABLES_PRMODELOHC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXANTVARIABLES', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obligatorio (true or false)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXANTVARIABLES', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relacion con la tabla ANTVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXANTVARIABLES', @level2type = N'COLUMN', @level2name = N'IDANTVARIABLES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relacion con la tabla PRMODELOHC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXANTVARIABLES', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXANTVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';


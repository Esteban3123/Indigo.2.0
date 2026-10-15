CREATE TABLE [dbo].[PRHCXEXAVARIABLES] (
    [ID]             INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDMODELOHC]     INT NOT NULL,
    [IDEXAVARIABLES] INT NOT NULL,
    [OBLIGATORIO]    BIT NOT NULL,
    [ESTADO]         BIT NOT NULL,
    CONSTRAINT [PK_PRHCXEXAVARIABLES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRHCXEXAVARIABLES_ANTVARIABLES] FOREIGN KEY ([IDEXAVARIABLES]) REFERENCES [dbo].[EXAVARIABLES] ([ID]),
    CONSTRAINT [FK_PRHCXEXAVARIABLES_PRMODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID])
);


GO
ALTER TABLE [dbo].[PRHCXEXAVARIABLES] NOCHECK CONSTRAINT [FK_PRHCXEXAVARIABLES_ANTVARIABLES];


GO
ALTER TABLE [dbo].[PRHCXEXAVARIABLES] NOCHECK CONSTRAINT [FK_PRHCXEXAVARIABLES_PRMODELOHC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Estado 1 = true   0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAVARIABLES', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Obligatorio  1 =  Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAVARIABLES', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Id de variables examen fisico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAVARIABLES', @level2type = N'COLUMN', @level2name = N'IDEXAVARIABLES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id modelo de historia clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAVARIABLES', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';


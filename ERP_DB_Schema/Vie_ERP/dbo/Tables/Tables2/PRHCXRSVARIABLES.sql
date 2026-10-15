CREATE TABLE [dbo].[PRHCXRSVARIABLES] (
    [ID]            INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDMODELOHC]    INT NOT NULL,
    [IDRSVARIABLES] INT NOT NULL,
    [OBLIGATORIO]   BIT NOT NULL,
    [ESTADO]        BIT NOT NULL,
    CONSTRAINT [PK_PRHCXRSVARIABLES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRHCXRSVARIABLES_PRMODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID]),
    CONSTRAINT [FK_PRHCXRSVARIABLES_RSVARIABLES] FOREIGN KEY ([IDRSVARIABLES]) REFERENCES [dbo].[RSVARIABLES] ([ID])
);


GO
ALTER TABLE [dbo].[PRHCXRSVARIABLES] NOCHECK CONSTRAINT [FK_PRHCXRSVARIABLES_PRMODELOHC];


GO
ALTER TABLE [dbo].[PRHCXRSVARIABLES] NOCHECK CONSTRAINT [FK_PRHCXRSVARIABLES_RSVARIABLES];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el estado   1 = si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSVARIABLES', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda obligatorios  1 = si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSVARIABLES', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id variables revision de sistemas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSVARIABLES', @level2type = N'COLUMN', @level2name = N'IDRSVARIABLES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id modelo de historia clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSVARIABLES', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXRSVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';


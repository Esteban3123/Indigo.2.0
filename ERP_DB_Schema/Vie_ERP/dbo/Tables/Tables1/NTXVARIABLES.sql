CREATE TABLE [dbo].[NTXVARIABLES] (
    [ID]                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDNTADMINISTRATIVAS] INT NOT NULL,
    [IDNTVARIABLES]       INT NOT NULL,
    [OBLIGATORIO]         BIT NOT NULL,
    [ESTADO]              BIT NOT NULL,
    CONSTRAINT [PK_NTXVARIABLES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_NTXVARIABLES_NTADMINISTRATIVAS] FOREIGN KEY ([IDNTADMINISTRATIVAS]) REFERENCES [dbo].[NTADMINISTRATIVAS] ([ID]),
    CONSTRAINT [FK_NTXVARIABLES_NTVARIABLES] FOREIGN KEY ([IDNTVARIABLES]) REFERENCES [dbo].[NTVARIABLES] ([ID])
);


GO
ALTER TABLE [dbo].[NTXVARIABLES] NOCHECK CONSTRAINT [FK_NTXVARIABLES_NTADMINISTRATIVAS];


GO
ALTER TABLE [dbo].[NTXVARIABLES] NOCHECK CONSTRAINT [FK_NTXVARIABLES_NTVARIABLES];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXVARIABLES', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Es obligatorio (0. no, 1. si)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXVARIABLES', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relación con la tabla NTVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXVARIABLES', @level2type = N'COLUMN', @level2name = N'IDNTVARIABLES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relación con la tabla NTADMINISTRATIVAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXVARIABLES', @level2type = N'COLUMN', @level2name = N'IDNTADMINISTRATIVAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';


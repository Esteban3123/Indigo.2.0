CREATE TABLE [dbo].[EXAVARIABLESD] (
    [ID]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPADRE]   INT           NOT NULL,
    [IDHIJO]    INT           NOT NULL,
    [EXPRESION] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_EXAVARIABLESD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_EXAVARIABLESD_EXAVARIABLES] FOREIGN KEY ([IDPADRE]) REFERENCES [dbo].[EXAVARIABLES] ([ID]),
    CONSTRAINT [FK_EXAVARIABLESD_EXAVARIABLES1] FOREIGN KEY ([IDHIJO]) REFERENCES [dbo].[EXAVARIABLES] ([ID])
);


GO
ALTER TABLE [dbo].[EXAVARIABLESD] NOCHECK CONSTRAINT [FK_EXAVARIABLESD_EXAVARIABLES];


GO
ALTER TABLE [dbo].[EXAVARIABLESD] NOCHECK CONSTRAINT [FK_EXAVARIABLESD_EXAVARIABLES1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Expresion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLESD', @level2type = N'COLUMN', @level2name = N'EXPRESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID relacionado con ID de tabla EXAVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDHIJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID relacionado con ID de tabla EXAVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDPADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVARIABLESD', @level2type = N'COLUMN', @level2name = N'ID';


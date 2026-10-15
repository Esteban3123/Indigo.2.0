CREATE TABLE [dbo].[RSVARIABLESD] (
    [ID]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPADRE]   INT           NOT NULL,
    [IDHIJO]    INT           NOT NULL,
    [EXPRESION] VARCHAR (250) NOT NULL,
    CONSTRAINT [PK_RSVARIABLESD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RSVARIABLESD_RSVARIABLES] FOREIGN KEY ([IDPADRE]) REFERENCES [dbo].[RSVARIABLES] ([ID]),
    CONSTRAINT [FK_RSVARIABLESD_RSVARIABLES1] FOREIGN KEY ([IDHIJO]) REFERENCES [dbo].[RSVARIABLES] ([ID])
);


GO
ALTER TABLE [dbo].[RSVARIABLESD] NOCHECK CONSTRAINT [FK_RSVARIABLESD_RSVARIABLES];


GO
ALTER TABLE [dbo].[RSVARIABLESD] NOCHECK CONSTRAINT [FK_RSVARIABLESD_RSVARIABLES1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Expresion a validar ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESD', @level2type = N'COLUMN', @level2name = N'EXPRESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Identidad del hijo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDHIJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Identidad del padre ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDPADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVARIABLESD', @level2type = N'COLUMN', @level2name = N'ID';


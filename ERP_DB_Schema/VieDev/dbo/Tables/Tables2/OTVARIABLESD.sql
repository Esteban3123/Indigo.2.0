CREATE TABLE [dbo].[OTVARIABLESD] (
    [ID]        INT           IDENTITY (1, 1) NOT NULL,
    [IDPADRE]   INT           NOT NULL,
    [IDHIJO]    INT           NOT NULL,
    [EXPRESION] VARCHAR (250) NOT NULL,
    CONSTRAINT [PK_OTVARIABLESD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_OTVARIABLESD_OTVARIABLES] FOREIGN KEY ([IDPADRE]) REFERENCES [dbo].[OTVARIABLES] ([ID]),
    CONSTRAINT [FK_OTVARIABLESD_OTVARIABLES1] FOREIGN KEY ([IDHIJO]) REFERENCES [dbo].[OTVARIABLES] ([ID])
);


GO
ALTER TABLE [dbo].[OTVARIABLESD] NOCHECK CONSTRAINT [FK_OTVARIABLESD_OTVARIABLES];


GO
ALTER TABLE [dbo].[OTVARIABLESD] NOCHECK CONSTRAINT [FK_OTVARIABLESD_OTVARIABLES1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Expresion a validar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLESD', @level2type = N'COLUMN', @level2name = N'EXPRESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla OTVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDHIJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla OTVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDPADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVARIABLESD', @level2type = N'COLUMN', @level2name = N'ID';


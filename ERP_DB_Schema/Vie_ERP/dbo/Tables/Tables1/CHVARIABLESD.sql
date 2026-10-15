CREATE TABLE [dbo].[CHVARIABLESD] (
    [ID]        INT           IDENTITY (1, 1) NOT NULL,
    [IDPADRE]   INT           NOT NULL,
    [IDHIJO]    INT           NOT NULL,
    [EXPRESION] VARCHAR (250) NOT NULL,
    CONSTRAINT [PK_CHVARIABLESD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CHVARIABLESD_CHVARIABLES] FOREIGN KEY ([IDPADRE]) REFERENCES [dbo].[CHVARIABLES] ([ID]),
    CONSTRAINT [FK_CHVARIABLESD_CHVARIABLES1] FOREIGN KEY ([IDHIJO]) REFERENCES [dbo].[CHVARIABLES] ([ID])
);


GO
ALTER TABLE [dbo].[CHVARIABLESD] NOCHECK CONSTRAINT [FK_CHVARIABLESD_CHVARIABLES];


GO
ALTER TABLE [dbo].[CHVARIABLESD] NOCHECK CONSTRAINT [FK_CHVARIABLESD_CHVARIABLES1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Expresiones de variables de lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLESD', @level2type = N'COLUMN', @level2name = N'EXPRESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla CHVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDHIJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla CHVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDPADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVARIABLESD', @level2type = N'COLUMN', @level2name = N'ID';


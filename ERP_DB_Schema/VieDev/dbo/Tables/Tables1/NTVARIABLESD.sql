CREATE TABLE [dbo].[NTVARIABLESD] (
    [ID]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPADRE]   INT           NOT NULL,
    [IDHIJO]    INT           NOT NULL,
    [EXPRESION] VARCHAR (250) NOT NULL,
    CONSTRAINT [PK_NTVARIABLESD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_NTVARIABLESD_NTVARIABLES] FOREIGN KEY ([IDPADRE]) REFERENCES [dbo].[NTVARIABLES] ([ID]),
    CONSTRAINT [FK_NTVARIABLESD_NTVARIABLES1] FOREIGN KEY ([IDHIJO]) REFERENCES [dbo].[NTVARIABLES] ([ID])
);


GO
ALTER TABLE [dbo].[NTVARIABLESD] NOCHECK CONSTRAINT [FK_NTVARIABLESD_NTVARIABLES];


GO
ALTER TABLE [dbo].[NTVARIABLESD] NOCHECK CONSTRAINT [FK_NTVARIABLESD_NTVARIABLES1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la expresión a validar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTVARIABLESD', @level2type = N'COLUMN', @level2name = N'EXPRESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla NTVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDHIJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relación con la tabla NTVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTVARIABLESD', @level2type = N'COLUMN', @level2name = N'IDPADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTVARIABLESD', @level2type = N'COLUMN', @level2name = N'ID';


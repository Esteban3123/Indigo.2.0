CREATE TABLE [dbo].[HCREFESPECIALIDADES] (
    [ID]          INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCREFCONT] INT      NOT NULL,
    [CODESPECI]   CHAR (3) NOT NULL,
    CONSTRAINT [PK_HCREFESPECIALIDADES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCREFESPECIALIDADES_HCREFCONT] FOREIGN KEY ([IDHCREFCONT]) REFERENCES [dbo].[HCREFCONT] ([AUTO]),
    CONSTRAINT [FK_HCREFESPECIALIDADES_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI])
);


GO
ALTER TABLE [dbo].[HCREFESPECIALIDADES] NOCHECK CONSTRAINT [FK_HCREFESPECIALIDADES_HCREFCONT];


GO
ALTER TABLE [dbo].[HCREFESPECIALIDADES] NOCHECK CONSTRAINT [FK_HCREFESPECIALIDADES_INESPECIA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFESPECIALIDADES', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla de referencia HCREFCONT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFESPECIALIDADES', @level2type = N'COLUMN', @level2name = N'IDHCREFCONT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFESPECIALIDADES', @level2type = N'COLUMN', @level2name = N'ID';


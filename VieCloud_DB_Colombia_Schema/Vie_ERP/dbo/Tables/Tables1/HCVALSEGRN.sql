CREATE TABLE [dbo].[HCVALSEGRN] (
    [Id]        INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMCONSEC] INT NOT NULL,
    [FOLIOMAMA] INT NOT NULL,
    [FOLIOHIJO] INT NOT NULL,
    CONSTRAINT [PK_HCVALSEGRN] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HCVALSEGRN_HCRECINAC] FOREIGN KEY ([NUMCONSEC]) REFERENCES [dbo].[HCRECINAC] ([NUMCONSEC])
);


GO
ALTER TABLE [dbo].[HCVALSEGRN] NOCHECK CONSTRAINT [FK_HCVALSEGRN_HCRECINAC];


GO
CREATE NONCLUSTERED INDEX [IX_HCVALSEGRN_FOLIOMAMA_NUMCONSEC]
    ON [dbo].[HCVALSEGRN]([FOLIOMAMA] ASC, [NUMCONSEC] ASC)
    INCLUDE([FOLIOHIJO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Folio o número de historia clínica del recién nacido; identificador único del neonato vinculado al registro de nacimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALSEGRN', @level2type = N'COLUMN', @level2name = N'FOLIOHIJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Folio del recien nacido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALSEGRN', @level2type = N'COLUMN', @level2name = N'FOLIOHIJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALSEGRN', @level2type = N'COLUMN', @level2name = N'FOLIOHIJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Folio o número de historia clínica de la madre; identificador único de la gestante vinculado al registro perinatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALSEGRN', @level2type = N'COLUMN', @level2name = N'FOLIOMAMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Folio de la madre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALSEGRN', @level2type = N'COLUMN', @level2name = N'FOLIOMAMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALSEGRN', @level2type = N'COLUMN', @level2name = N'FOLIOMAMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo de referencia cruzada hacia tabla HCRECINAC; llave foránea que vincula el registro de atención materna con el seguimiento neonatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALSEGRN', @level2type = N'COLUMN', @level2name = N'NUMCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALSEGRN', @level2type = N'COLUMN', @level2name = N'NUMCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALSEGRN', @level2type = N'COLUMN', @level2name = N'NUMCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único primario (INT IDENTITY) de la tabla HCVALSEGRN; clave principal del registro de seguimiento materno-neonatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALSEGRN', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALSEGRN', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALSEGRN', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de validación de seguimiento en historia clínica, donde cada fila relaciona un folio padre (registro principal) con un folio hijo (registro derivado o de detalle) dentro de un consecutivo de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALSEGRN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALSEGRN';

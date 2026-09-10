CREATE TABLE [dbo].[ODOPARTRACUPSD] (
    [ID]          INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDODOPARTRA] INT       NOT NULL,
    [CODSERIPS]   CHAR (20) NOT NULL,
    CONSTRAINT [PK_ODOPARTRACUPSD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODOPARTRACUPSD_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_ODOPARTRACUPSD_ODOPARTRA] FOREIGN KEY ([IDODOPARTRA]) REFERENCES [dbo].[ODOPARTRA] ([CONSECTRA])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS/CAEC del servicio odontológico prestado. Identificador estándar de procedimientos, servicios y productos en salud (RIPS). Referencia a tabla INCUPSIPS para clasificación de servicios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRACUPSD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRACUPSD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRACUPSD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la parte de tratamiento odontológico relacionada. Llave foránea que vincula a tabla ODOPARTRA (CONSECTRA) para trazar el tratamiento dental completo del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRACUPSD', @level2type = N'COLUMN', @level2name = N'IDODOPARTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRACUPSD', @level2type = N'COLUMN', @level2name = N'IDODOPARTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRACUPSD', @level2type = N'COLUMN', @level2name = N'IDODOPARTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo único de la relación entre tratamiento odontológico y códigos CUPS. Identificador primario (INT IDENTITY) para registro de servicios facturables en odontología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRACUPSD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRACUPSD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRACUPSD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de servicios (códigos CUPS/IPS) asociados a cada parámetro de radicación de odontología. Registra qué procedimientos o servicios odontológicos están vinculados a una configuración o regla de radicación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRACUPSD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRACUPSD';

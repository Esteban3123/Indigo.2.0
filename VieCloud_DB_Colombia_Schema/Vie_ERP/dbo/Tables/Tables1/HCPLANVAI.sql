CREATE TABLE [dbo].[HCPLANVAI] (
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IDPLAVACU] CHAR (4)                                                                         NOT NULL,
    [FECAPLVAC] DATETIME                                                                         NULL,
    [OBSVACUNA] CHAR (200)                                                                       NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_HCPLANVAI] PRIMARY KEY CLUSTERED ([IPCODPACI] ASC, [IDPLAVACU] ASC),
    CONSTRAINT [FK_HCPLANVAI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCPLANVAI_INPLANVAC] FOREIGN KEY ([IDPLAVACU]) REFERENCES [dbo].[INPLANVAC] ([IDPLAVACU])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCPLANVAI].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de auditoría y control; campo de auditoría para trazabilidad de registros (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CAmpo Auditoria  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones, notas clínicas o efectos adversos registrados en la aplicación de vacunas (VARCHAR 200)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAI', @level2type = N'COLUMN', @level2name = N'OBSVACUNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion de Vacunas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAI', @level2type = N'COLUMN', @level2name = N'OBSVACUNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAI', @level2type = N'COLUMN', @level2name = N'OBSVACUNA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de aplicación de la vacuna al paciente; registro opcional del evento de vacunación (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAI', @level2type = N'COLUMN', @level2name = N'FECAPLVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la que se realizo la aplicacion Opcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAI', @level2type = N'COLUMN', @level2name = N'FECAPLVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAI', @level2type = N'COLUMN', @level2name = N'FECAPLVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del Plan de Vacunas; clave foránea vinculada a catálogo INPLANVAC (CHAR 4)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAI', @level2type = N'COLUMN', @level2name = N'IDPLAVACU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Plan de Vacunas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAI', @level2type = N'COLUMN', @level2name = N'IDPLAVACU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAI', @level2type = N'COLUMN', @level2name = N'IDPLAVACU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente, cédula o documento de identificación; PII ofuscado; vinculación a registro INPACIENT (VARCHAR 25)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro del plan de vacunación aplicado a cada paciente en la historia clínica, incluyendo las vacunas administradas, fechas de aplicación y observaciones clínicas relacionadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANVAI';

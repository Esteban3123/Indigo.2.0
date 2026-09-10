CREATE TABLE [dbo].[HCFIRMFOL] (
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMEFOLIO] CHAR (10)                                                                        NOT NULL,
    [CODPROCRE] CHAR (20)                                                                        NOT NULL,
    [CODPROFIR] CHAR (20)                                                                        NOT NULL,
    [FECFIRFOL] DATETIME                                                                         NULL,
    CONSTRAINT [PK_HCFIRMFOL] PRIMARY KEY CLUSTERED ([IPCODPACI] ASC, [NUMEFOLIO] ASC),
    CONSTRAINT [FK_HCFIRMFOL_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCFIRMFOL].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de firma digital del folio de historia clínica. Timestamp del momento en que el profesional de la salud autenticó electrónicamente el documento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFIRMFOL', @level2type = N'COLUMN', @level2name = N'FECFIRFOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Firma Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFIRMFOL', @level2type = N'COLUMN', @level2name = N'FECFIRFOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFIRMFOL', @level2type = N'COLUMN', @level2name = N'FECFIRFOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del profesional de la salud que firma. Identificador del médico, enfermero, especialista o terapeuta que autoriza y valida el folio de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFIRMFOL', @level2type = N'COLUMN', @level2name = N'CODPROFIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFIRMFOL', @level2type = N'COLUMN', @level2name = N'CODPROFIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFIRMFOL', @level2type = N'COLUMN', @level2name = N'CODPROFIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del profesional creador. Identificador del profesional de la salud que originalmente generó o escribió el folio de la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFIRMFOL', @level2type = N'COLUMN', @level2name = N'CODPROCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFIRMFOL', @level2type = N'COLUMN', @level2name = N'CODPROCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFIRMFOL', @level2type = N'COLUMN', @level2name = N'CODPROCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial único de folio por paciente. Identificador correlativo de cada página, documento o nota clínica dentro de la historia de un paciente específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFIRMFOL', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio Consecutivo Unico por Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFIRMFOL', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFIRMFOL', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de identificación del paciente. Equivalente a cédula, documento de identidad o número de afiliación del paciente en el sistema de salud (PII - Información Protegida).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFIRMFOL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFIRMFOL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFIRMFOL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de firmas de folios en la historia clínica. Guarda qué profesional firmó cada folio de un proceso clínico, para qué paciente y cuándo fue firmado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFIRMFOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFIRMFOL';

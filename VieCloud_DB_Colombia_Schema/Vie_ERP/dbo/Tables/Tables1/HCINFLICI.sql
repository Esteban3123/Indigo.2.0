CREATE TABLE [dbo].[HCINFLICI] (
    [CODCONCEC] NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS] CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NOT NULL,
    [FECHAORDE] DATETIME                                                                         NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [TIPMEZLIQ] CHAR (1)                                                                         NOT NULL,
    [METAPLMED] CHAR (1)                                                                         NOT NULL,
    CONSTRAINT [PK_HCINFLICI] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCINFLICI_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCINFLICI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCINFLICI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCINFLICI_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCINFLICI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINFLICI].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINFLICI].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Método de aplicación de medicamento en órdenes de infusión/mezcla: 1=Bolo mezcla, 2=Infusión mezcla, 3=Bolo medicamento mezcla, 4=Infusión líquido, 5=Bolo medicamento líquido. Varchar(1), PII no-ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'METAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Metodo de Aplicacion de Medicamento  1: Bolo Mezcla  2: Infusion Mezcla  3: Bolo Medicamento Mezcla  4: Infusion Liquido  5: Bolo medicamento Liquido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'METAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'METAPLMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de orden médica de farmacología: 1=Mezcla (preparación compuesta), 2=Líquido (solución simple). Char(1), clasificador de fórmula.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'TIPMEZLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Orden Medica:  1. Mezcla  2. Liquido  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'TIPMEZLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'TIPMEZLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador de la unidad funcional/departamento donde se ejecuta la orden. FK→INUNIFUNC. Char(10), referencia estructura organizativa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención/establecimiento de salud donde se realiza la infusión. FK→ADCENATEN. Char(10), identificador sede/institución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/admisión del paciente. FK→ADINGRESO. Char(10), vincula orden a episodio asistencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (PII Identification_Ofuscado): cédula, identificación, documento. FK→INPACIENT. Varchar(25), mascara parcial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que ordena/ejecuta infusión (PII Identification_Ofuscado). FK→INPROFSAL. Char(20), mascara parcial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación de la orden médica de infusión. DateTime, timestamp registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'FECHAORDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'FECHAORDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'FECHAORDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio/documento administrativo de la orden. Nchar(10), referencia expediente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno del tipo de historia clínica (patrón nombre). Char(9), clasificador documento fuente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consecutivo: clave primaria auto-incremental de órdenes de infusión. Numeric(18) Identity, PK.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de liquidaciones de medicamentos en la historia clínica. Almacena las órdenes de mezclas y preparaciones de medicamentos (como mezclas intravenosas) asociadas a un paciente, ingreso y profesional de salud, incluyendo el tipo de mezcla y el método de preparación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLICI';

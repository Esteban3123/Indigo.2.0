CREATE TABLE [dbo].[HCCTRNOTT] (
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGIST] DATETIME                                                                         NOT NULL,
    [TITNOTENF] CHAR (40)                                                                        NOT NULL,
    [NOTENFSUB] VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "NoteSubjetive_Ofuscado", 0)') NULL,
    [NOTENFOBJ] VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "NoteObjetive_Ofuscado", 0)')  NULL,
    [NOTENFANA] VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "NoteAnalysis_Ofuscado", 0)')  NULL,
    [CODNIVIMP] CHAR (2)                                                                         NULL,
    [FECREGSIS] DATETIME                                                                         CONSTRAINT [DF_HCCTRNOTT_FECREGSIS] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ID]        INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODESPECI] CHAR (3)                                                                         NULL,
    CONSTRAINT [PK_HCCTRNOTT] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCCTRNOTT_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCCTRNOTT_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCTRNOTT_HCNIVIMPT] FOREIGN KEY ([CODNIVIMP]) REFERENCES [dbo].[HCNIVIMPT] ([CODNIVIMP]),
    CONSTRAINT [FK_HCCTRNOTT_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCCTRNOTT_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCCTRNOTT_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCCTRNOTT_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCCTRNOTT].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCCTRNOTT].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCCTRNOTT].[NOTENFSUB]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCCTRNOTT].[NOTENFOBJ]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCCTRNOTT].[NOTENFANA]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');



GO
CREATE NONCLUSTERED INDEX [IDX_HCCTRNOTT_IPCODPACI]
    ON [dbo].[HCCTRNOTT]([IPCODPACI] ASC)
    INCLUDE([NUMINGRES], [CODCENATE], [UFUCODIGO], [CODPROSAL], [FECREGIST]);


GO
CREATE NONCLUSTERED INDEX [IX_HCCTRNOTT_IPCODPACI_CODCENATE]
    ON [dbo].[HCCTRNOTT]([IPCODPACI] ASC, [CODCENATE] ASC)
    INCLUDE([CODPROSAL], [FECREGIST], [NUMINGRES], [UFUCODIGO]);


GO
CREATE NONCLUSTERED INDEX [IPCODPACI_FECREGIST]
    ON [dbo].[HCCTRNOTT]([IPCODPACI] ASC, [FECREGIST] ASC);


GO
CREATE TRIGGER [dbo].[Tgr_ActualizaEspecialidadNULL_HCCTRNOTT] 
   ON  [dbo].[HCCTRNOTT] 
   AFTER INSERT
AS 
BEGIN
	
if exists(select 1 from inserted where CODESPECI is null )begin
		update hc set CODESPECI = prof.CODESPEC1
		from HCCTRNOTT hc
		inner join INPROFSAL prof on prof.CODPROSAL = hc.CODPROSAL
		where hc.ID in (select ID from inserted)
end

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad médica (Char 3). Referencia FK a tabla INESPECIA. Identifica la disciplina clínica: medicina general, cardiología, pediatría, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo de especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY). Clave primaria clustered de la tabla HCCTRNOTT. Genera secuencial automático.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla (Autonumérico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro en sistema (DATETIME). Generada automáticamente por servidor. Marca timestamp de ingreso de la nota al EHR.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de importancia de la nota (Char 2). Referencia FK a HCNIVIMPT. Clasifica prioridad/urgencia: crítica, alta, normal, baja.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'CODNIVIMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel de Importancia de la Nota', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'CODNIVIMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'CODNIVIMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nota de análisis/evaluación clínica (VARCHAR MAX, PII ofuscada). Sección SOAP de análisis e impresión diagnóstica del profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'NOTENFANA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nota Terapia Analisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'NOTENFANA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'NOTENFANA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nota de hallazgos objetivos (VARCHAR MAX, PII ofuscada). Sección SOAP objetivo: signos vitales, examen físico, resultados de pruebas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'NOTENFOBJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nota Terapia Objetivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'NOTENFOBJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'NOTENFOBJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nota de síntomas subjetivos (VARCHAR MAX, PII ofuscada). Sección SOAP subjetivo: anamnesis, queja principal, historia clínica relatada por paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'NOTENFSUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nota Terapia Subjetivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'NOTENFSUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'NOTENFSUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Título o encabezado de la nota clínica (Char 40). Resumen breve del contenido: ''''Consulta Urgencia'''', ''''Evolución Diaria'''', ''''Valoración Inicial''''.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'TITNOTENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Titulo de la Nota', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'TITNOTENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'TITNOTENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del registro clínico (DATETIME). Marca cuándo el profesional documentó la atención, nota o procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'FECREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y Hora del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'FECREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'FECREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (Char 20, PII ofuscada). Referencia FK a INPROFSAL. Identifica médico, enfermero, terapeuta que genera la nota.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional (Char 10). Referencia FK a INUNIFUNC. Identifica área de atención: urgencias, hospitalización, consulta externa, UCI.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (Char 10). Referencia FK a ADCENATEN. Identifica sede, clínica u hospital donde se registró la nota.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/admisión (Char 10). Referencia FK a ADINGRESO. Vincula la nota a un episodio específico de atención del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (VARCHAR 25, PII ofuscada). Referencia FK a INPACIENT. Equivalente a cédula, documento de identidad o número de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas de enfermería registradas durante la atención clínica del paciente, organizadas por ingreso y profesional de salud. Contiene las anotaciones subjetivas, objetivas y de análisis del personal de enfermería para cada encuentro asistencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTT';

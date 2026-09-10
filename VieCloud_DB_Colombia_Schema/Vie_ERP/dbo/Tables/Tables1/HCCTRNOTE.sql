CREATE TABLE [dbo].[HCCTRNOTE] (
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGIST] DATETIME                                                                         NOT NULL,
    [TITNOTENF] CHAR (40)                                                                        NOT NULL,
    [NOTENFSUB] VARCHAR (MAX)                                                                    NULL,
    [NOTENFOBJ] VARCHAR (MAX)                                                                    NULL,
    [NOTENFANA] VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "NoteNursing_Ofuscado", 0)')   NULL,
    [CODNIVIMP] CHAR (2)                                                                         NULL,
    [FECREGSIS] DATETIME                                                                         CONSTRAINT [DF_HCCTRNOTE_FECREGSIS] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ID]        INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CONEXION]  VARCHAR (MAX)                                                                    NULL,
    [CODESPECI] CHAR (3)                                                                         NULL,
    CONSTRAINT [PK_HCCTRNOTE_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCCTRNOTE_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCCTRNOTE_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCTRNOTE_HCNIVIMPN] FOREIGN KEY ([CODNIVIMP]) REFERENCES [dbo].[HCNIVIMPN] ([CODNIVIMP]),
    CONSTRAINT [FK_HCCTRNOTE_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCCTRNOTE_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCCTRNOTE_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCCTRNOTE_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCCTRNOTE].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCCTRNOTE].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCCTRNOTE].[NOTENFANA]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
CREATE NONCLUSTERED INDEX [IX_HCCTRNOTE_IPCODPACI_NUMINGRES_CODCENATE_CODNIVIMP_CODPROSAL_FECREGIST_TITNOTENF_UFUCODIGO]
    ON [dbo].[HCCTRNOTE]([IPCODPACI] ASC, [NUMINGRES] ASC)
    INCLUDE([CODCENATE], [CODNIVIMP], [CODPROSAL], [FECREGIST], [TITNOTENF], [UFUCODIGO]);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCCTRNOTE_1]
    ON [dbo].[HCCTRNOTE]([IPCODPACI] ASC, [FECREGIST] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCCTRNOTE_IPCODPACI_CODCENATE_CODPROSAL_FECREGIST_NUMINGRES_TITNOTENF_UFUCODIGO]
    ON [dbo].[HCCTRNOTE]([IPCODPACI] ASC, [CODCENATE] ASC)
    INCLUDE([CODPROSAL], [FECREGIST], [NUMINGRES], [TITNOTENF], [UFUCODIGO]);


GO
CREATE NONCLUSTERED INDEX [IX_HCCTRNOTE_IPCODPACI_NUMINGRES_CODPROSAL_FECREGIST_FECREGSIS_NOTENFANA_NOTENFOBJ_NOTENFSUB_UFUCODIGO]
    ON [dbo].[HCCTRNOTE]([IPCODPACI] ASC, [NUMINGRES] ASC)
    INCLUDE([CODPROSAL], [FECREGIST], [FECREGSIS], [NOTENFANA], [NOTENFOBJ], [NOTENFSUB], [UFUCODIGO]);


GO
CREATE TRIGGER [dbo].[Tgr_ActualizaEspecialidadNULL_HCCTRNOTE] 
   ON  [dbo].[HCCTRNOTE] 
   AFTER INSERT
AS 
BEGIN
	
if exists(select 1 from inserted where CODESPECI is null )begin
		update hc set CODESPECI = prof.CODESPEC1
		from HCCTRNOTE hc
		inner join INPROFSAL prof on prof.CODPROSAL = hc.CODPROSAL
		where hc.ID in (select ID from inserted)
end


END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad médica con la cual se firmó y registró la nota de enfermería. Referencia a tabla INESPECIA. Búsquedas: especialidad, profesión, área clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el código de la especialidad con la cual se firmó la historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Información contextual de la sesión clínica cuando la nota de enfermería se registra desde procedimiento de hemodiálisis u otra terapia. VARCHAR(MAX). Búsquedas: hemodiálisis, terapia, sesión, conexión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'CONEXION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que se agrega para almacenar información cuando se hacen notas de enfermería desde sesion de hemodialisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'CONEXION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'CONEXION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) de la nota de enfermería. Clave primaria de la tabla HCCTRNOTE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla (Autonumérico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro en sistema (DATETIME). Generada automáticamente por función Common.getdate(). Búsquedas: fecha del sistema, registro automático, timestamp.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de nivel de importancia o prioridad de la nota de enfermería (CHAR 2). Referencia a tabla HCNIVIMPN. Búsquedas: prioridad, urgencia, relevancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'CODNIVIMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel de Importancia de la Nota', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'CODNIVIMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'CODNIVIMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sección de análisis/interpretación de la nota de enfermería (VARCHAR MAX). Campo ofuscado (NoteNursing_Ofuscado) por contener PII sensible. Búsquedas: evaluación, análisis, interpretación clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'NOTENFANA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nota Enfermeria Analisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'NOTENFANA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'NOTENFANA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sección de objetivos o metas clínicas de la nota de enfermería (VARCHAR MAX). Búsquedas: objetivo, meta terapéutica, plan de cuidado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'NOTENFOBJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nota Enfermeria Objetivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'NOTENFOBJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'NOTENFOBJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sección subjetiva de la nota de enfermería: síntomas, quejas y percepción del paciente (VARCHAR MAX). Búsquedas: síntomas, queja, subjetivo, relato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'NOTENFSUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nota Enfermeria Subjetivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'NOTENFSUB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'NOTENFSUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Título o encabezado descriptivo de la nota de enfermería (CHAR 40). Búsquedas: tema, asunto, descripción breve.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'TITNOTENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Titulo de la Nota', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'TITNOTENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'TITNOTENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro de la nota de enfermería (DATETIME). Puede diferir de FECREGSIS. Búsquedas: fecha registro, timestamp, cuándo se escribió.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'FECREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y Hora del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'FECREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'FECREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (enfermero, médico) que registró la nota (VARCHAR 25, ofuscado Identification_Ofuscado). Referencia a tabla INPROFSAL. Búsquedas: profesional, enfermero, médico, autor.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional donde se registró la nota (CHAR 10). Referencia a tabla INUNIFUNC. Búsquedas: unidad funcional, área, piso, sala, servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro o institución de atención donde se registró (CHAR 10). Referencia a tabla ADCENATEN. Búsquedas: centro, hospital, institución, sede.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso o atención del paciente asociado a la nota (CHAR 10). Referencia a tabla ADINGRESO. Búsquedas: ingreso, número de ingreso, atención, episodio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente (cédula, identificación, documento). VARCHAR 25, ofuscado Identification_Ofuscado por PII. Referencia a tabla INPACIENT. Búsquedas: paciente, cédula, identificación, documento, historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas de enfermería registradas en la historia clínica del paciente durante un ingreso hospitalario. Cada registro corresponde a una anotación clínica realizada por un profesional de salud, con sus componentes subjetivo, objetivo y de análisis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNOTE';

CREATE TABLE [dbo].[HCHEMODIA] (
    [CONSECUTI] NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [REGREGIST] DATETIME                                                                         NOT NULL,
    [NUMSESSIO] CHAR (20)                                                                        NOT NULL,
    [NUMMAQUIN] CHAR (20)                                                                        NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [TIEMSESIO] INT                                                                              NOT NULL,
    [DIALIZADO] CHAR (10)                                                                        NOT NULL,
    [CANTBOMBA] CHAR (10)                                                                        NOT NULL,
    [ELVALORUF] CHAR (10)                                                                        NOT NULL,
    [NUMAGUJAS] INT                                                                              NOT NULL,
    [CHEPARINA] CHAR (10)                                                                        NOT NULL,
    [SELCATETE] CHAR (10)                                                                        NOT NULL,
    [SELFAVFAV] CHAR (10)                                                                        NOT NULL,
    [VALENFERM] CHAR (400)                                                                       NOT NULL,
    [PESOPSECO] CHAR (10)                                                                        NOT NULL,
    [PESOPREDI] CHAR (10)                                                                        NOT NULL,
    [PESPOSDIA] CHAR (10)                                                                        NOT NULL,
    [FECREGSIS] DATETIME                                                                         CONSTRAINT [DF_HCHEMODIA_FECREGSIS] DEFAULT ([Common].[getdate]()) NOT NULL,
    CONSTRAINT [PK_HCHEMODIA] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC),
    CONSTRAINT [FK_HCHEMODIA_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCHEMODIA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCHEMODIA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCHEMODIA_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCHEMODIA_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCHEMODIA].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCHEMODIA].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro en el sistema (DateTime). Timestamp automático de inserción del registro de sesión de hemodiálisis. Auditoría de creación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Registro en el Sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso post-diálisis en mililitros (ml). Peso corporal del paciente al finalizar la sesión de hemodiálisis. Control de ganancia interdialítica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'PESPOSDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso Pos-Dialisis (ml)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'PESPOSDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'PESPOSDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso pre-diálisis en kilogramos (kg). Peso corporal del paciente al inicio de la sesión de hemodiálisis. Baseline de sesión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'PESOPREDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso Predialisis (kg)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'PESOPREDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'PESOPREDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso seco en kilogramos (kg). Peso objetivo o meta del paciente sin exceso de líquido. Referencia terapéutica para ultrafiltración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'PESOPSECO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso Seco (kg)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'PESOPSECO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'PESOPSECO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valoración de enfermería (VARCHAR 400). Observaciones clínicas y evaluación del paciente durante la sesión de diálisis. Notas de cuidado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'VALENFERM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valoracion de Enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'VALENFERM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'VALENFERM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Selección de tipo de fístula arteriovenosa (FAV). Opciones: 1=FAV (fístula nativa), 2=Injerto. Acceso vascular para hemodiálisis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'SELFAVFAV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FAV:  1. FAV  2. INJERTO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'SELFAVFAV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'SELFAVFAV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Selección de tipo de catéter venoso central. Opciones: 1=Permanente Yugular, 2=Transitorio Yugular, 3=Transitorio Subclavio, 4=Transitorio Femoral. Acceso vascular alternativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'SELCATETE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cateter:  1.Perm YUG  2.Trans. YUG  3.Trans. SBC  4.Trans. FM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'SELCATETE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'SELCATETE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis de heparina en unidades internacionales (UI). Anticoagulante administrado durante la sesión para prevenir coagulación del circuito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'CHEPARINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Heparina (UI)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'CHEPARINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'CHEPARINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de agujas utilizadas (INT). Número de agujas de punción para acceso vascular en la sesión de hemodiálisis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'NUMAGUJAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agujas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'NUMAGUJAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'NUMAGUJAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ultrafiltración (UF) en mililitros (ml). Volumen de líquido removido durante la sesión de diálisis. Control de balance hídrico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'ELVALORUF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'UF (ml)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'ELVALORUF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'ELVALORUF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Velocidad de bomba en mililitros por minuto (ml/min). Flujo de sangre en el circuito de hemodiálisis. Parámetro de flujo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'CANTBOMBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'bomba (ml/min)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'CANTBOMBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'CANTBOMBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de dializador (fl, filtro de luz). Membrana semipermeables utilizada en la sesión. Especificación del dispositivo de diálisis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'DIALIZADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dializador (fl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'DIALIZADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'DIALIZADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración de la sesión en número de horas (INT). Tiempo total de tratamiento de hemodiálisis. Parámetro temporal de terapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'TIEMSESIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Horas de la Sesion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'TIEMSESIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'TIEMSESIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (VARCHAR 25, PII ofuscado). Identificador único del médico, enfermero o terapeuta que realizó la sesión. FK a INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (CHAR 10). Identificador del servicio o centro de diálisis donde se realizó la sesión. FK a INUNIFUNC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (CHAR 10). Identificador de la institución de salud donde se registró la sesión. FK a ADCENATEN.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso o admisión (CHAR 10). Identificador del episodio de atención del paciente. FK a ADINGRESO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (VARCHAR 25, PII ofuscado). Identificación única del paciente (cédula, documento de identidad). FK a INPACIENT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de la máquina de hemodiálisis (CHAR 20). Identificador del equipo de diálisis utilizado en la sesión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'NUMMAQUIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la Maquina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'NUMMAQUIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'NUMMAQUIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de sesión (CHAR 20). Identificador único de la sesión de hemodiálisis realizada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'NUMSESSIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la Session', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'NUMSESSIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'NUMSESSIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del registro (DateTime). Timestamp de creación o entrada del registro de sesión en el sistema EHR.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'REGREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'REGREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'REGREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de la tabla (NUMERIC 18, PK Identity). Identificador único autoincrementable del registro de sesión de hemodiálisis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'CONSECUTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de sesiones de hemodiálisis por paciente. Almacena los parámetros clínicos y operativos de cada sesión de diálisis: duración, peso, tipo de acceso vascular, uso de heparina, agujas y valores de enfermería, vinculados al ingreso y al profesional de salud tratante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA';

CREATE TABLE [dbo].[INDIAGNOI] (
    [IDETIPHIS]          CHAR (9)                                                                         NULL,
    [NUMEFOLIO]          NCHAR (10)                                                                       NULL,
    [CODCENATE]          CHAR (10)                                                                        NULL,
    [UFUCODIGO]          CHAR (10)                                                                        NULL,
    [NUMINGRES]          CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]          VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODDIAGNO]          CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NOT NULL,
    [CODPROSAL]          CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [CODDIAPRI]          BIT MASKED WITH (FUNCTION = 'default()')                                         NOT NULL,
    [DIAINGEGR]          CHAR (1)                                                                         NOT NULL,
    [TIPDIAGNO]          CHAR (1)                                                                         NOT NULL,
    [CLADIAGNO]          CHAR (2)                                                                         NOT NULL,
    [OBSDIAGNO]          CHAR (250)                                                                       NOT NULL,
    [FECDIAGNO]          DATETIME                                                                         NOT NULL,
    [FOLDIAGNO]          INT                                                                              NOT NULL,
    [DIAESTADO]          INT                                                                              NULL,
    [INDAUDFOR]          NUMERIC (18)                                                                     NOT NULL,
    [PLANTDIAG]          TEXT                                                                             NULL,
    [TRATA4505]          INT                                                                              NULL,
    [FECHLEISH]          DATETIME                                                                         NULL,
    [T1]                 CHAR (2)                                                                         NULL,
    [T2]                 CHAR (2)                                                                         NULL,
    [N1]                 CHAR (2)                                                                         NULL,
    [N2]                 CHAR (2)                                                                         NULL,
    [M1]                 CHAR (2)                                                                         NULL,
    [IDADENFHUERFANAS]   INT                                                                              NULL,
    [M2]                 CHAR (2)                                                                         NULL,
    [ESTADIO]            INT                                                                              NULL,
    [TIPOCANCER]         INT                                                                              NULL,
    [CONFIRMATNMESTADIO] BIT                                                                              NULL,
    CONSTRAINT [PK_INDIAGNOI] PRIMARY KEY CLUSTERED ([NUMINGRES] ASC, [IPCODPACI] ASC, [CODDIAGNO] ASC, [CODDIAPRI] ASC),
    CONSTRAINT [FK_INDIAGNOI_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_INDIAGNOI_ADENFHUERFANAS] FOREIGN KEY ([IDADENFHUERFANAS]) REFERENCES [dbo].[ADENFHUERFANAS] ([ID]),
    CONSTRAINT [FK_INDIAGNOI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_INDIAGNOI_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_INDIAGNOI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_INDIAGNOI_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_INDIAGNOI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INDIAGNOI].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INDIAGNOI].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INDIAGNOI].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INDIAGNOI].[CODDIAPRI]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirmación de estadificación TNM (Tumor, Nódulo, Metástasis) y Estadio clínico en cáncer. Bit: 0=No confirmado, 1=Confirmado. Usado en oncología para validar clasificación de malignidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'CONFIRMATNMESTADIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Confirmación de campos T, N, M y Estadio --> 0=No, 1=Si ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'CONFIRMATNMESTADIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'CONFIRMATNMESTADIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de cáncer diagnosticado: 1=Primario, 2=Otro Primario, 3=Primario desconocido, 4=Metástasis. Determina origen e historia oncológica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'TIPOCANCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Cáncer.  1 - Primario  2 - Otro Primario  3 - Primario desconocido  4 - Metástasis  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'TIPOCANCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'TIPOCANCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estadio clínico del cáncer (0, A, B, C) según clasificación TNM. Solo aplica cuando diagnóstico es tipo Cáncer. Indicador de progresión y severidad tumoral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'ESTADIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para almancear el Estadio 2 solo cuando el diagnostico sea de tipo Cáncer, valores 0-A-B-C', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'ESTADIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'ESTADIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Componente Metástasis (M) secundario de la clasificación TNM en cáncer confirmado. Indica presencia/ausencia de metástasis distantes. Solo se registra cuando se confirma diagnóstico oncológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'M2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Para Almacenar el Datos ''''M'''' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'M2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'M2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de enfermedad huérfana (rara) asociada al diagnóstico. Referencia FK a tabla ADENFHUERFANAS. Usado para trazabilidad de enfermedades de baja prevalencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'IDADENFHUERFANAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion de la enfermedad huerfana asociada al Diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'IDADENFHUERFANAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'IDADENFHUERFANAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Componente Metástasis (M) primario de la clasificación TNM en cáncer confirmado. Indica presencia/ausencia de metástasis distantes. Clave para estadificación oncológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'M1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Para Almacenar el Datos ''''M'''' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'M1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'M1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Componente Nódulo (N) secundario de la clasificación TNM en cáncer confirmado. Evalúa afectación de ganglios linfáticos. Solo se registra cuando se confirma diagnóstico oncológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'N2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Para Almacenar el Datos ''''N'''' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'N2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'N2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Componente Nódulo (N) primario de la clasificación TNM en cáncer confirmado. Evalúa afectación de ganglios linfáticos regionales. Clave para estadificación oncológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'N1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Para Almacenar el Datos ''''N'''' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'N1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'N1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Componente Tumor (T) secundario de la clasificación TNM en cáncer confirmado. Describe tamaño y extensión local del tumor. Solo se registra cuando se confirma diagnóstico oncológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'T2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Para Almacenar el Datos ''''T'''' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'T2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'T2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Componente Tumor (T) primario de la clasificación TNM en cáncer confirmado. Describe tamaño y extensión local del tumor primario. Clave para estadificación oncológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'T1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Para Almacenar el Datos ''''T'''' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'T1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'T1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de terminación o finalización del tratamiento para Leishmaniasis. Datetime: marca cierre de protocolo terapéutico. Usado en seguimiento de enfermedades tropicales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'FECHLEISH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Terminación Tratamiento para Leishmaniasis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'FECHLEISH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'FECHLEISH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de cumplimiento de tratamiento interdisciplinario según diagnóstico (ansiedad, depresión, esquizofrenia, TDAH, consumo SPA, bipolaridad, hipotiroidismo congénito, sífilis gestacional/congénita, lepra). Valores: 1=En proceso interdisciplinario, 2=Completó, 16=Negación tradicional, 17=Contraindicación médica, 18=Negación usuario, 20=Otras razones, 22=Sin dato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'TRATA4505';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si el paciente con diagnostico de: Ansiedad, Depresión, Esquizofrenia, Deficit de atención, consumo SPA y Bipolaridad  1- El paciente está en procesio de atención por equipo interdisciplinario en el hospital ..  2- El paciente recibió atención por equipo interdisciplinario completo en el hospital...  16- El paciente no recibió atención por tener una tradición que se lo impide  17- No recibió atención por una condición de salud  18- No recibió atención por negación del usuario  20- No recibió atención por otras razones  22- Sin dato    Si el diagnostico Hipotiroidismo congenito, sifilis gestacional, sifilis congenita, lepra  1- El paciente recibe tratamiento en hospital...pero aún no ha terminado  2- El paciente recibió tratamiento en el hospital... y ya lo terminó  16- No recibió tratamiento por tener una tradición que se lo impide  17- No recibió tratamiento por una condición de salud que se lo impide  18- No recibió tratamiento por negación del usuario  20- No recibió tratamiento por otras razones  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'TRATA4505';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'TRATA4505';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de texto libre para almacenar contenido de plantilla diagnóstica. Permite documentación estructurada o narrativa del diagnóstico. Usado en historia clínica digital.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'PLANTDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para almacenar el contenido de la plantilla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'PLANTDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'PLANTDIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo reservado para auditoría interna. Numeric(18). Trazabilidad y control de cambios en registro diagnóstico. Uso administrativo/cumplimiento normativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Reservado Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del diagnóstico en historia clínica: 1=Activo, 2=Descartado. Indica vigencia diagnóstica durante ingreso. Usado para auditoría y seguimiento de diagnósticos finales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'DIAESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado   1: Activo  2: Descartado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'DIAESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'DIAESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio secuencial donde se registró/especificó el diagnóstico en historia clínica. Int. Referencia a ubicación física o lógica del documento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'FOLDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del folio de la historia clinica en donde se especifico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'FOLDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'FOLDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de asignación o registro del diagnóstico. Datetime. Marca evento clínico y temporal de identificación diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'FECDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Asignacion del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'FECDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'FECDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación o nota general descriptiva del diagnóstico. Char(250). Espacio para contexto clínico, hallazgos relevantes o aclaraciones diagnósticas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'OBSDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion general del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'OBSDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'OBSDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación temporal del diagnóstico: PR=Preoperatorio, PO=Postoperatorio, PP=Pre y Postoperatorio, HI=Histopatológico, NA=No Aplica. Relaciona diagnóstico a evento quirúrgico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'CLADIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clase de Diagnostico  PR: Pre-operatorio  PO: Pos-operatorio  PP: Pre y Pos-Operatorio  HI: Hispatologico  NA: No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'CLADIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'CLADIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo diagnóstico por certeza: I=Impresión diagnóstica (presuntivo), C=Confirmado Nuevo (primera vez confirmado), R=Confirmado Repetido (reafirmación). Indica nivel de certeza clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'TIPDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Diagnostico  I: Impresion Diagnostica  C: Confirmado Nuevo  R: Confirmado Repetido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'TIPDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'TIPDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica momento diagnóstico: I=Ingreso, E=Egreso, A=Ambos. Indica si diagnóstico se registra en entrada o salida del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'DIAINGEGR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el Diagnostico es de Ingreso o Egreso  I: Ingreso  E: Egreso  A: Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'DIAINGEGR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'DIAINGEGR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de diagnóstico principal del ingreso. Bit 0/1: solo aplica un principal por NUMINGRES. Clave primaria. Marcador de diagnóstico primario vs secundario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'CODDIAPRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diagnostico Principal - Solo Aplica uno por Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'CODDIAPRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'CODDIAPRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del profesional de salud (médico, especialista) que asigna diagnóstico. Varchar(25) PII ofuscado. Referencia FK a INPROFSAL. Trazabilidad profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico CIE-10 o clasificación clínica estándar del diagnóstico. Char(4) PII ofuscado. Referencia FK a INDIAGNOS. Normalización diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente (cédula, documento, identificación, equivalente). Varchar(25) PII ofuscado partial(). Referencia FK a INPACIENT. Identificación paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/admisión del paciente al centro de atención. Char(10) no nulo, PK. Referencia FK a ADINGRESO. Identifica episodio de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional (servicio, piso, especialidad) donde se diagnostica. Char(10). Referencia FK a INUNIFUNC. Ubicación clínica del diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro o institución de atención de salud. Char(10). Referencia FK a ADCENATEN. Identifica institución proveedora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio o expediente clínico donde se documenta diagnóstico. Nchar(10). Referencia a historia clínica física o digital.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o código interno del tipo de historia clínica asociado. Char(9). Clasificador de estructura o plantilla de registro diagnóstico usado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnósticos registrados por ingreso o atención del paciente. Incluye el código CIE-10, tipo, clasificación, fecha del diagnóstico, profesional que lo registró y datos de estadificación oncológica (TNM, estadio, tipo de cáncer), así como información de enfermedades huérfanas y leishmaniasis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOI';

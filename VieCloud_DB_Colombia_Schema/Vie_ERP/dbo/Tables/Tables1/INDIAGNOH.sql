CREATE TABLE [dbo].[INDIAGNOH] (
    [IDETIPHIS]          CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]          NCHAR (10)                                                                       NOT NULL,
    [CODCENATE]          CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]          CHAR (10)                                                                        NOT NULL,
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
    [FOLDIAGNO]          INT                                                                              NULL,
    [DIAESTADO]          INT                                                                              NULL,
    [INDAUDFOR]          NUMERIC (18)                                                                     NOT NULL,
    [PLANTDIAG]          VARCHAR (MAX)                                                                    NULL,
    [TRATA4505]          INT                                                                              NULL,
    [FECHLEISH]          DATETIME                                                                         NULL,
    [ESTADIO]            INT                                                                              NULL,
    [T1]                 CHAR (2)                                                                         NULL,
    [T2]                 CHAR (2)                                                                         NULL,
    [N1]                 CHAR (2)                                                                         NULL,
    [N2]                 CHAR (2)                                                                         NULL,
    [M1]                 CHAR (2)                                                                         NULL,
    [M2]                 CHAR (2)                                                                         NULL,
    [IDADENFHUERFANAS]   INT                                                                              NULL,
    [TIPOCANCER]         INT                                                                              NULL,
    [CONFIRMATNMESTADIO] BIT                                                                              NULL,
    CONSTRAINT [PK_INDIAGNOH] PRIMARY KEY CLUSTERED ([NUMEFOLIO] ASC, [NUMINGRES] ASC, [IPCODPACI] ASC, [CODDIAGNO] ASC),
    CONSTRAINT [FK_INDIAGNOH_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_INDIAGNOH_ADENFHUERFANAS] FOREIGN KEY ([IDADENFHUERFANAS]) REFERENCES [dbo].[ADENFHUERFANAS] ([ID]),
    CONSTRAINT [FK_INDIAGNOH_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_INDIAGNOH_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_INDIAGNOH_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_INDIAGNOH_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_INDIAGNOH_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[INDIAGNOH] NOCHECK CONSTRAINT [FK_INDIAGNOH_INDIAGNOS];


GO
ALTER TABLE [dbo].[INDIAGNOH] NOCHECK CONSTRAINT [FK_INDIAGNOH_INPROFSAL];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INDIAGNOH].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INDIAGNOH].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INDIAGNOH].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INDIAGNOH].[CODDIAPRI]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
CREATE NONCLUSTERED INDEX [PK_INDIAGNOH1]
    ON [dbo].[INDIAGNOH]([NUMINGRES] ASC, [IPCODPACI] ASC)
    INCLUDE([CODDIAGNO], [CODDIAPRI], [NUMEFOLIO], [OBSDIAGNO]);


GO
CREATE NONCLUSTERED INDEX [IX_INDIAGNOH_IPCODPACI_FECDIAGNO]
    ON [dbo].[INDIAGNOH]([IPCODPACI] ASC, [FECDIAGNO] DESC)
    INCLUDE([NUMINGRES], [CODDIAGNO], [CODDIAPRI], [NUMEFOLIO]);


GO
CREATE NONCLUSTERED INDEX [IX_INDIAGNOH__IPCODPACI__CODDIAGNO__INC__ALL2]
    ON [dbo].[INDIAGNOH]([IPCODPACI] ASC, [CODDIAGNO] ASC)
    INCLUDE([IDETIPHIS], [NUMEFOLIO], [CODCENATE], [UFUCODIGO], [NUMINGRES], [CODPROSAL], [CODDIAPRI], [DIAINGEGR], [TIPDIAGNO], [CLADIAGNO], [OBSDIAGNO], [FECDIAGNO], [FOLDIAGNO], [DIAESTADO], [INDAUDFOR], [PLANTDIAG], [TRATA4505], [FECHLEISH], [T2], [N1], [N2], [T1], [IDADENFHUERFANAS], [TIPOCANCER], [M1], [M2], [ESTADIO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirmación de estadificación TNM en cáncer (campos T, N, M y Estadio). Bit booleano: 0=No confirmado, 1=Confirmado. Usado en oncología para validar clasificación tumoral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'CONFIRMATNMESTADIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Confirmación de campos T, N, M y Estadio --> 0=No, 1=Si ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'CONFIRMATNMESTADIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'CONFIRMATNMESTADIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de cáncer según origen: 1=Primario, 2=Otro primario, 3=Primario desconocido, 4=Metástasis. Clasificación para oncología e historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'TIPOCANCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Cáncer.  1 - Primario   2 - Otro Primario   3 - Primario desconocido   4 - Metástasis  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'TIPOCANCER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'TIPOCANCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de enfermedad huérfana (rara) asociada al diagnóstico. FK a tabla ADENFHUERFANAS. Vinculación de patologías de baja prevalencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'IDADENFHUERFANAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion de la enfermedad huerfana asociada al Diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'IDADENFHUERFANAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'IDADENFHUERFANAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo Metástasis (M) del estadificación TNM en cáncer, confirmado. OBSOLETO desde 10-02-2024 por autorización Product Owner Oncología. Almacena clasificación de metástasis distantes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'M1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'10-02-202 -> Campo se vuleve obsoleto desde la fecha, por autorizacion de Product Owner en PBI de Oncologia.    Campo Para Almacenar el Datos ''''M'''' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'M1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'M1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo ganglios/Nodos (N) secundario del estadificación TNM en cáncer confirmado. Almacena clasificación de afectación ganglionar regional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'N2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Para Almacenar el Datos ''''N'''' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'N2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'N2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo ganglios/Nodos (N) primario del estadificación TNM en cáncer confirmado. Almacena clasificación de afectación ganglionar regional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'N1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Para Almacenar el Datos ''''N'''' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'N1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'N1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo Tumor (T) secundario del estadificación TNM en cáncer confirmado. Almacena clasificación de tamaño/extensión tumoral local.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'T2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Para Almacenar el Datos ''''T'''' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'T2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'T2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo Tumor (T) primario del estadificación TNM en cáncer confirmado. Almacena clasificación de tamaño/extensión tumoral local.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'T1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Para Almacenar el Datos ''''T'''' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'T1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'T1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estadio clínico de cáncer (I-IV). OBSOLETO desde 10-02-2024 por autorización Product Owner Oncología. Derivado de clasificación TNM.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'ESTADIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'10-02-202 -> Campo se vuleve obsoleto desde la fecha, por autorizacion de Product Owner en PBI de Oncologia.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'ESTADIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'ESTADIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de terminación/fin de tratamiento para Leishmaniasis. DateTime para seguimiento de patología parasitaria y cumplimiento terapéutico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'FECHLEISH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Terminación Tratamiento para Leishmaniasis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'FECHLEISH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'FECHLEISH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de atención recibida en diagnósticos priorizados (ansiedad, depresión, esquizofrenia, TDAH, consumo SPA, bipolaridad, hipotiroidismo congénito, sífilis, lepra). Códigos: 1=En proceso interdisciplinario, 2=Completado, 16-22=Razones de no atención. RIPS/auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'TRATA4505';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si el paciente con diagnostico de: Ansiedad, Depresión, Esquizofrenia, Deficit de atención, consumo SPA y Bipolaridad  1- El paciente está en procesio de atención por equipo interdisciplinario en el hospital ..  2- El paciente recibió atención por equipo interdisciplinario completo en el hospital...  16- El paciente no recibió atención por tener una tradición que se lo impide  17- No recibió atención por una condición de salud  18- No recibió atención por negación del usuario  20- No recibió atención por otras razones  22- Sin dato    Si el diagnostico Hipotiroidismo congenito, sifilis gestacional, sifilis congenita, lepra  1- El paciente recibe tratamiento en hospital...pero aún no ha terminado  2- El paciente recibió tratamiento en el hospital... y ya lo terminó  16- No recibió tratamiento por tener una tradición que se lo impide  17- No recibió tratamiento por una condición de salud que se lo impide  18- No recibió tratamiento por negación del usuario  20- No recibió tratamiento por otras razones  22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'TRATA4505';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'TRATA4505';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plantilla o template de diagnóstico reutilizable. VARCHAR(MAX) para almacenar estructura predefinida de diagnósticos, facilita documentación estandarizada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'PLANTDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plantillas de diagnosticos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'PLANTDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'PLANTDIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo reservado para auditoría interna. Numeric(18). Indicador de rastreo y control de cambios en registros diagnósticos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Reservado Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del diagnóstico en ingreso: 1=Activo, 2=Descartado. Permite marcar diagnósticos descartados sin eliminar el registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'DIAESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado   1: Activo  2: Descartado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'DIAESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'DIAESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio de historia clínica en donde se especificó el diagnóstico. Referencia a documento físico o digital de respaldo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'FOLDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del folio de la historia clinica en donde se especifico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'FOLDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'FOLDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de asignación/registro del diagnóstico. DateTime crítico para auditoría clínica y cumplimiento regulatorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'FECDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Asignacion del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'FECDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'FECDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones generales del diagnóstico. Campo texto 250 caracteres para notas clínicas complementarias, síntomas, hallazgos relevantes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'OBSDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion general del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'OBSDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'OBSDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación temporal del diagnóstico: PR=Preoperatorio, PO=Postoperatorio, PP=Pre y postoperatorio, HI=Histopatológico, NA=No aplica. Contexto quirúrgico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'CLADIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clase de Diagnostico  PR: Pre-operatorio  PO: Pos-operatorio  PP: Pre y Pos-Operatorio  HI: Hispatologico  NA: No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'CLADIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'CLADIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo diagnóstico: I=Impresión diagnóstica (probable), C=Confirmado nuevo, R=Confirmado repetido. Grado de certeza clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'TIPDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Diagnostico  I: Impresion Diagnostica  C: Confirmado Nuevo  R: Confirmado Repetido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'TIPDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'TIPDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Momento diagnóstico en atención: I=Ingreso, E=Egreso, A=Ambos. Define punto temporal de identificación de patología en ciclo asistencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'DIAINGEGR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el Diagnostico es de Ingreso o Egreso  I: Ingreso  E: Egreso  A: Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'DIAINGEGR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'DIAINGEGR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnóstico principal (BIT). Solo un diagnóstico principal permitido por ingreso/atención. Prioridad clínica y facturación RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'CODDIAPRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diagnostico Principal - Solo Aplica uno por Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'CODDIAPRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'CODDIAPRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (médico, especialista) que registra el diagnóstico. FK a INPROFSAL. PII enmascarado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico (CIE-10 o nomenclatura interna). FK a INDIAGNOS. Identificación estandarizada de patología, enfermedad, condición de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente (cédula, documento, identificación). FK a INPACIENT. PII Identification_Ofuscado en búsquedas no auditadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente a centro de atención. FK a ADINGRESO. Identifica episodio asistencial, atención, hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional (servicio, departamento, área clínica). FK a INUNIFUNC. Sede funcional de registro diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (hospital, clínica, CAMI). FK a ADCENATEN. Institución donde se diagnostica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio de historia clínica/expediente. Identificador de documento clínico consolidado del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno/tipo de historia clínica. Clasificación o nombre interno de modalidad de registro (papeleta, HCE, historia digital).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnósticos registrados en la historia clínica de cada ingreso o atención. Almacena los códigos CIE-10, tipo, clasificación, observaciones y estadificación oncológica (TNM) asociados a un paciente, profesional de salud y centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segunda subclasificación de metástasis a distancia dentro del sistema de estadificación oncológica TNM; complementa el componente M (metástasis) del diagnóstico de cáncer.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'M2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNOH', @level2type = N'COLUMN', @level2name = N'M2';

CREATE TABLE [dbo].[INDIAGNIH] (
    [IDETIPHIS] CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODDIAGNO] CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [CODDIAPRI] BIT MASKED WITH (FUNCTION = 'default()')                                         NOT NULL,
    [DIAINGEGR] CHAR (1)                                                                         NOT NULL,
    [TIPDIAGNO] CHAR (1)                                                                         NOT NULL,
    [CLADIAGNO] CHAR (2)                                                                         NOT NULL,
    [OBSDIAGNO] CHAR (250)                                                                       NOT NULL,
    [FECDIAGNO] DATETIME                                                                         NOT NULL,
    [FOLDIAGNO] INT                                                                              NOT NULL,
    [DIAESTADO] INT                                                                              NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    [PLANTDIAG] TEXT                                                                             NULL,
    [TRATA4505] INT                                                                              NULL,
    [FECHLEISH] DATETIME                                                                         NULL,
    [ESTADIO2]  CHAR (10)                                                                        NULL,
    [ESTADIO1]  CHAR (10)                                                                        NULL,
    [T1]        CHAR (2)                                                                         NULL,
    [T2]        CHAR (2)                                                                         NULL,
    [N1]        CHAR (2)                                                                         NULL,
    [N2]        CHAR (2)                                                                         NULL,
    [M]         CHAR (2)                                                                         NULL,
    CONSTRAINT [PK_INDIAGNIH] PRIMARY KEY CLUSTERED ([NUMEFOLIO] ASC, [NUMINGRES] ASC, [IPCODPACI] ASC, [CODDIAGNO] ASC),
    CONSTRAINT [FK_INDIAGNIH_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_INDIAGNIH_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_INDIAGNIH_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_INDIAGNIH_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_INDIAGNIH_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_INDIAGNIH_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INDIAGNIH].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INDIAGNIH].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INDIAGNIH].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INDIAGNIH].[CODDIAPRI]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de metástasis (M) en diagnóstico de cáncer confirmado; tipo CHAR(2), parte de estadificación TNM oncológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'M';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Para Almacenar el Datos ''''M'''' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'M';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'M';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de ganglios linfáticos regionales (N) en diagnóstico de cáncer confirmado; tipo CHAR(2), componente TNM oncológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'N2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Para Almacenar el Datos ''''N'''' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'N2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'N2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de ganglios linfáticos regionales (N) en diagnóstico de cáncer confirmado; tipo CHAR(2), componente TNM oncológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'N1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Para Almacenar el Datos ''''N'''' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'N1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'N1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de tumor primario (T) en diagnóstico de cáncer confirmado; tipo CHAR(2), componente TNM oncológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'T2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Para Almacenar el Datos ''''T'''' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'T2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'T2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de tumor primario (T) en diagnóstico de cáncer confirmado; tipo CHAR(2), componente TNM oncológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'T1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Para Almacenar el Datos ''''T'''' del Diagnostico Cuando sea de tipo Cancer y sea confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'T1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'T1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estadificación clínica primaria (0-IV) en cáncer confirmado; tipo CHAR(10), resumen TNM del tumor maligno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'ESTADIO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para almancear el Estadio1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'ESTADIO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'ESTADIO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estadificación clínica secundaria (valores 0-A-B-C) en cáncer confirmado; tipo CHAR(10), refinamiento oncológico TNM.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'ESTADIO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para almancear el Estadio 2 solo cuando el diagnostico sea de tipo Cáncer, valores 0-A-B-C', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'ESTADIO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'ESTADIO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de término de tratamiento para Leishmaniasis; tipo DATETIME, marcador de egreso terapéutico en enfermedad parasitaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'FECHLEISH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Terminación Tratamiento para Leishmaniasis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'FECHLEISH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'FECHLEISH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de atención/tratamiento según diagnóstico (depresión, ansiedad, esquizofrenia, SPA, hipotiroidismo, sífilis, lepra); tipo INT con códigos 1-22, trazabilidad RIPS de continuidad asistencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'TRATA4505';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si el paciente con diagnostico de: Ansiedad, Depresión, Esquizofrenia, Deficit de atención, consumo SPA y Bipolaridad 1- El paciente está en procesio de atención por equipo interdisciplinario en el hospital .. 2- El paciente recibió atención por equipo interdisciplinario completo en el hospital... 16- El paciente no recibió atención por tener una tradición que se lo impide 17- No recibió atención por una condición de salud 18- No recibió atención por negación del usuario 20- No recibió atención por otras razones 22- Sin dato Si el diagnostico Hipotiroidismo congenito, sifilis gestacional, sifilis congenita, lepra 1- El paciente recibe tratamiento en hospital...pero aún no ha terminado 2- El paciente recibió tratamiento en el hospital... y ya lo terminó 16- No recibió tratamiento por tener una tradición que se lo impide 17- No recibió tratamiento por una condición de salud que se lo impide 18- No recibió tratamiento por negación del usuario 20- No recibió tratamiento por otras razones 22- Sin dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'TRATA4505';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'TRATA4505';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plantilla clínica con contenido diagnóstico estructurado; tipo TEXT, template reutilizable en formulación de diagnósticos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'PLANTDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para almacenar el contenido de la plantilla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'PLANTDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'PLANTDIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera de auditoría y trazabilidad; tipo NUMERIC(18), campo reservado para control de cambios y cumplimiento normativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Reservado Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del diagnóstico (1=Activo, 2=Descartado); tipo INT, controla validez clínica del registro diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'DIAESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado   1: Activo  2: Descartado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'DIAESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'DIAESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio de historia clínica donde se especificó diagnóstico; tipo INT, referencia a ubicación física/digital en expediente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'FOLDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del folio de la historia clinica en donde se especifico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'FOLDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'FOLDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de asignación/registro del diagnóstico; tipo DATETIME, marca temporal clínica de documentación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'FECDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Asignacion del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'FECDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'FECDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones y notas clínicas generales del diagnóstico; tipo CHAR(250), campos adicionales para contexto diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'OBSDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion general del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'OBSDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'OBSDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clase de diagnóstico (PR=Preoperatorio, PO=Posoperatorio, PP=Pre-Pos, HI=Histopatológico, NA=NoAplica); tipo CHAR(2), contexto quirúrgico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'CLADIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clase de Diagnostico  PR: Pre-operatorio  PO: Pos-operatorio  PP: Pre y Pos-Operatorio  HI: Hispatologico  NA: No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'CLADIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'CLADIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de diagnóstico (I=Impresión, C=Confirmado Nuevo, R=Confirmado Repetido); tipo CHAR(1), certeza y novedad diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'TIPDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Diagnostico  I: Impresion Diagnostica  C: Confirmado Nuevo  R: Confirmado Repetido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'TIPDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'TIPDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Momento del diagnóstico (I=Ingreso, E=Egreso, A=Ambos); tipo CHAR(1), punto temporal en ciclo de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'DIAINGEGR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el Diagnostico es de Ingreso o Egreso  I: Ingreso  E: Egreso  A: Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'DIAINGEGR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'DIAINGEGR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnóstico principal (BIT, un único por ingreso); tipo BIT masked, PKg con CODDIAGNO, indicador de diagnóstico primario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'CODDIAPRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diagnostico Principal - Solo Aplica uno por Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'CODDIAPRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'CODDIAPRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código profesional de salud (médico, enfermero); tipo VARCHAR(25) PII masked, FK→INPROFSAL, responsable diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico CIE-10 u estándar; tipo CHAR(4) masked, FK→INDIAGNOS, identificación enfermedad/condición clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/cédula del paciente; tipo VARCHAR(25) PII masked Identification_Ofuscado, FK→INPACIENT, identidad único asegurado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/admisión hospitalaria; tipo CHAR(10), FK→ADINGRESO, identifica episodio de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código unidad funcional (servicio, área clínica); tipo CHAR(10), FK→INUNIFUNC, departamento responsable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código centro de atención (hospital, clínica, IPS); tipo CHAR(10), FK→ADCENATEN, institución donde se registra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio/historia clínica; tipo NCHAR(10), parte PK, referencia expediente paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno tipo historia clínica; tipo CHAR(9), clasificación documento clínico (urgencia, consulta, hospitalización).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnósticos registrados en la historia clínica de cada ingreso hospitalario. Guarda todos los diagnósticos (principal, secundarios, de ingreso y egreso) asignados por un profesional de salud a un paciente durante una atención, incluyendo tipo, clasificación CIE-10, observaciones, estadificación oncológica TNM y datos de seguimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH';

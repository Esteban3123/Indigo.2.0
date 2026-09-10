CREATE TABLE [dbo].[HCRADDOSISBRAQUI] (
    [ID]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADORDEN]          INT             NOT NULL,
    [FECHAREGISTRO]         DATETIME        NOT NULL,
    [USUARIOREGISTRO]       CHAR (20)       NOT NULL,
    [TOTALDOSIS]            DECIMAL (18, 2) NOT NULL,
    [CODESPECI]             CHAR (3)        NOT NULL,
    [ESTADOSESION]          TINYINT         NOT NULL,
    [OBSERVACION]           VARCHAR (MAX)   NULL,
    [TIPOBRAQUITERAPIA]     INT             NULL,
    [VOLUTRATAMIEN]         INT             NULL,
    [IDAGEQUIPTRA]          INT             NULL,
    [DOSISPRESCRITA]        INT             NULL,
    [UBICACION]             TINYINT         NULL,
    [TIPORADIO]             TINYINT         NULL,
    [TIPOTRATA]             TINYINT         NULL,
    [TECNICARADIOTEAPIA146] TINYINT         NULL,
    [CARACTERISTICA]        TINYINT         NULL,
    [MOTIVOFINALIZACION]    TINYINT         NULL,
    [SALIDAFUENTE]          VARCHAR (50)    NULL,
    [DISTANCIA]             DECIMAL (5, 2)  NULL,
    [IDCITA]                INT             NOT NULL,
    [CODCENATEDOSISBRA]     CHAR (10)       NULL,
    CONSTRAINT [PK_HCRADDOSISBRAQUI] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADDOSISBRAQUI_ADCENATEN] FOREIGN KEY ([CODCENATEDOSISBRA]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCRADDOSISBRAQUI_AGEQUIPTRA] FOREIGN KEY ([IDAGEQUIPTRA]) REFERENCES [dbo].[AGEQUIPTRA] ([ID]),
    CONSTRAINT [FK_HCRADDOSISBRAQUI_HCRADORDEN] FOREIGN KEY ([IDHCRADORDEN]) REFERENCES [dbo].[HCRADORDEN] ([ID]),
    CONSTRAINT [FK_HCRADDOSISBRAQUI_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI])
);


GO




GO



GO


GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (ADCENATEN) donde se aplicó la dosis de braquiterapia; identificador del sitio de tratamiento radiante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'CODCENATEDOSISBRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del centro de Atencion donde se aplico la dosis de braquiterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'CODCENATEDOSISBRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'CODCENATEDOSISBRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la cita médica (AGASICITA) con la que se registró la sesión de braquiterapia; referencia a la consulta/atención del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'IDCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID cita medica con la que se registro la dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'IDCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'IDCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distancia en centímetros entre la fuente radiante y el punto de referencia dosimétrico durante la sesión de braquiterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'DISTANCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Distancia en centímetros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'DISTANCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'DISTANCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salida o tasa de dosis de la fuente radiante empleada en la sesión de braquiterapia; parámetro técnico de intensidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'SALIDAFUENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Salida de fuentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'SALIDAFUENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'SALIDAFUENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pregunta RIPS 96-105: motivo de cierre del tratamiento (1=Toxicidad, 2=Otros motivos médicos, 3=Muerte, 4=Cambio EAPB, 5=Decisión usuario, 6=Motivos administrativos, 7=Otras causas, 98=No aplica, 55=Aseguramiento territorial).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'MOTIVOFINALIZACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pregunta 96-105  1= Toxicidad  2= Otros motivos médicos  3= Muerte  4= Cambio de EAPB  5= Decisión del usuario  6= Otros motivos administrativos  7= Otras causas no contempladas  98= No aplica  55= Persona con aseguramiento (régimen subsidiado o contributivo y que no son PPNA) que recibió servicios de salud por parte del ente territorial durante el periodo de reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'MOTIVOFINALIZACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'MOTIVOFINALIZACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pregunta RIPS 95-104: estado de finalización del tratamiento (1=Completo, 2=Incompleto pero finalizado, 3=Esquema incompleto en curso, 98=No aplica, 55=Aseguramiento territorial).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'CARACTERISTICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pregunta (95-104):    1= Finalizado, dosis completa de radioterapia administrada  2= Finalizado, dosis incompleta pero finalizada por algún motivo  3= No finalizado, esquema incompleto, pero aún bajo tratamiento  98= No aplica  55= Persona con aseguramiento (régimen subsidiado o contributivo y que no son PPNA) que recibió servicios de salud por parte del ente territorial durante el periodo de reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'CARACTERISTICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'CARACTERISTICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pregunta RIPS 146: técnica de radioterapia utilizada (0=Ninguna, 1=Convencional 2D, 2=IMRT, 3=VMAT, 4=IGRT, 5=Conformacional 3D, 6=Estereotaxia, 7=Radiocirugia, 8=Interna/radiofármaco, 9=Externa sin técnica especificada).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'TECNICARADIOTEAPIA146';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Preguta 146. 0=No recibió radioterapia  1=Convencional 2D  2=IMRT (radioterapia con intensidad moderada)  3=VMAT (modulada voluntariamente)  4=IGRT (radioterapia guiada por imagen)  5=Conformacional 3D  6=Radioterapia estereotaxica  7=Radiocirugia  8=Otra radioterapia interna o radiofármaco (ejemplo braquiterapia, yodoterapia)  9=Radioterapia externa sin mención de la técnica en los soportes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'TECNICARADIOTEAPIA146';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'TECNICARADIOTEAPIA146';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pregunta RIPS 125: tipo de tratamiento oncológico vigente del paciente (1=Radioterapia, 2=Sistémica, 3=Cirugía, 4-11=Combinaciones, cuidados paliativos o seguimiento).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'TIPOTRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'pregunta 125. Tipo de tratamiento que está recibinedo el paciente a la fecha (125):    1= Radioterapia    2= Terapia sistémica (incluye quimioterapia, anticuerpos monoclonales, terapia biológica, terapia hormonal)    3= Cirugía    4= 1 y 2    5= 1 y 3    6= 2 y 3    7= Manejo expectante pretratamiento    8= En seguimiento, luego de tratamiento durante el periodo  9=Antecedente de cáncer (no recibió ningún tratamiento, pero tiene como mínimo una consulta de seguimiento relacionada con el cáncer dentro del periodo)    10=1, 2 y 3    11=Manejo de cuidado paliativo o terapia complementaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'TIPOTRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'TIPOTRATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de radioterapia aplicada (1=Externa terapéutica, 2=Interna/braquiterapia, 3=Profiláctica, 4-7=Combinaciones); clasificación por naturaleza de emisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'TIPORADIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de radioterapia (90,99):    1=Externa para tratar    2=Interna para tratar (incluye, braquiterapia, yodo radioactivo)  3=Profiláctica (por ejemplo a sistema nervioso central)    4=1 y 2    5=1 y 3    6=2 y 3    7=1,2 y 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'TIPORADIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'TIPORADIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pregunta RIPS 89,98: ubicación temporal del esquema terapéutico (1=Neoadyuvancia, 2=Inicial curativo, 3=Adyuvancia, 4=Paliativo inicial, 5-10=Manejo de recaídas curativo/paliativo).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'UBICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ubicación temporal de esquema (89,98) :    1=Neoadyuvancia (manejo  inicial prequirúrgico)    2=Tratamiento inicial curativo sin cirugía sugerida    3=Adyuvancia (manejo inicial postquirúrgico)    4=Manejo paliativo inicial    5=Manejo curativo de primera recaída    6=Manejo paliativo de primera recaída    7=Manejo curativo de segunda recaída    8=Manejo paliativo de segunda recaída    9=Manejo curativo de tercera recaída o posterior    10=Manejo paliativo de tercera recaída o posterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'UBICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'UBICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Punto de referencia dosimétrico prescrito (1=Isocentro, 2=Piel, 3=Isocentro+Piel, 4=Puntos A, 5=Mucosa vaginal, 6=Mucosa, 7=10mm, 8=5mm).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'DOSISPRESCRITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Isocentro  2 - Piel  3  - Isocentro y piel  4  - Puntos A  5 - Mucosa vaginal  6 - Mucosa   7 - 10mm  8 - 5mm', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'DOSISPRESCRITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'DOSISPRESCRITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del equipo/aparato de braquiterapia (AGEQUIPTRA) utilizado en la sesión; identificador técnico del dispositivo radiante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'IDAGEQUIPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID equipo de braquiterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'IDAGEQUIPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'IDAGEQUIPTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen de tratamiento en centímetros cúbicos o unidad radiante; región anatómica irradiada en la sesión de braquiterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'VOLUTRATAMIEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Volumen de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'VOLUTRATAMIEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'VOLUTRATAMIEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de braquiterapia aplicada (1=Intracavitaria, 2=Intraluminal, 3=Intersticial, 4=Superficial); clasificación por técnica de posicionamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'TIPOBRAQUITERAPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Intracavitaria  2 - Intraluminal  3 - Intersticial  4 - Superficial  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'TIPOBRAQUITERAPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'TIPOBRAQUITERAPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación clínica o técnica de la aplicación/sesión de dosis de braquiterapia; notas adicionales del procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación de aplicación de dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la sesión de braquiterapia (1=Continua en curso, 2=Finalizada); indicador de completitud de la sesión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'ESTADOSESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Continua  2 - Finalizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'ESTADOSESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'ESTADOSESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad médica (INESPECIA) responsable del tratamiento; rama oncológica o radioterapéutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de dosis absorbida (Gy/Gray) aplicada en la sesión de braquiterapia; dosis radiante administrada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'TOTALDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total de la dosis aplicada en la sesión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'TOTALDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'TOTALDOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario/profesional que registró la sesión de dosis en el sistema; identificador del operador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'USUARIOREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo usuario creo registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'USUARIOREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'USUARIOREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación/registro del documento de dosis de braquiterapia en el sistema; timestamp de ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la orden de braquiterapia (HCRADORDEN) que origina la sesión; referencia a la prescripción madre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'IDHCRADORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la orden de braquiterapia HCRADORDEN', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'IDHCRADORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'IDHCRADORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de la sesión de dosis de braquiterapia; clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de dosis de braquiterapia aplicadas a pacientes oncológicos. Guarda cada sesión de tratamiento con braquiterapia, incluyendo la dosis prescrita y administrada, técnica utilizada, tipo de radiación y observaciones clínicas del procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISBRAQUI';

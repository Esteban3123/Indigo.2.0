CREATE TABLE [dbo].[HCRADESQUEMAS] (
    [ID]                    INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADORDEN]          INT       NOT NULL,
    [DOSTOTAL]              INT       NOT NULL,
    [DOSISPORSESION]        INT       NOT NULL,
    [NUMSESION]             INT       NOT NULL,
    [PORDOSISAUTO]          INT       NOT NULL,
    [UBICACION]             TINYINT   NULL,
    [TIPORADIO]             TINYINT   NULL,
    [TIPOTRATA]             TINYINT   NULL,
    [ESTADO]                TINYINT   NOT NULL,
    [NUMINGRES]             CHAR (10) NOT NULL,
    [FECHACONFIR]           DATETIME  NULL,
    [USUCONFIR]             CHAR (20) NULL,
    [FECHAFINALIZA]         DATETIME  NULL,
    [USUFINALIZA]           CHAR (20) NULL,
    [FECHACREA]             DATETIME  NOT NULL,
    [USUCREA]               CHAR (20) NOT NULL,
    [FECHAMOD]              DATETIME  NULL,
    [USUMOD]                CHAR (20) NULL,
    [TECNICARADIOTEAPIA146] INT       NULL,
    [CODCENATEPLANEACION]   CHAR (10) NULL,
    CONSTRAINT [PK_HCRADESQUEMAS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADESQUEMAS_ADCENATEN] FOREIGN KEY ([CODCENATEPLANEACION]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCRADESQUEMAS_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCRADESQUEMAS_HCRADORDEN] FOREIGN KEY ([IDHCRADORDEN]) REFERENCES [dbo].[HCRADORDEN] ([ID])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (unidad funcional) donde se planifica y registra el esquema de radioterapia. FK→ADCENATEN. CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'CODCENATEPLANEACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del centro de Atención el cual se registra cuando se crea la creación del Esquema.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'CODCENATEPLANEACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'CODCENATEPLANEACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Técnica de radioterapia aplicada: 0=Sin radioterapia, 1=Convencional 2D, 2=IMRT, 3=VMAT, 4=IGRT, 5=Conformacional 3D, 6=Estereotáxica, 7=Radiocirugia, 8=Interna/Radiofármaco (braquiterapia, yodoterapia), 9=Externa sin especificación. TINYINT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'TECNICARADIOTEAPIA146';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'0=No recibió radioterapia  1=Convencional 2D  2=IMRT (radioterapia con intensidad moderada)  3=VMAT (modulada voluntariamente)  4=IGRT (radioterapia guiada por imagen)  5=Conformacional 3D  6=Radioterapia estereotaxica  7=Radiocirugia  8=Otra radioterapia interna o radiofármaco (ejemplo braquiterapia, yodoterapia)  9=Radioterapia externa sin mención de la técnica en los soportes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'TECNICARADIOTEAPIA146';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'TECNICARADIOTEAPIA146';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del esquema de radioterapia. CHAR(20), auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'USUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'USUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'USUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro del esquema. DATETIME, auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'FECHAMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'FECHAMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'FECHAMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro del esquema de radioterapia. CHAR(20), auditoría obligatoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'USUCREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'USUCREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'USUCREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del esquema. DATETIME, auditoría obligatoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'FECHACREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'FECHACREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'FECHACREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que finalizó (cerró) el esquema de tratamiento oncológico. CHAR(20), auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'USUFINALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario finalizo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'USUFINALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'USUFINALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de finalización del esquema de radioterapia. DATETIME, cuando se marca como completado o finalizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'FECHAFINALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Finalizacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'FECHAFINALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'FECHAFINALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que confirmó la validez y aprobación del esquema de radioterapia. CHAR(20), auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'USUCONFIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario de confirmacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'USUCONFIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'USUCONFIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de confirmación (aprobación) del esquema por profesional autorizado. DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'FECHACONFIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de confirmacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'FECHACONFIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'FECHACONFIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso oncológico del paciente en tratamiento. FK→ADINGRESO. CHAR(10), PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de ingreso de tipo oncologico cuando se creo el esquema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del esquema: 1=Sin confirmar, 2=Confirmada, 3=Iniciado, 4=Finalizado, 5=Anulado, 6=Completado. TINYINT, workflow de radioterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Sin confirmar  2 - Confirmada  3 - Iniciado  4 - Finalizado  5 - Anulado  6 - Completado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de tratamiento oncológico recibido: 1=Radioterapia, 2=Sistémica (quimio/anticuerpos/biológica/hormonal), 3=Cirugía, 4=Radioteraupia+Sistémica, 5=Radioterapia+Cirugía, 6=Sistémica+Cirugía, 7=Expectante, 8=Seguimiento post-tratamiento, 9=Antecedente sin tratamiento, 10=Todos, 11=Paliativo. TINYINT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'TIPOTRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de tratamiento que está recibinedo el paciente a la fecha (125):    1= Radioterapia    2= Terapia sistémica (incluye quimioterapia, anticuerpos monoclonales, terapia biológica, terapia hormonal)    3= Cirugía    4= 1 y 2    5= 1 y 3    6= 2 y 3    7= Manejo expectante pretratamiento    8= En seguimiento, luego de tratamiento durante el periodo  9=Antecedente de cáncer (no recibió ningún tratamiento, pero tiene como mínimo una consulta de seguimiento relacionada con el cáncer dentro del periodo)    10=1, 2 y 3    11=Manejo de cuidado paliativo o terapia complementaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'TIPOTRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'TIPOTRATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modalidad de radioterapia: 1=Externa curativa, 2=Interna/Braquiterapia curativa, 3=Profiláctica (SNC), 4=Externa+Interna, 5=Externa+Profiláctica, 6=Interna+Profiláctica, 7=Todas las modalidades. TINYINT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'TIPORADIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de radioterapia (90,99):    1=Externa para tratar    2=Interna para tratar (incluye, braquiterapia, yodo radioactivo)  3=Profiláctica (por ejemplo a sistema nervioso central)    4=1 y 2    5=1 y 3    6=2 y 3    7=1,2 y 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'TIPORADIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'TIPORADIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contexto clínico del esquema: 1=Neoadyuvancia (pre-cirugía), 2=Curativo sin cirugía, 3=Adyuvancia (post-cirugía), 4=Paliativo inicial, 5-10=Manejo de recaídas (curativo/paliativo, primera/segunda/tercera+). TINYINT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'UBICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ubicación temporal de esquema (89,98) :    1=Neoadyuvancia (manejo  inicial prequirúrgico)    2=Tratamiento inicial curativo sin cirugía sugerida    3=Adyuvancia (manejo inicial postquirúrgico)    4=Manejo paliativo inicial    5=Manejo curativo de primera recaída    6=Manejo paliativo de primera recaída    7=Manejo curativo de segunda recaída    8=Manejo paliativo de segunda recaída    9=Manejo curativo de tercera recaída o posterior    10=Manejo paliativo de tercera recaída o posterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'UBICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'UBICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de dosis autorizado respecto al esquema planeado. INT, control de administración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'PORDOSISAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje dosis autorizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'PORDOSISAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'PORDOSISAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de sesiones de radioterapia programadas en el esquema. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'NUMSESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Sesiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'NUMSESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'NUMSESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis de radiación administrada por sesión (Gray o cGy). INT, planificación dosimétrica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'DOSISPORSESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis por sesion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'DOSISPORSESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'DOSISPORSESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis total acumulada de radiación en centigrays (cGy) para el esquema completo. INT, planificación dosimétrica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'DOSTOTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis total centigray', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'DOSTOTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'DOSTOTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden de radioterapia o braquiterapia padre. FK→HCRADORDEN. INT, relación 1:N con sesiones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'IDHCRADORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la orden de radio o braquiterapia (HCRADORDEN)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'IDHCRADORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'IDHCRADORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único auto-incremental del esquema de radioterapia. INT IDENTITY, PK.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Esquemas de radioterapia asociados a órdenes médicas de un ingreso. Registra la configuración de cada esquema de tratamiento radioterápico: dosis, sesiones, técnica, ubicación anatómica y ciclo de vida del esquema (creación, confirmación y finalización).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADESQUEMAS';

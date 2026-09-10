CREATE TABLE [dbo].[HCTRASLADOS] (
    [ID]                       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCSOLTRASLADOSINT]      INT           NULL,
    [IDHCREFCONP]              INT           NULL,
    [TIPOTRASLADO]             INT           NOT NULL,
    [ESTADO]                   INT           NOT NULL,
    [USUARIOTRASLADO]          CHAR (20)     NOT NULL,
    [UFUCODIGOORTRASLADO]      CHAR (10)     NOT NULL,
    [CODCENATEDETRASLADO]      CHAR (10)     NOT NULL,
    [UFUCODIGODETRASLADO]      CHAR (10)     NOT NULL,
    [FECHATRASLADO]            DATETIME      NOT NULL,
    [OBSERVACIONTRASLADO]      VARCHAR (MAX) NULL,
    [IDRCVEHICTRAS]            INT           NOT NULL,
    [FECHAANULA]               DATETIME      NULL,
    [USUARIOANULA]             CHAR (20)     NULL,
    [IDHCMOANULA]              CHAR (4)      NULL,
    [JUSTIFICAANULA]           VARCHAR (MAX) NULL,
    [IDRCVEHICTRASINICIO]      INT           NULL,
    [TIPOAMBULANCIA]           INT           NULL,
    [OTROTRASPORTEASISTENCIAL] VARCHAR (100) NULL,
    [MEDIOTRASPORTE]           INT           NULL,
    [IDRCEMPTRANS]             INT           NULL,
    [OTRAEMPRESATRASPORTADORA] VARCHAR (100) NULL,
    [NOMBREQUIENRECIBE]        VARCHAR (100) NULL,
    [CARGO]                    INT           NULL,
    [OTROCARGO]                VARCHAR (100) NULL,
    [FECHAINICIOTRASLADO]      DATETIME      NULL,
    CONSTRAINT [PK_HCTRASLADOS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCTRASLADOS_ADCENATEN] FOREIGN KEY ([CODCENATEDETRASLADO]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCTRASLADOS_HCMOANULB] FOREIGN KEY ([IDHCMOANULA]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_HCTRASLADOS_HCSOLTRASLADOSINT] FOREIGN KEY ([IDHCSOLTRASLADOSINT]) REFERENCES [dbo].[HCSOLTRASLADOSINT] ([ID]),
    CONSTRAINT [FK_HCTRASLADOS_HCTRASLADOS] FOREIGN KEY ([ID]) REFERENCES [dbo].[HCTRASLADOS] ([ID]),
    CONSTRAINT [FK_HCTRASLADOS_HCTRASLADOS1] FOREIGN KEY ([ID]) REFERENCES [dbo].[HCTRASLADOS] ([ID]),
    CONSTRAINT [FK_HCTRASLADOS_INUNIFUNC] FOREIGN KEY ([UFUCODIGODETRASLADO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCTRASLADOS_INUNIFUNC1] FOREIGN KEY ([UFUCODIGOORTRASLADO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCTRASLADOS_RCEMPTRANS] FOREIGN KEY ([IDRCEMPTRANS]) REFERENCES [dbo].[RCEMPTRANS] ([ID]),
    CONSTRAINT [FK_HCTRASLADOS_RCVEHICTRAS] FOREIGN KEY ([IDRCVEHICTRAS]) REFERENCES [dbo].[RCVEHICTRAS] ([Id])
);




GO



GO



GO



GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio del traslado asistencial del paciente; timestamp DATETIME que registra cuándo comenzó el movimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'FECHAINICIOTRASLADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicio de traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'FECHAINICIOTRASLADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'FECHAINICIOTRASLADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de cargo diferente a auxiliar, enfermero, médico general o especialista; VARCHAR(100) para profesionales de salud no catalogados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'OTROCARGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otro cargo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'OTROCARGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'OTROCARGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cargo/profesión de quien recibe el paciente en destino: 1=Auxiliar, 2=Enfermero(a), 3=Médico general, 4=Médico especialista, 5=Otro; INT catálogo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'CARGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cargo de la persona que recibe   1 -  Auxiliar  2 - Enfermero(a)  3 - Médico general  4 - Médico especialista  5 - Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'CARGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'CARGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del profesional de salud o personal que recibe al paciente en la unidad funcional destino; VARCHAR(100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'NOMBREQUIENRECIBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la persona quien recibe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'NOMBREQUIENRECIBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'NOMBREQUIENRECIBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de empresa transportadora asistencial externa no catalogada en RCEMPTRANS; VARCHAR(100), alternativa cuando IDRCEMPTRANS es nulo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'OTRAEMPRESATRASPORTADORA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otras empresas trasportadora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'OTRAEMPRESATRASPORTADORA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'OTRAEMPRESATRASPORTADORA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador PK de empresa transportadora asistencial (FK → RCEMPTRANS.ID); INT, referencia a prestador de transporte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'IDRCEMPTRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id empresas transportadoras', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'IDRCEMPTRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'IDRCEMPTRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vía de traslado del paciente: 1=Terrestre, 2=Marítimo/fluvial, 3=Aéreo; INT catálogo para modalidad de transporte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'MEDIOTRASPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Terrestre  2 - Maritimo y/o fluvial  3 - Aéreo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'MEDIOTRASPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'MEDIOTRASPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de medio de transporte asistencial no estándar (no ambulancia); VARCHAR(100), complementa MEDIOTRASPORTE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'OTROTRASPORTEASISTENCIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda otros trasporte Asistencial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'OTROTRASPORTEASISTENCIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'OTROTRASPORTEASISTENCIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Equipamiento de ambulancia: 1=Básico (soporte vital básico), 2=Medicalizado (soporte vital avanzado); INT catálogo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'TIPOAMBULANCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Básico  2 - Medicalizado  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'TIPOAMBULANCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'TIPOAMBULANCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del vehículo ambulancia asistencial al inicio del traslado; INT, FK → RCVEHICTRAS.Id para trazabilidad de flota.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'IDRCVEHICTRASINICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id vehiculo trasporte  inicio  Ambulancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'IDRCVEHICTRASINICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'IDRCVEHICTRASINICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo detallado de cancelación del traslado; VARCHAR(MAX), documentación PII sensible, requiere auditoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'JUSTIFICAANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion del motivo de anulación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'JUSTIFICAANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'JUSTIFICAANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de motivo de anulación predefinido (FK → HCMOANULB.CODMOTANU); CHAR(4), catálogo de razones de cancelación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'IDHCMOANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Motivo de anulación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'IDHCMOANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'IDHCMOANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/login del profesional que canceló el traslado; CHAR(20), auditoria de cambios, PII_Auditoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'USUARIOANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que Anula', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'USUARIOANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'USUARIOANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de cancelación del traslado; DATETIME NULL, timestamp de anulación si aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'FECHAANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Anulación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'FECHAANULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'FECHAANULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del vehículo ambulancia asistencial usado en el traslado (FK → RCVEHICTRAS.Id); INT, relación con flota de transporte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'IDRCVEHICTRAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de Vehiculos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'IDRCVEHICTRAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'IDRCVEHICTRAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas clínicas o administrativas sobre el traslado del paciente; VARCHAR(MAX), comentarios de atención prehospitalaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'OBSERVACIONTRASLADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion del traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'OBSERVACIONTRASLADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'OBSERVACIONTRASLADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del traslado del paciente; DATETIME NOT NULL, timestamp principal de evento asistencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'FECHATRASLADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'FECHATRASLADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'FECHATRASLADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Unidad Funcional destino donde se completó el traslado (FK → INUNIFUNC.UFUCODIGO); CHAR(10), área de atención final.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'UFUCODIGODETRASLADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad Funcional destino cuando se realizo el traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'UFUCODIGODETRASLADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'UFUCODIGODETRASLADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención destino donde ingresó el paciente trasladado (FK → ADCENATEN.CODCENATE); CHAR(10), sede receptora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'CODCENATEDETRASLADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro Atencón destino cuando se realizo el traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'CODCENATEDETRASLADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'CODCENATEDETRASLADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Unidad Funcional origen desde donde se originó el traslado (FK → INUNIFUNC.UFUCODIGO); CHAR(10), área de procedencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'UFUCODIGOORTRASLADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad Funcional orgien cuando se realizo el traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'UFUCODIGOORTRASLADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'UFUCODIGOORTRASLADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/login del profesional que realizó o asignó el traslado asistencial y ambulancia; CHAR(20), auditoria de movimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'USUARIOTRASLADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que realizo el traslado ó asignación de la ambuancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'USUARIOTRASLADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'USUARIOTRASLADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del traslado: 1=En Proceso (activo), 2=Anulada (cancelado); INT catálogo, indica ciclo de vida del evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - En Proceso  2 - Anulada ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modalidad del traslado: 1=Básico (simple), 2=Redondo (ida y retorno); INT catálogo, caracteriza ruta asistencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'TIPOTRASLADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Basico  2 - redondo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'TIPOTRASLADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'TIPOTRASLADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de referencia/contrarreferencia asociada al traslado (FK → tabla de referencias); INT, vincula solicitud de derivación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'IDHCREFCONP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guiarda el Id de refrencia contra referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'IDHCREFCONP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'IDHCREFCONP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de solicitud de traslado interno del paciente (FK → HCSOLTRASLADOSINT.ID); INT, trazabilidad de solicitud origen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'IDHCSOLTRASLADOSINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Solicitud de traslado interno ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'IDHCSOLTRASLADOSINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'IDHCSOLTRASLADOSINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de registro de traslado asistencial; INT IDENTITY PRIMARY KEY, clave única de evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de traslados de pacientes entre unidades funcionales o centros de atención, incluyendo el tipo de traslado, medio de transporte, vehículo, empresa transportadora, estado del traslado y datos de anulación cuando aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTRASLADOS';

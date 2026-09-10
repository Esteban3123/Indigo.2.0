CREATE TABLE [dbo].[HCFICHA100] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [FECHACCIDENTE]       DATE          NULL,
    [DIRACCIDENTE]        VARCHAR (200) NULL,
    [ACTIVIDADACCI]       INT           NULL,
    [OTRACTIVIDAD]        VARCHAR (200) NULL,
    [TIPOATENCION]        INT           NULL,
    [OTRATENCION]         VARCHAR (200) NULL,
    [SOMETIDAPRAC]        INT           NULL,
    [OTRAPRACTICA]        VARCHAR (200) NULL,
    [LOCAMORDEDURA]       INT           NULL,
    [HUELLACOLMILLO]      BIT           NULL,
    [VIOSERPIENTE]        BIT           NULL,
    [SERPCAPTURADA]       BIT           NULL,
    [AGENGENERO]          INT           NULL,
    [OTROAGENGENE]        VARCHAR (200) NULL,
    [AGENNOMBRE]          INT           NULL,
    [OTROAGENNOM]         VARCHAR (200) NULL,
    [EDEMA]               BIT           NULL,
    [DOLOR]               BIT           NULL,
    [ERITEMA]             BIT           NULL,
    [FLICTENAS]           BIT           NULL,
    [PARESTESIAS]         BIT           NULL,
    [EQUIMOSIS]           BIT           NULL,
    [HEMATOMAS]           BIT           NULL,
    [OTRAMANILOCAL]       BIT           NULL,
    [CUALOTRAML]          VARCHAR (200) NULL,
    [NAUSEA]              BIT           NULL,
    [HIPOTENSION]         BIT           NULL,
    [DEBILIDADMUSCU]      BIT           NULL,
    [HEMATEMESIS]         BIT           NULL,
    [DIFIHABLAR]          BIT           NULL,
    [VOMITO]              BIT           NULL,
    [DOLORABDOMI]         BIT           NULL,
    [OLIGURIA]            BIT           NULL,
    [HEMATURINA]          BIT           NULL,
    [DISFAGIA]            BIT           NULL,
    [SIALORREA]           BIT           NULL,
    [FASCIESNEU]          BIT           NULL,
    [CIANOSIS]            BIT           NULL,
    [HEMATOQUEXIA]        BIT           NULL,
    [DIARREA]             BIT           NULL,
    [ALTERAVISION]        BIT           NULL,
    [EPISTAXIS]           BIT           NULL,
    [VERTIGO]             BIT           NULL,
    [BRADICARDIA]         BIT           NULL,
    [ALTERASENSO]         BIT           NULL,
    [GINGIVORRAGIA]       BIT           NULL,
    [PTOSISPALPEBRAL]     BIT           NULL,
    [OTRAMANISISTE]       BIT           NULL,
    [CUALOTRAMS]          VARCHAR (200) NULL,
    [CELULITIS]           BIT           NULL,
    [ABSCESO]             BIT           NULL,
    [NECROSIS]            BIT           NULL,
    [MIONECROSIS]         BIT           NULL,
    [FASCEITIS]           BIT           NULL,
    [ALTERACIRCU]         BIT           NULL,
    [SINDROMECOM]         BIT           NULL,
    [OTRACOMLILOCA]       BIT           NULL,
    [CUALOTRACL]          VARCHAR (200) NULL,
    [ANEMIAGUDA]          BIT           NULL,
    [EDEMACEREBRAL]       BIT           NULL,
    [SHOCKHIPO]           BIT           NULL,
    [FALLAVENTILA]        BIT           NULL,
    [SHOCKSEPTICO]        BIT           NULL,
    [COMA]                BIT           NULL,
    [IRA]                 BIT           NULL,
    [CID]                 BIT           NULL,
    [HEMORRAGIAIN]        BIT           NULL,
    [OTRACOMPSISTE]       BIT           NULL,
    [CUALOTRACS]          VARCHAR (200) NULL,
    [GRACEDADACCI]        INT           NULL,
    [EMPLEOSUERO]         BIT           NULL,
    [TIEMPOTRANS]         NUMERIC (18)  NULL,
    [TIPOSUERO]           INT           NULL,
    [REACCIONES]          INT           NULL,
    [DOSISUERO]           NUMERIC (18)  NULL,
    [TIEMPOADM]           CHAR (5)      NULL,
    [REMITIDO]            BIT           NULL,
    [TRATAMIENTOQX]       BIT           NULL,
    [TIPOTRATAQX]         INT           NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [FABRICANTE]          INT           NULL,
    [OTROFABRICANTE]      VARCHAR (MAX) NULL,
    [LOTE]                VARCHAR (15)  NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    [TIEMPOTRANSDIAS]     INT           NULL,
    CONSTRAINT [PK_HCFICHA100] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA100_JSON] CHECK (isjson([JSON])=(1))
);


GO
ALTER TABLE [dbo].[HCFICHA100] NOCHECK CONSTRAINT [CK_HCFICHA100_JSON];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo transcurrido en días desde accidente ofídico a atención. Tipo: INT. Cálculo derivado para análisis de demora en urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TIEMPOTRANSDIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo Días', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TIEMPOTRANSDIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TIEMPOTRANSDIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacenamiento de nuevas columnas en formato JSON. Tipo: VARCHAR(MAX). Validado con CHECK CONSTRAINT isjson(). Extensibilidad de datos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión del formulario de notificación ofídica. Tipo: VARCHAR(20). NULL = primera versión. Control de cambios en estructura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de lote del suero antiofídico administrado. Tipo: VARCHAR(15). Trazabilidad y fabricación del biológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'LOTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lote que escribio ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'LOTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'LOTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especificación de otro fabricante de suero si no está en catálogo estándar. Tipo: VARCHAR(MAX). Campo libre complementario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTROFABRICANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cual otro fabricante ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTROFABRICANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTROFABRICANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fabricante del suero antiofídico: 1=Probiol, 2=Bioclon, 3=INS, 4=Otro. Tipo: INT. Trazabilidad y control de calidad del biológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'FABRICANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fabricante   1=Probiol  2=Bioclon  3=INS (Instituto Nacional de salud)  4=Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'FABRICANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'FABRICANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico CIE-10 del envenenamiento ofídico. Tipo: CHAR(4). Clasificación clínica y epidemiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Diagnostico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de intervención quirúrgica realizada: 1=Drenaje absceso, 2=Limpieza quirúrgica, 3=Desbridamiento, 4=Fasciotomía, 5=Injerto piel, 6=Amputación. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TIPOTRATAQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de tratamiento quirúrgico  1=Drenaje de absceso  2=Limpieza quirúrgica  3=Desbridamiento  4=Fasciotomía  5=Injerto de piel  6=Amputación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TIPOTRATAQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TIPOTRATAQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de intervención quirúrgica por envenenamiento ofídico. Tipo: BIT (True=Sí, False=No). Procedimiento de urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TRATAMIENTOQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tratamiento quirúrgico  True = Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TRATAMIENTOQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TRATAMIENTOQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Paciente remitido a otra institución de mayor complejidad. Tipo: BIT (True=Sí, False=No). Red de referencia y contrarreferencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'REMITIDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Remitido a otra institución  True = Si   False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'REMITIDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'REMITIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo de administración del suero antiofídico. Tipo: CHAR(5). Antiguo formato horas-minutos, desde V1_2020-03-06 en horas. Hito crítico terapéutico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TIEMPOADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(antigua version) --> Tiempo de administración de suero (Horas-Minutos)      /    (desde version V1_2020-03-06) --> Tiempo de administración de suero (Horas)   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TIEMPOADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TIEMPOADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de ampollas de suero antiofídico administradas. Tipo: NUMERIC(18). Dosificación según gravedad y género serpiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DOSISUERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis de suero (ampollas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DOSISUERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DOSISUERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reacciones adversas a suero antiofídico: 1=Ninguna, 2=Localizada, 3=Generalizada. Tipo: INT. Farmacovigilancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'REACCIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Reacciones a la aplicación del suero  1=Ninguna  2=Localizada  3=Generalizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'REACCIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'REACCIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de suero antiofídico: 1=Antiviperino (Bothrops/Lachesis/Crotalus), 2=Anti-elapidídico (Micrurus/coral). Tipo: INT. Especificidad según agente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TIPOSUERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de suero antiofídico -->     1 = Antiviperido (Bothrops, Lachesis, Crotálus)   2 = Anti-elapidídico (Micrurus sp: coral verdadera)    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TIPOSUERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TIPOSUERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo transcurrido en horas desde accidente a atención médica. Tipo: NUMERIC(18). Indicador crítico de demora prehospitalaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TIEMPOTRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo transcurrido (h)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TIEMPOTRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TIEMPOTRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Administración de suero antiofídico en urgencia. Tipo: BIT (True=Sí, False=No). Intervención terapéutica específica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'EMPLEOSUERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Empleó Suero  true = Si  False = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'EMPLEOSUERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'EMPLEOSUERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gravedad clínica del envenenamiento: 1=Leve, 2=Moderado, 3=Grave, 4=No envenenamiento. Tipo: INT. Escala de severidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'GRACEDADACCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Gravedad del accidente  1=Leve  2=Moderado  3=Grave  4=No envenenamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'GRACEDADACCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'GRACEDADACCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción libre de otras complicaciones sistémicas no listadas. Tipo: VARCHAR(200). Campo complementario de eventos adversos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CUALOTRACS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cual otro Complicaciones sistémicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CUALOTRACS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CUALOTRACS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de complicaciones sistémicas adicionales. Tipo: BIT. Presencia de manifestaciones generales no clasificadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRACOMPSISTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones sistémicas  Si   Seleccionado OTRACOMPSISTE= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRACOMPSISTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRACOMPSISTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicación sistémica: hemorragia interna/coagulopatía. Tipo: BIT. Manifestación grave sistémica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HEMORRAGIAIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones sistémicas  Si   Seleccionado HEMORRAGIAIN= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HEMORRAGIAIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HEMORRAGIAIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicación sistémica: coagulación intravascular diseminada. Tipo: BIT. Evento crítico de coagulación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones sistémicas  Si   Seleccionado CID= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicación sistémica: insuficiencia renal aguda. Tipo: BIT. Fallo orgánico por envenenamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'IRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones sistémicas  Si   Seleccionado IRA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'IRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'IRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicación sistémica: pérdida de conciencia/coma. Tipo: BIT. Manifestación neurológica grave.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'COMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones sistémicas  Si   Seleccionado COMA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'COMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'COMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicación sistémica: choque séptico secundario. Tipo: BIT. Complicación infecciosa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'SHOCKSEPTICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones sistémicas  Si   Seleccionado SHOCKSEPTICO= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'SHOCKSEPTICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'SHOCKSEPTICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicación sistémica: falla ventilatoria/parálisis respiratoria. Tipo: BIT. Compromiso respiratorio crítico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'FALLAVENTILA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones sistémicas  Si   Seleccionado FALLAVENTILA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'FALLAVENTILA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'FALLAVENTILA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicación sistémica: choque hipovolémico/hipotensión. Tipo: BIT. Inestabilidad hemodinámica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'SHOCKHIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones sistémicas  Si   Seleccionado SHOCKHIPO= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'SHOCKHIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'SHOCKHIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicación sistémica: edema cerebral. Tipo: BIT. Manifestación neurológica grave.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'EDEMACEREBRAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones sistémicas  Si   Seleccionado EDEMACEREBRAL= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'EDEMACEREBRAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'EDEMACEREBRAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicación sistémica: anemia aguda por hemorragia. Tipo: BIT. Pérdida hemática importante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ANEMIAGUDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones sistémicas  Si   Seleccionado ANEMIAGUDA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ANEMIAGUDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ANEMIAGUDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción libre de otras complicaciones locales no listadas. Tipo: VARCHAR(200). Campo complementario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CUALOTRACL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cual otro Complicaciones locales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CUALOTRACL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CUALOTRACL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de complicaciones locales no clasificadas. Tipo: BIT. Otras manifestaciones tisulares.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRACOMLILOCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones locales  Si   Seleccionado OTRACOMLILOCA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRACOMLILOCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRACOMLILOCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicación local: síndrome compartamental. Tipo: BIT. Presión tisular elevada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'SINDROMECOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones locales  Si   Seleccionado SINDROMECOM= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'SINDROMECOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'SINDROMECOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicación local: alteración circulatoria/trombosis. Tipo: BIT. Compromiso vascular local.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ALTERACIRCU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones locales  Si   Seleccionado ALTERACIRCU= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ALTERACIRCU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ALTERACIRCU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicación local: fasceítis necrotizante. Tipo: BIT. Infección profunda agresiva.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'FASCEITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones locales  Si   Seleccionado FASCEITIS= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'FASCEITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'FASCEITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicación local: necrosis muscular. Tipo: BIT. Muerte tisular muscular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'MIONECROSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones locales  Si   Seleccionado MIONECROSIS= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'MIONECROSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'MIONECROSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicación local: necrosis tisular en sitio mordedura. Tipo: BIT. Muerte celular extensa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'NECROSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones locales  Si   Seleccionado NECROSIS= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'NECROSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'NECROSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicación local: absceso/colección purulenta. Tipo: BIT. Infección localizada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ABSCESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones locales  Si   Seleccionado ABSCESO= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ABSCESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ABSCESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicación local: celulitis en sitio mordedura. Tipo: BIT. Inflamación difusa aguda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CELULITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complicaciones locales  Si   Seleccionado CELULITIS= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CELULITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CELULITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción libre de otras manifestaciones sistémicas no listadas. Tipo: VARCHAR(200). Campo complementario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CUALOTRAMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cual otro Manifestaciones sistemicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CUALOTRAMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CUALOTRAMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de manifestaciones sistémicas no clasificadas. Tipo: BIT. Otros síntomas generales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRAMANISISTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones sistémicas  Si     Seleccionado OTRAMANISISTE= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRAMANISISTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRAMANISISTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación sistémica: ptosis palpebral/parálisis oculomotora. Tipo: BIT. Afección neuromuscular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'PTOSISPALPEBRAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones sistémicas  Si     Seleccionado PTOSISPALPEBRAL= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'PTOSISPALPEBRAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'PTOSISPALPEBRAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación sistémica: sangrado de encías. Tipo: BIT. Alteración coagulativa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'GINGIVORRAGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones sistémicas  Si     Seleccionado GINGIVORRAGIA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'GINGIVORRAGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'GINGIVORRAGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación sistémica: alteración sensibilidad/parestesias. Tipo: BIT. Afección neurológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ALTERASENSO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones sistémicas  Si     Seleccionado ALTERASENSO= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ALTERASENSO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ALTERASENSO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación sistémica: bradicardia/ralentización cardiaca. Tipo: BIT. Alteración ritmo cardiaco.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'BRADICARDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones sistémicas  Si     Seleccionado BRADICARDIA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'BRADICARDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'BRADICARDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación sistémica: vértigo/mareo. Tipo: BIT. Síntoma neurovegetativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'VERTIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones sistémicas  Si     Seleccionado VERTIGO= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'VERTIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'VERTIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación sistémica: epistaxis/sangrado nasal. Tipo: BIT. Hemorragia mucosa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'EPISTAXIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones sistémicas  Si     Seleccionado EPISTAXIS= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'EPISTAXIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'EPISTAXIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación sistémica: alteración visual/visión borrosa. Tipo: BIT. Afección ocular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ALTERAVISION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones sistémicas  Si     Seleccionado ALTERAVISION= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ALTERAVISION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ALTERAVISION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación sistémica: diarrea. Tipo: BIT. Síntoma gastrointestinal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DIARREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones sistémicas  Si     Seleccionado DIARREA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DIARREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DIARREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación sistémica: hematoquecia/sangrado rectal. Tipo: BIT. Hemorragia digestiva baja.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HEMATOQUEXIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones sistémicas  Si     Seleccionado HEMATOQUEXIA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HEMATOQUEXIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HEMATOQUEXIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación sistémica: cianosis/coloración azulada. Tipo: BIT. Hipoxia periférica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CIANOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones sistémicas  Si     Seleccionado CIANOSIS= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CIANOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CIANOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación sistémica: fascies neurítica/alteración facial. Tipo: BIT. Cambio expresión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'FASCIESNEU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones sistémicas  Si     Seleccionado FASCIESNEU= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'FASCIESNEU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'FASCIESNEU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación sistémica: salivación excesiva. Tipo: BIT. Hipersecreción salival.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'SIALORREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones sistémicas  Si     Seleccionado SIALORREA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'SIALORREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'SIALORREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación sistémica: dificultad para tragar. Tipo: BIT. Afección deglutoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DISFAGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones sistémicas  Si     Seleccionado DISFAGIA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DISFAGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DISFAGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación sistémica: hematuria/sangre en orina. Tipo: BIT. Hemorragia urinaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HEMATURINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones sistémicas  Si     Seleccionado HEMATURINA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HEMATURINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HEMATURINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación sistémica: oliguria/disminución orina. Tipo: BIT. Fallo renal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OLIGURIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones sistémicas  Si     Seleccionado OLIGURIA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OLIGURIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OLIGURIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación sistémica: dolor abdominal. Tipo: BIT. Síntoma visceral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DOLORABDOMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones sistémicas  Si     Seleccionado DOLORABDOMI= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DOLORABDOMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DOLORABDOMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación sistémica: vómito/náusea. Tipo: BIT. Síntoma gastrointestinal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'VOMITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones sistémicas  Si     Seleccionado VOMITO= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'VOMITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'VOMITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación sistémica: dificultad para hablar. Tipo: BIT. Afección fonoarticulatoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DIFIHABLAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones sistémicas  Si     Seleccionado DIFIHABLAR= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DIFIHABLAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DIFIHABLAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación sistémica: hematemesis/vómito hemático. Tipo: BIT. Hemorragia digestiva alta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HEMATEMESIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones sistémicas  Si     Seleccionado HEMATEMESIS= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HEMATEMESIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HEMATEMESIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación sistémica: debilidad muscular/mialgia. Tipo: BIT. Afección neuromuscular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DEBILIDADMUSCU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones sistémicas  Si     Seleccionado DEBILIDADMUSCU= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DEBILIDADMUSCU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DEBILIDADMUSCU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación sistémica: hipotensión arterial. Tipo: BIT. Inestabilidad hemodinámica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HIPOTENSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones sistémicas  Si     Seleccionado HIPOTENSION= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HIPOTENSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HIPOTENSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación sistémica: náusea/malestar gastrointestinal. Tipo: BIT. Síntoma digestivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'NAUSEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones sistémicas  Si     Seleccionado NAUSEA= Truesi no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'NAUSEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'NAUSEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción libre de otras manifestaciones locales no listadas. Tipo: VARCHAR(200). Campo complementario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CUALOTRAML';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cual otra Manilocal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CUALOTRAML';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'CUALOTRAML';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de otras manifestaciones locales en sitio mordedura. Tipo: BIT. Síntomas adicionales locales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRAMANILOCAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones locales Item   Si   Seleccionado OTRAMANILOCAL= True  si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRAMANILOCAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRAMANILOCAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación local: hematoma/equimosis extensa. Tipo: BIT. Hemorragia subcutánea.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HEMATOMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones locales Item   Si   Seleccionado HEMATOMAS= True  si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HEMATOMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HEMATOMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación local: equimosis/morado. Tipo: BIT. Extravasación sanguínea.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'EQUIMOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones locales Item   Si   Seleccionado EQUIMOSIS= True  si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'EQUIMOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'EQUIMOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación local: parestesias/hormigueo en zona mordida. Tipo: BIT. Alteración sensorial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'PARESTESIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones locales Item   Si   Seleccionado PARESTESIAS= True  si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'PARESTESIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'PARESTESIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación local: flictenas/ampollas. Tipo: BIT. Lesión epidérmica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'FLICTENAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones locales Item   Si   Seleccionado FLICTENAS= True  si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'FLICTENAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'FLICTENAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación local: eritema/enrojecimiento. Tipo: BIT. Inflamación cutánea.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ERITEMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones locales Item   Si   Seleccionado Eritema= True  si no  Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ERITEMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ERITEMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación local: dolor en sitio de mordedura. Tipo: BIT. Síntoma local principal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DOLOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones locales Item   Si   Seleccionado Dolor= True  si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DOLOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DOLOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Manifestación local: edema/inflamación. Tipo: BIT. Aumento volumen local.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'EDEMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Manifestaciones locales Item   Si   Seleccionado Edema = True  si no Null', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'EDEMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'EDEMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especificación de otro nombre común de serpiente si no está catalogada. Tipo: VARCHAR(200). Campo libre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTROAGENNOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otro Age Nombre ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTROAGENNOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTROAGENNOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre común de la serpiente agresora (género ofídico). Tipo: INT. Identificación de especie.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'AGENNOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agente agresor, nombre común', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'AGENNOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'AGENNOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especificación de otro género de serpiente no listado. Tipo: VARCHAR(200). Campo complementario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTROAGENGENE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otro Age Genero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTROAGENGENE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTROAGENGENE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Género de la serpiente: 1=Bothrops, 2=Crotalus, 3=Micrurus, 4=Lachesis, 5=Pelamis, 6=Colubrido, 7=Sin identificar, 8=Otro. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'AGENGENERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agente agresor género:  1=Bothrops  2=Crotalus  3=Micrurus  4=lachesis  5=pelamis(serpiente de mar)  6=Colubrido  7=Sin identificar  8=Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'AGENGENERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'AGENGENERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Serpiente causante del accidente fue capturada para identificación. Tipo: BIT (True=Sí, False=No). Confirmación diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'SERPCAPTURADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Serpiente capturada  True = Sifalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'SERPCAPTURADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'SERPCAPTURADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Testigo presencial vio la serpiente que causó la mordedura. Tipo: BIT (True=Sí, False=No). Identificación visual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'VIOSERPIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Persona vió a serpiente que la mordió  True = Sifalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'VIOSERPIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'VIOSERPIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evidencia de huellas/marcas de colmillos venenosos. Tipo: BIT (True=Sí, False=No). Confirmación de envenenamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HUELLACOLMILLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Evidencia huellas de colmillos  True = Si  false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HUELLACOLMILLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'HUELLACOLMILLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Localización anatómica de la mordedura: 1=Cabeza/cara, 2=Miembros superiores, 3=Miembros inferiores, 4=Tórax anterior, 5=Abdomen, 6=Espalda, 7=Cuello, 8=Genitales, 9=Glúteos, 10=Dedos pie/mano, 11=Dedos mano. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'LOCAMORDEDURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Localización mordedura  1=Cabeza (cara)  2=Miembros superiores  3=Miembros inferiores  4=Tórax anterior  5=Abdomen  6=Espalda  7=Cuello  8=Genitales  9=Glúteos  10=Dedos de pie y de mano  11=Dedos de mano', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'LOCAMORDEDURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'LOCAMORDEDURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de otra práctica no médica realizada previa atención. Tipo: VARCHAR(200). Campo libre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRAPRACTICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cual otra Practica ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRAPRACTICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRAPRACTICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Paciente sometido a prácticas no médicas/tradicionales antes de atención. Tipo: INT. Identificación de riesgos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'SOMETIDAPRAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Persona sometida a prácticas no médicas?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'SOMETIDAPRAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'SOMETIDAPRAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especificación de otro tipo de atención prehospitalaria no listada. Tipo: VARCHAR(200). Campo complementario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRATENCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cual otro Atencion?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRATENCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRATENCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de atención prehospitalaria: 1=Incisión, 2=Punción, 3=Sangría, 4=Torniquete, 5=Inmovilización general, 6=Inmovilización miembro, 7=Succión mecánica, 8=Otro. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TIPOATENCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1=Incisión  2=Punción  3=Sangría  4=Torniquete  5=Inmovilización del enfermo  6=Inmovilización del miembro  7=Succión mecánica  8=Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TIPOATENCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'TIPOATENCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción libre de otra actividad realizada al momento del accidente. Tipo: VARCHAR(200). Campo complementario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRACTIVIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cual otro actividad?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRACTIVIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'OTRACTIVIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actividad que realizaba la víctima al momento del accidente ofídico. Tipo: INT. Contexto epidemiológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ACTIVIDADACCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Actividad que realizaba al momento del accidente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ACTIVIDADACCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ACTIVIDADACCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección, localidad o sitio geográfico donde ocurrió el accidente. Tipo: VARCHAR(200). Ubicación del evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DIRACCIDENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dirección lugar accidente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DIRACCIDENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'DIRACCIDENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del accidente ofídico. Tipo: DATE. Registro temporal del evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'FECHACCIDENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Accidnete ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'FECHACCIDENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'FECHACCIDENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la ficha/registro de notificación asociado. Tipo: INT. Relación con tabla padre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico del registro. Tipo: INT IDENTITY. Clave primaria de la tabla HCFICHA100.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha clínica de notificación de accidente ofídico (mordedura de serpiente). Registra los datos del evento: lugar y actividad al momento del accidente, características del animal agresor, signos y síntomas locales y sistémicos presentados por el paciente, complicaciones, gravedad y tratamiento con suero antiofídico aplicado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA100';

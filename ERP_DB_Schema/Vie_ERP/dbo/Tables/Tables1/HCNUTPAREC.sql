CREATE TABLE [dbo].[HCNUTPAREC] (
    [ID]                           INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCPARNUTC]                  INT                                                                              NOT NULL,
    [IDHCHISPACA]                  INT                                                                              NOT NULL,
    [CODPROSAL]                    CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [IPCODPACI]                    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                    CHAR (10)                                                                        NOT NULL,
    [FOLIORDEN]                    NCHAR (10)                                                                       NOT NULL,
    [FOLIOFIN]                     NCHAR (10)                                                                       NULL,
    [FECHAORDEN]                   DATETIME                                                                         NOT NULL,
    [PESOPACIE]                    NUMERIC (18, 2)                                                                  NOT NULL,
    [VOLUTOTAL]                    NUMERIC (18, 2)                                                                  NOT NULL,
    [VOLUADM]                      NUMERIC (18, 2)                                                                  NOT NULL,
    [TEMPOADMIN]                   INT                                                                              NOT NULL,
    [VELINFUSION]                  NUMERIC (18, 2)                                                                  NOT NULL,
    [VIADMIN]                      VARCHAR (20)                                                                     NOT NULL,
    [STATUS]                       INT                                                                              NOT NULL,
    [USERSUS]                      CHAR (20)                                                                        NULL,
    [SUSDATE]                      DATETIME                                                                         NULL,
    [AGRUPAQUETE]                  UNIQUEIDENTIFIER                                                                 NULL,
    [FECHAVERIFICA]                DATETIME                                                                         NULL,
    [CODPROSALVERIFICA]            CHAR (20)                                                                        NULL,
    [OBSERVERIFICA]                VARCHAR (5000)                                                                   NULL,
    [IDUNITDOSETYPE]               INT                                                                              NULL,
    [ReasonDiscontinuationOfDrug]  INT                                                                              NULL,
    [PatientRiskLevel]             INT                                                                              NULL,
    [PatientRiskLevelObservations] VARCHAR (1000)                                                                   NULL,
    [MOTSUSMED]                    VARCHAR (2000)                                                                   NULL,
    [IDTypesInfusionPumps]         INT                                                                              NULL,
    [TALLAPACIE]                   NUMERIC (18, 2)                                                                  NULL,
    [CODDIAGNO]                    CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    [NUMDIAS]                      INT                                                                              NULL,
    [LIQUIDOS]                     NUMERIC (18, 2)                                                                  NULL,
    [AGUAESTERIL]                  NUMERIC (18, 2)                                                                  NULL,
    [TIPOINFUSION]                 INT                                                                              NULL,
    [INDICACIONADM]                VARCHAR (300)                                                                    NULL,
    [INDICACIONADI]                VARCHAR (500)                                                                    NULL,
    [ValidityPrescription]         INT                                                                              NULL,
    [Justificacion]                VARCHAR (200)                                                                    NULL,
    [DateOfNutritionApplication]   DATETIME                                                                         NULL,
    [ApplyingProfessional]         CHAR (20)                                                                        NULL,
    [DateOfDiscardApplication]     DATETIME                                                                         NULL,
    [DiscardProfessional]          CHAR (20)                                                                        NULL,
    CONSTRAINT [PK__HCNUTPAR__3214EC27B6642FC2] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADINGRESO_NUTPAREC] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_CODPROSALVERIFICA] FOREIGN KEY ([CODPROSALVERIFICA]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCPARNUTC_NUTPAREC] FOREIGN KEY ([IDHCPARNUTC]) REFERENCES [dbo].[HCPARNUTC] ([ID]),
    CONSTRAINT [FK_HISPACA_NUTPAREC] FOREIGN KEY ([IDHCHISPACA]) REFERENCES [dbo].[HCHISPACA] ([ID]),
    CONSTRAINT [FK_IDUNITDOSETYPE] FOREIGN KEY ([IDUNITDOSETYPE]) REFERENCES [MixingStation].[UnitDoseType] ([Id]),
    CONSTRAINT [FK_INPACIENT_NUTPAREC] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_INPROFSAL_NUTPAREC] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[HCNUTPAREC] NOCHECK CONSTRAINT [FK_ADINGRESO_NUTPAREC];


GO
ALTER TABLE [dbo].[HCNUTPAREC] NOCHECK CONSTRAINT [FK_CODPROSALVERIFICA];


GO
ALTER TABLE [dbo].[HCNUTPAREC] NOCHECK CONSTRAINT [FK_HCPARNUTC_NUTPAREC];


GO
ALTER TABLE [dbo].[HCNUTPAREC] NOCHECK CONSTRAINT [FK_HISPACA_NUTPAREC];


GO
ALTER TABLE [dbo].[HCNUTPAREC] NOCHECK CONSTRAINT [FK_IDUNITDOSETYPE];


GO
ALTER TABLE [dbo].[HCNUTPAREC] NOCHECK CONSTRAINT [FK_INPACIENT_NUTPAREC];


GO
ALTER TABLE [dbo].[HCNUTPAREC] NOCHECK CONSTRAINT [FK_INPROFSAL_NUTPAREC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profesional que descarta la aplicación de la dosis de la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'DiscardProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de descarte de la aplicación de la dosis de la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'DateOfDiscardApplication';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profesional que aplica la dosis de la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'ApplyingProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la aplicación de la dosis de la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'DateOfNutritionApplication';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Vigencia de la prescripción en horas (0 ->24, 1->48, 2 -> 72)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'ValidityPrescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicación adicional nutrición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'INDICACIONADI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicaciones de administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'INDICACIONADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de infusion: 1:Continúa 2:Ciclada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'TIPOINFUSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado de la formula (Volumen total - Sumatoria de columna volúmenes administrar)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'AGUAESTERIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado de la formula (Volumen total / Peso)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'LIQUIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero dias NPT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'NUMDIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Talla del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'TALLAPACIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la pametrizacion de tipos de bombas de infusion (MedicalHistory.TypesInfusionPumps)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'IDTypesInfusionPumps';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de suspensión del la nutrición parenteral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'MOTSUSMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de los niveles de riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'PatientRiskLevelObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel de riesgo del paciente 1. Reacción alérgica 2. Reacción adversa 3. Intolerancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'PatientRiskLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Razón descontinuación de medicamento 1. Riesgos y reacciones adversas de medicamentos 2. Otra Razón o motivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'ReasonDiscontinuationOfDrug';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla de vie erp Tipo de dosis unitaria: [MixingStation].[UnitDoseType]     ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'IDUNITDOSETYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuando el quimico confirma ó verifica el esquema de Nutricion, esto se hace desde el dashboard atención farmaceutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'OBSERVERIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuando el quimico confirma ó verifica el esquema de Nutricion, esto se hace desde el dashboard atención farmaceutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'CODPROSALVERIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuando el quimico confirma ó verifica el esquema de Nutricion, esto se hace desde el dashboard atención farmaceutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'FECHAVERIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuando el quimico en el dashboard atención farmaceutica verfica el esquema de nutricion y guarda se actualiza este campo que es una relacion con HCFARMACD ya que alla tambien queda dicho agrupador.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'AGRUPAQUETE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la suspension de la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'SUSDATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que suspende nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'USERSUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la prescripción:  1. Solicitado   2.Anulado  3. Confirmado ó verficado por el quimico desde dashboard atenciòn farmeceutico.  4. Programada. 5. Aplicado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Via de administracion.  1- Cateter Central  2- Cateter lateral  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'VIADMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Velocidad de infusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'VELINFUSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo de administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'TEMPOADMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Volumen purga sumistrado para la prescripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'VOLUADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Volumen total sumistrado para la prescripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'VOLUTOTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peso del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'PESOPACIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la prescripción de la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'FECHAORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Folio desde el cual se finalizo la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'FOLIOFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Folio desde el cual se ordeno la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'FOLIORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de ingreso del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del profesional que prescribio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de HISPACA ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la pametrizacion de nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'IDHCPARNUTC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREC', @level2type = N'COLUMN', @level2name = N'ID';


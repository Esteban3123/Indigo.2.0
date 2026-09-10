CREATE TABLE [dbo].[HCHOJMEZC] (
    [CONSECUTI]                         NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]                         VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                         CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                         CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                         CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                         CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODPROAPL]                         CHAR (20)                                                                        NULL,
    [NOMMEZCLA]                         CHAR (200)                                                                       NOT NULL,
    [FECAPLMED]                         DATETIME                                                                         NULL,
    [FECREGSIS]                         DATETIME                                                                         CONSTRAINT [DF_HCHOJMEZC_FECREGSIS] DEFAULT ([Common].[getdate]()) NOT NULL,
    [FECFINDOS]                         DATETIME                                                                         NULL,
    [CABESTADO]                         CHAR (1)                                                                         NOT NULL,
    [INDAPLMED]                         VARCHAR (MAX)                                                                    NULL,
    [MOTSUSMED]                         CHAR (200)                                                                       NULL,
    [CODUSUSUS]                         CHAR (20)                                                                        NULL,
    [CODTIPEST]                         CHAR (3)                                                                         NULL,
    [DURACIDOS]                         CHAR (20)                                                                        NULL,
    [VALDURFIJ]                         INT                                                                              NULL,
    [UNIDURFIJ]                         CHAR (1)                                                                         NULL,
    [USUARIOPROGRAMAAPL]                CHAR (20)                                                                        NULL,
    [FECPROGRAMACIONAPL]                DATETIME                                                                         NULL,
    [DOSISUNICA]                        BIT                                                                              NULL,
    [DOSISAPLICACION]                   NUMERIC (18, 2)                                                                  NULL,
    [CODUNIMEDIAPLICACION]              VARCHAR (20)                                                                     NULL,
    [DURINFUSION]                       INT                                                                              NULL,
    [UNIDADINFUSION]                    INT                                                                              NULL,
    [DURFRECUENCIA]                     INT                                                                              NULL,
    [UNIDADFRECUENCIA]                  INT                                                                              NULL,
    [INDAPLMEDADICIONALES]              VARCHAR (1000)                                                                   NULL,
    [FECINITRA]                         DATETIME                                                                         NULL,
    [METAPLMED]                         VARCHAR (1)                                                                      NULL,
    [TIPMEZLIQ]                         VARCHAR (1)                                                                      NULL,
    [IDHCINFLIQC]                       NUMERIC (18)                                                                     NULL,
    [JUSTIFICACIONAPLCIACION]           VARCHAR (1000)                                                                   NULL,
    [FECHACOMPLETADO]                   DATETIME                                                                         NULL,
    [CODPROSALCOMPLETADO]               CHAR (20)                                                                        NULL,
    [FECHASUSPENDE]                     DATETIME                                                                         NULL,
    [UnitMeasurementInfusion]           CHAR (10)                                                                        NULL,
    [QuantityUnitInfusion]              NUMERIC (18, 2)                                                                  NULL,
    [QuantityCalculatedHour]            NUMERIC (18, 2)                                                                  NULL,
    [UnitMeasurementInfusionTitratable] CHAR (10)                                                                        NULL,
    [QuantityUnitInfusionTitratable]    NUMERIC (18, 2)                                                                  NULL,
    [QuantityCalculatedHourTitratable]  NUMERIC (18, 2)                                                                  NULL,
    [InfusionDoseLiquids]               NUMERIC (18, 2)                                                                  NULL,
    [Access]                            INT                                                                              NULL,
    [IDHCNUTPAREC]                      INT                                                                              NULL,
    [SuspensionJustification]           VARCHAR (2000)                                                                   NULL,
    [SuspensionReason]                  CHAR (4)                                                                         NULL,
    CONSTRAINT [PK_HCHOJMEZC] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC),
    CONSTRAINT [FK_HCHOJMEZC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCHOJMEZC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCHOJMEZC_CHTIPESTA] FOREIGN KEY ([CODTIPEST]) REFERENCES [dbo].[CHTIPESTA] ([CODTIPEST]),
    CONSTRAINT [FK_HCHOJMEZC_HCHOJMEZC] FOREIGN KEY ([CONSECUTI]) REFERENCES [dbo].[HCHOJMEZC] ([CONSECUTI]),
    CONSTRAINT [FK_HCHOJMEZC_HCINFLIQC] FOREIGN KEY ([IDHCINFLIQC]) REFERENCES [dbo].[HCINFLIQC] ([CODCONCEC]),
    CONSTRAINT [FK_HCHOJMEZC_HCNUTPAREC] FOREIGN KEY ([IDHCNUTPAREC]) REFERENCES [dbo].[HCNUTPAREC] ([ID]),
    CONSTRAINT [FK_HCHOJMEZC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCHOJMEZC_INPROFSAL1] FOREIGN KEY ([CODPROAPL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCHOJMEZC_INPROFSAL2] FOREIGN KEY ([CODUSUSUS]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCHOJMEZC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCHOJMEZC_INUNIMEDI] FOREIGN KEY ([CODUNIMEDIAPLICACION]) REFERENCES [dbo].[INUNIMEDI] ([CODUNIMED]),
    CONSTRAINT [FK_HCHOJMEZC_SuspensionReason] FOREIGN KEY ([SuspensionReason]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCHOJMEZC].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCHOJMEZC].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
CREATE NONCLUSTERED INDEX [IX_HCHOJMEZC__IPCODPACI__NUMINGRES__CABESTADO__INC__CONSECUTI__NOMMEZCLA__FECAPLMED]
    ON [dbo].[HCHOJMEZC]([IPCODPACI] ASC, [NUMINGRES] ASC, [CABESTADO] ASC)
    INCLUDE([CONSECUTI], [NOMMEZCLA], [FECAPLMED]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de anulación o descarte de la mezcla/líquido; código referenciado a tabla HCMOANULB; categoriza la razón por la cual se suspendió la aplicación del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'SuspensionReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de anulación/descarte ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'SuspensionReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'SuspensionReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación detallada de la suspensión o descarte de mezclas y líquidos; texto ampliado que documenta el fundamento clínico o administrativo de la cancelación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'SuspensionJustification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justifiación de la suspensión/descarte mezclas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'SuspensionJustification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'SuspensionJustification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vía de acceso para la administración: 1-Central (acceso vascular central), 2-Periférico (acceso vascular periférico); determina el tipo de catéter o línea utilizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'Access';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Accesos:  1- Central  2- Periferico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'Access';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'Access';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis de infusión del líquido prescrito; cantidad numérica (NUMERIC 18,2) de medicamento a infundir', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'InfusionDoseLiquids';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido - Dosis Infusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'InfusionDoseLiquids';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'InfusionDoseLiquids';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla titulable - cantidad calculada en centímetros cúbicos (CC) por hora; ajustable según respuesta clínica del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'QuantityCalculatedHourTitratable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Cantidad Calculada de CC por Hora titulable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'QuantityCalculatedHourTitratable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'QuantityCalculatedHourTitratable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla titulable - cantidad numérica de la unidad de medida para infusión titulable; valor modificable según titulación clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'QuantityUnitInfusionTitratable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Cantidad de la unidad de medida infusion titulable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'QuantityUnitInfusionTitratable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'QuantityUnitInfusionTitratable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla titulable - unidad de medida de infusión titulable: mcg/Kg/hr, mcg/Kg/min, mg/Kg/hr, mcg/min, mg/hr, UI/hr, mEq/hr; dosificación adaptable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'UnitMeasurementInfusionTitratable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Unidad de Medida de la infusion titulable de la mezcla:  mcg/Kg/hr  mcg/Kg/min  mg/Kg/hr  mcg/min  mg/hr  UI/hr  mEq/hr', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'UnitMeasurementInfusionTitratable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'UnitMeasurementInfusionTitratable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - cantidad calculada en centímetros cúbicos (CC) por hora; velocidad de infusión base para mezcla continua o frecuencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'QuantityCalculatedHour';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Cantidad Calculada de CC por Hora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'QuantityCalculatedHour';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'QuantityCalculatedHour';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - cantidad numérica de la unidad de medida para infusión; parámetro de dosificación en volumen o concentración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'QuantityUnitInfusion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Cantidad de la unidad de medida infusion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'QuantityUnitInfusion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'QuantityUnitInfusion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla - unidad de medida de infusión: mcg/Kg/hr, mcg/Kg/min, mg/Kg/hr, mcg/min, mg/hr, UI/hr, mEq/hr; estandariza el cálculo de dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'UnitMeasurementInfusion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mezcla - Unidad de Medida de la infusion de la mezcla:  mcg/Kg/hr  mcg/Kg/min  mg/Kg/hr  mcg/min  mg/hr  UI/hr  mEq/hr', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'UnitMeasurementInfusion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'UnitMeasurementInfusion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se efectuó la suspensión o cancelación de la aplicación del medicamento; marca el momento de interrupción del tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'FECHASUSPENDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que suspende esta aplicacion del medicamento.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'FECHASUSPENDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'FECHASUSPENDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (médico, enfermera, farmacéutico) que finalizó o marcó como completada la mezcla/líquido; PII ofuscado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CODPROSALCOMPLETADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo profesional que paso a completado la mezcla/liquido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CODPROSALCOMPLETADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CODPROSALCOMPLETADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que el registro de mezcla/líquido pasó a estado completado; indica fin de la administración o ciclo de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'FECHACOMPLETADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que paso el registro a completado  la mezcla/liquido  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'FECHACOMPLETADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'FECHACOMPLETADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación clínica de la aplicación del medicamento; nota registrada al momento de administrar la mezcla/líquido en el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONAPLCIACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion de la aplicacion del medicamento, campo que se registra al aplicar la mezcla/liquido.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONAPLCIACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONAPLCIACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) que relaciona este detalle con la cabecera de orden de mezcla/líquido en tabla HCINFLIQC; agrupa componentes de una prescripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'IDHCINFLIQC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que relaciona la cabecera de la orden de mezcla (HCINFLIQC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'IDHCINFLIQC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'IDHCINFLIQC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de orden médica: 1-Mezcla continua, 2-Líquido, 3-Mezcla frecuencia, 4-Mezcla magistral; categoriza el régimen de administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'TIPMEZLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Orden Medica:  1. Mezcla continua  2. Liquido  3. Mezcla Frecuencia   4. Mezcla Magistral ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'TIPMEZLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'TIPMEZLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Método de aplicación del medicamento: 1-Bolo mezcla continua, 2-Infusión mezcla, 3-Bolo medicamento mezcla, 4-Infusión líquido, 5-Bolo medicamento líquido; vía administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'METAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Metodo de Aplicacion de Medicamento  1: Bolo Mezcla Continua  2: Infusion Mezcla  3: Bolo Medicamento Mezcla  4: Infusion Liquido  5: Bolo medicamento Liquido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'METAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'METAPLMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora inicial del tratamiento con la mezcla/líquido; marca el inicio de la administración al paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'FECINITRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial del tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'FECINITRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'FECINITRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones adicionales para la administración del medicamento; instrucciones clínicas especiales registradas al programar la mezcla/líquido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'INDAPLMEDADICIONALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicacaciones adicionales de la administración del medicamentos, campo que se registra al progrmar el medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'INDAPLMEDADICIONALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'INDAPLMEDADICIONALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla frecuencia/magistral - unidad de tiempo de la frecuencia: 1-Minuto(s), 2-Hora(s), 3-Día(s), 4-Semana(s), 5-Mes(es), 6-Año(s); intervalo administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'UNIDADFRECUENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para la Mezcla Frecuencia y Magistral  Campo que me Identifica la Unidad de la Frecuencia de Aplicacion:     1:  Minuto(s)  2:  Hora(s)  3:  Dia(s)  4:  Semana(s)  5:  Mes(es)  6:  Año(s)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'UNIDADFRECUENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'UNIDADFRECUENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla frecuencia/magistral - valor numérico de la frecuencia de aplicación (cada X unidades); parámetro para intervalos de dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'DURFRECUENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para la Mezcla Frecuencia y Magistral  Campo que me Identifica la Frecuencia de Aplicacion cada xxx ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'DURFRECUENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'DURFRECUENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla frecuencia - unidad de tiempo de duración infusión: 1-Minuto(s), 2-Hora(s), 3-Día(s), 4-Semana(s), 5-Mes(es), 6-Año(s); horizonte temporal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'UNIDADINFUSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para la Mezcla Frecuencia  Campo que me Identifica la unidad de la Duración de la Infusión:    1:  Minuto(s)  2:  Hora(s)  3:  Dia(s)  4:  Semana(s)  5:  Mes(es)  6:  Año(s)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'UNIDADINFUSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'UNIDADINFUSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla frecuencia - duración numérica de la infusión; tiempo total de permanencia de la mezcla infundida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'DURINFUSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para la Mezcla Frecuencia  Campo que me Identifica la duración de la Infusion.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'DURINFUSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'DURINFUSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla frecuencia/magistral - código de unidad de medida aplicable (FK a INUNIMEDI); normaliza unidades farmacéuticas (ml, L, mg, gr, etc)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CODUNIMEDIAPLICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para la Mezcla Frecuencia y Magistral  Campo que me Identifica la unidad de Medida de la Aplicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CODUNIMEDIAPLICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CODUNIMEDIAPLICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla frecuencia/magistral - cantidad numérica de dosis por aplicación; volumen o masa administrada por evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'DOSISAPLICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para la Mezcla Frecuencia y Magistral  Campo que me identifica la Cantidad de Dosis de Aplicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'DOSISAPLICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'DOSISAPLICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mezcla frecuencia/magistral - indicador booleano (BIT) que marca si es dosis única; simplifica regímenes de una sola administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'DOSISUNICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  Dosis Unica: Campo para la Mezcla Frecuencia y Magistral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'DOSISUNICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'DOSISUNICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se programó la aplicación del medicamento en el sistema; timestamp de creación de la orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'FECPROGRAMACIONAPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Programación de la Aplicación del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'FECPROGRAMACIONAPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'FECPROGRAMACIONAPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario (profesional de la salud, enfermera) que programó la mezcla/líquido; PII ofuscado; trazabilidad de prescripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'USUARIOPROGRAMAAPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario que programo la mezcla  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'USUARIOPROGRAMAAPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'USUARIOPROGRAMAAPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo de la duración fija: 1-Minutos, 2-Horas, 3-Días; parámetro para mezclas de duración predeterminada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad del Valor de la duracion fija:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico de la duración fija de la mezcla; tiempo total de aplicación en la unidad especificada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la duracion fija', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración de la dosis; categoría del régimen: Tratamiento Continuo, Dosis Única, o Fija; esquema temporal de administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'DURACIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'- Duracion de la Dosis    Tratamiento Continuo   Dosis Única   Fija ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'DURACIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'DURACIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de estancia (FK a CHTIPESTA); identifica si es hospitalización, ambulatorio, UCI, etc; contexto clínico de la mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CODTIPEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del tipo de estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CODTIPEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CODTIPEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que suspendió la mezcla/líquido; profesional responsable de la cancelación; PII ofuscado; auditoría', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CODUSUSUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario que suspende', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CODUSUSUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CODUSUSUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de suspensión del medicamento; razón textual (hasta 200 caracteres) de por qué se interrumpió la administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'MOTSUSMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de Suspension', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'MOTSUSMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'MOTSUSMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones de aplicación de medicamentos; instrucciones clínicas generales para la administración segura de la mezcla/líquido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'INDAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones de Aplicacion de Medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'INDAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'INDAPLMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la mezcla/líquido: 1-Aplicado, 2-Completado, 3-Descartado/Suspendido por modificación o cancelación, 4-Sin Aplicar/Pendiente; ciclo de vida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CABESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(Ahora 29-03-2021)  Estado del Medicamento  1: Aplicado  2: Completado  3: Descartado/Suspendido por modficacion de la mezcla / cancelacion de aplicacion  4: Sin Aplicar/pendiente      (Antes)  Estado del Medicamento  1: Activo  2: Completado  3: Suspendido por modficacion de la mezcla / cancelacion de aplicacion            ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CABESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CABESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final de la dosis; MARCADO OBSOLETO (25-04-2021); campo heredado no utilizado en refactorización actual de mezclas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'FECFINDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final de la Dosis    SE MARCA COMO PBSOLETO ESTE CAMPO (25-04-2021 7:36PM)    Campo que no es necesario, con la nueva refactorizacion de mezclas  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'FECFINDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'FECFINDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del servidor al momento de registro en sistema; timestamp de auditoría generado automáticamente (getdate)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha/Hora del servidor para el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora real de aplicación del medicamento al paciente; momento efectivo de administración en atención clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'FECAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha/Hora real de aplicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'FECAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'FECAPLMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo de la mezcla o fórmula farmacéutica; identificación legible de los componentes o denominación comercial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'NOMMEZCLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'NOMMEZCLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'NOMMEZCLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud que aplica/administra la mezcla o suspende su aplicación; enfermero, médico; PII ofuscado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CODPROAPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional que Aplica la mezcla / Suspende', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CODPROAPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CODPROAPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (médico prescriptor) que prescribe la mezcla/líquido; PII ofuscado; FK a tabla profesionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud que prescribe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (FK a INUNIFUNC); departamento, servicio clínico o área donde se aplica la mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (FK a ADCENATEN); institución, hospital o clínica donde se registra la mezcla/líquido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso o admisión del paciente (FK a ADINGRESO); agrupa medicamentos del mismo episodio de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (FK a INPACIENT); identificación única del paciente; cédula, pasaporte o documento; PII ofuscado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo autonumérico (IDENTITY); identificador único de cada registro de mezcla/líquido; clave primaria de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CONSECUTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de mezclas y soluciones medicamentosas aplicadas a pacientes durante su ingreso hospitalario. Contiene la información de preparación, programación, aplicación y suspensión de mezclas intravenosas u otras combinaciones farmacológicas, incluyendo dosis, duración, infusión y el profesional responsable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la receta o parámetro nutricional asociado a la mezcla, vincula este registro con una prescripción de nutrición parenteral o enteral del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'IDHCNUTPAREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'IDHCNUTPAREC';

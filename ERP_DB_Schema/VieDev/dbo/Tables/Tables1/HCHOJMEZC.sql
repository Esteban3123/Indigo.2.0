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
ALTER TABLE [dbo].[HCHOJMEZC] NOCHECK CONSTRAINT [FK_HCHOJMEZC_ADCENATEN];


GO
ALTER TABLE [dbo].[HCHOJMEZC] NOCHECK CONSTRAINT [FK_HCHOJMEZC_ADINGRESO];


GO
ALTER TABLE [dbo].[HCHOJMEZC] NOCHECK CONSTRAINT [FK_HCHOJMEZC_CHTIPESTA];


GO
ALTER TABLE [dbo].[HCHOJMEZC] NOCHECK CONSTRAINT [FK_HCHOJMEZC_HCHOJMEZC];


GO
ALTER TABLE [dbo].[HCHOJMEZC] NOCHECK CONSTRAINT [FK_HCHOJMEZC_HCINFLIQC];


GO
ALTER TABLE [dbo].[HCHOJMEZC] NOCHECK CONSTRAINT [FK_HCHOJMEZC_INPACIENT];


GO
ALTER TABLE [dbo].[HCHOJMEZC] NOCHECK CONSTRAINT [FK_HCHOJMEZC_INPROFSAL1];


GO
ALTER TABLE [dbo].[HCHOJMEZC] NOCHECK CONSTRAINT [FK_HCHOJMEZC_INPROFSAL2];


GO
ALTER TABLE [dbo].[HCHOJMEZC] NOCHECK CONSTRAINT [FK_HCHOJMEZC_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCHOJMEZC] NOCHECK CONSTRAINT [FK_HCHOJMEZC_INUNIMEDI];


GO
CREATE NONCLUSTERED INDEX [IX_HCHOJMEZC__IPCODPACI__NUMINGRES__CABESTADO__INC__CONSECUTI__NOMMEZCLA__FECAPLMED]
    ON [dbo].[HCHOJMEZC]([IPCODPACI] ASC, [NUMINGRES] ASC, [CABESTADO] ASC)
    INCLUDE([CONSECUTI], [NOMMEZCLA], [FECAPLMED]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de anulación/descarte ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'SuspensionReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justifiación de la suspensión/descarte mezclas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'SuspensionJustification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo Accesos:  1- Central  2- Periferico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'Access';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mezcla - Unidad de Medida de la infusion titulable de la mezcla:  mcg/Kg/hr  mcg/Kg/min  mg/Kg/hr  mcg/min  mg/hr  UI/hr  mEq/hr', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'UnitMeasurementInfusionTitratable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mezcla - Unidad de Medida de la infusion de la mezcla:  mcg/Kg/hr  mcg/Kg/min  mg/Kg/hr  mcg/min  mg/hr  UI/hr  mEq/hr', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'UnitMeasurementInfusion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que suspende esta aplicacion del medicamento.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'FECHASUSPENDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que paso el registro a completado  la mezcla/liquido  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'FECHACOMPLETADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificacion de la aplicacion del medicamento, campo que se registra al aplicar la mezcla/liquido.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONAPLCIACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Orden Medica:  1. Mezcla continua  2. Liquido  3. Mezcla Frecuencia   4. Mezcla Magistral ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'TIPMEZLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Metodo de Aplicacion de Medicamento  1: Bolo Mezcla Continua  2: Infusion Mezcla  3: Bolo Medicamento Mezcla  4: Infusion Liquido  5: Bolo medicamento Liquido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'METAPLMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para la Mezcla Frecuencia y Magistral  Campo que me Identifica la Unidad de la Frecuencia de Aplicacion:     1:  Minuto(s)  2:  Hora(s)  3:  Dia(s)  4:  Semana(s)  5:  Mes(es)  6:  Año(s)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'UNIDADFRECUENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para la Mezcla Frecuencia y Magistral  Campo que me Identifica la Frecuencia de Aplicacion cada xxx ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'DURFRECUENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para la Mezcla Frecuencia  Campo que me Identifica la unidad de la Duración de la Infusión:    1:  Minuto(s)  2:  Hora(s)  3:  Dia(s)  4:  Semana(s)  5:  Mes(es)  6:  Año(s)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'UNIDADINFUSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para la Mezcla Frecuencia  Campo que me Identifica la duración de la Infusion.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'DURINFUSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para la Mezcla Frecuencia y Magistral  Campo que me Identifica la unidad de Medida de la Aplicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CODUNIMEDIAPLICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para la Mezcla Frecuencia y Magistral  Campo que me identifica la Cantidad de Dosis de Aplicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'DOSISAPLICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  Dosis Unica: Campo para la Mezcla Frecuencia y Magistral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'DOSISUNICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Usuario que programo la mezcla  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'USUARIOPROGRAMAAPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad del Valor de la duracion fija:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '- Duracion de la Dosis    Tratamiento Continuo   Dosis Única   Fija ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'DURACIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(Ahora 29-03-2021)  Estado del Medicamento  1: Aplicado  2: Completado  3: Descartado/Suspendido por modficacion de la mezcla / cancelacion de aplicacion  4: Sin Aplicar/pendiente      (Antes)  Estado del Medicamento  1: Activo  2: Completado  3: Suspendido por modficacion de la mezcla / cancelacion de aplicacion            ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'CABESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final de la Dosis    SE MARCA COMO PBSOLETO ESTE CAMPO (25-04-2021 7:36PM)    Campo que no es necesario, con la nueva refactorizacion de mezclas  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZC', @level2type = N'COLUMN', @level2name = N'FECFINDOS';


CREATE TABLE [dbo].[HCINFLIQA] (
    [CONSECUTI]                    NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC]                    NUMERIC (18)                                                                     NOT NULL,
    [IDETIPHIS]                    CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]                    NCHAR (10)                                                                       NOT NULL,
    [FECHAINIC]                    DATETIME                                                                         NOT NULL,
    [FECHAFINC]                    DATETIME                                                                         NULL,
    [CODPROSAL]                    CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [IPCODPACI]                    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                    CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                    CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                    CHAR (10)                                                                        NOT NULL,
    [TIPMEZLIQ]                    CHAR (1)                                                                         NOT NULL,
    [METAPLMED]                    CHAR (1)                                                                         NOT NULL,
    [MEZLIQPAC]                    CHAR (500)                                                                       NOT NULL,
    [ADMMEZLIQ]                    CHAR (500)                                                                       NOT NULL,
    [PREESTADO]                    INT                                                                              NOT NULL,
    [INDAPLMED]                    VARCHAR (MAX)                                                                    NULL,
    [CODDIAGNO]                    CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NOT NULL,
    [MOTSUSMED]                    VARCHAR (2000)                                                                   NULL,
    [NUMFOLSUS]                    CHAR (10)                                                                        NULL,
    [DOSISUNICA]                   BIT                                                                              NULL,
    [DOSISAPLICACION]              NUMERIC (18, 2)                                                                  NULL,
    [CODUNIMEDIAPLICACION]         VARCHAR (20)                                                                     NULL,
    [INICIOAPLICACION]             DATETIME                                                                         NULL,
    [DURINFUSION]                  INT                                                                              NULL,
    [UNIDADINFUSION]               INT                                                                              NULL,
    [DURFRECUENCIA]                INT                                                                              NULL,
    [UNIDADFRECUENCIA]             INT                                                                              NULL,
    [TIPODURACION]                 VARCHAR (30)                                                                     NULL,
    [DURACIONFIJA]                 INT                                                                              NULL,
    [UNIDADDURFIJA]                INT                                                                              NULL,
    [MEDPROGRAMADO]                INT                                                                              NULL,
    [CODCONCEC_ORIGEN]             NUMERIC (18)                                                                     NULL,
    [ReasonDiscontinuationOfDrug]  INT                                                                              NULL,
    [PatientRiskLevel]             INT                                                                              NULL,
    [PatientRiskLevelObservations] VARCHAR (1000)                                                                   NULL,
    [SUSDATE]                      DATETIME                                                                         NULL,
    CONSTRAINT [PK_HCINFLIQA_1] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC),
    CONSTRAINT [FK_HCINFLIQA_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCINFLIQA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCINFLIQA_HCINFLIQC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCINFLIQC] ([CODCONCEC]),
    CONSTRAINT [FK_HCINFLIQA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCINFLIQA_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCINFLIQA_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCINFLIQA] NOCHECK CONSTRAINT [FK_HCINFLIQA_ADCENATEN];


GO
ALTER TABLE [dbo].[HCINFLIQA] NOCHECK CONSTRAINT [FK_HCINFLIQA_ADINGRESO];


GO
ALTER TABLE [dbo].[HCINFLIQA] NOCHECK CONSTRAINT [FK_HCINFLIQA_HCINFLIQC];


GO
ALTER TABLE [dbo].[HCINFLIQA] NOCHECK CONSTRAINT [FK_HCINFLIQA_INPACIENT];


GO
ALTER TABLE [dbo].[HCINFLIQA] NOCHECK CONSTRAINT [FK_HCINFLIQA_INPROFSAL];


GO
ALTER TABLE [dbo].[HCINFLIQA] NOCHECK CONSTRAINT [FK_HCINFLIQA_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IDX_MezLiq]
    ON [dbo].[HCINFLIQA]([CODCONCEC] ASC)
    INCLUDE([PREESTADO]);


GO
CREATE NONCLUSTERED INDEX [HCINFCONC_Ix]
    ON [dbo].[HCINFLIQA]([PREESTADO] ASC)
    INCLUDE([CODCONCEC]);


GO
CREATE NONCLUSTERED INDEX [IX_HCINFLIQA_NUMINGRES]
    ON [dbo].[HCINFLIQA]([NUMINGRES] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCINFLIQA]
    ON [dbo].[HCINFLIQA]([IPCODPACI] ASC, [NUMINGRES] ASC, [NUMFOLSUS] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la suspensión de la nutrición enteral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'SUSDATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de los niveles de riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'PatientRiskLevelObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel de riesgo del paciente 1. Reacción alérgica 2. Reacción adversa 3. Intolerancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'PatientRiskLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Razón descontinuación de medicamento 1. Riesgos y reacciones adversas de medicamentos 2. Otra Razón o motivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'ReasonDiscontinuationOfDrug';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'consecutivo de origen de prescripcion de la mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CODCONCEC_ORIGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Programado en Hoja de Mezcla/Liquidos en enfermeria  0 - No Progrmado  1 - Programado  2 - Eliminado (cuando aparece la carta y la enfermera la elimina)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'MEDPROGRAMADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para la Mezcla Frecuencia y Mezcla Magistral.  Campo que me Identifica la Unidad de la Duracion fija:   1:  Minuto(s)  2:  Hora(s)  3:  Dia(s)  4:  Semana(s)  5:  Mes(es)  6:  Año(s)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'UNIDADDURFIJA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para la Mezcla Frecuencia y Mezcla Magistral.  Cmpo que me Identifica la Duracion cuando es FIJA la duracion ejemplo 24 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'DURACIONFIJA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para la Mezcla Frecuencia y Mezcla Magistral.     Tratamiento Continuo  Fija  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'TIPODURACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para la Mezcla Frecuencia y Magistral  Campo que me Identifica la Unidad de la Frecuencia de Aplicacion:     1:  Minuto(s)  2:  Hora(s)  3:  Dia(s)  4:  Semana(s)  5:  Mes(es)  6:  Año(s)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'UNIDADFRECUENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para la Mezcla Frecuencia y Magistral  Campo que me Identifica la Frecuencia de Aplicacion cada xxx   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'DURFRECUENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para la Mezcla Frecuencia  Campo que me Identifica la unidad de la Duración de la Infusión:    1:  Minuto(s)  2:  Hora(s)  3:  Dia(s)  4:  Semana(s)  5:  Mes(es)  6:  Año(s)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'UNIDADINFUSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para la Mezcla Frecuencia  Campo que me Identifica la duración de la Infusion.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'DURINFUSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para la Mezcla Frecuencia y Magistral  Campo que me Identifica el Incio de la Aplicacion, este campo lo coloca el medico en la orden medica de la mezcla.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'INICIOAPLICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para la Mezcla Frecuencia y Magistral  Campo que me Identifica la unidad de Medida de la Aplicacion  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CODUNIMEDIAPLICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para la Mezcla Frecuencia y Magistral  Campo que me identifica la Cantidad de Dosis de Aplicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'DOSISAPLICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para la Mezcla Frecuencia y Magistral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'DOSISUNICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Folio desde Donde se Suspendio el liquido/mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'NUMFOLSUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de suspension', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'MOTSUSMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Diagnostico principal o razon principal por la solicitud de la mezcla/liquido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicaciones de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'INDAPLMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Mezcla o Liquido  1: Iniciado: Cuando el Medicamento se Solicita por Primera Vez  2: Ciclo Completado  3: Tratamiento Modificado: Cuando existe una modificacion en la Dosificacion, Duracion o Frecuencia  4: Tratamiento Suspendido: Cuando el Medicamento es Suspendido  5: Alguno de los Medicamentos de la mezcla estan sin Existencia en el Kardex.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'PREESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Administracion de la Mezcla o Liquido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'ADMMEZLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la Mezcla o Liquido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'MEZLIQPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Metodo de Aplicacion de Medicamento  1: Bolo Mezcla  2: Infusion Mezcla  3: Bolo Medicamento Mezcla  4: Infusion Liquido  5: Bolo medicamento Liquido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'METAPLMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Orden Medica:  1. Mezcla continua  2. Liquido  3. Mezcla Frecuencia   4. Mezcla Magistral 5.Nutrición Parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'TIPMEZLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'FECHAFINC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'FECHAINIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Consecutivo de la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo ID de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CONSECUTI';


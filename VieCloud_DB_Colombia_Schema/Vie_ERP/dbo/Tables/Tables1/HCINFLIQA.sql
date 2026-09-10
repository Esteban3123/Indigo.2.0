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
    [AuthorizationEventId]          INT            NULL,
    CONSTRAINT [PK_HCINFLIQA_1] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC),
    CONSTRAINT [FK_HCINFLIQA_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCINFLIQA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCINFLIQA_HCINFLIQC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCINFLIQC] ([CODCONCEC]),
    CONSTRAINT [FK_HCINFLIQA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCINFLIQA_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCINFLIQA_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINFLIQA].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINFLIQA].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINFLIQA].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


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
ALTER INDEX [IX_HCINFLIQA_NUMINGRES]
    ON [dbo].[HCINFLIQA] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_HCINFLIQA]
    ON [dbo].[HCINFLIQA]([IPCODPACI] ASC, [NUMINGRES] ASC, [NUMFOLSUS] ASC);


GO
ALTER INDEX [IX_HCINFLIQA]
    ON [dbo].[HCINFLIQA] DISABLE;




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la suspensión de la nutrición enteral o mezcla/líquido. Registra cuándo se discontinuó la administración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'SUSDATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la suspensión de la nutrición enteral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'SUSDATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'SUSDATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas detalladas sobre los niveles de riesgo del paciente: reacciones alérgicas, adversas o intolerancia. Texto descriptivo VARCHAR(1000).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'PatientRiskLevelObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de los niveles de riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'PatientRiskLevelObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'PatientRiskLevelObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de riesgo clínico del paciente. Codificado: 1=Reacción alérgica, 2=Reacción adversa a medicamento, 3=Intolerancia. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'PatientRiskLevel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel de riesgo del paciente 1. Reacción alérgica 2. Reacción adversa 3. Intolerancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'PatientRiskLevel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'PatientRiskLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Razón de descontinuación de medicamento/mezcla. Codificado: 1=Riesgos y reacciones adversas, 2=Otra razón o motivo. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'ReasonDiscontinuationOfDrug';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Razón descontinuación de medicamento 1. Riesgos y reacciones adversas de medicamentos 2. Otra Razón o motivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'ReasonDiscontinuationOfDrug';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'ReasonDiscontinuationOfDrug';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de origen (referencia) de la prescripción o mezcla anterior. Permite trazabilidad de cambios. NUMERIC(18), FK opcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CODCONCEC_ORIGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'consecutivo de origen de prescripcion de la mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CODCONCEC_ORIGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CODCONCEC_ORIGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de programación en hoja de mezcla/líquidos de enfermería. Codificado: 0=No programado, 1=Programado, 2=Eliminado por enfermera. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'MEDPROGRAMADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Programado en Hoja de Mezcla/Liquidos en enfermeria  0 - No Progrmado  1 - Programado  2 - Eliminado (cuando aparece la carta y la enfermera la elimina)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'MEDPROGRAMADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'MEDPROGRAMADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para duración fija de mezcla magistral/frecuencia. Codificado: 1=Minuto(s), 2=Hora(s), 3=Día(s), 4=Semana(s), 5=Mes(es), 6=Año(s). INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'UNIDADDURFIJA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para la Mezcla Frecuencia y Mezcla Magistral.  Campo que me Identifica la Unidad de la Duracion fija:   1:  Minuto(s)  2:  Hora(s)  3:  Dia(s)  4:  Semana(s)  5:  Mes(es)  6:  Año(s)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'UNIDADDURFIJA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'UNIDADDURFIJA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico de duración fija (ej: 24) para mezcla magistral/frecuencia cuando no es continuo. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'DURACIONFIJA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para la Mezcla Frecuencia y Mezcla Magistral.  Cmpo que me Identifica la Duracion cuando es FIJA la duracion ejemplo 24 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'DURACIONFIJA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'DURACIONFIJA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de duración del tratamiento para mezcla frecuencia/magistral. Valores: ''''Continuo'''' o ''''Fija''''. VARCHAR(30).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'TIPODURACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para la Mezcla Frecuencia y Mezcla Magistral.     Tratamiento Continuo  Fija  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'TIPODURACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'TIPODURACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para frecuencia de aplicación. Codificado: 1=Minuto(s), 2=Hora(s), 3=Día(s), 4=Semana(s), 5=Mes(es), 6=Año(s). INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'UNIDADFRECUENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para la Mezcla Frecuencia y Magistral  Campo que me Identifica la Unidad de la Frecuencia de Aplicacion:     1:  Minuto(s)  2:  Hora(s)  3:  Dia(s)  4:  Semana(s)  5:  Mes(es)  6:  Año(s)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'UNIDADFRECUENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'UNIDADFRECUENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Intervalo numérico de frecuencia de aplicación (cada xxx unidades). Define cuándo se administra. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'DURFRECUENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para la Mezcla Frecuencia y Magistral  Campo que me Identifica la Frecuencia de Aplicacion cada xxx   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'DURFRECUENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'DURFRECUENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para duración de infusión en mezcla frecuencia. Codificado: 1=Minuto(s), 2=Hora(s), 3=Día(s), 4=Semana(s), 5=Mes(es), 6=Año(s). INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'UNIDADINFUSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para la Mezcla Frecuencia  Campo que me Identifica la unidad de la Duración de la Infusión:    1:  Minuto(s)  2:  Hora(s)  3:  Dia(s)  4:  Semana(s)  5:  Mes(es)  6:  Año(s)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'UNIDADINFUSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'UNIDADINFUSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración total de la infusión en la unidad especificada. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'DURINFUSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para la Mezcla Frecuencia  Campo que me Identifica la duración de la Infusion.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'DURINFUSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'DURINFUSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio de aplicación registrada por el médico en orden médica de mezcla/líquido. DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'INICIOAPLICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para la Mezcla Frecuencia y Magistral  Campo que me Identifica el Incio de la Aplicacion, este campo lo coloca el medico en la orden medica de la mezcla.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'INICIOAPLICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'INICIOAPLICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad de medida de la dosis aplicada (ej: mg, ml, mEq). VARCHAR(20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CODUNIMEDIAPLICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para la Mezcla Frecuencia y Magistral  Campo que me Identifica la unidad de Medida de la Aplicacion  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CODUNIMEDIAPLICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CODUNIMEDIAPLICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad numérica de la dosis a aplicar. NUMERIC(18,2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'DOSISAPLICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para la Mezcla Frecuencia y Magistral  Campo que me identifica la Cantidad de Dosis de Aplicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'DOSISAPLICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'DOSISAPLICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: dosis única o múltiples administraciones. BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'DOSISUNICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para la Mezcla Frecuencia y Magistral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'DOSISUNICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'DOSISUNICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio desde donde se suspendió el líquido o mezcla. Referencia para trazabilidad. CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'NUMFOLSUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Folio desde Donde se Suspendio el liquido/mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'NUMFOLSUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'NUMFOLSUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo clínico detallado de la suspensión de medicamento/mezcla. VARCHAR(2000).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'MOTSUSMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de suspension', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'MOTSUSMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'MOTSUSMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico principal o razón clínica para solicitar mezcla/líquido. PII enmascarado, FK a tabla de diagnósticos. CHAR(4).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Diagnostico principal o razon principal por la solicitud de la mezcla/liquido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones clínicas para administración de la mezcla/líquido. Instrucciones de enfermería. VARCHAR(MAX).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'INDAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'INDAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'INDAPLMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de procesamiento de la mezcla/líquido. Codificado: 1=Iniciado, 2=Ciclo completado, 3=Modificado (dosificación/duración/frecuencia), 4=Suspendido, 5=Sin existencia en Kardex. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'PREESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Mezcla o Liquido  1: Iniciado: Cuando el Medicamento se Solicita por Primera Vez  2: Ciclo Completado  3: Tratamiento Modificado: Cuando existe una modificacion en la Dosificacion, Duracion o Frecuencia  4: Tratamiento Suspendido: Cuando el Medicamento es Suspendido  5: Alguno de los Medicamentos de la mezcla estan sin Existencia en el Kardex.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'PREESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'PREESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada de la administración o via de la mezcla o líquido. CHAR(500).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'ADMMEZLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Administracion de la Mezcla o Liquido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'ADMMEZLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'ADMMEZLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción completa de componentes, fórmula o composición de la mezcla o líquido prescrito. CHAR(500).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'MEZLIQPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Mezcla o Liquido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'MEZLIQPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'MEZLIQPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Método de aplicación o vía. Codificado: 1=Bolo mezcla, 2=Infusión mezcla, 3=Bolo medicamento mezcla, 4=Infusión líquido, 5=Bolo medicamento líquido. CHAR(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'METAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Metodo de Aplicacion de Medicamento  1: Bolo Mezcla  2: Infusion Mezcla  3: Bolo Medicamento Mezcla  4: Infusion Liquido  5: Bolo medicamento Liquido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'METAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'METAPLMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de orden médica de mezcla/nutrición. Codificado: 1=Mezcla continua, 2=Líquido, 3=Mezcla frecuencia, 4=Mezcla magistral, 5=Nutrición parenteral. CHAR(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'TIPMEZLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Orden Medica:  1. Mezcla continua  2. Liquido  3. Mezcla Frecuencia   4. Mezcla Magistral 5.Nutrición Parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'TIPMEZLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'TIPMEZLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional que solicita/administra la mezcla. FK a INUNIFUNC. CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención donde se administra la mezcla/líquido. FK a ADCENATEN. CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente asociado a esta orden. FK a ADINGRESO. CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente. PII enmascarado, FK a INPACIENT. VARCHAR(25).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (médico) que prescribe. PII enmascarado, FK a INPROFSAL. CHAR(20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de finalización o vencimiento de la orden médica. DATETIME, puede ser NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'FECHAFINC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'FECHAFINC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'FECHAFINC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio de vigencia de la orden médica de mezcla/líquido. DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'FECHAINIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'FECHAINIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'FECHAINIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único de folio de la orden médica. NCHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno del tipo de historia clínica. CHAR(9).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consecutivo de la cabecera de orden. FK a HCINFLIQC, clave para relacionar detalles. NUMERIC(18).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo de la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) de la fila. Autoincrementable. NUMERIC(18).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo ID de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'CONSECUTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de líquidos de mezcla para infusión en historia clínica: guarda las órdenes y preparaciones de mezclas intravenosas (quimioterapia, nutrición parenteral, medicamentos en infusión) prescritas a un paciente durante un ingreso, incluyendo dosis, duración, frecuencia, estado y suspensiones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del último evento de autorización con estado Autorizado registrado para esta orden. Actualizado automáticamente en la misma transacción al guardar el evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQA', @level2type = N'COLUMN', @level2name = N'AuthorizationEventId';

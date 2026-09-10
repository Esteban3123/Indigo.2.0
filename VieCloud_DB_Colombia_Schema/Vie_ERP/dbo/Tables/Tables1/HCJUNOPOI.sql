CREATE TABLE [dbo].[HCJUNOPOI] (
    [CODCONCEC] CHAR (10)                                                                        NOT NULL,
    [IDETIPHIS] CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECORDMED] DATETIME                                                                         NOT NULL,
    [CODPRODUC] CHAR (20)                                                                        NOT NULL,
    [CODVIAADM] VARCHAR (20)                                                                     NOT NULL,
    [CODFORMED] VARCHAR (20)                                                                     NOT NULL,
    [DOSISPROD] NUMERIC (18, 2)                                                                  NULL,
    [CODUNIMED] VARCHAR (20)                                                                     NULL,
    [FRECUENCI] INT                                                                              NULL,
    [UNIFRECUE] CHAR (1)                                                                         NULL,
    [TIPFORMED] CHAR (1)                                                                         NOT NULL,
    [DURACIDOS] CHAR (20)                                                                        NOT NULL,
    [VALDURFIJ] INT                                                                              NULL,
    [UNIDURFIJ] CHAR (1)                                                                         NULL,
    [CANPEDPRO] INT                                                                              NOT NULL,
    [CODDIAGNO] CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NOT NULL,
    [DOSISDIAS] CHAR (500)                                                                       NULL,
    [DIASTRATA] INT                                                                              NULL,
    [INDTERAPR] VARCHAR (MAX)                                                                    NULL,
    [RESJUSMED] VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "SummaryHC_Ofuscado", 0)')     NULL,
    [EFEADVMED] VARCHAR (MAX)                                                                    NULL,
    [RAZVENMED] VARCHAR (MAX)                                                                    NULL,
    [RIEINMPAC] BIT                                                                              NULL,
    [AGOPOSTER] BIT                                                                              NULL,
    [MEDUTIPAI] BIT                                                                              NULL,
    [MEDEXPERI] BIT                                                                              NULL,
    [USOCORINV] BIT                                                                              NULL,
    [DESADMINI] VARCHAR (MAX)                                                                    NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_HCJUNOPOI] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [CODPRODUC] ASC),
    CONSTRAINT [FK_HCJUNOPOI_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCJUNOPOI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCJUNOPOI_HCVIAADMI] FOREIGN KEY ([CODVIAADM]) REFERENCES [dbo].[HCVIAADMI] ([CODVIAADM]),
    CONSTRAINT [FK_HCJUNOPOI_IHFORMEDI] FOREIGN KEY ([CODFORMED]) REFERENCES [dbo].[IHFORMEDI] ([CODFORMED]),
    CONSTRAINT [FK_HCJUNOPOI_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCJUNOPOI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCJUNOPOI_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCJUNOPOI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCJUNOPOI].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCJUNOPOI].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCJUNOPOI].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCJUNOPOI].[RESJUSMED]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicación del destino del paciente tras atención: 1=Hospitalización, 2=Urgencias, 3=Observación, 4=Cirugía, 5=Remisión, 6=Morgue, 7=Consulta Externa, 8=Salida. NUMERIC(18), auditado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicacion Paciente:  1: Orden de Hospitalizacion  2: Urgencias  3: Dejar en Observacion  4: Cirugia  5: Remitir  6: Morgue  7: Remitir a Consulta Externa  8: Salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la administración del medicamento: vía, horario, instrucciones especiales. VARCHAR(MAX), no enmascarado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'DESADMINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Administracion del Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'DESADMINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'DESADMINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si el medicamento corresponde a indicación registrada ante INVIMA. Uso autorizado por regulación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'USOCORINV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'El uso corresponde a la indicación registrada en el Invima ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'USOCORINV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'USOCORINV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si es medicamento en fase experimental o de investigación. Uso especial, requiere consentimiento informado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'MEDEXPERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Es un medicamento de experimentación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'MEDEXPERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'MEDEXPERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si el medicamento está autorizado y se utiliza comercialmente en el país. Medicamento de uso establecido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'MEDUTIPAI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Es un medicamento utilizado en el pais', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'MEDUTIPAI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'MEDUTIPAI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si se han agotado alternativas terapéuticas previas. Justificación de necesidad de medicamento especial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'AGOPOSTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se han agotado las posibilidades terapéuticas existentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'AGOPOSTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'AGOPOSTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si existe riesgo inminente para la vida del paciente. Urgencia, amenaza vital, situación crítica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'RIEINMPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Existe Riesgo Inminente para la Vida del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'RIEINMPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'RIEINMPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación clínica: razones y ventajas del medicamento seleccionado vs alternativas. VARCHAR(MAX), enmascarado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'RAZVENMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Explicacion de Razones y Ventajas del uso de este Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'RAZVENMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'RAZVENMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de posibles efectos adversos, reacciones adversas o complicaciones esperadas del medicamento. VARCHAR(MAX), no enmascarado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'EFEADVMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Posibles Efectos Adversos que se deriven del uso de este medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'EFEADVMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'EFEADVMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resumen de historia clínica y justificación médica del medicamento prescrito. VARCHAR(MAX), enmascarado SummaryHC_Ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'RESJUSMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resumen de la Historia Clinica y Justificacion del Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'RESJUSMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'RESJUSMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones terapéuticas: diagnósticos o condiciones clínicas para las cuales se prescribe el medicamento. VARCHAR(MAX), no enmascarado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'INDTERAPR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones Terapeuticas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'INDTERAPR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'INDTERAPR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de días de duración del tratamiento farmacológico. INT, período terapéutico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'DIASTRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dias ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'DIASTRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'DIASTRATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Esquema de dosis por día: distribución, horarios y cantidad diaria del medicamento. CHAR(500), detalle posológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'DOSISDIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'DOSISDIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'DOSISDIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CIE del diagnóstico principal o razón clínica de la solicitud del medicamento. CHAR(4), enmascarado DiagnosticCode_Ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Diagnostico principal o razon principal por la solicitud del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades solicitadas del producto farmacéutico. INT, cantidad pedida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Pedida del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para duración fija: 1=Minutos, 2=Horas, 3=Días. CHAR(1), clasificador temporal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad del Valor de la duracion fija:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico de la duración fija del tratamiento. INT, cantidad de unidades temporales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la duracion fija', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración total del tratamiento: período, intervalo o número de dosis. CHAR(20), período terapéutico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'DURACIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duracion del Tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'DURACIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'DURACIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de formulación del medicamento: 1=Peso, 2=Volumen, 3=Peso-Volumen, 4=Unidad administración. CHAR(1), forma farmacéutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'TIPFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Formulacion del medicamento:  1 Peso  2 Volumen  3 Peso-Volumen  4 Unidad de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'TIPFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'TIPFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de la frecuencia de administración: 1=Minutos, 2=Horas, 3=Días. CHAR(1), clasificador intervalo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad del Valor de la Frecuencia:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia de administración: número de veces que se administra el medicamento en la unidad de tiempo. INT, intervalo posológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'FRECUENCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'FRECUENCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'FRECUENCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad de medida: mg, ml, gramos, etc. VARCHAR(20), clasificador de dosificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis del medicamento por administración. NUMERIC(18,2), cantidad posológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'DOSISPROD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la forma de presentación: tabletas, cápsulas, solución inyectable, etc. VARCHAR(20), forma farmacéutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CODFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Forma de presentacion del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CODFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CODFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de vía de administración común: oral, IV, IM, tópica, etc. VARCHAR(20), ruta de administración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Via de Administracion Comun', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del producto farmacéutico, medicamento o principio activo. CHAR(20), llave primaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de solicitud o prescripción de la orden médica del medicamento. DATETIME, timestamp de registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Solicitud de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (médico, enfermero) que prescribió el medicamento. VARCHAR(25), FK a INPROFSAL, enmascarado Identification_Ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional donde se prescribe el medicamento: urgencias, hospitalización, consulta, etc. CHAR(10), FK a INUNIFUNC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención o institución prestadora de salud. CHAR(10), FK a ADCENATEN, ubicación clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso o admisión del paciente. CHAR(10), FK a ADINGRESO, identificador de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (cédula, documento de identidad, número de afiliado). VARCHAR(25), FK a INPACIENT, enmascarado Identification_Ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio del documento de prescripción o histórico clínico. NCHAR(10), referencia documental.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre interno o clasificación del tipo de historia clínica asociada a la prescripción. CHAR(9), identificador interno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código consecutivo del formato No POS (medicamentos no incluidos en POS). CHAR(10), llave primaria, nota: valor fijo 00000017.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Concecutivo del Formato No POS    Nota: Codigo 00000017', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de órdenes médicas de medicamentos en la historia clínica del paciente. Contiene la prescripción farmacológica con dosis, vía de administración, frecuencia, duración del tratamiento, diagnóstico asociado y justificación clínica, dentro de un ingreso o atención específica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOI';

CREATE TABLE [dbo].[HCINTERNO] (
    [IDETIPHIS]  CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]  NCHAR (10)                                                                       NOT NULL,
    [CONSFOLIO]  CHAR (10)                                                                        NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                        NOT NULL,
    [CODCENATE]  CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]  CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECHISPAC]  DATETIME                                                                         NOT NULL,
    [TIPHISPAC]  CHAR (2)                                                                         NOT NULL,
    [DIAGNOSTI]  VARCHAR (MAX)                                                                    NULL,
    [ANTECEDE]   VARCHAR (MAX)                                                                    NULL,
    [SUBJETIVO]  VARCHAR (MAX)                                                                    NULL,
    [EXAMENFI]   VARCHAR (MAX)                                                                    NULL,
    [PARACLINI]  VARCHAR (MAX)                                                                    NULL,
    [ANALISIS]   VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')      NULL,
    [DIETAS]     VARCHAR (MAX)                                                                    NULL,
    [MEDICAMEN]  VARCHAR (MAX)                                                                    NULL,
    [MEZYLIQUI]  VARCHAR (MAX)                                                                    NULL,
    [INSUYDISPO] VARCHAR (MAX)                                                                    NULL,
    [LABORATOR]  VARCHAR (MAX)                                                                    NULL,
    [PATOLOGIA]  VARCHAR (MAX)                                                                    NULL,
    [IMAGENES]   VARCHAR (MAX)                                                                    NULL,
    [PROCEDIQX]  VARCHAR (MAX)                                                                    NULL,
    [PROCENOQX]  VARCHAR (MAX)                                                                    NULL,
    [INTERCONSU] VARCHAR (MAX)                                                                    NULL,
    [RECOMENDA]  VARCHAR (MAX)                                                                    NULL,
    [CALFINTER]  DECIMAL (18, 1)                                                                  NULL,
    [CODESPECI]  CHAR (3)                                                                         NULL,
    [NUMEFOIN]   NCHAR (10)                                                                       NULL,
    CONSTRAINT [PK_HCINTERNO_1] PRIMARY KEY CLUSTERED ([NUMEFOLIO] ASC, [IPCODPACI] ASC),
    CONSTRAINT [FK_HCINTERNO_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCINTERNO_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINTERNO].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINTERNO].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINTERNO].[ANALISIS]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio generado por el estudiante/interno en práctica clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'NUMEFOIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Folio generado por el estudiante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'NUMEFOIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'NUMEFOIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad médica del profesional (Cardiología, Pediatría, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calificación numérica de la intervención/atención del interno (decimal 0-100)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'CALFINTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Calificacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'CALFINTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'CALFINTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones y recomendaciones clínicas emitidas por el especialista responsable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'RECOMENDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones para las recomendaciones por el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'RECOMENDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'RECOMENDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de interconsulta documentadas por el interno solicitante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'INTERCONSU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones para las interconsultas por el interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'INTERCONSU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'INTERCONSU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas de procedimientos no quirúrgicos realizados (endoscopia, punción, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'PROCENOQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones para los procedimientos NOQX por el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'PROCENOQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'PROCENOQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas de procedimientos quirúrgicos realizados por el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'PROCEDIQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones para las procedimientos QX por el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'PROCEDIQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'PROCEDIQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones e interpretación de estudios de imagen (radiografías, tomografías, resonancia, ecografía)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'IMAGENES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones para las imagenes por el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'IMAGENES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'IMAGENES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de resultados anatomopatológicos y análisis histológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'PATOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones para las patologias por el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'PATOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'PATOLOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de resultados de pruebas de laboratorio clínico (hemograma, química sanguínea, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'LABORATOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones para los laboratorios por el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'LABORATOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'LABORATOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones sobre insumos médicos y dispositivos utilizados en la atención del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'INSUYDISPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'observaciones para los insumos y dispositivos por el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'INSUYDISPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'INSUYDISPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones sobre mezclas farmacéuticas y líquidos administrados (sueros, medicamentos diluidos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'MEZYLIQUI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones para la mezcla y liquidos por los especialistas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'MEZYLIQUI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'MEZYLIQUI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones sobre medicamentos prescritos, dosis y vía de administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'MEDICAMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones para los medicamentos por el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'MEDICAMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'MEDICAMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones sobre prescripción dietética y restricciones nutricionales del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'DIETAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones para las dietas por el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'DIETAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'DIETAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones e interpretación clínica de análisis y resultados diagnósticos (PII ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'ANALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'observaciones para el analisis por el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'ANALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'ANALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de estudios paraclínicos complementarios (EKG, espirometría, pruebas funcionales)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'PARACLINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones para los paraclinicos por el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'PARACLINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'PARACLINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones del examen físico del paciente (inspección, palpación, auscultación)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'EXAMENFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones para el examne fisico por el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'EXAMENFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'EXAMENFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de síntomas, quejas y relato clínico reportado por el paciente (anamnesis)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'SUBJETIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones para el subjetivo de los especialistas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'SUBJETIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'SUBJETIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de antecedentes personales, familiares, alérgicos y patológicos del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'ANTECEDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'observaciones de los antecedentes para el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'ANTECEDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'ANTECEDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones clínicas y diagnósticos identificados por el especialista en la historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'DIAGNOSTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones para las observaciones del especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'DIAGNOSTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'DIAGNOSTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de historia clínica: I=Ingreso inicial, E=Evolución diaria, O=Otros modelos de apoyo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'TIPHISPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define si la historia es de Ingreso o Evolucion  I: Ingreso  E: Evolucion  O: Otros modelos de apoyo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'TIPHISPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'TIPHISPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro de la historia clínica del paciente en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'FECHISPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Historia Clinica Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'FECHISPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'FECHISPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud responsable (médico, interno, estudiante) - Identificación ofuscada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional donde se atiende (UCI, urgencias, hospitalización, ambulatorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención/institución donde se genera la historia clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único del ingreso del paciente a la institución (FK a ADINGRESO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (cédula, identificación, documento) - Identificación PII ofuscada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo secuencial de evoluciones dentro del mismo ingreso hospitalario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'CONSFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de Evoluciones de la Historia Clinica de Ingreso del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'CONSFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'CONSFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio único de la historia clínica (clave primaria junto con código paciente)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno del tipo/modelo de historia clínica utilizado en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas e historias clínicas internas de los pacientes durante su ingreso hospitalario. Registra la evolución médica diaria, incluyendo diagnósticos, antecedentes, examen físico, paraclínicos, medicamentos, procedimientos, interconsultas y recomendaciones realizadas por el profesional de salud en cada folio de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO';

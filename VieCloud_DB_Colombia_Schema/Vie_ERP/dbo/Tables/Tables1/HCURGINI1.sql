CREATE TABLE [dbo].[HCURGINI1] (
    [IDETIPHIS]    CHAR (9)                                                                          NOT NULL,
    [CODPROSAL]    CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')     NOT NULL,
    [NUMEFOLIO]    CHAR (10)                                                                         NOT NULL,
    [IPCODPACI]    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')  NOT NULL,
    [NUMINGRES]    CHAR (10)                                                                         NOT NULL,
    [CODCENATE]    CHAR (10)                                                                         NOT NULL,
    [UFUCODIGO]    CHAR (10)                                                                         NOT NULL,
    [FECINIATE]    DATETIME                                                                          NOT NULL,
    [MOTCONSUL]    VARCHAR (MAX)                                                                     NOT NULL,
    [ENFACTUAL]    VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "CurrentIllness_Ofuscado", 0)') NOT NULL,
    [REVSISTEMA]   VARCHAR (MAX)                                                                     NULL,
    [ANALISISP]    VARCHAR (MAX)                                                                     NULL,
    [INDICAPAC]    CHAR (2)                                                                          NOT NULL,
    [INDICAMED]    VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Instruction_Ofuscado", 0)')    NOT NULL,
    [TRAINTUNI]    BIT                                                                               NOT NULL,
    [MOTTRAINT]    VARCHAR (2000)                                                                    NULL,
    [TIPATEPAC]    CHAR (1)                                                                          NULL,
    [TIPCITMED]    CHAR (1)                                                                          NULL,
    [INDAUDFOR]    NUMERIC (18)                                                                      NOT NULL,
    [MOSEPICRI]    BIT                                                                               NULL,
    [CODESPTRA]    CHAR (3)                                                                          NULL,
    [FECHINIHI]    DATETIME                                                                          NULL,
    [FECHFINH]     DATETIME                                                                          NULL,
    [PLAINDMED]    VARCHAR (MAX)                                                                     NULL,
    [HCIRENAL]     INT                                                                               NULL,
    [HCIRENALPERI] INT                                                                               NULL,
    CONSTRAINT [PK_HCURGINI1] PRIMARY KEY CLUSTERED ([NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC),
    CONSTRAINT [FK_HCURGINI1_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCURGINI1_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCURGINI1_INESPECIA] FOREIGN KEY ([CODESPTRA]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCURGINI1_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCURGINI1_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCURGINI1_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCURGINI1].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCURGINI1].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCURGINI1].[ENFACTUAL]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCURGINI1].[INDICAMED]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de diálisis peritoneal registrada en HCI renal: 1=APD (Diálisis Peritoneal Manual), 2=CAPD (Diálisis Peritoneal Automatizada). Campo específico para pacientes en unidad renal con tratamiento peritoneal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'HCIRENALPERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'HCI Renal - Unidad Renal  Campo que se registra cuando el HCI sea de tipo Peritoneal.  1 -APD Diálisis Peritoneal Manual  2 - CAPD Diálisis Peritoneal Automatizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'HCIRENALPERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'HCIRENALPERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de atención renal en HCI: 1=Predialisis, 2=Diálisis Peritoneal, 3=Hemodiálisis. Identifica el programa o modalidad de tratamiento renal del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'HCIRENAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'HCI Renal - Unidad Renal  1 - Predialisis  2 - Peritoneal  3 - Hemodiálisis ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'HCIRENAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'HCIRENAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plantilla de indicaciones médicas generales; documento template que contiene las prescripciones y órdenes médicas estandarizadas para el paciente durante su atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'PLAINDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plantilla de Indicaciones medicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'PLAINDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'PLAINDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de finalización, cierre o término de la historia clínica de urgencia; indica cuándo se completó el registro clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'FECHFINH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Finalizacion de la Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'FECHFINH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'FECHFINH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio, apertura o creación de la historia clínica de urgencia; marca el momento de registro inicial de la atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'FECHINIHI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la Inicalizacion de la Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'FECHINIHI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'FECHINIHI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad médica tratante actual en la historia clínica; válido solo para unidades de hospitalización. Se actualiza con interconsultas y refleja la especialidad responsable actual, sin histórico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'CODESPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Historias Clinicas: Codigo de la Especialidad Tratante  Opcion valida solo para unidades funcionales de tipo Hospitalizacion - Se actualiza con Interconsultas. Muestra la especialidad acutal tratante, no se tiene opcion para guardar historico de especialidades tratantes, opcionalmente se puede obtener de HCHISPACA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'CODESPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'CODESPTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que indica si el registro debe mostrarse o incluirse en la epicrisis, resumen clínico final de la atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'MOSEPICRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mostrar en  Epicrisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'MOSEPICRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'MOSEPICRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador numérico de control de auditoría; campo de control interno para auditar y rastrear modificaciones en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de consulta médica: 1=Primera Vez (paciente nuevo), 2=Control (seguimiento). Clasifica la naturaleza de la atención médica ofrecida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'TIPCITMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Cita Medica:  1. Primera Vez  2. Control', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'TIPCITMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'TIPCITMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de atención según especialidad: 1=Ginecológica, 2=Obstétrica. Especifica la línea clínica de atención de urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'TIPATEPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Atencion:  1. Ginecologica  2. Obstetrica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'TIPATEPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'TIPATEPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo, causa o razón del traslado interno o externo del paciente a otra unidad, centro o institución; justificación clínica del movimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'MOTTRAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'MOTTRAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'MOTTRAINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de traslado interno: True=traslado a otra unidad funcional (misma IPS), False=traslado a otra IPS externa. Se relaciona con código de indicación 3 o 4.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'TRAINTUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define si se ha realizado un Traslado Interno a otra Unidad, Cuando en las Indicaciones Medicas la enumeracion es 3 o 4.  True: Traslado Interno  False: Traslado a otra IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'TRAINTUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'TRAINTUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones y prescripciones médicas generales dirigidas al paciente; órdenes clínicas, medicamentos, procedimientos y recomendaciones del profesional tratante. PII Ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'INDICAMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones Medicas Generales al Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'INDICAMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'INDICAMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (1-16) que define destino del paciente post-atención: 1=Urgencias, 2=Observación urgencias, 3=Hospitalización, 4=UCI Adulto, 5=UCI Pediátrica, 6=UCI Neonatal, 7=Consulta Externa, 8=Cirugía, 9=Hospitalización en Casa, 10=Referencia, 11=Morgue, 12=Salida, 13=Continúa en Unidad, 15=Retiro Voluntario, 16=Fuga.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'INDICAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define el destino del paciente  1. Trasladar a Urgencias: Solo consulta externa  2. Trasladar a Observacion Urgencias: solo Urgencias  3. Trasladar a Hospitalizacion: Dif Misma Unidad  4. Trasladar a  UCI Adulto: Dif Misma Unidad  5. Trasladar a UCI Pediatrica: Dif Misma Unidad  6. Trasladar a UCI Neonatal: Dif Misma Unidad  7. Trasladar a Consulta Externa: Dif Misma Unidad  8. Trasladar a  Cirugia: Dif Misma Unidad  9. Hospitalizacion en Casa  10. Referencia  11. Morgue  12. Salida  13. Continua en la Unidad  15. Retiro Voluntario  16. Fuga', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'INDICAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'INDICAPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis clínico, físico o de interpretación realizados en la evaluación; hallazgos de examen físico, análisis diagnóstico o valoración clínica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'ANALISISP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'ANALISISP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'ANALISISP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Revisión por sistemas; exploración ordenada de síntomas y signos en cada aparato o sistema corporal durante la evaluación clínica de urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'REVSISTEMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Revision por Sistemas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'REVSISTEMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'REVSISTEMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Enfermedad actual, padecimiento actual o queja principal del paciente; descripción de la patología o problema de salud que motivó la consulta de urgencia. PII Ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'ENFACTUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Enfermedad Actual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'ENFACTUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'ENFACTUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de consulta, queja principal o razón de la consulta de urgencia; síntoma, signo o evento que lleva al paciente a buscar atención urgente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'MOTCONSUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de Consulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'MOTCONSUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'MOTCONSUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio, apertura o admisión de la atención en urgencias; marca el registro temporal del primer contacto clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'FECINIATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'FECINIATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'FECINIATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de la unidad funcional donde se registra la historia; identifica el servicio, departamento o unidad que proporciona la atención (urgencias, hospitalización, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro o institución de atención donde se realiza la historia clínica; identifica la sede, hospital o clínica que atiende al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único del ingreso hospitalario o registro de admisión del paciente; identificador del evento de ingreso a la institución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación única del paciente en el sistema (cédula, documento, pasaporte o número de historia); identificador PII Ofuscado en búsquedas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único del folio o página de la historia clínica de urgencia; identificador secuencial del registro clínico generado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del profesional de la salud (médico, enfermero, especialista) que registra o firma la historia clínica; identificador PII del personal sanitario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno del tipo o categoría de historia clínica utilizado; clasificación interna del tipo de documento clínico generado en urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Historia clínica de urgencias — registro inicial de la consulta de urgencias de un paciente, incluyendo motivo de consulta, enfermedad actual, revisión por sistemas, análisis, indicaciones médicas y datos del episodio de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1';

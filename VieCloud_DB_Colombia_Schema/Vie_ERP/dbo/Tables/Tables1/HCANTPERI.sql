CREATE TABLE [dbo].[HCANTPERI] (
    [IDETIPHIS]      CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]      NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]      VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]      CHAR (10)                                                                        NOT NULL,
    [CODCENATE]      CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]      CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]      CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECHISPAC]      DATETIME                                                                         NOT NULL,
    [EDADMADRE]      INT                                                                              NULL,
    [EDADGESTA]      INT                                                                              NULL,
    [NUMPARIDA]      INT                                                                              NULL,
    [TIPOPARTO]      CHAR (1)                                                                         NULL,
    [CONTPRENA]      BIT                                                                              NULL,
    [CANTPRENA]      INT                                                                              NULL,
    [GESTACION]      CHAR (1)                                                                         NULL,
    [CANTGESTA]      INT                                                                              NULL,
    [IQGTOXOPL]      CHAR (1)                                                                         NULL,
    [CANTTOXO]       INT                                                                              NULL,
    [IQMTOXOPL]      CHAR (1)                                                                         NULL,
    [PRESENTAC]      CHAR (1)                                                                         NULL,
    [RESULTHIV]      CHAR (1)                                                                         NULL,
    [HEPATITIB]      CHAR (1)                                                                         NULL,
    [CANTHEPAT]      INT                                                                              NULL,
    [RESULVDRL]      CHAR (1)                                                                         NULL,
    [DILUCVDRL]      INT                                                                              NULL,
    [RUPPREMEM]      INT                                                                              NULL,
    [UNIDTIEMP]      CHAR (1)                                                                         NULL,
    [OTROSPERI]      VARCHAR (4000)                                                                   NULL,
    [IPGRUPSAP]      CHAR (2)                                                                         NULL,
    [IPRHSANGP]      CHAR (1)                                                                         NULL,
    [IPGRUPSAM]      CHAR (2)                                                                         NULL,
    [IPRHSANGM]      CHAR (1)                                                                         NULL,
    [PERICEFAL]      INT                                                                              NULL,
    [PERITORAX]      INT                                                                              NULL,
    [PERIABDOM]      INT                                                                              NULL,
    [TALLAPACI]      INT                                                                              NULL,
    [PESOPACIE]      INT                                                                              NULL,
    [BALLARDPA]      INT                                                                              NULL,
    [APGAR1PAC]      INT                                                                              NULL,
    [APGAR5PAC]      INT                                                                              NULL,
    [APGAR10PA]      INT                                                                              NULL,
    [ADAPNEONT]      CHAR (1)                                                                         NULL,
    [MECONIOPA]      BIT                                                                              NULL,
    [REANIMACI]      VARCHAR (4000)                                                                   NULL,
    [TIPALIMEN]      CHAR (1)                                                                         NULL,
    [EXAMENESE]      BIT MASKED WITH (FUNCTION = 'default()')                                         NULL,
    [DESCRIPEX]      VARCHAR (4000) MASKED WITH (FUNCTION = 'partial(0, "Description_Ofuscado", 0)')  NULL,
    [INDAUDFOR]      NUMERIC (18)                                                                     NOT NULL,
    [ChildbirthCare] INT                                                                              NULL,
    CONSTRAINT [PK_HCANTPERI] PRIMARY KEY CLUSTERED ([IDETIPHIS] ASC, [NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC),
    CONSTRAINT [FK_HCANTPERI_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCANTPERI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCANTPERI_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCANTPERI_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCANTPERI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPERI].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPERI].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPERI].[EXAMENESE]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPERI].[DESCRIPEX]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');



GO
CREATE NONCLUSTERED INDEX [IDX_AntecedentesPerinatales]
    ON [dbo].[HCANTPERI]([IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Atención al parto: 1=Intrahospitalario (en institución), 2=Extrahospitalario (domicilio/otro sitio). INT, tipo de asistencia neonatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'ChildbirthCare';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ATENCION AL PARTO --> en esta columna se almacenara 1- intrahospitalario 2-extrahospitalario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'ChildbirthCare';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'ChildbirthCare';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría, identificador único para trazabilidad de auditoría clínica. NUMERIC(18), PK.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de anomalías detectadas en examen clínico neonatal, hallazgos anormales. VARCHAR(4000), PII parcialmente ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'DESCRIPEX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Anomalia en el Examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'DESCRIPEX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'DESCRIPEX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Exámenes externos adicionales neonatal: True=Normal, False=Anormal. BIT, enmascarado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'EXAMENESE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Examenes Externos Adicionales  True: Normal  False: Anormal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'EXAMENESE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'EXAMENESE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de alimentación neonatal: 1=Sin definir, 2=Leche materna, 3=Artificial/suplementaria. CHAR(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'TIPALIMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Alimentacion  1: Sin Definir  2: Leche Materna  3: Artificial  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'TIPALIMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'TIPALIMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimientos de reanimación neonatal aplicados (descripción), resucitación. VARCHAR(4000).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'REANIMACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Reanimacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'REANIMACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'REANIMACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Meconio: True=Positivo (presencia), False=Negativo (ausencia). BIT, indicador de aspiración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'MECONIOPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Meconio  True: Positivo  False: Negativo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'MECONIOPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'MECONIOPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Adaptación neonatal: 1=Espontánea, 2=Conducida, 3=Inducida. CHAR(1), transición extrauterina.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'ADAPNEONT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Adaptacion Neonatal  1: Espontanea  2: Conducida  3: Inducida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'ADAPNEONT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'ADAPNEONT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Escala Apgar a los 10 minutos del nacimiento (0-10), valoración de vitalidad neonatal tardía. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'APGAR10PA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'APGAR 10 minutos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'APGAR10PA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'APGAR10PA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Escala Apgar a los 5 minutos del nacimiento (0-10), evaluación de adaptación temprana. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'APGAR5PAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'APGAR 5 minuto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'APGAR5PAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'APGAR5PAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Escala Apgar al 1 minuto del nacimiento (0-10), valoración inmediata vitalidad neonatal. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'APGAR1PAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'APGAR 1 minuto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'APGAR1PAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'APGAR1PAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntuación Ballard: evaluación edad gestacional por examen neuromuscular y físico. INT, semanas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'BALLARDPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ballard', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'BALLARDPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'BALLARDPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso del neonato/paciente en gramos (0-5000g aprox). INT, medida antropométrica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'PESOPACIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso en Gramos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'PESOPACIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'PESOPACIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Talla/longitud del neonato/paciente en centímetros. INT, medida antropométrica longitud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'TALLAPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Talla Paciente en cm', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'TALLAPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'TALLAPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Perímetro abdominal neonatal en centímetros, circunferencia abdominal. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'PERIABDOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Perimetro Abdominal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'PERIABDOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'PERIABDOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Perímetro torácico neonatal en centímetros, circunferencia del tórax. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'PERITORAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Perimetro Toraxico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'PERITORAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'PERITORAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Perímetro cefálico neonatal en centímetros, circunferencia de cabeza. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'PERICEFAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Perimetro Cefalico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'PERICEFAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'PERICEFAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor Rh materno: + (positivo) o - (negativo). CHAR(1), grupo sanguíneo madre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IPRHSANGM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RH Materno:  +  -', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IPRHSANGM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IPRHSANGM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo sanguíneo materno: A, B, AB, O. CHAR(2), tipificación sangre madre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IPGRUPSAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grupo Sanguineo Materno:  A  B  AB  O', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IPGRUPSAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IPGRUPSAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor Rh paterno: + (positivo) o - (negativo). CHAR(1), grupo sanguíneo padre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IPRHSANGP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RH Paterno:  +  -', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IPRHSANGP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IPRHSANGP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo sanguíneo paterno: A, B, AB, O. CHAR(2), tipificación sangre padre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IPGRUPSAP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grupo Sanguineo Paterno:  A  B  AB  O', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IPGRUPSAP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IPGRUPSAP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Otros antecedentes perinatales relevantes, datos maternos/gestacionales adicionales. VARCHAR(4000).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'OTROSPERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otros Perinatales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'OTROSPERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'OTROSPERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo ruptura prematura de membranas: 1=Horas, 2=Días. CHAR(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'UNIDTIEMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de Tiempo Ruptura Prematura de Membrana  1: Horas  2: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'UNIDTIEMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'UNIDTIEMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruptura prematura de membranas (minutos/horas/días antes parto), rotura bolsa amniótica. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'RUPPREMEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ruptura Prematura de Membrana', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'RUPPREMEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'RUPPREMEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diluciones VDRL (Laboratorio de Investigación de Enfermedades Venéreas), sérodiagnóstico sífilis. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'DILUCVDRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diluciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'DILUCVDRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'DILUCVDRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VDRL serología materna: True=Positivo (sífilis), False=Negativo. CHAR(1), treponema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'RESULVDRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'VDRL  True: Positivo  False: Negativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'RESULVDRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'RESULVDRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad/número referencia Hepatitis B materno (valor de laboratorio). INT, carga viral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CANTHEPAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad o Numero Referenci Hepatitis B', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CANTHEPAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CANTHEPAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antígeno superficie Hepatitis B materno: 1=Positivo, 2=Negativo, 3=No tiene/desconocido. CHAR(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'HEPATITIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ant. Sup. Hepatitis B  1: + Positivo  2: - Negativo  3: No Tiene', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'HEPATITIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'HEPATITIB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VIH/HIV serología materna: True=Positivo (infección), False=Negativo. CHAR(1), prueba inmunodeficiencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'RESULTHIV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'HIV / VIH  True: Positivo  False: Negativo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'RESULTHIV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'RESULTHIV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presentación fetal al parto: 1=Cefálica (cabeza), 2=Pélvica (glúteos), 3=Transversa. CHAR(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'PRESENTAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presentacion:  1: Cefalico  2: Pelviz  3: Transverso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'PRESENTAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'PRESENTAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IgM Toxoplasma materno (infección aguda): 1=Positivo, 2=Negativo, 3=No tiene. CHAR(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IQMTOXOPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'IqM Toxoplasma  1: + Positivo  2: - Negativo  3: No Tiene', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IQMTOXOPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IQMTOXOPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad/número referencia IgG Toxoplasma materno (valor laboratorio). INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CANTTOXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad o Numero referencia a IqG Toxoplasma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CANTTOXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CANTTOXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IgG Toxoplasma materno (inmunidad previa): True=Positivo, False=Negativo. CHAR(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IQGTOXOPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'IqG Toxoplasma  True: Positivo  False: Falso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IQGTOXOPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IQGTOXOPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad/número total de gestaciones previas (multiparidad), embarazos anteriores. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CANTGESTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad o Numero de Gestaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CANTGESTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CANTGESTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de gestación: 1=Único (singleton), 2=Múltiple (gemelos/gemelar). CHAR(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'GESTACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Gestacion  1: Unico  2: Multiple', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'GESTACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'GESTACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad/número de controles prenatales recibidos durante embarazo. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CANTPRENA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de Controles Prenatales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CANTPRENA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CANTPRENA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control prenatal: True=Sí (hubo controles), False=No (sin controles). BIT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CONTPRENA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control Prenatal True:Si False:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CONTPRENA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CONTPRENA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de parto: 1=Vaginal (natural), 2=Instrumentada (fórceps/ventosa), 3=Cesárea. CHAR(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'TIPOPARTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Parto:  1: Vaginal  2: Instrumentada  3: Cesarea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'TIPOPARTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'TIPOPARTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Paridad: número de partos previos (multípara/primípara). INT, antecedente obstétrico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'NUMPARIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Paridad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'NUMPARIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'NUMPARIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad gestacional al nacimiento en semanas (16-42 aprox). INT, semanas gestación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'EDADGESTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Gestacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'EDADGESTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'EDADGESTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad de la madre al momento del parto en años. INT, edad materna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'EDADMADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad de la Madre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'EDADMADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'EDADMADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha historia clínica/ingreso del paciente (neonato), timestamp registro. DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'FECHISPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Historia Clinica Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'FECHISPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'FECHISPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (médico/partera) responsable, FK INPROFSAL. VARCHAR(25), PII ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código unidad funcional (sala parto, obstetricia, neonatología), FK INUNIFUNC. CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código centro de atención/institución donde ocurrió parto, FK ADCENATEN. CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número ingreso neonato, identificador episodio atención, FK ADINGRESO. CHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/identificación del paciente (neonato), cédula/documento, FK INPACIENT. VARCHAR(25), PII ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número folio/historia clínica del neonato, secuencial. NCHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre interno tipo historia clínica (perinatal/neonatal), clasificador tipo registro. CHAR(9), PK.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro del período perinatal del recién nacido y su madre durante el ingreso hospitalario. Guarda información clínica del parto, control prenatal, resultados de laboratorio maternos, datos antropométricos y de adaptación neonatal del bebé.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI';

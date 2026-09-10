CREATE TABLE [dbo].[HCANTPEII] (
    [IDETIPHIS] CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECHISPAC] DATETIME                                                                         NOT NULL,
    [EDADMADRE] INT                                                                              NULL,
    [EDADGESTA] INT                                                                              NULL,
    [NUMPARIDA] INT                                                                              NULL,
    [TIPOPARTO] CHAR (1)                                                                         NULL,
    [CONTPRENA] BIT                                                                              NULL,
    [CANTPRENA] INT                                                                              NULL,
    [GESTACION] CHAR (1)                                                                         NULL,
    [CANTGESTA] INT                                                                              NULL,
    [IQGTOXOPL] CHAR (1)                                                                         NULL,
    [CANTTOXO]  INT                                                                              NULL,
    [IQMTOXOPL] CHAR (1)                                                                         NULL,
    [PRESENTAC] CHAR (1)                                                                         NULL,
    [RESULTHIV] BIT                                                                              NULL,
    [HEPATITIB] CHAR (1)                                                                         NULL,
    [CANTHEPAT] INT                                                                              NULL,
    [RESULVDRL] BIT                                                                              NULL,
    [DILUCVDRL] INT                                                                              NULL,
    [RUPPREMEM] INT                                                                              NULL,
    [UNIDTIEMP] CHAR (1)                                                                         NULL,
    [OTROSPERI] VARCHAR (4000)                                                                   NULL,
    [IPGRUPSAP] CHAR (2)                                                                         NULL,
    [IPRHSANGP] CHAR (1)                                                                         NULL,
    [IPGRUPSAM] CHAR (2)                                                                         NULL,
    [IPRHSANGM] CHAR (1)                                                                         NULL,
    [PERICEFAL] INT                                                                              NULL,
    [PERITORAX] INT                                                                              NULL,
    [PERIABDOM] INT                                                                              NULL,
    [TALLAPACI] INT                                                                              NULL,
    [PESOPACIE] NUMERIC (18, 3)                                                                  NULL,
    [BALLARDPA] INT                                                                              NULL,
    [APGAR1PAC] INT                                                                              NULL,
    [APGAR5PAC] INT                                                                              NULL,
    [APGAR10PA] INT                                                                              NULL,
    [ADAPNEONT] CHAR (1)                                                                         NULL,
    [MECONIOPA] BIT                                                                              NULL,
    [REANIMACI] VARCHAR (4000)                                                                   NULL,
    [TIPALIMEN] CHAR (1)                                                                         NULL,
    [EXAMENESE] BIT                                                                              NULL,
    [DESCRIPEX] VARCHAR (4000) MASKED WITH (FUNCTION = 'partial(0, "Description_Ofuscado", 0)')  NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_HCANTPEII] PRIMARY KEY CLUSTERED ([IDETIPHIS] ASC, [NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC),
    CONSTRAINT [FK_HCANTPEII_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCANTPEII_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCANTPEII_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCANTPEII_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCANTPEII_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPEII].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPEII].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCANTPEII].[DESCRIPEX]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría, identificador único del registro de auditoria (NUMERIC 18), clave para trazabilidad del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de anomalías halladas en examen físico neonatal, texto libre máximo 4000 caracteres, datos sensibles ofuscados (PII)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'DESCRIPEX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Anomalia en el Examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'DESCRIPEX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'DESCRIPEX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de exámenes externos adicionales: True=Normal, False=Anormal; evaluación complementaria del recién nacido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'EXAMENESE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Examenes Externos Adicionales  True: Normal  False: Anormal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'EXAMENESE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'EXAMENESE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de alimentación neonatal: 1=Sin Definir, 2=Leche Materna, 3=Artificial; método de nutrición del recién nacido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'TIPALIMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Alimentacion  1: Sin Definir  2: Leche Materna  3: Artificial  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'TIPALIMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'TIPALIMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimientos de reanimación neonatal realizados, texto libre máximo 4000 caracteres; maniobras de resucitación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'REANIMACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Reanimacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'REANIMACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'REANIMACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de meconio: True=Positivo (líquido teñido), False=Negativo; indicador de sufrimiento fetal perinatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'MECONIOPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Meconio  True: Positivo  False: Negativo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'MECONIOPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'MECONIOPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Adaptación neonatal: 1=Espontánea, 2=Conducida, 3=Inducida; grado de adaptación del recién nacido al medio extrauterino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'ADAPNEONT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Adaptacion Neonatal  1: Espontanea  2: Conducida  3: Inducida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'ADAPNEONT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'ADAPNEONT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntuación Apgar a los 10 minutos de nacimiento (INT 0-10); evaluación de vitalidad neonatal tardía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'APGAR10PA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'APGAR 10 minutos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'APGAR10PA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'APGAR10PA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntuación Apgar a los 5 minutos de nacimiento (INT 0-10); evaluación de vitalidad neonatal intermedia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'APGAR5PAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'APGAR 5 minuto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'APGAR5PAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'APGAR5PAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntuación Apgar al minuto de nacimiento (INT 0-10); evaluación inmediata de frecuencia cardíaca, respiración, tono muscular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'APGAR1PAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'APGAR 1 minuto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'APGAR1PAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'APGAR1PAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Escala de Ballard para edad gestacional neonatal (INT semanas); evaluación neuromuscular y física del recién nacido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'BALLARDPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ballard', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'BALLARDPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'BALLARDPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso del recién nacido en gramos (NUMERIC 18,3); medida antropométrica neonatal fundamental', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'PESOPACIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso en Gramos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'PESOPACIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'PESOPACIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Talla del recién nacido en centímetros (INT); medida de longitud neonatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'TALLAPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Talla Paciente en cm', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'TALLAPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'TALLAPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Perímetro abdominal del recién nacido (INT cm); medida antropométrica abdominal para detectar distensión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'PERIABDOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Perimetro Abdominal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'PERIABDOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'PERIABDOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Perímetro torácico del recién nacido (INT cm); medida antropométrica de la circunferencia del tórax', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'PERITORAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Perimetro Toraxico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'PERITORAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'PERITORAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Perímetro cefálico del recién nacido (INT cm); circunferencia de la cabeza, indicador de crecimiento neurológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'PERICEFAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Perimetro Cefalico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'PERICEFAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'PERICEFAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor RH materno: + (Positivo) o - (Negativo); antígeno de grupo sanguíneo de la madre, relevante para incompatibilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'IPRHSANGM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RH Materno:  +  -', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'IPRHSANGM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'IPRHSANGM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo sanguíneo materno: A, B, AB u O; clasificación ABO de la madre para compatibilidad transfusional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'IPGRUPSAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grupo Sanguineo Materno:  A  B  AB  O', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'IPGRUPSAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'IPGRUPSAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor RH paterno: + (Positivo) o - (Negativo); antígeno de grupo sanguíneo del padre para análisis genético', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'IPRHSANGP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RH Paterno:  +  -', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'IPRHSANGP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'IPRHSANGP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo sanguíneo paterno: A, B, AB u O; clasificación ABO del padre para determinación de compatibilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'IPGRUPSAP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grupo Sanguineo Paterno:  A  B  AB  O', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'IPGRUPSAP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'IPGRUPSAP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Otros antecedentes perinatales, eventos o hallazgos adicionales (VARCHAR 4000); información complementaria del período perinatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'OTROSPERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otros Perinatales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'OTROSPERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'OTROSPERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para ruptura prematura de membrana: 1=Horas, 2=Días; escala temporal asociada a RPM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'UNIDTIEMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de Tiempo Ruptura Prematura de Membrana  1: Horas  2: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'UNIDTIEMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'UNIDTIEMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruptura prematura de membrana (INT), duración en horas o días; complicación obstétrica con riesgo neonatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'RUPPREMEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ruptura Prematura de Membrana', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'RUPPREMEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'RUPPREMEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diluciones de prueba VDRL/RPR (INT); nivel de dilución sanguínea para sífilis materna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'DILUCVDRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diluciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'DILUCVDRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'DILUCVDRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado VDRL (Venereal Disease Research Laboratory): True=Positivo, False=Negativo; serología de sífilis materna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'RESULVDRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'VDRL  True: Positivo  False: Negativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'RESULVDRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'RESULVDRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad o número de referencias para Antígeno de Superficie de Hepatitis B (INT); valor cuantitativo de HBsAg', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'CANTHEPAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad o Numero Referenci Hepatitis B', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'CANTHEPAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'CANTHEPAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antígeno de Superficie Hepatitis B: 1=Positivo, 2=Negativo, 3=No realizado; screening materno de infección viral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'HEPATITIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ant. Sup. Hepatitis B  1: + Positivo  2: - Negativo  3: No Tiene', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'HEPATITIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'HEPATITIB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado VIH/HIV: True=Positivo, False=Negativo; prueba de detección de inmunodeficiencia humana materna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'RESULTHIV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'HIV / VIH  True: Positivo  False: Negativo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'RESULTHIV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'RESULTHIV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presentación fetal al parto: 1=Cefálica, 2=Pélvica, 3=Transversa; posición del feto en momento del nacimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'PRESENTAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presentacion:  1: Cefalico  2: Pelviz  3: Transverso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'PRESENTAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'PRESENTAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IgM Toxoplasma: 1=Positivo, 2=Negativo, 3=No realizado; anticuerpo de infección aguda por toxoplasmosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'IQMTOXOPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'IqM Toxoplasma  1: + Positivo  2: - Negativo  3: No Tiene', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'IQMTOXOPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'IQMTOXOPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad o número de referencia para IgG Toxoplasma (INT); valor cuantitativo de inmunidad a toxoplasmosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'CANTTOXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad o Numero referencia a IqG Toxoplasma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'CANTTOXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'CANTTOXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IgG Toxoplasma: True=Positivo, False=Negativo; anticuerpo de inmunidad adquirida a toxoplasmosis materna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'IQGTOXOPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'IqG Toxoplasma  True: Positivo  False: Falso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'IQGTOXOPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'IQGTOXOPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad o número total de gestaciones previas (INT); número de embarazos anteriores, multiparidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'CANTGESTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad o Numero de Gestaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'CANTGESTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'CANTGESTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de gestación: 1=Único, 2=Múltiple; determina si embarazo de uno o más fetos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'GESTACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Gestacion  1: Unico  2: Multiple', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'GESTACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'GESTACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de controles prenatales realizados (INT); número de atenciones de seguimiento durante embarazo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'CANTPRENA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de Controles Prenatales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'CANTPRENA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'CANTPRENA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Realización de control prenatal: True=Sí, False=No; indicador de seguimiento obstétrico durante embarazo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'CONTPRENA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control Prenatal True:Si False:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'CONTPRENA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'CONTPRENA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de parto: 1=Vaginal, 2=Instrumentada, 3=Cesárea; vía y modalidad de terminación del embarazo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'TIPOPARTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Parto:  1: Vaginal  2: Instrumentada  3: Cesarea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'TIPOPARTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'TIPOPARTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Paridad: número de partos previos (INT); cantidad de embarazos que llegaron a viabilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'NUMPARIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Paridad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'NUMPARIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'NUMPARIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad gestacional en semanas (INT); tiempo de gestación desde última menstruación hasta nacimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'EDADGESTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Gestacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'EDADGESTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'EDADGESTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad de la madre en años (INT); dato demográfico materno para análisis de riesgo obstétrico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'EDADMADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad de la Madre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'EDADMADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'EDADMADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de apertura de historia clínica del recién nacido (DATETIME); timestamp de documento clínico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'FECHISPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Historia Clinica Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'FECHISPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'FECHISPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que atiende (VARCHAR 25, FK a INPROFSAL, PII ofuscado); médico o partero responsable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional (CHAR 10, FK a INUNIFUNC); departamento o servicio de pediatría/ginecología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (CHAR 10, FK a ADCENATEN); institución, hospital o clínica donde nace', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso u hospitalización (CHAR 10, FK a ADINGRESO); identificador del episodio hospitalario materno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (recién nacido, VARCHAR 25, FK a INPACIENT, PII ofuscado); identificación equivalente a cédula', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio de historia clínica (NCHAR 10); secuencial del documento clínico neonatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre interno del tipo de historia clínica (CHAR 9); clasificación de documento: historia perinatal tipo II', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Historia clínica perinatal e información del recién nacido: registra los datos del parto, condiciones del neonato (Apgar, Ballard, peso, talla, perímetros), antecedentes maternos (edad, gestas, controles prenatales, serología), grupo sanguíneo del bebé y la madre, tipo de parto y resultados de exámenes al nacimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPEII';

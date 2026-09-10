CREATE TABLE [dbo].[HCESCNTON] (
    [CODCONCEC]  NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                        NOT NULL,
    [CODCENATE]  CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]  CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGSIS]  DATETIME                                                                         NOT NULL,
    [ESTFISGEN]  TINYINT                                                                          NOT NULL,
    [ESTMENTAL]  TINYINT                                                                          NOT NULL,
    [MOVILIDAD]  TINYINT                                                                          NOT NULL,
    [ACTIVIDAD]  TINYINT                                                                          NOT NULL,
    [INCTINENCI] TINYINT                                                                          NOT NULL,
    [HCESCALAID] INT                                                                              NULL,
    [CODESPECI]  CHAR (3)                                                                         NULL,
    CONSTRAINT [PK_HCESCNTON] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCESCNTON_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCESCNTON_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCESCNTON_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCESCNTON].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCESCNTON].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
CREATE NONCLUSTERED INDEX [_dta_index_HCESCNTON_7_1642593040__K2_K3_K5_8_9_10_11_12]
    ON [dbo].[HCESCNTON]([IPCODPACI] ASC, [NUMINGRES] ASC, [UFUCODIGO] ASC)
    INCLUDE([ACTIVIDAD], [ESTFISGEN], [ESTMENTAL], [INCTINENCI], [MOVILIDAD]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad médica o de enfermería del profesional que diligencia la escala de valoración. FK → INESPECIA. Búsqueda: especialidad, médico, enfermera, profesional de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la especialidad del medico o enfermera que Diligencia la escala. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador relacional con tabla cabecera HCESCALAS. INT. Referencia a escala de valoración clínica principal. Búsqueda: escala, valoración clínica, cabecera.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'HCESCALAID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla cabecera llamada HCESCALAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'HCESCALAID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'HCESCALAID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de incontinencia: 1=Urinaria y Fecal, 2=Urinaria o Fecal, 3=Ocasional, 4=Ninguna. TINYINT. Evaluación de continencia en ingreso/atención. Búsqueda: incontinencia, continencia, control esfínteres.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'INCTINENCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1: Urinaria y Fecal 2: Urinaria o Fecal 3: Ocasional 4: Ninguna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'INCTINENCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'INCTINENCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de actividad física del paciente: 1=Encamado, 2=Sentado, 3=Camina con ayuda, 4=Ambulante. TINYINT. Escala de funcionalidad. Búsqueda: actividad, movilización, ambulante, encamado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'ACTIVIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1: Encamado 2: Sentado 3: Camina con ayuda 4: Ambulante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'ACTIVIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'ACTIVIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grado de movilidad articular: 1=Inmóvil, 2=Muy Limitada, 3=Disminuida, 4=Total. TINYINT. Evaluación de rango de movimiento. Búsqueda: movilidad, movimiento, rango articular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'MOVILIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1: Inmovil 2: Muy Limitada 3: Disminuida 4: Total', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'MOVILIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'MOVILIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado mental del paciente: 1=Estuporoso/Comatoso, 2=Confuso, 3=Apático, 4=Alerta. TINYINT. Evaluación de conciencia y orientación. Búsqueda: estado mental, conciencia, orientación, alerta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'ESTMENTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1: Estuporoso y Comatoso 2: Confuso 3: Apatico 4: Alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'ESTMENTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'ESTMENTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado físico general: 1=Muy Malo, 2=Malo, 3=Regular, 4=Bueno. TINYINT. Valoración clínica integral de condición física. Búsqueda: estado general, condición física, salud general.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'ESTFISGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1: Muy Malo 2: Malo 3: Regular 4: Bueno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'ESTFISGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'ESTFISGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro en el sistema de la escala de valoración. DATETIME. Auditoría y trazabilidad de evaluación. Búsqueda: fecha registro, timestamp, auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (médico, enfermera, psicólogo) que realiza la escala. VARCHAR(20), PII ofuscado. FK implícito. Búsqueda: profesional, médico, enfermera, identificación profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'código del profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional donde se realiza la evaluación (urgencias, hospitalización, UCI, consulta). CHAR(10). Búsqueda: unidad funcional, departamento, área, servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'código de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (hospital, clínica, ambulatorio) donde se registra la escala. CHAR(10). Búsqueda: centro atención, institución, hospital, clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'código del centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente. CHAR(10). FK → ADINGRESO. Identificador de atención/internación. Búsqueda: ingreso, atención, número ingreso, internación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (cédula, documento de identidad, identificación). VARCHAR(25), PII ofuscado. FK → INPACIENT. Búsqueda: paciente, cédula, identificación, documento, código paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'código del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código consecutivo único de la escala de valoración. NUMERIC(18) IDENTITY. Clave primaria. Búsqueda: consecutivo, escala, registro escala.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de escalas de valoración funcional del paciente durante un ingreso hospitalario, incluyendo evaluación del estado físico general, mental, movilidad, actividad e incontinencia. Corresponde a la escala de valoración de riesgo o dependencia aplicada por un profesional de salud en un centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCNTON';

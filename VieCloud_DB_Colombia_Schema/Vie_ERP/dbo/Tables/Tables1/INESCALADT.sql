CREATE TABLE [dbo].[INESCALADT] (
    [NUMINGRES]         CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]         VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [EDADPUNTAJE]       TINYINT                                                                          NULL,
    [EDADSELECCION]     TINYINT                                                                          NULL,
    [FECHAREGIS]        DATETIME                                                                         NOT NULL,
    [CAIDPREVIA]        TINYINT                                                                          NOT NULL,
    [TRANQUILIZANTES]   TINYINT                                                                          NOT NULL,
    [DIURETICOS]        TINYINT                                                                          NOT NULL,
    [HIPOTENSORES]      TINYINT                                                                          NOT NULL,
    [ANTIPARKINSON]     TINYINT                                                                          NOT NULL,
    [ANTIDESPRESIVO]    TINYINT                                                                          NOT NULL,
    [OTROMEDICAMEN]     TINYINT                                                                          NOT NULL,
    [MEDICAMENTOS]      VARCHAR (MAX)                                                                    NULL,
    [ALTERACIONVISUAL]  TINYINT                                                                          NOT NULL,
    [ALTERACIONAUDIT]   TINYINT                                                                          NOT NULL,
    [ICTUSEXTREMIDAD]   TINYINT                                                                          NOT NULL,
    [ESTADOMENTAL]      TINYINT                                                                          NOT NULL,
    [SEGURAAYUDA]       TINYINT                                                                          NOT NULL,
    [INSEGURAAYUDA]     TINYINT                                                                          NOT NULL,
    [IMPOSIBLE]         TINYINT                                                                          NOT NULL,
    [PATOLOGIA]         TINYINT                                                                          NOT NULL,
    [ESTADONUTRICIONAL] TINYINT                                                                          NOT NULL,
    CONSTRAINT [PK_INESCALADT_1] PRIMARY KEY CLUSTERED ([IPCODPACI] ASC),
    CONSTRAINT [FK_INESCALADT_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_INESCALADT_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INESCALADT].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado nutricional del paciente: 1=adecuado, 0=inadecuado. Indica evaluación nutricional en contexto de riesgo de caídas. TINYINT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ESTADONUTRICIONAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado nutricional 1=adecuado  0=inadecuado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ESTADONUTRICIONAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ESTADONUTRICIONAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Patologías asociadas a caídas: 1=no asociada, 2=asociada a caídas. Factor clínico en escala de riesgo. TINYINT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'PATOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'patologías 1=No asiciada a caidas  2=asociada a acidas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'PATOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'PATOLOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Capacidad de deambulación imposible (score=4). Paciente no puede movilizarse de forma independiente. TINYINT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'IMPOSIBLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Deambulacion imposible 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'IMPOSIBLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'IMPOSIBLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deambulación insegura con o sin ayuda técnica (score=3). Requiere asistencia pero movilidad presente. TINYINT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'INSEGURAAYUDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Deambulacion insegura con ayuda/sin ayuda 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'INSEGURAAYUDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'INSEGURAAYUDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deambulación segura con ayuda física o dispositivo (score=2). Paciente deambula de forma segura con apoyo. TINYINT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'SEGURAAYUDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Deambulacion segura con ayuda 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'SEGURAAYUDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'SEGURAAYUDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado mental del paciente: 1=orientado, 2=confuso. Evaluación cognitiva en escala de riesgo de caídas. TINYINT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ESTADOMENTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado mental 1=orientado 2=confuso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ESTADOMENTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ESTADOMENTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ictus o accidente cerebrovascular en extremidades (score=3). Factor neurológico de riesgo. TINYINT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ICTUSEXTREMIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ictus en extremidades 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ICTUSEXTREMIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ICTUSEXTREMIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Alteración auditiva presente (score=2). Déficit en audición documentado en escala de riesgo. TINYINT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ALTERACIONAUDIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Alteración auditiva 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ALTERACIONAUDIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ALTERACIONAUDIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Alteración visual presente (score=1). Déficit en visión documentado en escala de riesgo de caídas. TINYINT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ALTERACIONVISUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Alteración visual 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ALTERACIONVISUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ALTERACIONVISUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Listado completo de medicamentos activos. Registro descriptivo en VARCHAR(MAX) de fármacos del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'MEDICAMENTOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'MEDICAMENTOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'MEDICAMENTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de otros medicamentos no clasificados en categorías estándar (si/no binario). TINYINT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'OTROMEDICAMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otro medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'OTROMEDICAMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'OTROMEDICAMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medicamento antidepresivo en uso del paciente (indicador binario). Factor de riesgo farmacológico. TINYINT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ANTIDESPRESIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antidepresivos 5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ANTIDESPRESIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ANTIDESPRESIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medicamento antiparkinsoniano en uso (indicador binario). Fármaco neurológico de riesgo. TINYINT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ANTIPARKINSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'AntiParkinson 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ANTIPARKINSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'ANTIPARKINSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medicamento hipotensor/antihipertensivo en uso (indicador binario). Riesgo de hipotensión. TINYINT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'HIPOTENSORES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hipotensores 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'HIPOTENSORES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'HIPOTENSORES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medicamento diurético en uso del paciente (indicador binario). Factor de riesgo de deshidratación. TINYINT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'DIURETICOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diureticos 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'DIURETICOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'DIURETICOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medicamento tranquilizante/sedante en uso (indicador binario). Riesgo de depresión del sistema nervioso central. TINYINT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'TRANQUILIZANTES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tranquilizantes 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'TRANQUILIZANTES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'TRANQUILIZANTES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de caídas previas: 1=sí, 2=no. Historial clínico de eventos previos. TINYINT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'CAIDPREVIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Caídas previas 1=si  2=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'CAIDPREVIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'CAIDPREVIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro de los datos de la escala de riesgo de caídas. DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'FECHAREGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'FECHAREGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'FECHAREGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo etario del paciente: 0=5 a 59 años, 1=menor de 5 años, 2=mayor de 60 años. Categorización por edad. TINYINT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'EDADSELECCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad seleccionada: 0: de 5 a 59 años, 1: menor de 5 años, 2: Mayor de 60 años , ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'EDADSELECCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'EDADSELECCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntaje asignado por edad en escala (valor 0 o 1). Componente puntuable del riesgo. TINYINT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'EDADPUNTAJE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntaje: 1 o 0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'EDADPUNTAJE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'EDADPUNTAJE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente (identificación/cédula/documento). Tipo VARCHAR(25) con mascara PII. FK a INPACIENT. Identification_Ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único de ingreso/atención hospitalaria del paciente. CHAR(10). FK a ADINGRESO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Escala de valoración de riesgo de caídas por ingreso hospitalario (escala de Downton u similar). Registra los factores de riesgo evaluados en cada paciente ingresado: historial de caídas previas, medicamentos de riesgo, alteraciones sensoriales, estado mental, movilidad y estado nutricional, junto con los puntajes resultantes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INESCALADT';

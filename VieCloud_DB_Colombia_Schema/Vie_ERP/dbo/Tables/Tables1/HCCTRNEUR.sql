CREATE TABLE [dbo].[HCCTRNEUR] (
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                        NOT NULL,
    [CODCENATE]  CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]  CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGIST]  DATETIME                                                                         NOT NULL,
    [APEOJOESP]  CHAR (10)                                                                        NULL,
    [APEOJOVOZ]  CHAR (1)                                                                         NULL,
    [APEOJODOL]  CHAR (1)                                                                         NULL,
    [APEOJONIN]  CHAR (1)                                                                         NULL,
    [RESVERORI]  CHAR (1)                                                                         NULL,
    [RESVERCON]  CHAR (1)                                                                         NULL,
    [RESVERINA]  CHAR (1)                                                                         NULL,
    [RESVERINC]  CHAR (1)                                                                         NULL,
    [RESVERNIN]  CHAR (1)                                                                         NULL,
    [RESMOTOBE]  CHAR (1)                                                                         NULL,
    [RESMOTLOC]  CHAR (1)                                                                         NULL,
    [RESMOTRET]  CHAR (1)                                                                         NULL,
    [RESMOTFLE]  CHAR (1)                                                                         NULL,
    [RESMOTEXT]  CHAR (1)                                                                         NULL,
    [RESMOTFLA]  CHAR (1)                                                                         NULL,
    [VALGLASGO]  CHAR (2)                                                                         NULL,
    [PUPDERTAM]  CHAR (1)                                                                         NULL,
    [PUPDERREAC] CHAR (2)                                                                         NULL,
    [PUPIZQTAM]  CHAR (1)                                                                         NULL,
    [PUPIZQREA]  CHAR (2)                                                                         NULL,
    [FMMMSSNOR]  CHAR (1)                                                                         NULL,
    [FMMMSSDEB]  CHAR (1)                                                                         NULL,
    [FMMMSSAUS]  CHAR (1)                                                                         NULL,
    [FMMMIINOR]  CHAR (1)                                                                         NULL,
    [FMMMIIDEB]  CHAR (1)                                                                         NULL,
    [FMMMIIAUS]  CHAR (1)                                                                         NULL,
    [ESTMENALE]  CHAR (1)                                                                         NULL,
    [ESTMENSOM]  CHAR (1)                                                                         NULL,
    [ESTMENEST]  CHAR (1)                                                                         NULL,
    [ESTMENCOM]  CHAR (1)                                                                         NULL,
    [OBSGENREG]  VARCHAR (500)                                                                    NULL,
    [REFNORMAL]  CHAR (1)                                                                         NULL,
    [REFAUMENT]  CHAR (1)                                                                         NULL,
    [REFDISMIN]  CHAR (1)                                                                         NULL,
    [REFBABYNS]  CHAR (1)                                                                         NULL,
    [RESNORMAL]  CHAR (1)                                                                         NULL,
    [RESCHEYNE]  CHAR (1)                                                                         NULL,
    [RESKUSSMA]  CHAR (1)                                                                         NULL,
    [RESPIBIOT]  CHAR (1)                                                                         NULL,
    [RESAPNEUS]  CHAR (1)                                                                         NULL,
    [RESPARRES]  CHAR (1)                                                                         NULL,
    [RESASIRES]  CHAR (1)                                                                         NULL,
    [POSCABPAC]  CHAR (3)                                                                         NULL,
    CONSTRAINT [PK_HCCTRNEUR] PRIMARY KEY CLUSTERED ([IPCODPACI] ASC, [FECREGIST] ASC),
    CONSTRAINT [FK_HCCTRNEUR_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCCTRNEUR_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCTRNEUR_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCCTRNEUR_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCCTRNEUR_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCCTRNEUR].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCCTRNEUR].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Posición de la cabecera del paciente durante examen neurológico; valores: decúbito dorsal, lateral, incorporado (0-180°). CHAR(3).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'POSCABPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Posicion de la Cabecera del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'POSCABPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'POSCABPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respiración con asistencia respiratoria; patrón de ventilación asistida o mecánica detectado en examen clínico. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESASIRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Respiracion - Asistencia Respiratoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESASIRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESASIRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respiración - paro respiratorio; ausencia total de movimiento respiratorio o apnea completa. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESPARRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Respiracion - Paro Respiratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESPARRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESPARRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respiración apneústica; patrón respiratorio irregular sin fase inspiratoria normal, asociado a lesión mesencefálica. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESAPNEUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Respiracion - Apneustica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESAPNEUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESAPNEUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respiración de Biot; patrón respiratorio con pausas irregulares seguidas de respiraciones profundas, signo neurológico grave. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESPIBIOT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Respiracion - Biot', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESPIBIOT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESPIBIOT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respiración de Kussmaul; respiración profunda y acelerada, típica de acidosis metabólica o coma. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESKUSSMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Respiracion - Kussmaul', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESKUSSMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESKUSSMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respiración de Cheyne-Stokes; patrón cíclico con aumento y disminución gradual de amplitud, separado por apnea. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESCHEYNE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Respiracion - Cheynestoke', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESCHEYNE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESCHEYNE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respiración normal; patrón respiratorio regular, espontáneo, sin anomalías en frecuencia o ritmo. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESNORMAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Respiracion - Normal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESNORMAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESNORMAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reflejo de Babinski; respuesta extensora plantar, signo de afección de vía piramidal. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'REFBABYNS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Reflejos - Babynsky', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'REFBABYNS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'REFBABYNS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reflejos disminuidos; respuesta reflex hipoactiva, reducida o deprimida en comparación con referencia normal. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'REFDISMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Reflejos - Disminuidos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'REFDISMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'REFDISMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reflejos aumentados; respuesta reflex hiperactiva, exagerada, indicativo de irritación o lesión neurológica. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'REFAUMENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Reflejos - Aumentados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'REFAUMENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'REFAUMENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reflejos normales; respuesta reflex osteotendinosa dentro de rango esperado, simétrica bilateral. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'REFNORMAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Reflejos - Normal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'REFNORMAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'REFNORMAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación general del registro neurológico; notas descriptivas adicionales del examen clínico completo. VARCHAR(500).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'OBSGENREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion General del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'OBSGENREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'OBSGENREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado mental - coma; pérdida de conciencia completa, sin respuesta a estímulos, sin ciclo sueño-vigilia. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'ESTMENCOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Mental - Coma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'ESTMENCOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'ESTMENCOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado mental - estupor; obnubilación profunda, despierta solo ante estímulos nociceptivos intensos. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'ESTMENEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Mental - Estupor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'ESTMENEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'ESTMENEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado mental - somnolencia; disminución del nivel de alerta, adormecimiento frecuente pero despertable. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'ESTMENSOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Mental - Somnolencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'ESTMENSOM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'ESTMENSOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado mental - alerta; vigilia normal, despierto espontáneamente, atento y responsivo al ambiente. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'ESTMENALE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Mental - Alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'ESTMENALE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'ESTMENALE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fuerza muscular miembros inferiores ausente; parálisis completa de extremidades inferiores, sin contracción muscular. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FMMMIIAUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fuerza Muscular - MMII Ausente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FMMMIIAUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FMMMIIAUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fuerza muscular miembros inferiores débil; debilidad marcada de extremidades inferiores, movimiento limitado contra gravedad. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FMMMIIDEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fuerza Muscular - MMII Debil', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FMMMIIDEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FMMMIIDEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fuerza muscular miembros inferiores normal; fuerza preservada en extremidades inferiores, grado 5/5. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FMMMIINOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fuerza Muscular - MMII Normal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FMMMIINOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FMMMIINOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fuerza muscular miembros superiores ausente; parálisis completa de extremidades superiores, sin contracción. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FMMMSSAUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fuerza Muscular - MMSS Ausente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FMMMSSAUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FMMMSSAUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fuerza muscular miembros superiores débil; debilidad marcada de extremidades superiores, movimiento limitado. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FMMMSSDEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fuerza Muscular - MMSS Debil', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FMMMSSDEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FMMMSSDEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fuerza muscular miembros superiores normal; fuerza preservada en extremidades superiores, grado 5/5. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FMMMSSNOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fuerza Muscular - MMSS Normal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FMMMSSNOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FMMMSSNOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pupila izquierda - reacción; respuesta pupilar a luz en ojo izquierdo, escala de reactividad. CHAR(2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'PUPIZQREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pupila Izaquierda - Reaccion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'PUPIZQREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'PUPIZQREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pupila izquierda - tamaño; diámetro pupilar del ojo izquierdo en mm, evaluación morfológica. CHAR(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'PUPIZQTAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pupila Izquierda - Tamaño', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'PUPIZQTAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'PUPIZQTAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pupila derecha - reacción; respuesta pupilar a luz en ojo derecho, escala de reactividad. CHAR(2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'PUPDERREAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pupila Derecha - Reaccion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'PUPDERREAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'PUPDERREAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pupila derecha - tamaño; diámetro pupilar del ojo derecho en mm, evaluación morfológica. CHAR(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'PUPDERTAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pupila Derecha - Tamaño', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'PUPDERTAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'PUPDERTAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor Glasgow calculado; puntuación escala de coma de Glasgow (3-15), evaluación neurológica estandarizada. CHAR(2).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'VALGLASGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Galsgow Calculado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'VALGLASGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'VALGLASGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuesta motora - flácida; tono muscular disminuido, sin resistencia al movimiento pasivo. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESMOTFLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Respuesta Motora - Flacido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESMOTFLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESMOTFLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuesta motora - extensión; extensión rígida de extremidades ante estímulo, decerebración. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESMOTEXT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Respuesta Motora - Extension', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESMOTEXT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESMOTEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuesta motora - flexión; flexión anormal de extremidades ante estímulo, descerebración. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESMOTFLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Respuesta Motora - Flexion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESMOTFLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESMOTFLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuesta motora - retira al dolor; retirada localizada de extremidad ante estímulo doloroso. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESMOTRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Respuesta Motora - Retira al Dolor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESMOTRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESMOTRET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuesta motora - localiza dolor; localización precisa del estímulo doloroso, respuesta intencional. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESMOTLOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Respuesta Motora - Localiza Dolor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESMOTLOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESMOTLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuesta motora - obedece órdenes; cumple comandos verbales, comprensión de instrucciones. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESMOTOBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Respuesta Motora - Obedece Ordenes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESMOTOBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESMOTOBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuesta verbal - ninguna; ausencia total de vocalización o lenguaje. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESVERNIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Respuesta Verbal - Ninguna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESVERNIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESVERNIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuesta verbal - ruidos incomprensibles; sonidos ininteligibles, sin palabras reconocibles. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESVERINC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Respuesta Verbal - Ruidos Incomprensibles', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESVERINC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESVERINC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuesta verbal - palabras inapropiadas; palabras aisladas desconectadas del contexto, sin relevancia. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESVERINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Respuesta Verbal - Palabras Inapropiadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESVERINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESVERINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuesta verbal - conversación confusa; lenguaje fluido pero desorientado, incoherente. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESVERCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Respuesta Verbal - Conversacion Confusa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESVERCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESVERCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuesta verbal - conversación orientada; lenguaje coherente, paciente orientado en persona, lugar y tiempo. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESVERORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Respuesta Verbal - Conversacion Orientada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESVERORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESVERORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Apertura de ojos - ninguna; ausencia de apertura ocular ante cualquier estímulo. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'APEOJONIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Apertura de Ojos - Ninguna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'APEOJONIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'APEOJONIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Apertura de ojos al dolor; apertura ocular solo ante estímulo doloroso nociceptivo. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'APEOJODOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Apertura de Ojos - Al Dolor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'APEOJODOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'APEOJODOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Apertura de ojos a la voz; apertura ocular ante comando verbal o ruido audible. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'APEOJOVOZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Apertura de Ojos - A la Voz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'APEOJOVOZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'APEOJOVOZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Apertura de ojos espontánea; apertura ocular sin estímulo, vigilia normal. CHAR(1), booleano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'APEOJOESP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Apertura de Ojos - Espontanea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'APEOJOESP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'APEOJOESP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del registro neurológico; timestamp del examen clínico, clave primaria. DATETIME, formato YYYY-MM-DD HH:MM:SS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FECREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y Hora del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FECREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FECREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud; identificador único del médico/especialista que realiza evaluación neurológica. VARCHAR(20), FK→INPROFSAL, Identification_Ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional; identificador del área clínica (UCI, urgencias, hospitalización) donde se realiza examen. CHAR(10), FK→INUNIFUNC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención; identificador de la institución de salud donde se registra evaluación neurológica. CHAR(10), FK→ADCENATEN.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso; identificador único de la admisión hospitalaria del paciente. CHAR(10), FK→ADINGRESO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente; identificador único paciente, cédula/documento/identificación, clave primaria. VARCHAR(25), FK→INPACIENT, Identification_Ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evaluación neurológica del paciente durante un ingreso hospitalario. Registra el examen neurológico incluyendo apertura ocular, respuesta verbal y motora (Escala de Glasgow), tamaño y reactividad pupilar, fuerza muscular en extremidades, estado mental y patrón respiratorio neurológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR';

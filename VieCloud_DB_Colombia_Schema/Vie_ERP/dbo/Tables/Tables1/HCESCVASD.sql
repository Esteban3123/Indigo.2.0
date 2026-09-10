CREATE TABLE [dbo].[HCESCVASD] (
    [CODCONSEC] NUMERIC (18) NOT NULL,
    [CODTIPDOL] TINYINT      NOT NULL,
    [CODPARCUE] TINYINT      NOT NULL,
    [PTSESTREP] TINYINT      NOT NULL,
    [PTSESTMOV] TINYINT      NOT NULL,
    [POSIMGCUX] INT          NOT NULL,
    [POSIMGCUY] INT          NOT NULL,
    CONSTRAINT [PK_HCESCVASD] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC, [CODPARCUE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Coordenada Y de posición en imagen de dolor corporal; eje vertical para mapeo espacial de localización del síntoma (NUMERIC/INT, sin máscara).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCVASD', @level2type = N'COLUMN', @level2name = N'POSIMGCUY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Posicion Y', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCVASD', @level2type = N'COLUMN', @level2name = N'POSIMGCUY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCVASD', @level2type = N'COLUMN', @level2name = N'POSIMGCUY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Coordenada X de posición en imagen de dolor corporal; eje horizontal para mapeo espacial de localización del síntoma (NUMERIC/INT, sin máscara).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCVASD', @level2type = N'COLUMN', @level2name = N'POSIMGCUX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Posicion X', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCVASD', @level2type = N'COLUMN', @level2name = N'POSIMGCUX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCVASD', @level2type = N'COLUMN', @level2name = N'POSIMGCUX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntos de movimiento articular o muscular; intensidad o cantidad de puntos con dolor durante el movimiento en escala evaluada (TINYINT, 0-255).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCVASD', @level2type = N'COLUMN', @level2name = N'PTSESTMOV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'puntos de movimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCVASD', @level2type = N'COLUMN', @level2name = N'PTSESTMOV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCVASD', @level2type = N'COLUMN', @level2name = N'PTSESTMOV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntos de reposo; intensidad o cantidad de puntos con dolor en posición estática o reposo en escala evaluada (TINYINT, 0-255).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCVASD', @level2type = N'COLUMN', @level2name = N'PTSESTREP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Puntos de reposo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCVASD', @level2type = N'COLUMN', @level2name = N'PTSESTREP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCVASD', @level2type = N'COLUMN', @level2name = N'PTSESTREP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de parte del cuerpo evaluada (cabeza anterior/posterior, cuello, miembros superiores/inferiores, tórax, abdomen, pelvis, región dorsal/lumbar/glútea); FK implícita a catálogo anatómico (TINYINT, 0-17).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCVASD', @level2type = N'COLUMN', @level2name = N'CODPARCUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parte del cuerpo  0: Cabeza Anterior  1: Cuello Anterior  2: Miembro Superior Anterior Derecho  3: Torax  4: Miembro Superior Anterior Izquierdo  5: Abdomen  6: Pelvis  7: Miembro Inferior Anterior Derecho  8: Miembro Inferior Anterior Izquierdo  9: Cabeza Posterior  10: Cuello Posterior  11: Miembro Superior Posterior Izquierdo  12: Region Dorsal  13: Miembro Superior Posterior Derecho  14: Region Lumbar  15: Region Glutea  16: Miembro Inferior Posterior Izquierdo  17: Miembro Inferior Posterior Derecho  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCVASD', @level2type = N'COLUMN', @level2name = N'CODPARCUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCVASD', @level2type = N'COLUMN', @level2name = N'CODPARCUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo de dolor, característica cualitativa: sordo, punzante, palpitante, quemante, hormigueo, opresivo, adormecido, calambres, dolorido (TINYINT, 0-8).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCVASD', @level2type = N'COLUMN', @level2name = N'CODTIPDOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de dolor  0  ''''Sordo''''  1 ''''Punzante / Cortante  2  ''''Palpitante''''  3  ''''Quemante''''  4  ''''Hormigueo, Burbujeo  5 ''''Opresivo''''  6 ''''Adormecido''''  7 ''''Calambres''''   8 ''''Dolorido''''', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCVASD', @level2type = N'COLUMN', @level2name = N'CODTIPDOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCVASD', @level2type = N'COLUMN', @level2name = N'CODTIPDOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo único de escala de valoración del dolor corporal (HCESCVASD); clave primaria agrupada con CODPARCUE (NUMERIC(18), PK).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCVASD', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCVASD', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCVASD', @level2type = N'COLUMN', @level2name = N'CODCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de la escala de valoración del dolor VASD (Visual Analogue Scale for Dyspnea o escala visual análoga del dolor), donde se almacena la evaluación clínica del dolor del paciente incluyendo tipo de dolor, parte del cuerpo afectada, puntajes de respuesta y movimiento, y la posición gráfica marcada en la escala visual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCVASD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCVASD';

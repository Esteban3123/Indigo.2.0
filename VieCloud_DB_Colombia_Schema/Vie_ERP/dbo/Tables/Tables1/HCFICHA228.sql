CREATE TABLE [dbo].[HCFICHA228] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [D16]                 INT           NULL,
    [D15]                 INT           NULL,
    [D13]                 INT           NULL,
    [D12]                 INT           NULL,
    [D11]                 INT           NULL,
    [D21]                 INT           NULL,
    [D22]                 INT           NULL,
    [D23]                 INT           NULL,
    [D25]                 INT           NULL,
    [D26]                 INT           NULL,
    [D46]                 INT           NULL,
    [D36]                 INT           NULL,
    [CLASIFCLIN]          INT           NULL,
    [PRESECARI]           BIT           NULL,
    [TIPCARIE]            BIT           NULL,
    [FUENTECONS]          INT           NULL,
    [INGESTCREM]          BIT           NULL,
    [INGESTENJUA]         BIT           NULL,
    [APLITOP]             BIT           NULL,
    [PERSORECLAC]         INT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA228] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA228_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA228_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA228] NOCHECK CONSTRAINT [CK_HCFICHA228_JSON];




GO
ALTER TABLE [dbo].[HCFICHA228] NOCHECK CONSTRAINT [CK_HCFICHA228_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos adicionales en formato JSON válido (VARCHAR MAX); contiene campos dinámicos de la notificación odontológica según versión de ficha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación odontológica; valor nulo indica primera versión, formato ej. V01_2020-03-06', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lactancia materna exclusiva hasta los 6 meses (solo en consultas con madre); 1=Sí, 2=No, 3=Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'PERSORECLAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'La Persona Recibió Lactancia Materna Exclusiva hasta los 6 meses (Solo personas que acuden con su madre a consulta)  1=si  2=no  3=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'PERSORECLAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'PERSORECLAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aplicaciones tópicas de flúor en último año (prevención dental); verdadero=Sí, falso=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'APLITOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aplicaciones Tópicas de Flúor en el ultimo año  True = Si False = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'APLITOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'APLITOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ingesta de enjuague bucal; verdadero=Sí, falso=No. [OBSOLETO desde V01_2020-03-06]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'INGESTENJUA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ingesta de Enjuague Bucal  True = Si False = NO    --- (Campo Obsoleto desde version V01_2020-03-06 )', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'INGESTENJUA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'INGESTENJUA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ingesta de crema dental; verdadero=Sí, falso=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'INGESTCREM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ingesta de Crema Dental  True = Si   False = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'INGESTCREM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'INGESTCREM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fuente de consumo de agua (solo niños ≤6 años y gestantes); 1=Acueducto, 2=Pozo/Aljibe, 3=Quebrada, 4=Embotellada, 5=Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'FUENTECONS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fuente de Consumo de Agua (Sólo niños de6 años y gestantes)  1=Acueducto  2=Pozo Subterráneo/Aljibe  3=Quebrada  4=Agua Embotellada  5=Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'FUENTECONS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'FUENTECONS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de caries dental; verdadero=Activa, falso=Inactiva. [OBSOLETO desde V01_2020-03-06]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'TIPCARIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Caries  True = activa  Fasle = Inactiva    --- (Campo Obsoleto desde version V01_2020-03-06 )', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'TIPCARIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'TIPCARIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de caries detectada en examen odontológico; verdadero=Sí, falso=No. [OBSOLETO desde V01_2020-03-06]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'PRESECARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Presencia de Caries  True = si   False = No    --- (Campo Obsoleto desde version V01_2020-03-06 )', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'PRESECARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'PRESECARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación clínica de fluorosis según Índice de Dean; 0=Normal, 1=Dudoso, 2=Muy Leve, 3=Leve, 4=Moderada, 5=Severa, 9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'CLASIFCLIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificación Clínica de las lesiones (Índice de Dean)  0=Normal1=Dudoso2=Muy Leve3=Leve4=Moderada5=Severa9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'CLASIFCLIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'CLASIFCLIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de diente 36 (molar inferior derecho) - Índice de Dean; 0=Normal, 1=Dudoso, 2=Muy Leve, 3=Leve, 4=Moderada, 5=Severa, 9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D36';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  0=Normal  1=Dudoso  2=Muy Leve  3=Leve  4=Moderada  5=Severa  9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D36';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D36';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de diente 46 (molar inferior izquierdo) - Índice de Dean; 0=Normal, 1=Dudoso, 2=Muy Leve, 3=Leve, 4=Moderada, 5=Severa, 9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D46';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  0=Normal  1=Dudoso  2=Muy Leve  3=Leve  4=Moderada  5=Severa  9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D46';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D46';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de diente 26 (molar superior izquierdo) - Índice de Dean; 0=Normal, 1=Dudoso, 2=Muy Leve, 3=Leve, 4=Moderada, 5=Severa, 9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D26';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  0=Normal  1=Dudoso  2=Muy Leve  3=Leve  4=Moderada  5=Severa  9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D26';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D26';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de diente 25 (premolar superior izquierdo) - Índice de Dean; 0=Normal, 1=Dudoso, 2=Muy Leve, 3=Leve, 4=Moderada, 5=Severa, 9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D25';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  0=Normal  1=Dudoso  2=Muy Leve  3=Leve  4=Moderada  5=Severa  9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D25';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D25';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de diente 23 (canino superior izquierdo) - Índice de Dean; 0=Normal, 1=Dudoso, 2=Muy Leve, 3=Leve, 4=Moderada, 5=Severa, 9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D23';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  0=Normal  1=Dudoso  2=Muy Leve  3=Leve  4=Moderada  5=Severa  9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D23';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D23';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de diente 22 (incisivo superior izquierdo) - Índice de Dean; 0=Normal, 1=Dudoso, 2=Muy Leve, 3=Leve, 4=Moderada, 5=Severa, 9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D22';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  0=Normal  1=Dudoso  2=Muy Leve  3=Leve  4=Moderada  5=Severa  9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D22';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D22';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de diente 21 (incisivo superior izquierdo central) - Índice de Dean; 0=Normal, 1=Dudoso, 2=Muy Leve, 3=Leve, 4=Moderada, 5=Severa, 9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D21';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  0=Normal  1=Dudoso  2=Muy Leve  3=Leve  4=Moderada  5=Severa  9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D21';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D21';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de diente 11 (incisivo superior derecho central) - Índice de Dean; 0=Normal, 1=Dudoso, 2=Muy Leve, 3=Leve, 4=Moderada, 5=Severa, 9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D11';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  0=Normal  1=Dudoso  2=Muy Leve  3=Leve  4=Moderada  5=Severa  9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D11';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D11';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de diente 12 (incisivo superior derecho) - Índice de Dean; 0=Normal, 1=Dudoso, 2=Muy Leve, 3=Leve, 4=Moderada, 5=Severa, 9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D12';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  0=Normal  1=Dudoso  2=Muy Leve  3=Leve  4=Moderada  5=Severa  9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D12';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D12';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de diente 13 (canino superior derecho) - Índice de Dean; 0=Normal, 1=Dudoso, 2=Muy Leve, 3=Leve, 4=Moderada, 5=Severa, 9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D13';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  0=Normal  1=Dudoso  2=Muy Leve  3=Leve  4=Moderada  5=Severa  9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D13';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D13';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de diente 15 (premolar superior derecho) - Índice de Dean; 0=Normal, 1=Dudoso, 2=Muy Leve, 3=Leve, 4=Moderada, 5=Severa, 9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D15';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  0=Normal  1=Dudoso  2=Muy Leve  3=Leve  4=Moderada  5=Severa  9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D15';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D15';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de diente 16 (molar superior derecho) - Índice de Dean para fluorosis dental; 0=Normal, 1=Dudoso, 2=Muy Leve, 3=Leve, 4=Moderada, 5=Severa, 9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D16';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Datos Especificos  0=Normal  1=Dudoso  2=Muy Leve  3=Leve  4=Moderada  5=Severa  9=No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D16';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'D16';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico odontológico (CHAR 4); se refiere a patología bucal identificada en examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de ficha notificación odontológica (FK a HCFICHANOTIFICACION); relaciona examen dental con evento de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único (INT IDENTITY); clave primaria de registro de examen dental', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha 228 de historia clínica odontológica: registra el índice COP/CEO y la clasificación de caries dentales por superficie (D11-D46), junto con datos clínicos de riesgo cariogénico, fuente de consulta y hábitos de higiene oral del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA228';

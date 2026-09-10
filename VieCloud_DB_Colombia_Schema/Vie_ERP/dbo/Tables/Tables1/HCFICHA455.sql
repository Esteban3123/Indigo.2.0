CREATE TABLE [dbo].[HCFICHA455] (
    [ID]                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT          NOT NULL,
    [CODDIAGNO]           CHAR (4)     NULL,
    [VISTORATAS]          BIT          NULL,
    [SISTALCANT]          BIT          NULL,
    [AGUASESTAN]          BIT          NULL,
    [DISPORESISOL]        BIT          NULL,
    [CUALOTRO]            VARCHAR (50) NULL,
    [FIEBRE]              BIT          NULL,
    [CEFALEA]             BIT          NULL,
    [MIALGIAS]            BIT          NULL,
    [HEPATOMEGALIA]       BIT          NULL,
    [ICTERICIA]           BIT          NULL,
    [PERROS]              BIT          NULL,
    [GATOS]               BIT          NULL,
    [BOVINOS]             BIT          NULL,
    [EQUINOS]             BIT          NULL,
    [PORCINOS]            BIT          NULL,
    [NINGUNO]             BIT          NULL,
    [OTROS]               BIT          NULL,
    [ACUEDUCTO]           BIT          NULL,
    [POZOCOMUN]           BIT          NULL,
    [RIO]                 BIT          NULL,
    [TANQUE]              BIT          NULL,
    [REPRESA]             BIT          NULL,
    [RIO2]                BIT          NULL,
    [ARROYO]              BIT          NULL,
    [LAGO]                BIT          NULL,
    [SINATENCE]           BIT          NULL,
    [VERSION]             VARCHAR (20) NULL,
    CONSTRAINT [PK_HCFICHA455] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCFICHA455_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: Sin antecedente de actividades recreativas en cuerpos de agua (represa, lago) últimos 30 días. true=Sí, false=No. Epidemiología, vigilancia en salud pública.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'SINATENCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si  ahi Actividades recreactivas en represa , lago en los ultimos 30  dias  es seleccionada Sin antecedente   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'SINATENCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'SINATENCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: Exposición a actividades recreativas en lago o laguna últimos 30 días. true=Sí, false=No. Riesgo de enfermedades transmitidas por agua.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'LAGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si  ahi Actividades recreactivas en represa , lago en los ultimos 30  dias  es seleccionada   Lago/Laguna  true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'LAGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'LAGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: Exposición a actividades recreativas en arroyo últimos 30 días. true=Sí, false=No. Factor de riesgo ambiental, contact con agua superficial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'ARROYO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si  ahi Actividades recreactivas en represa , lago en los ultimos 30  dias  es seleccionada  Arroyo  true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'ARROYO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'ARROYO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: Exposición a actividades recreativas en río últimos 30 días. true=Sí, false=No. Vigilancia epidemiológica de enfermedades hídricas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'RIO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si  ahi Actividades recreactivas en represa , lago en los ultimos 30  dias  es seleccionada   Rio  true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'RIO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'RIO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: Exposición a actividades recreativas en represa últimos 30 días. true=Sí, false=No. Antecedente de contacto con agua embalsada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'REPRESA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si  ahi Actividades recreactivas en represa , lago en los ultimos 30  dias  es seleccionada   Represa   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'REPRESA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'REPRESA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: Abastecimiento de agua por tanque/cisterna. true=Sí, false=No. Tipo de fuente de agua domiciliaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'TANQUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si  ahi Abastecimiento de agua  es seleccionada   Tanque   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'TANQUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'TANQUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: Abastecimiento de agua directo de río. true=Sí, false=No. Fuente de agua no potabilizada, riesgo sanitario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'RIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si  ahi Abastecimiento de agua  es seleccionada   Rio   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'RIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'RIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: Abastecimiento de agua por pozo comunitario o compartido. true=Sí, false=No. Fuente de agua subterránea comunitaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'POZOCOMUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si  ahi Abastecimiento de agua  es seleccionada   Pozo comunitario    true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'POZOCOMUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'POZOCOMUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: Abastecimiento de agua por acueducto municipal. true=Sí, false=No. Fuente de agua potabilizada, servicio público.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'ACUEDUCTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si  ahi Abastecimiento de agua  es seleccionada   Acueducto    true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'ACUEDUCTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'ACUEDUCTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: Presencia de otros animales en casa/domicilio (no especificados). true=Sí, false=No. Convivencia animal doméstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'OTROS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si  Hay animales en casa   es seleccionada   Otros    true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'OTROS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'OTROS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: Sin animales en casa/domicilio. true=Sí, false=No. Ausencia de mascotas o animales de compañía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'NINGUNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si  Hay animales en casa   es seleccionada  Ninguno   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'NINGUNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'NINGUNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: Presencia de cerdos/porcinos en casa o alrededor. true=Sí, false=No. Exposición a animales de granja domésticos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'PORCINOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si  Hay animales en casa   es seleccionada   Porcinos   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'PORCINOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'PORCINOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: Presencia de caballos/equinos en casa o alrededor. true=Sí, false=No. Exposición a animales de trabajo o recreación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'EQUINOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si  Hay animales en casa   es seleccionada   Equinos   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'EQUINOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'EQUINOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: Presencia de vacas/bovinos en casa o alrededor. true=Sí, false=No. Exposición a ganado mayor doméstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'BOVINOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si  Hay animales en casa   es seleccionada   Bovinos    true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'BOVINOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'BOVINOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: Presencia de gatos en casa/domicilio. true=Sí, false=No. Mascota felina, factor zoonótico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'GATOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si  Hay animales en casa   es seleccionada   Gatos    true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'GATOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'GATOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: Presencia de perros en casa/domicilio. true=Sí, false=No. Mascota canina, exposición animal doméstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'PERROS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si  Hay animales en casa   es seleccionada   Perros     true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'PERROS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'PERROS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: Signo clínico de coloración amarillenta de piel/mucosas. true=Presente, false=Ausente. Manifestación de hepatopatía o hemólisis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'ICTERICIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si signos y sintomas   es seleccionada   Ictericia     true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'ICTERICIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'ICTERICIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: Signo clínico de aumento de tamaño del hígado a palpación. true=Presente, false=Ausente. Hallazgo físico hepatopatológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'HEPATOMEGALIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si signos y sintomas   es seleccionada   Hepatomegalia    true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'HEPATOMEGALIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'HEPATOMEGALIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: Síntoma de dolor muscular generalizado. true=Presente, false=Ausente. Manifestación musculoesquelética sistémica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'MIALGIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si signos y sintomas   es seleccionada   Mialgias   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'MIALGIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'MIALGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: Síntoma de dolor de cabeza. true=Presente, false=Ausente. Cefalgia, manifestación neurológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'CEFALEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si signos y sintomas   es seleccionada   Cefalea      true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'CEFALEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'CEFALEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: Signo vital elevado, temperatura corporal >38°C. true=Presente, false=Ausente. Manifestación inflamatoria/infecciosa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si signos y sintomas   es seleccionada Fiebre     true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo texto VARCHAR(50): Descripción de otro signo/síntoma o exposición no listado en categorías predefinidas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'CUALOTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda   Cual  Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'CUALOTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'CUALOTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: Disposición de residuos sólidos. true/1=Recolección municipal, false/0=Disposición peridomiciliaria. Saneamiento ambiental.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'DISPORESISOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda disposicuión de residuos  solidos  1 =  Recolección  0 =disposición pericomiciliaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'DISPORESISOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'DISPORESISOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: Contacto con aguas estancadas (charcos, lagnas, pozos) últimos 30 días. true/1=Sí, false/0=No. Exposición ambiental, vigilancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'AGUASESTAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el contacto con aguas estancadas en los ultimos 30 dias   1 = Si    0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'AGUASESTAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'AGUASESTAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: Disponibilidad de sistema de alcantarillado en domicilio. true/1=Sí, false/0=No. Infraestructura sanitaria, saneamiento básico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'SISTALCANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  cuenta con sistema de alcantarillado   1 = Si  0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'SISTALCANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'SISTALCANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: Presencia observada de ratas dentro/alrededor de domicilio o lugar de trabajo. true/1=Sí, false/0=No. Control de plagas, epidemiología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'VISTORATAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda visto ratas dentro o alrededor de su domicilio o lugar de trabajo   1 = Si   0  = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'VISTORATAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'VISTORATAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo CHAR(4): Código diagnóstico CIE-10 u otra clasificación diagnóstica. FK a diagnóstico principal/notificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda  codigo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, clave foránea: Referencia a tabla HCFICHANOTIFICACION. Vincula evento notificable, vigilancia epidemiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT Identity: Identificador único (PK) de registro en HCFICHA455. Consecutivo auto-incremental, clave primaria clustered.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación epidemiológica 455 (Leptospirosis u otras enfermedades transmisibles de vigilancia). Registra síntomas clínicos, exposición a animales, fuentes de agua y condiciones ambientales del caso notificado, asociado a un diagnóstico CIE-10.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión del formulario o ficha de notificación utilizada para el registro del caso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA455', @level2type = N'COLUMN', @level2name = N'VERSION';

CREATE TABLE [dbo].[INTANTBII] (
    [AUTO]         INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ORDEN_INDIGO] VARCHAR (20) NOT NULL,
    [CODSERIPS]    VARCHAR (15) NOT NULL,
    [NOMUESTRA]    VARCHAR (50) NOT NULL,
    [NOMMICROR]    VARCHAR (50) NOT NULL,
    [NOMANTIBI]    VARCHAR (50) NOT NULL,
    [CMI]          VARCHAR (50) NOT NULL,
    [RESULTADO]    VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_INTANTBII] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultados de antibiograma por orden de laboratorio. Registra, para cada muestra analizada, los microorganismos identificados y la sensibilidad o resistencia a cada antibiótico evaluado, incluyendo la concentración mínima inhibitoria (CMI).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTBII';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTBII';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno autonumérico del registro de antibiograma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTBII', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTBII', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o código de la orden de laboratorio en el sistema Indigo a la que pertenece este resultado de antibiograma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTBII', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTBII', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio o procedimiento de laboratorio (CUPS/IPS) correspondiente al cultivo o antibiograma solicitado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTBII', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTBII', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del tipo de muestra biológica analizada (ejemplo: orina, hemocultivo, secreción herida).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTBII', @level2type = N'COLUMN', @level2name = N'NOMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTBII', @level2type = N'COLUMN', @level2name = N'NOMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del microorganismo identificado en el cultivo (bacteria, hongo u otro agente infeccioso hallado en la muestra).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTBII', @level2type = N'COLUMN', @level2name = N'NOMMICROR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTBII', @level2type = N'COLUMN', @level2name = N'NOMMICROR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del antibiótico o antimicrobiano evaluado en el antibiograma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTBII', @level2type = N'COLUMN', @level2name = N'NOMANTIBI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTBII', @level2type = N'COLUMN', @level2name = N'NOMANTIBI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración Mínima Inhibitoria (CMI): valor de concentración del antibiótico a partir del cual se inhibe el crecimiento del microorganismo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTBII', @level2type = N'COLUMN', @level2name = N'CMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTBII', @level2type = N'COLUMN', @level2name = N'CMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de sensibilidad del microorganismo al antibiótico evaluado (sensible, intermedio, resistente u otro valor reportado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTBII', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTBII', @level2type = N'COLUMN', @level2name = N'RESULTADO';

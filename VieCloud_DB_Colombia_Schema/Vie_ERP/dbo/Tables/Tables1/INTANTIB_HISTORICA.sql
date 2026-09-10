CREATE TABLE [dbo].[INTANTIB_HISTORICA] (
    [AUTO] INT NOT NULL,
    [ORDEN_INDIGO] INT NOT NULL,
    [CODSERIPS] CHAR (20) NOT NULL,
    [NOMUESTRA] VARCHAR (50) NOT NULL,
    [NOMMICROR] VARCHAR (150) NULL,
    [NOMANTIBI] VARCHAR (150) NOT NULL,
    [CMI] VARCHAR (50) NOT NULL,
    [RESULTADO] VARCHAR (MAX) NULL,
    [NUMMUESTRA] INT NULL,
    [IDINTERCTRL] INT NULL,
    [MICROBIOLOGIA] BIT NULL,
    [TIPORESULTADO] BIT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Historial de resultados de antibiogramas: registra por cada muestra de laboratorio los microorganismos identificados, los antibióticos evaluados, la concentración mínima inhibitoria (CMI) y el resultado de sensibilidad o resistencia. Sirve para el seguimiento microbiológico de pacientes y el control de infecciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIB_HISTORICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIB_HISTORICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno autoincremental del registro de antibiograma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIB_HISTORICA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIB_HISTORICA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de orden de laboratorio en el sistema Indigo al que pertenece este resultado de antibiograma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIB_HISTORICA', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIB_HISTORICA', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio o procedimiento de laboratorio (CUPS/RIPS) asociado al antibiograma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIB_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIB_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del tipo de muestra analizada (por ejemplo: orina, hemocultivo, secreción herida).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIB_HISTORICA', @level2type = N'COLUMN', @level2name = N'NOMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIB_HISTORICA', @level2type = N'COLUMN', @level2name = N'NOMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del microorganismo identificado en el cultivo (bacteria, hongo u otro agente infeccioso).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIB_HISTORICA', @level2type = N'COLUMN', @level2name = N'NOMMICROR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIB_HISTORICA', @level2type = N'COLUMN', @level2name = N'NOMMICROR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del antibiótico o antifúngico evaluado en el antibiograma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIB_HISTORICA', @level2type = N'COLUMN', @level2name = N'NOMANTIBI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIB_HISTORICA', @level2type = N'COLUMN', @level2name = N'NOMANTIBI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración Mínima Inhibitoria (CMI): valor que indica la menor concentración del antibiótico capaz de inhibir el crecimiento del microorganismo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIB_HISTORICA', @level2type = N'COLUMN', @level2name = N'CMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIB_HISTORICA', @level2type = N'COLUMN', @level2name = N'CMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de sensibilidad del microorganismo frente al antibiótico evaluado (sensible, intermedio, resistente u observaciones adicionales).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIB_HISTORICA', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial o identificador de la muestra dentro de la orden de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIB_HISTORICA', @level2type = N'COLUMN', @level2name = N'NUMMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIB_HISTORICA', @level2type = N'COLUMN', @level2name = N'NUMMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de control interno o interoperabilidad con el sistema externo de laboratorio que generó el resultado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIB_HISTORICA', @level2type = N'COLUMN', @level2name = N'IDINTERCTRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIB_HISTORICA', @level2type = N'COLUMN', @level2name = N'IDINTERCTRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de si el resultado corresponde a un examen de microbiología (cultivo): verdadero = es microbiología, falso = no aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIB_HISTORICA', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIB_HISTORICA', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de resultado del antibiograma: distingue entre resultado cuantitativo (CMI numérico) y cualitativo (sensible/resistente/intermedio).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIB_HISTORICA', @level2type = N'COLUMN', @level2name = N'TIPORESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTANTIB_HISTORICA', @level2type = N'COLUMN', @level2name = N'TIPORESULTADO';

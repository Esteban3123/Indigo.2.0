CREATE TABLE [dbo].[INTERCTRL_HISTORICA] (
    [AUTO] INT NOT NULL,
    [ORDEN_INDIGO] VARCHAR (20) NOT NULL,
    [NUMUESTRA] TINYINT NOT NULL,
    [ESTADOINT] BIT NOT NULL,
    [INTERPRET] VARCHAR (MAX) NULL,
    [AUTOLABOR] INT NOT NULL,
    [FECGENERA] VARCHAR (25) NULL,
    [FECREGIST] DATETIME NULL,
    [FECSERIPS] VARCHAR (20) NULL,
    [CODPROSAL] CHAR (70) NULL,
    [MICROBIOLOGIA] BIT NULL,
    [TIPORESULTADO] BIT NULL
);
GO
CREATE NONCLUSTERED INDEX [nci_msft_1_INTERCTRL_HISTORICA_6B46F6B1A4731580FCC067A7804430FA] ON [dbo].[INTERCTRL_HISTORICA] ([AUTOLABOR] ASC, [ORDEN_INDIGO] ASC) INCLUDE ([CODPROSAL], [ESTADOINT], [FECREGIST], [FECSERIPS], [MICROBIOLOGIA], [NUMUESTRA], [TIPORESULTADO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro histórico de interpretaciones y resultados de órdenes de laboratorio (interoperabilidad). Guarda el estado, las fechas clave y la interpretación de cada muestra procesada, incluyendo resultados de microbiología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL_HISTORICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL_HISTORICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno autoincremental del registro histórico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL_HISTORICA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL_HISTORICA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o código de la orden de laboratorio en el sistema Indigo, identificador de la solicitud de examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL_HISTORICA', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL_HISTORICA', @level2type = N'COLUMN', @level2name = N'ORDEN_INDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de muestra asociada a la orden, permite distinguir varias muestras dentro de un mismo pedido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL_HISTORICA', @level2type = N'COLUMN', @level2name = N'NUMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL_HISTORICA', @level2type = N'COLUMN', @level2name = N'NUMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la integración o interpretación del resultado (activo/inactivo, procesado/pendiente).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL_HISTORICA', @level2type = N'COLUMN', @level2name = N'ESTADOINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL_HISTORICA', @level2type = N'COLUMN', @level2name = N'ESTADOINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto con la interpretación clínica o comentario del resultado emitido por el laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL_HISTORICA', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL_HISTORICA', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno del laboratorio o del sistema externo que procesó la muestra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL_HISTORICA', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL_HISTORICA', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se generó el resultado o la interpretación en el laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECGENERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECGENERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que el resultado fue registrado en el sistema (fecha de registro, grabación).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del servicio reportada para RIPS, utilizada en la generación de reportes obligatorios de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL_HISTORICA', @level2type = N'COLUMN', @level2name = N'FECSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud que ordenó o validó el resultado; identificación del médico o profesional tratante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el resultado corresponde a un examen de microbiología (cultivo, antibiograma, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL_HISTORICA', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL_HISTORICA', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo o clasificación del resultado devuelto por el laboratorio (por ejemplo: numérico, textual, positivo/negativo).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL_HISTORICA', @level2type = N'COLUMN', @level2name = N'TIPORESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERCTRL_HISTORICA', @level2type = N'COLUMN', @level2name = N'TIPORESULTADO';

CREATE TABLE [dbo].[CHTARIFAS] (
    [CODPLAHOS] CHAR (3)     NOT NULL,
    [CODTIPEST] CHAR (3)     NOT NULL,
    [CODICAMAS] INT          NOT NULL,
    [TIPDISPON] INT          NOT NULL,
    [CODSERIPS] CHAR (20)    NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_CHTARIFAS] PRIMARY KEY CLUSTERED ([CODPLAHOS] ASC, [CODTIPEST] ASC, [CODICAMAS] ASC),
    CONSTRAINT [FK_CHTARIFAS_CHCAMASHO] FOREIGN KEY ([CODICAMAS]) REFERENCES [dbo].[CHCAMASHO] ([CODICAMAS]),
    CONSTRAINT [FK_CHTARIFAS_COPLANTIL] FOREIGN KEY ([CODPLAHOS]) REFERENCES [dbo].[COPLANTIL] ([CODPLANTI]) ON UPDATE CASCADE
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de auditoría/indicador de auditoría. Numérico(18), identifica el registro de auditoría o aprobación de la tarifa en el proceso de facturación y control de servicios de hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTARIFAS', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTARIFAS', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTARIFAS', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio IPS a facturar. Carácter(20), referencia al servicio de salud que se cobra al plan de beneficios; usado en RIPS, facturación y glosas de atención hospitalaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTARIFAS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Servicio IPS a Facturar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTARIFAS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTARIFAS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de disponibilidad de la cama para el plan de beneficios. Entero: 1=Disponible, 2=Requiere autorización previa para asignación, 3=No disponible. Controla acceso a cama de hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTARIFAS', @level2type = N'COLUMN', @level2name = N'TIPDISPON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Disponibilidad de la Cama para el plan de beneficios:  1: Disponible  2: Requiere de Autorizacion para asignarla  3: No Disponible', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTARIFAS', @level2type = N'COLUMN', @level2name = N'TIPDISPON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTARIFAS', @level2type = N'COLUMN', @level2name = N'TIPDISPON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la cama de hospitalización. Entero, FK a CHCAMASHO; identifica la unidad física de alojamiento en el centro de atención para ingreso y estancia del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTARIFAS', @level2type = N'COLUMN', @level2name = N'CODICAMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTARIFAS', @level2type = N'COLUMN', @level2name = N'CODICAMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTARIFAS', @level2type = N'COLUMN', @level2name = N'CODICAMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de estancia hospitalaria. Carácter(3), clasifica la modalidad de hospitalización (urgencia, electiva, UCI, piso, etc.) para tarifación y facturación RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTARIFAS', @level2type = N'COLUMN', @level2name = N'CODTIPEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTARIFAS', @level2type = N'COLUMN', @level2name = N'CODTIPEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTARIFAS', @level2type = N'COLUMN', @level2name = N'CODTIPEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plantilla de hospitalización. Carácter(3), FK a COPLANTIL; define el plan de beneficios y estructura de tarifas aplicables a servicios de ingreso y atención hospitalaria del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTARIFAS', @level2type = N'COLUMN', @level2name = N'CODPLAHOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plantilla de Hospitalizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTARIFAS', @level2type = N'COLUMN', @level2name = N'CODPLAHOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTARIFAS', @level2type = N'COLUMN', @level2name = N'CODPLAHOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tarifas de servicios según plan hospitalario, tipo de estancia y disponibilidad de camas. Relaciona cada combinación de plan, categoría de habitación y servicio (CUPS) con su indicador de auditoría o tarifa vigente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTARIFAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTARIFAS';

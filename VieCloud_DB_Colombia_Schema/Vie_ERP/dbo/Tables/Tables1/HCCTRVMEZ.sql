CREATE TABLE [dbo].[HCCTRVMEZ] (
    [CONSECUTI] NUMERIC (18) NOT NULL,
    [NOMMEZCLA] CHAR (200)   NOT NULL,
    [FECHAINIC] DATETIME     NOT NULL,
    CONSTRAINT [PK_HCCTRVMEZ] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC, [NOMMEZCLA] ASC),
    CONSTRAINT [FK_HCCTRVMEZ_HCCTRVENP] FOREIGN KEY ([CONSECUTI]) REFERENCES [dbo].[HCCTRVENP] ([CONSECUTI])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial de la venopunción; momento de inicio de la extracción de sangre o procedimiento de punción venosa. Tipo: DATETIME.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVMEZ', @level2type = N'COLUMN', @level2name = N'FECHAINIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial de la Venopuncion  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVMEZ', @level2type = N'COLUMN', @level2name = N'FECHAINIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVMEZ', @level2type = N'COLUMN', @level2name = N'FECHAINIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o denominación de la mezcla, combinación de reactivos o aditivos utilizados en el tubo de recolección de muestra durante la venopunción. Tipo: CHAR(200).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVMEZ', @level2type = N'COLUMN', @level2name = N'NOMMEZCLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVMEZ', @level2type = N'COLUMN', @level2name = N'NOMMEZCLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVMEZ', @level2type = N'COLUMN', @level2name = N'NOMMEZCLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo o número identificador único del procedimiento de venopunción; clave primaria que vincula con el registro de eventos de venopunción en HCCTRVENP. Tipo: NUMERIC(18). FK a HCCTRVENP.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVMEZ', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la Venopuncion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVMEZ', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVMEZ', @level2type = N'COLUMN', @level2name = N'CONSECUTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de mezclas o preparaciones farmacéuticas definidas en la historia clínica, con su nombre y fecha de inicio de vigencia. Permite identificar las combinaciones de medicamentos disponibles para prescripción o administración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVMEZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRVMEZ';

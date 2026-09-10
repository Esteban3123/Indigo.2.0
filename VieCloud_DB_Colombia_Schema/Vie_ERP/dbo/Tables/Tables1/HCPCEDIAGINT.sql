CREATE TABLE [dbo].[HCPCEDIAGINT] (
    [IDDIAGINT]      INT IDENTITY (1, 1) NOT NULL,
    [IDVALDIAG]      INT NOT NULL,
    [CODINTPCE]      INT NOT NULL,
    [IDHCPCECONTROL] INT NOT NULL,
    CONSTRAINT [PK_HCPCEDIAGINT] PRIMARY KEY CLUSTERED ([IDDIAGINT] ASC),
    CONSTRAINT [FK_HCPCEDIAGINT_HCINTERVENPCE] FOREIGN KEY ([CODINTPCE]) REFERENCES [dbo].[HCINTERVENPCE] ([CODINTPCE]),
    CONSTRAINT [FK_HCPCEDIAGINT_HCPCEVALDIAG] FOREIGN KEY ([IDVALDIAG]) REFERENCES [dbo].[HCPCEVALDIAG] ([IDVALDIAG])
);




GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_HCPCEDIAGINT_IDHCPCECONTROL_IDVALDIAG_CODINTPCE]
    ON [dbo].[HCPCEDIAGINT]([IDHCPCECONTROL] ASC, [IDVALDIAG] ASC)
    INCLUDE([CODINTPCE]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de registro de cambios, auditoría y control en el plan de cuidados de enfermería; vinculación a movimientos de cuidado enfermero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGINT', @level2type = N'COLUMN', @level2name = N'IDHCPCECONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de la tabla de los cambios de  cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGINT', @level2type = N'COLUMN', @level2name = N'IDHCPCECONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGINT', @level2type = N'COLUMN', @level2name = N'IDHCPCECONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de intervención asociada al plan de cuidados de enfermería (PCE); referencia a acciones y procedimientos de enfermería ejecutados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGINT', @level2type = N'COLUMN', @level2name = N'CODINTPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la intervención plan cuidado enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGINT', @level2type = N'COLUMN', @level2name = N'CODINTPCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGINT', @level2type = N'COLUMN', @level2name = N'CODINTPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del valor diagnóstico evaluado; vinculación a diagnósticos NANDA, CIE-10 o valorizaciones clínicas en historia de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGINT', @level2type = N'COLUMN', @level2name = N'IDVALDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el id valor diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGINT', @level2type = N'COLUMN', @level2name = N'IDVALDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGINT', @level2type = N'COLUMN', @level2name = N'IDVALDIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la relación diagnóstico-intervención en enfermería; clave primaria que vincula diagnósticos a intervenciones de cuidado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGINT', @level2type = N'COLUMN', @level2name = N'IDDIAGINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el diagnostico de intervencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGINT', @level2type = N'COLUMN', @level2name = N'IDDIAGINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGINT', @level2type = N'COLUMN', @level2name = N'IDDIAGINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los diagnósticos validados con las intervenciones o procedimientos clínicos registrados en la historia clínica del paciente, dentro de un control o seguimiento de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCEDIAGINT';

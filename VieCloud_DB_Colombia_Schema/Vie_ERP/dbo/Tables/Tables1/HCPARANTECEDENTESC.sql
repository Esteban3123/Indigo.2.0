CREATE TABLE [dbo].[HCPARANTECEDENTESC] (
    [ID]            INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [UFUCODIGO]     CHAR (10) NOT NULL,
    [EXIGIRSIEMPRE] BIT       NOT NULL,
    CONSTRAINT [PK_HCPARANTC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPARANTC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que marca si la parametrización de antecedentes clínicos debe ser obligatoria siempre en la unidad funcional; flag de obligatoriedad de captura de antecedentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESC', @level2type = N'COLUMN', @level2name = N'EXIGIRSIEMPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Exigir siempre la parametrización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESC', @level2type = N'COLUMN', @level2name = N'EXIGIRSIEMPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESC', @level2type = N'COLUMN', @level2name = N'EXIGIRSIEMPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador de la unidad funcional (centro de atención, servicio, departamento); clave foránea a INUNIFUNC que agrupa la configuración de antecedentes por área clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial (IDENTITY) de la configuración de antecedentes de cada unidad funcional; clave primaria de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESC', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de antecedentes clínicos del paciente en la historia clínica: define qué unidades funcionales exigen el registro de antecedentes de forma obligatoria en todo momento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESC';

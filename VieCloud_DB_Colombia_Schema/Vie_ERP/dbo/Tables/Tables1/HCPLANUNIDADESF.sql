CREATE TABLE [dbo].[HCPLANUNIDADESF] (
    [ID]             INT         IDENTITY (1, 1) NOT NULL,
    [IDHCPLANCUIENF] VARCHAR (3) NOT NULL,
    [UFUCODIGO]      CHAR (10)   NOT NULL,
    CONSTRAINT [PK_HCPLANUNIDADESF] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Unidad Funcional (UF) asignada al plan de cuidado; identificador único CHAR(10) que referencia el área, servicio o departamento de atención (ej: urgencia, hospitalización, consulta externa, cuidados intensivos). Sinónimos: código de área funcional, código de servicio, código de unidad de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANUNIDADESF', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANUNIDADESF', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANUNIDADESF', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador VARCHAR(3) del Plan de Cuidado de Enfermería; clave foránea que vincula la unidad funcional con el plan específico de atención y procedimientos de enfermería. Sinónimos: ID de plan enfermero, código de plan de cuidados, referencia a plan de atención de enfermería.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANUNIDADESF', @level2type = N'COLUMN', @level2name = N'IDHCPLANCUIENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el ID de  plan de cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANUNIDADESF', @level2type = N'COLUMN', @level2name = N'IDHCPLANCUIENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANUNIDADESF', @level2type = N'COLUMN', @level2name = N'IDHCPLANCUIENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único INT IDENTITY de la relación entre unidad funcional y plan de cuidado; clave primaria autoincrementable que garantiza unicidad del registro en la tabla de asociación (tabla puente/de relación).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANUNIDADESF', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANUNIDADESF', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANUNIDADESF', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los planes de cuidado de enfermería con las unidades funcionales (servicios o áreas) donde se aplican. Permite definir en qué unidades está habilitado cada plan de cuidado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANUNIDADESF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANUNIDADESF';

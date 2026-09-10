CREATE TABLE [dbo].[HCLISTACCXCHVARIABLES] (
    [ID]            INT IDENTITY (1, 1) NOT NULL,
    [IDHCLISTACC]   INT NOT NULL,
    [IDCHVARIABLES] INT NOT NULL,
    [OBLIGATORIO]   BIT NOT NULL,
    [ESTADO]        BIT NOT NULL,
    CONSTRAINT [PK_HCLISTACCXCHVARIABLES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCLISTACCXCHVARIABLES_CHVARIABLES] FOREIGN KEY ([IDCHVARIABLES]) REFERENCES [dbo].[CHVARIABLES] ([ID]),
    CONSTRAINT [FK_HCLISTACCXCHVARIABLES_HCLISTACC] FOREIGN KEY ([IDHCLISTACC]) REFERENCES [dbo].[HCLISTACC] ([CODCONSEC])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de activación de la variable en la lista de chequeo: 1=Activo (habilitado), 0=Inactivo (deshabilitado). Controla si la variable es vigente en la lista de chequeo de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHVARIABLES', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el estado 1 = true = Activo    0 = false = Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHVARIABLES', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHVARIABLES', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de obligatoriedad de la variable en la lista de chequeo: 1=Sí (campo requerido), 0=No (campo opcional). Define si el profesional debe diligenciar esta variable al completar la lista de chequeo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHVARIABLES', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si es obligatorio 1 = si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHVARIABLES', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHVARIABLES', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) de la variable de lista de chequeo referenciada. Vincula a tabla CHVARIABLES para recuperar nombre, tipo y definición de la variable clínica asociada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHVARIABLES', @level2type = N'COLUMN', @level2name = N'IDCHVARIABLES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id variables lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHVARIABLES', @level2type = N'COLUMN', @level2name = N'IDCHVARIABLES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHVARIABLES', @level2type = N'COLUMN', @level2name = N'IDCHVARIABLES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) de la cabecera/encabezado de la lista de chequeo. Vincula a tabla HCLISTACC para identificar a qué lista de chequeo de historia clínica pertenece esta variable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHVARIABLES', @level2type = N'COLUMN', @level2name = N'IDHCLISTACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID cabecera lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHVARIABLES', @level2type = N'COLUMN', @level2name = N'IDHCLISTACC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHVARIABLES', @level2type = N'COLUMN', @level2name = N'IDHCLISTACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) secuencial de la relación entre variable y lista de chequeo. Clave primaria IDENTITY que garantiza unicidad del registro de asignación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Asociación entre listas de chequeo de historia clínica y sus variables o campos evaluables. Indica qué variables pertenecen a cada lista de chequeo, si son obligatorias y si están activas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHVARIABLES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHVARIABLES';

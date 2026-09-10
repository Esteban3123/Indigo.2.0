CREATE TABLE [dbo].[NTXVARIABLES] (
    [ID]                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDNTADMINISTRATIVAS] INT NOT NULL,
    [IDNTVARIABLES]       INT NOT NULL,
    [OBLIGATORIO]         BIT NOT NULL,
    [ESTADO]              BIT NOT NULL,
    CONSTRAINT [PK_NTXVARIABLES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_NTXVARIABLES_NTADMINISTRATIVAS] FOREIGN KEY ([IDNTADMINISTRATIVAS]) REFERENCES [dbo].[NTADMINISTRATIVAS] ([ID]),
    CONSTRAINT [FK_NTXVARIABLES_NTVARIABLES] FOREIGN KEY ([IDNTVARIABLES]) REFERENCES [dbo].[NTVARIABLES] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro (BIT: 0=inactivo/eliminado, 1=activo), controla la vigencia de la relación entre variable administrativa y su configuración en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXVARIABLES', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXVARIABLES', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXVARIABLES', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT: 0=no obligatorio, 1=sí obligatorio), marca si la variable debe ser completada en procesos administrativos, facturación, RIPS o registros de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXVARIABLES', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Es obligatorio (0. no, 1. si)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXVARIABLES', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXVARIABLES', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a tabla NTVARIABLES (FK), identifica el catálogo o tipo de variable (parámetro configurable) asociado a la administración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXVARIABLES', @level2type = N'COLUMN', @level2name = N'IDNTVARIABLES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con la tabla NTVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXVARIABLES', @level2type = N'COLUMN', @level2name = N'IDNTVARIABLES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXVARIABLES', @level2type = N'COLUMN', @level2name = N'IDNTVARIABLES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a tabla NTADMINISTRATIVAS (FK), vincula la variable con una unidad funcional, centro de atención o entidad administrativa específica del ERP/EHR.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXVARIABLES', @level2type = N'COLUMN', @level2name = N'IDNTADMINISTRATIVAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con la tabla NTADMINISTRATIVAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXVARIABLES', @level2type = N'COLUMN', @level2name = N'IDNTADMINISTRATIVAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXVARIABLES', @level2type = N'COLUMN', @level2name = N'IDNTADMINISTRATIVAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo único (INT IDENTITY), clave primaria de la tabla de relación entre variables administrativas y catálogos de variables.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre notas o registros administrativos y las variables o campos configurables que les aplican, indicando si cada variable es obligatoria y si está activa. Permite definir qué datos deben capturarse en cada tipo de nota administrativa del sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXVARIABLES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXVARIABLES';

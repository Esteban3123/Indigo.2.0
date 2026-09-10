CREATE TABLE [dbo].[PRHCXEXAVARIABLES] (
    [ID]             INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDMODELOHC]     INT NOT NULL,
    [IDEXAVARIABLES] INT NOT NULL,
    [OBLIGATORIO]    BIT NOT NULL,
    [ESTADO]         BIT NOT NULL,
    CONSTRAINT [PK_PRHCXEXAVARIABLES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRHCXEXAVARIABLES_ANTVARIABLES] FOREIGN KEY ([IDEXAVARIABLES]) REFERENCES [dbo].[EXAVARIABLES] ([ID]),
    CONSTRAINT [FK_PRHCXEXAVARIABLES_PRMODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID])
);


GO
ALTER TABLE [dbo].[PRHCXEXAVARIABLES] NOCHECK CONSTRAINT [FK_PRHCXEXAVARIABLES_PRMODELOHC];




GO



GO
ALTER TABLE [dbo].[PRHCXEXAVARIABLES] NOCHECK CONSTRAINT [FK_PRHCXEXAVARIABLES_PRMODELOHC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo (BIT): 1 = Activo/Verdadero, 0 = Inactivo/Falso. Indica si la variable está habilitada para uso en el modelo de HC. Buscar por: estado variable, disponible, activo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAVARIABLES', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Estado 1 = true   0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAVARIABLES', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAVARIABLES', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de obligatoriedad (BIT): 1 = Sí (requerido en HC), 0 = No (opcional). Define si la variable debe completarse obligatoriamente en el modelo de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAVARIABLES', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Obligatorio  1 =  Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAVARIABLES', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAVARIABLES', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de variables del examen físico (FK → EXAVARIABLES). Referencia los campos/parámetros de examen (ej: presión arterial, temperatura, frecuencia cardíaca). Buscar por: variable examen, parámetro vital, campo clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAVARIABLES', @level2type = N'COLUMN', @level2name = N'IDEXAVARIABLES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Id de variables examen fisico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAVARIABLES', @level2type = N'COLUMN', @level2name = N'IDEXAVARIABLES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAVARIABLES', @level2type = N'COLUMN', @level2name = N'IDEXAVARIABLES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del modelo de historia clínica (FK → PRMODELOHC). Referencia la plantilla o estructura de HC a la que pertenece la variable de examen. Buscar por: modelo HC, plantilla clínica, estructura de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAVARIABLES', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id modelo de historia clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAVARIABLES', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAVARIABLES', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único, clave primaria (INT IDENTITY). Consecutivo secuencial de la tabla PRHCXEXAVARIABLES que registra la relación entre modelos de historia clínica y variables de examen físico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre modelos de historia clínica y las variables de examen o evaluación que les aplican, indicando cuáles campos son obligatorios y si están activos. Permite configurar qué variables deben diligenciarse en cada formulario clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAVARIABLES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXEXAVARIABLES';

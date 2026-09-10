CREATE TABLE [dbo].[PRHCEXAMENFISICO] (
    [ID]           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDMODELOHC]   INT          NOT NULL,
    [CODIGO]       INT          NOT NULL,
    [SIGVITDESCRI] VARCHAR (50) NULL,
    [SIGVITOBLIG]  BIT          NULL,
    CONSTRAINT [PK_PRHCEXAMENFISICO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRHCEXAMENFISICO_PRMODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID])
);


GO
ALTER TABLE [dbo].[PRHCEXAMENFISICO] NOCHECK CONSTRAINT [FK_PRHCEXAMENFISICO_PRMODELOHC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obligatoriedad del signo vital (BIT: 0=opcional, 1=requerido). Indica si la medición del signo vital es obligatoria en el examen físico del modelo de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXAMENFISICO', @level2type = N'COLUMN', @level2name = N'SIGVITOBLIG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obligatoriedad del signo vital', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXAMENFISICO', @level2type = N'COLUMN', @level2name = N'SIGVITOBLIG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXAMENFISICO', @level2type = N'COLUMN', @level2name = N'SIGVITOBLIG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del signo vital (VARCHAR 50). Nombre o etiqueta del parámetro vital: presión arterial, frecuencia cardíaca, temperatura, saturación de oxígeno, frecuencia respiratoria, peso, talla, IMC, glucometría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXAMENFISICO', @level2type = N'COLUMN', @level2name = N'SIGVITDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del signo vital', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXAMENFISICO', @level2type = N'COLUMN', @level2name = N'SIGVITDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXAMENFISICO', @level2type = N'COLUMN', @level2name = N'SIGVITDESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del signo vital (INT). Identificador numérico único del signo vital dentro del catálogo de parámetros vitales del sistema para búsqueda y referencia cruzada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXAMENFISICO', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del signo vital', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXAMENFISICO', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXAMENFISICO', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del modelo de historia clínica parametrizada (INT, FK → PRMODELOHC.ID). Referencia a la plantilla o formulario HC a la cual pertenece este signo vital en el examen físico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXAMENFISICO', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de modelo de HC parametrizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXAMENFISICO', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXAMENFISICO', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK IDENTITY). Clave primaria autoincremental del registro de signo vital en examen físico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXAMENFISICO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXAMENFISICO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXAMENFISICO', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de los signos vitales y exámenes físicos asociados a los modelos de historia clínica, incluyendo los parámetros obligatorios que deben ser diligenciados durante la atención del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXAMENFISICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXAMENFISICO';

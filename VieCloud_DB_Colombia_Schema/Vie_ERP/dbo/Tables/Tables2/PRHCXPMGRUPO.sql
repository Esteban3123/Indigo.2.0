CREATE TABLE [dbo].[PRHCXPMGRUPO] (
    [ID]            INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDMODELOHC]    INT NOT NULL,
    [CODPLANMANEJO] INT NOT NULL,
    CONSTRAINT [PK_PRHCXPMGRUPO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRHCXPMGRUPO_PRMODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID])
);


GO
ALTER TABLE [dbo].[PRHCXPMGRUPO] NOCHECK CONSTRAINT [FK_PRHCXPMGRUPO_PRMODELOHC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de plan/componente de manejo clínico: Dieta (1), Medicamentos (2), Insumos/Dispositivos (3), Laboratorios (4), Patologías (5), Imágenes de diagnóstico (6), Procedimientos quirúrgicos (7), Procedimientos no quirúrgicos (8), Interconsultas (9), Recomendaciones (10), Hemocomponentes (11), PAD (12), Formulación de lentes (13). INT, clave de clasificación en plan de manejo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXPMGRUPO', @level2type = N'COLUMN', @level2name = N'CODPLANMANEJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'    Dieta = 1          Medicamentos = 2          InsumosDispositivos = 3          Laboratorios = 4          Patologías = 5          ImágenesDx = 6          ProcedimientosQx = 7          ProcedimientosNoQx = 8          Interconsultas = 9          Recomendaciones = 10          Hemocomponentes = 11          PAD = 12          Formulación de lentes = 13', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXPMGRUPO', @level2type = N'COLUMN', @level2name = N'CODPLANMANEJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXPMGRUPO', @level2type = N'COLUMN', @level2name = N'CODPLANMANEJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del modelo de historia clínica asociado a este grupo de plan de manejo. Referencia a PRMODELOHC (FK). INT, define la estructura y plantilla de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXPMGRUPO', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el id modelo de historia clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXPMGRUPO', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXPMGRUPO', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y consecutivo (identity) de la asociación entre modelo de historia clínica y tipo de plan de manejo. INT, clave primaria clustered.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXPMGRUPO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXPMGRUPO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXPMGRUPO', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agrupa los planes de manejo asociados a cada modelo de historia clínica. Permite organizar y relacionar los diferentes planes de manejo dentro de las plantillas o modelos de HC utilizados en la atención al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXPMGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCXPMGRUPO';

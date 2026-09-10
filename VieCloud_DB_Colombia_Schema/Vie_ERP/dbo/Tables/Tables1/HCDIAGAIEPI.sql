CREATE TABLE [dbo].[HCDIAGAIEPI] (
    [ID]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCAIEPIID]       INT           NOT NULL,
    [DIAGNOSTICO]     VARCHAR (200) NULL,
    [COLOR]           VARCHAR (100) NULL,
    [RECOMENDACIONES] VARCHAR (800) NULL,
    CONSTRAINT [PK_HCDIAGAIEPI] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCDIAGAIEPI_HCAIEPI] FOREIGN KEY ([HCAIEPIID]) REFERENCES [dbo].[HCAIEPI] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recomendaciones clínicas, orientaciones terapéuticas y acciones sugeridas derivadas del diagnóstico registrado en la historia clínica. Texto descriptivo (VARCHAR 800) que incluye indicaciones de tratamiento, seguimiento, derivaciones o intervenciones recomendadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGAIEPI', @level2type = N'COLUMN', @level2name = N'RECOMENDACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recomendaciones relacionadas al diagnotico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGAIEPI', @level2type = N'COLUMN', @level2name = N'RECOMENDACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGAIEPI', @level2type = N'COLUMN', @level2name = N'RECOMENDACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de color o etiqueta cromática asociada al diagnóstico para clasificación visual, priorización o categorización clínica en la historia clínica. Identificador visual (VARCHAR 100) usado en interfaces de usuario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGAIEPI', @level2type = N'COLUMN', @level2name = N'COLOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Color correspondiente al diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGAIEPI', @level2type = N'COLUMN', @level2name = N'COLOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGAIEPI', @level2type = N'COLUMN', @level2name = N'COLOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre, descripción o denominación médica del diagnóstico principal registrado en la historia clínica. Denominación clínica (VARCHAR 200) que identifica la condición, enfermedad o hallazgo clínico documentado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGAIEPI', @level2type = N'COLUMN', @level2name = N'DIAGNOSTICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGAIEPI', @level2type = N'COLUMN', @level2name = N'DIAGNOSTICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGAIEPI', @level2type = N'COLUMN', @level2name = N'DIAGNOSTICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) que vincula este registro diagnóstico con su historia clínica padre en la tabla HCAIEPI. Clave foránea FK que relaciona diagnósticos secundarios o detalles con el evento/episodio clínico principal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGAIEPI', @level2type = N'COLUMN', @level2name = N'HCAIEPIID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que se relaciona con el ID de la tabla HCAIEPI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGAIEPI', @level2type = N'COLUMN', @level2name = N'HCAIEPIID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGAIEPI', @level2type = N'COLUMN', @level2name = N'HCAIEPIID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y autonumérico (INT IDENTITY) que genera automáticamente la clave primaria de este registro diagnóstico en la tabla HCDIAGAIEPI. Número secuencial de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGAIEPI', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGAIEPI', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGAIEPI', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnósticos asociados a episodios de atención en historia clínica de IA/EPI, incluyendo el diagnóstico registrado, una clasificación por color (semáforo o prioridad) y las recomendaciones clínicas correspondientes a cada diagnóstico del episodio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGAIEPI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDIAGAIEPI';

CREATE TABLE [dbo].[HCRADDOSISTIPOS] (
    [ID]             INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADDOSIS]   INT NOT NULL,
    [IDHCRADMAESTIP] INT NOT NULL,
    [TIPO]           INT NOT NULL,
    CONSTRAINT [PK_HCRADDOSISTIPOS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADDOSISTIPOS_HCRADDOSIS] FOREIGN KEY ([IDHCRADDOSIS]) REFERENCES [dbo].[HCRADDOSIS] ([ID]),
    CONSTRAINT [FK_HCRADDOSISTIPOS_HCRADMAESTIP] FOREIGN KEY ([IDHCRADMAESTIP]) REFERENCES [dbo].[HCRADMAESTIP] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de dosis: 1=Factores Asociados al Tratamiento, 2=Sistema. Indica la categoría o naturaleza de la dosis administrada en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOS', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 -  Factores Asociados al Tratamiento   2 - Sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOS', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOS', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de relación con tabla maestra HCRADMAESTIP. Referencia el tipo maestro de dosis o clasificación de medicamentos/procedimientos vinculados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOS', @level2type = N'COLUMN', @level2name = N'IDHCRADMAESTIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla maestro de: HCRADMAESTIP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOS', @level2type = N'COLUMN', @level2name = N'IDHCRADMAESTIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOS', @level2type = N'COLUMN', @level2name = N'IDHCRADMAESTIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de relación con tabla HCRADDOSIS. Vincula el registro de dosis específica del paciente en la historia clínica a sus tipos asociados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOS', @level2type = N'COLUMN', @level2name = N'IDHCRADDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla HCRADDOSIS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOS', @level2type = N'COLUMN', @level2name = N'IDHCRADDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOS', @level2type = N'COLUMN', @level2name = N'IDHCRADDOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT Identity). Clave primaria consecutiva de la tabla de tipos de dosis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de tipos asociados a las dosis de medicamentos o tratamientos en la historia clínica. Relaciona cada dosis con su tipo o categoría según el maestro de tipos definido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADDOSISTIPOS';

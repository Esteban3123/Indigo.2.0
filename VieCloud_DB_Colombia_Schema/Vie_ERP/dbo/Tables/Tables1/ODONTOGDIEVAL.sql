CREATE TABLE [dbo].[ODONTOGDIEVAL] (
    [ID]           INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCONTROLVAL] INT     NOT NULL,
    [DIENTE]       TINYINT NOT NULL,
    CONSTRAINT [PK_ODONTOGDIEVAL__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOGDIEVAL_ODONTOGCTRLVAL] FOREIGN KEY ([IDCONTROLVAL]) REFERENCES [dbo].[ODONTOGCTRLVAL] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o código del diente evaluado en odontología (1-32, nomenclatura dental FDI). Identifica la pieza dental específica registrada en el control odontológico. Tipo: TINYINT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIEVAL', @level2type = N'COLUMN', @level2name = N'DIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el diente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIEVAL', @level2type = N'COLUMN', @level2name = N'DIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIEVAL', @level2type = N'COLUMN', @level2name = N'DIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del control odontológico padre (clave foránea a ODONTOGCTRLVAL.ID). Vincula cada diente evaluado al registro de evaluación odontológica general del paciente. Tipo: INT, FK.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIEVAL', @level2type = N'COLUMN', @level2name = N'IDCONTROLVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con la tabla ODONTOGCTRLVAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIEVAL', @level2type = N'COLUMN', @level2name = N'IDCONTROLVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIEVAL', @level2type = N'COLUMN', @level2name = N'IDCONTROLVAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único consecutivo del registro de diente evaluado. Clave primaria de la tabla ODONTOGDIEVAL. Tipo: INT IDENTITY(1,1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIEVAL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIEVAL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIEVAL', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de los dientes evaluados en cada control de valoración odontológica. Asocia cada diente revisado con su respectivo control de evolución dental del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIEVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIEVAL';

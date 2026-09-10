CREATE TABLE [dbo].[ODONTOGDIETRA] (
    [ID]           INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCTRLDIETRA] INT     NOT NULL,
    [DIENTE]       TINYINT NOT NULL,
    CONSTRAINT [PK_ODONTOGDIETRA__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOGDIETRA_ODONTOGCTRLTRA] FOREIGN KEY ([IDCTRLDIETRA]) REFERENCES [dbo].[ODONTOGCTRLTRA] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de control odontológico dietético (FK → ODONTOGCTRLTRA.ID). Vincula cada diente evaluado con su registro de control dietético asociado en odontología. Tipo: INT. Dominio: referencias a controles odontológicos, dieta, seguimiento dental.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIETRA', @level2type = N'COLUMN', @level2name = N'IDCTRLDIETRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con la tabla ODONTOGDIETRA, ODONTOGCTRLTRA, ODONTOGDIETRA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIETRA', @level2type = N'COLUMN', @level2name = N'IDCTRLDIETRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIETRA', @level2type = N'COLUMN', @level2name = N'IDCTRLDIETRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de los dientes asociados a una dietra (dieta/restricción odontológica) dentro del control odontológico. Vincula cada diente específico con su registro de control dietra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIETRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIETRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro diente-dietra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIETRA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIETRA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del diente según la nomenclatura odontológica (pieza dental).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIETRA', @level2type = N'COLUMN', @level2name = N'DIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIETRA', @level2type = N'COLUMN', @level2name = N'DIENTE';

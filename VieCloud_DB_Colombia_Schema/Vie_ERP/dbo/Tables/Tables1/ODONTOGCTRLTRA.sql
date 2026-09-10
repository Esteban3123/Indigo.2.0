CREATE TABLE [dbo].[ODONTOGCTRLTRA] (
    [ID]        INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCONTROL] INT NOT NULL,
    CONSTRAINT [PK_ODONTOGCTRLTRA__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOGCTRLTRA_ODONTOGCTRLTRA] FOREIGN KEY ([IDCONTROL]) REFERENCES [dbo].[ODONTOGCTR] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del control odontológico/tratamiento dental asociado. Referencia a la tabla ODONTOGCTR que contiene los registros maestros de controles y atenciones odontológicas del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLTRA', @level2type = N'COLUMN', @level2name = N'IDCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el ID control', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLTRA', @level2type = N'COLUMN', @level2name = N'IDCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLTRA', @level2type = N'COLUMN', @level2name = N'IDCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) y consecutivo secuencial de la tabla de detalles o transacciones de control odontológico. Generado automáticamente por identidad (IDENTITY).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLTRA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLTRA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLTRA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de control de tratamientos odontológicos. Vincula cada entrada de seguimiento o control con el tratamiento odontológico correspondiente del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGCTRLTRA';

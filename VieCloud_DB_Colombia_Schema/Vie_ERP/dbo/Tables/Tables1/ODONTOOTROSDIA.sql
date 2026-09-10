CREATE TABLE [dbo].[ODONTOOTROSDIA] (
    [ID]            INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDODONTOGRAMA] INT            NOT NULL,
    [CODDIAGNO]     CHAR (4)       NOT NULL,
    [OBSDIAGNO]     NVARCHAR (250) NULL,
    CONSTRAINT [PK_ODONTOOTROSDIA__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOOTROSDIA_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_ODONTOOTROSDIA_ODONTOGCTR] FOREIGN KEY ([IDODONTOGRAMA]) REFERENCES [dbo].[ODONTOGCTR] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación, nota o comentario clínico general del diagnóstico odontológico registrado en el odontograma; texto descriptivo de hallazgos, condiciones dentales o recomendaciones (NVARCHAR 250, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSDIA', @level2type = N'COLUMN', @level2name = N'OBSDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación general del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSDIA', @level2type = N'COLUMN', @level2name = N'OBSDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSDIA', @level2type = N'COLUMN', @level2name = N'OBSDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico odontológico (CHAR 4); identificador de la condición dental, patología o hallazgo clínico; referencia a tabla INDIAGNOS para obtener descripción completa del diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSDIA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el código del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSDIA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSDIA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del odontograma o registro dental asociado; clave foránea que vincula al odontograma principal (tabla ODONTOGCTR) para la atención odontológica del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSDIA', @level2type = N'COLUMN', @level2name = N'IDODONTOGRAMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id de odontograma ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSDIA', @level2type = N'COLUMN', @level2name = N'IDODONTOGRAMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSDIA', @level2type = N'COLUMN', @level2name = N'IDODONTOGRAMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) y clave primaria de la tabla; número secuencial o consecutivo autoincrementable que identifica cada registro de diagnóstico odontológico adicional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSDIA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSDIA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSDIA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnósticos adicionales u otros hallazgos registrados en un odontograma clínico. Complementa el odontograma principal con códigos diagnósticos (CIE-10 u odontológicos) y sus observaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSDIA';

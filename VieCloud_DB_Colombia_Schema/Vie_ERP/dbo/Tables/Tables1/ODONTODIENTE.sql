CREATE TABLE [dbo].[ODONTODIENTE] (
    [ID]              INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDODONTOCONTROL] INT         NOT NULL,
    [DIENTE]          TINYINT     NOT NULL,
    [TIPOODONTOGRAMA] VARCHAR (1) NOT NULL,
    CONSTRAINT [PK_ODONTODIENTE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTODIENTE_ODONTOCONTROL] FOREIGN KEY ([IDODONTOCONTROL]) REFERENCES [dbo].[ODONTOCONTROL] ([ID])
);




GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ODONTODIENTE]
    ON [dbo].[ODONTODIENTE]([IDODONTOCONTROL] ASC, [DIENTE] ASC, [TIPOODONTOGRAMA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de odontograma: V=Valoración/evaluación dental, T=Tratamiento odontológico. VARCHAR(1). Clasifica si el registro corresponde a evaluación clínica o procedimiento terapéutico en la consulta odontológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTE', @level2type = N'COLUMN', @level2name = N'TIPOODONTOGRAMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'V - Valoracion  T - Tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTE', @level2type = N'COLUMN', @level2name = N'TIPOODONTOGRAMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTE', @level2type = N'COLUMN', @level2name = N'TIPOODONTOGRAMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número identificador del diente en el odontograma (1-32). TINYINT. Referencia a la pieza dental específica según nomenclatura dental internacional para registro de hallazgos y tratamientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTE', @level2type = N'COLUMN', @level2name = N'DIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del diente del odontograma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTE', @level2type = N'COLUMN', @level2name = N'DIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTE', @level2type = N'COLUMN', @level2name = N'DIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la consulta/control odontológico relacionado. INT, clave foránea FK a ODONTOCONTROL.ID. Vincula cada diente evaluado o tratado a su atención odontológica madre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTE', @level2type = N'COLUMN', @level2name = N'IDODONTOCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla ODONTOCONTROL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTE', @level2type = N'COLUMN', @level2name = N'IDODONTOCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTE', @level2type = N'COLUMN', @level2name = N'IDODONTOCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de diente en odontograma. INT IDENTITY(1,1), clave primaria. Cada evaluación o tratamiento de una pieza dental genera un registro único.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTE', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de los dientes evaluados en cada control odontológico del paciente, indicando el número de diente y el tipo de odontograma asociado (inicial, periódico, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTE';

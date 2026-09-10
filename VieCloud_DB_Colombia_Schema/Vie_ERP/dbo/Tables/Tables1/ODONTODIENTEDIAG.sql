CREATE TABLE [dbo].[ODONTODIENTEDIAG] (
    [ID]             INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDODONTODIENTE] INT NOT NULL,
    [CONSECDIA]      INT NOT NULL,
    [TIPO]           INT NOT NULL,
    CONSTRAINT [PK_ODONTODIENTEDIAG] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTODIENTEDIAG_ODONTODIENTE] FOREIGN KEY ([IDODONTODIENTE]) REFERENCES [dbo].[ODONTODIENTE] ([ID]),
    CONSTRAINT [FK_ODONTODIENTEDIAG_ODOPARDIA] FOREIGN KEY ([CONSECDIA]) REFERENCES [dbo].[ODOPARDIA] ([CONSECDIA])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de superficie dental donde se ubica el diagnóstico odontológico: 1=Nivel diente completo, 2=Vestibular superior, 3=Oclusal/centro, 4=Palatino superior, 5=Distal izquierdo, 6=Distal derecho, 7=Mesial derecho, 8=Mesial izquierdo, 9=Vestibular inferior, 10=Lingual inferior. Define localización exacta de hallazgo/diagnóstico en pieza dental.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTEDIAG', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se establece el diagnostico en que superficie esta ubicado,  o si esta a nivel de diente.    1: Nivel Diente (Imagen)  2: Vestibular Arriba (Si es un diente SUPERIOR)   3: Oclusal (CENTRO)  4: Palatino (Si es un diente SUPERIOR)   5: Distal Izquierda (Si es un diente IZQUIERDO)  6: Distal Derecha (Si es un diente DERECHO)  7: Mesial Derecha (Si es un diente DERECHO)  8: Mesial Izquierda (Si es un diente IZQUIERDO)  9: Vestibular Abajo (Si es un diente INFERIOR)   10: Lingual (Si es un diente INFERIOR)   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTEDIAG', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTEDIAG', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia de clave foránea a tabla ODOPARDIA (consecutivo diagnóstico odontológico). Identifica el diagnóstico dental asociado: caries, fractura, inflamación, movilidad, retracción gingival u otro hallazgo odontológico registrado en la evaluación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTEDIAG', @level2type = N'COLUMN', @level2name = N'CONSECDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tabla que contiene los diagnosticos  (ODOPARDIA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTEDIAG', @level2type = N'COLUMN', @level2name = N'CONSECDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTEDIAG', @level2type = N'COLUMN', @level2name = N'CONSECDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de clave foránea a tabla ODONTODIENTE. Referencia la pieza dental específica (diente del 1 al 32) bajo evaluación/seguimiento odontológico en el paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTEDIAG', @level2type = N'COLUMN', @level2name = N'IDODONTODIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla de control de diente (ODONTODIENTE)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTEDIAG', @level2type = N'COLUMN', @level2name = N'IDODONTODIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTEDIAG', @level2type = N'COLUMN', @level2name = N'IDODONTODIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, auto-incremental) de la relación diagnóstico-diente en sistema odontológico. Clave primaria de registro individual en tabla de diagnósticos dentales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTEDIAG', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTEDIAG', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTEDIAG', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de diagnósticos asociados a cada diente en la historia odontológica del paciente. Relaciona cada pieza dental con sus diagnósticos o hallazgos clínicos bucales, permitiendo el seguimiento del estado de cada diente en el odontograma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTEDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTEDIAG';

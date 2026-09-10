CREATE TABLE [dbo].[ODONTOGTRATRA] (
    [ID]          INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCTRDIETRA] INT         NOT NULL,
    [CONSECTRA]   INT         NOT NULL,
    [TIPO]        VARCHAR (1) NOT NULL,
    CONSTRAINT [PK_ODONTOGTRATRA__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOGTRATRA_ODONTOGDIETRA] FOREIGN KEY ([IDCTRDIETRA]) REFERENCES [dbo].[ODONTOGDIETRA] ([ID]),
    CONSTRAINT [FK_ODONTOGTRATRA_ODOPARTRA] FOREIGN KEY ([CONSECTRA]) REFERENCES [dbo].[ODOPARTRA] ([CONSECTRA])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación odontológica de la superficie dental o ubicación del tratamiento: 1=Nivel Diente (Imagen), 2=Vestibular Superior, 3=Oclusal/Centro, 4=Palatino Superior, 5=Distal Izquierda, 6=Distal Derecha, 7=Mesial Derecha, 8=Mesial Izquierda, 9=Vestibular Inferior, 10=Lingual Inferior. Determina zona anatómica donde se aplica procedimiento odontológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRATRA', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se establece el diagnostico en que superficie esta ubicado,  o si esta a nivel de diente.    1: Nivel Diente (Imagen)  2: Vestibular Arriba (Si es un diente SUPERIOR)   3: Oclusal (CENTRO)  4: Palatino (Si es un diente SUPERIOR)   5: Distal Izquierda (Si es un diente IZQUIERDO)  6: Distal Derecha (Si es un diente DERECHO)  7: Mesial Derecha (Si es un diente DERECHO)  8: Mesial Izquierda (Si es un diente IZQUIERDO)  9: Vestibular Abajo (Si es un diente INFERIOR)   10: Lingual (Si es un diente INFERIOR)     ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRATRA', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRATRA', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonúmerico/consecutivo que identifica el tratamiento odontológico específico. Referencia a tabla ODOPARTRA (parámetro de tratamiento). Clave foránea que vincula este registro con el catálogo de tratamientos disponibles.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRATRA', @level2type = N'COLUMN', @level2name = N'CONSECTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Autonumerico de los tratamientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRATRA', @level2type = N'COLUMN', @level2name = N'CONSECTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRATRA', @level2type = N'COLUMN', @level2name = N'CONSECTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del diente tratado. Referencia a tabla ODONTOGDIETRA (registro odontológico de diente). Clave foránea que vincula este tratamiento con el diente específico dentro de la ficha odontológica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRATRA', @level2type = N'COLUMN', @level2name = N'IDCTRDIETRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con la tabla ODONTOGDIETRA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRATRA', @level2type = N'COLUMN', @level2name = N'IDCTRDIETRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRATRA', @level2type = N'COLUMN', @level2name = N'IDCTRDIETRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) y clave primaria de la tabla ODONTOGTRATRA. Consecutivo que registra cada relación entre superficie dental y tratamiento odontológico en la historia clínica odontológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRATRA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRATRA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRATRA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de los tipos de tratamientos dentales asociados a cada control o diagnóstico odontológico. Guarda el detalle de los tratamientos realizados o planificados dentro de un plan de tratamiento odontológico, identificando el tipo de intervención para cada pieza o área tratada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRATRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRATRA';

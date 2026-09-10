CREATE TABLE [dbo].[ODONTODIENTETRATA] (
    [ID]             INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDODONTODIENTE] INT       NOT NULL,
    [CONSECTRA]      INT       NOT NULL,
    [TIPO]           INT       NOT NULL,
    [CODSERIPS]      CHAR (20) CONSTRAINT [DF_ODONTODIENTETRATA_CODSERIPS] DEFAULT ((232101)) NULL,
    [CANTIDAD]       INT       NULL,
    CONSTRAINT [PK_ODONTODIENTETRATA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTODIENTETRATA_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_ODONTODIENTETRATA_ODONTODIENTE] FOREIGN KEY ([IDODONTODIENTE]) REFERENCES [dbo].[ODONTODIENTE] ([ID]),
    CONSTRAINT [FK_ODONTODIENTETRATA_ODOPARTRA] FOREIGN KEY ([CONSECTRA]) REFERENCES [dbo].[ODOPARTRA] ([CONSECTRA])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades o sesiones del tratamiento odontológico enviado/prestado. Número entero que cuantifica repeticiones o dosis del procedimiento dental.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTETRATA', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Me especifica la cantidad de tratamiento que se envio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTETRATA', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTETRATA', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS (procedimiento odontológico) para facturación en RIPS. Vinculado desde odontograma; permite nulos si el tratamiento carece de CUPS parametrizado. Referencia a tabla INCUPSIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTETRATA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CUPS con la cual se debe facturar, este cups se llena desde el registro del Odontograma.  Nota: Permite nulos ya que es posible que el tratamiento No tenga parametrizado algun CUPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTETRATA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTETRATA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ubicación anatómica de la lesión/tratamiento en la superficie dental: 1=Diente completo, 2=Vestibular superior, 3=Oclusal, 4=Palatino, 5=Distal izquierda, 6=Distal derecha, 7=Mesial derecha, 8=Mesial izquierda, 9=Vestibular inferior, 10=Lingual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTETRATA', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se establece el diagnostico en que superficie esta ubicado,  o si esta a nivel de diente.    1: Nivel Diente (Imagen)  2: Vestibular Arriba (Si es un diente SUPERIOR)   3: Oclusal (CENTRO)  4: Palatino (Si es un diente SUPERIOR)   5: Distal Izquierda (Si es un diente IZQUIERDO)  6: Distal Derecha (Si es un diente DERECHO)  7: Mesial Derecha (Si es un diente DERECHO)  8: Mesial Izquierda (Si es un diente IZQUIERDO)  9: Vestibular Abajo (Si es un diente INFERIOR)   10: Lingual (Si es un diente INFERIOR)     ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTETRATA', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTETRATA', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo del tratamiento odontológico parametrizado. Clave foránea a tabla ODOPARTRA que contiene definición y configuración del procedimiento dental.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTETRATA', @level2type = N'COLUMN', @level2name = N'CONSECTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Consecutivo de la tabla de parametros del tratamiento (ODOPARTRA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTETRATA', @level2type = N'COLUMN', @level2name = N'CONSECTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTETRATA', @level2type = N'COLUMN', @level2name = N'CONSECTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del diente registrado en control odontológico. Clave foránea a tabla ODONTODIENTE que vincula este tratamiento al diente específico del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTETRATA', @level2type = N'COLUMN', @level2name = N'IDODONTODIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla de control de diente (ODONTODIENTE)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTETRATA', @level2type = N'COLUMN', @level2name = N'IDODONTODIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTETRATA', @level2type = N'COLUMN', @level2name = N'IDODONTODIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) del registro de tratamiento odontológico. Clave primaria de la tabla ODONTODIENTETRATA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTETRATA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTETRATA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTETRATA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los tratamientos odontológicos asociados a cada diente del paciente, incluyendo el tipo de tratamiento, el procedimiento (código CUPS) y la cantidad aplicada. Permite llevar el detalle clínico del plan de tratamiento dental por diente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTETRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTETRATA';

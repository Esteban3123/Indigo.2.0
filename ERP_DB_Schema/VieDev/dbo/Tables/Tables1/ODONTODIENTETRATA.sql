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
ALTER TABLE [dbo].[ODONTODIENTETRATA] NOCHECK CONSTRAINT [FK_ODONTODIENTETRATA_INCUPSIPS];


GO
ALTER TABLE [dbo].[ODONTODIENTETRATA] NOCHECK CONSTRAINT [FK_ODONTODIENTETRATA_ODONTODIENTE];


GO
ALTER TABLE [dbo].[ODONTODIENTETRATA] NOCHECK CONSTRAINT [FK_ODONTODIENTETRATA_ODOPARTRA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CUPS con la cual se debe facturar, este cups se llena desde el registro del Odontograma.  Nota: Permite nulos ya que es posible que el tratamiento No tenga parametrizado algun CUPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTETRATA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se establece el diagnostico en que superficie esta ubicado,  o si esta a nivel de diente.    1: Nivel Diente (Imagen)  2: Vestibular Arriba (Si es un diente SUPERIOR)   3: Oclusal (CENTRO)  4: Palatino (Si es un diente SUPERIOR)   5: Distal Izquierda (Si es un diente IZQUIERDO)  6: Distal Derecha (Si es un diente DERECHO)  7: Mesial Derecha (Si es un diente DERECHO)  8: Mesial Izquierda (Si es un diente IZQUIERDO)  9: Vestibular Abajo (Si es un diente INFERIOR)   10: Lingual (Si es un diente INFERIOR)     ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTETRATA', @level2type = N'COLUMN', @level2name = N'TIPO';


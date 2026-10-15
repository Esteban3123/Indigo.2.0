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
ALTER TABLE [dbo].[ODONTODIENTEDIAG] NOCHECK CONSTRAINT [FK_ODONTODIENTEDIAG_ODONTODIENTE];


GO
ALTER TABLE [dbo].[ODONTODIENTEDIAG] NOCHECK CONSTRAINT [FK_ODONTODIENTEDIAG_ODOPARDIA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se establece el diagnostico en que superficie esta ubicado,  o si esta a nivel de diente.    1: Nivel Diente (Imagen)  2: Vestibular Arriba (Si es un diente SUPERIOR)   3: Oclusal (CENTRO)  4: Palatino (Si es un diente SUPERIOR)   5: Distal Izquierda (Si es un diente IZQUIERDO)  6: Distal Derecha (Si es un diente DERECHO)  7: Mesial Derecha (Si es un diente DERECHO)  8: Mesial Izquierda (Si es un diente IZQUIERDO)  9: Vestibular Abajo (Si es un diente INFERIOR)   10: Lingual (Si es un diente INFERIOR)   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTEDIAG', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tabla que contiene los diagnosticos  (ODOPARDIA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTEDIAG', @level2type = N'COLUMN', @level2name = N'CONSECDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla de control de diente (ODONTODIENTE)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTEDIAG', @level2type = N'COLUMN', @level2name = N'IDODONTODIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTODIENTEDIAG', @level2type = N'COLUMN', @level2name = N'ID';


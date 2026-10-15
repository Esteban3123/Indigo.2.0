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
ALTER TABLE [dbo].[ODONTOGTRATRA] NOCHECK CONSTRAINT [FK_ODONTOGTRATRA_ODONTOGDIETRA];


GO
ALTER TABLE [dbo].[ODONTOGTRATRA] NOCHECK CONSTRAINT [FK_ODONTOGTRATRA_ODOPARTRA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se establece el diagnostico en que superficie esta ubicado,  o si esta a nivel de diente.    1: Nivel Diente (Imagen)  2: Vestibular Arriba (Si es un diente SUPERIOR)   3: Oclusal (CENTRO)  4: Palatino (Si es un diente SUPERIOR)   5: Distal Izquierda (Si es un diente IZQUIERDO)  6: Distal Derecha (Si es un diente DERECHO)  7: Mesial Derecha (Si es un diente DERECHO)  8: Mesial Izquierda (Si es un diente IZQUIERDO)  9: Vestibular Abajo (Si es un diente INFERIOR)   10: Lingual (Si es un diente INFERIOR)     ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRATRA', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Autonumerico de los tratamientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRATRA', @level2type = N'COLUMN', @level2name = N'CONSECTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relación con la tabla ODONTOGDIETRA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRATRA', @level2type = N'COLUMN', @level2name = N'IDCTRDIETRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGTRATRA', @level2type = N'COLUMN', @level2name = N'ID';


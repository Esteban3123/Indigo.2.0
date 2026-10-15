CREATE TABLE [dbo].[HCTIPENCD] (
    [AUTO]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AUTOHCTIPENCC] INT           NOT NULL,
    [DESPREENC]     VARCHAR (500) NOT NULL,
    [PREREQENC]     BIT           NOT NULL,
    [TIPPREENC]     CHAR (1)      NOT NULL,
    [VALMINNUM]     INT           NOT NULL,
    [VALMAXNUM]     INT           NOT NULL,
    [PRESELUNI]     XML           NULL,
    [OBSERVASI]     VARCHAR (MAX) NULL,
    [OBSERVANO]     VARCHAR (MAX) NULL,
    [OBSERVANI]     VARCHAR (MAX) NULL,
    [OBSERVALIS]    VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCTIPENCD] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Recomendaciones para cuando el tipo de respuesta es lista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'OBSERVALIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'La respectivas recomendaciones cuando el tipo de respuesta es Ninguna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'OBSERVANI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'La respectivas recomendaciones cuando el tipo de respuesta es No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'OBSERVANO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'La respectivas recomendaciones cuando el tipo de respuesta es si.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'OBSERVASI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la lista de valores para las preguntas de seleccion unica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'PRESELUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor maximo para las respuestas de tipo numerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'VALMAXNUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor minimo para las respuestas de tipo numerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'VALMINNUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de respuesta de la pregunta    1. Tipo de respuesta Si - No -Ninguno  2. Tipo de respuesta Numerica  3. Tipo de respuesta Lista de seleccion  4. Tipo de respuesta Multilinea (Texto)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'TIPPREENC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si la pregunta es requerida en la encuesta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'PREREQENC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la pregunta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'DESPREENC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auto de la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'AUTOHCTIPENCC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Columna autonumerica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'AUTO';


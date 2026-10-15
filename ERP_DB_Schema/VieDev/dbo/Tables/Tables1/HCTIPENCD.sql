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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de respuesta de la pregunta    1. Tipo de respuesta Si - No -Ninguno  2. Tipo de respuesta Numerica  3. Tipo de respuesta Lista de seleccion  4. Tipo de respuesta Multilinea (Texto)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'TIPPREENC';


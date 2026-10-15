CREATE TABLE [dbo].[HCTIPENCC] (
    [AUTO]       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NOMENCENF]  VARCHAR (200) NOT NULL,
    [GENEROAPLI] INT           NULL,
    [UNIEDADMI]  INT           NULL,
    [EDADMINIM]  INT           NULL,
    [UNIEDADMA]  INT           NULL,
    [EDADMAXIM]  INT           NULL,
    [ESTADOENC]  INT           NULL,
    CONSTRAINT [PK_HCTIPENCC] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado  1 activo -  2 inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'ESTADOENC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad Maxima en la que aplica  la lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'EDADMAXIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Medida para Edad Maxima: 1: Años    2: Meses    3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'UNIEDADMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad Minima en la que aplica la lista de Chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'EDADMINIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Medida para Edad Minima:  1: Años    2: Meses    3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'UNIEDADMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sexo Aplicacion Encuesta 1 Masculino  - 2 femenino - 3  Aplica para Ambas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'GENEROAPLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Lista chekeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'NOMENCENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Columna autonumerica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCC', @level2type = N'COLUMN', @level2name = N'AUTO';


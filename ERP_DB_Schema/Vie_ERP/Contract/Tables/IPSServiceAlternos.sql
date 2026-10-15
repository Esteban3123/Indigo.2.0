CREATE TABLE [Contract].[IPSServiceAlternos] (
    [Code]          VARCHAR (20) NOT NULL,
    [CIRUJANO]      VARCHAR (20) NOT NULL,
    [ANESTESIOLOGO] VARCHAR (20) NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el código del Anestesiologo.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSServiceAlternos', @level2type = N'COLUMN', @level2name = N'ANESTESIOLOGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el código del Cirujano.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSServiceAlternos', @level2type = N'COLUMN', @level2name = N'CIRUJANO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador de los alternos.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'IPSServiceAlternos', @level2type = N'COLUMN', @level2name = N'Code';


CREATE TABLE [Report].[TempCartera] (
    [IdCuenta] VARCHAR (50) NOT NULL,
    [Cuenta]   VARCHAR (20) NULL,
    [Descri]   VARCHAR (20) NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TempCartera', @level2type = N'COLUMN', @level2name = N'Descri';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TempCartera', @level2type = N'COLUMN', @level2name = N'Cuenta';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TempCartera', @level2type = N'COLUMN', @level2name = N'IdCuenta';


CREATE TABLE [dbo].[HCQXVIABO] (
    [CODVIAABO] CHAR (2)      NOT NULL,
    [DESVIAABO] VARCHAR (100) NULL,
    [ESTADOREG] BIT           NOT NULL,
    [GENCODVIA] TINYINT       NULL,
    CONSTRAINT [PK_HCQXVIABO] PRIMARY KEY CLUSTERED ([CODVIAABO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de intervencion quirurgica  1 - Basico  2 - Bilateral -- SOAT  3 - Bilateral Multiple -- SOAT  4 - MIVIE (Multiple Igual Via Igual Especialista) -- ISS  5 - MDVIE (Multiple Diferente Via Igual Especialista) -- ISS  6 - MIVDE (Multiple Igual Via Diferente Especialista) -- ISS  7 - MDVDE (Multiple Diferente Via Diferente Especialista) -- ISS  8 - PolitraumaIV (Politrauma Igual Via) -- ISS  9 - PolitraumaDV (Politrauma Diferente Via) -- ISS  10- No Cruento.    Campo que se registra para integracion nativa de control de cuentas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXVIABO', @level2type = N'COLUMN', @level2name = N'GENCODVIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro  True = Activo  False = Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXVIABO', @level2type = N'COLUMN', @level2name = N'ESTADOREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Via de Abordaje', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXVIABO', @level2type = N'COLUMN', @level2name = N'DESVIAABO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Interno de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCQXVIABO', @level2type = N'COLUMN', @level2name = N'CODVIAABO';


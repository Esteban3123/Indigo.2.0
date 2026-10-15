CREATE TABLE [dbo].[AGCAUCANC] (
    [CODCAUCAN]  CHAR (3)       NOT NULL,
    [DESCAUCAN]  NVARCHAR (150) NOT NULL,
    [ESTCAUCAN]  BIT            NOT NULL,
    [MOSTRARWEB] BIT            NULL,
    CONSTRAINT [PK_AGCAUCANC] PRIMARY KEY CLUSTERED ([CODCAUCAN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mostrar citas En Web  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCANC', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';


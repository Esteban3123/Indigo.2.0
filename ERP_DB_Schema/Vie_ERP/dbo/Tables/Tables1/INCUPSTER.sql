CREATE TABLE [dbo].[INCUPSTER] (
    [CODSERIPS] CHAR (20)  NOT NULL,
    [DESSERIPS] CHAR (300) NOT NULL,
    CONSTRAINT [PK_INCUPSTER] PRIMARY KEY CLUSTERED ([CODSERIPS] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Procedimiento o Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSTER', @level2type = N'COLUMN', @level2name = N'DESSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSTER', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


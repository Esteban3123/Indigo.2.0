CREATE TABLE [dbo].[HCREGICIR] (
    [IDREGIANES] INT         NOT NULL,
    [CODCIRUGI]  CHAR (10)   NOT NULL,
    [NOMCIRGIA]  NCHAR (500) NOT NULL,
    CONSTRAINT [PK_HCREGICIR] PRIMARY KEY CLUSTERED ([IDREGIANES] ASC, [CODCIRUGI] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la Cirugia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGICIR', @level2type = N'COLUMN', @level2name = N'NOMCIRGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Cirugia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGICIR', @level2type = N'COLUMN', @level2name = N'CODCIRUGI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Registrado en la Cabecera de Anestesia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGICIR', @level2type = N'COLUMN', @level2name = N'IDREGIANES';


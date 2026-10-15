CREATE TABLE [dbo].[HCREGMONI] (
    [IDREGISANES] INT           NOT NULL,
    [CODCATESUB]  VARCHAR (102) NOT NULL,
    [HORAREGIST]  CHAR (5)      NOT NULL,
    [VALOR]       CHAR (10)     NOT NULL,
    [CODCATEGO]   CHAR (2)      NOT NULL,
    [CODSUBCATE]  VARCHAR (102) NOT NULL,
    [ORDREGANE]   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SURGERYDATE] DATETIME      NULL,
    CONSTRAINT [PK_HCREGMONI] PRIMARY KEY CLUSTERED ([IDREGISANES] ASC, [CODCATESUB] ASC, [HORAREGIST] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la Cirugia y se usa para listar de manera correcta la informacion en el reporte         ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'SURGERYDATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Orden registro anestesia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'ORDREGANE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Subcategoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'CODSUBCATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Categoria ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'CODCATEGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la Subcategoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y Hora del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'HORAREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Categoria y Subcategoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'CODCATESUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Registrado en la Cabecera de Anestesia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGMONI', @level2type = N'COLUMN', @level2name = N'IDREGISANES';


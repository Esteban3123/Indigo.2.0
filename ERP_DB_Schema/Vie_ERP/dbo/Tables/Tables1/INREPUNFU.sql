CREATE TABLE [dbo].[INREPUNFU] (
    [CODCENATE]  NCHAR (10) NOT NULL,
    [UFUCODIGO]  NCHAR (10) NOT NULL,
    [CODREPORT]  NCHAR (10) NOT NULL,
    [PERMITEIMP] BIT        NOT NULL,
    [NUMCOPIAS]  NCHAR (10) NULL,
    [PERMODCOP]  BIT        NULL,
    CONSTRAINT [PK_INREPUNFU] PRIMARY KEY CLUSTERED ([CODCENATE] ASC, [UFUCODIGO] ASC, [CODREPORT] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permitir modificar y copiar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPUNFU', @level2type = N'COLUMN', @level2name = N'PERMODCOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de copias a imprimir', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPUNFU', @level2type = N'COLUMN', @level2name = N'NUMCOPIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite identificar si el reporte se encuetra habilitado 1:si  2:no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPUNFU', @level2type = N'COLUMN', @level2name = N'PERMITEIMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPUNFU', @level2type = N'COLUMN', @level2name = N'CODREPORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPUNFU', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INREPUNFU', @level2type = N'COLUMN', @level2name = N'CODCENATE';


CREATE TABLE [dbo].[CHOTIPBAC] (
    [CODTIPBAC] CHAR (3)  NOT NULL,
    [DESTIPBAC] CHAR (40) NOT NULL,
    CONSTRAINT [PK_CHOTIPBAC] PRIMARY KEY CLUSTERED ([CODTIPBAC] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Tipo de Bacteria - Infeccion Intrahospitalaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOTIPBAC', @level2type = N'COLUMN', @level2name = N'DESTIPBAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Tipo de Bacteria - Infeccion Intrahospitalaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOTIPBAC', @level2type = N'COLUMN', @level2name = N'CODTIPBAC';


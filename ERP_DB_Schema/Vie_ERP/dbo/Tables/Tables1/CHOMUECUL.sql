CREATE TABLE [dbo].[CHOMUECUL] (
    [CODMUECUL] CHAR (3)  NOT NULL,
    [DESMUECUL] CHAR (40) NOT NULL,
    CONSTRAINT [PK_CHOMUECUL] PRIMARY KEY CLUSTERED ([CODMUECUL] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la Muestra Cultivada - Infeccion Intrahospitalaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOMUECUL', @level2type = N'COLUMN', @level2name = N'DESMUECUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Muestra Cultivada - Infeccion Intrahospitalaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHOMUECUL', @level2type = N'COLUMN', @level2name = N'CODMUECUL';


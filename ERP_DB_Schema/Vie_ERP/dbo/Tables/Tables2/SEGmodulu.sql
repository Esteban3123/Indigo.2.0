CREATE TABLE [dbo].[SEGmodulu] (
    [indmodulo] CHAR (2)  NOT NULL,
    [indmoddes] CHAR (80) NOT NULL,
    CONSTRAINT [PK_SEGmodulu] PRIMARY KEY CLUSTERED ([indmodulo] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Modulo en INDIGO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGmodulu', @level2type = N'COLUMN', @level2name = N'indmoddes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del modulo en INDIGO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGmodulu', @level2type = N'COLUMN', @level2name = N'indmodulo';


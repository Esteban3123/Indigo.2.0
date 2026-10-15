CREATE TABLE [dbo].[SEGmenusu] (
    [indidmenu] VARCHAR (5) NOT NULL,
    [indmodulo] CHAR (2)    NOT NULL,
    [indopcion] INT         NOT NULL,
    CONSTRAINT [PK_SEGmenusu] PRIMARY KEY CLUSTERED ([indidmenu] ASC, [indmodulo] ASC),
    CONSTRAINT [FK_SEGmenusu_SEGmodulu] FOREIGN KEY ([indmodulo]) REFERENCES [dbo].[SEGmodulu] ([indmodulo])
);


GO
ALTER TABLE [dbo].[SEGmenusu] NOCHECK CONSTRAINT [FK_SEGmenusu_SEGmodulu];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Opcion 0->Generales  1-> Archivo 2->Procesos 3-> Utilidades 4->Reportes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGmenusu', @level2type = N'COLUMN', @level2name = N'indopcion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del modulo en INDIGO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGmenusu', @level2type = N'COLUMN', @level2name = N'indmodulo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Menu en INDIGO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGmenusu', @level2type = N'COLUMN', @level2name = N'indidmenu';


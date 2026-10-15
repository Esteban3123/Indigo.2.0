CREATE TABLE [Inventory].[ProductoSubgrupos] (
    [DCI]               NVARCHAR (255) NULL,
    [DCIInvima]         NVARCHAR (255) NULL,
    [CodATCN]           NVARCHAR (255) NULL,
    [ATC]               NVARCHAR (255) NULL,
    [Grupo]             NVARCHAR (255) NULL,
    [Subgrupo]          NVARCHAR (255) NULL,
    [Descripcion ATC]   NVARCHAR (255) NULL,
    [CUMS]              NVARCHAR (255) NULL,
    [descripcion Cums]  NVARCHAR (255) NULL,
    [Pos (Si-No)]       NVARCHAR (255) NULL,
    [FORMAFARMACEUTICA] NVARCHAR (255) NULL,
    [VIASUNICAS]        NVARCHAR (255) NULL,
    [REGISTROSANITARIO] NVARCHAR (255) NULL,
    [FECHAVENCIMIENTO]  NVARCHAR (255) NULL,
    [ConcentracionF]    NVARCHAR (255) NULL,
    [Costo]             NVARCHAR (255) NULL,
    [PrecioVenta]       NVARCHAR (255) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio de venta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'PrecioVenta';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'Costo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'ConcentracionF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'FECHAVENCIMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro Sanitario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'REGISTROSANITARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unicas Vías', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'VIASUNICAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Forma Farmaceutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'FORMAFARMACEUTICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Pos ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'Pos (Si-No)';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripcion del Cums', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'descripcion Cums';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CUMS', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'CUMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del ATC', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'Descripcion ATC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'SubGrupo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'Subgrupo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'Grupo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ATC', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'ATC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del ATC', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'CodATCN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Invima del DCI', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'DCIInvima';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DCI', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductoSubgrupos', @level2type = N'COLUMN', @level2name = N'DCI';


CREATE TABLE [dbo].[tarifa] (
    [ID COMPANY]           VARCHAR (9)     NULL,
    [CODIGO DE CONTRATO]   VARCHAR (20)    NULL,
    [CONTRATO]             VARCHAR (100)   NULL,
    [CODIGO DEL PRODUCTO]  VARCHAR (20)    NOT NULL,
    [PRODUCTO]             VARCHAR (200)   NOT NULL,
    [FECHA DE VENCIMIENTO] DATE            NULL,
    [GRUPO]                VARCHAR (100)   NULL,
    [SUB GRUPO]            VARCHAR (100)   NULL,
    [TIPO DE PRODUCTO]     VARCHAR (100)   NOT NULL,
    [ATC]                  VARCHAR (20)    NULL,
    [PRODUCTO DE CONTROL]  VARCHAR (2)     NULL,
    [PRECIO REGULADO]      VARCHAR (2)     NOT NULL,
    [POS]                  VARCHAR (6)     NULL,
    [COSTO]                DECIMAL (18, 2) NOT NULL,
    [ULTIMO COSTO]         NUMERIC (18, 2) NULL,
    [PRECIO DE VENTA]      NUMERIC (18, 2) NOT NULL,
    [DIFERENCIA %]         NUMERIC (18)    NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la diferencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tarifa', @level2type = N'COLUMN', @level2name = N'DIFERENCIA %';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda elprecio de venta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tarifa', @level2type = N'COLUMN', @level2name = N'PRECIO DE VENTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el ultimo costo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tarifa', @level2type = N'COLUMN', @level2name = N'ULTIMO COSTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el costo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tarifa', @level2type = N'COLUMN', @level2name = N'COSTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el pos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tarifa', @level2type = N'COLUMN', @level2name = N'POS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda precio regulado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tarifa', @level2type = N'COLUMN', @level2name = N'PRECIO REGULADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda producto control', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tarifa', @level2type = N'COLUMN', @level2name = N'PRODUCTO DE CONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el nombre  ATC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tarifa', @level2type = N'COLUMN', @level2name = N'ATC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda que tipo de grupo es', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tarifa', @level2type = N'COLUMN', @level2name = N'TIPO DE PRODUCTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el subgrupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tarifa', @level2type = N'COLUMN', @level2name = N'SUB GRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tarifa', @level2type = N'COLUMN', @level2name = N'GRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de vencimiento del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tarifa', @level2type = N'COLUMN', @level2name = N'FECHA DE VENCIMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el producto nombre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tarifa', @level2type = N'COLUMN', @level2name = N'PRODUCTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tarifa', @level2type = N'COLUMN', @level2name = N'CODIGO DEL PRODUCTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tarifa', @level2type = N'COLUMN', @level2name = N'CONTRATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo del contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tarifa', @level2type = N'COLUMN', @level2name = N'CODIGO DE CONTRATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'tarifa', @level2type = N'COLUMN', @level2name = N'ID COMPANY';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Almacena el listado de tarifas de productos asociados a contratos por compañía, registrando costo, último costo y precio de venta, así como la diferencia porcentual entre ambos. Cada producto se clasifica por tipo, grupo, subgrupo y código ATC, e incluye indicadores de control (producto controlado, precio regulado, POS) y fecha de vencimiento de la tarifa vigente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'tarifa';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'tarifa';
GO

CREATE TABLE [dbo].[sismedVentas] (
    [Cedula]                CHAR (15)     NOT NULL,
    [Nombre]                CHAR (250)    NOT NULL,
    [Ingreso]               CHAR (10)     NOT NULL,
    [CodigoServicio]        VARCHAR (20)  NOT NULL,
    [NombreServicio]        VARCHAR (200) NOT NULL,
    [FechaServicio]         DATETIME      NOT NULL,
    [Cantidad]              INT           NOT NULL,
    [ValorUnitario]         NUMERIC (18)  NOT NULL,
    [Descuento]             NUMERIC (18)  NOT NULL,
    [Total]                 NUMERIC (18)  NOT NULL,
    [Categoria]             VARCHAR (123) NOT NULL,
    [GrupoAtencion]         VARCHAR (123) NOT NULL,
    [EntidadAdministradora] VARCHAR (123) NULL,
    [TerceroEntidad]        VARCHAR (318) NULL,
    [UnidadesFuncionales]   VARCHAR (73)  NOT NULL,
    [CentroCosto]           VARCHAR (223) NOT NULL,
    [Factura]               VARCHAR (15)  NOT NULL,
    [UsuarioFacturacion]    VARCHAR (20)  NOT NULL,
    [UsuarioCargo]          VARCHAR (20)  NOT NULL,
    [FechaFactura]          DATETIME      NOT NULL,
    [CodeCUM]               VARCHAR (20)  NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el codigo CUM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'CodeCUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la fecha de la factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'FechaFactura';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el cargo del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'UsuarioCargo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la facturación del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'UsuarioFacturacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Factura';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el centro de costo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'CentroCosto';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la unidades funcionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'UnidadesFuncionales';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la entidad como tercero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'TerceroEntidad';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la entidad administradora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'EntidadAdministradora';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el grupo de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'GrupoAtencion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la categoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Categoria';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el total', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Total';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el descuento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Descuento';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el valor unitario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'ValorUnitario';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la cantidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Cantidad';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la fecha del servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'FechaServicio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el nombre del servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'NombreServicio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el codigo del servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'CodigoServicio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Ingreso';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el nombre del sistema de ventas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Nombre';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la cédula', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'sismedVentas', @level2type = N'COLUMN', @level2name = N'Cedula';


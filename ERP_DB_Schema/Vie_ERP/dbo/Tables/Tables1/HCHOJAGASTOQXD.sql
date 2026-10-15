CREATE TABLE [dbo].[HCHOJAGASTOQXD] (
    [ID]                     INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCHOJAGASTOQX]        INT          NOT NULL,
    [CODPRODUC]              CHAR (20)    NOT NULL,
    [IDPRODUCTO]             INT          NULL,
    [CANTIDADENTREGADA]      INT          NOT NULL,
    [CANTIDADGASTADA]        INT          NULL,
    [CANTIDADDEVOLVER]       INT          NULL,
    [CANTIDADACEPTADADEV]    INT          NULL,
    [ORIGENSOLICITUD]        INT          NOT NULL,
    [FECHAREGISTRO]          DATETIME     NOT NULL,
    [CONSEKARDEX]            VARCHAR (50) NULL,
    [RequestType]            INT          NULL,
    [StatusOrder]            INT          NULL,
    [IdNursingPackagesOrder] INT          NULL,
    CONSTRAINT [PK_HCHOJAGASTOQXD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [fk_consecutivoKardex] FOREIGN KEY ([CONSEKARDEX]) REFERENCES [dbo].[HCKARDPAC] ([NUMCONSEC]),
    CONSTRAINT [FK_HCHOJAGASTOQXD_HCHOJAGASTOQX] FOREIGN KEY ([IDHCHOJAGASTOQX]) REFERENCES [dbo].[HCHOJAGASTOQX] ([ID])
);


GO
ALTER TABLE [dbo].[HCHOJAGASTOQXD] NOCHECK CONSTRAINT [fk_consecutivoKardex];


GO
ALTER TABLE [dbo].[HCHOJAGASTOQXD] NOCHECK CONSTRAINT [FK_HCHOJAGASTOQXD_HCHOJAGASTOQX];


GO
CREATE NONCLUSTERED INDEX [UX_HCHOJAGASTOQXD_IDHCHOJAGASTOQX_CODPRODUC_RequestType]
    ON [dbo].[HCHOJAGASTOQXD]([IDHCHOJAGASTOQX] ASC, [CODPRODUC] ASC, [RequestType] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relación con la tabla: NursingPackagesOrder donde me va indicar la relacion con el paquete que fue ordenado desde solicitud de medicamentos e insumos (ENFERMERIA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'IdNursingPackagesOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Producto pendiente ó no validado en su totalidad, no aparece el check verde en la hoja de gasto qx    2 - Producto validado es decir que aparece el chek verde en la hoja de gasto qx', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'StatusOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1: Productos asociados al paquete Qx, es decir RequestType = 1    2: Productos solicitados por solicitud de medicamento e insumos, es decir RequestType = 2    3:  Productos solicitados por solicitud de medicamento e insumos (paquete de enfermeria), es decir RequestType = 3  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'RequestType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'relacion con el consecutivo del kardex - HCKARDPAC ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'CONSEKARDEX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro de la hoja de gasto quirúrgico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1) Dispensacion  2) Inventario en consignacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'ORIGENSOLICITUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cantidad acepta en la devolucion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'CANTIDADACEPTADADEV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cantidad a devolver a inventario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'CANTIDADDEVOLVER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cantidad gastada en la cirugia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'CANTIDADGASTADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cantidad entregada a la cirugia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'CANTIDADENTREGADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del producto  (VIE ERP)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'IDPRODUCTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de producto de IHLISTPRO - puede ser nulo cuando se cargan producto de origen inventario en consignacion ya que vienen de la tabla de productos de VIE ERP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de Hoja de gasto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'IDHCHOJAGASTOQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'ID';


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



GO



GO
CREATE NONCLUSTERED INDEX [UX_HCHOJAGASTOQXD_IDHCHOJAGASTOQX_CODPRODUC_RequestType]
    ON [dbo].[HCHOJAGASTOQXD]([IDHCHOJAGASTOQX] ASC, [CODPRODUC] ASC, [RequestType] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT NULL | Identificador de paquete de enfermería ordenado; relación con tabla NursingPackagesOrder que vincula medicamentos e insumos solicitados desde módulo de enfermería a la hoja de gasto quirúrgico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'IdNursingPackagesOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla: NursingPackagesOrder donde me va indicar la relacion con el paquete que fue ordenado desde solicitud de medicamentos e insumos (ENFERMERIA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'IdNursingPackagesOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'IdNursingPackagesOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT NULL | Estado de validación del producto en hoja de gasto: 1=Pendiente/No validado (sin check verde), 2=Validado (check verde confirmado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'StatusOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Producto pendiente ó no validado en su totalidad, no aparece el check verde en la hoja de gasto qx    2 - Producto validado es decir que aparece el chek verde en la hoja de gasto qx', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'StatusOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'StatusOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT NULL | Tipo de origen de la solicitud: 1=Productos paquete quirúrgico, 2=Solicitud medicamentos e insumos, 3=Paquete de enfermería (medicamentos e insumos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'RequestType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1: Productos asociados al paquete Qx, es decir RequestType = 1    2: Productos solicitados por solicitud de medicamento e insumos, es decir RequestType = 2    3:  Productos solicitados por solicitud de medicamento e insumos (paquete de enfermeria), es decir RequestType = 3  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'RequestType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'RequestType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(50) NULL | Número de consecutivo del kardex de paciente; FK a HCKARDPAC.NUMCONSEC | Trazabilidad de inventario y movimiento de medicamentos/insumos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'CONSEKARDEX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'relacion con el consecutivo del kardex - HCKARDPAC ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'CONSEKARDEX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'CONSEKARDEX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME NOT NULL | Fecha y hora de registro del movimiento en hoja de gasto quirúrgico; auditoría y control de operación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de registro de la hoja de gasto quirúrgico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT NOT NULL | Origen del suministro: 1=Dispensación (farmacia/almacén), 2=Inventario en consignación (proveedor directo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'ORIGENSOLICITUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1) Dispensacion  2) Inventario en consignacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'ORIGENSOLICITUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'ORIGENSOLICITUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT NULL | Cantidad de unidades aceptadas en devolución a inventario; control de recepción de mercancía devuelta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'CANTIDADACEPTADADEV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cantidad acepta en la devolucion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'CANTIDADACEPTADADEV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'CANTIDADACEPTADADEV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT NULL | Cantidad de unidades a devolver a inventario tras cirugía; diferencia entre entregada y gastada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'CANTIDADDEVOLVER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cantidad a devolver a inventario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'CANTIDADDEVOLVER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'CANTIDADDEVOLVER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT NULL | Cantidad de unidades efectivamente utilizadas en el procedimiento quirúrgico; consumo real', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'CANTIDADGASTADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cantidad gastada en la cirugia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'CANTIDADGASTADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'CANTIDADGASTADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT NOT NULL | Cantidad de unidades entregadas al acto quirúrgico; despacho inicial de medicamentos e insumos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'CANTIDADENTREGADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cantidad entregada a la cirugia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'CANTIDADENTREGADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'CANTIDADENTREGADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT NULL | Identificador único del producto en VIE ERP; clave primaria del maestro de productos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'IDPRODUCTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del producto  (VIE ERP)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'IDPRODUCTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'IDPRODUCTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(20) NOT NULL | Código del producto desde IHLISTPRO; nullable si proviene de inventario en consignación sin equivalente en ERP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de producto de IHLISTPRO - puede ser nulo cuando se cargan producto de origen inventario en consignacion ya que vienen de la tabla de productos de VIE ERP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT NOT NULL | Identificador de cabecera de hoja de gasto quirúrgico; FK a HCHOJAGASTOQX.ID | Agrupa detalles de consumo por procedimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'IDHCHOJAGASTOQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de Hoja de gasto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'IDHCHOJAGASTOQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'IDHCHOJAGASTOQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT IDENTITY(1,1) NOT NULL | Autonumérico único de línea de detalle; clave primaria del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de consumo de materiales e insumos quirúrgicos por procedimiento (hoja de gastos de quirófano). Registra qué productos fueron entregados, gastados y devueltos durante una intervención quirúrgica, trazando el movimiento de inventario en pabellón.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAGASTOQXD';

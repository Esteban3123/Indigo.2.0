CREATE TABLE [dbo].[HCORDPROQD] (
    [ID]          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AUTOPROCED]  INT          NOT NULL,
    [CODPRODUC]   CHAR (20)    NOT NULL,
    [CANTSOLICIT] INT          NOT NULL,
    [CANTENTREG]  INT          CONSTRAINT [DF_HCORDPROQD_CANTENTREG] DEFAULT ((0)) NOT NULL,
    [MATESTADO]   TINYINT      NOT NULL,
    [NUMORDEN]    VARCHAR (20) NULL,
    CONSTRAINT [PK_HCORDPROQD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORDPROQD_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de orden de compra generado por VIE ERP; referencia única del pedido de materiales/productos de osteosíntesis al proveedor (VARCHAR 20, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'NUMORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que indica el numero de orden generado  lo afecta VIE ERP cuando hacen la orden de compra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'NUMORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'NUMORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del material en solicitud: 1=Solicitado, 2=Orden de compra generada por VIE ERP; indica fase del proceso de adquisición de productos quirúrgicos (TINYINT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'MATESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la solicitud:   1-> Solicitado.   2-> Orden Compra generada para el producto, estado que lo coloca vie erp.       ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'MATESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'MATESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades entregadas/recibidas del producto, actualizada por VIE ERP al confirmar recepción de orden de compra (INT, default 0)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'CANTENTREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad entregada, la afecta VIE ERP cuando hacen la orden de compra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'CANTENTREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'CANTENTREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades solicitadas del producto de osteosíntesis para el procedimiento quirúrgico (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'CANTSOLICIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Solicitada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'CANTSOLICIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'CANTSOLICIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto de osteosíntesis (implantes, instrumentales quirúrgicos); referencia FK a IHLISTPRO.CODPRODUC (CHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del producto de osteosintesis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autoincrementable del procedimiento quirúrgico asociado a la solicitud de materiales (INT, FK implícita)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'AUTOPROCED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del autonumerico del procedimiento qx', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'AUTOPROCED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'AUTOPROCED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico primario (identidad) de la línea de solicitud de producto en orden quirúrgica; clave principal de la tabla (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de productos o insumos solicitados en órdenes de procedimientos clínicos (quirúrgicos o de sala). Registra cada ítem requerido para un procedimiento: el producto solicitado, la cantidad pedida y la cantidad efectivamente entregada, junto con su estado de despacho.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQD';

CREATE TABLE [dbo].[InventoryAdjustments] (
    [ID_COMPANY]           VARCHAR (9)     NULL,
    [NRO DOCUMENTO]        VARCHAR (20)    NOT NULL,
    [FECHA DOCUMENTO]      DATETIME        NOT NULL,
    [FECHA CONFIRMACION]   DATETIME        NULL,
    [TIPO AJUSTE]          VARCHAR (17)    NULL,
    [CONCEPTO AJUSTE]      VARCHAR (123)   NULL,
    [TERCERO]              VARCHAR (328)   NULL,
    [ESTADO]               VARCHAR (10)    NULL,
    [SEDE]                 VARCHAR (100)   NOT NULL,
    [BODEGA]               VARCHAR (123)   NOT NULL,
    [OPERACION]            VARCHAR (5)     NOT NULL,
    [TIPO]                 VARCHAR (100)   NOT NULL,
    [CODIGO PRODUCTO]      VARCHAR (20)    NOT NULL,
    [DESCRIPCION PRODUCTO] VARCHAR (400)   NOT NULL,
    [CODIGO PADRE]         VARCHAR (20)    NULL,
    [DESCRIPCION PADRE]    VARCHAR (200)   NULL,
    [REGISTRO SANITARIO]   VARCHAR (30)    NULL,
    [LOTE]                 VARCHAR (50)    NULL,
    [FECHA VENCIMIENTO]    DATE            NULL,
    [IVA]                  VARCHAR (200)   NULL,
    [CANTIDAD]             INT             NOT NULL,
    [VALOR  UNITARIO]      NUMERIC (18, 2) NOT NULL,
    [VALOR TOTAL]          NUMERIC (29, 2) NULL,
    [PACIENTE]             VARCHAR (278)   NOT NULL,
    [CENTRO DE COSTO]      VARCHAR (200)   NOT NULL,
    [FECHA BUSQUEDA]       DATE            NULL,
    [ULT_ACTUAL]           DATETIME        NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Registra los ajustes de inventario de productos (medicamentos e insumos) realizados en bodegas y sedes de una organización de salud. Cada registro identifica el documento de ajuste, el tipo y concepto del ajuste, el estado, el producto afectado con su lote y fecha de vencimiento, así como cantidades y valores unitarios/totales. Incluye referencias a paciente y centro de costo, lo que permite vincular el movimiento de inventario con la atención clínica correspondiente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'InventoryAdjustments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'InventoryAdjustments';
GO

CREATE TABLE [dbo].[SALDOSGLOSAS_ESTANCARTERA] (
    [Id]                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Cliente]                VARCHAR (20)  NOT NULL,
    [Factura]                VARCHAR (20)  NULL,
    [EstadoFactura]          VARCHAR (2)   NULL,
    [CategoriaFactura]       VARCHAR (2)   NULL,
    [Fecha]                  DATETIME      NOT NULL,
    [Plazo]                  VARCHAR (2)   NULL,
    [NumCuota]               VARCHAR (2)   NULL,
    [CuentaContableSaldo]    VARCHAR (20)  NOT NULL,
    [CentroCosto]            VARCHAR (20)  NULL,
    [Observacion]            VARCHAR (100) NULL,
    [Valor]                  DECIMAL (18)  NOT NULL,
    [Saldo]                  DECIMAL (18)  NOT NULL,
    [CentroCostoGlosas]      VARCHAR (20)  NULL,
    [CuentaSinRadicar]       VARCHAR (20)  NOT NULL,
    [CuentaRadicada]         VARCHAR (20)  NULL,
    [CuentaGlosa]            VARCHAR (20)  NOT NULL,
    [CuentaConciliacion]     VARCHAR (20)  NOT NULL,
    [CuentaJuridico]         VARCHAR (20)  NOT NULL,
    [CuentaOrdenGlosa]       VARCHAR (20)  NULL,
    [CuentaAcreedoresGlosas] VARCHAR (20)  NULL,
    [Observacion2]           VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_SALDOSGLOSAS_ESTANCARTERA__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldos y estado de cartera de glosas: registra el seguimiento contable y financiero de facturas con glosas, incluyendo los saldos pendientes por etapa (sin radicar, radicada, en glosa, conciliación, jurídico) para la gestión de cartera de clientes (aseguradoras, EPS, pagadores).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de cartera.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del cliente o pagador (EPS, aseguradora, entidad contratante) al que pertenece la factura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'Cliente';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'Cliente';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de la factura de venta o cuenta de cobro sobre la que se gestiona la glosa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'Factura';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'Factura';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual de la factura en el proceso de cobro (ej: radicada, glosada, conciliada, en cobro jurídico).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'EstadoFactura';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'EstadoFactura';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Categoría o clasificación de la factura según el tipo de servicio o contrato (ej: hospitalización, urgencias, ambulatorio).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'CategoriaFactura';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'CategoriaFactura';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de referencia del registro de cartera o de la factura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'Fecha';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'Fecha';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plazo de pago acordado con el cliente o pagador (en días o código de plazo contractual).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'Plazo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'Plazo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de cuota o pago parcial asociado a la factura, cuando el pago se realiza en instalamentos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'NumCuota';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'NumCuota';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable principal donde se registra el saldo de la factura en cartera.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'CuentaContableSaldo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'CuentaContableSaldo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centro de costo o unidad de negocio responsable de la factura o del servicio prestado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'CentroCosto';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'CentroCosto';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nota o comentario adicional sobre el estado o gestión de la cartera de esa factura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'Observacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'Observacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total original de la factura o cuenta de cobro (monto facturado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'Valor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'Valor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo pendiente de cobro de la factura, descontando pagos y notas crédito aplicados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'Saldo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'Saldo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centro de costo específico al que se asignan los movimientos contables de las glosas de esa factura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'CentroCostoGlosas';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'CentroCostoGlosas';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable donde se registra el valor de la factura que aún no ha sido radicada ante el pagador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'CuentaSinRadicar';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'CuentaSinRadicar';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable donde se registra el valor de la factura ya radicada y en proceso de pago.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'CuentaRadicada';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'CuentaRadicada';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable que acumula el valor glosado (objetado o rechazado) por el pagador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'CuentaGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'CuentaGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable utilizada para registrar los valores en proceso de conciliación de glosas con el pagador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'CuentaConciliacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'CuentaConciliacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable donde se registran glosas trasladadas a cobro jurídico o proceso legal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'CuentaJuridico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'CuentaJuridico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable para registrar órdenes de glosa emitidas por el pagador durante la auditoría de cuentas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'CuentaOrdenGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'CuentaOrdenGlosa';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable de acreedores relacionada con glosas reconocidas o aceptadas que generan una obligación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'CuentaAcreedoresGlosas';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'CuentaAcreedoresGlosas';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segunda nota o comentario complementario sobre la gestión de glosa o cartera de la factura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'Observacion2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SALDOSGLOSAS_ESTANCARTERA', @level2type = N'COLUMN', @level2name = N'Observacion2';

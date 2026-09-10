CREATE TABLE [dbo].[PortfolioInitialBalance] (
    [Id]                                                                 INT          IDENTITY (1, 1) NOT NULL,
    [Cliente]                                                            VARCHAR (50) NULL,
    [Nº Factura]                                                         VARCHAR (50) NULL,
    [Estado Factura]                                                     VARCHAR (50) NULL,
    [Categoria de Factura]                                               VARCHAR (50) NULL,
    [Fecha]                                                              VARCHAR (50) NULL,
    [Plazo]                                                              VARCHAR (50) NULL,
    [Numero Cuota]                                                       VARCHAR (50) NULL,
    [Cuenta Contable Saldos]                                             VARCHAR (50) NULL,
    [Centro de Costo]                                                    VARCHAR (50) NULL,
    [Observacion]                                                        VARCHAR (50) NULL,
    [Valor Factura]                                                      NUMERIC (18) NULL,
    [Saldo Cuenta]                                                       NUMERIC (18) NULL,
    [Centro de Costos Glosas]                                            VARCHAR (50) NULL,
    [Cuenta Contable sin Radicar]                                        VARCHAR (50) NULL,
    [Cuenta Contable Radicada]                                           VARCHAR (50) NULL,
    [Cuenta Contable Glosa Subsanable (Empresa Privada)]                 VARCHAR (50) NULL,
    [Cuenta Contable Conciliación (Empresa Privada)]                     VARCHAR (50) NULL,
    [Cuenta Contable Cobro Juridico o Dificil Recaudo (Empresa Privada)] VARCHAR (50) NULL,
    [Cuenta Contable Orden de Glosa (Empresa Publica)]                   VARCHAR (50) NULL,
    [Cuenta Contable Acreedores Glosas (Empresa Publica)]                VARCHAR (50) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldos iniciales de cartera por factura. Registra el estado contable de arranque de cada factura emitida a clientes (aseguradoras, EPS, empresas privadas o públicas), incluyendo valores, cuentas contables asociadas y clasificación de glosas para el proceso de cartera y conciliación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de saldo inicial de cartera.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o código del cliente o pagador al que se emitió la factura (EPS, aseguradora, empresa, entidad pública o privada).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Cliente';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Cliente';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de la factura de venta o prestación de servicios de salud asociada al saldo inicial de cartera.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Nº Factura';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Nº Factura';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual de la factura en el proceso de cartera (por ejemplo: radicada, sin radicar, en glosa, pagada, en cobro jurídico).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Estado Factura';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Estado Factura';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación o categoría de la factura según el tipo de servicio o contrato (hospitalización, urgencias, ambulatorio, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Categoria de Factura';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Categoria de Factura';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la factura o del registro del saldo inicial de cartera.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Fecha';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Fecha';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plazo de pago acordado o vencimiento de la factura con el cliente o pagador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Plazo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Plazo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de cuota o pago parcial al que corresponde el saldo registrado, cuando la factura se paga en cuotas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Numero Cuota';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Numero Cuota';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable del plan de cuentas utilizada para registrar el saldo de la factura en la contabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Cuenta Contable Saldos';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Cuenta Contable Saldos';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centro de costo o unidad de negocio al que se imputa la factura y su saldo de cartera.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Centro de Costo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Centro de Costo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o notas adicionales sobre el estado o particularidades del saldo inicial de la factura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Observacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Observacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total facturado al cliente por los servicios de salud prestados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Valor Factura';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Valor Factura';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo contable pendiente de pago o por conciliar en la cuenta asociada a la factura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Saldo Cuenta';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Saldo Cuenta';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centro de costo específico al que se asignan las glosas (devoluciones o descuentos) aplicadas sobre la factura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Centro de Costos Glosas';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Centro de Costos Glosas';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable del plan de cuentas donde se registran las facturas que aún no han sido radicadas ante el pagador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Cuenta Contable sin Radicar';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Cuenta Contable sin Radicar';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable donde se registran las facturas ya radicadas formalmente ante el cliente o aseguradora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Cuenta Contable Radicada';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Cuenta Contable Radicada';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable para registrar glosas subsanables (corregibles) en facturas con empresas privadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Cuenta Contable Glosa Subsanable (Empresa Privada)';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Cuenta Contable Glosa Subsanable (Empresa Privada)';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable utilizada en el proceso de conciliación de cartera con empresas privadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Cuenta Contable Conciliación (Empresa Privada)';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Cuenta Contable Conciliación (Empresa Privada)';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable para facturas en cobro jurídico o de difícil recaudo con empresas privadas, cartera morosa o en litigio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Cuenta Contable Cobro Juridico o Dificil Recaudo (Empresa Privada)';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Cuenta Contable Cobro Juridico o Dificil Recaudo (Empresa Privada)';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable para registrar órdenes de glosa emitidas por entidades públicas sobre las facturas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Cuenta Contable Orden de Glosa (Empresa Publica)';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Cuenta Contable Orden de Glosa (Empresa Publica)';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable de acreedores por glosas aplicadas por empresas públicas, representa valores descontados o en disputa con entidades del sector público.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Cuenta Contable Acreedores Glosas (Empresa Publica)';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalance', @level2type = N'COLUMN', @level2name = N'Cuenta Contable Acreedores Glosas (Empresa Publica)';

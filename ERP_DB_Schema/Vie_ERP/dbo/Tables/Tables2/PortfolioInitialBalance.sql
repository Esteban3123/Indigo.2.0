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


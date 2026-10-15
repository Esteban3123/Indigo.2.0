CREATE TABLE [dbo].[ImportProductRates] (
    [CodigoProducto]         VARCHAR (50)  NOT NULL,
    [TipoLiquidacion]        TINYINT       NOT NULL,
    [TipoTarifa]             TINYINT       NOT NULL,
    [TipoPorcentaje]         TINYINT       NOT NULL,
    [CUPS]                   VARCHAR (50)  NULL,
    [DescripcionRelacionada] VARCHAR (50)  NULL,
    [FechaInicial]           DATE          NOT NULL,
    [FechaFinal]             DATE          NOT NULL,
    [Porcentaje]             DECIMAL (18)  NULL,
    [PreciodeVenta]          DECIMAL (18)  NOT NULL,
    [PrecioconRecargo]       DECIMAL (18)  NOT NULL,
    [Contratado]             BIT           NULL,
    [AlCotizar]              BIT           NULL,
    [Observaciones]          VARCHAR (500) NULL
);


CREATE TABLE [Report].[TableAgrupadoresSanitas] (
    [ID]                           INT             IDENTITY (1, 1) NOT NULL,
    [CUPS]                         CHAR (10)       NOT NULL,
    [codigoDescripcionRelacionada] INT             NULL,
    [grupoIndigo]                  VARCHAR (60)    NULL,
    [descripcionCUPS]              VARCHAR (300)   NULL,
    [sanitasPGP]                   VARCHAR (30)    NULL,
    [observacion]                  VARCHAR (300)   NULL,
    [agrupadorNT]                  VARCHAR (100)   NULL,
    [valor]                        NUMERIC (20, 2) NULL
);


CREATE TABLE [dbo].[Fondos] (
    [CedulaEmpleado]      VARCHAR (50)  NULL,
    [FondoSalud]          VARCHAR (200) NULL,
    [FondoPension]        VARCHAR (200) NULL,
    [FondoARL]            VARCHAR (200) NULL,
    [CajaCompensacion]    VARCHAR (200) NULL,
    [FondoCesantias]      VARCHAR (200) NULL,
    [FondoSaludPrepagada] VARCHAR (200) NULL,
    [ValorSaludPrepagada] NUMERIC (18)  NULL,
    [IdFondoSalud]        INT           NULL,
    [IdFondoPension]      INT           NULL,
    [IdFondoARL]          INT           NULL,
    [IdCaja]              INT           NULL,
    [IdCesantias]         INT           NULL,
    [IdSaludPrepagada]    INT           NULL
);


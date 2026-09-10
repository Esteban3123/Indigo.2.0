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
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Almacena la afiliación de seguridad social de cada empleado, identificado por su cédula, registrando las entidades a las que pertenece en salud, pensión, riesgos laborales (ARL), caja de compensación, cesantías y salud prepagada, junto con sus respectivos identificadores numéricos. Incluye además el valor monetario asociado al plan de salud prepagada del empleado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fondos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fondos';
GO

CREATE TABLE [Payroll].[Fondos_Contratos] (
    [Cedula]                VARCHAR (20)  NULL,
    [Id_Empleado]           INT           NULL,
    [Id_Contrato]           INT           NULL,
    [TipoFondo_Salud]       TINYINT       NULL,
    [FondoSalud]            VARCHAR (MAX) NULL,
    [Id__FSalud]            INT           NULL,
    [TipoFondo_Pension]     TINYINT       NULL,
    [FondoPension]          VARCHAR (MAX) NULL,
    [Id__FPension]          INT           NULL,
    [TipoFondo_ARL]         TINYINT       NULL,
    [FondoARL]              VARCHAR (MAX) NULL,
    [Id__FARL]              INT           NULL,
    [TipoFondo_Caja]        TINYINT       NULL,
    [CajaCompensacion]      VARCHAR (MAX) NULL,
    [Id__CajaCompensacion]  INT           NULL,
    [TipoFondo_Cesantias]   TINYINT       NULL,
    [FondoCesantias]        VARCHAR (MAX) NULL,
    [Id__FCesantias]        INT           NULL,
    [FondoSaludPrepagada]   VARCHAR (MAX) NULL,
    [Id__FSaludPrepagada]   INT           NULL,
    [ValorSaludPrepagada]   VARCHAR (MAX) NULL,
    [FondoPensionPrepagada] VARCHAR (MAX) NULL,
    [Id__FPensionPrepagada] INT           NULL,
    [ValorPensionPrepagada] VARCHAR (MAX) NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla del módulo de nómina que almacena la afiliación a fondos de seguridad social asociada a cada contrato de empleado. Registra las entidades de salud, pensión, ARL, caja de compensación y cesantías vinculadas, incluyendo sus identificadores y tipos de fondo. Adicionalmente, contempla fondos complementarios de salud y pensión prepagada con sus respectivos valores de aporte.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'TABLE', @level1name=N'Fondos_Contratos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'TABLE', @level1name=N'Fondos_Contratos';
GO

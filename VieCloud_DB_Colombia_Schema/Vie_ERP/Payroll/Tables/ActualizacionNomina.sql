CREATE TABLE [Payroll].[ActualizacionNomina] (
    [Cedula]                VARCHAR (20) NULL,
    [CodigoUnidadFuncional] VARCHAR (15) NULL,
    [CodigoCentroCosto]     VARCHAR (15) NULL,
    [CodigoBanco]           VARCHAR (15) NULL,
    [NumeroCuentaBancaria]  VARCHAR (20) NULL,
    [TipoCuentaBancaria]    INT          NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de trabajo del módulo de nómina que almacena actualizaciones pendientes o en tránsito para empleados identificados por cédula, incluyendo reasignaciones de unidad funcional, centro de costo y datos bancarios (banco, número y tipo de cuenta). Su estructura sin llaves ni índices sugiere que actúa como tabla de carga temporal o de importación masiva previa a la actualización del maestro de empleados.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'TABLE', @level1name=N'ActualizacionNomina';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'TABLE', @level1name=N'ActualizacionNomina';
GO

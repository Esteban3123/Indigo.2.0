CREATE TABLE [Payroll].[PersonasNuevas] (
    [NumeroIdentificacion]  VARCHAR (20) NULL,
    [CodigoUnidadFuncional] VARCHAR (50) NULL,
    [CodigoCentroDeCosto]   VARCHAR (50) NULL,
    [Id_Banco]              INT          NULL,
    [CodigoBanco]           VARCHAR (20) NULL,
    [NumeroCuentaBancaria]  VARCHAR (70) NULL,
    [TipoCuenta]            NCHAR (10)   NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de carga o staging en el esquema de nómina que almacena datos de nuevas personas a incorporar al sistema de pagos. Registra su identificación, unidad funcional, centro de costo y la información bancaria necesaria para el pago (banco, número y tipo de cuenta). Su estructura sin llaves primarias ni foráneas sugiere uso como tabla transitoria de importación o integración.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'TABLE', @level1name=N'PersonasNuevas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'TABLE', @level1name=N'PersonasNuevas';
GO

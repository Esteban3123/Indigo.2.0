CREATE TABLE [Payroll].[ContratosNomina] (
    [Cedula]                        VARCHAR (20)   NULL,
    [IdEmpleado]                    INT            NULL,
    [TipoContrato]                  INT            NULL,
    [CodigoCargo]                   VARCHAR (15)   NULL,
    [IdCargo]                       INT            NULL,
    [CodigoUnidadFuncional]         VARCHAR (15)   NULL,
    [IdUnidadFuncional]             INT            NULL,
    [CodigoTipoContrato]            VARCHAR (15)   NULL,
    [IdTipoContrato]                INT            NULL,
    [FechaContratacion]             VARCHAR (15)   NULL,
    [FechaInicioContrato]           VARCHAR (15)   NULL,
    [FechaFinContrato]              VARCHAR (15)   NULL,
    [SalarioBasico]                 NUMERIC (18)   NULL,
    [PeriododePago]                 NVARCHAR (255) NULL,
    [FormadePago]                   NVARCHAR (255) NULL,
    [PeriodoPrueba]                 INT            NULL,
    [DiasPeriodoPrueba]             INT            NULL,
    [PorcentajeSueldoPeriodoPrueba] INT            NULL,
    [TipoCotizantePension]          INT            NULL,
    [CodigoGrupo]                   VARCHAR (15)   NULL,
    [IdGrupo]                       INT            NULL,
    [CodigoBanco]                   VARCHAR (15)   NULL,
    [IdBanco]                       INT            NULL,
    [NumeroCuentaBancaria]          VARCHAR (20)   NULL,
    [TipoCuentaBancaria]            INT            NULL,
    [HorasDiarias]                  INT            NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de staging o carga masiva que almacena contratos laborales de empleados para su procesamiento en nómina. Registra datos del contrato como tipo, cargo, unidad funcional, fechas de contratación e inicio/fin, salario básico, período y forma de pago, así como información bancaria para dispersión de pagos. Incluye parámetros del período de prueba y tipo de cotizante para pensión, sugiriendo integración con procesos de seguridad social y liquidación salarial.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'TABLE', @level1name=N'ContratosNomina';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'TABLE', @level1name=N'ContratosNomina';
GO

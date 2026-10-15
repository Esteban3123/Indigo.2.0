CREATE TABLE [Payroll].[Empleados] (
    [Cedula]                         VARCHAR (MAX)  NULL,
    [FechaAdmision]                  VARCHAR (50)   NULL,
    [PorcentajeRiesgosProfesionales] DECIMAL (8, 4) NULL,
    [Pensionado]                     INT            NULL,
    [CodigoTipoDeEmpleado]           VARCHAR (10)   NULL,
    [CodeEmployeeType]               INT            NULL,
    [CodigoCentroCosto]              VARCHAR (15)   NULL,
    [CodeCostCenter]                 INT            NULL,
    [CodigoCentroTrabajo]            VARCHAR (10)   NULL,
    [CodeWorkCenter]                 INT            NULL,
    [DeduccionVivienda]              DECIMAL (18)   NULL,
    [DeduccionEducacion]             DECIMAL (18)   NULL,
    [ProcedimientoRetencionFuente]   TINYINT        NULL,
    [FechaUltimasVacaciones]         DATE           NULL
);


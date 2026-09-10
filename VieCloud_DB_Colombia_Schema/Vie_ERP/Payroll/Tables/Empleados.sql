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
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla del módulo de nómina que almacena información laboral y tributaria de los empleados. Registra datos de vinculación como fecha de admisión, porcentaje de riesgos profesionales y estado de pensionado, junto con clasificaciones de tipo de empleado, centro de costo y centro de trabajo en sus variantes de código alfanumérico e identificador entero. Incluye deducciones de vivienda y educación, el procedimiento de retención en la fuente aplicable y la fecha de últimas vacaciones.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'TABLE', @level1name=N'Empleados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'TABLE', @level1name=N'Empleados';
GO

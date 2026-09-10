CREATE TABLE [dbo].[Empleado] (
    [Cedula]             VARCHAR (50)    NULL,
    [FechaAdmision]      DATE            NULL,
    [PorcentajeRiesgos]  DECIMAL (18, 4) NULL,
    [Pensionado]         BIT             NULL,
    [CodigoTipoEmpleado] VARCHAR (10)    NULL,
    [CentroCosto]        VARCHAR (10)    NULL,
    [CentroTrabajo]      VARCHAR (10)    NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla que almacena información laboral de empleados, identificados por su cédula, incluyendo fecha de ingreso, porcentaje de riesgo laboral, estado de pensión y clasificación por tipo de empleado, centro de costo y centro de trabajo. No posee clave primaria definida ni restricciones de integridad referencial explícitas en su DDL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Empleado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Empleado';
GO

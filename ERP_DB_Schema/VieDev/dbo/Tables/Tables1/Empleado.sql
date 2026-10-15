CREATE TABLE [dbo].[Empleado] (
    [Cedula]             VARCHAR (50)    NULL,
    [FechaAdmision]      DATE            NULL,
    [PorcentajeRiesgos]  DECIMAL (18, 4) NULL,
    [Pensionado]         BIT             NULL,
    [CodigoTipoEmpleado] VARCHAR (10)    NULL,
    [CentroCosto]        VARCHAR (10)    NULL,
    [CentroTrabajo]      VARCHAR (10)    NULL
);


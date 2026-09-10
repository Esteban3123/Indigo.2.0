CREATE TABLE [dbo].[SubirEmpledos] (
    [Cedula]               VARCHAR (50)   NULL,
    [FechaAdmision]        DATE           NULL,
    [RiesgosProfesionales] DECIMAL (8, 4) NULL,
    [Pensionado]           BIT            NULL,
    [CodigoTipoEmpleado]   VARCHAR (20)   NULL,
    [CodigoCentroCosto]    VARCHAR (50)   NULL,
    [CodigoCentroTrabajo]  VARCHAR (10)   NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de carga o staging para importar registros de empleados, almacenando datos básicos como cédula de identidad, fecha de admisión, tarifa de riesgos profesionales, estado de pensión y códigos de clasificación (tipo de empleado, centro de costo y centro de trabajo). Al no tener restricciones ni llaves foráneas, funciona como zona temporal previa a la inserción en tablas definitivas del módulo de nómina o recursos humanos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SubirEmpledos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SubirEmpledos';
GO

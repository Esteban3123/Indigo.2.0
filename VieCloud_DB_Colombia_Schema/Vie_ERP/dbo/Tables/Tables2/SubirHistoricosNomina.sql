CREATE TABLE [dbo].[SubirHistoricosNomina] (
    [Cedula]                 VARCHAR (20) NULL,
    [DiasTrabajados]         INT          NULL,
    [TotalDevengado]         NUMERIC (18) NULL,
    [TotalDeducido]          NUMERIC (18) NULL,
    [TotalPagado]            NUMERIC (18) NULL,
    [BasePension]            NUMERIC (18) NULL,
    [AportePensionEmpleado]  NUMERIC (18) NULL,
    [AportePensionPatrono]   NUMERIC (18) NULL,
    [BaseSalud]              NUMERIC (18) NULL,
    [AporteSaludEmpleado]    NUMERIC (18) NULL,
    [AporteSaludPatrono]     NUMERIC (18) NULL,
    [BasePrimas]             NUMERIC (18) NULL,
    [ProvisionPrimas]        NUMERIC (18) NULL,
    [BaseVacaciones]         NUMERIC (18) NULL,
    [ProvisionVacaciones]    NUMERIC (18) NULL,
    [BaseCesantias]          NUMERIC (18) NULL,
    [ProvisionCesantias]     NUMERIC (18) NULL,
    [ProvisionIntCesantias]  NUMERIC (18) NULL,
    [DiasProvision]          INT          NULL,
    [ValorIncAmbulatoria]    NUMERIC (18) NULL,
    [ValorIncHospitalaria]   NUMERIC (18) NULL,
    [ValorLicMaternidad]     NUMERIC (18) NULL,
    [BaseSena]               NUMERIC (18) NULL,
    [AporteSena]             NUMERIC (18) NULL,
    [BaseICBF]               NUMERIC (18) NULL,
    [AporteICBF]             NUMERIC (18) NULL,
    [BaseCajaCompensacion]   NUMERIC (18) NULL,
    [AporteCajaCompensacion] NUMERIC (18) NULL,
    [BaseRetencion]          NUMERIC (18) NULL,
    [ValorRetencion]         NUMERIC (18) NULL,
    [PromedioSaludAnterior]  NUMERIC (18) NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de carga o staging utilizada para importar registros históricos de nómina por empleado (identificado por cédula). Almacena los totales devengados, deducidos y pagados, junto con bases y aportes de seguridad social (salud, pensión), parafiscales (SENA, ICBF, caja de compensación), provisiones laborales (primas, vacaciones, cesantías e intereses) e incapacidades y retención en la fuente, siguiendo la estructura de nómina colombiana.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SubirHistoricosNomina';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SubirHistoricosNomina';
GO

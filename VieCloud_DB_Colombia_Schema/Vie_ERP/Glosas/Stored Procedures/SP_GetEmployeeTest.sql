
CREATE PROCEDURE [Glosas].[SP_GetEmployeeTest]
AS
BEGIN
	SELECT TOP 100 * FROM Payroll.Employee 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de prueba que retorna los primeros 100 empleados registrados en la nómina. Consulta el maestro de empleados con su información laboral, contractual y de afiliaciones (salud, pensión, riesgos profesionales). Es un SP de desarrollo o diagnóstico, no de producción, usado para verificar datos del módulo de Nómina desde el contexto del módulo de Glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GetEmployeeTest';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GetEmployeeTest';
-- GO

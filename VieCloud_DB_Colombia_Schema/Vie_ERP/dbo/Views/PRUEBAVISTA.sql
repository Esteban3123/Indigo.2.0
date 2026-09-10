CREATE VIEW [dbo].[PRUEBAVISTA] 
AS 
SELECT TOP 10 * FROM Payroll.Employee
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de prueba que expone los primeros 10 registros de la tabla maestra de empleados de nómina. No aplica filtros ni transformaciones adicionales, por lo que su uso aparenta ser exploratorio o de desarrollo, sin finalidad productiva definida.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'PRUEBAVISTA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'PRUEBAVISTA';
GO

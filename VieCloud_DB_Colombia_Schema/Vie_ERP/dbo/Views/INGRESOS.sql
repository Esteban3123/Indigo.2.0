
CREATE VIEW [dbo].[INGRESOS]
AS
SELECT        NUMINGRES, IFECHAING, IESTADOIN
FROM            dbo.ADINGRESO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista simplificada que expone los datos esenciales de los ingresos o admisiones de pacientes registrados en la tabla ADINGRESO. Muestra el número de ingreso, la fecha de ingreso y el estado del ingreso (activo, cerrado, etc.) para cada episodio de atención, independientemente de la modalidad (urgencias, hospitalización, consulta externa). Sirve como acceso directo y ligero a la información básica de ingresos para consultas de reportería, integraciones o procesos que solo requieren identificar si un paciente está o estuvo ingresado y en qué condición.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'INGRESOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'INGRESOS';
GO

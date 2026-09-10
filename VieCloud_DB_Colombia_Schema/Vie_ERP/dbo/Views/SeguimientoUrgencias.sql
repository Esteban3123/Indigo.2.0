
CREATE VIEW [dbo].[SeguimientoUrgencias]
AS
SELECT        A.NUMINGRES AS Ingreso, A.IPCODPACI AS CodigoPaciente, P.IPNOMCOMP AS nombrepaciente, A.IFECHAING AS FechaIngreso, A.IESTADOIN AS ESTADO, 
                         A.UFUCODIGO AS UnidadFuncional
FROM            dbo.ADINGRESO AS A INNER JOIN
                         dbo.INPACIENT AS P ON A.IPCODPACI = P.IPCODPACI
WHERE        (A.IFECHAING > '22/04/2014 14:00:00') AND (A.UFUCODIGO IN ('001', '043', '002', '003')) AND (A.IESTADOIN IN ('', 'F'))
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Seguimiento en tiempo real de pacientes activos en el servicio de urgencias. Combina los datos de ingresos (ADINGRESO) con la información personal del paciente (INPACIENT) para mostrar, por cada episodio de atención en urgencias, el número de ingreso, la cédula o código del paciente, su nombre completo, la fecha y hora de ingreso, el estado del ingreso y la unidad funcional asignada. Filtra únicamente los ingresos registrados a partir de abril de 2014 en las unidades funcionales correspondientes a urgencias (códigos 001, 002, 003 y 043) y con estados activos o en proceso de finalización. Sirve para monitoreo operativo de urgencias, tableros de control asistencial y reportes de ocupación en salas de urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'SeguimientoUrgencias';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'SeguimientoUrgencias';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los ingresos de pacientes en unidades de urgencias para seguimiento operativo, filtrando por fecha de corte, unidades funcionales específicas y estados activos o finalizados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'SeguimientoUrgencias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de ingresos en ADINGRESO con paciente asociado en INPACIENT (INNER JOIN por IPCODPACI).; Las unidades funcionales ''001'', ''043'', ''002'', ''003'' deben corresponder a áreas de urgencias en el catálogo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'SeguimientoUrgencias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Excluye ingresos anteriores al 22/04/2014 14:00:00 (fecha de corte fija).; Solo considera ingresos en cuatro unidades funcionales predefinidas asociadas a urgencias.; Solo muestra ingresos con estado vacío ('''') o ''F'' (finalizado), excluyendo otros estados como cancelados o anulados.; Excluye ingresos sin paciente registrado en el maestro INPACIENT (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'SeguimientoUrgencias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso; Paciente; Urgencias; Unidad funcional; Estado del ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'SeguimientoUrgencias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADINGRESO: Solo retorna ingresos cuya IFECHAING > ''22/04/2014 14:00:00'', UFUCODIGO IN (''001'',''043'',''002'',''003'') y IESTADOIN IN ('''',''F'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'SeguimientoUrgencias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'SeguimientoUrgencias';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'SeguimientoUrgencias';
GO

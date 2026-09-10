
CREATE PROCEDURE [dbo].[ESE_SP_Asistencial_Hoja_venopuncion]
@FechaIni DateTime,
@FechaFin DateTime

AS
SELECT  VEN.IPCODPACI,PAC.IPPRIAPEL,PAC.IPSEGAPEL,PAC.IPPRINOMB,PAC.IPSEGNOMB , VEN.NUMINGRES,VEN.CODCENATE,CA.NOMCENATE,
        VEN.UFUCODIGO, UNI.UFUDESCRI, VEN.CODPROSAL, PRO.NOMMEDICO,VEN.NUMCATETE, VEN.NOMCATETE,VEN.NOMVENUTI , VEN.FECHAINIC,
		CASE WHEN  VEN.EXTREMIDA = 1 THEN 'superior derecha'
		     WHEN  VEN.EXTREMIDA = 2 THEN 'superior izuierda'
			 WHEN  VEN.EXTREMIDA = 3 THEN 'Inferior derecha'
			 WHEN  VEN.EXTREMIDA = 4 THEN 'Inferior izquierda'
		END as 'Extremidad', 
		VEN.ESTACTIVA as 'vena activa', VEN.MOTITERMI

FROM HCCTRVENP VEN
     
INNER JOIN ADCENATEN CA ON VEN.CODCENATE=CA.CODCENATE
INNER JOIN INPACIENT PAC ON VEN.IPCODPACI =PAC.IPCODPACI
INNER JOIN INUNIFUNC UNI ON VEN.UFUCODIGO = UNI.UFUCODIGO
INNER JOIN INPROFSAL PRO ON VEN.CODPROSAL = PRO.CODPROSAL

WHERE VEN.FECHAINIC BETWEEN @FechaIni AND @FechaFin
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de la hoja de venopunción (canalización de venas) para un rango de fechas determinado. Consolida información del paciente (cédula, nombre completo), del ingreso hospitalario, del centro de atención, la unidad funcional, el profesional de salud responsable, y los datos propios del catéter venoso: número, nombre, sitio de venopunción, fecha de inicio, extremidad canalizada (superior/inferior derecha/izquierda) y estado activo o motivo de terminación. Se utiliza para auditoría y seguimiento clínico de accesos vasculares periféricos instalados en pacientes hospitalizados o en urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_Hoja_venopuncion';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_Hoja_venopuncion';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los registros de venopunciones (catéteres venosos) realizadas dentro de un rango de fechas, enriqueciendo con datos de paciente, centro de atención, unidad funcional y profesional de la salud.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Hoja_venopuncion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las venopunciones deben tener centro de atención, paciente, unidad funcional y profesional asociados existentes (INNER JOIN obliga correspondencia).; Se requiere un rango de fechas (inicio y fin) para filtrar por la fecha de inicio de la venopunción.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Hoja_venopuncion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se devuelven venopunciones cuya FECHAINIC cae dentro del rango parametrizado.; Sólo se incluyen venopunciones con relaciones íntegras hacia centro de atención, paciente, unidad funcional y profesional (INNER JOIN).; La extremidad se codifica en cuatro valores: 1=superior derecha, 2=superior izquierda, 3=inferior derecha, 4=inferior izquierda.; El procedimiento es de solo lectura; no modifica datos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Hoja_venopuncion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Venopunción; Catéter venoso; Paciente; Centro de atención; Unidad funcional; Profesional de la salud; Ingreso (NUMINGRES); Extremidad de punción; Estado activo de la vena; Motivo de terminación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Hoja_venopuncion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCCTRVENP: Cuando FECHAINIC está entre el rango indicado, se retorna el detalle de la venopunción junto con datos relacionados de paciente, centro, unidad y profesional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Hoja_venopuncion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si VEN.EXTREMIDA = 1 → Se etiqueta la extremidad como ''superior derecha''; si VEN.EXTREMIDA = 2 → Se etiqueta la extremidad como ''superior izuierda'' (sic); si VEN.EXTREMIDA = 3 → Se etiqueta la extremidad como ''Inferior derecha''; si VEN.EXTREMIDA = 4 → Se etiqueta la extremidad como ''Inferior izquierda'' else Extremidad queda en NULL si el código no está entre 1 y 4', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Hoja_venopuncion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCCTRVENP; dbo.ADCENATEN; dbo.INPACIENT; dbo.INUNIFUNC; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Hoja_venopuncion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Hoja_venopuncion';
-- GO

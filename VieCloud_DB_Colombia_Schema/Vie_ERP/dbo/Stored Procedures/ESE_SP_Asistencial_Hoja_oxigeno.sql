
CREATE PROCEDURE [dbo].[ESE_SP_Asistencial_Hoja_oxigeno]
@FechaIni DateTime,
@FechaFin DateTime

AS
SELECT  OXI.IPCODPACI,PAC.IPPRIAPEL,PAC.IPSEGAPEL,PAC.IPPRINOMB,PAC.IPSEGNOMB , OXI.NUMINGRES,OXI.CODCENATE,CA.NOMCENATE,
        OXI.UFUCODIGO, UNI.UFUDESCRI, OXI.CODPROSAL, PRO.NOMMEDICO, OXI.LITRXMINUT,OXI.TOTHORAS, OXI.TOTLITADM , OXI.FECHAREGI,
		CASE WHEN  oxi.CODVIAADM = 001 THEN 'Canula Nasal'
		     WHEN  oxi.CODVIAADM = 002 THEN 'Ventury 24-28'
			 WHEN  oxi.CODVIAADM = 003 THEN 'Ventury 31-35'
			 WHEN  oxi.CODVIAADM = 004 THEN 'Ventury 40-50'
			 WHEN  oxi.CODVIAADM = 005 THEN 'Tubo oratraqueal'
		END as 'Via administracion', oxi.CODVIAADM
		

FROM HCCONOXIG OXI
     
INNER JOIN ADCENATEN CA  ON OXI.CODCENATE = CA.CODCENATE
INNER JOIN INPACIENT PAC ON OXI.IPCODPACI = PAC.IPCODPACI
INNER JOIN INUNIFUNC UNI ON OXI.UFUCODIGO = UNI.UFUCODIGO
INNER JOIN INPROFSAL PRO ON OXI.CODPROSAL = PRO.CODPROSAL

WHERE OXI.FECHAREGI BETWEEN @FechaIni AND @FechaFin
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de hoja de oxigenoterapia para un rango de fechas determinado, mostrando cada sesión de administración de oxígeno u otros gases medicinales aplicada a pacientes hospitalizados. Combina información del registro de oxigenoterapia (HCCONOXIG) con los datos del paciente (nombre completo, cédula), el número de ingreso, la sede de atención, la unidad funcional (sala o servicio), el profesional de salud responsable, y los parámetros clínicos como litros por minuto, total de horas administradas y volumen total de litros entregados. Traduce el código de vía de administración a su descripción legible (cánula nasal, máscara Venturi en sus distintos rangos, tubo orotraqueal). Se utiliza para auditoría clínica, seguimiento de consumo de gases medicinales y trazabilidad asistencial de pacientes en oxigenoterapia dentro del período consultado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_Hoja_oxigeno';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_Hoja_oxigeno';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los registros de administración de oxígeno a pacientes en un rango de fechas, enriquecidos con datos del paciente, centro de atención, unidad funcional, profesional y vía de administración.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Hoja_oxigeno';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas de registro debe estar definido (FechaIni y FechaFin); Cada consumo de oxígeno debe tener centro de atención, paciente, unidad funcional y profesional de salud existentes (INNER JOIN obliga correspondencia)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Hoja_oxigeno';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen consumos con paciente, centro, unidad funcional y profesional válidos por los INNER JOIN; Las vías de administración válidas presentadas son códigos 001 a 005; otros códigos quedan sin descripción; El filtro temporal aplica sobre la fecha de registro del consumo, no sobre fechas de ingreso u otras', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Hoja_oxigeno';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Administración de oxígeno; Paciente; Ingreso; Centro de atención; Unidad funcional; Profesional de salud; Vía de administración (cánula nasal, Ventury, tubo orotraqueal); Litros por minuto; Total de horas y litros administrados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Hoja_oxigeno';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCCONOXIG: Devuelve registros de consumo de oxígeno cuya FECHAREGI esté entre @FechaIni y @FechaFin', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Hoja_oxigeno';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CODVIAADM = 001 → Etiqueta vía de administración como ''Canula Nasal''; si CODVIAADM = 002 → Etiqueta vía de administración como ''Ventury 24-28''; si CODVIAADM = 003 → Etiqueta vía de administración como ''Ventury 31-35''; si CODVIAADM = 004 → Etiqueta vía de administración como ''Ventury 40-50''; si CODVIAADM = 005 → Etiqueta vía de administración como ''Tubo oratraqueal''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Hoja_oxigeno';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCCONOXIG; dbo.ADCENATEN; dbo.INPACIENT; dbo.INUNIFUNC; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Hoja_oxigeno';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Hoja_oxigeno';
-- GO

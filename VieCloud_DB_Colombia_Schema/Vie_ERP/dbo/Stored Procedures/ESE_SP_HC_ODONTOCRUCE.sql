

CREATE PROCEDURE [dbo].[ESE_SP_HC_ODONTOCRUCE]
@FechaIni DateTime,
@FechaFin DateTime
AS
BEGIN

select TIPCITMED, case TIPCITMED when 1 then 'Primera vez' when 2 then 'Control' end as 'Tipo Cita', A.FECHISPAC,  * from HCHISPACA A 
INNER JOIN HCURGING1  B ON A.NUMEFOLIO = B.NUMEFOLIO AND A.IPCODPACI  = B.IPCODPACI AND A.NUMINGRES = B.NUMINGRES 
WHERE A.IDMODELOHC IN (28,33) AND A.FECHISPAC BETWEEN @FechaIni and @FechaFin ORDER BY A.FECHISPAC ---Odontologia
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera un reporte de atenciones odontológicas registradas en la historia clínica, cruzando la nota clínica general (HCHISPACA) con la nota de urgencias (HCURGING1) para obtener únicamente los folios correspondientes a los modelos de historia clínica de odontología (modelos 28 y 33). Filtra los registros por un rango de fechas de atención ingresado como parámetro (@FechaIni, @FechaFin) y clasifica cada atención según el tipo de cita: primera vez o control. Se utiliza para auditoría, seguimiento y estadísticas de la producción odontológica en un período determinado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_HC_ODONTOCRUCE';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_HC_ODONTOCRUCE';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las atenciones odontológicas (modelos HC 28 y 33) cruzadas con su ingreso de urgencias en un rango de fechas, clasificando la cita como Primera vez o Control.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_ODONTOCRUCE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las historias clínicas deben tener ingreso correspondiente en la tabla de urgencias con misma combinación de folio, paciente e ingreso; El rango de fechas debe estar definido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_ODONTOCRUCE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan historias clínicas asociadas a los modelos de odontología (28 y 33); Solo se incluyen historias que tengan ingreso de urgencias coincidente (INNER JOIN); El resultado siempre se entrega ordenado ascendentemente por fecha de la historia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_ODONTOCRUCE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Odontología; Historia clínica; Tipo de cita (Primera vez/Control); Ingreso de urgencias; Paciente; Folio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_ODONTOCRUCE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCHISPACA: Devuelve registros cuyo IDMODELOHC ∈ (28,33) y FECHISPAC dentro del rango, unidos a HCURGING1 por folio+paciente+ingreso, ordenados por fecha de la historia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_ODONTOCRUCE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPCITMED = 1 → Etiqueta la cita como ''Primera vez'' else Si TIPCITMED = 2 etiqueta como ''Control''; otro valor queda NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_ODONTOCRUCE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.HCURGING1', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_ODONTOCRUCE';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_ODONTOCRUCE';
-- GO

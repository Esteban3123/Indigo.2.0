-- Stored Procedure

-- =============================================
-- Author:		Rafael Eduardo patiño Cabrera
-- Create date: 26/03/2019
-- Description:	retorna los programas RIAS a los que aplica un paciente y informacion si ha estado icrito (el ultimo regsitro de isncripcion)
-- =============================================
CREATE PROCEDURE [dbo].[SP_RIAS_GestionRiasXPaciente]
	@Identificacion Varchar(25)
AS
BEGIN
	

	SET NOCOUNT ON;
	declare @FechaNacimiento as datetime
	select  @FechaNacimiento = IPFECNACI from INPACIENT where IPCODPACI = @Identificacion	 
	declare @Edaddias as integer= datediff(DAY,@FechaNacimiento,[Common].[GETDATE]()) 

	SELECT RP.ID as IDRIASXPACIENTE, R.ID, R.CODPRO + ' - ' + R.NOMBRE as RIAS, IIF(RP.ESTADO is null, 'No Inscrito',case RP.ESTADO when 1 then 'No Inscrito' when 2 then 'Inscrito' when 3 then 'Egresado' END) as Estado, RP.FECHAINCRIPCION  as FechaInscripcion,
						isnull(A.DESMOTANU,'') as MotivoEgreso, isnull(RP.JUSTIFICACIONEGRE,'') as JustificacionEgreso 	
	FROM (
					select ID,
						CODPRO,
						NOMBRE,
						case TIPOEDAD when 0 then EDADMINIMA     --Dia
					   						when 1 then (EDADMINIMA * 30) --Meses a dias
											when 2 then (EDADMINIMA * 365)  --años a Dias
						End as EDADMINIMA_DIAS,
						case TIPOEDAD when 0 then EDADMAXIMA  + 1    --Dia
					   						when 1 then (EDADMAXIMA * 30) + 30 --Meses a dias
											when 2 then (EDADMAXIMA * 365) + 365 --años a Dias
						End as EDADMAXIMA_DIAS,
						case TIPOEDAD when 0 then 'Dias'
											when 1 then 'Meses'
											when 2 then 'Años'	END as UnidadRangoEdad,
						SEXO
						from RIAS 
			) as R left join RIASXPACIENTE RP on R.ID = RP.IDRIAS and RP.IPCODPACI = @Identificacion
			left join HCMOANULB A on A.CODMOTANU = RP.CODMOTIVOEGRE
	 WHERE @Edaddias BETWEEN R.EDADMINIMA_DIAS AND R.EDADMAXIMA_DIAS   

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y retorna los programas de Rutas Integrales de Atención en Salud (RIAS) que aplican a un paciente según su edad y sexo, junto con el estado de inscripción actual (No Inscrito, Inscrito o Egresado) en cada programa. Recibe la cédula o identificación del paciente, calcula su edad en días comparándola con los rangos definidos en el catálogo de RIAS, y cruza esa información con las inscripciones registradas en RIASXPACIENTE para mostrar la fecha de inscripción, el motivo de egreso y la justificación cuando corresponda. Es utilizado para la gestión y seguimiento de programas preventivos y de atención integral por paciente, permitiendo identificar en cuáles programas debe o puede estar enrolado y cuál es su situación actual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_RIAS_GestionRiasXPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_RIAS_GestionRiasXPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los programas RIAS aplicables a un paciente según su edad actual, junto con el último estado de inscripción (no inscrito/inscrito/egresado) y datos de egreso si existen.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_GestionRiasXPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el paciente en INPACIENT para poder calcular su edad (de lo contrario la fecha de nacimiento queda nula y el filtro por rango de edad no resolverá coincidencias).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_GestionRiasXPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La edad del paciente se calcula en días desde su fecha de nacimiento hasta la fecha actual del sistema.; Solo se devuelven RIAS cuya ventana de edad (convertida a días) contiene la edad actual del paciente.; Si el paciente nunca se ha inscrito al RIAS (sin registro en RIASXPACIENTE), su estado se asume ''No Inscrito''.; Los rangos máximos en meses/años se amplían un periodo (mes/año) para incluir el último periodo completo.; MotivoEgreso y JustificacionEgreso nunca son NULL en el resultado (se sustituyen por cadena vacía).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_GestionRiasXPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; RIAS (Rutas Integrales de Atención en Salud); Inscripción a programa; Egreso de programa; Motivo de anulación/egreso; Rango de edad por programa; Sexo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_GestionRiasXPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.RIAS: Devuelve un resultset con los programas RIAS donde la edad del paciente en días está entre EDADMINIMA_DIAS y EDADMAXIMA_DIAS, junto con su estado de inscripción y motivo/justificación de egreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_GestionRiasXPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPOEDAD = 0 (Días), 1 (Meses) o 2 (Años) → Convierte EDADMINIMA/EDADMAXIMA a días: días tal cual, meses*30 (+30 al máximo), o años*365 (+365 al máximo); si RP.ESTADO IS NULL o = 1 → Se reporta ''No Inscrito'' else ESTADO=2 → ''Inscrito''; ESTADO=3 → ''Egresado''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_GestionRiasXPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_GestionRiasXPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPACIENT; dbo.RIAS; dbo.RIASXPACIENTE; dbo.HCMOANULB', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_GestionRiasXPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_RIAS_GestionRiasXPaciente';
-- GO

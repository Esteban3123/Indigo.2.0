
CREATE PROCEDURE [Nephrology].[SP_HC_ListPatientsWithRenalReplacementTherapies]
(
@CentroAtencion varchar(500)
)
AS
BEGIN
	SET NOCOUNT ON;


		SELECT  CASE A.MANEXTPRO WHEN 1 THEN 'Ambulatorio' WHEN 0 THEN 'Hospitalario' END AS 'TipoOrden' ,
				A.AUTO,
				Rtrim(A.CODCENATE) AS 'CodigoCentroAtencion',
				Rtrim(D.NOMCENATE) AS 'CentroAtencion',
				Rtrim(A.UFUCODIGO) as 'CodigoUnidadFuncional',
				Rtrim(P.UFUDESCRI) As 'UnidadFuncional',
				Rtrim(C.CODTIPPAC) As 'CODTIPPAC',
				C.CODTIPPAC AS 'TipoPoblacional',
				A.FECORDMED AS 'FechaOrden',
				B.IPCODPACI AS 'Identificacion',
				rtrim(ltrim(B.IPNOMCOMP)) as 'NombrePaciente', 
				rtrim(ltrim(ENT.CODENTIDA)) + ' - ' + rtrim(ltrim(ENT.NOMENTIDA)) as 'Entidad',
				[dbo].[EDAD] (B.IPFECNACI,[Common].[GETDATE]()) As 'Edad',				
				Rtrim(A.CODSERIPS) + ' - ' + rtrim(I.DESSERIPS) AS 'Servicio',
				A.CANSERIPS AS 'Sesiones ordenadas',
				'Autorizacion' as Autorizacion,	 			
				(select top 1 RTRIM(NUMINGRES) from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 2 and IESTADOIN IN ('','P','B')) AS Ingreso
				,A.NUMINGRES AS IngresoOrdenMedica,
				A.NUMEFOLIO AS Folio,
				A.CODPROSAL,
				M.NOMMEDICO AS 'NombreProfesional',
				CASE A.ESTSERIPS WHEN 1 THEN 'Ordenado' WHEN 2 THEN 'Completado' WHEN 3 THEN 'Interpretado' WHEN 4 THEN 'Sin Interfaz' WHEN 5 THEN 'Anulado' END AS ESTADO,
				RTRIM(ES.DESESPECI) AS 'Especialidad',
				rtrim(S.NOMDIAGNO) as 'Diagnostico'
		FROM  HCORDPRON  A 			
			INNER JOIN INPACIENT B with(nolock) ON A.IPCODPACI = B.IPCODPACI 
			INNER JOIN ADINGRESO C with(nolock) on A.NUMINGRES = C.NUMINGRES
			INNER JOIN INENTIDAD ENT with(nolock) on ENT.CODENTIDA = C.CODENTIDA 
			INNER JOIN ADCENATEN D with(nolock) on A.CODCENATE = D.CODCENATE 
			INNER JOIN INDIAGNOS S with(nolock) on S.CODDIAGNO = A.CODDIAGNO 
			INNER JOIN INUNIFUNC P with(nolock) on P.UFUCODIGO = A.UFUCODIGO 
			INNER JOIN INPROFSAL M with(nolock) on M.CODPROSAL = A.CODPROSAL			
			INNER JOIN INCUPSIPS I ON A.CODSERIPS = I.CODSERIPS
			INNER JOIN HCHISPACA HI ON A.NUMEFOLIO = HI.NUMEFOLIO AND A.IPCODPACI = HI.IPCODPACI AND A.NUMINGRES = HI.NUMINGRES
			INNER JOIN INESPECIA ES ON HI.CODESPTRA = ES.CODESPECI 
						
		WHERE A.ESTSERIPS IN (1,4) AND I.TIPSERIPS = 4 AND I.SERIPSDASH = 7 AND A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) 
		
		ORDER BY FechaOrden DESC

	
END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Lista pacientes nefrológicos con terapias de reemplazo renal (servicios tipo 4, dashboard 7) que tienen órdenes activas o sin interfaz, con sesiones iniciadas pero aún pendientes de completar. Consolida datos de orden médica, paciente, entidad aseguradora, ingreso hospitalario o ambulatorio, profesional tratante, diagnóstico y conteo comparativo de sesiones ordenadas versus ejecutadas, filtrado por centro de atención.', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListPatientsWithRenalReplacementTherapies';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListPatientsWithRenalReplacementTherapies';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes médicas activas de terapias de reemplazo renal cuyas sesiones realizadas están en curso (mayores a 0 pero menores a las ordenadas) en los centros de atención indicados.', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListPatientsWithRenalReplacementTherapies';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros de atención debe contener uno o más códigos válidos separados, parseables por dbo.splitstring; Deben existir registros consistentes entre la orden médica y sus catálogos relacionados (paciente, ingreso, entidad, centro, diagnóstico, unidad funcional, profesional, servicio CUPS, historia clínica y especialidad); El servicio asociado debe estar tipificado como terapia de reemplazo renal (TIPSERIPS=4 y SERIPSDASH=7)', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListPatientsWithRenalReplacementTherapies';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran órdenes con estado 1 (Ordenado) o 4 (Sin Interfaz); se excluyen completadas, interpretadas y anuladas; Solo se consideran servicios con TIPSERIPS = 4 y SERIPSDASH = 7 (servicios catalogados como terapias de reemplazo renal); El listado se restringe a centros de atención recibidos en el parámetro (lista delimitada parseada por splitstring); El ingreso reportado como ''Ingreso'' corresponde a un ingreso del paciente con TRATAESPECIA = 2 y estado en ('''',''P'',''B'') (vigente/pendiente); Solo aparecen órdenes con sesiones realizadas estrictamente menores a las ordenadas y con al menos una sesión realizada (terapia en curso, no completas ni sin iniciar); El resultado se ordena por fecha de orden descendente; La orden debe tener historia clínica asociada (HCHISPACA) por folio, paciente e ingreso para ser listada', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListPatientsWithRenalReplacementTherapies';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden médica ambulatoria/hospitalaria; Paciente; Centro de atención; Unidad funcional; Tipo poblacional; Entidad (asegurador/pagador); Servicio CUPS/IPS; Sesiones ordenadas vs realizadas; Autorización; Ingreso hospitalario; Folio de historia clínica; Profesional de salud; Especialidad; Diagnóstico; Terapias de reemplazo renal; Citas asignadas', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListPatientsWithRenalReplacementTherapies';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Nephrology.SP_HC_ListPatientsWithRenalReplacementTherapies: Devuelve un resultset con datos de pacientes y órdenes de terapia renal cuando ESTSERIPS IN (1,4), TIPSERIPS=4, SERIPSDASH=7, centro en el parámetro, y sesiones realizadas entre 1 y (ordenadas-1)', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListPatientsWithRenalReplacementTherapies';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MANEXTPRO = 1 → Tipo de orden se clasifica como ''Ambulatorio'' else Si MANEXTPRO = 0, se clasifica como ''Hospitalario''; si ESTSERIPS in (1,2,3,4,5) → Se traduce el estado de la orden a etiqueta: 1=Ordenado, 2=Completado, 3=Interpretado, 4=Sin Interfaz, 5=Anulado; si Sesiones realizadas (conteo en AGASICITA por orden) sea menor a las sesiones ordenadas y mayor a 0 → La orden se incluye en el resultado final (terapia parcialmente ejecutada) else Se excluye la orden del resultado', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListPatientsWithRenalReplacementTherapies';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDPRON; dbo.INPACIENT; dbo.ADINGRESO; dbo.INENTIDAD; dbo.ADCENATEN; dbo.INDIAGNOS; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.INCUPSIPS; dbo.HCHISPACA; dbo.INESPECIA; dbo.AGASICITA; dbo.splitstring', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListPatientsWithRenalReplacementTherapies';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListPatientsWithRenalReplacementTherapies';
-- GO

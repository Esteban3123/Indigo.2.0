
CREATE PROCEDURE [Nephrology].[SP_HC_listNewOrdersRenalReplacementTherapies]
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
				A.CANSERIPS AS 'Sesiones',
				'Autorizacion' as Autorizacion	 			
				,Ingreso = (select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 2 and IESTADOIN IN ('','P','B')) 
				,A.NUMINGRES AS IngresoOrdenMedica,
				A.NUMEFOLIO AS Folio,
				A.CODPROSAL,
				M.NOMMEDICO AS 'NombreProfesional',
				CASE A.ESTSERIPS WHEN 1 THEN 'Ordenado' WHEN 2 THEN 'Completado' WHEN 3 THEN 'Interpretado' WHEN 4 THEN 'Sin Interfaz' WHEN 5 THEN 'Anulado' END AS ESTADO,
				RTRIM(ES.DESESPECI) AS 'Especialidad'
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
						
		WHERE A.ESTSERIPS IN (1,4) AND I.TIPSERIPS = 4 AND I.SERIPSDASH = 7 AND A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) ORDER BY A.FECORDMED DESC  
	
END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Recupera órdenes médicas nuevas de terapias de reemplazo renal (servicios CUPS de tipo 4, categoría dashboard 7) que aún no tienen cita asignada (`AG.IdHCORDPRON IS NULL`) y se encuentran en estado "Ordenado" o "Sin Interfaz". Filtra por uno o varios centros de atención recibidos como parámetro. Retorna información del paciente, profesional, unidad funcional, entidad aseguradora, número de sesiones y tipo de orden (ambulatorio u hospitalario), ordenado por fecha descendente.', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_listNewOrdersRenalReplacementTherapies';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_listNewOrdersRenalReplacementTherapies';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes médicas de terapias de reemplazo renal aún no agendadas (sin cita asignada) en estado Ordenado o Sin Interfaz, filtradas por uno o varios centros de atención.', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_listNewOrdersRenalReplacementTherapies';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros de atención debe contener uno o varios códigos separables por dbo.splitstring; Cada orden debe tener paciente, ingreso, entidad, centro, diagnóstico, unidad funcional, profesional, servicio CUPS, historia clínica y especialidad relacionados (joins INNER); Los servicios deben estar parametrizados como TIPSERIPS=4 y SERIPSDASH=7 para considerarse terapia de reemplazo renal', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_listNewOrdersRenalReplacementTherapies';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan órdenes en estado ''Ordenado'' (1) o ''Sin Interfaz'' (4); Solo se incluyen servicios cuyo TIPSERIPS = 4 y SERIPSDASH = 7 (terapias de reemplazo renal); Se excluyen órdenes que ya tienen una cita asignada en AGASICITA (IdHCORDPRON no nulo); El listado se restringe a los centros de atención recibidos en el parámetro; Los resultados se ordenan por fecha de orden descendente (más recientes primero); Solo se muestran órdenes que tengan historia clínica asociada (HCHISPACA por folio, paciente e ingreso)', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_listNewOrdersRenalReplacementTherapies';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Terapia de reemplazo renal; Orden médica; Paciente; Ingreso hospitalario; Centro de atención; Unidad funcional; Entidad (asegurador); Profesional de salud; Especialidad; Folio de historia clínica; Diagnóstico; Servicio CUPS; Tipo poblacional; Sesiones; Autorización; Cita asignada', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_listNewOrdersRenalReplacementTherapies';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando ESTSERIPS IN (1,4) AND TIPSERIPS=4 AND SERIPSDASH=7 AND no existe cita en AGASICITA AND el centro está en el parámetro, se retorna la orden con datos de paciente, ingreso, entidad, profesional, servicio y especialidad', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_listNewOrdersRenalReplacementTherapies';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MANEXTPRO = 1 → Se clasifica la orden como ''Ambulatorio'' else Si MANEXTPRO = 0 se clasifica como ''Hospitalario''; si ESTSERIPS = 1/2/3/4/5 → Se traduce a estado: Ordenado/Completado/Interpretado/Sin Interfaz/Anulado respectivamente; si ADINGRESO con TRATAESPECIA = 2 y IESTADOIN IN ('''',''P'',''B'') → Se toma el primer NUMINGRES como ingreso vigente del paciente (tratamiento especial)', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_listNewOrdersRenalReplacementTherapies';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDPRON; dbo.INPACIENT; dbo.ADINGRESO; dbo.INENTIDAD; dbo.ADCENATEN; dbo.INDIAGNOS; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.INCUPSIPS; dbo.HCHISPACA; dbo.INESPECIA; dbo.AGASICITA; dbo.splitstring; Common.GETDATE; dbo.EDAD', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_listNewOrdersRenalReplacementTherapies';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_listNewOrdersRenalReplacementTherapies';
-- GO

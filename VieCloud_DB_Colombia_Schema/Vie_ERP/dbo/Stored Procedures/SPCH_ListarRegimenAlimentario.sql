-- =============================================
-- Author:      Maria Rozo
-- Create Date: 01/03/2022
-- Description: SP Para listar consultas de Regimen Alimentario
-- =============================================
CREATE PROCEDURE [dbo].[SPCH_ListarRegimenAlimentario]
(
	@TipoUnidad Char(2),
	@CentroAtencion Char(10),
	@UnidadFuncional Char(10),
	@FechaInicial DateTime,
	@FechaFinal DateTime,
	@Comida Int
)
AS
	SET @FechaInicial = @FechaInicial+'00:00:00.000'
	SET @FechaFinal = @FechaFinal+'23:59:00.000'
BEGIN
    SET NOCOUNT ON
	SELECT
		A.CreationDate AS 'DateRequestDiet',
		RTRIM(B.UFUDESCRI) AS 'FunctionalUnit',
		RTRIM(C.DESCCAMAS) AS 'Bed',
		(CASE A.FoodKind
		WHEN 1 THEN  'Desayuno'
		WHEN 2 THEN 'Almuerzo'
		WHEN 3 THEN 'Cena'
		WHEN 4 THEN 'Complemento Mañana'
		WHEN 5 THEN 'Complemento Tarde'
		WHEN 6 THEN 'Otro'
		ELSE 'Indeterminado'
		END) as 'FoodKind',
		A.Id as 'DietPatientCode',
		dbo.ConsultarDietasEnfermeria(D.IPCODPACI, E.NUMINGRES,A.ID) As 'DietType',
		RTRIM(A.PhysicianObservation) AS 'PhysicianObservation',
		RTRIM(A.NurseObservation) AS 'NurseObservation',
		CAST(GETDATE() AS DATE) AS 'DatePrint',
		RTRIM(D.IPNOMCOMP) AS 'PatientName',
		RTRIM(A.PatientCode) AS 'PatientIdentification',
		dbo.TipoDocumento(D.IPTIPODOC) AS 'IDAcronym',
		CAST(D.IPFECNACI as DATE) AS 'DateBirth',
		RTRIM([dbo].[EDAD] (D.IPFECNACI,[Common].[GETDATE]())) As 'Age',
		A.DateDietOrder AS 'DateDietOrder',
		F.FECINIEST AS 'DateHospitalization',
		DATEDIFF("d",F.FECINIEST,[Common].[GETDATE]())+1 AS 'Days',
		F.FECINIEST AS 'AdmissionDate',
		RTRIM(COALESCE(NULLIF(H.NOMDIAGNO,''),'')) AS 'Diagnostic',
		RTRIM(G.ENTCONTAC) AS 'Entity',
		CAST(1 AS BIT) AS 'Print'
	FROM
		MedicalDiet.DietControlNursing A
		INNER JOIN dbo.INUNIFUNC B ON A.FunctionalUnit = B.UFUCODIGO
		INNER JOIN dbo.CHCAMASHO C ON A.CodeBed=C.CODICAMAS
		INNER JOIN dbo.INPACIENT D ON A.PatientCode = D.IPCODPACI
		INNER JOIN dbo.ADINGRESO E ON A.AdmissionNumber = E.NUMINGRES
		INNER JOIN dbo.CHREGESTA F ON E.NUMINGRES = F.NUMINGRES
		INNER JOIN dbo.INENTIDAD G ON E.CODENTIDA = G.CODENTIDA
		INNER JOIN dbo.INDIAGNOS AS H ON A.CodeDiagnostic = H.CODDIAGNO
	WHERE
		F.REGESTADO='1'
		AND A.AttentionCenter=@CentroAtencion
		AND A.FunctionalUnit=@UnidadFuncional
		AND A.CreationDate BETWEEN @FechaInicial AND @FechaFinal
		AND A.FoodKind LIKE (CASE WHEN @Comida <> 0 THEN CAST(@Comida AS CHAR(1)) ELSE '%' END)
	ORDER BY
		C.DESCCAMAS ASC
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el régimen alimentario de pacientes hospitalizados para un período, centro de atención, unidad funcional y tipo de comida específicos. Combina las órdenes de dieta registradas por enfermería (DietControlNursing) con datos del paciente (nombre, cédula, tipo de documento, fecha de nacimiento, edad), la cama y unidad funcional asignada, el número de ingreso y fecha de hospitalización, la entidad aseguradora o pagador, y el diagnóstico CIE-10 asociado. El tipo de comida se traduce a texto legible (Desayuno, Almuerzo, Cena, Complemento Mañana, Complemento Tarde, Otro) y se consulta el detalle del tipo de dieta mediante la función ConsultarDietasEnfermeria. Se utiliza para imprimir o visualizar el censo de dietas del día en una unidad de hospitalización, apoyando la gestión nutricional y la coordinación entre enfermería, cocina y el equipo médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarRegimenAlimentario';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarRegimenAlimentario';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de régimen alimentario activas de pacientes hospitalizados, filtradas por centro de atención, unidad funcional, rango de fechas y tipo de comida, con datos clínicos y administrativos para impresión.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarRegimenAlimentario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener un ingreso con estancia activa (REGESTADO=''1'') en CHREGESTA.; Deben existir relaciones válidas entre la orden de dieta y las tablas de unidad funcional, cama, paciente, ingreso, entidad y diagnóstico (todos INNER JOIN).; El rango de fechas se ajusta al día completo: FechaInicial a las 00:00 y FechaFinal a las 23:59.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarRegimenAlimentario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan órdenes de dieta de pacientes con estancia hospitalaria en estado activo (''1'').; La marca ''Print'' siempre se devuelve como 1 (BIT) para todas las filas.; Los días de estancia se calculan como DATEDIFF(d, FECINIEST, hoy) + 1.; El diagnóstico se devuelve vacío si es NULL o cadena vacía (COALESCE/NULLIF).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarRegimenAlimentario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Régimen alimentario / dieta hospitalaria; Orden de dieta por enfermería; Tipo de comida (desayuno, almuerzo, cena, complementos); Paciente hospitalizado; Ingreso/admisión; Cama hospitalaria; Unidad funcional; Centro de atención; Diagnóstico clínico; Entidad pagadora; Estancia hospitalaria activa; Edad del paciente; Días de hospitalización', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarRegimenAlimentario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MedicalDiet.DietControlNursing: Devuelve resultset de órdenes de dieta cuando F.REGESTADO=''1'' y la orden está dentro del rango de fechas, centro y unidad funcional indicados, ordenado por descripción de cama ascendente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarRegimenAlimentario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Comida <> 0 → Filtra por el tipo de comida indicado (FoodKind = @Comida) else No filtra por tipo de comida (acepta todos los valores con LIKE ''%''); si A.FoodKind según valor 1..6 → Traduce el código a etiqueta: 1=Desayuno, 2=Almuerzo, 3=Cena, 4=Complemento Mañana, 5=Complemento Tarde, 6=Otro else Etiqueta ''Indeterminado''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarRegimenAlimentario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ConsultarDietasEnfermeria; dbo.TipoDocumento; dbo.EDAD; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarRegimenAlimentario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MedicalDiet.DietControlNursing; dbo.INUNIFUNC; dbo.CHCAMASHO; dbo.INPACIENT; dbo.ADINGRESO; dbo.CHREGESTA; dbo.INENTIDAD; dbo.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarRegimenAlimentario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarRegimenAlimentario';
-- GO

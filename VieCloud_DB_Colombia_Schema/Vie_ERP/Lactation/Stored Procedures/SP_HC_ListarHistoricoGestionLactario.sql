
CREATE PROCEDURE [Lactation].[SP_HC_ListarHistoricoGestionLactario]
(
    @CentroAtencion VARCHAR(100),
    @FechaInicial datetime,
	@FechaFinal datetime,
	@Paciente VARCHAR(25)
)
AS
BEGIN
    SET NOCOUNT ON

	IF LEN(@Paciente) = 0
		BEGIN
		-- Consulta 1: FÓRMULA LÁCTEA (de HCPRESCRA)
			SELECT		
				A.Id,       
				'FORMULA LACTEA' AS 'TipoComponente',
				A.FECINIDOS AS 'FechaOrden',
				A.NUMINGRES AS 'Ingreso',
				A.IPCODPACI AS 'CodPaciente',
				CONCAT(A.IPCODPACI, ' - ', RTRIM(P.IPNOMCOMP)) AS 'Paciente',
				A.CODPROSAL AS 'CodProfesional',
				R.CODESPECI AS 'CodEspecialidad',
				CONCAT(RTRIM(PR.NOMMEDICO), ' - ', RTRIM(K.DESESPECI)) AS 'Profesional',				
				A.CODCENATE AS 'CodCentroAtencion',
				A.UFUCODIGO AS 'CodUnidadFuncional',
				CONCAT(RTRIM(CA.NOMCENATE), ' - ', RTRIM(U.UFUDESCRI)) AS 'Ubicacion',
				RTRIM(C.DESPRODUC) AS 'ComponenteLacteo',
				CASE A.StatusFormulaSairy WHEN 5 THEN 'Preparación terminada' WHEN 4 THEN 'Anulado' ELSE '' END AS 'EstadoLabel',
				A.StatusFormulaSairy AS 'ESTADO',
				A.DairyComponentCancellationCode AS 'CodMotivoAnulacion', 
				RTRIM(B.DESMOTANU) AS 'MotivoAnulacion', 
				A.DairyComponentCancellationJustification AS 'JustificacionAnulacion', 
				P.IPFECNACI AS 'FechaNacimiento'			
			FROM HCPRESCRA A 
			INNER JOIN ADINGRESO E WITH(NOLOCK) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES 
			INNER JOIN IHLISTPRO C WITH(NOLOCK) ON A.CODPRODUC = C.CODPRODUC 
			INNER JOIN INPACIENT P ON A.IPCODPACI = P.IPCODPACI
			LEFT OUTER JOIN CHREGESTA R WITH(NOLOCK) ON P.IPCODPACI=R.IPCODPACI AND R.REGESTADO = 1
			LEFT OUTER JOIN dbo.INESPECIA K with (nolock) ON R.CODESPECI = K.CODESPECI
			INNER JOIN INPROFSAL PR ON A.CODPROSAL = PR.CODPROSAL
			INNER JOIN INUNIFUNC U WITH(NOLOCK) ON A.UFUCODIGO = U.UFUCODIGO 
			INNER JOIN ADCENATEN CA WITH(NOLOCK) ON A.CODCENATE = CA.CODCENATE
			INNER JOIN HCHISPACA HISP ON HISP.NUMEFOLIO = A.NUMEFOLIO AND HISP.IPCODPACI = A.IPCODPACI AND A.NUMINGRES = HISP.NUMINGRES
			LEFT JOIN dbo.HCMOANULB B with (nolock) ON A.DairyComponentCancellationCode = B.CODMOTANU
			WHERE A.FormulaSairy = 1 
			AND A.StatusFormulaSairy IN (4,5) 
			AND A.CODCENATE IN (SELECT Value FROM dbo.splitstring(@CentroAtencion)) AND HISP.GENCONEXT = 0
			AND CAST(A.FECINIDOS AS DATE) BETWEEN CAST(@FechaInicial AS DATE) AND CAST(@FechaFinal AS DATE)

			UNION ALL

			-- Consulta 2: LECHE MATERNA (de BreastMilkOrder)
			SELECT 
				O.Id,
				'LECHE MATERNA' AS 'TipoComponente',
				O.OrderDate AS 'FechaOrden',
				O.NUMINGRES AS 'Ingreso',
				O.IPCODPACI AS 'CodPaciente',
				CONCAT(O.IPCODPACI, ' - ', RTRIM(P.IPNOMCOMP)) AS 'Paciente',
				O.ProfessionalCode AS 'CodProfesional',
				R.CODESPECI AS 'CodEspecialidad',
				CONCAT(RTRIM(PR.NOMMEDICO), ' - ', RTRIM(K.DESESPECI)) AS 'Profesional',
				O.CareCenterCode AS 'CodCentroAtencion', 
				O.FunctionalUnitCode AS 'CodUnidadFuncional',
				CONCAT(RTRIM(CA.NOMCENATE), ' - ', RTRIM(U.UFUDESCRI)) AS 'Ubicacion',
				RTRIM(CH.DESTIPDIE) AS 'ComponenteLacteo',
				CASE O.Status WHEN 5 THEN 'Preparación terminada' WHEN 4 THEN 'Anulado' ELSE '' END AS 'EstadoLabel',
				O.Status AS 'ESTADO',
				O.CancellationReason AS 'CodMotivoAnulacion', 
				RTRIM(B.DESMOTANU) AS 'MotivoAnulacion', 
				O.CancellationJustification AS 'JustificacionAnulacion', 
				P.IPFECNACI AS 'FechaNacimiento'
			FROM Lactation.BreastMilkOrder O
			INNER JOIN ADINGRESO E WITH(NOLOCK) ON O.IPCODPACI = E.IPCODPACI AND O.NUMINGRES = E.NUMINGRES 
			INNER JOIN INPACIENT P ON O.IPCODPACI = P.IPCODPACI
			LEFT OUTER JOIN CHREGESTA R WITH(NOLOCK) ON P.IPCODPACI=R.IPCODPACI AND R.REGESTADO = 1
			LEFT OUTER JOIN dbo.INESPECIA K with (nolock) ON R.CODESPECI = K.CODESPECI
			INNER JOIN INPROFSAL PR ON O.ProfessionalCode = PR.CODPROSAL
			INNER JOIN CHTIPDIET CH ON O.DietCode = CH.CODTIPDIE
			INNER JOIN ADCENATEN CA WITH(NOLOCK) ON O.CareCenterCode = CA.CODCENATE
			INNER JOIN INUNIFUNC U WITH(NOLOCK) ON O.FunctionalUnitCode = U.UFUCODIGO 
			INNER JOIN HCHISPACA HIS WITH(NOLOCK) ON  O.HCHISPACAId = HIS.Id
			LEFT JOIN dbo.HCMOANULB B with (nolock) ON O.CancellationReason = B.CODMOTANU
			WHERE 
			O.CareCenterCode IN (SELECT Value FROM dbo.splitstring(@CentroAtencion))
			AND O.Status IN (4,5) AND HIS.GENCONEXT = 0
			AND CAST(O.OrderDate AS DATE) BETWEEN CAST(@FechaInicial AS DATE) AND CAST(@FechaFinal AS DATE)

			ORDER BY FechaOrden DESC
		END
	ELSE
		BEGIN
		-- Consulta 1: FÓRMULA LÁCTEA (de HCPRESCRA)
			SELECT		
				A.Id,       
				'FORMULA LACTEA' AS 'TipoComponente',
				A.FECINIDOS AS 'FechaOrden',
				A.NUMINGRES AS 'Ingreso',
				A.IPCODPACI AS 'CodPaciente',
				CONCAT(A.IPCODPACI, ' - ', RTRIM(P.IPNOMCOMP)) AS 'Paciente',
				A.CODPROSAL AS 'CodProfesional',
				R.CODESPECI AS 'CodEspecialidad',
				CONCAT(RTRIM(PR.NOMMEDICO), ' - ', RTRIM(K.DESESPECI)) AS 'Profesional',				
				A.CODCENATE AS 'CodCentroAtencion',
				A.UFUCODIGO AS 'CodUnidadFuncional',
				CONCAT(RTRIM(CA.NOMCENATE), ' - ', RTRIM(U.UFUDESCRI)) AS 'Ubicacion',
				RTRIM(C.DESPRODUC) AS 'ComponenteLacteo',
				CASE A.StatusFormulaSairy WHEN 5 THEN 'Preparación terminada' WHEN 4 THEN 'Anulado' ELSE '' END AS 'EstadoLabel',
				A.StatusFormulaSairy AS 'ESTADO',
				A.DairyComponentCancellationCode AS 'CodMotivoAnulacion', 
				RTRIM(B.DESMOTANU) AS 'MotivoAnulacion', 
				A.DairyComponentCancellationJustification AS 'JustificacionAnulacion', 
				P.IPFECNACI AS 'FechaNacimiento'
			FROM HCPRESCRA A 
			INNER JOIN ADINGRESO E WITH(NOLOCK) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES 
			INNER JOIN IHLISTPRO C WITH(NOLOCK) ON A.CODPRODUC = C.CODPRODUC 
			INNER JOIN INPACIENT P ON A.IPCODPACI = P.IPCODPACI
			LEFT OUTER JOIN CHREGESTA R WITH(NOLOCK) ON P.IPCODPACI=R.IPCODPACI AND R.REGESTADO = 1
			LEFT OUTER JOIN dbo.INESPECIA K with (nolock) ON R.CODESPECI = K.CODESPECI
			INNER JOIN INPROFSAL PR ON A.CODPROSAL = PR.CODPROSAL
			INNER JOIN INUNIFUNC U WITH(NOLOCK) ON A.UFUCODIGO = U.UFUCODIGO 
			INNER JOIN ADCENATEN CA WITH(NOLOCK) ON A.CODCENATE = CA.CODCENATE
			INNER JOIN HCHISPACA HISP ON HISP.NUMEFOLIO = A.NUMEFOLIO AND HISP.IPCODPACI = A.IPCODPACI AND A.NUMINGRES = HISP.NUMINGRES
			LEFT JOIN dbo.HCMOANULB B with (nolock) ON A.DairyComponentCancellationCode = B.CODMOTANU
			WHERE A.FormulaSairy = 1 
			AND A.StatusFormulaSairy IN (4,5) 
			AND A.CODCENATE IN (SELECT Value FROM dbo.splitstring(@CentroAtencion)) AND HISP.GENCONEXT = 0
			AND A.IPCODPACI = @Paciente AND CAST(A.FECINIDOS AS DATE) BETWEEN CAST(@FechaInicial AS DATE) AND CAST(@FechaFinal AS DATE)

			UNION ALL

			-- Consulta 2: LECHE MATERNA (de BreastMilkOrder)
			SELECT 
				O.Id,
				'LECHE MATERNA' AS 'TipoComponente',
				O.OrderDate AS 'FechaOrden',
				O.NUMINGRES AS 'Ingreso',
				O.IPCODPACI AS 'CodPaciente',
				CONCAT(O.IPCODPACI, ' - ', RTRIM(P.IPNOMCOMP)) AS 'Paciente',
				O.ProfessionalCode AS 'CodProfesional',
				R.CODESPECI AS 'CodEspecialidad',
				CONCAT(RTRIM(PR.NOMMEDICO), ' - ', RTRIM(K.DESESPECI)) AS 'Profesional',				
				O.CareCenterCode AS 'CodCentroAtencion', 
				O.FunctionalUnitCode AS 'CodUnidadFuncional',
				CONCAT(RTRIM(CA.NOMCENATE), ' - ', RTRIM(U.UFUDESCRI)) AS 'Ubicacion',
				RTRIM(CH.DESTIPDIE) AS 'ComponenteLacteo',
				CASE O.Status WHEN 5 THEN 'Preparación terminada' WHEN 4 THEN 'Anulado' ELSE '' END AS 'EstadoLabel',
				O.Status AS 'ESTADO',
				O.CancellationReason AS 'CodMotivoAnulacion', 
				RTRIM(B.DESMOTANU) AS 'MotivoAnulacion', 
				O.CancellationJustification AS 'JustificacionAnulacion', 
				P.IPFECNACI AS 'FechaNacimiento'
			FROM Lactation.BreastMilkOrder O
			INNER JOIN ADINGRESO E WITH(NOLOCK) ON O.IPCODPACI = E.IPCODPACI AND O.NUMINGRES = E.NUMINGRES 
			INNER JOIN INPACIENT P ON O.IPCODPACI = P.IPCODPACI
			LEFT OUTER JOIN CHREGESTA R WITH(NOLOCK) ON P.IPCODPACI=R.IPCODPACI AND R.REGESTADO = 1
			LEFT OUTER JOIN dbo.INESPECIA K with (nolock) ON R.CODESPECI = K.CODESPECI
			INNER JOIN INPROFSAL PR ON O.ProfessionalCode = PR.CODPROSAL
			INNER JOIN CHTIPDIET CH ON O.DietCode = CH.CODTIPDIE
			INNER JOIN ADCENATEN CA WITH(NOLOCK) ON O.CareCenterCode = CA.CODCENATE
			INNER JOIN INUNIFUNC U WITH(NOLOCK) ON O.FunctionalUnitCode = U.UFUCODIGO 
			INNER JOIN HCHISPACA HIS WITH(NOLOCK) ON  O.HCHISPACAId = HIS.Id
			LEFT JOIN dbo.HCMOANULB B with (nolock) ON O.CancellationReason = B.CODMOTANU
			WHERE 
			O.CareCenterCode IN (SELECT Value FROM dbo.splitstring(@CentroAtencion))
			AND O.Status IN (4,5) AND HIS.GENCONEXT = 0
			AND O.IPCODPACI = @Paciente AND CAST(O.OrderDate AS DATE) BETWEEN CAST(@FechaInicial AS DATE) AND CAST(@FechaFinal AS DATE)

			ORDER BY FechaOrden DESC
		END	
	END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Retorna el historial de órdenes del lactario —fórmula láctea (HCPRESCRA) y leche materna (BreastMilkOrder)— con estado finalizado (5) o anulado (4), filtrando por centro de atención, rango de fechas y opcionalmente por paciente. Consolida mediante UNION ALL ambos tipos de componente lácteo, enriqueciendo cada registro con datos del paciente, profesional, especialidad, ubicación y motivo/justificación de anulación. Está orientado a reporting histórico de gestión del lactario para auditoría o seguimiento clínico.', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoGestionLactario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoGestionLactario';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el histórico de gestión del lactario combinando órdenes de fórmula láctea y de leche materna en estado ''Preparación terminada'' o ''Anulado'' para uno o varios centros de atención y un rango de fechas, opcionalmente filtrando por paciente.', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoGestionLactario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@CentroAtencion debe contener uno o más códigos de centro separados (procesables por dbo.splitstring); @FechaInicial y @FechaFinal deben definir un rango válido sobre la fecha de la orden; Las órdenes deben estar asociadas a una historia clínica (HCHISPACA) con GENCONEXT = 0 (no generada por interconsulta/externa); Para fórmula láctea, el registro en HCPRESCRA debe tener FormulaSairy = 1', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoGestionLactario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes con estado 4 (Anulado) o 5 (Preparación terminada); cualquier otro estado queda excluido; Solo se consideran órdenes asociadas a historias clínicas con GENCONEXT = 0; El filtro de centro de atención siempre se aplica vía dbo.splitstring sobre @CentroAtencion en ambas fuentes; El rango de fechas se compara a nivel de DATE (sin hora) sobre FECINIDOS u OrderDate; La especialidad se obtiene únicamente del registro CHREGESTA con REGESTADO = 1 (especialidad activa/vigente del paciente); Para fórmula láctea, solo se devuelven registros marcados como FormulaSairy = 1', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoGestionLactario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Lactario; Fórmula láctea; Leche materna; Orden de preparación; Anulación de orden; Motivo y justificación de anulación; Centro de atención; Unidad funcional; Especialidad médica; Historia clínica (folio); Paciente / ingreso hospitalario; Dieta', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoGestionLactario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Lactation.BreastMilkOrder + HCPRESCRA: Devuelve un resultset unificado (UNION ALL) etiquetando cada fila como ''FORMULA LACTEA'' (HCPRESCRA con FormulaSairy=1) o ''LECHE MATERNA'' (Lactation.BreastMilkOrder), ordenado por FechaOrden DESC', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoGestionLactario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si LEN(@Paciente) = 0 → Ejecuta la consulta unificada filtrando solo por centro de atención y rango de fechas, sin restringir paciente else Ejecuta la misma consulta unificada agregando el filtro IPCODPACI = @Paciente en ambas ramas del UNION; si StatusFormulaSairy / Status = 5 → Etiqueta EstadoLabel = ''Preparación terminada'' else Si = 4 etiqueta ''Anulado''; cualquier otro valor queda como cadena vacía', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoGestionLactario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoGestionLactario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCRA; dbo.ADINGRESO; dbo.IHLISTPRO; dbo.INPACIENT; dbo.CHREGESTA; dbo.INESPECIA; dbo.INPROFSAL; dbo.INUNIFUNC; dbo.ADCENATEN; dbo.HCHISPACA; dbo.HCMOANULB; Lactation.BreastMilkOrder; dbo.CHTIPDIET', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoGestionLactario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoGestionLactario';
-- GO

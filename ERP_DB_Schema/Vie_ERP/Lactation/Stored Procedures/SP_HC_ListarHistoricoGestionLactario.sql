
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
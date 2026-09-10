
CREATE view [Report].[UploadCubeVieClinicalMacroReport] as 

	WITH CTE_LISTADO AS
	(
		SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
			CASE pac.iptipodoc 
				WHEN 1 THEN 'CC'
				WHEN 2 THEN 'CE'
				WHEN 3 THEN 'TI'
				WHEN 4 THEN 'RC'
				WHEN 5 THEN 'PA'
				WHEN 6 THEN 'AS'
				WHEN 7 THEN 'MS'
				WHEN 8 THEN 'NU'
				WHEN 9 THEN 'CN'
				WHEN 10 THEN 'CD'
				WHEN 11 THEN 'SC' 
				WHEN 12 THEN 'PE' 
				WHEN 13 THEN 'PT'
				WHEN 14 THEN 'DE'
				WHEN 15 THEN 'SI' END AS 'TIPO IDENTIFICACION',--[TipoIdentificacion],
	
			A.IPCODPACI AS 'NRO IDENTIFICACION',--[NroIdentificacion],
			RTRIM(PAC.IPPRIAPEL) AS 'PRIMER APELLIDO',--[PrimerApellido],
			RTRIM(PAC.IPSEGAPEL) AS 'SEGUNDO APELLIDO',--[SegundoApellido], 
			RTRIM(PAC.IPPRINOMB) AS 'PRIMER NOMBRE',--[PrimerNombre] , 
			RTRIM(PAC.IPSEGNOMB) AS 'SEGUNDO NOMBRE',--[SegundoNombre],
			CASE PAC.IPSEXOPAC WHEN '1' THEN 'H' WHEN '2' THEN 'M' END AS 'CODIGO SEXO',--[CodigoSexo],
			CASE PAC.IPSEXOPAC WHEN '1' THEN 'Hombre' WHEN '2' THEN 'Mujer' END AS 'SEXO',--[Sexo],
			UPPER(PAC.IPDIRECCI) AS 'DIRECCION',--[Direccion], 
			PAC.IPTELMOVI AS 'TELEFONO PRINCIPAL',--[TelefonoPrincipal], 
			PAC.IPTELEFON AS 'TELEFONO ALTERNATIVO',--[TelefonoAlternativo],
			CASE 
				WHEN HEA.Id IS NOT NULL THEN RTRIM(HEA.Name) ELSE RTRIM(ENT.NOMENTIDA) END AS 'ENTIDAD',--[Entidad],
			CG.Name AS 'GRUPO ATENCION',--[GrupoAtencion],
			A.NUMAUTORI 'NRO AUTORIZACION',--[NroAutorizacion],

			GC.NUMINGRES 'NRO INGRESO',--[NroIngreso],
			RTRIM(C.NOMCENATE) AS 'CENTRO ATENCION',--[CentroAtencion],
			AGC.DESCRICON AS 'CONSULTORIO',--[Consultorio],
			CAST(A.FECHORAIN as date) 'FECHA CITA',--[FechaCita],
			CONVERT(varchar,CAST(A.FECHORAIN AS datetime),108) as 'HORA CITA',--[HoraCita],
			A.CODPROSAL AS 'IDENTIFICACION PROFESIONAL',--[IdentificacionProfesional],
			E.NOMMEDICO AS 'PROFESIONAL',--[Profesional],
			G.NOMUSUARI 'USUARIO ASIGNO',--[UsuarioAsigno],
			'ASIGNADA' AS 'ESTADO INICIAL',--[EstadoInicial],
			CASE 
				WHEN A.CODESTCIT = '0' THEN 'ASIGNADA' 
				WHEN A.CODESTCIT = '1' THEN 'CUMPLIDA' 
				WHEN A.CODESTCIT = '2' THEN 'INCUMPLIDA'
				WHEN A.CODESTCIT = '3' THEN 'PREASIGNADA' 
				WHEN A.CODESTCIT = '4' THEN 'CANCELADA' 
				WHEN A.CODESTCIT = '5' THEN 'CANCELADA' END AS 'ESTADO ACTUAL',--[EstadoActual],
			CASE WHEN  GC.NUMINGRES IS NULL THEN 'No' ELSE 'Si' END 'CITA LEGALIZADA',--[CitaLegalizada],
			ACT.CODSERIPS AS 'CUPS',--[CUPS], 
			CUPS.Description AS 'DESCRIPCION'--[DescripcionCUPS]

		FROM AGASICITA A with (nolock)
		INNER JOIN ADCENATEN AS C with (nolock) ON A.CODCENATE = C.CODCENATE
		INNER JOIN INPROFSAL AS E with (nolock) ON A.CODPROSAL = E.CODPROSAL
		INNER JOIN AGCONSULT AS AGC with (nolock) ON AGC.CODIGOCON =A.CODIGOCON AND AGC.CODCENATE =C.CODCENATE
		INNER JOIN INPACIENT AS PAC with (nolock) ON A.IPCODPACI = PAC.IPCODPACI
		INNER JOIN Contract.HealthAdministrator AS HEA with (nolock) ON A.GENCONENTITY = HEA.Id
		INNER JOIN INENTIDAD AS ENT with (nolock) ON PAC.CODENTIDA = ENT.CODENTIDA
		INNER JOIN SEGusuaru AS G with (nolock) ON A.CODUSUASI = G.CODUSUARI
		INNER JOIN DBO.AGACTIMED AS ACT ON ACT.CODACTMED =A.CODACTMED
		INNER JOIN Contract .CUPSEntity AS CUPS ON CUPS.Code =ACT.CODSERIPS
		LEFT JOIN Contract.CareGroup AS CG WITH (NOLOCK) ON CG.Id =PAC.GENCAREGROUP
		LEFT JOIN (SELECT CS1.* FROM ADCONCOEX  CS1 INNER JOIN (
		SELECT MAX(CS.CODCONCEC) CODCONCEC,CS.IPCODPACI, CS.IPFECHCIT FROM ADCONCOEX AS CS  with (nolock) 
		WHERE CS.CONESTADO <>'2' /*AND IPCODPACI LIKE '24822605' AND NUMCONCIT='201983'*/
		GROUP BY CS.IPCODPACI, CS.IPFECHCIT
		) CS2 ON CS1.IPCODPACI=CS2.IPCODPACI AND CS1.IPFECHCIT=CS2.IPFECHCIT AND CS1.CODCONCEC=CS2.CODCONCEC) AS GC ON GC.NUMCONCIT = A.CODAUTONU
		WHERE YEAR(A.FECHORAIN)>=2023 and A.TIPSOLICITU IN(1) --AND A.CODESTCIT = '0' 

	), CTE_FACTURACION AS
	(
		SELECT 
			AdmissionNumber AS INGRESO,
			InvoiceNumber AS NroFactura,
			CUPS.Code AS CODIGO_CUP,
			CUPS.Description CUPS,
			CDD.Code AS CODIGO_RELACIONADO,
			CDD.Name AS DESCRIPCION_RELACIONADA,
			F.InvoiceDate AS [FechaFactura],
			DF.TotalSalesPrice AS [ValorServicio],
			F.InvoiceValue AS 'VALOR FACTURA'
		FROM Billing.Invoice AS F
		LEFT JOIN Billing.InvoiceDetail AS DF WITH (NOLOCK) ON DF.InvoiceId = F.Id
		LEFT JOIN Billing.ServiceOrderDetail AS SOD WITH (NOLOCK) ON SOD.Id = DF.ServiceOrderDetailId
		LEFT JOIN Contract.CUPSEntity AS CUPS WITH (NOLOCK) ON CUPS.Id = SOD.CUPSEntityId
		LEFT JOIN Contract.CUPSEntityContractDescriptions AS CECD WITH (NOLOCK) ON CECD.ID=SOD.CUPSEntityContractDescriptionId
		LEFT JOIN Contract.ContractDescriptions AS CDD WITH (NOLOCK) ON CECD.ContractDescriptionId =CDD.Id
		LEFT JOIN CTE_LISTADO AS FIN ON FIN.[NRO INGRESO] = F.AdmissionNumber 
		WHERE F.Status =1 AND CUPS.ServiceType IN (6,8)
	)

	SELECT 
		LIS.*,
		nota.fechinihi AS 'FECHA INICIO HC',--[FechaInicioHC], 
		nota.fechfinh AS 'FECHA FIN HC',--[FechaFinHC], 
		nota.numefolio AS 'NRO FOLIO',--[NroFolio],
		FAC.NroFactura 'NRO FACTURA',
		FAC.FechaFactura 'FECHA FACTURA',
		FAC.ValorServicio 'VALOR SERVICIO',
		CAST(LIS.[FECHA CITA] AS DATE) 'FECHA BUSQUEDA',
	    CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL

	FROM CTE_LISTADO AS LIS
	LEFT JOIN CTE_FACTURACION AS FAC ON LIS.[NRO INGRESO] = FAC.INGRESO 
	LEFT JOIN dbo.hcurging1 AS nota ON lis.[NRO INGRESO] = nota.numingres AND lis.[NRO IDENTIFICACION] = nota.ipcodpaci AND lis.[IDENTIFICACION PROFESIONAL] = nota.codprosal
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de aplanado para reporting/BI (cubo) que consolida citas médicas ambulatorias del tipo solicitud 1 desde 2023, enriquecidas con datos demográficos del paciente, entidad pagadora, profesional, estado de la cita y código CUPS. Complementa cada cita con su factura asociada (número, fecha, valor de servicio) para servicios de tipo 6 u 8, y con datos de la historia clínica de urgencias (folios y fechas de apertura/cierre). Incluye marca de tiempo de última actualización en zona horaria Pakistan Standard Time.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMacroReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMacroReport';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida citas ambulatorias desde 2023 con datos demográficos del paciente, entidad responsable, profesional, estado de la cita, legalización (ingreso), CUPS asociados, facturación de consultas y notas de historia clínica para alimentar un cubo macro clínico.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMacroReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cita debe tener año de FECHORAIN >= 2023 y TIPSOLICITU = 1.; Toda cita debe tener centro de atención, profesional, consultorio, paciente, administradora de salud (HealthAdministrator), entidad del paciente, usuario asignador, actividad médica y CUPS asociados (INNER JOIN).; La actividad médica (AGACTIMED) debe tener un CODSERIPS que exista en Contract.CUPSEntity.; Para la sección de facturación, las facturas deben estar en estado 1 y el CUPS debe ser de ServiceType 6 u 8.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMacroReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El estado inicial reportado siempre es ''ASIGNADA'' independientemente del estado actual.; El campo ''FECHA BUSQUEDA'' siempre es la parte DATE de FECHORAIN.; La marca de última actualización siempre se calcula con GETDATE() trasladado a la zona horaria ''Pakistan Standard Time''.; ID_COMPANY siempre es el nombre de la base de datos truncado a 9 caracteres.; Solo se incluyen citas con TIPSOLICITU = 1 y año >= 2023.; La legalización de cita se determina por la existencia del ingreso máximo no anulado (CONESTADO<>''2'') vinculado al CODAUTONU de la cita.; Códigos CODESTCIT 4 y 5 se reportan ambos como ''CANCELADA''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMacroReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita ambulatoria; Paciente; Tipo de identificación; Administradora de salud (EPS/aseguradora); Entidad responsable de pago; Grupo de atención; Autorización; Ingreso (legalización de cita); Centro de atención; Consultorio; Profesional de salud; Estado de cita (asignada/cumplida/incumplida/preasignada/cancelada); CUPS; Historia clínica (folio, fecha inicio/fin); Factura; Detalle de factura; Orden de servicio; Valor del servicio; Concesión/legalización (ADCONCOEX)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMacroReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalMacroReport: Devuelve un dataset por cita (desde 2023, TIPSOLICITU=1) enriquecido con paciente, entidad, profesional, estado, CUPS, factura asociada al ingreso y nota de HC; incluye marca de actualización GETDATE() convertida a ''Pakistan Standard Time''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMacroReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pac.iptipodoc IN (1..15) → Mapea a códigos de tipo de identificación (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI).; si PAC.IPSEXOPAC = ''1'' o ''2'' → Traduce a ''Hombre''/''Mujer'' (código ''H''/''M''); otros valores quedan NULL.; si HEA.Id IS NOT NULL → Usa el nombre de Contract.HealthAdministrator como entidad. else Usa el nombre de INENTIDAD asociado al paciente.; si A.CODESTCIT en {''0'',''1'',''2'',''3'',''4'',''5''} → Traduce el estado a ASIGNADA/CUMPLIDA/INCUMPLIDA/PREASIGNADA/CANCELADA (los códigos 4 y 5 se consolidan como CANCELADA).; si GC.NUMINGRES IS NULL → Marca CITA LEGALIZADA = ''No''. else Marca CITA LEGALIZADA = ''Si''.; si CS.CONESTADO <> ''2'' al consolidar ADCONCOEX → Toma el MAX(CODCONCEC) por paciente y fecha de cita para asociar el ingreso/legalización vigente, excluyendo concesiones en estado ''2''.; si F.Status = 1 AND CUPS.ServiceType IN (6,8) → Solo considera facturas activas y servicios CUPS de los tipos 6 u 8 para enriquecer la cita con factura.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMacroReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.ADCENATEN; dbo.INPROFSAL; dbo.AGCONSULT; dbo.INPACIENT; Contract.HealthAdministrator; dbo.INENTIDAD; dbo.SEGusuaru; dbo.AGACTIMED; Contract.CUPSEntity; Contract.CareGroup; dbo.ADCONCOEX; Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; dbo.hcurging1', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMacroReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMacroReport';
GO

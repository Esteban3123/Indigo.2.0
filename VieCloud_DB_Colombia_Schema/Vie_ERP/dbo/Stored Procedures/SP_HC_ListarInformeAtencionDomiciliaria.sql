

CREATE  PROCEDURE [dbo].[SP_HC_ListarInformeAtencionDomiciliaria](
@Centro varchar(20),
@Entidad varchar(1000), --Vienen varias entidades!!
@TipoAtencion int,
@FechaInicial varchar(20),
@FechaFinal varchar(20)

)
AS

BEGIN	
if @Entidad ='' set @Entidad =null
	SET NOCOUNT ON;
	Select UFUCODIGO,NUMINGRES,FECHAREGISTRO,DIAS,IPCODPACI,IPNOMCOMP,FechaNacimiento as'Fecha Nacimiento', Edad,NOMENTIDA,DiagPrincipal,EstadoProg,
	CantLaboratorios, Case CantLaboratorios When 0 then 'No' else 'Si' End as CantLaboratorios1,
	CantImagenes,Case CantImagenes When 0 then 'No' else 'Si' End as CantImagenes1,
	CantProcedimientosQx,Case CantProcedimientosQx When 0 then 'No' else 'Si' End as CantProcedimientosQx1,
	CantProcedimientosNoQx,Case CantProcedimientosNoQx When 0 then 'No' else 'Si' End as CantProcedimientosNoQx1,
	CantPatologias,Case CantPatologias When 0 then 'No' else 'Si' End as CantPatologias1,
	CantInsumosDispo,Case CantInsumosDispo When 0 then 'No' else 'Si' End as CantInsumosDispo1,
	CantMedicamentos,Case CantMedicamentos When 0 then 'No' else 'Si' End as CantMedicamentos1,
	ATENCIONAUTORIZA,ATENCIONREALIZA,ENFERMERIAAUTORIZA,ENFERMERIAREALIZA,TERAPIAAUTORIZA,TERAPIAREALIZA,PSICOLOGIAAUTORIZA,PSICOLOGIAREALIZA
	From (
		SELECT 	
		I.UFUCODIGO,
		I.NUMINGRES, 
		PA.FECHAREGISTRO 
		,DATEDIFF(DAY, PA.FECHAREGISTRO,ISNULL( FECHAEGRESO,[Common].[GETDATE]())) DIAS
		 ,I.IPCODPACI	
		,PAC.IPNOMCOMP
		,PAC.IPFECNACI AS 'FechaNacimiento'
		, CAST('' AS CHAR(50)) AS Edad
		,ENT.NOMENTIDA 
		,(SELECT NOMDIAGNO   FROM INDIAGNOS DG
		  LEFT JOIN INDIAGNOP DP ON DP.CODDIAGNO =DG.CODDIAGNO
		  WHERE DP.NUMINGRES=I.NUMINGRES AND DP.CODDIAPRI =1 AND DP.NUMEFOLIO =
		  (SELECT NUMEFOLIO FROM
			(
			SELECT ROW_NUMBER() OVER (ORDER BY CAST(NUMEFOLIO AS INT) DESC) AUTO_ID
			, *
			FROM HCHISPACA
			WHERE NUMINGRES = I.NUMINGRES
			) T
			WHERE AUTO_ID = 1) 		  
		) AS DiagPrincipal,
		CASE   WHEN  FECHAEGRESO IS NULL THEN 'Asignado' ELSE 'Egresado' END   AS EstadoProg,
		(select count(1) from  HCORDLABO where NUMINGRES=I.NUMINGRES and IPCODPACI=I.IPCODPACI AND ESTSERIPS IN ('1','2','3','4','5','6','7','8')) AS CantLaboratorios,
		(select COUNT(1) from HCORDPROQ where NUMINGRES=I.NUMINGRES and IPCODPACI=I.IPCODPACI AND ESTSERIPS IN ('1','2','3','4')) AS CantProcedimientosQx,
		(select COUNT(1) from HCORDPRON where NUMINGRES=I.NUMINGRES and IPCODPACI=I.IPCODPACI AND ESTSERIPS in ('1','2','3','4','5')  ) AS CantProcedimientosNoQx,
		(select COUNT(1) from HCORDIMAG where NUMINGRES=I.NUMINGRES and IPCODPACI=I.IPCODPACI AND ESTSERIPS IN ('1','2','3','4','5','6','7','8')) AS CantImagenes,
		(select COUNT(1) from HCORDPATO where NUMINGRES=I.NUMINGRES and IPCODPACI=I.IPCODPACI AND ESTSERIPS IN ('1','2','3','4','5','6','7','8') ) AS CantPatologias,		
		(select COUNT(1) from HCSOLINSC where NUMINGRES=I.NUMINGRES and IPCODPACI=I.IPCODPACI	) AS CantInsumosDispo,	
		(SELECT  count(1) FROM dbo.HCPRESCRA A 
			INNER JOIN dbo.INPROFSAL B ON A.CODPROSAL=B.CODPROSAL
			INNER JOIN dbo.ADcenaten C ON A.CODCENATE=C.codcenate
			INNER JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO
			INNER JOIN dbo.IHLISTPRO E ON A.CODPRODUC=E.CODPRODUC
			INNER JOIN dbo.INDIAGNOS AS F ON A.CODDIAGNO = F.CODDIAGNO
			LEFT OUTER JOIN dbo.INUNIMEDI AS G ON A.CODUNIMFN = G.CODUNIMED 
			INNER JOIN dbo.HCVIAADMI AS H ON A.CODVIAADM = H.CODVIAADM 
			INNER JOIN dbo.IHFORMEDI AS I ON A.CODFORMED = I.CODFORMED 
			INNER JOIN DBO.HCHISPACA AS J ON A.NUMEFOLIO=J.NUMEFOLIO AND A.IPCODPACI=J.IPCODPACI 
			LEFT OUTER JOIN dbo.INESPECIA N ON J.CODESPTRA=N.CODESPECI 
			LEFT JOIN dbo.HCPRESCRC V ON A.CODCONCEC= V.CODCONCEC WHERE A.IPCODPACI= PAC.IPCODPACI  
			AND PREESTADO IN (1,6,3,4,2,7,5) ) as CantMedicamentos,
			
		 Case ATENCIONAUTORIZA when 0 then 'N/A' Else Cast(ATENCIONAUTORIZA as varchar) End As  ATENCIONAUTORIZA,
		Case ATENCIONREALIZA when 0 then 'N/A' Else Cast(ATENCIONREALIZA as varchar) End As ATENCIONREALIZA, 
		Case ENFERMERIAAUTORIZA when 0 then 'N/A' Else Cast(ENFERMERIAAUTORIZA as varchar) End As ENFERMERIAAUTORIZA,
		Case ENFERMERIAREALIZA when 0 then 'N/A' Else Cast(ENFERMERIAREALIZA as varchar) End As ENFERMERIAREALIZA,
		Case TERAPIAAUTORIZA when 0 then 'N/A' Else Cast(TERAPIAAUTORIZA as varchar) End As TERAPIAAUTORIZA, 
		Case TERAPIAREALIZA when 0 then 'N/A' Else Cast(TERAPIAREALIZA as varchar) End As TERAPIAREALIZA,
		Case PSICOLOGIAAUTORIZA when 0 then 'N/A' Else Cast(PSICOLOGIAAUTORIZA as varchar) End As PSICOLOGIAAUTORIZA,
		Case PSICOLOGIAREALIZA when 0 then 'N/A' Else Cast(PSICOLOGIAREALIZA as varchar) End As PSICOLOGIAREALIZA
		FROM PADASIGNACION PA 
		INNER JOIN ADINGRESO I  with (nolock) on PA.NUMEROINGRESO =I.NUMINGRES					
		INNER JOIN INUNIFUNC UF with (nolock) ON UF.UFUCODIGO =I.UFUCODIGO
		INNER JOIN INENTIDAD ENT with (nolock) ON I.CODENTIDA=ENT.CODENTIDA 
	    INNER JOIN INPACIENT PAC with (nolock) ON I.IPCODPACI =PAC.IPCODPACI  		 						
		WHERE 
		PA.FECHAREGISTRO >= @FechaInicial AND PA.FECHAREGISTRO <= @FechaFinal AND 
		PA.TIPOATENCION =@TipoAtencion AND
		I.CODCENATE =@Centro AND (@Entidad is null or  I.CODENTIDA  IN (SELECT Value FROM dbo.splitstring(@Entidad)))	 ) as T	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el informe de atención domiciliaria (PAD) para un centro de atención, un rango de fechas y una o varias entidades (EPS/aseguradoras). Por cada paciente asignado al programa domiciliario, consolida datos del ingreso, identificación y nombre del paciente, edad, entidad aseguradora, diagnóstico principal (CIE-10), estado del caso (asignado o egresado), días en el programa, y conteos de órdenes activas de laboratorios, imágenes, procedimientos quirúrgicos y no quirúrgicos, patologías, insumos/dispositivos y medicamentos prescritos. Adicionalmente muestra las sesiones autorizadas versus realizadas por área de atención: médica, enfermería, terapia y psicología. Es el reporte operativo y de seguimiento clínico-administrativo del programa de hospitalización domiciliaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarInformeAtencionDomiciliaria';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarInformeAtencionDomiciliaria';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un informe consolidado de pacientes asignados a atención domiciliaria en un centro, periodo y tipo de atención dados, incluyendo diagnóstico principal, estado, días transcurridos y conteos de servicios clínicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInformeAtencionDomiciliaria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas debe ser comparable como texto/fecha contra PADASIGNACION.FECHAREGISTRO; Debe existir el centro de atención indicado en ADINGRESO.CODCENATE; La función dbo.splitstring debe estar disponible para parsear la lista de entidades; Si se filtra por entidades, deben venir separadas en una cadena interpretable por dbo.splitstring; Los ingresos deben tener paciente, unidad funcional y entidad válidos para aparecer (joins INNER)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInformeAtencionDomiciliaria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran asignaciones cuyo TIPOATENCION coincide con el solicitado y dentro del rango de fechas de registro indicado; Solo se incluyen ingresos del centro de atención solicitado; El diagnóstico principal corresponde al folio de historia clínica más reciente del ingreso (mayor NUMEFOLIO) y al registro marcado como diagnóstico principal (CODDIAPRI=1); Los conteos por categoría sólo cuentan registros con ESTSERIPS dentro de los estados válidos definidos por categoría (laboratorios/imágenes/patologías 1-8, procedimientos Qx 1-4, procedimientos NoQx 1-5); Los medicamentos contados son únicamente prescripciones con PREESTADO en (1,2,3,4,5,6,7) y asociadas al paciente; Cuando no se especifican entidades, no se restringe por entidad; El procedimiento es de solo lectura: no modifica datos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInformeAtencionDomiciliaria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Atención domiciliaria; Ingreso hospitalario; Asignación de paciente; Diagnóstico principal (CIE-10); Egreso; Órdenes de laboratorio; Órdenes de imágenes diagnósticas; Procedimientos quirúrgicos; Procedimientos no quirúrgicos; Patología; Insumos y dispositivos médicos; Prescripción de medicamentos; Entidad responsable de pago; Profesional de la salud (atención, enfermería, terapia, psicología); Historia clínica (folio); Unidad funcional; Centro de atención', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInformeAtencionDomiciliaria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ?: Devuelve un resultset con los pacientes asignados (PADASIGNACION) cuyo ingreso pertenece al centro y rango de fechas indicados, junto con indicadores Si/No por categoría de servicios y profesionales autorizadores/realizadores', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInformeAtencionDomiciliaria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Parámetro de entidad llega como cadena vacía → Se trata como NULL y se omite el filtro por entidad (se incluyen todas) else Se filtra por las entidades listadas separando la cadena con dbo.splitstring; si FECHAEGRESO IS NULL en la asignación → Se reporta el estado del programa como ''Asignado'' y los días se calculan contra la fecha actual else Se reporta como ''Egresado'' y los días se calculan hasta FECHAEGRESO; si Cantidad por categoría (laboratorios, imágenes, procedimientos Qx/NoQx, patologías, insumos, medicamentos) es 0 → Se muestra indicador ''No'' else Se muestra indicador ''Si''; si Profesional autorizador/realizador (atención, enfermería, terapia, psicología) es 0 → Se reporta ''N/A'' else Se reporta el código del profesional como texto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInformeAtencionDomiciliaria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.PADASIGNACION; dbo.ADINGRESO; dbo.INUNIFUNC; dbo.INENTIDAD; dbo.INPACIENT; dbo.HCHISPACA; dbo.INDIAGNOS; dbo.INDIAGNOP; dbo.HCORDLABO; dbo.HCORDPROQ; dbo.HCORDPRON; dbo.HCORDIMAG; dbo.HCORDPATO; dbo.HCSOLINSC; dbo.HCPRESCRA; dbo.INPROFSAL; dbo.ADcenaten; dbo.IHLISTPRO; dbo.INUNIMEDI; dbo.HCVIAADMI; dbo.IHFORMEDI; dbo.INESPECIA; dbo.HCPRESCRC; dbo.splitstring', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInformeAtencionDomiciliaria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInformeAtencionDomiciliaria';
-- GO

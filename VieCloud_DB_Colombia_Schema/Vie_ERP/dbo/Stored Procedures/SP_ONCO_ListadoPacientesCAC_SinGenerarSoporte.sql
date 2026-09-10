

-- =============================================
-- Author:		Rafael Patiño
-- Create date: 21-12-2021
-- Description:	SP carga los paciente a generar soporte cuentas de alto costo CAC
-- =============================================

CREATE PROCEDURE [dbo].[SP_ONCO_ListadoPacientesCAC_SinGenerarSoporte]
(
	@IdEntidad integer,
	@FechaInicial datetime,
	@Fechafinal datetime,
	@IdEjecuciones varchar(500)
	   
)
AS
BEGIN
	SET NOCOUNT ON;

	
				 		--Consulta externa 
						--'    Tipo Especialidad
						--'0- Ninguna
						--'1- Oftalmología
						--'2- Nutrición
						--'3- Psicología
						--'5- Anestesiología
						--'6- Mastología
						--'7- Oncología
						--'8- Hematología
						--'9- Cx oncológica
						--'10- Ortopedia oncológica
						--'11- Cx Plastica oncológica
						--'12- Urológia oncológica
						--'13- dermatología oncológica
						--'14- Cuidados paliativos
						--'15- Fisioterapia

					WITH tmp_ConsultaExternaOncologicas AS
					(
					    select H.IPCODPACI  from 
						dbo.HCHISPACA H with(nolock) inner join 
						dbo.INESPECIA E with(nolock) on E.CODESPECI = H.CODESPTRA AND H.GENCONEXT =1 inner join
						dbo.HCONCOPREG P with(nolock) on P.[6] = H.IPCODPACI --AND P.CODDIAGNO = H.CODDIAGNO
						where  E.TIPESPECI IN (1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23) AND P.IDEntidadVIE = @IdEntidad AND H.FECHISPAC between (@FechaInicial) AND (@FechaFinal)
					),
				    --Consultamos folios de hospitalizacion
					tmp_Hospitalizaciones AS
					(					
					select H.IPCODPACI 
					from dbo.HCHISPACA H  inner join
					dbo.INPROFSAL P with(nolock) on P.CODPROSAL  = H.CODPROSAL AND P.TIPPROFES in (1,6) inner join
					dbo.ADINGRESO I with(nolock) on I.NUMINGRES = H.NUMINGRES AND (I.UFUEGRHOS is not null or I.UFUAACTHOS is not null ) inner join
					dbo.HCONCOPREG ONC with(nolock) on ONC.[6] = H.IPCODPACI
					WHERE ONC.IDEntidadVIE = @IdEntidad AND H.FECHISPAC between (@FechaInicial) AND (@FechaFinal)
					),
					--informe QX
					tmp_InformeQX AS
					(
					select H.IPCODPACI 
					from dbo.HCQXINFOR H inner join
					dbo.HCONCOPREG ONC with(nolock) on ONC.[6] = H.IPCODPACI 
					WHERE ONC.IDEntidadVIE = @IdEntidad AND H.FECHORINI between (@FechaInicial) AND (@FechaFinal)
					),
					---aplicaciones de dosis de radio y braquiterapia
					tmp_AplicacionesRadio AS
					(
					select 			
					  A.IPCODPACI 
					from HCRADORDEN A 
					INNER JOIN dbo.HCRADESQUEMAS ES with(nolock) ON A.ID = ES.IDHCRADORDEN
					INNER JOIN dbo.HCORDPRON ORD with(nolock) ON A.IDHCORDPRON = ORD.AUTO
					INNER JOIN dbo.INCUPSIPS  E with(nolock) ON A.CODSERIPS = E.CODSERIPS AND E.SERIPSDASH  = 6 --Radioterapia Externa
					inner join dbo.HCONCOPREG ONC with(nolock) on ONC.[6] = A.IPCODPACI 
					WHERE  A.ESTADO IN (3,4,5,6,7) AND ONC.IDEntidadVIE = @IdEntidad AND A.FECHAREGISTRO between (@FechaInicial) AND (@FechaFinal)
					),
					Tmp_Braquiterapias AS
					(
					select   A.IPCODPACI 
					from dbo.HCRADORDEN A 
					INNER JOIN dbo.HCORDPRON ORD with(nolock) ON A.IDHCORDPRON = ORD.AUTO
					INNER JOIN dbo.INCUPSIPS  E with(nolock) ON A.CODSERIPS = E.CODSERIPS AND E.SERIPSDASH  = 13 ---Braquiterapias
					inner join dbo.HCONCOPREG ONC with(nolock) on ONC.[6] = A.IPCODPACI 
					WHERE A.ESTADO IN (5,7,8) AND ONC.IDEntidadVIE = @IdEntidad AND A.FECHAREGISTRO between (@FechaInicial) AND (@FechaFinal)
					),
					--aplicaciones de quimioterapia 
					Tmp_Quimio AS
					(
					select   IPCODPACI
					from dbo.HCHOJAMED H 
					inner join dbo.HCONCOPREG ONC with(nolock) on ONC.[6] = H.IPCODPACI 
					where  H.IDHCORDQUIMIO IS NOT NULL AND H.MEDESTADO = '2' AND ONC.IDEntidadVIE = @IdEntidad AND H.FECAPLMED between (@FechaInicial) AND (@FechaFinal)
					),
					--patologias con cancer 
					TMP_patologiasCancer_Hosp  AS
					(
					select IPCODPACI
					from dbo.HCORDPATO H 
					inner join PathologyALULA.INTEGRATIONCONTROL CTR with(nolock) on H.AUTO = CTR.idpathology AND H.CODSERIPS = CTR.CUPS
					inner join PathologyALULA.OBSERVATION_REQUESTS O with(nolock) on O.placer_order_number = CTR.id and ctr.ordertype  ='INT' 
					inner join PathologyALULA.DIAGNOSIS_RESULTS R with(nolock) on R.order_id = O.id  
					inner join dbo.HCONCOPREG ONC with(nolock) on ONC.[6] = H.IPCODPACI 
					where  H.CAC23 IS NOT NULL AND H.ESTSERIPS NOT IN(1,6) AND R.report_base64 is not null AND ONC.IDEntidadVIE = @IdEntidad AND H.FECORDMED between (@FechaInicial) AND (@FechaFinal)
					),
					TMP_patologiasCancer_Amb AS
					(
					select  IPCODPACI
					from dbo.AMBORDPAT H 
					inner join PathologyALULA.INTEGRATIONCONTROL CTR with(nolock) on H.AUTO = CTR.idpathology AND H.CODSERIPS = CTR.CUPS
					inner join PathologyALULA.OBSERVATION_REQUESTS O with(nolock) on O.placer_order_number = CTR.id and ctr.ordertype  ='AMB' 
					inner join PathologyALULA.DIAGNOSIS_RESULTS R with(nolock) on R.order_id = O.id 
					inner join dbo.HCONCOPREG ONC with(nolock) on ONC.[6] = H.IPCODPACI  
					where H.CAC23 IS NOT NULL AND H.ESTSERIPS NOT IN(1,6)  AND R.report_base64 is not null AND ONC.IDEntidadVIE = @IdEntidad AND H.FECORDMED between (@FechaInicial) AND (@FechaFinal)
					),
					--medicamentos domiciliarios
					TMP_MedicamentoDomiciliarios AS
					(
					select tmp.IPCODPACI
					from Inventory.PharmaceuticalDispensing D inner join
					Inventory.PharmaceuticalDispensingDetail DD with(nolock) on D.Id = DD.PharmaceuticalDispensingId inner join
					Inventory.InventoryProduct P with(nolock) on P.Id = dd.ProductId inner join
					Inventory.ATC Atc with(nolock) on atc.id = P.ATCId inner join
					(
						select C.CODCONCEC , C.NUMINGRES, CC.CODPRODUC, C.CODBODEGA, C.CODPROSAL,C.IPCODPACI    from dbo.HCFARMEPC C inner join dbo.HCFARMEPD CC with(nolock) on C.CODCONCEC = CC.CODCONCEC  where C.ORDESTADO =2 AND CC.PROESTADO = 2 AND C.IDHCORDPRON IS NOT NULL 
					)as TMP on D.AdmissionNumber = TMP.NUMINGRES AND ATc.Code = tmp.CODPRODUC AND TMP.CODPROSAL = DD.OrderedHealthProfessionalCode  inner join 
					dbo.HCONCOPREG ONC with(nolock) on ONC.[6] = TMP.IPCODPACI
					where D.Status = 2 AND D.SurgeryExpenseSheetId is null AND D.AffectInventory =  1 AND D.EntityName = 'SaveDashboardPharmacy' AND ONC.IDEntidadVIE = @IdEntidad AND D.AdmissionNumber in (select distinct H.NUMINGRES from dbo.HCFARMEPC H  where H.ORDESTADO = 2 AND IDHCORDPRON  is not null AND H.ORDESTADO = 2 AND H.FECHAORDE between (@FechaInicial) AND (@FechaFinal))  
					),
					--nota prematura 
					TMP_NotasPrematuraFinalizacion AS
					(
					select IPCODPACI
					from dbo.HCCTRNOTE A inner join dbo.HCONCOPREG ONC ON A.IPCODPACI = ONC.[6]
					where  TITNOTENF = 'FINALIZACIÓN PREMATURA QUIMIOTERAPIA' AND (FECREGIST BETWEEN @FechaInicial AND @FechaFinal) AND ONC.IDEntidadVIE = @IdEntidad
					),
					Tmp_ConsultasOncologicas AS
					(
							select IPCODPACI from tmp_ConsultaExternaOncologicas
						UNION 
							select IPCODPACI from tmp_Hospitalizaciones
						UNION 
							select IPCODPACI from tmp_InformeQX		
						UNION 
							select IPCODPACI from tmp_AplicacionesRadio					
						UNION 
							select IPCODPACI from Tmp_Braquiterapias
						UNION
							select IPCODPACI from Tmp_Quimio
						UNION 
							select IPCODPACI from TMP_patologiasCancer_Hosp
						UNION 
							select IPCODPACI from TMP_patologiasCancer_Amb
						union
							select IPCODPACI from TMP_MedicamentoDomiciliarios
						UNION 
							select IPCODPACI from TMP_NotasPrematuraFinalizacion
					)				
				
				
				SELECT DISTINCT IPCODPACI INTO #tabla_ConsultasOncologicas FROM Tmp_ConsultasOncologicas

	if @IdEjecuciones = '' begin

		
				Select	A.ID ,
					dbo.TipoDocumento(P.IPTIPODOC) as TipoIdentificacion,  				
					rtrim(ltrim(P.IPCODPACI)) as Identificacion,
					rtrim(P.IPNOMCOMP) as NombrePaciente,
					rtrim(D.CODDIAGNO) + ' - ' + rtrim(D.NOMDIAGNO) as Diagnostico,
					E.Code + ' - ' + E.Name as Entidad,
					[dbo].[Edad](convert(date,P.IPFECNACI),convert(date,getdate())) as Edad,
					A.[7] as FechaNacimiento,
					convert(int,0) as Generado
				FROM HCONCOPREG A with(nolock)
					INNER JOIN INPACIENT P with(nolock) ON A.[6] = P.IPCODPACI
					INNER JOIN INDIAGNOS D with(nolock) ON A.CODDIAGNO = D.CODDIAGNO
					INNER JOIN Contract.HealthAdministrator as E on E.Id = A.IDEntidadVIE 
					INNER JOIN #tabla_ConsultasOncologicas Tmp on A.[6] = tmp.IPCODPACI
				WHERE A.IDEntidadVIE = @IdEntidad
						

	end else begin

	
			 
				--no incluye los pacientes generados correctamente en de las ejecuciones seleccionadas
				Select	A.ID ,
					dbo.TipoDocumento(P.IPTIPODOC) as TipoIdentificacion,   				
					rtrim(ltrim(P.IPCODPACI)) as Identificacion,
					rtrim(P.IPNOMCOMP) as NombrePaciente,
					rtrim(D.CODDIAGNO) + ' - ' + rtrim(D.NOMDIAGNO) as Diagnostico,
					E.Code + ' - ' + E.Name as Entidad,
					[dbo].[Edad](convert(date,P.IPFECNACI),convert(date,getdate())) as Edad,
					A.[7] as FechaNacimiento,
					convert(int,0) as Generado
				FROM HCONCOPREG A with(nolock)
					INNER JOIN INPACIENT P with(nolock) ON A.[6] = P.IPCODPACI
					INNER JOIN INDIAGNOS D with(nolock) ON A.CODDIAGNO = D.CODDIAGNO
					INNER JOIN Contract.HealthAdministrator as E on E.Id = A.IDEntidadVIE 
				    INNER JOIN #tabla_ConsultasOncologicas Tmp on A.[6] = tmp.IPCODPACI 
				WHERE A.IDEntidadVIE = @IdEntidad  
					AND P.IPCODPACI not in (select distinct d.IPCODPACI from HCSOPORTECAC c inner join HCSOPORTECACPACIENTE d on c.ID = d.IDHCSOPORTECAC  where c.ID in ( SELECT Value FROM dbo.SplitString(@IdEjecuciones)) and d.COMPLETO = 1 and c.TIPOBUSQUEDA = 1)
			
			UNION
			-- traemos paciente que no fueron generados en la ejecuciones seleccionadas en el front
				Select	A.ID ,
					dbo.TipoDocumento(P.IPTIPODOC) as TipoIdentificacion,   				
					rtrim(ltrim(P.IPCODPACI)) as Identificacion,
					rtrim(P.IPNOMCOMP) as NombrePaciente,
					rtrim(D.CODDIAGNO) + ' - ' + rtrim(D.NOMDIAGNO) as Diagnostico,
					E.Code + ' - ' + E.Name as Entidad,
					[dbo].[Edad](convert(date,P.IPFECNACI),convert(date,getdate())) as Edad,
					A.[7] as FechaNacimiento,
					convert(int,0) as Generado
				FROM HCONCOPREG A with(nolock)
					INNER JOIN INPACIENT P with(nolock) ON A.[6] = P.IPCODPACI
					INNER JOIN INDIAGNOS D with(nolock) ON A.CODDIAGNO = D.CODDIAGNO
					INNER JOIN Contract.HealthAdministrator as E on E.Id = A.IDEntidadVIE 
					INNER JOIN #tabla_ConsultasOncologicas Tmp on A.[6] = tmp.IPCODPACI
				WHERE A.IDEntidadVIE = @IdEntidad 
					AND P.IPCODPACI in (select distinct d.IPCODPACI from HCSOPORTECAC c inner join HCSOPORTECACPACIENTE d on c.ID = d.IDHCSOPORTECAC  where c.ID in ( SELECT Value FROM dbo.SplitString(@IdEjecuciones)) and d.COMPLETO = 0 and c.TIPOBUSQUEDA = 1)

	end

	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que identifica los pacientes oncológicos registrados en el módulo de Cuentas de Alto Costo (CAC) que aún no tienen soporte generado, dentro de un rango de fechas y para una entidad específica. Consolida múltiples fuentes de actividad oncológica: consultas externas en especialidades oncológicas (usando historias clínicas y el catálogo de especialidades), hospitalizaciones con folios clínicos, informes quirúrgicos, aplicaciones de radioterapia y braquiterapia, sesiones de quimioterapia, resultados de patología con diagnóstico de cáncer (hospitalizados y ambulatorios) y medicamentos domiciliarios de alto costo. Cruza todos estos registros contra el padrón oncológico (HCONCOPREG) para verificar que el paciente esté inscrito en el programa CAC de la entidad, y retorna el listado de pacientes que tienen actividad asistencial en el período pero a los cuales aún no se les ha generado el soporte de facturación o reporte exigido para cuentas de alto costo. Es utilizado por el área de facturación y gestión CAC para garantizar que ningún paciente oncológico quede sin su soporte documental en los plazos requeridos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListadoPacientesCAC_SinGenerarSoporte';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListadoPacientesCAC_SinGenerarSoporte';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista pacientes oncológicos (cuentas de alto costo - CAC) que tuvieron actividad asistencial en un rango de fechas para una entidad pagadora y aún no tienen soporte CAC generado satisfactoriamente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoPacientesCAC_SinGenerarSoporte';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe estar registrado en HCONCOPREG con la entidad VIE indicada.; Las actividades clínicas (consultas, hospitalizaciones, cirugías, radioterapia, braquiterapia, quimioterapia, patologías, dispensación de medicamentos, notas) deben haber ocurrido dentro del rango de fechas indicado.; Si se consulta filtrando por ejecuciones previas, los IDs deben ser parseables vía dbo.SplitString y existir en HCSOPORTECAC con TIPOBUSQUEDA = 1.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoPacientesCAC_SinGenerarSoporte';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todos los pacientes retornados pertenecen a la entidad pagadora (@IdEntidad) en HCONCOPREG.IDEntidadVIE.; Solo se consideran soportes CAC con TIPOBUSQUEDA = 1.; El listado siempre se restringe a pacientes con al menos una actividad clínica oncológica en el rango de fechas.; La columna Generado siempre se devuelve como 0 (no generado).; La identificación del paciente en oncología se referencia mediante HCONCOPREG.[6] = IPCODPACI.; Patologías incluidas requieren reporte (report_base64) presente y marcador CAC23 no nulo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoPacientesCAC_SinGenerarSoporte';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Cuando @IdEjecuciones = '''' devuelve todos los pacientes oncológicos de la entidad con actividad clínica en el rango.; [RETURN_RESULT] RESULTSET: Cuando @IdEjecuciones <> '''' excluye pacientes ya generados (COMPLETO=1) en las ejecuciones indicadas y agrega (UNION) los pacientes que quedaron incompletos (COMPLETO=0) en esas ejecuciones, siempre con TIPOBUSQUEDA=1.; [INSERT] #tabla_ConsultasOncologicas: Inserta pacientes únicos resultantes de la unión de 10 fuentes clínicas oncológicas filtradas por entidad y rango de fechas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoPacientesCAC_SinGenerarSoporte';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @IdEjecuciones = '''' → Lista todos los pacientes oncológicos identificados sin filtrar por ejecuciones previas. else Excluye pacientes generados completos en las ejecuciones seleccionadas y une los marcados como incompletos (COMPLETO=0) en esas ejecuciones.; si HCHISPACA con GENCONEXT=1 y especialidad TIPESPECI IN (1..23) → Considera al paciente como con consulta externa oncológica relevante.; si ADINGRESO con UFUEGRHOS o UFUAACTHOS no nulos y profesional TIPPROFES IN (1,6) → Considera al paciente como hospitalizado relevante para CAC.; si HCRADORDEN.ESTADO IN (3,4,5,6,7) y SERIPSDASH=6 → Incluye paciente con aplicaciones de radioterapia externa.; si HCRADORDEN.ESTADO IN (5,7,8) y SERIPSDASH=13 → Incluye paciente con braquiterapias.; si HCHOJAMED.IDHCORDQUIMIO IS NOT NULL y MEDESTADO=''2'' → Incluye paciente con aplicaciones de quimioterapia.; si HCORDPATO/AMBORDPAT.CAC23 IS NOT NULL, ESTSERIPS NOT IN (1,6) y reporte de patología (report_base64) presente → Incluye paciente con patología oncológica reportada (hospitalaria o ambulatoria).; si PharmaceuticalDispensing.Status=2, SurgeryExpenseSheetId IS NULL, AffectInventory=1, EntityName=''SaveDashboardPharmacy'' y orden HCFARMEPC.ORDESTADO=2 con IDHCORDPRON no nulo → Incluye paciente con dispensación de medicamentos domiciliarios.; si HCCTRNOTE.TITNOTENF = ''FINALIZACIÓN PREMATURA QUIMIOTERAPIA'' → Incluye paciente con nota de finalización prematura de quimioterapia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoPacientesCAC_SinGenerarSoporte';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.TipoDocumento; dbo.Edad; dbo.SplitString', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoPacientesCAC_SinGenerarSoporte';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INESPECIA; dbo.HCONCOPREG; dbo.INPROFSAL; dbo.ADINGRESO; dbo.HCQXINFOR; dbo.HCRADORDEN; dbo.HCRADESQUEMAS; dbo.HCORDPRON; dbo.INCUPSIPS; dbo.HCHOJAMED; dbo.HCORDPATO; dbo.AMBORDPAT; PathologyALULA.INTEGRATIONCONTROL; PathologyALULA.OBSERVATION_REQUESTS; PathologyALULA.DIAGNOSIS_RESULTS; Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; Inventory.InventoryProduct; Inventory.ATC; dbo.HCFARMEPC; dbo.HCFARMEPD; dbo.HCCTRNOTE; dbo.INPACIENT; dbo.INDIAGNOS; Contract.HealthAdministrator; dbo.HCSOPORTECAC; dbo.HCSOPORTECACPACIENTE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoPacientesCAC_SinGenerarSoporte';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoPacientesCAC_SinGenerarSoporte';
-- GO

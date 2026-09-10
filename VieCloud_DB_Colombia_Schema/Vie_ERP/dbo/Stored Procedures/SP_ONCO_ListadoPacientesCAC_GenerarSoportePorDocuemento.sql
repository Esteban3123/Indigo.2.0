		CREATE PROCEDURE [dbo].[SP_ONCO_ListadoPacientesCAC_GenerarSoportePorDocuemento]
(
	@Identificacion varchar(MAX),
	@FechaInicial datetime,
	@Fechafinal datetime
)
AS
BEGIN
	SET NOCOUNT ON;
	 
	 /*
		WITH GrupoCancer
		AS (  
			SELECT CODDIAGNO, IPCODPACI from HCGRUPOCANCERPACIC with(nolock) WHERE FECHAREGISTRO BETWEEN @FechaInicial AND @Fechafinal AND (IPCODPACI IN (SELECT Value FROM dbo.SplitString(@Identificacion))  )
			)

       Select   A.ID ,
				dbo.TipoDocumento(P.IPTIPODOC) as TipoIdentificacion,  				
				rtrim(ltrim(P.IPCODPACI)) as Identificacion,
				rtrim(P.IPNOMCOMP) as NombrePaciente,
				E.Code + ' - ' + E.Name as Entidad,
				[dbo].[Edad](convert(date,P.IPFECNACI),convert(date,getdate())) as Edad,
				A.[7] as FechaNacimiento,
				convert(int,0) as Generado
		FROM HCONCOPREG A with(nolock)
		    INNER JOIN INPACIENT P with(nolock) ON A.[6] = P.IPCODPACI
			INNER JOIN Contract.HealthAdministrator as E with(nolock) on E.Id = A.IDEntidadVIE  
			INNER JOIN (select distinct CODDIAGNO, IPCODPACI from GrupoCancer) as GC on GC.IPCODPACI = A.[6] and GC.CODDIAGNO = A.CODDIAGNO 
		WHERE A.[6] IN (SELECT Value FROM dbo.SplitString(@Identificacion)) */

			declare @tabla_ConsultasOncologicas as table(IPCODPACI varchar(25))

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

					insert into @tabla_ConsultasOncologicas
					SELECT * FROM
					(
					select distinct H.IPCODPACI  from 
					dbo.HCHISPACA H with(nolock) inner join 
					dbo.INESPECIA E with(nolock) on E.CODESPECI = H.CODESPTRA AND H.GENCONEXT =1 inner join
					dbo.HCONCOPREG P with(nolock) on P.[6] = H.IPCODPACI --AND P.CODDIAGNO = H.CODDIAGNO
					where  E.TIPESPECI IN (1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23) AND IPCODPACI IN (SELECT Value FROM dbo.SplitString(@Identificacion)) AND H.FECHISPAC between (@FechaInicial) AND (@FechaFinal)
					UNION ALL
					--Consultamos folios de hospitalizacion
					select distinct H.IPCODPACI 
					from dbo.HCHISPACA H  inner join
					dbo.INPROFSAL P with(nolock) on P.CODPROSAL  = H.CODPROSAL AND P.TIPPROFES in (1,6) inner join
					dbo.ADINGRESO I with(nolock) on I.NUMINGRES = H.NUMINGRES AND (I.UFUEGRHOS is not null or I.UFUAACTHOS is not null ) inner join
					dbo.HCONCOPREG ONC with(nolock) on ONC.[6] = H.IPCODPACI
					WHERE H.IPCODPACI IN (SELECT Value FROM dbo.SplitString(@Identificacion)) AND H.FECHISPAC between (@FechaInicial) AND (@FechaFinal)
					UNION ALL
					--informe QX
					select distinct H.IPCODPACI 
					from dbo.HCQXINFOR H inner join
					dbo.HCONCOPREG ONC with(nolock) on ONC.[6] = H.IPCODPACI 
					WHERE H.IPCODPACI IN (SELECT Value FROM dbo.SplitString(@Identificacion)) AND H.FECHORINI between (@FechaInicial) AND (@FechaFinal)
					UNION ALL
					---aplicaciones de dosis de radio y braquiterapia
					select 			
					distinct  A.IPCODPACI 
					from HCRADORDEN A 
					INNER JOIN dbo.HCRADESQUEMAS ES with(nolock) ON A.ID = ES.IDHCRADORDEN
					INNER JOIN dbo.HCORDPRON ORD with(nolock) ON A.IDHCORDPRON = ORD.AUTO
					INNER JOIN dbo.INCUPSIPS  E with(nolock) ON A.CODSERIPS = E.CODSERIPS AND E.SERIPSDASH  = 6 --Radioterapia Externa
					inner join dbo.HCONCOPREG ONC with(nolock) on ONC.[6] = A.IPCODPACI 
					WHERE  A.ESTADO IN (3,4,5,6,7) AND A.IPCODPACI IN (SELECT Value FROM dbo.SplitString(@Identificacion))  AND A.FECHAREGISTRO between (@FechaInicial) AND (@FechaFinal)
					UNION ALL
					select distinct  A.IPCODPACI 
					from dbo.HCRADORDEN A 
					INNER JOIN dbo.HCORDPRON ORD with(nolock) ON A.IDHCORDPRON = ORD.AUTO
					INNER JOIN dbo.INCUPSIPS  E with(nolock) ON A.CODSERIPS = E.CODSERIPS AND E.SERIPSDASH  = 13 ---Braquiterapias
					inner join dbo.HCONCOPREG ONC with(nolock) on ONC.[6] = A.IPCODPACI 
					WHERE A.ESTADO IN (5,7,8) AND A.IPCODPACI IN (SELECT Value FROM dbo.SplitString(@Identificacion)) AND A.FECHAREGISTRO between (@FechaInicial) AND (@FechaFinal)
					UNION ALL
					--aplicaciones de quimioterapia 
					select  distinct IPCODPACI
					from dbo.HCHOJAMED H 
					inner join dbo.HCONCOPREG ONC with(nolock) on ONC.[6] = H.IPCODPACI 
					where  H.IDHCORDQUIMIO IS NOT NULL AND H.MEDESTADO = '2' AND H.IPCODPACI IN (SELECT Value FROM dbo.SplitString(@Identificacion)) AND  H.FECAPLMED between (@FechaInicial) AND (@FechaFinal)
					UNION ALL
					--patologias con cancer 
					select IPCODPACI
					from dbo.HCORDPATO H 
					inner join PathologyALULA.INTEGRATIONCONTROL CTR with(nolock) on H.AUTO = CTR.idpathology AND H.CODSERIPS = CTR.CUPS
					inner join PathologyALULA.OBSERVATION_REQUESTS O with(nolock) on O.placer_order_number = CTR.id and ctr.ordertype  ='INT' 
					inner join PathologyALULA.DIAGNOSIS_RESULTS R with(nolock) on R.order_id = O.id  
					inner join dbo.HCONCOPREG ONC with(nolock) on ONC.[6] = H.IPCODPACI 
					where  H.CAC23 IS NOT NULL AND H.ESTSERIPS NOT IN(1,6) AND R.report_base64 is not null AND H.IPCODPACI IN (SELECT Value FROM dbo.SplitString(@Identificacion))  AND H.FECORDMED between (@FechaInicial) AND (@FechaFinal)
					union
					select  IPCODPACI
					from dbo.AMBORDPAT H 
					inner join PathologyALULA.INTEGRATIONCONTROL CTR with(nolock) on H.AUTO = CTR.idpathology AND H.CODSERIPS = CTR.CUPS
					inner join PathologyALULA.OBSERVATION_REQUESTS O with(nolock) on O.placer_order_number = CTR.id and ctr.ordertype  ='AMB' 
					inner join PathologyALULA.DIAGNOSIS_RESULTS R with(nolock) on R.order_id = O.id 
					inner join dbo.HCONCOPREG ONC with(nolock) on ONC.[6] = H.IPCODPACI  
					where H.CAC23 IS NOT NULL AND H.ESTSERIPS NOT IN(1,6)  AND R.report_base64 is not null AND H.IPCODPACI IN (SELECT Value FROM dbo.SplitString(@Identificacion))  AND H.FECORDMED between (@FechaInicial) AND (@FechaFinal)
					UNION ALL
					--medicamentos domiciliarios
					select distinct tmp.IPCODPACI
					from Inventory.PharmaceuticalDispensing D inner join
					Inventory.PharmaceuticalDispensingDetail DD with(nolock) on D.Id = DD.PharmaceuticalDispensingId inner join
					Inventory.InventoryProduct P with(nolock) on P.Id = dd.ProductId inner join
					Inventory.ATC Atc with(nolock) on atc.id = P.ATCId inner join
					(
						select C.CODCONCEC , C.NUMINGRES, CC.CODPRODUC, C.CODBODEGA, C.CODPROSAL,C.IPCODPACI    from dbo.HCFARMEPC C inner join dbo.HCFARMEPD CC with(nolock) on C.CODCONCEC = CC.CODCONCEC  where C.ORDESTADO =2 AND CC.PROESTADO = 2 AND C.IDHCORDPRON IS NOT NULL 
					)as TMP on D.AdmissionNumber = TMP.NUMINGRES AND ATc.Code = tmp.CODPRODUC AND TMP.CODPROSAL = DD.OrderedHealthProfessionalCode  inner join 
					dbo.HCONCOPREG ONC with(nolock) on ONC.[6] = TMP.IPCODPACI
					where D.Status = 2 AND D.SurgeryExpenseSheetId is null AND D.AffectInventory =  1 AND D.EntityName = 'SaveDashboardPharmacy' AND tmp.IPCODPACI IN (SELECT Value FROM dbo.SplitString(@Identificacion))  AND D.AdmissionNumber in (select distinct H.NUMINGRES from dbo.HCFARMEPC H  where H.ORDESTADO = 2 AND IDHCORDPRON  is not null AND H.ORDESTADO = 2 AND H.FECHAORDE between (@FechaInicial) AND (@FechaFinal))  
					UNION ALL
					--nota prematura 
					select distinct IPCODPACI
					from dbo.HCCTRNOTE A inner join dbo.HCONCOPREG ONC ON A.IPCODPACI = ONC.[6]
					where  TITNOTENF = 'FINALIZACIÓN PREMATURA QUIMIOTERAPIA' AND (FECREGIST BETWEEN @FechaInicial AND @FechaFinal) AND A.IPCODPACI IN (SELECT Value FROM dbo.SplitString(@Identificacion)) 
					) as TMP
				

       Select   A.ID ,
				dbo.TipoDocumento(P.IPTIPODOC) as TipoIdentificacion,  				
				rtrim(ltrim(P.IPCODPACI)) as Identificacion,
				rtrim(P.IPNOMCOMP) as NombrePaciente,
				E.Code + ' - ' + E.Name as Entidad,
				[dbo].[Edad](convert(date,P.IPFECNACI),convert(date,getdate())) as Edad,
				A.[7] as FechaNacimiento,
				convert(int,0) as Generado
		FROM HCONCOPREG A with(nolock)
		    INNER JOIN INPACIENT P with(nolock) ON A.[6] = P.IPCODPACI
			INNER JOIN Contract.HealthAdministrator as E with(nolock) on E.Id = A.IDEntidadVIE  
			INNER JOIN (select distinct IPCODPACI from @tabla_ConsultasOncologicas) as GC on GC.IPCODPACI = A.[6] --and GC.CODDIAGNO = A.CODDIAGNO 
		WHERE A.[6] IN (SELECT Value FROM dbo.SplitString(@Identificacion)) 
	
END

--------------------------------------

/****** Object:  StoredProcedure [dbo].[SP_ONCO_ListadoPacientesCAC_SinGenerarSoporte]    Script Date: 12/16/2021 4:57:29 PM ******/
SET ANSI_NULLS ON
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de oncología que genera el soporte o evidencia de atención oncológica para uno o varios pacientes identificados por su cédula (o lista de cédulas), dentro de un rango de fechas. Consolida en una tabla temporal todos los pacientes que tuvieron al menos una de las siguientes actividades oncológicas: consulta externa con especialidad oncológica (usando historias clínicas HCHISPACA e INESPECIA), hospitalización atendida por médico oncólogo o hematólogo (cruzando HCHISPACA con INPROFSAL y ADINGRESO), informe quirúrgico oncológico (HCQXINFOR), aplicación de radioterapia externa o braquiterapia, aplicación de quimioterapia, o resultado de patología con diagnóstico de cáncer; en todos los casos validando que el paciente esté registrado como paciente oncológico en HCONCOPREG. Se utiliza en el módulo CAC (Cáncer de Alta Complejidad) para verificar y respaldar que un paciente tuvo atención oncológica efectiva en el período solicitado, apoyando procesos de auditoría, reporte a entidades y generación de soportes de facturación oncológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListadoPacientesCAC_GenerarSoportePorDocuemento';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListadoPacientesCAC_GenerarSoportePorDocuemento';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la lista de pacientes oncológicos (registrados en HCONCOPREG) cuyas identificaciones se reciben, que tuvieron al menos una atención oncológica efectiva (consulta, hospitalización, cirugía, radioterapia, braquiterapia, quimioterapia, patología o medicamento domiciliario) dentro del rango de fechas, para apoyar la generación de soportes CAC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoPacientesCAC_GenerarSoportePorDocuemento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las identificaciones se reciben como cadena delimitada y deben poder ser tokenizadas por dbo.SplitString.; El rango de fechas (inicial y final) debe ser válido para filtrar las atenciones.; El paciente debe existir en HCONCOPREG (registro oncológico) para ser considerado.; Los pacientes deben existir en INPACIENT y tener entidad asociada en Contract.HealthAdministrator.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoPacientesCAC_GenerarSoportePorDocuemento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve un único conjunto con datos del paciente oncológico (ID, tipo y número de identificación, nombre, entidad, edad, fecha de nacimiento y Generado=0) cuando el paciente está en la lista de identificaciones, existe en HCONCOPREG y aparece al menos una vez en la tabla temporal de atenciones oncológicas.; [INSERT] @tabla_ConsultasOncologicas: Cuando existe HCHISPACA con GENCONEXT=1 y especialidad con TIPESPECI entre 1 y 23 dentro del rango de fechas → se inserta el paciente como atención de consulta externa oncológica.; [INSERT] @tabla_ConsultasOncologicas: Cuando existe HCHISPACA cuyo profesional tiene TIPPROFES IN (1,6) y el ingreso ADINGRESO tiene UFUEGRHOS o UFUAACTHOS no nulos en el rango de fechas → se inserta el paciente como atención de hospitalización.; [INSERT] @tabla_ConsultasOncologicas: Cuando existe HCQXINFOR con FECHORINI dentro del rango → se inserta el paciente como atención de informe quirúrgico.; [INSERT] @tabla_ConsultasOncologicas: Cuando existe HCRADORDEN con ESTADO IN (3,4,5,6,7), esquema en HCRADESQUEMAS y CUPS con SERIPSDASH=6 en el rango → se inserta el paciente como aplicación de radioterapia externa.; [INSERT] @tabla_ConsultasOncologicas: Cuando existe HCRADORDEN con ESTADO IN (5,7,8) y CUPS con SERIPSDASH=13 en el rango → se inserta el paciente como aplicación de braquiterapia.; [INSERT] @tabla_ConsultasOncologicas: Cuando existe HCHOJAMED con IDHCORDQUIMIO no nulo, MEDESTADO=''2'' y FECAPLMED en el rango → se inserta el paciente como aplicación de quimioterapia.; [INSERT] @tabla_ConsultasOncologicas: Cuando existe HCORDPATO o AMBORDPAT con CAC23 no nulo, ESTSERIPS NOT IN (1,6), integrada con PathologyALULA y con report_base64 no nulo en el rango → se inserta el paciente como patología con cáncer.; [INSERT] @tabla_ConsultasOncologicas: Cuando existe dispensación farmacéutica (PharmaceuticalDispensing Status=2, AffectInventory=1, SurgeryExpenseSheetId nulo, EntityName=''SaveDashboardPharmacy'') ligada a HCFARMEPC/HCFARMEPD con ORDESTADO=2, PROESTADO=2 e IDHCORDPRON no nulo, y la orden está en el rango → se inserta el paciente como medicamento domiciliario.; [INSERT] @tabla_ConsultasOncologicas: Cuando existe HCCTRNOTE con TITNOTENF=''FINALIZACIÓN PREMATURA QUIMIOTERAPIA'' y FECREGIST en el rango → se inserta el paciente como nota de finalización prematura de quimioterapia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoPacientesCAC_GenerarSoportePorDocuemento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Especialidad con TIPESPECI IN (1..23) y HCHISPACA.GENCONEXT=1 → Considera la atención como consulta externa oncológica.; si ADINGRESO.UFUEGRHOS o UFUAACTHOS no nulos y profesional con TIPPROFES IN (1,6) → Considera la atención como hospitalización válida.; si HCRADORDEN.ESTADO IN (3,4,5,6,7) y SERIPSDASH=6 → Clasifica como radioterapia externa.; si HCRADORDEN.ESTADO IN (5,7,8) y SERIPSDASH=13 → Clasifica como braquiterapia.; si HCHOJAMED.IDHCORDQUIMIO IS NOT NULL y MEDESTADO=''2'' → Clasifica como aplicación efectiva de quimioterapia.; si Orden de patología con CAC23 no nulo, ESTSERIPS NOT IN (1,6) y reporte (report_base64) presente → Clasifica como patología oncológica reportada.; si ctr.ordertype=''INT'' vs ''AMB'' → Diferencia patología hospitalaria (HCORDPATO) de patología ambulatoria (AMBORDPAT).; si TITNOTENF=''FINALIZACIÓN PREMATURA QUIMIOTERAPIA'' → Considera la nota como evidencia de atención oncológica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoPacientesCAC_GenerarSoportePorDocuemento';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoPacientesCAC_GenerarSoportePorDocuemento';
-- GO

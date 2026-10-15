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
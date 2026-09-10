

-- =============================================
-- Author:		Kevin Garay
-- Create date: 19/05/2016
-- Description:	SP que lista los pacientes a reportar segun norma 2463
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ConsultarResolucion2463]
	    @CentroAtencion as varchar(20),
		@EAPB as varchar(20),
		@IdEntidad as varchar(20),
		@FechaInicial as datetime,
		@FechaFinal as datetime 
AS
BEGIN
	
	SET NOCOUNT ON;

  CREATE TABLE #tmpADRES2463D(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[1] [varchar](20) NULL,
	[2] [varchar](30) NULL,
	[3] [varchar](20) NULL,
	[4] [varchar](30) NULL,
	[5] [varchar](2) NULL,
	[6] [varchar](20) NULL,
	[7] [date] NULL,
	[8] [varchar](1) NULL,
	[9] [varchar](1) NULL,
	[10] [varchar](6) NULL,
	[11] [int] NULL,
	[12] [int] NULL,
	[13] [varchar](5) NULL,
	[14] [varchar](30) NULL,
	[15] [date] NULL,
	[16] [varchar](12) NULL,
	[17] [date] NULL,
	[18] [int] NULL,
	[19] [date] NULL,
	[19.1] [int] NULL,
	[20] [int] NULL,
	[21] [date] NULL,
	[21.1] [int] NULL,
	[22] [int] NULL,
	[23] [varchar](5) NULL,
	[24] [int] NULL,
	[25] [int] NULL,
	[26] [int] NULL,
	[27] [varchar](5) NULL,
	[27.1] [date] NULL,
	[28] [varchar](5) NULL,
	[28.1] [date] NULL,
	[29] [varchar](5) NULL,
	[29.1] [date] NULL,
	[30] [varchar](5) NULL,
	[30.1] [date] NULL,
	[31] [varchar](5) NULL,
	[31.1] [date] NULL,
	[32] [varchar](5) NULL,
	[32.1] [date] NULL,
	[33] [varchar](5) NULL,
	[33.1] [date] NULL,
	[34] [varchar](5) NULL,
	[34.1] [date] NULL,
	[35] [varchar](5) NULL,
	[36] [int] NULL,
	[37] [int] NULL,
	[38] [int] NULL,
	[39] [int] NULL,
	[40] [date] NULL,
	[41] [int] NULL,
	[42] [int] NULL,
	[43] [int] NULL,
	[44] [date] NULL,
	[45] [date] NULL,
	[46] [int] NULL,
	[47] [varchar](5) NULL,
	[48] [int] NULL,
	[49] [int] NULL,
	[50] [varchar](5) NULL,
	[51] [varchar](5) NULL,
	[52] [int] NULL,
	[53] [int] NULL,
	[54] [int] NULL,
	[55] [date] NULL,
	[56] [date] NULL,
	[57] [int] NULL,
	[58] [int] NULL,
	[59] [varchar](5) NULL,
	[60] [varchar](5) NULL,
	[61] [varchar](5) NULL,
	[62] [int] NULL,
	[62.1] [int] NULL,
	[62.2] [int] NULL,
	[62.3] [int] NULL,
	[62.4] [int] NULL,
	[62.5] [int] NULL,
	[62.6] [int] NULL,
	[62.7] [int] NULL,
	[62.8] [int] NULL,
	[62.9] [int] NULL,
	[62.10] [int] NULL,
	[62.11] [int] NULL,
	[63] [date] NULL,
	[63.1] [varchar](12) NULL,
	[64] [int] NULL,
	[65] [varchar](6) NULL,
	[66] [varchar](12) NULL,
	[67] [int] NULL,
	[68] [int] NULL,
	[69] [int] NULL,
	[69.1] [date] NULL,
	[69.2] [date] NULL,
	[69.3] [date] NULL,
	[69.4] [date] NULL,
	[69.5] [date] NULL,
	[69.6] [date] NULL,
	[69.7] [date] NULL,
	[70] [int] NULL,
	[70.1] [int] NULL,
	[70.2] [int] NULL,
	[70.3] [int] NULL,
	[70.4] [int] NULL,
	[70.5] [int] NULL,
	[70.6] [int] NULL,
	[70.7] [varchar](20) NULL,
	[70.8] [varchar](20) NULL,
	[70.9] [varchar](20) NULL,
	[71] [int] NULL,
	[72] [date] NULL,
	[73] [date] NULL,
	[74] [int] NULL,
	[75] [int] NULL,
	[76] [int] NULL,
	[77] [int] NULL,
	[78] [varchar](6) NULL,
	[79] [int] NULL,
	[80] [int] NULL,
	[80.1] [date] NULL,
	[IDFICHA] [int] NULL
 )
	------------pruebas-------------------------
	--declare @CentroAtencion as varchar(20) = '01'
	--declare @EAPB as varchar(20) = 'EPS003'
	--declare @IdEntidad as varchar(20) = '20'
	--declare @FechaInicial as date = '01/05/2016'
	--declare @FechaFinal as date = '30/06/2016'
	--------------------------------------------

	/*********************************************************************Ficha Renal - HCRENFICH************************************************************* */
	
	declare @INPACIENT as varchar(25)
	declare @22 as int
	declare @38 as int
	declare @39 as int
	declare @40 as date
	declare @41 as int 
	declare @42 as int
	declare @43 as int
	declare @44 as date
	declare @45 as date
	declare @46 as int
	declare @49 as int
	declare @54 as int
	declare @55 as date
	declare @56 as date
	declare @57 as int
	declare @62 as int
	declare @62_1 as int
	declare @62_2 as int
	declare @62_3 as int
	declare @62_4 as int
	declare @62_5 as int
	declare @62_6 as int
	declare @62_7 as int
	declare @62_8 as int
	declare @62_9 as int
	declare @62_10 as int
	declare @62_11 as int
	declare @63 as date
	declare @63_1 as varchar(12)
	declare @64 as int
	declare @66 as varchar(12)
	declare @16 as varchar(12)
	declare @18 as int
	declare @19 as datetime
	declare @20 as int
	declare @21 as datetime
	declare @IDFICHA as int

	DECLARE CursorFichaRenal CURSOR FOR 
	SELECT P.IPCODPACI,
			A.ETILOGIA, A.ERC,A.ESTADIO, A.FECDIAGNO,CASE PROGATERC WHEN 11 then 1 else PROGATERC end as PROGATERC,A.TFGINITRR,
			A.MODINITRR,A.FECHINITRR,A.FECINGUREN, A.TRRHEMODIA, A.TRRDIAPERI, A.VACUHEPA, A.FECDIAGHC, A.FECDIAGHC, A.TRRTERNODIA, A.INDITRANS,A.ITCANACT, A.ITINFCRO, A.ITNOTRANS,
			A.ITESPVIDA, A.ITPOTILIM, A.ITENFCARD, A.ITINFEVIH, A.ITINFEVHC, A.ITENFINMU, A.ITENFPULM, A.ITOTRENFC,A.FECINGLIES, A.IPSLISESPE,A.RECTRAREN,A.IPSREATRA,CEN.CODIPSSEC,A.HTA,A.FECDIAGHTA,
			A.DM,A.FECDIAGDM,A.ID
	FROM HCRENFICH AS A with(nolock)
	INNER JOIN INPACIENT AS P with(nolock) ON P.IPCODPACI = A.IPCODPACI 
	LEFT JOIN COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = P.CCCONTRAT
	left join INENTADM AS ENT with(nolock) ON ENT.CODENTADM = CONT.CODENTADM
	INNER JOIN ADCENATEN AS CEN with(nolock) ON CEN.CODCENATE = A.CODCENATE
	where (A.CODCENATE  = @CentroAtencion AND A.FECREGISTRO between @FechaInicial and @FechaFinal) -- AND (CONT.CODENTADM = @EAPB or I.GENCONENTITY = @IdEntidad)
	order by A.FECREGISTRO ASC

	OPEN CursorFichaRenal
		FETCH NEXT FROM CursorFichaRenal
		INTO @INPACIENT, @22, @38, @39,@40,@41,@42,@43,@44,@45,@46,@49,@54,@55,@56,@57,@62,@62_1,@62_2,@62_3,@62_4,@62_5,@62_6,@62_7,@62_8,@62_9,@62_10,@62_11,@63,@63_1,@64,@66,@16,@18,@19,@20,@21,@IDFICHA

		WHILE @@FETCH_STATUS = 0
		BEGIN
			if(select count(*) from #tmpADRES2463D where [6] = @INPACIENT) > 0 begin
			print 'update'
				update #tmpADRES2463D
				set [22] = case when @22 is null then [22] else @22 end,
					[38] = case when @38 is null then [38] else @38 end,
					[39] = case when @39 is null then [39] else @39 end,
					[40] = case when @40 is null then [40] else @40 end,
					[41] = case when @41 is null then [41] else @41 end,
					[42] = case when @42 is null then [42] else @42 end,
					[43] = case when @43 is null then [43] else @43 end,
					[44] = case when @44 is null then [44] else @44 end,
					[45] = case when @45 is null then [45] else @45 end,
					[46] = case when @46 is null then [46] else @46 end,
					[49] = case when @49 is null then [49] else @49 end,
					[54] = case when @54 is null then [54] else @54 end,
					[55] = case when @55 is null then [55] else @55 end,
					[56] = case when @56 is null then [56] else @56 end,
					[57] = case when @57 is null then [57] else @57 end,
					[62] = case when @62 is null then [62] else @62 end,
					[62.1] = case when @62_1 is null then [62.1] else @62_1 end,
					[62.2] = case when @62_2 is null then [62.2] else @62_2 end,
					[62.3] = case when @62_3 is null then [62.3] else @62_3 end,
					[62.4] = case when @62_4 is null then [62.4] else @62_4 end,
					[62.5] = case when @62_5 is null then [62.5] else @62_5 end,
					[62.6] = case when @62_6 is null then [62.6] else @62_6 end,
					[62.7] = case when @62_7 is null then [62.7] else @62_7 end,
					[62.8] = case when @62_8 is null then [62.8] else @62_8 end,
					[62.9] = case when @62_9 is null then [62.9] else @62_9 end,
					[62.10] = case when @62_10 is null then [62.10] else @62_10 end,
					[62.11] = case when @62_11 is null then [62.11] else @62_11 end,
					[63] = case when @63 is null then [63] else @63 end,
					[63.1] = case when @63_1 is null then [63.1] else @63_1 end,
					[64] = case when @64 is null then [64] else @64 end,
					[66] = case when @66 is null then [66] else @66 end,
					[16] = case when @16 is null then [16] else @16 end,
					[18] = case when @18 is null then [18] else @18 end,
					[19] = case when @19 is null then [19] else @19 end,
					[20] = case when @20 is null then [20] else @20 end,
					[21] = case when @21 is null then [21] else @21 end,
					[IDFICHA] = case when @IDFICHA is null then [IDFICHA] else @IDFICHA end

				where [6] = @INPACIENT
			end else begin
			print 'insert'
				insert into #tmpADRES2463D([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[78],[22],[38],[39],[40],[41],[42],[43],[44],[45],[46],[49],[54],[55],[56],[57],[62],[62.1],[62.2],[62.3],[62.4],[62.5],
				[62.6],[62.7],[62.8],[62.9],[62.10],[62.11],[63],[63.1],[64],[66],[16],[18],[19],[20],[21],[IDFICHA])
				SELECT IPPRINOMB,case convert(nvarchar(50),IPSEGNOMB) when null then 'NONE' when '' then 'NONE' else IPSEGNOMB END as IPSEGNOMB,
						IPPRIAPEL, case convert(nvarchar(50),IPSEGAPEL) when null then 'NONE' when '' then 'NONE' else IPSEGAPEL END as IPSEGAPEL,
						case P.IPTIPODOC when 3 then 'TI'
											when 2 then 'CE'
											when 1 then 'CC'
											when 4 then 'RC'
											when 5 then 'PA'
											when 7 then 'MS'
											when 8 then 'NV'
											when 6 then 'AS' END AS TIPOIDEN,
						P.IPCODPACI, IPFECNACI, 
						case P.IPSEXOPAC when  1 then 'M'
											when  2 then 'F' END AS IPSEXOPAC, 
						case ENT.REGIMEN when 'C' then 'C'
											when 'S' then 'S'
											when 'E' then 'P'
											when 'P' then 'E' END AS REGIMEN,
						ENT.CODSUPERINTEN,GE.CODGRUPOE, 
						(select top 1 TIPOPOBESP from ADPOBESPEPAC POBP with(nolock) INNER JOIN ADPOBESPE POB with(nolock) ON POB.ID = POBP.IDADPOBESPE) AS POBESP,
						UB.DEPMUNCOD,P.IPTELEFON + ' - ' + P.IPTELMOVI,ENT.CODSUPERINTEN,
						@22, @38, @39,@40,@41,@42,@43,@44,@45,@46,@49,@54,@55,@56,@57,@62,@62_1,@62_2,@62_3,@62_4,@62_5,@62_6,@62_7,@62_8,@62_9,@62_10,@62_11,@63,@63_1,@64,@66,@16,@18,@19,@20,@21,@IDFICHA
				FROM INPACIENT AS P with(nolock) 
				LEFT JOIN ADGRUETNI AS GE with(nolock) ON GE.CODGRUPOE = P.CODGRUPOE
				INNER JOIN ADACTIVID AS ACT with(nolock) ON ACT.codactivi = P.CODACTIVI 
				LEFT JOIN ADNIVELED AS NIV with(nolock) ON NIV.NIVECODIGO = P.NIVECODIGO 
				LEFT JOIN COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = P.CCCONTRAT
				left join INENTADM AS ENT with(nolock) ON ENT.CODENTADM = CONT.CODENTADM
				INNER JOIN INUBICACI UB ON P.AUUBICACI = UB.AUUBICACI
				where P.IPCODPACI = @INPACIENT
			end
		FETCH NEXT FROM CursorFichaRenal
		INTO @INPACIENT, @22, @38, @39,@40,@41,@42,@43,@44,@45,@46,@49,@54,@55,@56,@57,@62,@62_1,@62_2,@62_3,@62_4,@62_5,@62_6,@62_7,@62_8,@62_9,@62_10,@62_11,@63,@63_1,@64,@66,@16,@18,@19,@20,@21,@IDFICHA
		END
	CLOSE CursorFichaRenal
	DEALLOCATE CursorFichaRenal

	/********************************************************************* Diagnosticos - HCPLANTIPIF************************************************************* */
	--18,19,20,21,38,40
	declare @FECDIAGNO as datetime
	declare @CODTIPIFI as int

	DECLARE CursorDiagnosticos CURSOR FOR 
	select D.IPCODPACI,D.FECDIAGNO,TIP.CODTIPIFI from 
	HCPLANTIPIF AS TIP with(nolock) INNER JOIN 
	INDIAGNOP AS D with(nolock) ON D.CODDIAGNO = TIP.CODDIAGNO INNER JOIN
	ADINGRESO AS I with(nolock) ON I.NUMINGRES = D.NUMINGRES LEFT JOIN
	COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = I.CODCONTRA left join 
	INENTADM AS ENT with(nolock) ON ENT.CODENTADM = CONT.CODENTADM
	where (D.CODCENATE  = @CentroAtencion AND D.FECDIAGNO between @FechaInicial and @FechaFinal) AND TIP.CODTIPIFI IN(36,37,38) AND (CONT.CODENTADM = @EAPB or I.GENCONENTITY = @IdEntidad)
	order by D.FECDIAGNO ASC

	OPEN CursorDiagnosticos
		FETCH NEXT FROM CursorDiagnosticos
		INTO @INPACIENT, @FECDIAGNO, @CODTIPIFI

		WHILE @@FETCH_STATUS = 0
		BEGIN
			if(select count(*) from #tmpADRES2463D where [6] = @INPACIENT) > 0 begin
			print 'update d'
				update #tmpADRES2463D
				set [18] = case when @CODTIPIFI = 36 then 1 else [18] end,
					[19] = case when @CODTIPIFI = 36 then @FECDIAGNO else [19] end,
					[20] = case when @CODTIPIFI = 37 then 1 else [20] end,
					[21] = case when @CODTIPIFI = 37 then @FECDIAGNO else [21] end,
					[38] = case when @CODTIPIFI = 38 then 1 else [38] end,
					[40] = case when @CODTIPIFI = 38 then @FECDIAGNO else [40] end

				where [6] = @INPACIENT
			end else begin
			print 'insert d'
				insert into #tmpADRES2463D([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[78],[18],[19],[20],[21],[38],[40])
				SELECT IPPRINOMB,case convert(nvarchar(50),IPSEGNOMB) when null then 'NONE' when '' then 'NONE' else IPSEGNOMB END as IPSEGNOMB,
						IPPRIAPEL, case convert(nvarchar(50),IPSEGAPEL) when null then 'NONE' when '' then 'NONE' else IPSEGAPEL END as IPSEGAPEL,
						case P.IPTIPODOC when 3 then 'TI'
											when 2 then 'CE'
											when 1 then 'CC'
											when 4 then 'RC'
											when 5 then 'PA'
											when 7 then 'MS'
											when 8 then 'NV'
											when 6 then 'AS' END AS TIPOIDEN,
						P.IPCODPACI, IPFECNACI, 
						case P.IPSEXOPAC when  1 then 'M'
											when  2 then 'F' END AS IPSEXOPAC, 
						case ENT.REGIMEN when 'C' then 'C'
											when 'S' then 'S'
											when 'E' then 'P'
											when 'P' then 'E' END AS REGIMEN,
						ENT.CODSUPERINTEN,GE.CODGRUPOE, 
						(select top 1 TIPOPOBESP from ADPOBESPEPAC POBP with(nolock) INNER JOIN ADPOBESPE POB with(nolock) ON POB.ID = POBP.IDADPOBESPE) AS POBESP,
						UB.DEPMUNCOD,P.IPTELEFON + ' - ' + P.IPTELMOVI,ENT.CODSUPERINTEN,
						case when @CODTIPIFI = 36 then 1 end, case when @CODTIPIFI = 36 then @FECDIAGNO end, case when @CODTIPIFI = 37 then 1 end, case when @CODTIPIFI = 37 then @FECDIAGNO end,
						case when @CODTIPIFI = 38 then 1 end, case when @CODTIPIFI = 38 then @FECDIAGNO end
				FROM INPACIENT AS P with(nolock) 
				LEFT JOIN ADGRUETNI AS GE with(nolock) ON GE.CODGRUPOE = P.CODGRUPOE
				INNER JOIN ADACTIVID AS ACT with(nolock) ON ACT.codactivi = P.CODACTIVI 
				LEFT JOIN ADNIVELED AS NIV with(nolock) ON NIV.NIVECODIGO = P.NIVECODIGO 
				LEFT JOIN COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = P.CCCONTRAT
				left join INENTADM AS ENT with(nolock) ON ENT.CODENTADM = CONT.CODENTADM
				INNER JOIN INUBICACI UB ON P.AUUBICACI = UB.AUUBICACI
				where P.IPCODPACI = @INPACIENT
			end
		FETCH NEXT FROM CursorDiagnosticos
		INTO @INPACIENT, @FECDIAGNO, @CODTIPIFI
		END
	CLOSE CursorDiagnosticos
	DEALLOCATE CursorDiagnosticos

	/********************************************************************* Novedades - HCNOVFICREN************************************************************* */
	declare @MOTNOVEDA as int
	declare @FECNOVEDA as date
	declare @CAUMUERTE as int 
	declare @FECMUERTE as date

	DECLARE CursorNovedades CURSOR FOR 
	SELECT P.IPCODPACI,NOV.MOTNOVEDA,FECNOVEDA,CAUMUERTE,FECMUERTE
	FROM HCNOVFICREN NOV INNER JOIN HCRENFICH AS A with(nolock) ON A.ID = NOV.IDFICHAR
	INNER JOIN INPACIENT AS P with(nolock) ON P.IPCODPACI = A.IPCODPACI 
	LEFT JOIN COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = P.CCCONTRAT
	left join INENTADM AS ENT with(nolock) ON ENT.CODENTADM = CONT.CODENTADM
	where (A.CODCENATE  = @CentroAtencion AND A.FECREGISTRO between @FechaInicial and @FechaFinal) AND (CONT.CODENTADM = @EAPB) 
	order by A.FECREGISTRO ASC
	
	OPEN CursorNovedades
		FETCH NEXT FROM CursorNovedades
		INTO @INPACIENT, @MOTNOVEDA, @FECNOVEDA, @CAUMUERTE, @FECMUERTE

		WHILE @@FETCH_STATUS = 0
		BEGIN
			if(select count(*) from #tmpADRES2463D where [6] = @INPACIENT) > 0 begin
			print 'update n'
				update #tmpADRES2463D
				set [73] = case when @MOTNOVEDA = 9 then @FECNOVEDA else [73] end,
					[79] = @MOTNOVEDA,
					[80] = case when @MOTNOVEDA = 1 then @CAUMUERTE else [80] end,
					[80.1] = case when @MOTNOVEDA = 1 then @FECMUERTE else [80.1] end

				where [6] = @INPACIENT
			end else begin
			print 'insert n'
				insert into #tmpADRES2463D([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[78],[73],[79],[80],[80.1])
				SELECT IPPRINOMB,case convert(nvarchar(50),IPSEGNOMB) when null then 'NONE' when '' then 'NONE' else IPSEGNOMB END as IPSEGNOMB,
						IPPRIAPEL, case convert(nvarchar(50),IPSEGAPEL) when null then 'NONE' when '' then 'NONE' else IPSEGAPEL END as IPSEGAPEL,
						case P.IPTIPODOC when 3 then 'TI'
											when 2 then 'CE'
											when 1 then 'CC'
											when 4 then 'RC'
											when 5 then 'PA'
											when 7 then 'MS'
											when 8 then 'NV'
											when 6 then 'AS' END AS TIPOIDEN,
						P.IPCODPACI, IPFECNACI, 
						case P.IPSEXOPAC when  1 then 'M'
											when  2 then 'F' END AS IPSEXOPAC, 
						case ENT.REGIMEN when 'C' then 'C'
											when 'S' then 'S'
											when 'E' then 'P'
											when 'P' then 'E' END AS REGIMEN,
						ENT.CODSUPERINTEN,GE.CODGRUPOE, 
						(select top 1 TIPOPOBESP from ADPOBESPEPAC POBP with(nolock) INNER JOIN ADPOBESPE POB with(nolock) ON POB.ID = POBP.IDADPOBESPE) AS POBESP,
						UB.DEPMUNCOD,P.IPTELEFON + ' - ' + P.IPTELMOVI,ENT.CODSUPERINTEN,
						case when @MOTNOVEDA = 9 then @FECNOVEDA end, @MOTNOVEDA, case when @MOTNOVEDA = 1 then @CAUMUERTE end,case when @MOTNOVEDA = 1 then @FECMUERTE end
				FROM INPACIENT AS P with(nolock) 
				LEFT JOIN ADGRUETNI AS GE with(nolock) ON GE.CODGRUPOE = P.CODGRUPOE
				INNER JOIN ADACTIVID AS ACT with(nolock) ON ACT.codactivi = P.CODACTIVI 
				LEFT JOIN ADNIVELED AS NIV with(nolock) ON NIV.NIVECODIGO = P.NIVECODIGO 
				LEFT JOIN COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = P.CCCONTRAT
				left join INENTADM AS ENT with(nolock) ON ENT.CODENTADM = CONT.CODENTADM
				INNER JOIN INUBICACI UB ON P.AUUBICACI = UB.AUUBICACI
				where P.IPCODPACI = @INPACIENT
			end
		FETCH NEXT FROM CursorNovedades
		INTO @INPACIENT, @MOTNOVEDA, @FECNOVEDA, @CAUMUERTE, @FECMUERTE
		END
	CLOSE CursorNovedades
	DEALLOCATE CursorNovedades

	
	------------------------------------------------------------INTERCTRL-INTERLABD------------------------------------- Tabla cuando tenemos interfaz. Cuando el parametro esta en 1 campo INTLABOAC tabla HCPARLABO

	DECLARE @PARAMETRO AS bit = (SELECT INTLABOAC from HCPARLABO WHERE CODCENATE = @CentroAtencion)
 IF @PARAMETRO = 1 
	BEGIN
		DECLARE @TIPCREATI AS int --Creatinina : 1-Si 0-No										27-27.1
		DECLARE @TIPHEMGLI AS int --Hemoglobina Glicosilada: 1-Si 0-No							28-28.1
		DECLARE @TIPMICROA as int --Microalbuminuria : 1-Si  0-No								29-29.1
		DECLARE @CREATINURIA as int --Creatinuría : 1-Si  0-No									30-30.1
		DECLARE @COLETOTAL as int --Colesterol total : 1-Si  0-No								31-31.1
		DECLARE @TIPHDL as int -- HDL : 1-Si  0-No												32-32.1
		DECLARE @LDL as int -- LDL : 1-Si  0-No													33-33.1	
		DECLARE @PTH as int -- PTH : 1-Si  0-No													34-34.1	
		DECLARE @ALBUMSERICA as int -- Albúmina Sérica  : 1-Si  0-No							60
		DECLARE @ALBUMFOSFO as int -- Fósforo : 1-Si  0-No										61
		DECLARE @FECREGIST AS date
		DECLARE @VALOR AS VARCHAR(MAX)
		DECLARE @CODPACIENTE AS VARCHAR(15)

		DECLARE CursorLaboratoriosInterfaz CURSOR FOR 	
		
			SELECT L.IPCODPACI, C.FECREGIST, R.VALOR, S.TIPCREATI, S.TIPHEMGLI, S.TIPMICROA, S.CREATINURIA, S.COLETOTAL, S.TIPHDL, S.LDL, S.PTH, S.ALBUMSERICA, S.ALBUMFOSFO FROM INTERLABD R 
				INNER JOIN HCORDLABO AS L with(nolock) ON L.AUTO = R.AUTOLABOR 
				INNER JOIN INCUPSIPS AS S with(nolock) ON S.CODSERIPS = L.CODSERIPS  
				INNER JOIN INTERCTRL AS C with(nolock) ON C.AUTOLABOR = L.AUTO
				INNER JOIN ADINGRESO AS I with(nolock) ON I.NUMINGRES = L.NUMINGRES 
				LEFT  JOIN COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = I.CODCONTRA
				LEFT  JOIN HCRIESGOSP AS RP with(nolock) ON I .IPCODPACI = RP.IPCODPACI 
			WHERE (S.TIPCREATI = 1 OR S.TIPHEMGLI = 1 OR S.TIPMICROA = 1 OR S.CREATINURIA = 1 OR S.COLETOTAL = 1 OR S.TIPHDL = 1 OR S.LDL = 1 OR S.PTH = 1 OR S.ALBUMSERICA = 1 OR S.ALBUMFOSFO = 1)
				   AND  I.CODCENATE = @CentroAtencion AND  (CONT.CODENTADM = @EAPB OR I.GENCONENTITY = @IdEntidad) AND C.FECREGIST  between @FechaInicial and @FechaFinal 
			UNION all
			SELECT L.IPCODPACI, C.FECREGIST, R.VALOR, S.TIPCREATI, S.TIPHEMGLI, S.TIPMICROA, S.CREATINURIA, S.COLETOTAL, S.TIPHDL, S.LDL, S.PTH, S.ALBUMSERICA, S.ALBUMFOSFO FROM INTERLABD R 
				INNER JOIN AMBORDLAB AS L with(nolock) ON L.AUTO = R.AUTOLABOR 
				INNER JOIN INCUPSIPS AS S with(nolock) ON S.CODSERIPS = L.CODSERIPS  
				INNER JOIN INTERCTRL AS C with(nolock) ON C.AUTOLABOR = L.AUTO
				INNER JOIN ADINGRESO AS I with(nolock) ON L.NUMINGRES = I.NUMINGRES 
				LEFT  JOIN COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = I.CODCONTRA
				LEFT  JOIN HCRIESGOSP AS RP with(nolock) ON I.IPCODPACI = RP.IPCODPACI 
			WHERE (S.TIPCREATI = 1 OR S.TIPHEMGLI = 1 OR S.TIPMICROA = 1 OR S.CREATINURIA = 1 OR S.COLETOTAL = 1 OR S.TIPHDL = 1 OR S.LDL = 1 OR S.PTH = 1 OR S.ALBUMSERICA = 1 OR S.ALBUMFOSFO = 1)
					AND  I.CODCENATE = @CentroAtencion AND  (CONT.CODENTADM = @EAPB OR I.GENCONENTITY = @IdEntidad) AND C.FECREGIST  between @FechaInicial and @FechaFinal 
			order by FECREGIST ASC

			 			
			OPEN CursorLaboratoriosInterfaz
				FETCH NEXT FROM CursorLaboratoriosInterfaz
				INTO @CODPACIENTE, @FECREGIST, @VALOR,@TIPCREATI, @TIPHEMGLI, @TIPMICROA, @CREATINURIA, @COLETOTAL, @TIPHDL, @LDL, @PTH, @ALBUMSERICA, @ALBUMFOSFO

		WHILE @@FETCH_STATUS = 0
				BEGIN	
				
			    if (select count(*) from #tmpADRES2463D where [6] = @INPACIENT) > 0 begin
					--si el paciente ya esta registrado	procedemos actualizar Los campos
					update #tmpADRES2463D
					set  [27] = CASE WHEN @TIPCREATI = 1 THEN @VALOR ELSE [27] END, 
						 [27.1] = CASE WHEN @TIPCREATI = 1 THEN @FECREGIST ELSE [27.1] END,
						 [28] = CASE WHEN @TIPHEMGLI = 1 THEN @VALOR ELSE [28] END, 
						 [28.1] = CASE WHEN @TIPHEMGLI = 1 THEN @FECREGIST ELSE [28.1] END,
						 [29] = CASE WHEN @TIPMICROA = 1 THEN @VALOR ELSE [29] END, 
						 [29.1] = CASE WHEN @TIPMICROA = 1 THEN @FECREGIST ELSE [29.1] END,
						 [30] = CASE WHEN @CREATINURIA = 1 THEN @VALOR ELSE [30] END, 
						 [30.1] = CASE WHEN @CREATINURIA = 1 THEN @FECREGIST ELSE [30.1] END,
						 [31] = CASE WHEN @COLETOTAL = 1 THEN @VALOR ELSE [31] END, 
						 [31.1] = CASE WHEN @COLETOTAL = 1 THEN @FECREGIST ELSE [31.1] END,
						 [32] = CASE WHEN @TIPHDL = 1 THEN @VALOR ELSE [32] END, 
						 [32.1] = CASE WHEN @TIPHDL = 1 THEN @FECREGIST ELSE [32.1] END,
						 [33] = CASE WHEN @LDL = 1 THEN @VALOR ELSE [33] END, 
						 [33.1] = CASE WHEN @LDL = 1 THEN @FECREGIST ELSE [33.1] END,
						 [34] = CASE WHEN @PTH = 1 THEN @VALOR ELSE [34] END, 
						 [34.1] = CASE WHEN @PTH = 1 THEN @FECREGIST ELSE [34.1] END,
						 [60] = CASE WHEN @ALBUMSERICA = 1 THEN @VALOR ELSE [60] END, 
						 [61] = CASE WHEN @ALBUMFOSFO = 1 THEN @VALOR ELSE [61] END

						 where [6] =  @CODPACIENTE 
						 PRINT 'Modifico l'
				end else begin

				--si el paciente no esta registrado, procedemos a registrarlo
					print 'insert'
					insert into #tmpADRES2463D([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[78],[27],[27.1],[28],[28.1],[29],[29.1],[30],[30.1],[31],[31.1],[32],[32.1],[33],[33.1],[34],[34.1],[60],[61])
					SELECT IPPRINOMB,case convert(nvarchar(50),IPSEGNOMB) when null then 'NONE' when '' then 'NONE' else IPSEGNOMB END as IPSEGNOMB,
						IPPRIAPEL, case convert(nvarchar(50),IPSEGAPEL) when null then 'NONE' when '' then 'NONE' else IPSEGAPEL END as IPSEGAPEL,
						case P.IPTIPODOC when 3 then 'TI'
											when 2 then 'CE'
											when 1 then 'CC'
											when 4 then 'RC'
											when 5 then 'PA'
											when 7 then 'MS'
											when 8 then 'NV'
											when 6 then 'AS' END AS TIPOIDEN,
						P.IPCODPACI, IPFECNACI, 
						case P.IPSEXOPAC when  1 then 'M'
											when  2 then 'F' END AS IPSEXOPAC, 
						case ENT.REGIMEN when 'C' then 'C'
											when 'S' then 'S'
											when 'E' then 'P'
											when 'P' then 'E' END AS REGIMEN,
						ENT.CODSUPERINTEN,GE.CODGRUPOE, 
						(select top 1 TIPOPOBESP from ADPOBESPEPAC POBP with(nolock) INNER JOIN ADPOBESPE POB with(nolock) ON POB.ID = POBP.IDADPOBESPE) AS POBESP,
						UB.DEPMUNCOD,P.IPTELEFON + ' - ' + P.IPTELMOVI,ENT.CODSUPERINTEN,
						CASE WHEN @TIPCREATI = 1 THEN @VALOR END, 
							CASE WHEN @TIPCREATI = 1 THEN @FECREGIST END,
							CASE WHEN @TIPHEMGLI = 1 THEN @VALOR END, 
							CASE WHEN @TIPHEMGLI = 1 THEN @FECREGIST END,
							CASE WHEN @TIPMICROA = 1 THEN @VALOR END, 
							CASE WHEN @TIPMICROA = 1 THEN @FECREGIST END,
							CASE WHEN @CREATINURIA = 1 THEN @VALOR END, 
							CASE WHEN @CREATINURIA = 1 THEN @FECREGIST END,
							CASE WHEN @COLETOTAL = 1 THEN @VALOR END, 
							CASE WHEN @COLETOTAL = 1 THEN @FECREGIST END,
							CASE WHEN @TIPHDL = 1 THEN @VALOR END, 
							CASE WHEN @TIPHDL = 1 THEN @FECREGIST END,
							CASE WHEN @LDL = 1 THEN @VALOR END, 
							CASE WHEN @LDL = 1 THEN @FECREGIST END,
							CASE WHEN @PTH = 1 THEN @VALOR END, 
							CASE WHEN @PTH = 1 THEN @FECREGIST END,
							CASE WHEN @ALBUMSERICA = 1 THEN @VALOR END, 
							CASE WHEN @ALBUMFOSFO = 1 THEN @VALOR END
					FROM INPACIENT AS P with(nolock) 
					LEFT JOIN ADGRUETNI AS GE with(nolock) ON GE.CODGRUPOE = P.CODGRUPOE
					INNER JOIN ADACTIVID AS ACT with(nolock) ON ACT.codactivi = P.CODACTIVI 
					LEFT JOIN ADNIVELED AS NIV with(nolock) ON NIV.NIVECODIGO = P.NIVECODIGO 
					LEFT JOIN COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = P.CCCONTRAT
					left join INENTADM AS ENT with(nolock) ON ENT.CODENTADM = CONT.CODENTADM
					INNER JOIN INUBICACI UB ON P.AUUBICACI = UB.AUUBICACI
					where P.IPCODPACI = @INPACIENT
				END	

					FETCH NEXT FROM CursorLaboratoriosInterfaz
					INTO @CODPACIENTE, @FECREGIST, @VALOR,@TIPCREATI, @TIPHEMGLI, @TIPMICROA, @CREATINURIA, @COLETOTAL, @TIPHDL, @LDL, @PTH, @ALBUMSERICA, @ALBUMFOSFO
				END 
			PRINT ' Hay iter Laboratorio'
				CLOSE CursorLaboratoriosInterfaz
				DEALLOCATE CursorLaboratoriosInterfaz
	  END
 ELSE
	 BEGIN
		
			DECLARE @RESCREATININA as decimal(18,2) --Resultado Creatinina 27
			DECLARE @FECRESCREATININA as datetime --Fecha Creatinina 27.1--
			DECLARE @RESHEMGLO as decimal(18, 2) --Hemoglobina Glicosilada: Valor mínimo 5 y máximo 20, permitir decimales 28
			DECLARE @FECRESHEMGLO as datetime ----Hemoglobina Glicosilada: fecha 28.1--
			DECLARE @RESMICROALBU as decimal(18,2) --RESULTADO DE Microalbuminuria 29 
			DECLARE @FECMICROALBU as datetime --FECHA DE RESULTADO DE Microalbuminuria 29.1--
			DECLARE @RESCREATINUR as decimal(18,2) --Resultado de la Creatinuria (Longitud 5) (Unidades: mg/dl) 30
			DECLARE @FECCREATINUR as datetime --Fecha de la Creatinuria (Longitud 5) (Unidades: mg/dl) 30.1--
			DECLARE @RESCOLESTOTAL as decimal(18, 2) --Resultado de Colesterol Total (Longitud 5) (Unidades: mg/dl) 31
			DECLARE @FECCOLESTOTAL as datetime --Fecha de Colesterol Total (Longitud 5) (Unidades: mg/dl)31.1--
			DECLARE @RESHDL as decimal(18, 2) --Resultado HDL  (Longitud 5) (Unidades: mg/dl) 32
			DECLARE @FECHDL as datetime --FECHA DE RESULTADO DE HDL 32.1--
			DECLARE @RESLDL as decimal(18, 2) --Resultado LDL (Longitud 5) (Unidades: mg/dl) 33
			DECLARE @FECLDL as datetime --Fecha LDL 33.1--
			DECLARE @RESPTH as decimal(18, 2) --Resultado PTH 34
			DECLARE @FECPTH as datetime --Fecha PTH 34.1--
			DECLARE @RESALBUSERICA as decimal(18, 2) --Resultado Albumina Serica (Longitud 5) (Unidades: g/dl) 60
			DECLARE @RESFOSFORO as decimal(18, 2) --Resultado  Fosforo (Longitud 5) (Unidades: mg/dl) 61

			DECLARE CursorLaboratorosNoInterfaz CURSOR FOR
				
				SELECT HL.IPCODPACI,RESCREATININA, FECRESCREATININA, RESHEMGLO, FECRESHEMGLO,RESMICROALBU, FECMICROALBU, RESCREATINUR, FECCREATINUR, RESCOLESTOTAL, FECCOLESTOTAL, RESHDL, FECHDL, RESLDL, FECLDL, RESPTH,
						FECPTH, RESALBUSERICA,RESFOSFORO
					FROM HCORDLABO AS HL with(nolock)
						INNER JOIN ADINGRESO AS I with(nolock) ON HL.NUMINGRES  = I.NUMINGRES 
						INNER JOIN INPACIENT AS P with(nolock) ON P.IPCODPACI = HL.IPCODPACI 
						INNER JOIN ADINGRESO AS ING with(nolock) ON ING.NUMINGRES = HL.NUMINGRES
						LEFT  JOIN COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = ING.CODCONTRA
						LEFT JOIN HCRIESGOSP AS RP with(nolock) ON P.IPCODPACI = RP.IPCODPACI 
				 WHERE   I.CODCENATE = @CentroAtencion  AND  (CONT.CODENTADM = @EAPB or ING.GENCONENTITY = @IdEntidad) and (
					FECRESCREATININA between @FechaInicial and @FechaFinal or FECRESHEMGLO between @FechaInicial and @FechaFinal or FECMICROALBU between @FechaInicial and @FechaFinal
					or FECCREATINUR between @FechaInicial and @FechaFinal or FECCOLESTOTAL between @FechaInicial and @FechaFinal 
					or FECHDL between @FechaInicial and @FechaFinal or FECLDL between @FechaInicial and @FechaFinal or FECPTH between @FechaInicial and @FechaFinal
					or FECALBUSERICA between @FechaInicial and @FechaFinal or FECFOSFOR between @FechaInicial and @FechaFinal
				 )  
				UNION ALL 
				SELECT AL.IPCODPACI,RESCREATININA, FECRESCREATININA, RESHEMGLO, FECRESHEMGLO,RESMICROALBU, FECMICROALBU, RESCREATINUR, FECCREATINUR, RESCOLESTOTAL, FECCOLESTOTAL, RESHDL, FECHDL, RESLDL, FECLDL, RESPTH,
						FECPTH, RESALBUSERICA,RESFOSFORO
					FROM AMBORDLAB AS AL with(nolock) 
						INNER JOIN ADINGRESO AS I with(nolock) ON AL.NUMINGRES  = I.NUMINGRES 
						INNER JOIN INPACIENT AS P with(nolock) ON P.IPCODPACI = AL.IPCODPACI 
						INNER JOIN ADINGRESO AS ING with(nolock) ON ING.NUMINGRES = AL.NUMINGRES
						LEFT  JOIN COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = ING.CODCONTRA
						LEFT JOIN HCRIESGOSP AS RP with(nolock) ON P.IPCODPACI = RP.IPCODPACI
						WHERE   I.CODCENATE = @CentroAtencion AND (CONT.CODENTADM = @EAPB or ING.GENCONENTITY = @IdEntidad) and (
					FECRESCREATININA between @FechaInicial and @FechaFinal or FECRESHEMGLO between @FechaInicial and @FechaFinal or FECMICROALBU between @FechaInicial and @FechaFinal
					or FECCREATINUR between @FechaInicial and @FechaFinal or FECCOLESTOTAL between @FechaInicial and @FechaFinal 
					or FECHDL between @FechaInicial and @FechaFinal or FECLDL between @FechaInicial and @FechaFinal or FECPTH between @FechaInicial and @FechaFinal
					or FECALBUSERICA between @FechaInicial and @FechaFinal or FECFOSFOR between @FechaInicial and @FechaFinal
				 )  
				ORDER BY FECRESCREATININA ASC, FECRESHEMGLO ASC, FECMICROALBU ASC,FECCREATINUR ASC,FECCOLESTOTAL ASC,FECHDL ASC,FECLDL ASC,FECPTH ASC

				OPEN CursorLaboratorosNoInterfaz
				FETCH NEXT FROM CursorLaboratorosNoInterfaz
				INTO @INPACIENT,@RESCREATININA, @FECRESCREATININA, @RESHEMGLO, @FECRESHEMGLO,@RESMICROALBU, @FECMICROALBU, @RESCREATINUR, @FECCREATINUR, @RESCOLESTOTAL, @FECCOLESTOTAL, @RESHDL, @FECHDL, @RESLDL, @FECLDL, @RESPTH,
						@FECPTH, @RESALBUSERICA,@RESFOSFORO
				
		WHILE @@FETCH_STATUS = 0
				BEGIN	
				
			    if (select count(*) from #tmpADRES2463D where [6] = @INPACIENT) > 0 begin
					 
					--si el paciente ya esta registrado	procedemos actualizar Los campos
					update #tmpADRES2463D
					
					set  [27] = CASE WHEN @RESCREATININA IS NOT NULL THEN @RESCREATININA ELSE [27] END, 
						 [27.1] = CASE WHEN @FECRESCREATININA IS NOT NULL THEN @FECRESCREATININA ELSE [27.1] END,
						 [28] = CASE WHEN @RESHEMGLO = 1 THEN @RESHEMGLO ELSE [28] END, 
						 [28.1] = CASE WHEN @FECRESHEMGLO = 1 THEN @FECRESHEMGLO ELSE [28.1] END,
						 [29] = CASE WHEN @RESMICROALBU IS NOT NULL THEN @RESMICROALBU ELSE [29] END, 
						 [29.1] = CASE WHEN @FECMICROALBU IS NOT NULL THEN @FECREGIST ELSE [29.1] END,
						 [30] = CASE WHEN @RESCREATINUR IS NOT NULL THEN @RESCREATINUR ELSE [30] END, 
						 [30.1] = CASE WHEN @FECCREATINUR IS NOT NULL THEN @FECCREATINUR ELSE [30.1] END,
						 [31] = CASE WHEN @RESCOLESTOTAL IS NOT NULL THEN @RESCOLESTOTAL ELSE [31] END, 
						 [31.1] = CASE WHEN @FECCOLESTOTAL IS NOT NULL THEN @FECCOLESTOTAL ELSE [31.1] END,
						 [32] = CASE WHEN @RESHDL IS NOT NULL THEN @RESHDL ELSE [32] END, 
						 [32.1] = CASE WHEN @FECHDL IS NOT NULL THEN @FECHDL ELSE [32.1] END,
						 [33] = CASE WHEN @RESLDL IS NOT NULL THEN @RESLDL ELSE [33] END, 
						 [33.1] = CASE WHEN @FECLDL IS NOT NULL THEN @FECLDL ELSE [33.1] END,
						 [34] = CASE WHEN @RESPTH IS NOT NULL THEN @RESPTH ELSE [34] END, 
						 [34.1] = CASE WHEN @FECPTH IS NOT NULL THEN @FECPTH ELSE [34.1] END,
						 [60] = CASE WHEN @RESALBUSERICA IS NOT NULL THEN @RESALBUSERICA ELSE [60] END, 
						 [61] = CASE WHEN @RESFOSFORO IS NOT NULL THEN @RESFOSFORO ELSE [61] END
						 
						 where [6] =  @INPACIENT 
						 PRINT 'Modifico'
				end else begin

					--si el paciente no esta registrado, procedemos a registrarlo
					print 'insert'
					insert into #tmpADRES2463D([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[78],[27],[27.1],[28],[28.1],[29],[29.1],[30],[30.1],[31],[31.1],[32],[32.1],[33],[33.1],[34],[34.1],[60],[61])
					SELECT IPPRINOMB,case convert(nvarchar(50),IPSEGNOMB) when null then 'NONE' when '' then 'NONE' else IPSEGNOMB END as IPSEGNOMB,
						IPPRIAPEL, case convert(nvarchar(50),IPSEGAPEL) when null then 'NONE' when '' then 'NONE' else IPSEGAPEL END as IPSEGAPEL,
						case P.IPTIPODOC when 3 then 'TI'
											when 2 then 'CE'
											when 1 then 'CC'
											when 4 then 'RC'
											when 5 then 'PA'
											when 7 then 'MS'
											when 8 then 'NV'
											when 6 then 'AS' END AS TIPOIDEN,
						P.IPCODPACI, IPFECNACI, 
						case P.IPSEXOPAC when  1 then 'M'
											when  2 then 'F' END AS IPSEXOPAC, 
						case ENT.REGIMEN when 'C' then 'C'
											when 'S' then 'S'
											when 'E' then 'P'
											when 'P' then 'E' END AS REGIMEN,
						ENT.CODSUPERINTEN,GE.CODGRUPOE, 
						(select top 1 TIPOPOBESP from ADPOBESPEPAC POBP with(nolock) INNER JOIN ADPOBESPE POB with(nolock) ON POB.ID = POBP.IDADPOBESPE) AS POBESP,
						UB.DEPMUNCOD,P.IPTELEFON + ' - ' + P.IPTELMOVI,ENT.CODSUPERINTEN,
						 CASE WHEN @RESCREATININA IS NOT NULL THEN @RESCREATININA END, 
						 CASE WHEN @FECRESCREATININA IS NOT NULL THEN @FECRESCREATININA END,
						 CASE WHEN @RESHEMGLO = 1 THEN @RESHEMGLO END, 
						 CASE WHEN @FECRESHEMGLO = 1 THEN @FECRESHEMGLO END,
						 CASE WHEN @RESMICROALBU IS NOT NULL THEN @RESMICROALBU END, 
						 CASE WHEN @FECMICROALBU IS NOT NULL THEN @FECREGIST END,
						 CASE WHEN @RESCREATINUR IS NOT NULL THEN @RESCREATINUR END, 
						 CASE WHEN @FECCREATINUR IS NOT NULL THEN @FECCREATINUR END,
						 CASE WHEN @RESCOLESTOTAL IS NOT NULL THEN @RESCOLESTOTAL END, 
						 CASE WHEN @FECCOLESTOTAL IS NOT NULL THEN @FECCOLESTOTAL END,
						 CASE WHEN @RESHDL IS NOT NULL THEN @RESHDL END, 
						 CASE WHEN @FECHDL IS NOT NULL THEN @FECHDL END,
						 CASE WHEN @RESLDL IS NOT NULL THEN @RESLDL END, 
						 CASE WHEN @FECLDL IS NOT NULL THEN @FECLDL END,
						 CASE WHEN @RESPTH IS NOT NULL THEN @RESPTH END, 
						 CASE WHEN @FECPTH IS NOT NULL THEN @FECPTH END,
						 CASE WHEN @RESALBUSERICA IS NOT NULL THEN @RESALBUSERICA END, 
						 CASE WHEN @RESFOSFORO IS NOT NULL THEN @RESFOSFORO END
					FROM INPACIENT AS P with(nolock) 
					LEFT JOIN ADGRUETNI AS GE with(nolock) ON GE.CODGRUPOE = P.CODGRUPOE
					INNER JOIN ADACTIVID AS ACT with(nolock) ON ACT.codactivi = P.CODACTIVI 
					LEFT JOIN ADNIVELED AS NIV with(nolock) ON NIV.NIVECODIGO = P.NIVECODIGO 
					LEFT JOIN COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = P.CCCONTRAT
					left join INENTADM AS ENT with(nolock) ON ENT.CODENTADM = CONT.CODENTADM
					INNER JOIN INUBICACI UB ON P.AUUBICACI = UB.AUUBICACI
					where P.IPCODPACI = @INPACIENT
				END	

					FETCH NEXT FROM CursorLaboratorosNoInterfaz
					INTO @INPACIENT,@RESCREATININA, @FECRESCREATININA, @RESHEMGLO, @FECRESHEMGLO,@RESMICROALBU, @FECMICROALBU, @RESCREATINUR, @FECCREATINUR, @RESCOLESTOTAL, @FECCOLESTOTAL, @RESHDL, @FECHDL, @RESLDL, @FECLDL, @RESPTH,
						@FECPTH, @RESALBUSERICA,@RESFOSFORO
				END 
			PRINT ' no Hay Interfaz Laboratorio'	
			
			CLOSE CursorLaboratorosNoInterfaz
			DEALLOCATE CursorLaboratorosNoInterfaz
	END

	
	/********************************************************************* Promedio KTV - HCEXFISIC, pregunta 47************************************************************* */

	DECLARE @PROMEDIOKTV AS VARCHAR(5)
	
	DECLARE CursorPromedioKTV CURSOR FOR 
		SELECT IPCODPACI , CONVERT(numeric(18,2), SUM(CONVERT(decimal(18,2),REPLACE(KTV,',','.'))) / count(*)) AS KTV
		FROM HCEXFISIC AS F with(nolock)
		WHERE   F.IPCODPACI IN(SELECT [6] COLLATE Modern_Spanish_CI_AS FROM #tmpADRES2463D) AND KTV is not null AND FECREGITE between @FechaInicial and @FechaFinal
		group by IPCODPACI				
		
		OPEN CursorPromedioKTV
				FETCH NEXT FROM CursorPromedioKTV
				INTO @INPACIENT, @PROMEDIOKTV

			WHILE @@FETCH_STATUS = 0
			BEGIN	
				update #tmpADRES2463D 
				set   [47] =   CASE WHEN @PROMEDIOKTV IS NOT NULL THEN @PROMEDIOKTV ELSE [47] END
				where [6]  =   @INPACIENT  
			print 'entro' + @INPACIENT
				
				FETCH NEXT FROM CursorPromedioKTV
				INTO @INPACIENT, @PROMEDIOKTV
			END
			CLOSE CursorPromedioKTV
			DEALLOCATE CursorPromedioKTV

/********************************************************************* Promedio Horas de sesión de hemodialisis - HCSESHEM, pregunta 51************************************************************* */

	DECLARE @PROMHORSESION AS INT
	
	DECLARE CursorHorasSesion CURSOR FOR 
		SELECT IPCODPACI , SUM(HORASES) AS NUMHORAS
		FROM HCSESHEM AS S with(nolock)
		WHERE  S.IPCODPACI IN(SELECT [6] COLLATE Modern_Spanish_CI_AS FROM #tmpADRES2463D) AND S.FECFINSES between @FechaInicial and @FechaFinal
		group by IPCODPACI				
		
		OPEN CursorHorasSesion
				FETCH NEXT FROM CursorHorasSesion
				INTO @INPACIENT, @PROMHORSESION

			WHILE @@FETCH_STATUS = 0
			BEGIN	
				update #tmpADRES2463D 
				set   [51] =   CASE WHEN @PROMHORSESION IS NOT NULL THEN @PROMHORSESION ELSE [51] END
				where [6]  =   @INPACIENT  
			print 'entro' + @INPACIENT
				
				FETCH NEXT FROM CursorHorasSesion
				INTO @INPACIENT, @PROMHORSESION
			END
			CLOSE CursorHorasSesion
			DEALLOCATE CursorHorasSesion

	/********************************************************************* Examen Fisico - HCEXFISIC************************************************************* */
	----------------------------------------------------------------------.HCEXFISIC. Historia Fisica Paciente -----------------------------------------------------------------------------
					----------------------------------------------Aca consulto todos los paciente diferentes a los recien nacidos------------------------------------------------
	DECLARE @FECREGITE DATE
	DECLARE @PESOKILOGR AS VARCHAR(10)
	DECLARE @TALLAPACI AS INTEGER
	DECLARE @TENARTSIS AS INTEGER
	DECLARE @TENARTDIA AS INTEGER
	DECLARE @TFG AS VARCHAR(5)
	DECLARE @KTV AS VARCHAR(5)

	DECLARE CursorInformacionFisicas CURSOR FOR 
		SELECT F.IPCODPACI, FECREGITE,CONVERT(INT,PESOPACIE)/1000 AS PESOKILOGR,TALLAPACI, TENARTSIS, TENARTDIA, CONVERT(VARCHAR(5),TFG) AS TFG, CONVERT(VARCHAR(5),KTV) AS KTV
		FROM HCEXFISIC AS F with(nolock)
		WHERE   F.IPCODPACI IN(SELECT [6] COLLATE Modern_Spanish_CI_AS FROM #tmpADRES2463D) And FECREGITE = (Select Max(FECREGITE) from HCEXFISIC where IPCODPACI = F.IPCODPACI)
		ORDER BY FECREGITE  ASC
					
		
		OPEN CursorInformacionFisicas
				FETCH NEXT FROM CursorInformacionFisicas
				INTO @INPACIENT, @FECREGITE, @PESOKILOGR,@TALLAPACI,@TENARTSIS,@TENARTDIA, @TFG, @KTV

			WHILE @@FETCH_STATUS = 0
			BEGIN	
				update #tmpADRES2463D 
				set   [23] =   CASE WHEN @PESOKILOGR IS NOT NULL THEN @PESOKILOGR ELSE [23] END,  
					  [24] =   CASE WHEN @TALLAPACI IS NOT NULL THEN @TALLAPACI ELSE [24] END,
					  [25] =   CASE WHEN @TENARTSIS IS NOT NULL THEN @TENARTSIS ELSE [25] END,
					  [26] =   CASE WHEN @TENARTDIA IS NOT NULL THEN @TENARTDIA ELSE [26] END,
					  [35] =   CASE WHEN @TFG IS NOT NULL THEN @TFG ELSE [35] END,
					  [50] =   CASE WHEN @KTV IS NOT NULL THEN @KTV ELSE [50] END
				where [6]  =   @INPACIENT  
			print 'entro' + @INPACIENT
				
				FETCH NEXT FROM CursorInformacionFisicas
				INTO @INPACIENT, @FECREGITE, @PESOKILOGR,@TALLAPACI,@TENARTSIS,@TENARTDIA, @TFG, @KTV
			END
			CLOSE CursorInformacionFisicas
			DEALLOCATE CursorInformacionFisicas

	
	select * from #tmpADRES2463D
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento almacenado que genera el reporte de pacientes con Enfermedad Renal Crónica (ERC) exigido por la Resolución 2463 del Ministerio de Salud de Colombia. Consulta la ficha renal del paciente (HCRENFICH) cruzando información del maestro de pacientes (INPACIENT), contratos con entidades pagadoras (COCONTRAT), catálogo de aseguradoras/EPS (INENTADM) y centros de atención (ADCENATEN), para consolidar en una tabla temporal estructurada todos los campos normativos del reporte: etiología de la enfermedad renal, estadio ERC, tasa de filtración glomerular, modalidad de terapia de reemplazo renal (hemodiálisis, diálisis peritoneal, trasplante), comorbilidades (HTA, diabetes, VIH, hepatitis, enfermedad cardiovascular, pulmonar), fechas de diagnóstico e inicio de tratamiento, vacunación contra hepatitis y datos de la IPS prestadora. Recibe como parámetros el centro de atención, la EAPB/entidad, el identificador de entidad y el rango de fechas de registro, y está diseñado para alimentar el archivo plano o la interfaz de reporte regulatorio que se debe enviar a la Cuenta de Alto Costo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ConsultarResolucion2463';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ConsultarResolucion2463';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye y devuelve el reporte de pacientes con enfermedad renal crónica/HTA/DM exigido por la Resolución 2463, consolidando ficha renal, diagnósticos, novedades, laboratorios (con o sin interfaz) y mediciones físicas en una tabla temporal columnar.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarResolucion2463';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención debe existir en HCPARLABO para determinar si los laboratorios provienen de interfaz (INTLABOAC=1) o de captura manual.; Las fichas renales (HCRENFICH) deben tener FECREGISTRO dentro del rango [@FechaInicial,@FechaFinal] y CODCENATE = @CentroAtencion.; Para diagnósticos, novedades y laboratorios se exige que el contrato pertenezca a la EAPB (@EAPB) o que el ingreso esté asociado a la entidad @IdEntidad.; El paciente debe existir en INPACIENT con ubicación en INUBICACI y actividad en ADACTIVID.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarResolucion2463';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] #tmpADRES2463D: Cuando el paciente del cursor de ficha renal aún no existe en #tmpADRES2463D (count=0 por columna [6]), se inserta con datos demográficos de INPACIENT y los campos clínicos de HCRENFICH.; [UPDATE] #tmpADRES2463D: Cuando el paciente ya existe en #tmpADRES2463D, se actualizan los campos de ficha renal solo si el valor nuevo no es NULL (patrón ''CASE WHEN @x IS NULL THEN col ELSE @x END''), preservando el valor previo en caso contrario.; [UPDATE] #tmpADRES2463D: Para diagnósticos: si CODTIPIFI=36 se marca [18]=1 y [19]=fecha (HTA); si =37 se marca [20]=1 y [21]=fecha (DM); si =38 se marca [38]=1 y [40]=fecha.; [INSERT] #tmpADRES2463D: Cuando llega un diagnóstico (HCPLANTIPIF en 36,37,38) de un paciente no presente, se inserta nuevo registro con demografía y solo el indicador/fecha del tipo correspondiente.; [UPDATE] #tmpADRES2463D: Para novedades: siempre se asigna [79]=@MOTNOVEDA; si MOTNOVEDA=9 se asigna [73]=fecha de novedad; si MOTNOVEDA=1 (fallecimiento) se asignan [80]=causa de muerte y [80.1]=fecha de muerte.; [INSERT] #tmpADRES2463D: Si la novedad corresponde a un paciente no presente en la temporal, se inserta con demografía y los campos de novedad según las mismas reglas de MOTNOVEDA.; [UPDATE] #tmpADRES2463D: Cuando HCPARLABO.INTLABOAC=1, se recorren laboratorios de INTERLABD (vía HCORDLABO y AMBORDLAB) y se mapea el VALOR/FECREGIST a las columnas [27..34.1], [60], [61] según el flag de tipo de examen (TIPCREATI, TIPHEMGLI, etc.) en INCUPSIPS.; [INSERT] #tmpADRES2463D: Si en el flujo de laboratorios con interfaz el paciente no está, se inserta con demografía y los resultados/fechas correspondientes según el tipo de examen activo.; [UPDATE] #tmpADRES2463D: Cuando HCPARLABO.INTLABOAC<>1, se actualizan resultados de creatinina, hemoglobina glicosilada, microalbuminuria, creatinuria, colesterol, HDL, LDL, PTH, albúmina sérica y fósforo desde HCORDLABO/AMBORDLAB cuando el valor no es NULL.; [INSERT] #tmpADRES2463D: Si en el flujo sin interfaz el paciente no existe, se inserta con demografía y los resultados/fechas no nulos correspondientes.; [UPDATE] #tmpADRES2463D: Promedio KTV: para pacientes ya en la temporal, [47] se actualiza con SUM(KTV)/COUNT(*) calculado sobre HCEXFISIC en el rango de fechas, solo si no es NULL.; [UPDATE] #tmpADRES2463D: Promedio horas de sesión: [51] se actualiza con SUM(HORASES) de HCSESHEM cuyas FECFINSES estén en el rango, solo si no es NULL.; [UPDATE] #tmpADRES2463D: Examen físico más reciente (MAX(FECREGITE) por paciente en HCEXFISIC) actualiza peso/1000, talla, TA sistólica, TA diastólica, TFG y KTV en columnas [23],[24],[25],[26],[35],[50] cuando no son NULL.; [RETURN_RESULT] (resultset): Al finalizar se devuelve SELECT * FROM #tmpADRES2463D con todas las columnas numeradas conforme a la Resolución 2463.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarResolucion2463';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarResolucion2463';
-- GO

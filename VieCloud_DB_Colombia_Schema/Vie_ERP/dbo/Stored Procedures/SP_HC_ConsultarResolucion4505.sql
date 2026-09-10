
-- =============================================
-- Author:		Juan Patiño
-- Create date: 29/02/2016
-- Description:	SP que lista los pacientes a reportar segun norma 4505
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ConsultarResolucion4505]
	    @CentroAtencion as varchar(MAX),
		@EAPB as varchar(20),
		@IdEntidad as varchar(20),
		@FechaInicial as datetime,
		@FechaFinal as datetime 
AS
BEGIN
	
	SET NOCOUNT ON;

   CREATE TABLE #tmpADRES4505D(
	[ID] [int] IDENTITY(1,1) NOT NULL, 
	[0] [int] NULL,
	[1] [int] NULL,
	[2] [varchar](30) NULL,
	[3] [varchar](2) NULL,
	[4] [varchar](18) NULL,
	[5] [varchar](30) NULL,
	[6] [varchar](30) NULL,
	[7] [varchar](30) NULL,
	[8] [varchar](30) NULL,
	[9] [date] NULL,
	[10] [varchar](1) NULL,
	[11] [int] NULL,
	[12] [char](4) NULL,
	[13] [int] NULL,
	[14] [int] NULL,
	[15] [int] NULL,
	[16] [int] NULL,
	[17] [int] NULL,
	[18] [int] NULL,
	[19] [int] NULL,
	[20] [int] NULL,
	[21] [int] NULL,
	[22] [int] NULL,
	[23] [int] NULL,
	[24] [int] NULL,
	[25] [int] NULL,
	[26] [int] NULL,
	[27] [int] NULL,
	[28] [int] NULL,
	[29] [date] NULL,
	[30] [varchar](10) NULL,
	[31] [date] NULL,
	[32] [int] NULL,
	[33] [date] NULL,
	[34] [int] NULL,
	[35] [int] NULL,
	[36] [int] NULL,
	[37] [int] NULL,
	[38] [int] NULL,
	[39] [int] NULL,
	[40] [int] NULL,
	[41] [int] NULL,
	[42] [int] NULL,
	[43] [int] NULL,
	[44] [int] NULL,
	[45] [int] NULL,
	[46] [int] NULL,
	[47] [int] NULL,
	[48] [int] NULL,
	[49] [date] NULL,
	[50] [date] NULL,
	[51] [date] NULL,
	[52] [date] NULL,
	[53] [date] NULL,
	[54] [int] NULL,
	[55] [date] NULL,
	[56] [date] NULL,
	[57] [int] NULL,
	[58] [date] NULL,
	[59] [int] NULL,
	[60] [int] NULL,
	[61] [int] NULL,
	[62] [date] NULL,
	[63] [date] NULL,
	[64] [date] NULL,
	[65] [date] NULL,
	[66] [date] NULL,
	[67] [date] NULL,
	[68] [date] NULL,
	[69] [date] NULL,
	[70] [int] NULL,
	[71] [int] NULL,
	[72] [date] NULL,
	[73] [date] NULL,
	[74] [int] NULL,
	[75] [date] NULL,
	[76] [date] NULL,
	[77] [int] NULL,
	[78] [date] NULL,
	[79] [varchar](max) NULL,
	[80] [date] NULL,
	[81] [varchar](max) NULL,
	[82] [date] NULL,
	[83] [varchar](max) NULL,
	[84] [date] NULL,
	[85] [varchar](max) NULL,
	[86] [int] NULL,
	[87] [date] NULL,
	[88] [int] NULL,
	[89] [int] NULL,
	[90] [int] NULL,
	[91] [date] NULL,
	[92] [int] NULL,
	[93] [date] NULL,
	[94] [int] NULL,
	[95] [varchar](20) NULL,
	[96] [date] NULL,
	[97] [int] NULL,
	[98] [varchar](20) NULL,
	[99] [date] NULL,
	[100] [date] NULL,
	[101] [int] NULL,
	[102] [varchar](20) NULL,
	[103] [date] NULL,
	[104] [varchar](max) NULL,
	[105] [date] NULL,
	[106] [date] NULL,
	[107] [varchar](max) NULL,
	[108] [date] NULL,
	[109] [varchar](max) NULL,
	[110] [date] NULL,
	[111] [date] NULL,
	[112] [date] NULL,
	[113] [varchar](max) NULL,
	[114] [int] NULL,
	[115] [int] NULL,
	[116] [int] NULL,
	[117] [int] NULL,
	[118] [date] NULL
 )

	/********************************************************************* Identificacion - ADCONCOEX************************************************************* */
	
	/*declare @CentroAtencion as varchar(20) = '001'
	declare @EAPB as varchar(20) = ''
	declare @IdEntidad as varchar(20) = '3055'
	declare @FechaInicial as date = '01/01/2016'
	declare @FechaFinal as date = '31/05/2019'

	--SELECT * FROM Contract.HealthAdministrator */

	---ADCONCOEX     63 - 67 - 68 - 76
	insert into #tmpADRES4505D([0],[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[63],[67],[68],[75],[76])
	SELECT '2',null,null,case P.IPTIPODOC when 3 then 'TI'
							 when 2 then 'CE'
							 when 1 then 'CC'
							 when 4 then 'RC'
							 when 5 then 'PA'
							 when 7 then 'MS'
							 when 8 then 'NV'
							 when 6 then 'AS' END AS TIPOIDEN,
		    P.IPCODPACI, IPPRIAPEL, case convert(nvarchar(50),IPSEGAPEL) when null then 'NONE' when '' then 'NONE' else IPSEGAPEL END as IPSEGAPEL ,IPPRINOMB,case convert(nvarchar(50),IPSEGNOMB) when null then 'NONE' when '' then 'NONE' else IPSEGNOMB END as IPSEGNOMB ,IPFECNACI,
							 case P.IPSEXOPAC when  1 then 'M'
						     when  2 then 'F' END AS IPSEXOPAC,
			GE.TIPOET, ACT.codactivi , NIV.NIVECODIGO,
			case I.TIPCONOFT when 1 then A.IPFECHCIT else '1800-01-01' END as [63],
			case I.TIPCONNUT when 1 then A.IPFECHCIT else '1800-01-01' END as [67],
			case I.TIPCONPSI when 1 then A.IPFECHCIT else '1800-01-01' END as [68],
			case I.TIPASEPRE   when 1 then A.IPFECHCIT else '1800-01-01' END as [75],
			case I.TIPASEPOS   when 1 then A.IPFECHCIT else '1800-01-01' END as [76]
	FROM ADCONCOEX AS A with(nolock)
	INNER JOIN INCUPSIPS AS I with(nolock) ON I.CODSERIPS  =  A.CODSERIPS 
	INNER JOIN INPACIENT AS P with(nolock) ON P.IPCODPACI = A.IPCODPACI 
	LEFT JOIN ADGRUETNI AS GE with(nolock) ON GE.CODGRUPOE = P.CODGRUPOE
	INNER JOIN ADACTIVID AS ACT with(nolock) ON ACT.codactivi = P.CODACTIVI 
	LEFT JOIN ADNIVELED AS NIV with(nolock) ON NIV.NIVECODIGO = P.NIVECODIGO 
	INNER JOIN ADINGRESO AS ING with(nolock) ON ING.NUMINGRES = A.NUMINGRES
	LEFT JOIN COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = ING.CODCONTRA
	where (A.CODCENATE  in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) AND IPFECHACO between @FechaInicial and @FechaFinal) AND (CONT.CODENTADM = @EAPB or ING.GENCONENTITY = @IdEntidad)
	
	
	--select * from Contract.HealthAdministrator

	/********************************************************************* RIESGOS  - HCRIESGOSP ************************************************************* */
	declare @INPACIENT as varchar(25)
	declare @GESTACION as tinyint
	declare @SIFGESCON as tinyint
	declare @HIPERINDGES as tinyint
	declare @HIPTERCON as tinyint
	declare @SINTOMRESP as tinyint
	declare @TUBERMULT as tinyint
	declare @LEPRA as tinyint
	declare @OBESDESNPRO as tinyint
	declare @VICTMALTRATO as tinyint
	declare @VICTVIOLSEX as tinyint
	declare @INFTRASEX as tinyint
	declare @ENFMENTAL as tinyint
	declare @CANCERVIX as tinyint
	declare @CANSENO as tinyint
	declare @FECVICMAL as datetime
	declare @FECVICVIOSEX as datetime

	DECLARE CursorRiesgos CURSOR FOR 	
	select P.IPCODPACI, R.GESTACION,SIFGESCON,HIPERINDGES,HIPTERCON,SINTOMRESP,TUBERMULT,LEPRA,OBESDESNPRO,VICTMALTRATO,VICTVIOLSEX,
		INFTRASEX,ENFMENTAL,CANCERVIX,CANSENO,FECVICMAL,FECVICVIOSEX
	from 
		HCRIESGOSP  AS R 
		INNER JOIN ADINGRESO AS I with(nolock) on R.NUMINGRCES = I.NUMINGRES 
		INNER JOIN INPACIENT AS P with(nolock) on P.IPCODPACI = R.IPCODPACI 
		LEFT  JOIN COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = I.CODCONTRA
		where
	 	(CONT.CODENTADM = @EAPB or I.GENCONENTITY = @IdEntidad) AND ( I.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) and
		(R.FECGESTA between @FechaInicial and @FechaFinal   OR
		R.FECSIFGES between @FechaInicial and @FechaFinal  OR
		R.FECHIPER between @FechaInicial and @FechaFinal   OR
		R.FECHIPOTE  between @FechaInicial and @FechaFinal OR
		R.FECSINTO between @FechaInicial and @FechaFinal   OR
		R.FECTUBER between @FechaInicial and @FechaFinal   OR
		R.FECLEPRA between @FechaInicial and @FechaFinal   OR
		R.FECOBEDES between @FechaInicial and @FechaFinal  OR
		R.FECVICMAL between @FechaInicial and @FechaFinal  OR
		R.FECVICVIOSEX between @FechaInicial and @FechaFinal OR
		R.FECENFMEN between @FechaInicial and @FechaFinal   OR
		R.FECCANCER between @FechaInicial and @FechaFinal   OR
		R.FECCANSEN between @FechaInicial and @FechaFinal   OR
        R.FECINFTRA  between @FechaInicial and @FechaFinal    ))
				
	
		OPEN CursorRiesgos
			FETCH NEXT FROM CursorRiesgos 
			INTO @INPACIENT,@GESTACION, @SIFGESCON, @HIPERINDGES,@HIPTERCON, @SINTOMRESP, @TUBERMULT, @LEPRA, @OBESDESNPRO , @VICTMALTRATO , @VICTVIOLSEX ,@INFTRASEX, @ENFMENTAL, @CANCERVIX ,@CANSENO,@FECVICMAL,@FECVICVIOSEX
			
			WHILE @@FETCH_STATUS = 0
			BEGIN
			
			if (select count(*) from #tmpADRES4505D where [4] = @INPACIENT) > 0 begin
				--si el paciente ya esta registrado	porcedemos actualizar	
			  update #tmpADRES4505D 
			   set  [14] = case when @GESTACION is null then [14] else @GESTACION end
				   ,[15] = @SIFGESCON 
				   ,[16] = case when @HIPERINDGES  is null then [16] else @HIPERINDGES  end 
				   ,[17] = case when @HIPTERCON is null then [17] else @HIPTERCON end 
				   ,[18] = case when @SINTOMRESP is null then [18] else @SINTOMRESP end
				   ,[19] = case when  @TUBERMULT is null then [19] else @TUBERMULT end
				   ,[20] = case when @LEPRA is null then [20] else @LEPRA end
				   ,[21] = case when @OBESDESNPRO is null then [21] else @OBESDESNPRO end
				   ,[22] = case when  @VICTMALTRATO is null then [22] else @VICTMALTRATO end
				   ,[23] = case when @VICTVIOLSEX is null then [23] else @VICTVIOLSEX end
				   ,[24] = case when @INFTRASEX is null then [24] else @INFTRASEX end
				   ,[25] = case when @ENFMENTAL is null then [25] else @ENFMENTAL end
				   ,[26] = case when @CANCERVIX is null then [26] else @CANCERVIX end
				   ,[27] = case when  @CANSENO is null then [27] else @CANSENO end
				   ,[28] = 21 
				   ,[65] = case when  @FECVICMAL is null then [65] else @FECVICMAL end
				   ,[66] = case when  @FECVICVIOSEX is null then [66] else @FECVICVIOSEX end
				where [4] =   @INPACIENT 
			end else begin 
				--si el paciente no esta registrado, procedemos a registrarlo
			   -- print 'insert'

				insert into #tmpADRES4505D([0],[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[65],[66])
				SELECT '2',null,null,case P.IPTIPODOC when 3 then 'TI'
										 when 2 then 'CE'
										 when 1 then 'CC'
										 when 4 then 'RC'
										 when 5 then 'PA'
										 when 7 then 'MS'
										  when 8 then 'NV'
										 when 6 then 'AS' END AS TIPOIDEN,
						 P.IPCODPACI, IPPRIAPEL, case convert(nvarchar(50),IPSEGAPEL) when null then 'NONE' when '' then 'NONE' else IPSEGAPEL END as IPSEGAPEL ,IPPRINOMB,case convert(nvarchar(50),IPSEGNOMB) when null then 'NONE' when '' then 'NONE' else IPSEGNOMB END as IPSEGNOMB ,IPFECNACI,
										 case P.IPSEXOPAC when  1 then 'M'
										 when  2 then 'F' END AS IPSEXOPAC,
						GE.TIPOET, ACT.codactivi , NIV.NIVECODIGO,
						@GESTACION,@SIFGESCON, @HIPERINDGES,@HIPTERCON, @SINTOMRESP, @TUBERMULT, @LEPRA, @OBESDESNPRO , @VICTMALTRATO , @VICTVIOLSEX ,@INFTRASEX, @ENFMENTAL, @CANCERVIX ,@CANSENO,21 as [28],
						@FECVICMAL as [65],@FECVICVIOSEX as [66]    
				FROM  
					INPACIENT AS P with(nolock)
					LEFT JOIN ADGRUETNI AS GE with(nolock) ON GE.CODGRUPOE = P.CODGRUPOE
					INNER JOIN ADACTIVID AS ACT with(nolock) ON ACT.codactivi = P.CODACTIVI 
					LEFT JOIN ADNIVELED AS NIV with(nolock) ON NIV.NIVECODIGO = P.NIVECODIGO 
				where P.IPCODPACI = @INPACIENT 

			end
			
				FETCH NEXT FROM CursorRiesgos 
				INTO @INPACIENT,@GESTACION, @SIFGESCON, @HIPERINDGES,@HIPTERCON, @SINTOMRESP, @TUBERMULT, @LEPRA, @OBESDESNPRO , @VICTMALTRATO , @VICTVIOLSEX ,@INFTRASEX, @ENFMENTAL, @CANCERVIX ,@CANSENO,@FECVICMAL,@FECVICVIOSEX
			END 
		CLOSE CursorRiesgos
		DEALLOCATE CursorRiesgos

	
	/********************************************************************* INTERVENCIONES************************************************************* */

		declare @FECPROPAR as datetime

		DECLARE CursorIntervenciones CURSOR FOR 	
		SELECT P.IPCODPACI,MAX(FECPROPAR)
		from 
			HCANTGINE  AS H
			inner join ADINGRESO AS I with(nolock) on H.NUMINGRES = I.NUMINGRES 
			inner join INPACIENT AS P with(nolock) on P.IPCODPACI = H.IPCODPACI 
			left  join COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = I.CODCONTRA
		where  
			I.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) AND (CONT.CODENTADM = @EAPB or I.GENCONENTITY = @IdEntidad) AND
			H.FECHISPAC between @FechaInicial and @FechaFinal and FECPROPAR IS NOT NULL
		group by P.IPCODPACI 

		OPEN CursorIntervenciones
			FETCH NEXT FROM CursorIntervenciones
			INTO @INPACIENT,@FECPROPAR
			
			WHILE @@FETCH_STATUS = 0
				BEGIN

				if (select count(*) from #tmpADRES4505D where [4] = @INPACIENT) > 0 begin
						--si el paciente ya esta registrado	porcedemos actualizar	
					   update #tmpADRES4505D 
					   set [33] =  @FECPROPAR
					   where [4] = @INPACIENT 
				end else begin 
					--si el paciente no esta registrado, procedemos a registrarlo
					--print 'insert'
					insert into #tmpADRES4505D([0],[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[33])
					SELECT '2',null,null,case P.IPTIPODOC when 3 then 'TI'
											 when 2 then 'CE'
											 when 1 then 'CC'
											 when 4 then 'RC'
											 when 5 then 'PA'
											 when 7 then 'MS'
											  when 8 then 'NV'
											 when 6 then 'AS' END AS TIPOIDEN,
							 P.IPCODPACI, IPPRIAPEL, case convert(nvarchar(50),IPSEGAPEL) when null then 'NONE' when '' then 'NONE' else IPSEGAPEL END as IPSEGAPEL ,IPPRINOMB,case convert(nvarchar(50),IPSEGNOMB) when null then 'NONE' when '' then 'NONE' else IPSEGNOMB END as IPSEGNOMB ,IPFECNACI,
											 case P.IPSEXOPAC when  1 then 'M'
											 when  2 then 'F' END AS IPSEXOPAC,
							GE.TIPOET, ACT.codactivi , NIV.NIVECODIGO,
							@FECPROPAR as [33]
					FROM 	
						INPACIENT AS P with(nolock)
						LEFT JOIN ADGRUETNI AS GE ON GE.CODGRUPOE = P.CODGRUPOE
						INNER JOIN ADACTIVID AS ACT ON ACT.codactivi = P.CODACTIVI 
						LEFT JOIN ADNIVELED AS NIV ON NIV.NIVECODIGO = P.NIVECODIGO 
					where P.IPCODPACI = @INPACIENT 
				end

									
					FETCH NEXT FROM CursorIntervenciones 
					INTO @INPACIENT,@FECPROPAR
				END 
			CLOSE CursorIntervenciones
			DEALLOCATE CursorIntervenciones

	      -------------------------------------Recien Nacidos HCRECINAC-------------------------------------- 34-52
			declare @EDADGESNAC as integer
			declare @FECHISPAC as datetime
			declare @IPCODPACI as varchar(18)
			declare @IPPRIAPEL as varchar(30)
			declare @SEGUAPELLIDO as varchar(30)
			declare @IPPRINOMB as varchar(30)
			declare @IPSEGNOM as varchar(30)
			declare @FECHANACIM as date
			declare @IPSEXOPAC as varchar(1)
			declare @CODGRUPOE as integer

			DECLARE CursorIntervenciones2 CURSOR FOR 	

				SELECT  EDADGESNAC, Max(FECHISPAC) AS FECHISPAC, RTRIM(P.IPCODPACI)+ '0' + RTRIM(NUMHIJREG) AS IPCODPACI,  
						P.IPPRIAPEL,CASE RTRIM(P.IPSEGAPEL) when  NULL THEN 'NONE' WHEN '' THEN 'NONE' ELSE RTRIM(P.IPSEGAPEL)  END  SEGUAPELLIDO, 'HIJO DE ' + RTRIM(IPPRINOMB) AS IPPRINOMB,CASE  RTRIM(P.IPSEGNOMB) WHEN NULL THEN 'NONE' when '' Then 'NONE' ELSE RTRIM(P.IPSEGNOMB) END IPSEGNOM, RN.FECHANACIM,
						 CASE RN.SEXRECNAC WHEN 1 THEN 'M'   WHEN 2 THEN 'F' END AS IPSEXOPAC,GE.TIPOET			
				FROM 
					HCRECINAC  AS RN
					INNER JOIN ADINGRESO AS I with(nolock) on RN.NUMINGRES = I.NUMINGRES 
					INNER JOIN INPACIENT AS P with(nolock) on P.IPCODPACI = RN.IPCODPACI 
					LEFT  JOIN ADGRUETNI AS GE with(nolock) ON GE.CODGRUPOE = P.CODGRUPOE
					LEFT  JOIN COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = I.CODCONTRA
				WHERE  
					I.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) AND (CONT.CODENTADM = @EAPB or I.GENCONENTITY = @IdEntidad) AND 
					RN.FECHISPAC between @FechaInicial and @FechaFinal   and (EDADGESNAC IS NOT NULL or FECHISPAC IS NOT NULL)
				GROUP BY EDADGESNAC,RTRIM(P.IPCODPACI) + '0' + RTRIM(NUMHIJREG),  RTRIM(NUMHIJREG),P.IPPRIAPEL,P.IPSEGAPEL,IPPRINOMB,P.IPSEGNOMB,
				RN.FECHANACIM,SEXRECNAC,GE.TIPOET

			
			OPEN CursorIntervenciones2
				FETCH NEXT FROM CursorIntervenciones2
				INTO @EDADGESNAC,@FECHISPAC,@IPCODPACI,@IPPRIAPEL,@SEGUAPELLIDO,@IPPRINOMB,@IPSEGNOM,@FECHANACIM,@IPSEXOPAC,@CODGRUPOE
				
				WHILE @@FETCH_STATUS = 0
				BEGIN

				if (select count(*) from #tmpADRES4505D where [4] = @IPCODPACI) > 0 begin --@IPCODPACI
					--si el paciente ya esta registrado	procedemos actualizar	
					update #tmpADRES4505D 
					set  [34] =  CASE WHEN @EDADGESNAC IS NOT NULL THEN @EDADGESNAC ELSE [34] END
						,[52] =  CASE WHEN @FECHISPAC IS NOT NULL THEN @FECHISPAC  ELSE [52] END
						,[4] =	 CASE WHEN @IPCODPACI IS NOT NULL THEN @IPCODPACI  ELSE [4] END
						,[5] =	 CASE WHEN @IPPRIAPEL IS NOT NULL THEN @IPPRIAPEL  ELSE [5] END
						,[6] =	 CASE WHEN @SEGUAPELLIDO IS NOT NULL THEN @SEGUAPELLIDO  ELSE [6] END
						,[7] =	 CASE WHEN @IPPRINOMB IS NOT NULL THEN @IPPRINOMB  ELSE [7] END
						,[8] =	 CASE WHEN @IPSEGNOM IS NOT NULL THEN @IPSEGNOM  ELSE [8] END
						,[9] =	 CASE WHEN @FECHANACIM IS NOT NULL THEN @FECHANACIM  ELSE [9] END
						,[10] =	 CASE WHEN @IPSEXOPAC IS NOT NULL THEN @IPSEXOPAC  ELSE [10] END
						,[11] =	 CASE WHEN @CODGRUPOE IS NOT NULL THEN @CODGRUPOE  ELSE [11] END
					where [4] = @INPACIENT 
					print 'MODIFICO DATOS DE NACIDOS'
				end  else begin 
					--si el paciente no esta registrado, procedemos a registrarlo
					--print 'insert DATOS DE NACIDOS'
					insert into #tmpADRES4505D([0],[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[34],[52])
					VALUES  ('2',null,null,'MS',@IPCODPACI,@IPPRIAPEL,@SEGUAPELLIDO,@IPPRINOMB,@IPSEGNOM,@FECHANACIM,@IPSEXOPAC,@CODGRUPOE,'9998','13',@EDADGESNAC,@FECHISPAC)
					END	
								
				FETCH NEXT FROM CursorIntervenciones2
				INTO @EDADGESNAC,@FECHISPAC,@IPCODPACI,@IPPRIAPEL,@SEGUAPELLIDO,@IPPRINOMB,@IPSEGNOM,@FECHANACIM,@IPSEXOPAC,@CODGRUPOE
			END 
		CLOSE CursorIntervenciones2
		DEALLOCATE CursorIntervenciones2 

		---------------------------------------------------------------------------------------------Esquema Vacunacion----------------------------------------------
   
	declare @RESVAC as int
	declare @IDPLAVACU as char(4)
		
		DECLARE CursorEsquemaVacunacion CURSOR FOR 	
			SELECT HV.IPCODPACI,case HV.IDPLAVACU   
									when '0001' then 1 --35 BCG
									when '0002' then 1 --36 Hepatitis B
									when '0003' then 1 --37 Pentavalente   -39 DPT menores de 5 años
									when '0008' then 2 --37 Pentavalente   -39 DPT menores de 5 años
									when '0013' then 3 --37
									when '0006' then 1 --38 Polio
									when '0011' then 2 --38
									when '0016' then 3 --38
									when '0022' then 4 --38
									when '0024' then 5 --38
									when '0013' then 3 --39 DPT menores de 5 años
									when '0021' then 4 --39 	
									when '0023' then 5 --39 
									when '0007' then 1 --40 Rotavirus
									when '0012' then 2 --40
									when '0033' then 1 --41 Neumococo
									when '0034' then 2 --41
									when '0035' then 3 --41
									when '0017' then 1 --42 Influenza Niños
									when '0018' then 2 --42
									when '0040' then 3 --42
									when '0020' then 1 --43 Fiebre Amarilla 
									when '0039' then 1 --44 Hepatitis A 
									when '0019' then 1 --45 Triple Viral Niños
									when '0025' then 2 --45 
									when '0036' then 1 --46
									when '0037' then 2 --46
									when '0038' then 3 --46
									when '0027' then 1 --47 TD-TT Edad fertil
									when '0028' then 2 --47
									when '0029' then 3 --47
									when '0030' then 4 --47
									when '0031' then 5 --47 	  
								END AS RESVAC,  HV.IDPLAVACU
		from HCPLANVAC as HV
			INNER JOIN INPLANVAC as inp with(nolock) on inp.IDPLAVACU = HV.IDPLAVACU 
		where HV.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) AND (HV.CODENTIDA = @EAPB or HV.CODENTIDA = @IdEntidad)   AND 
	   		  HV.FECAPLVAC  between @FechaInicial and @FechaFinal  -- AND MTVNOAPL = 0 28/04/2016 orden de wiliam no filtramos por estado 
		
			  ORDER BY IPCODPACI, RESVAC
			

		OPEN CursorEsquemaVacunacion
				FETCH NEXT FROM CursorEsquemaVacunacion
				INTO @INPACIENT, @RESVAC, @IDPLAVACU 

		WHILE @@FETCH_STATUS = 0
				BEGIN	

			    if (select count(*) from #tmpADRES4505D where [4] = @INPACIENT) > 0 begin
					--si el paciente ya esta registrado	procedemos actualizar Los campos
					update #tmpADRES4505D 
					set  [35] =  case when  @IDPLAVACU = '0001' then @RESVAC else [35] end
						,[36] =  case when  @IDPLAVACU = '0002' then @RESVAC else [36] end
						,[37] =  case when (@IDPLAVACU = '0003' or @IDPLAVACU = '0008' or @IDPLAVACU = '0013') then @RESVAC else [37] end
						,[38] =  case when (@IDPLAVACU = '0006' or @IDPLAVACU = '0011' or @IDPLAVACU = '0016' or @IDPLAVACU = '0022' or @IDPLAVACU = '0024') then @RESVAC else [38] end
						,[39] =  case when (@IDPLAVACU = '0003' or @IDPLAVACU = '0008' or @IDPLAVACU = '0013' or @IDPLAVACU = '0021' or @IDPLAVACU = '0023') then @RESVAC else [39] end
						,[40] =  case when (@IDPLAVACU = '0007' or @IDPLAVACU = '0012') then @RESVAC else [40] end
						,[41] =  case when (@IDPLAVACU = '0033' or @IDPLAVACU = '0034' or @IDPLAVACU = '0035') then @RESVAC else [41] end
						,[42] =  case when (@IDPLAVACU = '0017' or @IDPLAVACU = '0018' or @IDPLAVACU = '0040') then @RESVAC else [42] end
						,[43] =  case when (@IDPLAVACU = '0020') then @RESVAC else [43] end
						,[44] =  case when (@IDPLAVACU = '0039') then @RESVAC else [44] end
						,[45] =  case when (@IDPLAVACU = '0019' or @IDPLAVACU = '0025') then @RESVAC else [45] end
						,[46] =  case when (@IDPLAVACU = '0036' or @IDPLAVACU = '0037' or @IDPLAVACU = '0038') then @RESVAC else [46] end
						,[47] =  case when (@IDPLAVACU = '0027' or @IDPLAVACU = '0028' or @IDPLAVACU = '0029'or @IDPLAVACU = '0030' or @IDPLAVACU = '0031') then @RESVAC else [46] end
						 where [4] =  @INPACIENT 
				end else begin
				--si el paciente no esta registrado, procedemos a registrarlo
			--	print 'insert'
					insert into #tmpADRES4505D([0],[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[35],[36],[37],[38],[39],[40],[41],[42],[43],[44],[45],[46],[47])
					SELECT '2',null,null,case P.IPTIPODOC when 3 then 'TI'
											 when 2 then 'CE'
											 when 1 then 'CC'
											 when 4 then 'RC'
											 when 5 then 'PA'
											 when 7 then 'MS'
											  when 8 then 'NV'
											 when 6 then 'AS' END AS TIPOIDEN,
							 P.IPCODPACI, IPPRIAPEL, case convert(nvarchar(50),IPSEGAPEL) when null then 'NONE' when '' then 'NONE' else IPSEGAPEL END as IPSEGAPEL ,IPPRINOMB,case convert(nvarchar(50),IPSEGNOMB) when null then 'NONE' when '' then 'NONE' else IPSEGNOMB END as IPSEGNOMB ,IPFECNACI,
											 case P.IPSEXOPAC when  1 then 'M'
											 when  2 then 'F' END AS IPSEXOPAC,
							GE.TIPOET, ACT.codactivi , NIV.NIVECODIGO,
							--@FECPROPAR as [33]
						 [35] =  case when  @IDPLAVACU = '0001' then @RESVAC end
						,[36] =  case when  @IDPLAVACU = '0002' then @RESVAC end
						,[37] =  case when (@IDPLAVACU = '0003' or @IDPLAVACU = '0008' or @IDPLAVACU = '0013') then @RESVAC end
						,[38] =  case when (@IDPLAVACU = '0006' or @IDPLAVACU = '0011' or @IDPLAVACU = '0016' or @IDPLAVACU = '0022' or @IDPLAVACU = '0024') then @RESVAC end
						,[39] =  case when (@IDPLAVACU = '0003' or @IDPLAVACU = '0008' or @IDPLAVACU = '0013' or @IDPLAVACU = '0021' or @IDPLAVACU = '0023') then @RESVAC end
						,[40] =  case when (@IDPLAVACU = '0007' or @IDPLAVACU = '0012') then @RESVAC end
						,[41] =  case when (@IDPLAVACU = '0033' or @IDPLAVACU = '0034' or @IDPLAVACU = '0035') then @RESVAC end
						,[42] =  case when (@IDPLAVACU = '0017' or @IDPLAVACU = '0018' or @IDPLAVACU = '0040') then @RESVAC end
						,[43] =  case when (@IDPLAVACU = '0020') then @RESVAC end
						,[44] =  case when (@IDPLAVACU = '0039') then @RESVAC end
						,[45] =  case when (@IDPLAVACU = '0019' or @IDPLAVACU = '0025') then @RESVAC end
						,[46] =  case when (@IDPLAVACU = '0036' or @IDPLAVACU = '0037' or @IDPLAVACU = '0038') then @RESVAC end
						,[47] =  case when (@IDPLAVACU = '0027' or @IDPLAVACU = '0028' or @IDPLAVACU = '0029'or @IDPLAVACU = '0030' or @IDPLAVACU = '0031') then @RESVAC end
				    FROM 	
						INPACIENT AS P with(nolock)
						LEFT JOIN ADGRUETNI AS GE with(nolock) ON GE.CODGRUPOE = P.CODGRUPOE
						INNER JOIN ADACTIVID AS ACT with(nolock) ON ACT.codactivi = P.CODACTIVI 
						LEFT JOIN ADNIVELED AS NIV with(nolock) ON NIV.NIVECODIGO = P.NIVECODIGO 
					where P.IPCODPACI = @INPACIENT 
				end

								
					FETCH NEXT FROM CursorEsquemaVacunacion 
					INTO @INPACIENT, @RESVAC, @IDPLAVACU 
				END 
			CLOSE CursorEsquemaVacunacion
			DEALLOCATE CursorEsquemaVacunacion
			

------------------------------------------Tabla->HCATINPAR-Colum->FECINIATE----------------------------- 49-51
		declare @FECINIATE as datetime

		DECLARE CursorIntervenciones4 CURSOR FOR 	
		SELECT P.IPCODPACI,MAX(FECINIATE) 
		from 
			HCATINPAR  AS HC
			inner join ADINGRESO AS I with(nolock) on HC.NUMINGRES = I.NUMINGRES 
			inner join INPACIENT AS P with(nolock) on P.IPCODPACI = HC.IPCODPACI 
			left  join COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = I.CODCONTRA
		where  
			I.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) AND (CONT.CODENTADM = @EAPB or I.GENCONENTITY = @IdEntidad) AND
			HC.FECINIATE between @FechaInicial and @FechaFinal    and FECINIATE IS NOT NULL
		group by P.IPCODPACI 

		OPEN CursorIntervenciones4
			FETCH NEXT FROM CursorIntervenciones4
			INTO @INPACIENT,@FECINIATE
			
			WHILE @@FETCH_STATUS = 0
				BEGIN

				if (select count(*) from #tmpADRES4505D where [4] = @INPACIENT) > 0 begin
						--si el paciente ya esta registrado	porcedemos actualizar	
					   update #tmpADRES4505D 
					   set [49] =  @FECINIATE
						  ,[51] =  @FECINIATE
					   where [4] = @INPACIENT 
				end else begin 
					--si el paciente no esta registrado, procedemos a registrarlo
					--print 'insert'
					insert into #tmpADRES4505D([0],[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[49],[51])
					SELECT '2',null,null,case P.IPTIPODOC when 3 then 'TI'
											 when 2 then 'CE'
											 when 1 then 'CC'
											 when 4 then 'RC'
											 when 5 then 'PA'
											 when 7 then 'MS'
											  when 8 then 'NV'
											 when 6 then 'AS' END AS TIPOIDEN,
							 P.IPCODPACI, IPPRIAPEL, case convert(nvarchar(50),IPSEGAPEL) when null then 'NONE' when '' then 'NONE' else IPSEGAPEL END as IPSEGAPEL ,IPPRINOMB,case convert(nvarchar(50),IPSEGNOMB) when null then 'NONE' when '' then 'NONE' else IPSEGNOMB END as IPSEGNOMB ,IPFECNACI,
											 case P.IPSEXOPAC when  1 then 'M'
											 when  2 then 'F' END AS IPSEXOPAC,
							GE.TIPOET, ACT.codactivi , NIV.NIVECODIGO,
							@FECINIATE as [49],
							@FECINIATE as [51]
					FROM 	
						INPACIENT AS P with(nolock)
						LEFT JOIN ADGRUETNI AS GE with(nolock) ON GE.CODGRUPOE = P.CODGRUPOE
						INNER JOIN ADACTIVID AS ACT with(nolock) ON ACT.codactivi = P.CODACTIVI 
						LEFT JOIN ADNIVELED AS NIV with(nolock) ON NIV.NIVECODIGO = P.NIVECODIGO 
					where P.IPCODPACI = @INPACIENT 
				end

									
					FETCH NEXT FROM CursorIntervenciones4
					INTO @INPACIENT,@FECINIATE
				END 
			CLOSE CursorIntervenciones4
			DEALLOCATE CursorIntervenciones4

	------------------------------------------HCEPICRIS----------------------------- 50
		declare @FECREGEPI as datetime

		DECLARE CursorIntervenciones5 CURSOR FOR 	
		SELECT P.IPCODPACI,MAX(FECREGEPI)
		from 
			HCEPICRIS  AS HE
			inner join ADINGRESO AS I with(nolock) on HE.NUMINGRES = I.NUMINGRES 
			inner join INPACIENT AS P with(nolock) on P.IPCODPACI = HE.IPCODPACI
			left  join COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = I.CODCONTRA 
		where  
			I.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) AND (CONT.CODENTADM = @EAPB or I.GENCONENTITY = @IdEntidad) AND
			HE.FECREGEPI between @FechaInicial and @FechaFinal    and FECREGEPI IS NOT NULL
		group by P.IPCODPACI 

		OPEN CursorIntervenciones5
			FETCH NEXT FROM CursorIntervenciones5
			INTO @INPACIENT,@FECREGEPI
			

			WHILE @@FETCH_STATUS = 0
				BEGIN

				if (select count(*) from #tmpADRES4505D where [4] = @INPACIENT) > 0 begin
						--si el paciente ya esta registrado	porcedemos actualizar	
					   update #tmpADRES4505D 
					   set [50] =  @FECREGEPI
					   where [4] = @INPACIENT 
				end else begin 
					--si el paciente no esta registrado, procedemos a registrarlo
					--print 'insert'
					insert into #tmpADRES4505D([0],[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[50])
					SELECT '2',null,null,case P.IPTIPODOC when 3 then 'TI'
											 when 2 then 'CE'
											 when 1 then 'CC'
											 when 4 then 'RC'
											 when 5 then 'PA'
											 when 7 then 'MS'
											  when 8 then 'NV'
											 when 6 then 'AS' END AS TIPOIDEN,
							 P.IPCODPACI, IPPRIAPEL, case convert(nvarchar(50),IPSEGAPEL) when null then 'NONE' when '' then 'NONE' else IPSEGAPEL END as IPSEGAPEL ,IPPRINOMB,case convert(nvarchar(50),IPSEGNOMB) when null then 'NONE' when '' then 'NONE' else IPSEGNOMB END as IPSEGNOMB ,IPFECNACI,
											 case P.IPSEXOPAC when  1 then 'M'
											 when  2 then 'F' END AS IPSEXOPAC,
							GE.TIPOET, ACT.codactivi , NIV.NIVECODIGO,
							@FECREGEPI as [50]
					FROM 	
						INPACIENT AS P with(nolock)
						LEFT JOIN ADGRUETNI AS GE with(nolock) ON GE.CODGRUPOE = P.CODGRUPOE
						INNER JOIN ADACTIVID AS ACT with(nolock) ON ACT.codactivi = P.CODACTIVI 
						LEFT JOIN ADNIVELED AS NIV with(nolock) ON NIV.NIVECODIGO = P.NIVECODIGO 
					where P.IPCODPACI = @INPACIENT 
				end

									
					FETCH NEXT FROM CursorIntervenciones5
					INTO @INPACIENT,@FECREGEPI
				END 
			CLOSE CursorIntervenciones5
			DEALLOCATE CursorIntervenciones5

	------------------------------------------HCTIPCONS----------------------------- nueva tabla pero vamos a tener los mismos errores de la otras que toca mezclar datos 
	--Tabla que me contesta las preguntas 53-56-57-58-69-72-73 en la cual en la tabla se guarda
	--para la pregunta:53->1, 56->2, 57->3, 58->3(Númerico), 69->4, 72->5, 73->6; para la pregunta 57 que es un numerico se suma todo las controles prenatales que haya tenido el paciente es decir la pregunta  56->2, 57->3

		DECLARE @TIPOCONS AS INT
		DECLARE @FECHAREG AS date

		DECLARE CursorTipoConsulta CURSOR FOR 	
		SELECT P.IPCODPACI,TIPOCONSU,FECHAREG
		from 
			HCTIPCONS  AS HC
			INNER JOIN ADINGRESO AS I with(nolock) on HC.NUMINGRES = I.NUMINGRES 
			INNER JOIN INPACIENT AS P with(nolock) on P.IPCODPACI = HC.IPCODPACI 
			LEFT  JOIN COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = I.CODCONTRA
		where  
			I.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) AND (CONT.CODENTADM = @EAPB or I.GENCONENTITY = @IdEntidad) AND
			HC.FECHAREG between @FechaInicial and @FechaFinal    and FECHAREG IS NOT NULL 
		ORDER BY FECHAREG ASC

		OPEN CursorTipoConsulta
			FETCH NEXT FROM CursorTipoConsulta
			INTO @INPACIENT,@TIPOCONS,@FECHAREG
			

			WHILE @@FETCH_STATUS = 0
				BEGIN

				if (select count(*) from #tmpADRES4505D where [4] = @INPACIENT) > 0 begin
				--si el paciente ya esta registrado	procedemos actualizar Los campos
					update #tmpADRES4505D 
					set  [53] =  case when @TIPOCONS = '1' then @FECHAREG else [53] end
						,[56] =  case when @TIPOCONS = '2' then @FECHAREG else [56] end
						,[57] =  case when @TIPOCONS IN('3','2') then case when [57] is null then 1 else [57] + 1 end else [57] end 
						,[58] =  case when @TIPOCONS = '3' then @FECHAREG else [58] end
						,[69] =  case when @TIPOCONS = '4' then @FECHAREG else [69] end
						,[72] =  case when @TIPOCONS = '5' then @FECHAREG else [72] end
						,[73] =  case when @TIPOCONS = '6' then @FECHAREG else [73] end
						 where [4] =  @INPACIENT 
				end else begin
					--si el paciente no esta registrado, procedemos a registrarlo
					--print 'insert'
					insert into #tmpADRES4505D([0],[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[53],[56],[57],[58],[69],[72],[73])
					SELECT '2',null,null,case P.IPTIPODOC when 3 then 'TI'
											 when 2 then 'CE'
											 when 1 then 'CC'
											 when 4 then 'RC'
											 when 5 then 'PA'
											 when 7 then 'MS'
											  when 8 then 'NV'
											 when 6 then 'AS' END AS TIPOIDEN,
							 P.IPCODPACI, IPPRIAPEL, case convert(nvarchar(50),IPSEGAPEL) when null then 'NONE' when '' then 'NONE' else IPSEGAPEL END as IPSEGAPEL ,IPPRINOMB,case convert(nvarchar(50),IPSEGNOMB) when null then 'NONE' when '' then 'NONE' else IPSEGNOMB END as IPSEGNOMB ,IPFECNACI,
											 case P.IPSEXOPAC when  1 then 'M'
											 when  2 then 'F' END AS IPSEXOPAC,
							GE.TIPOET, ACT.codactivi , NIV.NIVECODIGO,
							 [53] =  case when @TIPOCONS = '1' then @FECHAREG  end
							,[56] =  case when @TIPOCONS = '2' then @FECHAREG  end
							,[57] =  case when @TIPOCONS IN('3','2') then 1   end
							,[58] =  case when @TIPOCONS = '3' then @FECHAREG  end
							,[69] =  case when @TIPOCONS = '4' then @FECHAREG  end
							,[72] =  case when @TIPOCONS = '5' then @FECHAREG  end
							,[73] =  case when @TIPOCONS = '6' then @FECHAREG  end
					FROM 	
						INPACIENT AS P with(nolock)
						LEFT JOIN ADGRUETNI AS GE with(nolock) ON GE.CODGRUPOE = P.CODGRUPOE
						INNER JOIN ADACTIVID AS ACT with(nolock) ON ACT.codactivi = P.CODACTIVI 
						LEFT JOIN ADNIVELED AS NIV with(nolock) ON NIV.NIVECODIGO = P.NIVECODIGO 
					where P.IPCODPACI = @INPACIENT 
				end
												
					FETCH NEXT FROM CursorTipoConsulta
					INTO @INPACIENT,@TIPOCONS,@FECHAREG
				END 
			CLOSE CursorTipoConsulta
			DEALLOCATE CursorTipoConsulta

			

	------------------------------------------INDIAGNOP----------------------------- 
	--62-64-65-77-114-115-116-117-118 La lógica es la siguiente: Saco todo los registro que el campo TRATA4505 no este nulo para con ellos por medio del diagnostico ir a la tabla de tipificacion y mirar si el diagnostico tiene la tipificacion (15,16,17,18,19,20,5,2,3,8,9)
	-- si lo tiene lo que hago es el campo TRATA4505 Asigno al control y con el CODTIPIFI se en que campo. ¿como se en que campo debe de ir? por lo siguiente:
		--campo 64 ->11  codigo tipificacion
		--campo 65 ->12
		--campo 77-> 15-16-17-18-19-20 
		--campo 114-> 5
		--campo 115-> 2
		--campo 116-> 3
		--campo 117-> 8-9
		--campo 62 -> 29  si esta la tipificacion 39 mando la fecha del diagnostio en el campo de valoracion visual(62)
	
	DECLARE @TRATA4505 AS INT
	DECLARE @CODTIPIFI AS INT
	DECLARE @FECHA AS DATE
	DECLARE @FECHAlEISHM AS DATE
		
		DECLARE CursorDiagnosticos CURSOR FOR 	
		 select INP.IPCODPACI,TRATA4505, TIP.CODTIPIFI, INP.FECDIAGNO,FECHLEISH 
		from INDIAGNOP as INP
				INNER  JOIN ADINGRESO AS I with(nolock) ON INP.NUMINGRES  = I.NUMINGRES 
				INNER  JOIN INPACIENT AS P with(nolock) ON P.IPCODPACI = INP.IPCODPACI 
				LEFT   JOIN HCRIESGOSP AS HR with(nolock) ON HR.IPCODPACI = INP.IPCODPACI
				LEFT   JOIN COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = I.CODCONTRA
				INNER  JOIN HCPLANTIPIF AS TIP with(nolock) ON TIP.CODDIAGNO = INP.CODDIAGNO WHERE /*(INP.TRATA4505 IS NOT NULL OR INP.FECHLEISH IS NOT NULL) AND*/ TIP.CODTIPIFI IN (11,12,15,16,17,18,19,20,5,2,3,8,9,29,35)
																					and I.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) AND  (CONT.CODENTADM = @EAPB or I.GENCONENTITY = @IdEntidad)   
	   																				and INP.FECDIAGNO  between @FechaInicial and @FechaFinal   
			 			
			OPEN CursorDiagnosticos
				FETCH NEXT FROM CursorDiagnosticos
				INTO @INPACIENT, @TRATA4505, @CODTIPIFI,@FECHA,@FECHAlEISHM

		WHILE @@FETCH_STATUS = 0
				BEGIN	
				
			    if (select count(*) from #tmpADRES4505D where [4] = @INPACIENT) > 0 begin
					--si el paciente ya esta registrado	procedemos actualizar Los campos
					update #tmpADRES4505D 
					
					set   [62] =   CASE WHEN @CODTIPIFI = 29 THEN @FECHA END
						, [64] =   CASE WHEN @CODTIPIFI = 11 THEN @FECHA ELSE [64] END
						, [65] =   CASE WHEN @CODTIPIFI = 12 THEN @FECHA ELSE [65] END
						, [77]  =  CASE when @CODTIPIFI IN(15,16,17,18,19,20)  THEN @TRATA4505 ELSE [77] END
					    , [114] =  CASE WHEN @CODTIPIFI = 5 THEN @TRATA4505 ELSE [114] END
						, [115] =  CASE WHEN @CODTIPIFI = 2 THEN @TRATA4505 ELSE [115] END
					    , [116] =  CASE WHEN @CODTIPIFI = 3 THEN @TRATA4505 ELSE [116] END
						, [117] =  CASE when @CODTIPIFI IN(8,9)  then @TRATA4505 else [117] END
						, [118] =  CASE when @CODTIPIFI =  35  then @FECHAlEISHM else [118] END
						 where [4] =  @INPACIENT 
				end else begin

				--si el paciente no esta registrado, procedemos a registrarlo
					--print 'insert'
					insert into #tmpADRES4505D([0],[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[62],[64],[65],[77],[114],[115],[116],[117],[118])
					SELECT '2',null,null,case P.IPTIPODOC when 3 then 'TI'
											 when 2 then 'CE'
											 when 1 then 'CC'
											 when 4 then 'RC'
											 when 5 then 'PA'
											 when 7 then 'MS'
											  when 8 then 'NV'
											 when 6 then 'AS' END AS TIPOIDEN,
							 P.IPCODPACI, IPPRIAPEL, case convert(nvarchar(50),IPSEGAPEL) when null then 'NONE' when '' then 'NONE' else IPSEGAPEL END as IPSEGAPEL ,IPPRINOMB,case convert(nvarchar(50),IPSEGNOMB) when null then 'NONE' when '' then 'NONE' else IPSEGNOMB END as IPSEGNOMB ,IPFECNACI,
											 case P.IPSEXOPAC when  1 then 'M'
											 when  2 then 'F' END AS IPSEXOPAC,
							GE.TIPOET, ACT.codactivi , NIV.NIVECODIGO,
						 [62] =  CASE WHEN @CODTIPIFI = 29 then  @FECHA END
						,[64] =  CASE WHEN @CODTIPIFI = 11  THEN @FECHA END
						,[65] =  CASE WHEN @CODTIPIFI = 12  THEN @FECHA END
						,[77] =  CASE WHEN @CODTIPIFI IN(15,16,17,18,19,20)  THEN @TRATA4505 END
						,[114] = CASE WHEN @CODTIPIFI = 5 THEN @TRATA4505  END
						,[115] = CASE WHEN @CODTIPIFI = 2 THEN @TRATA4505 END
						,[116] = CASE WHEN @CODTIPIFI = 3 THEN @TRATA4505 END
						,[117] = CASE when @CODTIPIFI IN(8,9)  then @TRATA4505 END
						,[118] = CASE when @CODTIPIFI = 35  then @FECHAlEISHM END
				   FROM 	
						INPACIENT AS P with(nolock)
						LEFT JOIN ADGRUETNI AS GE with(nolock) ON GE.CODGRUPOE = P.CODGRUPOE
						INNER JOIN ADACTIVID AS ACT with(nolock) ON ACT.codactivi = P.CODACTIVI 
						LEFT JOIN ADNIVELED AS NIV with(nolock) ON NIV.NIVECODIGO = P.NIVECODIGO 
					where P.IPCODPACI = @INPACIENT 
				end
				 									
					FETCH NEXT FROM CursorDiagnosticos
					INTO @INPACIENT, @TRATA4505, @CODTIPIFI, @FECHA, @FECHAlEISHM
				END 
			CLOSE CursorDiagnosticos
			DEALLOCATE CursorDiagnosticos

					

	------------------------------------------------------------HCHISPACA-----------Esta tabla se busco mejor los datos con ADCONCOEX Más rapides y menos costoso que sacar datos desde HCHISPACA
	--Estos campos se solucionarn de una ves en otros cursores 63-67-68

	------------------------------------------------------------INTERCTRL-INTERLABD------------------------------------- Tabla cuando tenemos interfaz. Cuando el parametro esta en 1 campo INTLABOAC tabla HCPARLABO

	DECLARE @PARAMETRO AS bit = (SELECT top 1 INTLABOAC  from HCPARLABO where CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)))
 IF @PARAMETRO = 1 
	BEGIN
		DECLARE @TIPANTHEP AS int --Tipo 4505: Antígeno de Superficie Hepatitis B en Gestantes: 1-Si 0-No	78-79
		DECLARE @TIPSERSIF AS int --Tipo 4505: Serología para Sífilis: 1-Si 0-No							80-81
		DECLARE @TIPELIVIH AS int --Tipo 4505: Elisa para VIH: 1-Si 0-No									82-83
		DECLARE @TIPTSHNEO AS int --Tipo 4505:TSH Neonatal: 1-Si 0-No										84-85
		DECLARE @TIPHEMOGL AS int --Tipo 4505:Hemoglobina : 1-Si 0-No										103-104
		DECLARE @TIPCREATI AS int --Tipo 4505: Creatinina : 1-Si 0-No										105-106
		DECLARE @TIPHEMGLI AS int --Tipo 4505:Hemoglobina Glicosilada: 1-Si 0-No							108-109-
		DECLARE @TIPBACDIA AS int --Tipo 4505:Baciloscopia de Diagnóstico: 1-Si 0-No						112-113
		DECLARE @TIPMICROA as int --Tipo 4505:Microalbuminuria : 1-Si  0-No									110
		DECLARE @TIPGLIBASA as int --Tipo 4505:Glisemia Basal: 1-Si  0-No									105
		DECLARE @TIPHDL as int -- Tipo 4505:HDL : 1-Si  0-No												111
		DECLARE @FECREGIST AS date
		DECLARE @VALOR AS VARCHAR(MAX)
		DECLARE @CODPACIENTE AS VARCHAR(15)
		DECLARE @GES AS INT
		DECLARE @FECGES AS DATE

		DECLARE CursorLaboratoriosInterfaz CURSOR FOR 	
		
			SELECT L.IPCODPACI, C.FECREGIST, R.VALOR, S.TIPANTHEP,GESTACION,FECGESTA,S.TIPSERSIF,S.TIPELIVIH,S.TIPTSHNEO,S.TIPHEMOGL,S.TIPCREATI,S.TIPHEMGLI,S.TIPBACDIA,s.TIPMICROA,s.TIPGLIBASA,s.TIPHDL FROM INTERLABD R 
				INNER JOIN HCORDLABO AS L with(nolock) ON L.AUTO = R.AUTOLABOR 
				INNER JOIN INCUPSIPS AS S with(nolock) ON S.CODSERIPS = L.CODSERIPS  
				INNER JOIN INTERCTRL AS C with(nolock) ON C.AUTOLABOR = L.AUTO
				INNER JOIN ADINGRESO AS I with(nolock) ON I.NUMINGRES = L.NUMINGRES 
				LEFT  JOIN COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = I.CODCONTRA
				LEFT  JOIN HCRIESGOSP AS RP with(nolock) ON I .IPCODPACI = RP.IPCODPACI 
			WHERE (S.TIPANTHEP = 1 OR S.TIPSERSIF = 1 OR S.TIPELIVIH = 1 OR S.TIPTSHNEO = 1 OR S.TIPHEMOGL = 1 OR S.TIPCREATI = 1 OR S.TIPHEMGLI = 1 OR S.TIPBACDIA = 1 or s.TIPMICROA = 1 or S.TIPGLIBASA = 1 or S.TIPHDL = 1)
				   AND  I.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) AND  (CONT.CODENTADM = @EAPB OR I.GENCONENTITY = @IdEntidad) AND  ( I.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) and C.FECREGIST  between @FechaInicial and @FechaFinal or (FECGESTA between @FechaInicial and @FechaFinal and TIPANTHEP IS NOT NULL and TIPANTHEP = 1 ))
			UNION all
			SELECT  L.IPCODPACI, C.FECREGIST, R.VALOR,S.TIPANTHEP,GESTACION,FECGESTA,S.TIPSERSIF,S.TIPELIVIH,S.TIPTSHNEO,S.TIPHEMOGL,S.TIPCREATI,S.TIPHEMGLI,S.TIPBACDIA,s.TIPMICROA,s.TIPGLIBASA,s.TIPHDL   FROM INTERLABD R 
				INNER JOIN AMBORDLAB AS L with(nolock) ON L.AUTO = R.AUTOLABOR 
				INNER JOIN INCUPSIPS AS S with(nolock) ON S.CODSERIPS = L.CODSERIPS  
				INNER JOIN INTERCTRL AS C with(nolock) ON C.AUTOLABOR = L.AUTO
				INNER JOIN ADINGRESO AS I with(nolock) ON L.NUMINGRES = I.NUMINGRES 
				LEFT  JOIN COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = I.CODCONTRA
				LEFT  JOIN HCRIESGOSP AS RP with(nolock) ON I.IPCODPACI = RP.IPCODPACI 
			WHERE (S.TIPANTHEP = 1 OR S.TIPANTHEP = 1 OR S.TIPSERSIF = 1 OR S.TIPELIVIH = 1 OR S.TIPTSHNEO = 1 OR S.TIPHEMOGL = 1 OR S.TIPCREATI = 1 OR S.TIPHEMGLI = 1 OR S.TIPBACDIA = 1 or S.TIPGLIBASA = 1 or S.TIPHDL = 1) 
					AND  I.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) AND  (CONT.CODENTADM = @EAPB OR I.GENCONENTITY = @IdEntidad) AND ( I.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) and C.FECREGIST  between @FechaInicial and @FechaFinal  or (FECGESTA between @FechaInicial and @FechaFinal and TIPANTHEP IS NOT NULL and TIPANTHEP = 1 ) )
			order by FECREGIST ASC

			 			
			OPEN CursorLaboratoriosInterfaz
				FETCH NEXT FROM CursorLaboratoriosInterfaz
				INTO @CODPACIENTE, @FECREGIST, @VALOR,@TIPANTHEP,@GES,@FECGES,@TIPSERSIF,@TIPELIVIH,@TIPTSHNEO,@TIPHEMOGL,@TIPCREATI,@TIPHEMGLI,@TIPBACDIA,@TIPMICROA,@TIPGLIBASA,@TIPHDL

		WHILE @@FETCH_STATUS = 0
				BEGIN	
				
			    if (select count(*) from #tmpADRES4505D where [4] = @INPACIENT) > 0 begin
					--si el paciente ya esta registrado	procedemos actualizar Los campos
					update #tmpADRES4505D
			
					set   [79] =   CASE WHEN @TIPANTHEP = 1 AND (@GES IS NOT NULL AND @GES = 1)  THEN CASE WHEN @VALOR LIKE 'NEGATIVO%' THEN '1' WHEN @VALOR LIKE 'POSITIVO%' THEN '2' ELSE @VALOR END ELSE [79] END 
						, [78] =   CASE WHEN @TIPANTHEP = 1 AND (@GES IS NOT NULL AND @GES = 1) THEN  @FECREGIST ELSE [78] END
						, [81] =   CASE WHEN @TIPSERSIF = 1 THEN CASE WHEN @VALOR LIKE 'NO REACTIV%' THEN '1' WHEN @VALOR LIKE 'REACTIVA%' THEN '2' ELSE @VALOR END ELSE [81] END 
						, [80] =   CASE WHEN @TIPSERSIF = 1 THEN  @FECREGIST ELSE [80] END
						, [83] =   CASE WHEN @TIPELIVIH = 1 THEN CASE WHEN @VALOR LIKE 'NEGATIVO%' THEN '1' WHEN @VALOR LIKE 'POSITIVO%' THEN '2' ELSE @VALOR END ELSE [83] END 
						, [82] =   CASE WHEN @TIPELIVIH = 1 THEN  @FECREGIST ELSE [82] END
						, [85]  =  CASE when @TIPTSHNEO = 1 THEN CASE WHEN @VALOR LIKE 'NORMAL%' THEN '1' WHEN @VALOR LIKE 'ANORMAL%' THEN '2' ELSE @VALOR END ELSE [85] END
						, [84]  =  CASE when @TIPTSHNEO = 1 THEN  @FECREGIST ELSE [84] END
					    , [104] =  CASE WHEN @TIPHEMOGL = 1 THEN convert(varchar(20),@VALOR) ELSE [104] END  
						, [103] =  CASE when @TIPHEMOGL = 1 THEN @FECREGIST ELSE [103] END
						, [107] =  CASE WHEN @TIPCREATI = 1 THEN @VALOR ELSE [107] END 
						, [106] =  CASE WHEN @TIPCREATI = 1 THEN @FECREGIST ELSE [106] END
					    , [109] =  CASE WHEN @TIPHEMGLI = 1 THEN @VALOR ELSE [109] END 
						, [108] =  CASE WHEN @TIPHEMGLI = 1 THEN @FECREGIST ELSE [108] END
						, [113] =  CASE WHEN @TIPBACDIA = 1 THEN CASE WHEN @VALOR LIKE 'NEGATIVA%' THEN '1' WHEN @VALOR LIKE 'POSITIVA%' THEN '2' ELSE @VALOR END ELSE [113] END 
						, [112] =  CASE when @TIPBACDIA = 1 THEN @FECREGIST ELSE [112] END
						, [110] =  CASE WHEN @TIPMICROA = 1 THEN @FECREGIST ELSE [110] END 
						, [105] =  CASE WHEN @TIPGLIBASA = 1 THEN @FECREGIST ELSE [105] END 
						, [111] =  CASE WHEN @TIPHDL     = 1 THEN @FECREGIST ELSE [111] END 
						 where [4] =  @CODPACIENTE 
						 --PRINT 'Modifico'
				end else begin
				 declare @a as integer = 1
				--si el paciente no esta registrado, procedemos a registrarlo
				--	print 'insert'
					insert into #tmpADRES4505D([0],[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[79],[78],[81],[80],[83],[82],[85],[84],[104],[103],[107],[106],[109],[108],[113],[112],[110],[105],[111])
					SELECT '2',null,null,case P.IPTIPODOC when 3 then 'TI'
											 when 2 then 'CE'
											 when 1 then 'CC'
										     when 4 then 'RC'
											 when 5 then 'PA'
											 when 7 then 'MS'
											  when 8 then 'NV'
											 when 6 then 'AS' END AS TIPOIDEN,
							 P.IPCODPACI, IPPRIAPEL, case convert(nvarchar(50),IPSEGAPEL) when null then 'NONE' when '' then 'NONE' else IPSEGAPEL END as IPSEGAPEL ,IPPRINOMB,case convert(nvarchar(50),IPSEGNOMB) when null then 'NONE' when '' then 'NONE' else IPSEGNOMB END as IPSEGNOMB ,IPFECNACI,
											 case P.IPSEXOPAC when  1 then 'M'
											 when  2 then 'F' END AS IPSEXOPAC,
							GE.TIPOET, ACT.codactivi , NIV.NIVECODIGO,
						 [79] =  CASE WHEN @TIPANTHEP = 1  AND (@GES IS NOT NULL AND  @GES = 1) THEN CASE WHEN @VALOR LIKE 'NEGATIVO%' THEN '1' WHEN @VALOR LIKE 'POSITIVO%' THEN '2' ELSE @VALOR END  END 
						,[78] =  CASE WHEN @TIPANTHEP = 1  AND (@GES IS NOT NULL AND @GES = 1) THEN  @FECREGIST  END
						,[81] =  CASE WHEN @TIPSERSIF = 1 THEN CASE WHEN @VALOR LIKE 'NO REACTIV%' THEN '1' WHEN @VALOR LIKE 'REACTIVA%' THEN '2' ELSE @VALOR END  END 
						,[80] =  CASE WHEN @TIPSERSIF = 1 THEN  @FECREGIST  END
						,[83] =  CASE WHEN @TIPELIVIH = 1 THEN CASE WHEN @VALOR LIKE 'NEGATIVO%' THEN '1' WHEN @VALOR LIKE 'POSITIVO%' THEN '2' ELSE @VALOR END  END
						,[82] =  CASE WHEN @TIPELIVIH = 1 THEN  @FECREGIST  END
						,[85]  = CASE when @TIPTSHNEO = 1 THEN CASE WHEN @VALOR LIKE 'NORMAL%' THEN '1' WHEN @VALOR LIKE 'ANORMAL%' THEN '2' ELSE @VALOR END  END 
						,[84] =  CASE WHEN @TIPTSHNEO = 1 THEN  @FECREGIST  END
						,[104] = CASE WHEN @TIPHEMOGL = 1 THEN convert(varchar(20),@VALOR) END 
						,[103] = CASE WHEN @TIPHEMOGL = 1 THEN @FECREGIST  END
						,[107] = CASE WHEN @TIPCREATI = 1 THEN @VALOR END 
						,[106] = CASE WHEN @TIPCREATI = 1 THEN @FECREGIST  END
						,[109] = CASE WHEN @TIPHEMGLI = 1 THEN @VALOR END 
						,[108] = CASE WHEN @TIPHEMGLI = 1 THEN @FECREGIST  END
						,[113] = CASE WHEN @TIPBACDIA = 1 THEN @VALOR END 
						,[112] = CASE when @TIPBACDIA = 1 THEN @FECREGIST  END
						,[110] = CASE when @TIPMICROA = 1 THEN @FECREGIST  END 
						,[105] = CASE WHEN @TIPGLIBASA = 1 THEN @FECREGIST  END 
						,[111] = CASE WHEN @TIPHDL  = 1 THEN @FECREGIST  END 
						
				   FROM INPACIENT AS P with(nolock)
						LEFT JOIN ADGRUETNI AS GE with(nolock) ON GE.CODGRUPOE = P.CODGRUPOE
						INNER JOIN ADACTIVID AS ACT with(nolock) ON ACT.codactivi = P.CODACTIVI 
						LEFT JOIN ADNIVELED AS NIV with(nolock) ON NIV.NIVECODIGO = P.NIVECODIGO 
					where P.IPCODPACI = @CODPACIENTE 
				END	

					FETCH NEXT FROM CursorLaboratoriosInterfaz
					INTO @CODPACIENTE, @FECREGIST, @VALOR,@TIPANTHEP,@GES,@FECGES,@TIPSERSIF,@TIPELIVIH,@TIPTSHNEO,@TIPHEMOGL,@TIPCREATI,@TIPHEMGLI,@TIPBACDIA,@TIPMICROA,@TIPGLIBASA,@TIPHDL
				END 
			PRINT ' Hay iterrfaz Laboratorio'
				CLOSE CursorLaboratoriosInterfaz
				DEALLOCATE CursorLaboratoriosInterfaz
	  END
 ELSE
	 BEGIN
		DECLARE @RESANTSUPH AS INT			--RESULTADO Antígeno de Superficie Hepatitis B en Gestantes: 1. Negativo, 2. Positivo   79
		DECLARE @FECRESANTSUPH AS DATE		--FECHA RESULTADO Antígeno de Superficie Hepatitis B en Gestantes						78
		DECLARE @RESSERSIF AS INT			--RESULTADO Serología para Sífilis: 1. No Reactiva, 2. Reactiva				81
		DECLARE @FECRESSERSIF AS DATE		--FECHA RESULTADO Serología para Sífilis:									80
		DECLARE @RESELIVIH AS INT			--- RESULTADO Elisa para VIH: 1. Negativo, 2. Positivo 83
		DECLARE @FECRESELIVIH AS DATE		---FECHA Elisa para VIH: 1. Negativo, 2. Positivo		82
		DECLARE @RESTSHNEO AS INT			--TSH Neonatal: 1. Normal, 2. Anormal					85
		DECLARE @FECRESTSHNEO AS DATE		----fecha TSH Neonatal									84
		DECLARE @RESHEMOGLO AS varchar(max)		 --Hemoglobina: Valor mínimo 1.5 y máximo 20				104
		DECLARE @FECRESHEMOGLO AS DATE		---FECHA Hemoglobina									103
		DECLARE @FECGLISBASAL AS DATE		---- Fecha DE GLISEMIA BASAL							105 -
		DECLARE @RESCREATININA AS varchar(max)		---Resultado creatinina						107
		DECLARE @FECRESCREATININA AS DATE	---FECHA DE Creatinina						106
		DECLARE @RESHEMGLO AS varchar(max)			--- Hemoglobina Glicosilada: Valor mínimo 5 y máximo 20, permitir decimales			109
		DECLARE @FECRESHEMGLO AS DATE		---Fecha hemoglobina glicosada														108
		DECLARE @FECMICROALBU AS DATE		 ---FECHA DE RESULTADO DE Microalbuminuria											110
		DECLARE @FECHDL AS DATE				---FECHA DE RESULTADO DE HDL													111
		DECLARE @RESBASDIAG AS INT			----Baciloscopia de Diagnóstico: 1. Negativa, 2. Positiva						113
		DECLARE @FECRESBASDIAG AS DATE		 ---FECHA DE Baciloscopia de Diagnóstico										112
		DECLARE @GESTAC AS INT
		DECLARE @FECGESTAC AS DATE
			
			DECLARE CursorLaboratorosNoInterfaz CURSOR FOR
				
				SELECT HL.IPCODPACI,RESANTSUPH,FECRESANTSUPH, GESTACION,FECGESTA,RESSERSIF,FECRESSERSIF,RESELIVIH,FECRESELIVIH,RESTSHNEO,FECRESTSHNEO,RESHEMOGLO,FECRESHEMOGLO,FECGLISBASAL,RESCREATININA,FECRESCREATININA,RESHEMGLO,FECRESHEMGLO,FECMICROALBU,FECHDL,RESBASDIAG,FECRESBASDIAG
					FROM HCORDLABO AS HL with(nolock)
						INNER JOIN ADINGRESO AS I with(nolock) ON HL.NUMINGRES COLLATE Modern_Spanish_CI_AS  = I.NUMINGRES COLLATE Modern_Spanish_CI_AS
						INNER JOIN INPACIENT AS P with(nolock) ON P.IPCODPACI COLLATE Modern_Spanish_CI_AS  = HL.IPCODPACI COLLATE Modern_Spanish_CI_AS
						INNER JOIN ADINGRESO AS ING with(nolock) ON ING.NUMINGRES COLLATE Modern_Spanish_CI_AS  = HL.NUMINGRES COLLATE Modern_Spanish_CI_AS
						LEFT  JOIN COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA COLLATE Modern_Spanish_CI_AS  = ING.CODCONTRA COLLATE Modern_Spanish_CI_AS
						LEFT JOIN HCRIESGOSP AS RP with(nolock) ON P.IPCODPACI COLLATE Modern_Spanish_CI_AS  = RP.IPCODPACI  COLLATE Modern_Spanish_CI_AS
				 WHERE   I.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion))  AND  (CONT.CODENTADM = @EAPB or ING.GENCONENTITY = @IdEntidad) and (FECRESANTSUPH between @FechaInicial and @FechaFinal or (FECGESTA between @FechaInicial and @FechaFinal and RESANTSUPH IS NOT NULL ) or FECRESSERSIF between @FechaInicial and @FechaFinal or FECRESELIVIH between @FechaInicial and @FechaFinal or FECRESTSHNEO between @FechaInicial and @FechaFinal or FECRESHEMOGLO between @FechaInicial and @FechaFinal or FECGLISBASAL between @FechaInicial and @FechaFinal or FECRESCREATININA between @FechaInicial and @FechaFinal or FECRESHEMGLO between @FechaInicial and @FechaFinal or FECMICROALBU between @FechaInicial and @FechaFinal or FECRESBASDIAG between @FechaInicial and @FechaFinal)  
				UNION ALL 
				SELECT AL.IPCODPACI,RESANTSUPH,FECRESANTSUPH, GESTACION,FECGESTA, RESSERSIF,FECRESSERSIF,RESELIVIH,FECRESELIVIH,RESTSHNEO,FECRESTSHNEO,RESHEMOGLO,FECRESHEMOGLO,FECGLISBASAL,RESCREATININA,FECRESCREATININA,RESHEMGLO,FECRESHEMGLO,FECMICROALBU,FECHDL,RESBASDIAG,FECRESBASDIAG
					FROM AMBORDLAB AS AL with(nolock) 
						INNER JOIN ADINGRESO AS I with(nolock) ON AL.NUMINGRES COLLATE Modern_Spanish_CI_AS  = I.NUMINGRES COLLATE Modern_Spanish_CI_AS
						INNER JOIN INPACIENT AS P with(nolock) ON P.IPCODPACI COLLATE Modern_Spanish_CI_AS = AL.IPCODPACI COLLATE Modern_Spanish_CI_AS
						INNER JOIN ADINGRESO AS ING with(nolock) ON ING.NUMINGRES COLLATE Modern_Spanish_CI_AS = AL.NUMINGRES COLLATE Modern_Spanish_CI_AS
						LEFT  JOIN COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA COLLATE Modern_Spanish_CI_AS = ING.CODCONTRA COLLATE Modern_Spanish_CI_AS
						LEFT JOIN HCRIESGOSP AS RP with(nolock) ON P.IPCODPACI COLLATE Modern_Spanish_CI_AS = RP.IPCODPACI COLLATE Modern_Spanish_CI_AS
						WHERE   I.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) AND (CONT.CODENTADM = @EAPB or ING.GENCONENTITY = @IdEntidad) and (FECRESANTSUPH between @FechaInicial and @FechaFinal or (FECGESTA between @FechaInicial and @FechaFinal and RESANTSUPH IS NOT NULL) or FECRESSERSIF between @FechaInicial and @FechaFinal or FECRESELIVIH between @FechaInicial and @FechaFinal or FECRESTSHNEO between @FechaInicial and @FechaFinal or FECRESHEMOGLO between @FechaInicial and @FechaFinal or FECGLISBASAL between @FechaInicial and @FechaFinal or FECRESCREATININA between @FechaInicial and @FechaFinal or FECRESHEMGLO between @FechaInicial and @FechaFinal or FECMICROALBU between @FechaInicial and @FechaFinal or FECRESBASDIAG between @FechaInicial and @FechaFinal)  
				ORDER BY FECRESANTSUPH ASC, FECRESSERSIF ASC, FECRESELIVIH ASC,FECRESTSHNEO ASC,FECRESHEMOGLO ASC,FECGLISBASAL ASC,FECRESCREATININA ASC,FECRESHEMGLO ASC,FECMICROALBU ASC,FECRESBASDIAG ASC

				OPEN CursorLaboratorosNoInterfaz
				FETCH NEXT FROM CursorLaboratorosNoInterfaz
				INTO @INPACIENT,@RESANTSUPH,@FECRESANTSUPH,@GESTAC,@FECGESTAC,@RESSERSIF,@FECRESSERSIF,@RESELIVIH,@FECRESELIVIH,@RESTSHNEO,@FECRESTSHNEO,@RESHEMOGLO,@FECRESHEMOGLO,@FECGLISBASAL,@RESCREATININA,@FECRESCREATININA,@RESHEMGLO,@FECRESHEMGLO,@FECMICROALBU,@FECHDL,@RESBASDIAG,@FECRESBASDIAG
				
		WHILE @@FETCH_STATUS = 0
				BEGIN	
				
			    if (select count(*) from #tmpADRES4505D where [4] = @INPACIENT) > 0 begin
					 
					--si el paciente ya esta registrado	procedemos actualizar Los campos
					update #tmpADRES4505D

					set    [79] = CASE WHEN @RESANTSUPH IS NOT NULL AND @GESTAC = 1 THEN @RESANTSUPH ELSE [79] END 
						 , [78] = CASE WHEN @FECRESANTSUPH IS NOT NULL AND @GESTAC = 1 THEN  @FECRESANTSUPH ELSE [78] END
						 , [81] = CASE WHEN @RESSERSIF IS NOT NULL THEN @RESSERSIF ELSE [81] END 
						 , [80] = CASE WHEN @FECRESSERSIF IS NOT NULL THEN  @FECRESSERSIF ELSE [80] END
						 , [83] = CASE WHEN @RESELIVIH IS NOT NULL THEN @RESELIVIH ELSE [83] END 
						 , [82] = CASE WHEN @FECRESELIVIH IS NOT NULL THEN  @FECRESELIVIH ELSE [82] END
						 , [85] = CASE when @RESTSHNEO IS NOT NULL THEN @RESTSHNEO ELSE [85] END 
						 , [84] = CASE when @FECRESTSHNEO IS NOT NULL THEN  @FECRESTSHNEO ELSE [84] END
					     , [104] = CASE WHEN @RESHEMOGLO  IS NOT NULL THEN @RESHEMOGLO  ELSE [104] END 
						 , [103] = CASE when @FECRESHEMOGLO IS NOT NULL THEN @FECRESHEMOGLO ELSE [103] END
						 , [105] = CASE WHEN @FECGLISBASAL IS NOT NULL THEN @FECGLISBASAL ELSE [105] END 
						 , [107] = CASE WHEN @RESCREATININA IS NOT NULL THEN @RESCREATININA ELSE [107] END 
						 , [106] = CASE WHEN @FECRESCREATININA IS NOT NULL THEN @FECRESCREATININA ELSE [106] END
					     , [109] = CASE WHEN @RESHEMGLO IS NOT NULL THEN @RESHEMGLO ELSE [109] END 
						 , [108] = CASE WHEN @FECRESHEMGLO IS NOT NULL THEN @FECRESHEMGLO ELSE [108] END
						 , [110] = CASE WHEN @FECMICROALBU IS NOT NULL THEN @FECMICROALBU ELSE [110] END 
						 , [111] = CASE WHEN @FECHDL  IS NOT NULL THEN @FECHDL ELSE [111] END 
						 , [113] = CASE WHEN @RESBASDIAG IS NOT NULL THEN @RESBASDIAG ELSE [113] END 
						 , [112] = CASE when @FECRESBASDIAG IS NOT NULL THEN @FECRESBASDIAG ELSE [112] END
						 where [4] =  @INPACIENT 
						 PRINT 'Modifico'
						print @RESHEMOGLO
				end else begin

					--si el paciente no esta registrado, procedemos a registrarlo
					print 'insert'
					insert into #tmpADRES4505D([0],[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[79],[78],[81],[80],[83],[82],[85],[84],[104],[103],[105],[107],[106],[109],[108],[110],[111],[113],[112])
					SELECT '2',null,null,case P.IPTIPODOC when 3 then 'TI'
											 when 2 then 'CE'
											 when 1 then 'CC'
											 when 4 then 'RC'
											 when 5 then 'PA'
											 when 7 then 'MS'
											  when 8 then 'NV'
											 when 6 then 'AS' END AS TIPOIDEN,
							 P.IPCODPACI, IPPRIAPEL, case convert(nvarchar(50),IPSEGAPEL) when null then 'NONE' when '' then 'NONE' else IPSEGAPEL END as IPSEGAPEL ,IPPRINOMB,case convert(nvarchar(50),IPSEGNOMB) when null then 'NONE' when '' then 'NONE' else IPSEGNOMB END as IPSEGNOMB ,IPFECNACI,
											 case P.IPSEXOPAC when  1 then 'M'
											 when  2 then 'F' END AS IPSEXOPAC,
							GE.TIPOET, ACT.codactivi , NIV.NIVECODIGO,
						  [79] =   CASE WHEN @RESANTSUPH IS NOT NULL AND @GESTAC = 1 THEN @RESANTSUPH END , [78] = CASE WHEN @FECRESANTSUPH IS NOT NULL AND @GESTAC = 1 THEN  @FECRESANTSUPH  END
						, [81] =   CASE WHEN @RESSERSIF IS NOT NULL THEN @RESSERSIF  END , [80] = CASE WHEN @FECRESSERSIF IS NOT NULL THEN  @FECRESSERSIF  END
						, [83] =   CASE WHEN @RESELIVIH IS NOT NULL THEN @RESELIVIH  END , [82] = CASE WHEN @FECRESELIVIH IS NOT NULL THEN  @FECRESELIVIH  END
						, [85]  =  CASE when @RESTSHNEO IS NOT NULL THEN @RESTSHNEO  END , [84] = CASE when @FECRESTSHNEO IS NOT NULL THEN  @FECRESTSHNEO  END
					    , [104] =  CASE WHEN @RESHEMOGLO  IS NOT NULL THEN @RESHEMOGLO END , [103] = CASE when @FECRESHEMOGLO IS NOT NULL THEN @FECRESHEMOGLO  END
						, [105] =  CASE WHEN @FECGLISBASAL IS NOT NULL THEN @FECGLISBASAL  END 
						, [107] =  CASE WHEN @RESCREATININA IS NOT NULL THEN @RESCREATININA  END , [106] = CASE WHEN @FECRESCREATININA IS NOT NULL THEN @FECRESCREATININA  END
					    , [109] =  CASE WHEN @RESHEMGLO IS NOT NULL THEN @RESHEMGLO  END , [108] = CASE WHEN @FECRESHEMGLO IS NOT NULL THEN @FECRESHEMGLO  END
						, [110] =  CASE WHEN @FECMICROALBU IS NOT NULL THEN @FECMICROALBU  END 
						, [111] =  CASE WHEN @FECHDL  IS NOT NULL THEN @FECHDL END 
						, [113] =  CASE WHEN @RESBASDIAG IS NOT NULL THEN @RESBASDIAG END , [112] = CASE when @FECRESBASDIAG IS NOT NULL THEN @FECRESBASDIAG  END
					
				   FROM INPACIENT AS P with(nolock)
						LEFT JOIN ADGRUETNI AS GE with(nolock) ON GE.CODGRUPOE COLLATE Modern_Spanish_CI_AS = P.CODGRUPOE COLLATE Modern_Spanish_CI_AS
						INNER JOIN ADACTIVID AS ACT with(nolock) ON ACT.codactivi COLLATE Modern_Spanish_CI_AS = P.CODACTIVI COLLATE Modern_Spanish_CI_AS
						LEFT JOIN ADNIVELED AS NIV with(nolock) ON NIV.NIVECODIGO COLLATE Modern_Spanish_CI_AS = P.NIVECODIGO COLLATE Modern_Spanish_CI_AS
					where P.IPCODPACI = @INPACIENT 
				END	

					FETCH NEXT FROM CursorLaboratorosNoInterfaz
					INTO @INPACIENT,@RESANTSUPH,@FECRESANTSUPH,@GESTAC,@FECGESTAC,@RESSERSIF,@FECRESSERSIF,@RESELIVIH,@FECRESELIVIH,@RESTSHNEO,@FECRESTSHNEO,@RESHEMOGLO,@FECRESHEMOGLO,@FECGLISBASAL,@RESCREATININA,@FECRESCREATININA,@RESHEMGLO,@FECRESHEMGLO,@FECMICROALBU,@FECHDL,@RESBASDIAG,@FECRESBASDIAG
				END 
			PRINT ' no Hay Interfaz Laboratorio'	
			
			CLOSE CursorLaboratorosNoInterfaz
			DEALLOCATE CursorLaboratorosNoInterfaz
	END
	
	
	PRINT 'RAFA'
	----------------------------------------------------------93---------------------------------------
	DECLARE @FECHAHISPACA AS DATE
	
	DECLARE Cursor93 CURSOR FOR 														
	 			SELECT QXR.IPCODPACI, HIS.FECHISPAC 
				FROM 
					HCQXREALI AS QXR 
					INNER JOIN HCHISPACA AS HIS  with(nolock) ON HIS.NUMEFOLIO = QXR.NUMEFOLIO AND HIS.NUMINGRES = QXR.NUMINGRES 
					INNER JOIN INCUPSIPS AS INC  with(nolock) ON INC.CODSERIPS  = QXR.CODSERIPS
				    INNER JOIN ADINGRESO AS I with(nolock) on HIS.NUMINGRES = I.NUMINGRES 
					LEFT  JOIN COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = I.CODCONTRA
				WHERE TIPBIOCER = 1 AND I.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) AND (CONT.CODENTADM = @EAPB or I.GENCONENTITY = @IdEntidad) AND
					HIS.FECHISPAC between @FechaInicial and @FechaFinal

		OPEN Cursor93
				FETCH NEXT FROM Cursor93
				INTO @INPACIENT, @FECHAHISPACA
				
		WHILE @@FETCH_STATUS = 0
				BEGIN	
				
			    if (select count(*) from #tmpADRES4505D where [4] = @INPACIENT ) > 0 begin
								
					--si el paciente ya esta registrado	procedemos actualizar Los campos
					update #tmpADRES4505D 
						set   [93] =   CASE WHEN @FECHAHISPACA IS NOT NULL THEN @FECHAHISPACA ELSE [93] END  --Fecha de resultado de Biopsia Seno Cervical
					 where[4] =    @INPACIENT  
				 print '93+93+93+93+93+93+93+93+93+93+93+93+93+'+ @INPACIENT  
				end else begin
				--si el paciente no esta registrado, procedemos a registrarlo
					insert into #tmpADRES4505D([0],[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[93])
			
					
					SELECT '2',null,null,case P.IPTIPODOC when 3 then 'TI'
											 when 2 then 'CE'
											 when 1 then 'CC'
											 when 4 then 'RC'
											 when 5 then 'PA'
											 when 7 then 'MS'
											 when 8 then 'NV'
											 when 6 then 'AS' END AS TIPOIDEN,
							 P.IPCODPACI, IPPRIAPEL, case convert(nvarchar(50),IPSEGAPEL) when null then 'NONE' when '' then 'NONE' else IPSEGAPEL END as IPSEGAPEL ,IPPRINOMB,case convert(nvarchar(50),IPSEGNOMB) when null then 'NONE' when '' then 'NONE' else IPSEGNOMB END as IPSEGNOMB ,IPFECNACI,
											 case P.IPSEXOPAC when  1 then 'M'
											 when  2 then 'F' END AS IPSEXOPAC,
							GE.TIPOET, ACT.codactivi , NIV.NIVECODIGO,
						 [93] =   CASE WHEN @FECHAHISPACA IS NOT NULL THEN @FECHAHISPACA  END										     
					FROM 	
						INPACIENT AS P with(nolock)
						LEFT JOIN ADGRUETNI AS GE with(nolock) ON GE.CODGRUPOE = P.CODGRUPOE
						INNER JOIN ADACTIVID AS ACT with(nolock) ON ACT.codactivi = P.CODACTIVI 
						LEFT JOIN ADNIVELED AS NIV with(nolock) ON NIV.NIVECODIGO = P.NIVECODIGO 
					where P.IPCODPACI = @INPACIENT 
				 print 'INSERTANDO 93+93+93+93+93+93+93+93+93+93+93+93+93+'+ @INPACIENT  
				end				
					FETCH NEXT FROM Cursor93
					INTO @INPACIENT, @FECHAHISPACA
				END 
			CLOSE Cursor93
			DEALLOCATE Cursor93

	-------------------------------------------------------------Patologias------------------------------------------------------
	-- De patologias sacamos informacion de la tabla HCORDPATO Cuando el paciente se encuentra hospitalizado y se le realizan Patologia y
	-- AMBORDPAT Se guarda la información de patologias deder consulta externa.
	DECLARE @FECORDMED DATE
	DECLARE @FECSENCER AS DATE
	DECLARE @BIOSENCER AS INT
	DECLARE @FECSENBACAF AS DATE
	DECLARE @BIOSENBACAF AS INT
	DECLARE @CODIPSSEC AS VARCHAR(20)
		
		
	DECLARE CursorPatologias CURSOR FOR 
		SELECT PATO.IPCODPACI,FECORDMED,FECSENCER,BIOSENCER,FECSENBACAF,BIOSENBACAF,CODIPSSEC  ---PATO.IPCODPACI,PATO.NUMINGRES,
		FROM HCORDPATO AS PATO with(nolock)
				INNER JOIN ADINGRESO AS I with(nolock) ON PATO.NUMINGRES  = I.NUMINGRES 
				INNER JOIN INPACIENT AS P with(nolock) ON P.IPCODPACI = PATO.IPCODPACI 
				INNER JOIN ADINGRESO AS ING with(nolock) ON ING.NUMINGRES = PATO.NUMINGRES
				LEFT  JOIN COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = ING.CODCONTRA
				LEFT  JOIN ADCENATEN AS  AD with(nolock) ON AD.CODCENATE = I.CODCENATE 
		WHERE   I.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) AND  (CONT.CODENTADM = @EAPB or ING.GENCONENTITY = @IdEntidad) and (PATO.FECSENCER   between @FechaInicial and @FechaFinal or PATO.FECSENBACAF   between @FechaInicial and @FechaFinal)  
	UNION ALL 
		SELECT PAT.IPCODPACI,FECORDMED,FECSENCER,BIOSENCER,FECSENBACAF,BIOSENBACAF,CODIPSSEC  ---PAT.IPCODPACI,PAT.NUMINGRES,
		FROM AMBORDPAT AS PAT with(nolock) 
				INNER JOIN ADINGRESO AS I with(nolock) ON PAT.NUMINGRES  = I.NUMINGRES 
				INNER JOIN INPACIENT AS P with(nolock) ON P.IPCODPACI = PAT.IPCODPACI 
				INNER JOIN ADINGRESO AS ING with(nolock) ON ING.NUMINGRES = PAT.NUMINGRES
				LEFT  JOIN COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = ING.CODCONTRA
				LEFT  JOIN ADCENATEN AS  AD with(nolock) ON AD.CODCENATE = I.CODCENATE 
		WHERE  I.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) AND  (CONT.CODENTADM = @EAPB or ING.GENCONENTITY = @IdEntidad) and (PAt.FECSENCER   between @FechaInicial and @FechaFinal or PAT.FECSENBACAF   between @FechaInicial and @FechaFinal)   
		ORDER BY FECSENCER ASC, FECSENBACAF ASC
														
	 			
			OPEN CursorPatologias
				FETCH NEXT FROM CursorPatologias
				INTO @INPACIENT, @FECORDMED, @FECSENCER, @BIOSENCER, @FECSENBACAF,@BIOSENBACAF,@CODIPSSEC
				
		WHILE @@FETCH_STATUS = 0
				BEGIN	
				
			    if (select count(*) from #tmpADRES4505D where [4] = @INPACIENT ) > 0 begin
								
					--si el paciente ya esta registrado	procedemos actualizar Los campos
					update #tmpADRES4505D 
						set   [93] =   CASE WHEN @FECSENCER IS NOT NULL THEN @FECSENCER ELSE [93] END  --Fecha de resultado de Biopsia Seno Cervical
						, [94] =   CASE WHEN @BIOSENCER IS NOT NULL THEN @BIOSENCER ELSE [94] END --Resultado de Biopsia cervical
					    , [99] =   CASE WHEN @FECSENBACAF IS NOT NULL then @FECORDMED ELSE [99] END --Fecha Toma Biopsia Seno por BACAF (Se deja la fecha Orden Medica) ' Aca pregunto si tiene Fecha Toma Biopsia Seno por BACAF, si tiene asigno la fecha de la orden medica.
						, [100] =  CASE WHEN @FECSENBACAF IS NOT NULL THEN @FECSENBACAF ELSE [100] END--Fecha de resultado de Biopsia de seno por bacaf
						, [101] =  CASE WHEN @BIOSENBACAF IS NOT NULL THEN @BIOSENBACAF ELSE [101] END --resultado de biopsia de seno por bacaf
						, [95] =   CASE WHEN @BIOSENCER IS NOT NULL THEN @CODIPSSEC ELSE [95] END --Asigno el codigo de habilitación solo si el campo de resultado de biopsia cervial no es nulo
						, [102] =  CASE WHEN @BIOSENBACAF IS NOT NULL THEN @CODIPSSEC ELSE [102] END  --Asigno el codigo de habilitación solo si el campo de Resultado Biopsia Seno BACAF cervial no es nulo
					 where[4] =    @INPACIENT  
				--	 print 'MODIFICANDO patologias bienbienbienbienbienbienbien'+ @INPACIENT  
				end else begin
				--si el paciente no esta registrado, procedemos a registrarlo
					insert into #tmpADRES4505D([0],[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[93],[94],[99],[100],[101],[95],[102])
					SELECT '2',null,null,case P.IPTIPODOC when 3 then 'TI'
											 when 2 then 'CE'
											 when 1 then 'CC'
											 when 4 then 'RC'
											 when 5 then 'PA'
											 when 7 then 'MS'
											  when 8 then 'NV'
											 when 6 then 'AS' END AS TIPOIDEN,
							 P.IPCODPACI, IPPRIAPEL, case convert(nvarchar(50),IPSEGAPEL) when null then 'NONE' when '' then 'NONE' else IPSEGAPEL END as IPSEGAPEL ,IPPRINOMB,case convert(nvarchar(50),IPSEGNOMB) when null then 'NONE' when '' then 'NONE' else IPSEGNOMB END as IPSEGNOMB ,IPFECNACI,
											 case P.IPSEXOPAC when  1 then 'M'
											 when  2 then 'F' END AS IPSEXOPAC,
							GE.TIPOET, ACT.codactivi , NIV.NIVECODIGO,
						 [93] =   CASE WHEN @FECSENCER IS NOT NULL THEN @FECSENCER  END
						,[94] =   CASE WHEN @BIOSENCER IS NOT NULL THEN @BIOSENCER  END
						,[99] =   CASE WHEN @FECSENBACAF IS NOT NULL THEN @FECORDMED  END -- Aca pregunto si tiene Fecha Toma Biopsia Seno por BACAF, si tiene asigno la fecha de la orden medica.
						,[100] =  CASE WHEN @FECSENBACAF IS NOT NULL THEN @FECSENBACAF  END
						,[101] =  CASE WHEN @BIOSENBACAF IS NOT NULL THEN @BIOSENBACAF  END
						,[95] =   CASE WHEN @BIOSENCER IS NOT NULL THEN @CODIPSSEC  END --Asigno el codigo de habilitación solo si el campo de resultado de biopsia cervial no es nulo
						,[102] =   CASE WHEN @BIOSENBACAF IS NOT NULL THEN @CODIPSSEC  END --Asigno el codigo de habilitación solo si el campo de Resultado Biopsia Seno BACAF  no es nulo
				     
					FROM 	
						INPACIENT AS P with(nolock)
						LEFT JOIN ADGRUETNI AS GE with(nolock) ON GE.CODGRUPOE = P.CODGRUPOE
						INNER JOIN ADACTIVID AS ACT with(nolock) ON ACT.codactivi = P.CODACTIVI 
						LEFT JOIN ADNIVELED AS NIV with(nolock) ON NIV.NIVECODIGO = P.NIVECODIGO 
					where P.IPCODPACI = @INPACIENT 
				end				
					FETCH NEXT FROM CursorPatologias
					INTO @INPACIENT,@FECORDMED, @FECSENCER, @BIOSENCER, @FECSENBACAF,@BIOSENBACAF,@CODIPSSEC
				END 
			CLOSE CursorPatologias
			DEALLOCATE CursorPatologias
			
			

	----------------------------------------------------------------------------ADCENATEN Centro Atención---------------------------------------------------------------------
	----------95-98-102 se realizan en las preguntas que me piden el resto de datos es porque hay una condicion que llenar estos campos solo si otra esa deligenciada--------
	

	----------------------------------------------------------------------------Imagenes---------------------------------------------------------------------
	--------------------------------------------De Imagenes sacamos informacion de la tabla HCORDIMAG y AMBORDIMA.------------------------------------------------------------
	DECLARE @FECHARESCLA DATE
	DECLARE @CLASIFICABIRADS AS INT
	DECLARE @CODIPSSECC AS VARCHAR(20)

	DECLARE CursorImagenes CURSOR FOR 
		SELECT IMG.IPCODPACI,FECHARESCLA,CLASIFICABIRADS,CODIPSSEC --NUMINGRES,IPCODPACI,
		FROM HCORDIMAG AS IMG with(nolock)
				INNER JOIN ADINGRESO AS I with(nolock) ON IMG.NUMINGRES  = I.NUMINGRES 
				INNER JOIN INPACIENT AS P with(nolock) ON P.IPCODPACI = IMG.IPCODPACI 
				INNER JOIN ADINGRESO AS ING with(nolock) ON ING.NUMINGRES = IMG.NUMINGRES
				LEFT  JOIN COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = ING.CODCONTRA
				LEFT  JOIN ADCENATEN AS  AD with(nolock) ON AD.CODCENATE = I.CODCENATE 
		WHERE   I.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) AND  (CONT.CODENTADM = @EAPB or ING.GENCONENTITY = @IdEntidad) and IMG.FECHARESCLA between @FechaInicial and @FechaFinal
	UNION ALL 
		SELECT IMA.IPCODPACI,FECHARESCLA,CLASIFICABIRADS,CODIPSSEC  ---PAT.IPCODPACI,PAT.NUMINGRES,
		FROM AMBORDIMA AS IMA with(nolock) 
				INNER JOIN ADINGRESO AS I with(nolock) ON IMA.NUMINGRES  = I.NUMINGRES 
				INNER JOIN INPACIENT AS P with(nolock) ON P.IPCODPACI = IMA.IPCODPACI 
				INNER JOIN ADINGRESO AS ING with(nolock) ON ING.NUMINGRES = IMA.NUMINGRES
				LEFT  JOIN COCONTRAT AS CONT with(nolock) ON CONT.CODCONTRA = ING.CODCONTRA
				LEFT  JOIN ADCENATEN AS  AD with(nolock) ON AD.CODCENATE = I.CODCENATE 
		WHERE  I.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) AND  (CONT.CODENTADM = @EAPB or ING.GENCONENTITY = @IdEntidad) and IMA.FECHARESCLA  between @FechaInicial and @FechaFinal    
		ORDER BY FECHARESCLA ASC
					
		
			OPEN CursorImagenes
				FETCH NEXT FROM CursorImagenes
				INTO @INPACIENT,@FECHARESCLA, @CLASIFICABIRADS,@CODIPSSECC

		WHILE @@FETCH_STATUS = 0
				BEGIN	
				
				if (select count(*) from #tmpADRES4505D where [4] = @INPACIENT) > 0 begin
				
					--si el paciente ya esta registrado	procedemos actualizar Los campos
					update #tmpADRES4505D 
					set   [96] =    CASE WHEN @FECHARESCLA IS NOT NULL THEN @FECHARESCLA ELSE [96] END  --Fecha Mamografía
						, [97] =   CASE WHEN @CLASIFICABIRADS IS NOT NULL THEN @CLASIFICABIRADS ELSE [97] END  --Resultado Mamografía
						, [98] =   CASE WHEN @CLASIFICABIRADS IS NOT NULL THEN @CODIPSSECC ELSE [98] END --Asigno al control siempre solo si tiene Mamografia
					 where [4]  =    @INPACIENT
					   
				end else begin
				
				--si el paciente no esta registrado, procedemos a registrarlo
				--	print 'insert'
					insert into #tmpADRES4505D([0],[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[96],[97],[98])
					SELECT '2',null,null,case P.IPTIPODOC when 3 then 'TI'
											 when 2 then 'CE'
											 when 1 then 'CC'
											 when 4 then 'RC'
											 when 5 then 'PA'
											 when 7 then 'MS'
											  when 8 then 'NV'
											 when 6 then 'AS' END AS TIPOIDEN,
							 P.IPCODPACI, IPPRIAPEL, case convert(nvarchar(50),IPSEGAPEL) when null then 'NONE' when '' then 'NONE' else IPSEGAPEL END as IPSEGAPEL ,IPPRINOMB,case convert(nvarchar(50),IPSEGNOMB) when null then 'NONE' when '' then 'NONE' else IPSEGNOMB END as IPSEGNOMB ,IPFECNACI,
											 case P.IPSEXOPAC when  1 then 'M'
											 when  2 then 'F' END AS IPSEXOPAC,
							GE.TIPOET, ACT.codactivi , NIV.NIVECODIGO,
						 [96] =  CASE WHEN @FECHARESCLA IS NOT NULL THEN @FECHARESCLA  END 
						,[97] =  CASE WHEN @CLASIFICABIRADS IS NOT NULL THEN @CLASIFICABIRADS  END 
						,[98] =  CASE WHEN @CLASIFICABIRADS IS NOT NULL THEN @CODIPSSECC  END 
				    FROM 	
						INPACIENT AS P with(nolock)
						LEFT JOIN ADGRUETNI AS GE with(nolock) ON GE.CODGRUPOE = P.CODGRUPOE
						INNER JOIN ADACTIVID AS ACT with(nolock) ON ACT.codactivi = P.CODACTIVI 
						LEFT JOIN ADNIVELED AS NIV with(nolock) ON NIV.NIVECODIGO = P.NIVECODIGO 
					where P.IPCODPACI = @INPACIENT 
				end
				 									
					FETCH NEXT FROM CursorImagenes
					INTO @INPACIENT,@FECHARESCLA, @CLASIFICABIRADS,@CODIPSSECC
				END 
			CLOSE CursorImagenes
			DEALLOCATE CursorImagenes

			----------------------------------------------------------------------.HCEXFISIC. Historia Fisica Paciente -----------------------------------------------------------------------------
					----------------------------------------------Aca consulto todos los paciente diferentes a los recien nacidos------------------------------------------------
	DECLARE @FECREGITE DATE
	DECLARE @PESOKILOGR AS VARCHAR(10)
	DECLARE @TALLAPACI AS INTEGER

	DECLARE CursorInformacionFisicas CURSOR FOR 
		SELECT F.IPCODPACI, FECREGITE,convert(varchar,PESOPACIE/1000) AS PESOKILOGR,TALLAPACI 
		FROM HCEXFISIC AS F with(nolock)
		WHERE   F.IPCODPACI IN(SELECT [4] COLLATE Modern_Spanish_CI_AS FROM #tmpADRES4505D where  [4] COLLATE Modern_Spanish_CI_AS NOT IN(	
																				SELECT DISTINCT P.IPCODPACI COLLATE Modern_Spanish_CI_AS
																					from 
																						HCRECINAC  AS RN
																						inner join ADINGRESO AS I on  RN.NUMINGRES COLLATE Modern_Spanish_CI_AS = I.NUMINGRES  COLLATE Modern_Spanish_CI_AS
																						inner join INPACIENT AS P on  P.IPCODPACI COLLATE Modern_Spanish_CI_AS = RN.IPCODPACI  COLLATE Modern_Spanish_CI_AS
																					where  
																						I.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) AND I.CODENTIDA = @EAPB  AND 
																						RN.FECHISPAC between @FechaInicial and @FechaFinal   and (EDADGESNAC IS NOT NULL or FECHISPAC IS NOT NULL)
																					)
																						) And FECREGITE = (Select Max(FECREGITE) from HCEXFISIC where IPCODPACI = F.IPCODPACI)
		ORDER BY FECREGITE  ASC
					
		
		OPEN CursorInformacionFisicas
				FETCH NEXT FROM CursorInformacionFisicas
				INTO @INPACIENT, @FECREGITE, @PESOKILOGR,@TALLAPACI

			WHILE @@FETCH_STATUS = 0
			BEGIN	
			--if (select count(*) from #tmpADRES4505D where [4] = @INPACIENT) > 0 begin
				--si el paciente ya esta registrado	procedemos actualizar Los campos
					update #tmpADRES4505D 
					set   [29] =   CASE WHEN @FECREGITE IS NOT NULL THEN @FECREGITE ELSE [29] END  
						, [30] =   CASE WHEN @PESOKILOGR IS NOT NULL THEN @PESOKILOGR ELSE [30] END 
						, [31] =   CASE WHEN @FECREGITE IS NOT NULL THEN @FECREGITE ELSE [31] END
						, [32] =   CASE WHEN @TALLAPACI IS NOT NULL THEN @TALLAPACI ELSE [32] END 
					where [4]  =   @INPACIENT  
				--print 'entro' + @INPACIENT
			--end
				
				FETCH NEXT FROM CursorInformacionFisicas
				INTO @INPACIENT, @FECREGITE, @PESOKILOGR,@TALLAPACI
			END
			CLOSE CursorInformacionFisicas
			DEALLOCATE CursorInformacionFisicas
			
	
			--------------------------------------48-54-55-59-60-61-70-71-74-86-87-88-89-90-91-92---------------------------------
			DECLARE @FECNAC DATE
			DECLARE @48 int 
			DECLARE @54 int
			DECLARE @55 DATE
			DECLARE @59 int
			DECLARE @60 int
			DECLARE @61 int
			DECLARE @70 int
			DECLARE @71 int
			DECLARE @74 int
			DECLARE @86 int
			DECLARE @87 DATE
			DECLARE @88 int
			DECLARE @89 int
			DECLARE @90 int
			DECLARE @91 DATE
			DECLARE @92 int
			DECLARE @EDAD INT
			DECLARE @SEXO VARCHAR(1)
			
	DECLARE PreguntasCondiciones CURSOR FOR 

	SELECT [4],[9],[10],[48],[54],[55],[59],[60],[61],[70],[71],[74],[86],[87],[88],[89],[90],[91],[92] FROM #tmpADRES4505D as T WITH(NOLOCK) WHERE [9] is not null 
		
		OPEN PreguntasCondiciones
				FETCH NEXT FROM PreguntasCondiciones
				INTO @INPACIENT,@FECNAC,@SEXO,@48,@54,@55,@59,@60,@61,@70,@71,@74,@86,@87,@88,@89,@90,@91,@92
				
				WHILE @@FETCH_STATUS = 0
				BEGIN	
					SELECT	@EDAD = (DATEDIFF(MONTH ,@FECNAC,(CONVERT(DATE, @FechaFinal)))/12) --SACO MESES DIVIDO EN 12 AÑOS
						PRINT @EDAD
						PRINT @INPACIENT
						PRINT @SEXO
					update #tmpADRES4505D 
						set   [48] =  CASE WHEN (@EDAD < 2) THEN '0' ELSE '22' END 
							, [54] =  CASE WHEN (@EDAD < 10 OR @EDAD > 60) THEN '0' ELSE '21' END	
							, [55] =  CASE WHEN (@EDAD < 10 OR @EDAD > 60) THEN '1845-01-01' ELSE '1800-01-01' END
							, [59] =  CASE WHEN (@SEXO = 'M') THEN '0' WHEN (@SEXO = 'F' AND @EDAD < 10 OR @EDAD > 60) THEN '0' ELSE '21' END
							, [60] =  CASE WHEN (@SEXO = 'M') THEN '0' WHEN (@SEXO = 'F' AND @EDAD < 10 OR @EDAD > 60) THEN '0' ELSE '21' END
							, [61] =  CASE WHEN (@SEXO = 'M') THEN '0' WHEN (@SEXO = 'F' AND @EDAD < 10 OR @EDAD > 60) THEN '0' ELSE '21' END
							, [70] =  CASE WHEN (@EDAD > 10) THEN '0' ELSE '21' END 
							, [71] =  CASE WHEN (@EDAD > 10) THEN '0' ELSE '21' END
							, [74] =  CASE WHEN [24] = '1' THEN '999' ELSE '0' END --PRINT [24]
							, [86] =  CASE WHEN (@SEXO = 'M' OR @EDAD < 11) THEN '0' ELSE '22' END
							, [87] =  CASE WHEN (@SEXO = 'M' OR @EDAD < 11) THEN '1845-01-01' ELSE '1800-01-01' END
							, [89] =  CASE WHEN (@SEXO = 'M' OR @EDAD < 11) THEN '0' ELSE '999' END
							, [90] =  CASE WHEN (@SEXO = 'M' OR @EDAD < 11) THEN '0' ELSE '999' END
							, [91] =  CASE WHEN (@EDAD < 11) THEN '1845-01-01' ELSE '1800-01-01' END
							, [92] =  CASE WHEN (@SEXO = 'M' OR @EDAD < 11) THEN '0' ELSE '999' END
						where [4]  =   @INPACIENT
					
			
					FETCH NEXT FROM PreguntasCondiciones
						INTO @INPACIENT,@FECNAC,@SEXO,@48,@54,@55,@59,@60,@61,@70,@71,@74,@86,@87,@88,@89,@90,@91,@92
				END

				CLOSE PreguntasCondiciones
				DEALLOCATE PreguntasCondiciones

				select * from #tmpADRES4505D
	--select * from #tmpADRES4505D
	
	/*insert into #tmpADRES4505D 

	select * from ADRES4505D
	select top 100 *  from INPACIENT */
	

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte de pacientes requerido por la Resolución 4505 del Ministerio de Salud de Colombia, la cual obliga a las IPS a informar las actividades de protección específica, detección temprana e intervención de factores de riesgo realizadas. Recibe como parámetros el centro de atención, la EAPB (EPS), la entidad, y el rango de fechas de consulta. Cruza información de pacientes (cédula, nombre, sexo, fecha de nacimiento, tipo de documento) desde INPACIENT, con las citas y atenciones registradas en ADCONCOEX, el catálogo de servicios CUPS/IPS desde INCUPSIPS para identificar el tipo de actividad realizada (oftalmología, nutrición, psicología, asesoría pre y posparto, entre otras), y datos de grupos étnicos, actividad económica y nivel educativo. Adicionalmente incorpora variables de riesgos clínicos del paciente como gestación, hipertensión, tuberculosis, cáncer de cuello uterino, violencia, entre otros, construyendo una tabla temporal con más de cien campos numéricos y de fechas que corresponden a cada indicador exigido por la norma para ser entregado a los entes de control del sistema de salud colombiano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ConsultarResolucion4505';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ConsultarResolucion4505';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de pacientes y sus indicadores clínicos (consultas, riesgos, vacunación, laboratorios, imágenes, patologías, diagnósticos) consolidados según la Resolución 4505 de Colombia para un centro de atención, EAPB y rango de fechas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarResolucion4505';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro @CentroAtencion debe ser una lista parseable por dbo.splitstring (uno o varios códigos de centro de atención).; Debe existir al menos un registro en HCPARLABO para los centros indicados a fin de determinar si hay interfaz de laboratorio (INTLABOAC).; El rango @FechaInicial - @FechaFinal acota todas las fuentes clínicas consultadas.; La afiliación se valida por contrato (COCONTRAT.CODENTADM = @EAPB) o por entidad de ingreso (ADINGRESO.GENCONENTITY = @IdEntidad).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarResolucion4505';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ConsultarResolucion4505';
-- GO

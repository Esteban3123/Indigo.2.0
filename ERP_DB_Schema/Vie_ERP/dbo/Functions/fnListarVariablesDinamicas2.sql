
-- =============================================
-- Author:		Rafael Eduardo Patiño Cabrera
-- Create date: 09/05/2019
-- Description:	listar variables dinamica de manera masiva - odontologia
-- =============================================
CREATE FUNCTION [dbo].[fnListarVariablesDinamicas2]
(
 @IDHCHISPACA INT
)
RETURNS @T table(Query VARCHAR(MAX)) 
AS
BEGIN

  DECLARE @queryANT AS VARCHAR(MAX);
  DECLARE @queryRES AS VARCHAR(MAX); 
  DECLARE @queryEXA AS VARCHAR(MAX);
  DECLARE @columnasANT VARCHAR(max)='';
  DECLARE @columnasRES VARCHAR(max)='';
  DECLARE @columnasEXA VARCHAR(max)='';

  
    DECLARE @IDMODELOHC as integer
	declare @FechaHistoria as date
    select @IDMODELOHC = IDMODELOHC,@FechaHistoria = FECHISPAC   from HCHISPACA where ID = @IDHCHISPACA  

	
	SELECT @columnasANT = coalesce(@columnasANT + quotename( cast(VARIABLE as varchar(100))) + ',', '')
	FROM (select distinct vr.ID, vr.VARIABLE from ANTVARIABLES vr inner join PRHCXANTVARIABLES vrHC on vr.ID = vrHC.IDANTVARIABLES where vrHC.IDMODELOHC = @IDMODELOHC) as DTM
	order by DTM.ID 
	
	
	SELECT @columnasRES = coalesce(@columnasRES + quotename( cast(VARIABLE as varchar(100))) + ',', '')
	FROM (select distinct vr.ID, vr.VARIABLE from RSVARIABLES vr inner join PRHCXRSVARIABLES vrHC on vr.ID = vrHC.IDRSVARIABLES where vrHC.IDMODELOHC = @IDMODELOHC and VARIABLE not in ('Xerostomía','Fractura','Sinusitis') ) as DTM
	order by DTM.ID 
	

	SELECT @columnasEXA = coalesce(@columnasEXA + quotename( cast(VARIABLE as varchar(100))) + ',', '')
	FROM (select distinct vr.ID, vr.VARIABLE from EXAVARIABLES vr inner join PRHCXEXAVARIABLES vrHC on vr.ID = vrHC.IDEXAVARIABLES where vrHC.IDMODELOHC = @IDMODELOHC) as DTM
	order by DTM.ID 
	

	if @columnasANT = '' or @columnasRES= '' or @columnasEXA = '' begin
		return	
	end
	

	set @columnasANT  = left(@columnasANT,LEN(@columnasANT)-1)
	set @columnasRES  = left(@columnasRES,LEN(@columnasRES)-1)
	set @columnasEXA  = left(@columnasEXA,LEN(@columnasEXA)-1)

	
	DECLARE @nombreTablaANT varchar(20) 
	DECLARE @nombreTablaRES varchar(20)
	DECLARE @nombreTablaEXA varchar(20)

	

	declare @Mes varchar(20) = month(@FechaHistoria)
	if len(@Mes) =1  
	begin
		set @Mes = '0' + @Mes 
		SET @nombreTablaANT= CONCAT('ANTVALORES', YEAR(@FechaHistoria) , @Mes);
		SET @nombreTablaRES= CONCAT('RSVALORES', YEAR(@FechaHistoria) , @Mes);
		SET @nombreTablaEXA= CONCAT('EXAVALORES', YEAR(@FechaHistoria) , @Mes);
	end else begin
		SET @nombreTablaANT= CONCAT('ANTVALORES', YEAR(@FechaHistoria) , MONTH(@FechaHistoria));
		SET @nombreTablaRES= CONCAT('RSVALORES', YEAR(@FechaHistoria) , MONTH(@FechaHistoria));
		SET @nombreTablaEXA= CONCAT('EXAVALORES', YEAR(@FechaHistoria) , MONTH(@FechaHistoria));
	end


	

	SELECT @queryANT = CONCAT('
	SELECT ', @columnasANT, ',IDHCHISPACA as IDHCHISPACA_ANT ','
	from
	(
		SELECT VARIABLE, IIF(L.NOMBRE is null,VALOR,L.NOMBRE) as VALOR,B.IDHCHISPACA 
		FROM ANTVARIABLES A ',
		' LEFT JOIN ', @nombreTablaANT, 
		' B on A.ID=B.IDANTVARIABLE  LEFT JOIN ANTVARIABLESL L on B.IDITEMLISTA = L.ID ',
		' WHERE IDHCHISPACA=',@IDHCHISPACA,'
	) as st
	pivot
	(
		max(VALOR)
		FOR VARIABLE in (' + @columnasANT + ')
	) as pivottable');

	
	
	SELECT @queryRES = CONCAT('
	SELECT ', @columnasRES, ',IDHCHISPACA as IDHCHISPACA_RES ','
	from
	(
		SELECT VARIABLE, IIF(L.NOMBRE is null,VALOR,L.NOMBRE) as VALOR,B.IDHCHISPACA 
		FROM RSVARIABLES A ',
		' LEFT JOIN ', @nombreTablaRES,  
		' B on A.ID=B.IDRSVARIABLE  LEFT JOIN RSVARIABLESL L on B.IDITEMLISTA = L.ID ',
		' WHERE IDHCHISPACA=',@IDHCHISPACA,'
	) as st
	pivot
	(
		MAX(VALOR)
		FOR VARIABLE in (' + @columnasRES + ')
	) as pivottable');


	
	SELECT @queryEXA = CONCAT('
	SELECT ', @columnasEXA, ',IDHCHISPACA as IDHCHISPACA_EXA ','
	from
	(
		SELECT VARIABLE, IIF(L.NOMBRE is null,VALOR,L.NOMBRE) as VALOR,B.IDHCHISPACA  
		FROM EXAVARIABLES A ',
		' LEFT JOIN ', @nombreTablaEXA, 
		' B on A.ID=B.IDEXAVARIABLE  LEFT JOIN EXAVARIABLESL L on B.IDITEMLISTA = L.ID ',
		' WHERE IDHCHISPACA=',@IDHCHISPACA,'
	) as st
	pivot
	(
		max(VALOR)
		FOR VARIABLE in (' + @columnasEXA + ')
	) as pivottable');

	

	DECLARE @SQL NVARCHAR(MAX); 

	if @IDMODELOHC = 33 or @IDMODELOHC = 28 or @IDMODELOHC = 42 begin
	
			set @SQL = 'select CASE C.IPTIPODOC  WHEN 1 THEN ''Cédula de Ciudadanía'' When 2 Then ''Cédula de Extranjería'' When 3 Then ''Tarjeta de Identidad'' When 4 Then ''Registro Civil'' When 5 Then ''Pasaporte'' When 6 Then ''Adulto Sin Identificación'' When 7 Then ''Menor Sin Identificación'' When 8 Then ''Número único de identificación personal'' When 9 Then ''Certificado Nacido Vivo'' When 10 Then ''Carnet Diplomático'' When 11 Then ''Salvoconducto'' When 12 Then ''Permiso especial de Permanencia'' END AS ''Tipo de ID del paciente'',
						C.IPCODPACI AS ''Numero de id del paciente'',C.IPPRIAPEL AS ''Apellido 1'',C.IPSEGAPEL AS ''Apellido 2'',C.IPPRINOMB AS ''Nombre 1'',C.IPSEGNOMB AS ''Nombre 2'',C.IPFECNACI AS ''Fecha de nacieminto'', (cast(datediff(dd,IPFECNACI,[Common].[GETDATE]()) / 365.25 as int)) as ''Edad en años'',
						CASE IPSEXOPAC WHEN 1 THEN ''Masculino'' when 2 then ''Femenino'' end as ''Sexo'', D.NUMINGRES,
						E.Code AS ''Código Grupo de atención'',E.Name AS ''Nombre Grupo de atención'',F.CODENTIDA as ''Código Entidad'',F.NOMENTIDA as ''Nombre entidad'',
						case EntityType when 1 then ''EPS Contributivo'' when 2 then ''EPS Subsidiado'' when 3 then ''ET Vinculados Municipios'' when 4 then ''ET Vinculados Departamentos'' when 5 then ''ARL Riesgos Laborales'' when 6 then ''MP Medicina Prepagada'' when 7 then ''IPS Privada'' when 8 then ''IPS Publica'' when 9 then ''Regimen Especial'' when 10 then ''Accidentes de transito'' when 11 then ''Fosyga'' when 12 then ''Otros'' when 13 then ''Aseguradoras'' when 99 then ''Particulares'' end as ''Tipo de entidad'',
						G.CODPROSAL AS ''Código del profesional'',G.NOMMEDICO AS ''Nombre del profesional'',
						(H.CEO_C + H.CPO_C) AS ''Total dientes cariados'',
						(H.CEO_E + H.CPO_P) AS ''Total dientes perdidos-Extraidos'',
						(H.CEO_O + H.CPO_O) AS ''Total dientes Obturados'',
						(CEO_C + CEO_E + CEO_O) AS ''Suma de CEO'',
						(CPO_C + CPO_P + CPO_O) AS ''Suma de CPO'',
						A.FECHISPAC AS ''Fecha del folio'',
						I.CODDIAGNO AS ''Codigo Diagnostico'',
						I.NOMDIAGNO AS ''Diagnostico principal'',
						Case GESTACION When 0 then ''No aplica'' When 1 then ''Si'' When 2 then ''No'' When 2 then ''Riesgo no evaluado'' end as ''Estado Embarazo'',
						Case VICTMALTRATO when 0 then ''No aplica'' when 1 then ''Si es mujer victima de maltrato'' when 2 then ''Si es menor victima de maltrato'' when 3 then ''No'' when 21 then ''Riesgo no evaluado'' end as ''Victima de maltrato'',
						Case VICTVIOLESEXU when 1 then ''Si'' when 2 then ''No'' when 21 Then ''Riesgo no evaluado'' end as ''Victima Violencia sexual'',A.IDMODELOHC, A.ID , 
						case TIPCITMED when 1 then ''Primera vez'' when 2 then ''Control'' end as ''Tipo Cita'',REPLACE(MOTCONSUL, ''"'' , '''') AS ''Motivo Consulta'',REPLACE(ENFACTUAL, ''"'' , '''') AS ''Enfermedad Actual'',REPLACE(ANALISISP, ''"'' , '''') AS ''Analisis'',
						tmpRES.*,tmpANT.*,tmpEXA.* 
						From dbo.HCHISPACA A
						left JOIN dbo.ODONTOCONTROL B WITH (NOLOCK) ON A.NUMEFOLIO = B.NUMEFOLIO AND A.IPCODPACI = B.IPCODPACI AND A.NUMINGRES = B.NUMINGRES 
						INNER JOIN HCURGING1  X ON X.NUMEFOLIO = A.NUMEFOLIO AND X.IPCODPACI  = A.IPCODPACI AND X.NUMINGRES = A.NUMINGRES 
						INNER JOIN dbo.INPACIENT C WITH (NOLOCK) ON A.IPCODPACI = C.IPCODPACI 
						INNER JOIN dbo.ADINGRESO D WITH (NOLOCK) ON A.NUMINGRES = D.NUMINGRES 
						INNER JOIN Contract.CareGroup E WITH (NOLOCK) ON E.Id = D.GENCAREGROUP 
						INNER JOIN dbo.INENTIDAD F WITH (NOLOCK) ON F.CODENTIDA = D.CODENTIDA 
						INNER JOIN dbo.INPROFSAL G WITH (NOLOCK) ON G.CODPROSAL = A.CODPROSAL 
						left JOIN dbo.ODONTOCONTROLVALO H WITH (NOLOCK) ON H.IDODONTOCONTROL = A.ID 
						INNER JOIN dbo.INDIAGNOS  I WITH (NOLOCK) ON I.CODDIAGNO  = a.CODDIAGNO
						LEFT JOIN  dbo.HCRIESGOSP J  WITH (NOLOCK) ON J.NUMINGRCES = A.NUMINGRES 
						left join (  '+ isnull(@queryANT,'')  + ' ) tmpANT on tmpANT.IDHCHISPACA_ANT = A.ID
						left join (  '+ isnull(@queryRES,'') + ' ) tmpRES on tmpRES.IDHCHISPACA_RES = A.ID
						left join (  '+ isnull(@queryEXA,'')  + ' ) tmpEXA on tmpEXA.IDHCHISPACA_EXA = A.ID
						Where A.IDMODELOHC = ' + convert(varchar(20),@IDMODELOHC) + '   and A.ID =' + convert(varchar(20),@IDHCHISPACA) + ' '
	
	end else begin
		
				set @SQL = 'select  ING.NUMINGRES as Ingreso,ING.CODCENATE as CodCentro,RTRIM(cen.NOMCENATE) as CentroAtencion ,RTRIM(PAC.IPPRINOMB) AS ''1.PrimerNombre'',RTRIM(PAC.IPSEGNOMB) AS ''2.SegundoNombre'',RTRIM(PAC.IPPRIAPEL) AS ''3.PrimerApellido'',RTRIM(PAC.IPSEGAPEL) AS ''4.SegundoApellido'',
							case PAC.IPTIPODOC when 1 then ''CC''  when 2 then ''CE''  when 3 then ''TI''  when 4 then ''RC''  when 5 then ''PA''
							when 6 then ''AS''  when 7 then ''MS''  when 8 then ''NUIP''  END as ''5.TipoIdentificacion'',RTRIM(ING.IPCODPACI) as ''6.Identificacion'', cast(PAC.IPFECNACI as DATE) AS ''7.FechaNacimiento'',
							YEAR(GETDATE()) - YEAR(PAC.IPFECNACI) AS Edad,case PAC.IPSEXOPAC when 1 then ''M'' when 2 then ''F'' END as ''8.Sexo'',
							CASE WHEN CGR.EntityType=1 THEN ''Contributivo'' WHEN CGR.EntityType=2  THEN ''Subsidiado'' WHEN CGR.EntityType=3 THEN ''ET Vinculados Municipios - N''
							WHEN CGR.EntityType=4 THEN ''ET Vinculados Departamentos - N'' WHEN CGR.EntityType=5 THEN ''ARL Riesgos Laborales - P'' WHEN CGR.EntityType=6 THEN ''MP Medicina Prepagada - P''
							WHEN CGR.EntityType=7 THEN ''IPS Privada - P'' WHEN CGR.EntityType=8 THEN ''IPS Publica - P'' WHEN CGR.EntityType=9 THEN ''Regimen Especial'' WHEN CGR.EntityType=10 THEN ''Accidentes de transito -P''
							WHEN CGR.EntityType=11 THEN ''Fosyga - P'' WHEN CGR.EntityType=12 THEN ''Otros - N'' WHEN CGR.EntityType=13 THEN ''Aseguradoras - P'' WHEN CGR.EntityType=99 THEN ''Particulares - P'' END as ''9.Regimen'',
							ING.CODENTIDA as ''10.CodigoEPSEntidadTerritorial'', ENT.NOMENTIDA AS ''Entidad'' ,
							CASE WHEN GRE.TIPOET IS NULL THEN ''6'' ELSE GRE.TIPOET END AS ''11.GrupoEtnico'',
							CASE WHEN PE.TIPOPOBESP IS NULL THEN ''99'' ELSE PE.TIPOPOBESP  END AS ''12.PoblacionEspecial'',
							SUBSTRING(UBI.UBINOMBRE ,1,15) AS ''13.MunicipioResidencia'',case when PAC.IPTELMOVI is null then PAC.IPTELEFON ELSE PAC.IPTELMOVI END AS ''14.Telefono'',PAC.IPDIRECCI AS Direccion ,
							CAST(HC.FECHISPAC AS date )  as ''17.FechaHistoria'',ING.CODDIAEGR AS ''CIE-10'',DIAG.NOMDIAGNO AS Diagnostico,
							(EFIS.PESOPACIE /1000) AS ''23.Peso'',EFIS.TALLAPACI  AS ''24.Talla'',Round(((EFIS.PESOPACIE /1000)/(EFIS.TALLAPACI*EFIS.TALLAPACI)*1000),2,0)  as ''IMC'',
							EFIS.TENARTSIS  AS ''25.TAS'',EFIS.TENARTDIA AS ''26.TAD'',
							S.NOMMEDICO as ''medico'',cast(EFIS.NEOPERABD as char) as ''PerimetroAbdominal'',URG.MOTCONSUL AS MotivoConsulta,HC.IDMODELOHC, HC.ID , tmpRES.*,tmpANT.*,tmpEXA.*
							FROM         dbo.ADINGRESO AS ING INNER JOIN
							dbo.HCHISPACA AS HC WITH (NOLOCK) ON ING.NUMINGRES =HC.NUMINGRES AND ING.IPCODPACI = HC.IPCODPACI  INNER JOIN
							dbo.HCURGING1 AS URG ON HC.NUMINGRES =URG.NUMINGRES AND HC.IPCODPACI =URG.IPCODPACI AND HC.NUMEFOLIO =URG.NUMEFOLIO  INNER JOIN
							dbo.INDIAGNOS AS DIAG WITH (NOLOCK) ON ING.CODDIAEGR =DIAG.CODDIAGNO INNER JOIN
							dbo.INPACIENT AS PAC WITH (NOLOCK) ON ING.IPCODPACI =PAC.IPCODPACI INNER JOIN
							dbo.INENTIDAD AS ENT WITH (NOLOCK) ON ING.CODENTIDA =ENT.CODENTIDA INNER JOIN
							dbo.ADCENATEN AS Cen on Cen.CODCENATE =ING.CODCENATE INNER JOIN
							dbo.INPROFSAL AS S ON S.CODPROSAL =hc.CODPROSAL INNER JOIN
							dbo.INUBICACI AS UBI ON PAC.AUUBICACI =UBI.AUUBICACI LEFT OUTER JOIN
							Contract .CareGroup AS CGR WITH (NOLOCK) ON ING.GENCAREGROUP =CGR.Id LEFT OUTER JOIN
							dbo.ADGRUETNI AS GRE WITH (NOLOCK) ON PAC.CODGRUPOE =GRE.CODGRUPOE LEFT OUTER JOIN
							  (SELECT PA.ID, PA.IPCODPACI,PA.IDADPOBESPE FROM dbo.ADPOBESPEPAC AS PA INNER JOIN
								(SELECT MIN(ID) AS ID, IPCODPACI FROM dbo.ADPOBESPEPAC GROUP BY IPCODPACI ) AS G ON G.ID=PA.ID) AS G2 ON PAC.IPCODPACI =G2.IPCODPACI LEFT OUTER JOIN
							dbo.ADPOBESPE AS PE ON PE.ID =G2.IDADPOBESPE INNER JOIN
							dbo.HCEXFISIC AS EFIS ON ING.NUMINGRES =EFIS.NUMINGRES AND EFIS.IPCODPACI =ING.IPCODPACI AND HC.NUMEFOLIO =EFIS.NUMEFOLIO
							left join (  '+ isnull(@queryANT,'')  + ' ) tmpANT on tmpANT.IDHCHISPACA_ANT = HC.ID
						   left join (  '+ isnull(@queryRES,'') + ' ) tmpRES on tmpRES.IDHCHISPACA_RES = HC.ID
						   left join (  '+ isnull(@queryEXA,'')  + ' ) tmpEXA on tmpEXA.IDHCHISPACA_EXA = HC.ID
							Where HC.IDMODELOHC = ' + convert(varchar(20),@IDMODELOHC) + '   and HC.ID =' + convert(varchar(20),@IDHCHISPACA) + ' '
		
	end
	    
   insert into @T(Query) values (@SQL )   
  return
END
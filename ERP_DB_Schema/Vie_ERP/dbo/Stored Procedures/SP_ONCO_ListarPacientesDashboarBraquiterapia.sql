CREATE PROCEDURE [dbo].[SP_ONCO_ListarPacientesDashboarBraquiterapia] 
 @CentroAtencion varchar(500),
 @Estado int,
 @OrdenConCita bit
AS
BEGIN

	 if @OrdenConCita = 0 begin
		select 
			A.ID,
			case when  FECHAPLANEA is Null  then 'Sin Programar Planeación' Else 'Planeación programada' End as 'Planeacion',isnull(NOREFIEREFECHAPLANEA, 0) as NOREFIEREFECHAPLANEA,
			A.CODPROSAL,A.CODDIAGNO,/*A.CODCENATE*/A.FECHAORDEN, 
			rtrim(F.CODPROSAL) as CODPROSAL,
			rtrim(A.CODSERIPS) as CODSERIPS,
			Rtrim(A.IPCODPACI) As Identificacion, A.FECHAORDEN,
			Rtrim(IPNOMCOMP) As NombrePaciente,
			Rtrim(E.CODSERIPS) +' - '+ Rtrim(DESSERIPS) + '. ' + isnull(CD.name,'') As Procedimiento,
			Rtrim(CE.NOMCENATE) as CentroAtencion,
			Rtrim(F.NOMMEDICO) as Profesional,
			Rtrim(D.CODDIAGNO) + ' - ' + Rtrim(D.NOMDIAGNO ) as Diagnostico,
			Rtrim(D.CODDIAGNO) as CodigoDiagnostico,
			Rtrim(ESP.DESESPECI) as Especialidad,
			Rtrim(ltrim(CE.CODCENATE)) as 'CODCENATE',Rtrim(F.CODPROSAL) as 'CODPROSAL',Rtrim(ESP.CODESPECI) as 'CODESPECI',
			A.IDDESCRIPCIONRELACIONADA,A.FECHAPROSIMULA, A.FECHAPLANEA, 
			CD.Code + ' - ' + CD.Name as DescripcionRelacionada,
			[dbo].[Edad](B.IPFECNACI,[Common].[GETDATE]()) as Edad,
			ORD.MANEXTPRO as Extramural,
			(select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 5 and IESTADOIN IN ('','P','B')  ) as NUMINGRES,
			CASE ORD.MANEXTPRO when 1 then 'Ambulatoria' else 'Hospitalario' END as OrigenOrden,
			'Autorización' AS Autorizacion,
			ORD.AUTO EntityId,
			Ingreso = (select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 5 and IESTADOIN IN ('','P', 'B')  ),
			[dbo].[RiskFactorAlert](A.IPCODPACI,'',1) AS IconoRiesgos, [dbo].[RiskFactorAlert](A.IPCODPACI,(select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 5 and IESTADOIN IN ('','P','B')),2) AS IconoEscalas
		from HCRADORDEN A 
			INNER JOIN HCORDPRON ORD with(nolock) ON A.IDHCORDPRON = ORD.AUTO  AND A.ESTADO = @Estado AND A.CITABRAQUIASIGNADA IS NULL
			INNER JOIN INCUPSIPS  E with(nolock) ON A.CODSERIPS = E.CODSERIPS
			INNER JOIN INPACIENT B ON A.IPCODPACI = B.IPCODPACI 
			INNER JOIN ADCENATEN CE with(nolock) ON CE.CODCENATE = A.CODCENATEULTIMO
			INNER JOIN INPROFSAL   F with(nolock) ON A.CODPROSAL  = F.CODPROSAL
			INNER JOIN INDIAGNOS  D with(nolock) ON A.CODDIAGNO = D.CODDIAGNO
			INNER JOIN INESPECIA ESP with(nolock) ON ESP.CODESPECI  = A.CODESPECI
			LEFT JOIN contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
			LEFT JOIN contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId
		WHERE E.SERIPSDASH = 13 AND A.CODCENATEULTIMO IN (SELECT Value FROM dbo.SplitString(@CentroAtencion))

	end else if @OrdenConCita =1 begin


			select 
			A.ID,
			case when  FECHAPLANEA is Null  then 'Sin Programar Planeación' Else 'Planeación programada' End as 'Planeacion',isnull(NOREFIEREFECHAPLANEA, 0) as NOREFIEREFECHAPLANEA,
			A.CODPROSAL,A.CODDIAGNO,/*A.CODCENATE*/A.FECHAORDEN, 
			rtrim(F.CODPROSAL) as CODPROSAL,
			rtrim(A.CODSERIPS) as CODSERIPS,
			Rtrim(A.IPCODPACI) As Identificacion, A.FECHAORDEN,
			Rtrim(IPNOMCOMP) As NombrePaciente,
			Rtrim(E.CODSERIPS) +' - '+ Rtrim(DESSERIPS) + '. ' + isnull(CD.name,'') As Procedimiento,
			Rtrim(CE.NOMCENATE) as CentroAtencion,
			Rtrim(F.NOMMEDICO) as Profesional,
			Rtrim(D.CODDIAGNO) + ' - ' + Rtrim(D.NOMDIAGNO ) as Diagnostico,
			Rtrim(D.CODDIAGNO) as CodigoDiagnostico,
			Rtrim(ESP.DESESPECI) as Especialidad,
			Rtrim(ltrim(CE.CODCENATE)) as 'CODCENATE',Rtrim(F.CODPROSAL) as 'CODPROSAL',Rtrim(ESP.CODESPECI) as 'CODESPECI',
			A.IDDESCRIPCIONRELACIONADA,A.FECHAPROSIMULA, A.FECHAPLANEA, 
			CD.Code + ' - ' + CD.Name as DescripcionRelacionada,
			[dbo].[Edad](B.IPFECNACI,[Common].[GETDATE]()) as Edad,
			ORD.MANEXTPRO as Extramural,
			(select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 5 and IESTADOIN IN ('','P','B')  ) as NUMINGRES,
            CASE ORD.MANEXTPRO when 1 then 'Ambulatoria' else 'Hospitalario' END as OrigenOrden,
			(select top 1 FECHORAIN from AGASICITA where IDHCRADORDEN = A.ID order by FECHORAIN asc ) as FechaPrimerCita,
			Ingreso = (select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 5 and IESTADOIN IN ('','P','B')  ),
			[dbo].[RiskFactorAlert](A.IPCODPACI,'',1) AS IconoRiesgos, [dbo].[RiskFactorAlert](A.IPCODPACI,(select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 5 and IESTADOIN IN ('','P','B')),2) AS IconoEscalas
		from HCRADORDEN A 
			INNER JOIN HCORDPRON ORD with(nolock) ON A.IDHCORDPRON = ORD.AUTO  AND A.ESTADO = @Estado AND A.CITABRAQUIASIGNADA = 1
			INNER JOIN INCUPSIPS  E with(nolock) ON A.CODSERIPS = E.CODSERIPS
			INNER JOIN INPACIENT B ON A.IPCODPACI = B.IPCODPACI 
			INNER JOIN ADCENATEN CE with(nolock) ON CE.CODCENATE = A.CODCENATEULTIMO 
			INNER JOIN INPROFSAL   F with(nolock) ON A.CODPROSAL  = F.CODPROSAL
			INNER JOIN INDIAGNOS  D with(nolock) ON A.CODDIAGNO = D.CODDIAGNO
			INNER JOIN INESPECIA ESP with(nolock) ON ESP.CODESPECI  = A.CODESPECI
			LEFT JOIN contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
			LEFT JOIN contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId
		WHERE E.SERIPSDASH = 13 AND A.CODCENATEULTIMO IN (SELECT Value FROM dbo.SplitString(@CentroAtencion))



		end

END
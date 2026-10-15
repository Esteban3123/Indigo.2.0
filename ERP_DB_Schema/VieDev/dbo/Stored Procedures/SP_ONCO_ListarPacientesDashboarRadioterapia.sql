-- =============================================
-- Author:		Rafael Patiño
-- Create date: 12/02/2020
-- Description:	Listar pacientes en las pestañas del Dashboard oncologico
-- =============================================
CREATE PROCEDURE [dbo].[SP_ONCO_ListarPacientesDashboarRadioterapia] 
 @CentroAtencion varchar(500),
 @TipoServicio int,
 @Estado int
AS
BEGIN
		select 
			A.ID,ESQUE.ID AS 'IDEsquema',
			case when  FECHAPLANEA is Null  then 'Sin Programar Planeación' Else 'Planeación programada' End as 'Planeacion',isnull(NOREFIEREFECHAPLANEA, 0) as NOREFIEREFECHAPLANEA,
			case when  FECHACONTORNEO is Null  then 'Sin Programar Contorneo' Else 'Contorneo programado' End as 'Contorneo',isnull(NOREFIEREFECHACONTOR, 0) as NOREFIEREFECHACONTOR,
			A.CODPROSAL,A.CODDIAGNO,A.FECHAORDEN, 
			rtrim(F.CODPROSAL) as CODPROSAL,
			rtrim(A.CODSERIPS) as CODSERIPS,
			Rtrim(A.IPCODPACI) As Identificacion, 
			Rtrim(IPNOMCOMP) As NombrePaciente,
			Rtrim(E.CODSERIPS) +' - '+ Rtrim(DESSERIPS) + '. ' + isnull(CD.name,'') As Procedimiento,
			Rtrim(CE.NOMCENATE) as CentroAtencion,
			Rtrim(F.NOMMEDICO) as Profesional,
			Rtrim(D.CODDIAGNO) + ' - ' + Rtrim(D.NOMDIAGNO ) as Diagnostico,
			Rtrim(D.CODDIAGNO) as CodigoDiagnostico,
			Rtrim(ESP.DESESPECI) as Especialidad,
			Rtrim(A.CODCENATEULTIMO) as 'CODCENATE',Rtrim(F.CODPROSAL) as 'CODPROSAL',Rtrim(ESP.CODESPECI) as 'CODESPECI',
			A.IDDESCRIPCIONRELACIONADA,A.FECHAPROSIMULA, A.FECHAPLANEA, A.FECHACONTORNEO,
			CD.Code + ' - ' + CD.Name as DescripcionRelacionada,
			[dbo].[Edad](B.IPFECNACI,[Common].[GETDATE]()) as Edad,
			ORD.MANEXTPRO as Extramural,rtrim(ORD.NUMINGRES) as NUMINGRES, rtrim(ORD.UFUCODIGO) as UFUCODIGO, CASE ORD.MANEXTPRO when 1 then 'Ambulatoria' else 'Hospitalario' END as OrigenOrden,
			'Autorización' AS Autorizacion,
			ORD.AUTO EntityId,
			Ingreso = (select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 4 and IESTADOIN IN ('','P','B')  ),
			Concat('Sesión: ', (select COUNT(*) from AGASICITA  where IPCODPACI = A.IPCODPACI AND IDHCRADESQUEMAS = ESQUE.ID AND TIPTRATAMIENTO = 2 AND CODESTCIT IN (1)), '/',  ESQUE.NUMSESION ) as 'Sesion'
			,[dbo].[RiskFactorAlert](A.IPCODPACI,'',1) AS IconoRiesgos, [dbo].[RiskFactorAlert](A.IPCODPACI,(select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 4 and IESTADOIN IN ('','P','B')  ),2) AS IconoEscalas
		from HCRADORDEN A 
		    left JOIN HCRADESQUEMAS ESQUE with(nolock) ON A.ID = ESQUE.IDHCRADORDEN 
			INNER JOIN HCORDPRON ORD with(nolock) ON A.IDHCORDPRON = ORD.AUTO  AND A.ESTADO = @Estado 
			INNER JOIN INCUPSIPS  E with(nolock) ON A.CODSERIPS = E.CODSERIPS
			INNER JOIN INPACIENT B ON A.IPCODPACI = B.IPCODPACI 
			INNER JOIN ADCENATEN CE with(nolock) ON CE.CODCENATE = A.CODCENATEULTIMO
			INNER JOIN INPROFSAL   F with(nolock) ON A.CODPROSAL  = F.CODPROSAL
			INNER JOIN INDIAGNOS  D with(nolock) ON A.CODDIAGNO = D.CODDIAGNO
			INNER JOIN INESPECIA ESP with(nolock) ON ESP.CODESPECI  = A.CODESPECI
			LEFT JOIN contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
			LEFT JOIN contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId
		WHERE E.SERIPSDASH = @TipoServicio AND A.CODCENATEULTIMO IN (SELECT Value FROM dbo.SplitString(@CentroAtencion))
END
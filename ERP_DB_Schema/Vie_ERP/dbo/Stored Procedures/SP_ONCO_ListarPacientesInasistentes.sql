/*29/09/2020 - Rafael Patiño - SP para listar citas inasistentes de tratamientos activos */
CREATE PROCEDURE [dbo].[SP_ONCO_ListarPacientesInasistentes]
(
@CentroAtencion varchar(2000),
@TipoTratamiento int
)
AS
BEGIN
	SET NOCOUNT ON;

	

	IF @TipoTratamiento = 1 BEGIN 
		
			SELECT
			Orden.ID as IDORDEN,
			C.CODAUTONU as IdCita,
			rtrim(P.IPCODPACI) as Identificacion,
			(select top 1 NUMINGRES from ADINGRESO where ipcodpaci = Orden.IPCODPACI AND IESTADOIN IN(' ','P','B') AND TRATAESPECIA = 3 order by IFECHAING desc) as Ingreso,
			rtrim(P.IPNOMCOMP) as NombrePaciente,
			[dbo].[Edad](IPFECNACI, [Common].[GETDATE]()) as Edad,
			Scheme.Code + ' - ' + Scheme.Description as NombreEsquema, 
			rtrim(Diag.CODDIAGNO) + ' - ' + rtrim(Diag.NOMDIAGNO) as Diagnostico,
			rtrim(Cent.CODCENATE) + ' - ' + rtrim(Cent.NOMCENATE) as CentroAtencion,
			H.Code + ' - ' + H.Name as Entidad,
			'Ciclo: ' + convert(varchar(20),DiaCiclo.CICLO) + ' día: ' + convert(varchar(20),DiaCiclo.DIA)  as Dia,
			C.FECHORAIN as FechaCita,
			(select top 1 CODTIPPAC from ADINGRESO where ipcodpaci = Orden.IPCODPACI AND IESTADOIN IN(' ','P','B') AND TRATAESPECIA = 3 order by IFECHAING desc)as CODTIPPAC,
			Rtrim(Cent.CODCENATE) AS 'Codigo Centro Atencion',
			'' as ObservacionVisado, [dbo].[RiskFactorAlert](C.IPCODPACI,'',1) AS IconoRiesgos, [dbo].[RiskFactorAlert](C.IPCODPACI,(select top 1 NUMINGRES from ADINGRESO where ipcodpaci = Orden.IPCODPACI AND IESTADOIN IN(' ','P','B') AND TRATAESPECIA = 3 order by IFECHAING desc),2) AS IconoEscalas
		FROM AGASICITA  C with (nolock)  inner join 
			(
						--select  
						--	(select  top 1 C.CODAUTONU from ehr.HCORDCICLOSD D with (nolock) inner join dbo.AGASICITA C with (nolock) on C.IDHCORDCICLOSD = D.ID where D.IDHCORDQUIMIO = ORD.ID order by C.FECHORAIN desc) as IdUltimaCitaTratamiento,
						--	ORD.ID as IDordenQumio, ORD.IPCODPACI 
						--from ehr.HCORDQUIMIO ORD with (nolock)
						--where ORD.ESTADO IN (1,2) AND ORD.ORDENCONCITA = 1
				select  
					(select  top 1 C.CODAUTONU from dbo.AGASICITA C with (nolock) where C.IDHCORDCICLOSD = D.ID AND C.CODESTCIT IN ('0','2','3','4','5') AND D.IDHCORDQUIMIO = ORD.ID order by C.CODAUTONU desc) as IdUltimaCitaTratamiento,
					ORD.ID as IDordenQumio, ORD.IPCODPACI 
					from ehr.HCORDQUIMIO ORD with (nolock) inner join 
					ehr.HCORDCICLOSD D with (nolock) on ORD.ID = D.IDHCORDQUIMIO
					where ORD.ESTADO IN (1,2) AND ORD.ORDENCONCITA = 1 
			 ) X on X.IdUltimaCitaTratamiento = C.CODAUTONU AND C.CODCENATE  in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) inner join
			 EHR.HCORDQUIMIO Orden with (nolock) on X.IDordenQumio = orden.ID inner join 
			 INPACIENT P with (nolock) on P.IPCODPACI = Orden.IPCODPACI inner join
			 EHR.Schemes Scheme with (nolock) on Scheme.Id = orden.SchemesId inner join
			 INDIAGNOS Diag with (nolock) on Diag.CODDIAGNO = Orden.CODDIAGNO inner join
			 ADCENATEN Cent with (nolock) on cent.CODCENATE = C.CODCENATE inner join 
			 Contract.HealthAdministrator  H with (nolock) on H.id = P.GENCONENTITY inner join 
			 EHR.HCORDCICLOSD DiaCiclo with (nolock) on DiaCiclo.ID = C.IDHCORDCICLOSD 
			 WHERE C.CODESTCIT IN ('0','2','3','4','5') AND C.FECHORAIN  >= DATEADD(month,-12, Common.GETDATE())

	END IF @TipoTratamiento = 2 BEGIN
	   
	   SELECT
	        Orden.ID as IDORDEN,
			C.CODAUTONU as IdCita,
			Esquema.ID as IDEsquemaRadio,
			rtrim(P.IPCODPACI) as Identificacion,
			(select top 1 NUMINGRES from ADINGRESO where ipcodpaci = Orden.IPCODPACI AND IESTADOIN IN(' ','P','B') AND TRATAESPECIA = 4 order by IFECHAING desc) as Ingreso,
			rtrim(P.IPNOMCOMP) as NombrePaciente,
			[dbo].[Edad](IPFECNACI, [Common].[GETDATE]()) as Edad,
			rtrim(Diag.CODDIAGNO) + ' - ' + rtrim(Diag.NOMDIAGNO) as Diagnostico,
			rtrim(Cent.CODCENATE) + ' - ' + rtrim(Cent.NOMCENATE) as CentroAtencion,
			H.Code + ' - ' + H.Name as Entidad,
			C.FECHORAIN as FechaCita,
			rtrim(Cups.CODSERIPS) + ' - ' + rtrim(Cups.DESSERIPS) as CUPS,
			(select top 1 CODTIPPAC from ADINGRESO where ipcodpaci = Orden.IPCODPACI AND IESTADOIN IN(' ','P','B') AND TRATAESPECIA = 4 order by IFECHAING desc)as CODTIPPAC,
			Rtrim(Cent.CODCENATE) AS 'Codigo Centro Atencion',
			'' as ObservacionVisado, [dbo].[RiskFactorAlert](C.IPCODPACI,'',1) AS IconoRiesgos, [dbo].[RiskFactorAlert](C.IPCODPACI,(select top 1 NUMINGRES from ADINGRESO where ipcodpaci = Orden.IPCODPACI AND IESTADOIN IN(' ','P','B') AND TRATAESPECIA = 4 order by IFECHAING desc),2) AS IconoEscalas
		FROM AGASICITA  C with (nolock)  inner join 
			(
						select  
							(select  top 1 C.CODAUTONU from HCRADESQUEMAS E with (nolock) inner join dbo.AGASICITA C with (nolock) on C.IDHCRADESQUEMAS = E.ID where C.IDHCRADESQUEMAS = ESQ.ID order by C.FECHORAIN desc) as IdUltimaCitaTratamiento,
							ORD.ID as IDOrdenRadioterapia,
							ESQ.ID as IDEsquema
							from HCRADORDEN ORD with (nolock) INNER JOIN HCRADESQUEMAS ESQ with (nolock) on ORD.ID = ESQ.IDHCRADORDEN 
							--inner join INCUPSIPS Cups with (nolock) on Cups.CODSERIPS = ORD.CODSERIPS and Cups.SERIPSDASH = 6
						   where ORD.ESTADO NOT IN (5,6,7)
			 ) X on X.IdUltimaCitaTratamiento = C.CODAUTONU inner join
			 HCRADORDEN Orden with (nolock) on X.IDOrdenRadioterapia = orden.ID inner join 
			 HCRADESQUEMAS Esquema with (nolock) on X.IDEsquema = Esquema.ID inner join 
			 INCUPSIPS Cups with (nolock) on Cups.CODSERIPS = Orden.CODSERIPS AND Cups.SERIPSDASH = 6 inner join  
			 INPACIENT P with (nolock) on P.IPCODPACI = Orden.IPCODPACI inner join
		     INDIAGNOS Diag with (nolock) on Diag.CODDIAGNO = Orden.CODDIAGNO inner join
			 ADCENATEN Cent with (nolock) on cent.CODCENATE = C.CODCENATE inner join 
			 Contract.HealthAdministrator  H with (nolock) on H.id = P.GENCONENTITY 
			 WHERE C.CODESTCIT IN ('0','2','3','4','5') AND C.FECHORAIN < [Common].[GETDATE]() AND C.CODCENATE  in (SELECT Value FROM dbo.splitstring(@CentroAtencion))

	END IF @TipoTratamiento = 3 BEGIN
	   
	   SELECT 
	        Orden.ID as IDORDEN,
			C.CODAUTONU as IdCita,
			rtrim(P.IPCODPACI) as Identificacion,
			(select top 1 NUMINGRES from ADINGRESO where ipcodpaci = Orden.IPCODPACI AND IESTADOIN IN(' ','P','B') AND TRATAESPECIA = 5 order by IFECHAING desc) as Ingreso,
			rtrim(P.IPNOMCOMP) as NombrePaciente,
			[dbo].[Edad](IPFECNACI, [Common].[GETDATE]()) as Edad,
			rtrim(Diag.CODDIAGNO) + ' - ' + rtrim(Diag.NOMDIAGNO) as Diagnostico,
			rtrim(Cent.CODCENATE) + ' - ' + rtrim(Cent.NOMCENATE) as CentroAtencion,
			H.Code + ' - ' + H.Name as Entidad,
			C.FECHORAIN as FechaCita,
			rtrim(Cups.CODSERIPS) + ' - ' + rtrim(Cups.DESSERIPS) as CUPS,
			(select top 1 CODTIPPAC from ADINGRESO where ipcodpaci = Orden.IPCODPACI AND IESTADOIN IN(' ','P','B') AND TRATAESPECIA = 5 order by IFECHAING desc)as CODTIPPAC,
			Rtrim(Cent.CODCENATE) AS 'Codigo Centro Atencion',
			'' as ObservacionVisado, [dbo].[RiskFactorAlert](C.IPCODPACI,'',1) AS IconoRiesgos, [dbo].[RiskFactorAlert](C.IPCODPACI,(select top 1 NUMINGRES from ADINGRESO where ipcodpaci = Orden.IPCODPACI AND IESTADOIN IN(' ','P','B') AND TRATAESPECIA = 5 order by IFECHAING desc),2) AS IconoEscalas
		FROM AGASICITA  C inner join 
			(
						select  
							(select  top 1 C.CODAUTONU from HCRADORDEN  O with (nolock) inner join dbo.AGASICITA C with (nolock) on C.IDHCRADORDEN = O.ID where C.IDHCRADORDEN = ORD.ID order by C.FECHORAIN desc) as IdUltimaCitaTratamiento,
							ORD.ID as IDOrdenBraquiterapia, ORD.IPCODPACI 
							from HCRADORDEN ORD with (nolock)
							--inner join INCUPSIPS Cups with (nolock) on Cups.CODSERIPS = ORD.CODSERIPS and Cups.SERIPSDASH = 13
						   where ORD.ESTADO  NOT IN (5,6,7) 
			 ) X on X.IdUltimaCitaTratamiento = C.CODAUTONU inner join
			 HCRADORDEN Orden with (nolock) on X.IDOrdenBraquiterapia = orden.ID inner join 
			 INCUPSIPS Cups with (nolock) on Cups.CODSERIPS = Orden.CODSERIPS AND Cups.SERIPSDASH = 13 inner join  
			 INPACIENT P with (nolock) on P.IPCODPACI = Orden.IPCODPACI inner join
		     INDIAGNOS Diag with (nolock) on Diag.CODDIAGNO = Orden.CODDIAGNO inner join
			 ADCENATEN Cent with (nolock) on cent.CODCENATE = C.CODCENATE inner join 
			 Contract.HealthAdministrator  H with (nolock) on H.id = P.GENCONENTITY 
			 WHERE C.CODESTCIT IN ('0','2','3','4','5') AND C.FECHORAIN < [Common].[GETDATE]() AND C.CODCENATE  in (SELECT Value FROM dbo.splitstring(@CentroAtencion))
	END

     
end
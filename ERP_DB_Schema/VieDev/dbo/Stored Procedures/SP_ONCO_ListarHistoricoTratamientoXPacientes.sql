-- =============================================
-- Author:		Rafael Patiño
-- Create date: 12/02/2020
-- Description:	Listar pacientes en las pestañas del Dashboard oncologico
-- =============================================
CREATE PROCEDURE [dbo].[SP_ONCO_ListarHistoricoTratamientoXPacientes] 
  @TipoServicio int,
  @Paciente VARCHAR(25)
AS
BEGIN
			
			declare @FiltoIngreso_TrataEspecial as integer
			if @TipoServicio = 5 begin --quimio
				set @FiltoIngreso_TrataEspecial = 3
			end if @TipoServicio = 6 begin --radio
				set @FiltoIngreso_TrataEspecial = 4
			end if @TipoServicio = 13 begin --braqui
			    set @FiltoIngreso_TrataEspecial = 5
			end

			select 
			A.ID,
			A.CODPROSAL,A.CODDIAGNO,A.CODCENATE,A.FECHAORDEN,A.CODCENATE, 
			rtrim(F.CODPROSAL) as CODPROSAL,
			rtrim(A.CODSERIPS) as CODSERIPS,
			Rtrim(A.IPCODPACI) As Identificacion, A.FECHAORDEN,
			Rtrim(IPNOMCOMP) As NombrePaciente,
			Rtrim(E.CODSERIPS) +' - '+ Rtrim(DESSERIPS) + '. ' + isnull(CD.name,'') As Procedimiento,
			Rtrim(CE.NOMCENATE ) as CentroAtencion,
			Rtrim(F.NOMMEDICO) as Profesional,
			Rtrim(D.CODDIAGNO) + ' - ' + Rtrim(D.NOMDIAGNO ) as Diagnostico,
			Rtrim(D.CODDIAGNO) as CodigoDiagnostico,
			Rtrim(ESP.DESESPECI) as Especialidad,
			A.IDDESCRIPCIONRELACIONADA,
			CD.Code + ' - ' + CD.Name as DescripcionRelacionada,
			[dbo].[Edad](B.IPFECNACI,[Common].[GETDATE]()) as Edad,
			ORD.MANEXTPRO as Extramural,ORD.NUMINGRES,CASE ORD.MANEXTPRO when 1 then 'Ambulatoria' else 'Hospitalario' END as OrigenOrden,
			Ingreso = (select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = @FiltoIngreso_TrataEspecial and IESTADOIN IN ('','P')  )
		from HCRADORDEN A 
			INNER JOIN HCORDPRON ORD with(nolock) ON A.IDHCORDPRON = ORD.AUTO  
			INNER JOIN INCUPSIPS  E with(nolock) ON A.CODSERIPS = E.CODSERIPS
			INNER JOIN INPACIENT B ON A.IPCODPACI = B.IPCODPACI 
			INNER JOIN ADCENATEN CE with(nolock) ON CE.CODCENATE = A.CODCENATE
			INNER JOIN INPROFSAL   F with(nolock) ON A.CODPROSAL  = F.CODPROSAL
			INNER JOIN INDIAGNOS  D with(nolock) ON A.CODDIAGNO = D.CODDIAGNO
			INNER JOIN INESPECIA ESP with(nolock) ON ESP.CODESPECI  = A.CODESPECI
			LEFT JOIN contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
			LEFT JOIN contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId
		WHERE A.IPCODPACI = @Paciente and E.SERIPSDASH = @TipoServicio

END
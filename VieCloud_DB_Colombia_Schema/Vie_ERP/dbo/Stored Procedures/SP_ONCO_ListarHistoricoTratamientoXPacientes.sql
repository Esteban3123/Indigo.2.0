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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento del módulo oncológico que lista el historial completo de tratamientos (quimioterapia, radioterapia o braquiterapia) de un paciente específico en el Dashboard Oncológico. Recibe como parámetros el tipo de servicio oncológico (5=quimio, 6=radio, 13=braqui) y la cédula o identificación del paciente, y devuelve cada orden de tratamiento registrada en HCRADORDEN junto con datos del paciente (INPACIENT), el profesional tratante (INPROFSAL), el centro de atención (ADCENATEN), el diagnóstico CIE-10 (INDIAGNOS), la especialidad (INESPECIA), el procedimiento CUPS con su descripción (INCUPSIPS), y la descripción de contrato/concepto de facturación asociada (ContractDescriptions/CUPSEntityContractDescriptions). Además, calcula la edad del paciente y determina si existe un ingreso activo de tratamiento especial (hospitalario o ambulatorio) vigente en ADINGRESO según el tipo de servicio solicitado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarHistoricoTratamientoXPacientes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarHistoricoTratamientoXPacientes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el histórico de órdenes de tratamientos especiales (quimioterapia, radioterapia o braquiterapia) de un paciente para el dashboard oncológico, incluyendo datos clínicos, procedimiento, profesional y eventual ingreso asociado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarHistoricoTratamientoXPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en INPACIENT y tener órdenes en HCRADORDEN.; El tipo de servicio debe corresponder a un valor del dashboard oncológico (5=quimio, 6=radio, 13=braqui) para mapear correctamente el tipo de ingreso (3, 4, 5).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarHistoricoTratamientoXPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran ingresos en estado vacío o ''P'' como vigentes para asociar a la orden.; El mapeo TipoServicio→TrataEspecial es fijo: 5→3, 6→4, 13→5; otros valores dejan el filtro de ingreso sin asignar.; La edad se calcula con la función dbo.Edad sobre la fecha actual del sistema (Common.GETDATE).; Solo se listan órdenes cuyo CUPS está habilitado para el dashboard (SERIPSDASH coincide con el tipo de servicio).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarHistoricoTratamientoXPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente oncológico; quimioterapia; radioterapia; braquiterapia; orden de tratamiento; diagnóstico; especialidad médica; procedimiento CUPS; ingreso hospitalario/ambulatorio; centro de atención; tratamiento especial; dashboard oncológico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarHistoricoTratamientoXPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCRADORDEN: Devuelve las órdenes del paciente (A.IPCODPACI=@Paciente) cuyo CUPS está marcado para el dashboard (E.SERIPSDASH=@TipoServicio), enriquecidas con paciente, profesional, diagnóstico, especialidad, centro y descripción contractual.; [RETURN_RESULT] ADINGRESO: Para cada orden retorna el primer NUMINGRES del paciente cuyo TRATAESPECIA coincide con el mapeo del tipo de servicio y cuyo IESTADOIN está vacío o en ''P'' (pendiente/activo).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarHistoricoTratamientoXPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @TipoServicio = 5 (quimioterapia) → Filtra ingresos con TRATAESPECIA = 3; si @TipoServicio = 6 (radioterapia) → Filtra ingresos con TRATAESPECIA = 4; si @TipoServicio = 13 (braquiterapia) → Filtra ingresos con TRATAESPECIA = 5; si ORD.MANEXTPRO = 1 → Marca el origen de la orden como ''Ambulatoria'' else Marca el origen como ''Hospitalario''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarHistoricoTratamientoXPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Edad; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarHistoricoTratamientoXPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCRADORDEN; dbo.HCORDPRON; dbo.INCUPSIPS; dbo.INPACIENT; dbo.ADCENATEN; dbo.INPROFSAL; dbo.INDIAGNOS; dbo.INESPECIA; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarHistoricoTratamientoXPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarHistoricoTratamientoXPacientes';
-- GO

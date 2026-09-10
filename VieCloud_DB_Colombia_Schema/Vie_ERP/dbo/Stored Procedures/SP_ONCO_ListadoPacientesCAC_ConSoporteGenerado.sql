

-- =============================================
-- Author:		Rafael Patiño
-- Create date: 29-12-2020
-- Description:	SP carga los paciente que ya tienen soporte cuentas de alto costo CAC generado
-- =============================================

CREATE PROCEDURE [dbo].[SP_ONCO_ListadoPacientesCAC_ConSoporteGenerado]
(
	@IdEntidad integer,
	@IdEjecuciones varchar(500)
)
AS
BEGIN
	SET NOCOUNT ON;
	 
	  -- QX programados
       Select   A.ID ,
				dbo.TipoDocumento(P.IPTIPODOC) as TipoIdentificacion,  				
				rtrim(ltrim(P.IPCODPACI)) as Identificacion,
				P.IPNOMCOMP as NombrePaciente,
				E.Code + ' - ' + E.Name as Entidad,
				[dbo].[Edad](convert(date,P.IPFECNACI),convert(date,getdate())) as Edad,
				A.[7] as FechaNacimiento
		FROM HCONCOPREG A with(nolock)
		    INNER JOIN INPACIENT P with(nolock) ON A.[6] = P.IPCODPACI
			INNER JOIN Contract.HealthAdministrator as E on E.Id = A.IDEntidadVIE  
		WHERE A.IDEntidadVIE = @IdEntidad 
						and P.IPCODPACI in (
											select distinct d.IPCODPACI from HCSOPORTECAC c 
													inner join HCSOPORTECACPACIENTE d on c.ID = d.IDHCSOPORTECAC  
													where c.ID in ( SELECT Value FROM dbo.SplitString(@IdEjecuciones)) and d.COMPLETO = 1) --tomamos los paciente de la ejecuciones que seleccione el usuario desde front
	
END

--------------------------------

/****** Object:  StoredProcedure [dbo].[SP_ONCO_ListadoPacientesCAC_GenerarSoportePorDocuemento]    Script Date: 20/12/2021 12:03:27 p. m. ******/
SET ANSI_NULLS ON
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes oncológicos de Cuenta de Alto Costo (CAC) que ya tienen soporte documental completamente generado. Dado un identificador de entidad (EPS/aseguradora) y una lista de ejecuciones seleccionadas por el usuario, cruza el registro oncológico del paciente (HCONCOPREG) con sus datos demográficos (INPACIENT) y la entidad pagadora (HealthAdministrator), filtrando únicamente aquellos pacientes cuya documentación CAC esté marcada como completa en HCSOPORTECACPACIENTE. Retorna por cada paciente su tipo y número de identificación, nombre completo, entidad de salud, edad calculada y fecha de nacimiento, permitiendo al equipo de oncología o cuentas médicas verificar qué pacientes ya cuentan con soporte CAC listo para radicación o auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListadoPacientesCAC_ConSoporteGenerado';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListadoPacientesCAC_ConSoporteGenerado';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes oncológicos de una entidad cuyo soporte para Cuenta de Alto Costo (CAC) ya fue generado completamente en las ejecuciones seleccionadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoPacientesCAC_ConSoporteGenerado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La entidad (administradora) debe existir en Contract.HealthAdministrator.; Los IDs de ejecuciones deben venir como cadena delimitada parseable por dbo.SplitString.; Deben existir registros en HCSOPORTECAC y HCSOPORTECACPACIENTE asociados a las ejecuciones indicadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoPacientesCAC_ConSoporteGenerado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran pacientes con soporte CAC marcado como completo (COMPLETO = 1).; Solo se incluyen pacientes vinculados a la entidad VIE solicitada.; El listado se restringe a las ejecuciones de soporte CAC seleccionadas por el usuario.; Aplica DISTINCT sobre los pacientes para evitar duplicados por múltiples soportes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoPacientesCAC_ConSoporteGenerado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente oncológico; Cuenta de Alto Costo (CAC); Soporte CAC; Entidad / Administradora de salud; Tipo de documento de identificación; Edad del paciente; Ejecución de generación de soporte', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoPacientesCAC_ConSoporteGenerado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Retorna pacientes de HCONCOPREG cuya entidad VIE coincide con la entidad indicada y cuyo documento aparece en HCSOPORTECACPACIENTE con COMPLETO=1 dentro de las ejecuciones HCSOPORTECAC seleccionadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoPacientesCAC_ConSoporteGenerado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si HCSOPORTECACPACIENTE.COMPLETO = 1 y el HCSOPORTECAC pertenece a las ejecuciones indicadas → Se incluye al paciente en el listado else Se excluye del listado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoPacientesCAC_ConSoporteGenerado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SplitString; dbo.TipoDocumento; dbo.Edad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoPacientesCAC_ConSoporteGenerado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCONCOPREG; dbo.INPACIENT; Contract.HealthAdministrator; dbo.HCSOPORTECAC; dbo.HCSOPORTECACPACIENTE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoPacientesCAC_ConSoporteGenerado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListadoPacientesCAC_ConSoporteGenerado';
-- GO



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

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,09-04-2019,>
-- Description:	<Description, Luista los CUPS con descripcion relacionada>
-- =============================================
CREATE PROCEDURE [dbo].[SP_AD_ListarCupsConfiguracionServiciosSuceptibles] 

AS
BEGIN
	
	SET NOCOUNT ON;

	Select  cs.CODCONCEC, Convert(BIT,0) As 'Seleccion',Rtrim(s.CODSERIPS) as 'Codigo',rtrim(s.DESSERIPS) as 'Nombre',
		  Rtrim(B.DESGRUIPS) as 'Nombre Grupo', Rtrim(DESSUBIPS) as 'Nombre SubGrupo',
		   case S.TIPSERIPS when '1' then 'Laboratorios'  when '2' Then 'Patologias' when '3' Then 'Imagenes Diagnosticas' when '4' then 'Procedimeintos no Qx' when '5' then 'Procedimientos Qx'  when '6' then 'Interconsultas' when '7' then 'Ninguno' when '8' then 'Consulta Externa' when '9' Then 'Hemocomponentes' end as 'Tipo Servicio',
		   Descriptions.Name as DescripcionRelacionada,IDDESCRIPCIONRELACIONADA,cs.INAPLICA,cs.SUSCEPTIB,
		   CAST(0 as bit)  Actualizado,
		   Case when exists(Select IDADCONFSER From ADCONFSERD Where IDADCONFSER = CS.CODCONCEC) THEN 'Servicios parametrizados' ELSE 'Servicios sin parametrizar' END AS 'Parametrizado'
	From  ADCONFSER cs
		--INNER JOIN ADCONFSERD F ON cs.CODCONCEC = F.IDADCONFSER
		INNER JOIN INCUPSIPS S  ON  cs.CODSERIPS = S.CODSERIPS 
		INNER JOIN Contract.CUPSEntity C  with(nolock) ON S.CODSERIPS = C.Code
	    LEFT JOIN INCUPSGRU B with(nolock) ON S.CODGRUIPS = B.CODGRUIPS 
	    LEFT JOIN INCUPSSUB E with(nolock) ON S.CODGRUSUB = E.CODGRUSUB
		LEFT join contract.CUPSEntityContractDescriptions CupsDescriptions with(nolock) on  CS.IDDESCRIPCIONRELACIONADA  = CupsDescriptions.id 
        LEFT join contract.ContractDescriptions Descriptions with(nolock) on Descriptions.id = CupsDescriptions.ContractDescriptionId
	Where SIPSESTADO = 1 ORDER BY TIPSERIPS asc

	

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los servicios CUPS configurados como susceptibles de condición especial en el módulo de admisión, combinando la configuración de cada servicio (si aplica o no, si es susceptible) con su información maestra: código, nombre, grupo, subgrupo, tipo de servicio (laboratorio, imagen diagnóstica, procedimiento quirúrgico, consulta externa, interconsulta, entre otros) y la descripción de contrato relacionada. También indica si cada servicio ya tiene parámetros adicionales definidos (''parametrizado'') o no. Se utiliza para administrar y visualizar qué procedimientos o servicios de salud tienen configuraciones especiales dentro del proceso de atención y facturación en admisiones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_ListarCupsConfiguracionServiciosSuceptibles';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_ListarCupsConfiguracionServiciosSuceptibles';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los servicios CUPS configurados como susceptibles, con su descripción contractual relacionada, agrupación, tipo de servicio y estado de parametrización.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarCupsConfiguracionServiciosSuceptibles';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en la tabla de configuración de servicios (ADCONFSER) vinculados al catálogo CUPS (INCUPSIPS) y a la entidad CUPS contractual.; Los servicios CUPS deben tener estado activo (SIPSESTADO = 1) para ser listados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarCupsConfiguracionServiciosSuceptibles';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan servicios CUPS con estado activo (SIPSESTADO = 1).; Cada servicio listado pertenece simultáneamente al catálogo INCUPSIPS y a la entidad CUPS contractual (Contract.CUPSEntity), garantizando consistencia entre el maestro local y el catálogo contractual.; Las columnas ''Seleccion'' y ''Actualizado'' siempre se devuelven como BIT 0 (valor por defecto para uso del cliente).; El resultado siempre se entrega ordenado por tipo de servicio en orden ascendente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarCupsConfiguracionServiciosSuceptibles';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'CUPS (Clasificación Única de Procedimientos en Salud); Servicios susceptibles; Configuración de servicios; Grupo y subgrupo de CUPS; Tipo de servicio (Laboratorios, Patologías, Imágenes Diagnósticas, Procedimientos Qx/no Qx, Interconsultas, Consulta Externa, Hemocomponentes); Descripción contractual relacionada; Parametrización de servicios; Inaplicabilidad y susceptibilidad del servicio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarCupsConfiguracionServiciosSuceptibles';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ADCONFSER: Devuelve el listado de servicios configurados susceptibles solo cuando SIPSESTADO = 1, ordenado ascendentemente por tipo de servicio (TIPSERIPS).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarCupsConfiguracionServiciosSuceptibles';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPSERIPS = ''1''..''9'' → Traduce el código de tipo de servicio a etiqueta de negocio: 1=Laboratorios, 2=Patologías, 3=Imágenes Diagnósticas, 4=Procedimientos no Qx, 5=Procedimientos Qx, 6=Interconsultas, 7=Ninguno, 8=Consulta Externa, 9=Hemocomponentes.; si Existe registro en ADCONFSERD con IDADCONFSER igual al código de configuración (CODCONCEC) → Marca el servicio como ''Servicios parametrizados''. else Marca el servicio como ''Servicios sin parametrizar''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarCupsConfiguracionServiciosSuceptibles';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADCONFSER; dbo.ADCONFSERD; dbo.INCUPSIPS; Contract.CUPSEntity; dbo.INCUPSGRU; dbo.INCUPSSUB; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarCupsConfiguracionServiciosSuceptibles';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarCupsConfiguracionServiciosSuceptibles';
-- GO

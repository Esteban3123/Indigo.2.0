CREATE PROCEDURE [dbo].[SP_AD_ListarFavoritosLiquidacion]
(
@Centro char(10)
)
AS
BEGIN
	SET NOCOUNT ON;
        SELECT 
			CODCENATE,
			RTRIM(A.CODSERIPS) AS Codigo,
			IDDESCRIPCIONRELACIONADA,
			A.ID, 
			A.IDADPARCOEX,
			RTRIM(DESSERIPS) AS Servicio,
			Descriptions.Code + ' - ' + Descriptions.Name as DescripcionRelacionada,
			ARSCODIGO AS AreaServicio,
			Case 
			when IPSMACCOS is null then 'No'
			when IPSMACCOS is not null and IPSMACCOS = 0 then 'No'
			when IPSMACCOS is not null and IPSMACCOS = 1 then 'Si'
			End AS PermiteCambio,			 
			TIPSERIPS AS Tipo, 
			Case TIPSERIPS 
				when 1 then 'Laboratorios'
				when 2 then 'Patologias'
				when 3 then 'Imagenes Diagnosticas'
				when 4 then 'Procedimeintos no Qx'
				when 5 then 'Procedimientos Qx'
				when 6 then 'Interconsultas'
				when 7 then 'Ninguno'
				when 8 then 'Consulta Externa'
			End As TipoServicio
       FROM
		   dbo.ADFAVLIQU A 
		   INNER JOIN  dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS
		   left join contract.CUPSEntityContractDescriptions CupsDescriptions with(nolock) on  CupsDescriptions.Id = IDDESCRIPCIONRELACIONADA
		   left join contract.ContractDescriptions Descriptions with(nolock) on Descriptions.id = CupsDescriptions.ContractDescriptionId
       WHERE
			CODCENATE=@Centro

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los servicios o procedimientos (CUPS/IPS) marcados como favoritos o frecuentes para la liquidación y facturación en un centro de atención específico. Para cada servicio favorito recupera su código CUPS, descripción, área de servicio, tipo de servicio (laboratorio, imagen diagnóstica, procedimiento quirúrgico, interconsulta, etc.) y si permite cambio de valor. Además, integra la descripción del concepto de contrato asociado al servicio, combinando el código y nombre del concepto de cobro definido en el contrato. Se utiliza para agilizar el proceso de liquidación mostrando al facturador los servicios más usados en el centro de atención, evitando búsquedas repetitivas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_ListarFavoritosLiquidacion';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_ListarFavoritosLiquidacion';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los servicios favoritos de liquidación configurados para un centro de atención, enriquecidos con datos del CUPS/IPS, descripción contractual, tipo de servicio y si permite cambio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarFavoritosLiquidacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proporcionarse un centro de atención para filtrar; Existencia de la relación entre el favorito y el catálogo de CUPS/IPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarFavoritosLiquidacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven favoritos cuyo CODCENATE coincide con el centro indicado; Cada favorito debe tener un CUPS/IPS válido en INCUPSIPS (INNER JOIN); La descripción contractual es opcional (LEFT JOIN), no filtra el resultado; PermiteCambio siempre se entrega como ''Si'' o ''No'', nunca nulo; El código y la descripción del servicio se devuelven sin espacios en blanco a la derecha (RTRIM)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarFavoritosLiquidacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de atención; Servicios favoritos de liquidación; CUPS/IPS; Área de servicio; Tipo de servicio (Laboratorios, Patologías, Imágenes Diagnósticas, Procedimientos Qx/no Qx, Interconsultas, Consulta Externa); Descripción contractual del CUPS; Permiso de cambio de servicio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarFavoritosLiquidacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando CODCENATE = @Centro, se retorna el listado de favoritos con su código, servicio, descripción contractual, área, tipo y bandera PermiteCambio derivada de IPSMACCOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarFavoritosLiquidacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPSMACCOS IS NULL o IPSMACCOS = 0 → PermiteCambio = ''No'' else Si IPSMACCOS = 1 → PermiteCambio = ''Si''; si TIPSERIPS según valor (1..8) → Se traduce a etiqueta de tipo de servicio: Laboratorios, Patologias, Imagenes Diagnosticas, Procedimientos no Qx, Procedimientos Qx, Interconsultas, Ninguno, Consulta Externa', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarFavoritosLiquidacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADFAVLIQU; dbo.INCUPSIPS; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarFavoritosLiquidacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarFavoritosLiquidacion';
-- GO

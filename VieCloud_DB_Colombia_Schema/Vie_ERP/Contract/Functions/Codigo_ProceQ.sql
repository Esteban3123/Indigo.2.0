-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [Contract].[Codigo_ProceQ]
(
	@Id int
)
RETURNS varchar (300)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @resultado1 varchar (300)

	-- Add the T-SQL statements to compute the return value here
	SELECT @resultado1 = s.Code 
	FROM Billing.ServiceOrderDetailSurgical oq 
	JOIN Contract.IPSService s on s.Id=oq.IPSServiceId
	WHERE oq.Id = @Id

	-- Return the result of the function
	RETURN (@resultado1)

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que, dado el identificador interno de un detalle quirúrgico de orden de servicio, devuelve el código CUPS del procedimiento quirúrgico asociado. Consulta el detalle quirúrgico de facturación (ServiceOrderDetailSurgical) y lo cruza con el catálogo de servicios de la IPS (IPSService) para obtener el código del procedimiento. Se usa para recuperar el código de procedimiento quirúrgico (CUPS) de un ítem específico de una orden facturada, útil en reportería de cirugías, glosas y conciliación de servicios.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'Codigo_ProceQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'Codigo_ProceQ';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el código del servicio/procedimiento del catálogo de la IPS asociado a un detalle quirúrgico de una orden de facturación.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'Codigo_ProceQ';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un detalle quirúrgico de orden de servicio cuyo identificador coincida con el parámetro de entrada.; El detalle quirúrgico debe estar vinculado a un servicio del catálogo de la IPS.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'Codigo_ProceQ';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El código retornado siempre proviene del catálogo Contract.IPSService asociado al detalle quirúrgico vía IPSServiceId.; Si no existe coincidencia para el identificador, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'Codigo_ProceQ';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'servicio quirúrgico; orden de servicio; catálogo de servicios IPS; código de procedimiento', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'Codigo_ProceQ';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Contract.IPSService: Cuando el detalle quirúrgico identificado se une con el catálogo IPSService por IPSServiceId, retorna el Code del servicio.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'Codigo_ProceQ';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ServiceOrderDetailSurgical; Contract.IPSService', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'Codigo_ProceQ';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'Codigo_ProceQ';
GO

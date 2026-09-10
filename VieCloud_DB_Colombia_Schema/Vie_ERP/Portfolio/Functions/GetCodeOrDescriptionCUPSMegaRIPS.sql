-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [Portfolio].[GetCodeOrDescriptionCUPSMegaRIPS]
(
	@ServiceOrderDetailSurgicalId int,
	@IsCode bit
)
RETURNS varchar (300)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @result varchar (300)

	if @IsCode = 1 begin
		select  @result=s.Code from Billing.ServiceOrderDetailSurgical oq 
		inner join [Contract].IPSService s on s.Id=oq.IPSServiceId
		where oq.Id =@ServiceOrderDetailSurgicalId	
	end
	else begin
		select  @result = s.Name from Billing.ServiceOrderDetailSurgical oq 
		inner join [Contract].IPSService s on s.Id=oq.IPSServiceId
		where oq.Id =@ServiceOrderDetailSurgicalId
	end
	-- Return the result of the function
	RETURN @result
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que, dado el identificador de un detalle de orden quirúrgica en facturación, retorna el código CUPS o el nombre del servicio de salud asociado, según el indicador recibido. Si el parámetro @IsCode es verdadero devuelve el código CUPS del procedimiento quirúrgico; si es falso, devuelve la descripción o nombre del servicio. Para ello cruza el detalle quirúrgico facturado (ServiceOrderDetailSurgical) con el catálogo maestro de servicios de la IPS (IPSService). Se usa principalmente en la generación de archivos RIPS y reportes de facturación que requieren mostrar el código o nombre del procedimiento quirúrgico prestado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'FUNCTION', @level1name = N'GetCodeOrDescriptionCUPSMegaRIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'FUNCTION', @level1name = N'GetCodeOrDescriptionCUPSMegaRIPS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el código CUPS o el nombre del servicio quirúrgico asociado a un detalle de orden quirúrgica, según el indicador booleano recibido, para uso en generación de RIPS y reportes de facturación.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetCodeOrDescriptionCUPSMegaRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Billing.ServiceOrderDetailSurgical con el Id recibido; El registro debe tener un IPSServiceId válido que exista en Contract.IPSService', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetCodeOrDescriptionCUPSMegaRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor retornado siempre proviene del IPSService asociado al detalle quirúrgico vía ServiceOrderDetailSurgical.IPSServiceId; Si no existe coincidencia para el Id recibido, el resultado retornado es NULL (no se asigna valor a @result); El resultado se trunca al tipo varchar(300)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetCodeOrDescriptionCUPSMegaRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Procedimiento quirúrgico; Código CUPS; Catálogo de servicios IPS; Detalle quirúrgico de orden de servicio; Facturación; RIPS', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetCodeOrDescriptionCUPSMegaRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Cuando @IsCode=1 retorna IPSService.Code; en caso contrario retorna IPSService.Name, obtenidos al unir ServiceOrderDetailSurgical con IPSService por IPSServiceId filtrando por el Id recibido', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetCodeOrDescriptionCUPSMegaRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @IsCode = 1 → Retorna IPSService.Code (código CUPS del procedimiento quirúrgico) else Retorna IPSService.Name (descripción/nombre del procedimiento quirúrgico)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetCodeOrDescriptionCUPSMegaRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ServiceOrderDetailSurgical; Contract.IPSService', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetCodeOrDescriptionCUPSMegaRIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'FUNCTION', @level1name=N'GetCodeOrDescriptionCUPSMegaRIPS';
GO

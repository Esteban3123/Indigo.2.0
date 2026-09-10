
-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2018-05-01
-- Description:	Retarifica un listado de servicios
-- =============================================

CREATE Procedure [Billing].[SP_RunChangeRatesServices]
	@RevenueControlDetailId Int,
	@CareGroupId Int,
	@ListHomologationsXml Xml,
	@PatientGenus Int,
	@PatientBirth DateTime,
	@distributionToRetarificXml Xml,
	@retarificServiceOrdenDetailSurgicalXml Xml
AS
Begin
	Set Nocount On;

	Declare @StatusResultOut Bit,
		@MessageResultOut Varchar(Max),
		@DistributionToRetarificOut Xml,
		@ListNewServiceOrderDetailOut Xml

	Exec [Billing].[SP_RunChangeRatesServices_Output]
		@RevenueControlDetailId,
		@CareGroupId,
		0,
		@ListHomologationsXml,
		@PatientGenus,
		@PatientBirth,
		@distributionToRetarificXml,
		@retarificServiceOrdenDetailSurgicalXml,
		@StatusResultOut Output,
		@MessageResultOut Output,
		@DistributionToRetarificOut Output,
		@ListNewServiceOrderDetailOut Output

	Select @StatusResultOut as [StatusResult], @MessageResultOut as [MessageResult], 
		@DistributionToRetarificOut As DistributionToRetarific, @ListNewServiceOrderDetailOut As ListNewServiceOrderDetail
	Return
	
End
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que ejecuta la retarificación de un listado de servicios de facturación, es decir, recalcula las tarifas aplicadas a órdenes de servicios médicos ya registradas. Recibe el identificador del detalle de control de ingresos, el grupo de atención, una lista de homologaciones de servicios en XML, el sexo y la fecha de nacimiento del paciente, y dos estructuras XML con la distribución de servicios a retarificar y el detalle de órdenes quirúrgicas. Internamente delega toda la lógica al procedimiento SP_RunChangeRatesServices_Output y retorna el estado del proceso, un mensaje de resultado, la distribución actualizada y el listado de nuevos detalles de orden de servicio generados tras el cambio de tarifas.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_RunChangeRatesServices';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_RunChangeRatesServices';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que ejecuta la retarificación de un listado de servicios delegando en el procedimiento interno _Output y expone sus salidas como un result-set con estado, mensaje, distribución retarificada y nuevo detalle de orden de servicio.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_RunChangeRatesServices';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el procedimiento Billing.SP_RunChangeRatesServices_Output con la firma esperada (incluyendo los 4 parámetros OUTPUT).; Los XML de homologaciones, distribución a retarificar y detalle quirúrgico deben tener la estructura que espera el procedimiento interno.; Se requieren identificadores válidos de detalle de control de ingresos y grupo de atención, así como datos demográficos del paciente (género y fecha de nacimiento).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_RunChangeRatesServices';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre invoca al procedimiento de salida con el tercer parámetro fijo en 0 (modo/flag predeterminado).; El resultado se entrega como un único result-set con cuatro columnas: StatusResult, MessageResult, DistributionToRetarific y ListNewServiceOrderDetail.; Actúa como wrapper de presentación: no realiza cálculos propios, delega completamente la lógica al procedimiento _Output.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_RunChangeRatesServices';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Retarificación de servicios; Homologaciones; Distribución de servicios; Orden de servicio quirúrgica; Grupo de atención (CareGroup); Control de ingresos (RevenueControl); Género y fecha de nacimiento del paciente', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_RunChangeRatesServices';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (result-set): Tras ejecutar SP_RunChangeRatesServices_Output, retorna un SELECT con StatusResult, MessageResult, DistributionToRetarific y ListNewServiceOrderDetail provenientes de los parámetros OUTPUT.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_RunChangeRatesServices';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.SP_RunChangeRatesServices_Output', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_RunChangeRatesServices';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_RunChangeRatesServices';
-- GO


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
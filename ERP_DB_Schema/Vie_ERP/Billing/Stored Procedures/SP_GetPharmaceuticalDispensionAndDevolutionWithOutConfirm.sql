
-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2018-01-13
-- Description:	SP que devuelve las dispensaciones y devoluciones no confirmadas por un ingreso
-- =============================================

CREATE PROCEDURE [Billing].[SP_GetPharmaceuticalDispensionAndDevolutionWithOutConfirm]
	@admissionNumber varchar(10)
AS
BEGIN
	SET NOCOUNT ON;
	
	select 1 as [TypeDispensing], (
		SELECT STUFF((
			SELECT '-' + Code
			FROM Inventory.PharmaceuticalDispensing with(nolock)
			where AdmissionNumber = @admissionNumber And [Status] = 1
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')
	) as Codes

	union all

	select 2 as [TypeDispensing], (
		SELECT STUFF((
			SELECT '-' + Code
			FROM Inventory.PharmaceuticalDispensingDevolution with(nolock)
			where AdmissionNumber = @admissionNumber And [Status] = 1
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '')
	) as Codes

END
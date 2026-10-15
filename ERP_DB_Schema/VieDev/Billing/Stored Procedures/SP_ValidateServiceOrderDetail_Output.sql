-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-07-16
-- Description:	Procedimiento que se encarga de validar los detalles de la orden de servicio
-- =============================================
CREATE  PROCEDURE [Billing].[SP_ValidateServiceOrderDetail_Output]
	@ServiceOrderDetailXml XML,
	@UserCode VARCHAR(20),
	------------------------------------------------------
	@CodeResult Int OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @errors VARCHAR(MAX)

	DECLARE @TableDetail TABLE
	(
		OperatingUnitId INT,
		PatientCode VARCHAR(20),
		AdmissionNumber VARCHAR(20),
		IdDetail INT, 
		CareGroupId int NOT NULL, 
		ExcludeIds varchar(200),
		HealthAdministratorId INT, 
		IPSServiceId INT, 
		AuthorizationNumber VARCHAR(20),
		IsDelete bit NOT NULL,
		CUPSEntityContractDescriptionId INT,
		IsServiceOrderDetailControlJustify BIT NOT NULL,
		ServiceOrderDetailControlJustification VARCHAR(500)
		
	)
	
	BEGIN TRY
		INSERT INTO @TableDetail
			SELECT	t.x.value('OperatingUnitId[1]','int'),
					t.x.value('PatientCode[1]','varchar(20)'),
					t.x.value('AdmissionNumber[1]','varchar(20)'),
					t.x.value('Id[1]','int'),
					t.x.value('CareGroupId[1]','int'),
					t.x.value('ExcludeIds[1]','varchar(200)' ),
					t.x.value('HealthAdministratorId[1]','int'),
					t.x.value('IPSServiceId[1]','int'),
					t.x.value('AuthorizationNumber[1]','varchar(20)'),
					t.x.value('IsDelete[1]','bit'),
					t.x.value('CUPSEntityContractDescriptionId[1]','int'),
					t.x.value('IsServiceOrderDetailControlJustify[1]','bit'),
					t.x.value('ServiceOrderDetailControlJustification[1]','varchar(500)')
			FROM @ServiceOrderDetailXml.nodes('/ServiceOrderDetail') t(x)

		/***********************************************  VALIDACIONES ***********************************************/
	
		IF EXISTS 
		(
			SELECT 1 
			FROM Billing.SettingsBilling sb WITH (NOLOCK)
			JOIN @TableDetail td ON sb.IdOperatingUnit = td.OperatingUnitId			
			JOIN Billing.ServiceOrderDetail sod ON td.CareGroupId = sod.CareGroupId
				AND td.HealthAdministratorId = sod.HealthAdministratorId
				AND td.IPSServiceId = sod.IPSServiceId
				AND ISNULL(td.CUPSEntityContractDescriptionId, 0) = ISNULL(sod.CUPSEntityContractDescriptionId, 0)
				AND sod.IsDelete = 0
				AND td.IdDetail <> sod.Id
				AND td.AuthorizationNumber = sod.AuthorizationNumber
			JOIN Billing.ServiceOrder so WITH (NOLOCK) ON sod.ServiceOrderId = so.Id
				AND so.PatientCode = td.PatientCode
				AND so.Status <> 3
			JOIN Billing.ServiceOrderDetailControl sodc WITH (NOLOCK) ON sod.Id = sodc.ServiceOrderDetailId
			LEFT JOIN Billing.ServiceOrderDetailControl tsodc WITH (NOLOCK) ON td.IdDetail = tsodc.ServiceOrderDetailId
			LEFT JOIN dbo.ADINGRESO ing WITH (NOLOCK) ON td.AdmissionNumber = ing.NUMINGRES
			WHERE sb.AuthorizationNumberControl = 1  
				AND td.IsDelete = 0 AND td.IsServiceOrderDetailControlJustify = 0 
				AND tsodc.Id IS NULL
				AND ISNULL(ing.TIPOINGRE, 1) = 1
				AND sod.Id not in (select * from STRING_SPLIT(td.ExcludeIds, ','))
		) BEGIN
			SET @errors = STUFF((
				SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + 'CUPS: ' + ce.Code + ' - Autorización: ' + sod.AuthorizationNumber
				FROM Billing.SettingsBilling sb WITH (NOLOCK)
				JOIN @TableDetail td ON sb.IdOperatingUnit = td.OperatingUnitId				
				JOIN Billing.ServiceOrderDetail sod ON td.CareGroupId = sod.CareGroupId
					AND td.HealthAdministratorId = sod.HealthAdministratorId
					AND td.IPSServiceId = sod.IPSServiceId
					AND ISNULL(td.CUPSEntityContractDescriptionId, 0) = ISNULL(sod.CUPSEntityContractDescriptionId, 0)
					AND sod.IsDelete = 0
					AND td.IdDetail <> sod.Id
					AND td.AuthorizationNumber = sod.AuthorizationNumber
				JOIN Billing.ServiceOrder so WITH (NOLOCK) ON sod.ServiceOrderId = so.Id
					AND so.PatientCode = td.PatientCode
					AND so.Status <> 3
				JOIN Billing.ServiceOrderDetailControl sodc WITH (NOLOCK) ON sod.Id = sodc.ServiceOrderDetailId
				JOIN Contract.CUPSEntity ce WITH (NOLOCK) ON sod.CUPSEntityId = ce.Id
				LEFT JOIN Billing.ServiceOrderDetailControl tsodc WITH (NOLOCK) ON td.IdDetail = tsodc.ServiceOrderDetailId
				LEFT JOIN dbo.ADINGRESO ing WITH (NOLOCK) ON td.AdmissionNumber = ing.NUMINGRES
				WHERE sb.AuthorizationNumberControl = 1 
					AND td.IsDelete = 0 AND td.IsServiceOrderDetailControlJustify = 0 
					AND tsodc.Id IS NULL
					AND ISNULL(ing.TIPOINGRE, 1) = 1
					AND sod.Id not in (select * from STRING_SPLIT(td.ExcludeIds, ','))
				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				
			SELECT	@CodeResult = 999, 
					@MessageResult = CONCAT('El número de autorización asociada a los siguientes servicios ya se encuentra registrada en el Control de Autorización de Servicios Detallados: ', CHAR(13) + CHAR(10) + @errors)
			RETURN
		END

		/************************************************* RESULTADO *************************************************/

		SELECT	@CodeResult = 0, 
				@MessageResult = 'Registro validado'
	END TRY
	BEGIN CATCH
		SELECT	@CodeResult = 999,
				@MessageResult = CONCAT('Error validando detalle de la orden de servicio: ', ERROR_MESSAGE(), ' - ', ERROR_LINE())
	END CATCH
END
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valida los ítems de una orden de servicio de facturación antes de guardarlos, verificando que el número de autorización asociado a cada servicio (CUPS) no esté duplicado para el mismo paciente en otra orden activa. Recibe los detalles a validar en formato XML, los desempaqueta y los compara contra los registros existentes en ServiceOrderDetail y ServiceOrderDetailControl, consultando también la configuración de facturación por unidad operativa (SettingsBilling) y el tipo de ingreso del paciente en ADINGRESO. Si detecta autorizaciones repetidas sin justificación previa, retorna un código de error (999) con el listado de los CUPS y números de autorización conflictivos; de lo contrario confirma que el registro es válido (código 0). Se usa en el proceso de facturación para evitar que un mismo número de autorización quede asociado a dos ítems distintos de servicios detallados de un paciente hospitalizado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateServiceOrderDetail_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateServiceOrderDetail_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida que los detalles de una orden de servicio no compartan número de autorización con otros ítems ya registrados en el Control de Autorización de Servicios Detallados, devolviendo código 0 si es válido o 999 con el detalle de los conflictos.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateServiceOrderDetail_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe respetar el esquema /ServiceOrderDetail con los nodos esperados (OperatingUnitId, PatientCode, AdmissionNumber, Id, CareGroupId, ExcludeIds, HealthAdministratorId, IPSServiceId, AuthorizationNumber, IsDelete, CUPSEntityContractDescriptionId, IsServiceOrderDetailControlJustify, ServiceOrderDetailControlJustification).; La unidad operativa del detalle debe existir en Billing.SettingsBilling para que aplique el control de autorización.; ExcludeIds debe ser una lista separada por comas compatible con STRING_SPLIT.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateServiceOrderDetail_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El control de autorización solo se aplica cuando SettingsBilling.AuthorizationNumberControl=1 para la unidad operativa del detalle.; Solo se consideran ingresos cuyo TIPOINGRE sea 1 (o nulo, asumido como 1) — es decir, hospitalización.; Los detalles marcados como IsDelete=1 o con IsServiceOrderDetailControlJustify=1 quedan exentos de la validación de duplicado de autorización.; Los IDs incluidos en ExcludeIds quedan excluidos de la búsqueda de conflictos.; Las órdenes de servicio con Status=3 (canceladas/anuladas) no se consideran en la validación.; El procedimiento nunca modifica datos: solo retorna códigos y mensajes mediante parámetros OUTPUT.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateServiceOrderDetail_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de servicio; Detalle de orden de servicio; Número de autorización; Control de autorización de servicios detallados; Paciente; Ingreso/Admisión hospitalaria; CUPS; Administradora de salud; Unidad operativa; Facturación', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateServiceOrderDetail_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @CodeResult/@MessageResult: Cuando existe otro ServiceOrderDetail (no eliminado, distinto al actual, mismo CareGroup, HealthAdministrator, IPSService, CUPSEntityContractDescription y AuthorizationNumber) ya registrado en ServiceOrderDetailControl para una ServiceOrder del mismo paciente con Status<>3, y el detalle entrante no tiene control propio ni justificación, se devuelve CodeResult=999 con el listado de CUPS y autorizaciones en conflicto.; [RETURN_RESULT] @CodeResult/@MessageResult: Si no se detectan conflictos de autorización, se devuelve CodeResult=0 y MessageResult=''Registro validado''.; [RETURN_RESULT] @CodeResult/@MessageResult: Ante cualquier excepción capturada, devuelve CodeResult=999 y MessageResult con el ERROR_MESSAGE() y ERROR_LINE() prefijados por ''Error validando detalle de la orden de servicio:''.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateServiceOrderDetail_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si SettingsBilling.AuthorizationNumberControl = 1 para la unidad operativa, td.IsDelete=0, td.IsServiceOrderDetailControlJustify=0, no existe ServiceOrderDetailControl para el detalle entrante, ISNULL(ADINGRESO.TIPOINGRE,1)=1 (hospitalización), y existe otro ServiceOrderDetail con la misma autorización fuera de ExcludeIds → Retorna error 999 con el listado de servicios (CUPS + autorización) en conflicto else Retorna código 0 ''Registro validado''', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateServiceOrderDetail_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.SettingsBilling; Billing.ServiceOrderDetail; Billing.ServiceOrder; Billing.ServiceOrderDetailControl; Contract.CUPSEntity; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateServiceOrderDetail_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateServiceOrderDetail_Output';
-- GO

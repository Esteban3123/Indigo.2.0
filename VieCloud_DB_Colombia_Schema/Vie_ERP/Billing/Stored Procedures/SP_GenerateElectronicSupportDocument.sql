
-- ===============================================================================================================
-- Author:	Giovanny Plazas Lozano	
-- Create date: 2022/07/28
-- Description:	Procedimiento para Generar Documentos Soporte, apartir de CxP
-- ==============================================================================================================
CREATE PROCEDURE [Billing].[SP_GenerateElectronicSupportDocument]
	@AccountPayableIds As xml,
	@IdAutorization as int = null
	------------------------------------------------------
AS
SET
  ANSI_NULLS,
  QUOTED_IDENTIFIER,
  CONCAT_NULL_YIELDS_NULL,
  ANSI_WARNINGS,
  ANSI_PADDING
ON;
BEGIN
	SET NOCOUNT ON

	

	DECLARE @MessageAuxTablet  Table(	Id int identity(1,1),
										IdDS INT,
										Code varchar(20),
										Message varchar(max))
/*----------------------------------------------------*/
BEGIN TRY

	IF NOT EXISTS (SELECT 1
					FROM @AccountPayableIds.nodes('/Data') t(x)
					WHERE t.x.value('CodeUser[1]','Varchar(20)') IS NOT NULL)
					BEGIN
						INSERT INTO @MessageAuxTablet VALUES(NULL,'999','No hay Datos para generar')

						SELECT * from @MessageAuxTablet
						RETURN
					END

	IF EXISTS( SELECT 1
				FROM @AccountPayableIds.nodes('/Data/AccountPayableIds') t(x))
				BEGIN 
					EXEC Billing.SP_GenerateElectronicSupportDocumentAC @AccountPayableIds, @IdAutorization
					RETURN
				END
	ELSE IF EXISTS( SELECT 1
				FROM @AccountPayableIds.nodes('/Data/VoucherTransactionIds') t(x))
				BEGIN 
					EXEC Billing.SP_GenerateElectronicSupportDocumentVT @AccountPayableIds, @IdAutorization
					RETURN
				END
	ELSE
				BEGIN
					INSERT INTO @MessageAuxTablet VALUES(NULL,'999','La estructura del XML es erronea')
				END

	
	/*se retorna la tabla de control*/
	SELECT *
	from @MessageAuxTablet
	RETURN
END TRY
BEGIN CATCH
	SELECT 0 as Id ,NULL as IdDS,'999' as Code,CONCAT('Error generando el documento Soporte', ERROR_MESSAGE(),' - Linea: ',ERROR_LINE()) AS [Message]
END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento central para generar Documentos Soporte Electrónicos a partir de cuentas por pagar (CxP). Recibe un XML con los identificadores de los documentos a procesar y un número de autorización opcional; según la estructura del XML, delega la generación al subprocedimiento correspondiente: SP_GenerateElectronicSupportDocumentAC cuando se trata de cuentas por pagar directas (AccountPayableIds), o SP_GenerateElectronicSupportDocumentVT cuando proviene de transacciones de comprobantes de egreso (VoucherTransactionIds). Incluye validación de datos de entrada y manejo de errores, retornando mensajes de resultado o fallo para cada operación de generación de documento soporte electrónico de proveedor.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateElectronicSupportDocument';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateElectronicSupportDocument';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Enrutador que, según la estructura del XML recibido, delega la generación del Documento Soporte Electrónico al subprocedimiento adecuado (cuentas por pagar o transacciones de comprobante).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateElectronicSupportDocument';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe contener al menos un nodo /Data con CodeUser no nulo; de lo contrario se retorna mensaje de error ''999 No hay Datos para generar''.; El XML debe contener nodos /Data/AccountPayableIds o /Data/VoucherTransactionIds; cualquier otra estructura se considera errónea.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateElectronicSupportDocument';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La rama AccountPayableIds tiene prioridad sobre VoucherTransactionIds cuando ambos coexisten.; Cualquier excepción es capturada y devuelta como un único resultset con Code=''999'' (no propaga el error).; Solo se delega a un único subprocedimiento por invocación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateElectronicSupportDocument';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Documento Soporte Electrónico; Cuentas por Pagar (AccountPayable); Comprobante de Egreso / VoucherTransaction; Autorización', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateElectronicSupportDocument';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @MessageAuxTablet: Cuando el XML no tiene CodeUser, inserta fila con Code=''999'' y mensaje ''No hay Datos para generar''.; [INSERT] @MessageAuxTablet: Cuando el XML no contiene ni AccountPayableIds ni VoucherTransactionIds, inserta fila con Code=''999'' y mensaje ''La estructura del XML es erronea''.; [RETURN_RESULT] @MessageAuxTablet: Retorna el contenido de la tabla de control de mensajes al finalizar el flujo sin delegación.; [RETURN_RESULT] (resultset): En CATCH retorna una fila con Code=''999'' y mensaje concatenando ''Error generando el documento Soporte'', ERROR_MESSAGE() y ERROR_LINE().', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateElectronicSupportDocument';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe ningún nodo /Data con CodeUser no nulo en el XML → Inserta mensaje ''999 - No hay Datos para generar'' y retorna else Continúa la evaluación de la estructura; si Existen nodos /Data/AccountPayableIds en el XML → Ejecuta Billing.SP_GenerateElectronicSupportDocumentAC y retorna else Evalúa si existen VoucherTransactionIds; si Existen nodos /Data/VoucherTransactionIds en el XML → Ejecuta Billing.SP_GenerateElectronicSupportDocumentVT y retorna else Inserta mensaje ''999 - La estructura del XML es erronea''', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateElectronicSupportDocument';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.SP_GenerateElectronicSupportDocumentAC; Billing.SP_GenerateElectronicSupportDocumentVT', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateElectronicSupportDocument';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateElectronicSupportDocument';
-- GO

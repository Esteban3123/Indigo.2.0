

-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 13/10/2016
-- Description:	Procedimiento que se encarga de validar el traslado
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_GetProvisionAndDeterioration] 
	@CourtDate as date,
	@Process as int
AS
BEGIN

	--Tabla temporal para registrar las cuentas por cobrar y retornar
	declare @TempTable table(Id int primary key identity, AccountReceivableId int, CodeAccountReceivable varchar(20), 
	InvoiceNumber varchar(20), Balance numeric(18,0), ConfirmDate date, Ages varchar(50), Percentage decimal(5,2), Value numeric(18,0))
	
	Begin try
	
		
		
		select * from @TempTable
		
	end try
	begin catch
		select 999 as CodeMessage, ERROR_MESSAGE() as Message, 0 Id
	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento almacenado del módulo de Cartera que calcula y retorna información de provisión y deterioro de cuentas por cobrar, filtrando por una fecha de corte y un proceso específico. Trabaja sobre la tabla de Cartera (Portfolio) para obtener el saldo, número de factura, días de antigüedad (aging), porcentaje y valor de deterioro de cada cuenta por cobrar. Está diseñado para apoyar el análisis financiero y contable de la cartera, permitiendo identificar el nivel de riesgo o deterioro de las facturas pendientes de cobro según la fecha de corte indicada.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GetProvisionAndDeterioration';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GetProvisionAndDeterioration';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el listado base de cuentas por cobrar con su provisión y deterioro a una fecha de corte; actualmente retorna la tabla temporal vacía (esqueleto sin lógica implementada).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GetProvisionAndDeterioration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requiere una fecha de corte y un identificador de proceso como parámetros de entrada.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GetProvisionAndDeterioration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado siempre es una tabla con la estructura definida (AccountReceivableId, CodeAccountReceivable, InvoiceNumber, Balance, ConfirmDate, Ages, Percentage, Value), aún cuando esté vacía.; Ante cualquier error en ejecución se retorna un resultado con CodeMessage=999 en lugar de propagar la excepción.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GetProvisionAndDeterioration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuentas por cobrar; Provisión; Deterioro; Edades de cartera; Saldo; Factura', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GetProvisionAndDeterioration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @TempTable: Siempre retorna el contenido de la tabla temporal @TempTable (vacía en esta versión) con columnas de cartera: Id, AccountReceivableId, CodeAccountReceivable, InvoiceNumber, Balance, ConfirmDate, Ages, Percentage, Value.; [RETURN_RESULT] (resultset de error): En el bloque CATCH, si ocurre un error retorna una fila con CodeMessage=999, Message=ERROR_MESSAGE() e Id=0.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GetProvisionAndDeterioration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GetProvisionAndDeterioration';
-- GO

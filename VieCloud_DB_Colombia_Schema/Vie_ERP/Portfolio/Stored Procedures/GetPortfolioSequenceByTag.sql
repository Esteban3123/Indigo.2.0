
-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2018-07-19
-- Description:	Liquidación
-- =============================================

CREATE Procedure [Portfolio].[GetPortfolioSequenceByTag]
	@Tag Varchar(5),
	@OperativeUnitId Int,
	@StatusResult Bit Output,
	@Message Varchar(255) Output,
	@IdSequence Int Output
AS
Begin
	Set Nocount On;
	Begin Try

		Declare @PortfolioSequenceId Int,
			@Scope Varchar(2)

		Select @PortfolioSequenceId = ps.Id,
			@Scope = ps.Scope
		From Portfolio.PortfolioSequence ps With(Nolock) Where IdForm = @Tag

		If @PortfolioSequenceId Is Not Null And @PortfolioSequenceId > 0 Begin
			If @Scope = 'O' Begin --El ambito es a nivel de organización				
				Select Top 1 @IdSequence = psd.Id 
				From Portfolio.PortfolioSequenceDetail psd With(Nolock) 
				Where IdSequensePortfolioC = @PortfolioSequenceId
			End
			Else If @Scope = 'OU' Begin --El ambito es a nivel de unidad operativa
				Select Top 1 @IdSequence = psd.Id 
				From Portfolio.PortfolioSequenceDetail psd With(Nolock) 
				Where IdSequensePortfolioC = @PortfolioSequenceId And IdOperatingUnit = @OperativeUnitId
			End
			If @IdSequence Is Null Or @IdSequence <= 0 Begin
				Set @IdSequence = -1
				Set @StatusResult = 0
				Set @Message = 'No se encontró secuencia numérica para el Tag {0} de Cuentas por Cobrar' + @Tag
			End
			Else Begin
				Set @StatusResult = 1
				Set @Message = ''
			End			
			Return
		End
		Else Begin
			Set @StatusResult = 0
			Set @Message = 'No se encontró secuencia numérica para el Tag ' + @Tag + ' de Cuentas por Cobrar'
			Set @IdSequence = -1
		End
	End Try
	Begin Catch		
		Set @StatusResult = 0
		Set @Message = 'No se encontró secuencia numérica para el Tag {0} de Cuentas por Cobrar' + @Tag
		Set @IdSequence = -1
	End Catch	
End
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el identificador de la secuencia numérica (consecutivo) configurada para un tipo de formulario del portafolio de servicios, identificado por una etiqueta corta (Tag). Busca en la configuración de secuencias (PortfolioSequence) si el formulario usa numeración a nivel de organización o de unidad operativa, y luego obtiene el detalle de secuencia correspondiente (PortfolioSequenceDetail) según ese alcance y la unidad operativa indicada. Se usa en el proceso de liquidación y generación de documentos como facturas u órdenes en cuentas por cobrar, garantizando que cada documento reciba el número consecutivo correcto. Devuelve el ID de la secuencia encontrada o un indicador de error si no existe configuración para el Tag solicitado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'GetPortfolioSequenceByTag';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'GetPortfolioSequenceByTag';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Resuelve el identificador de la secuencia numérica de Cuentas por Cobrar asociada a un Tag de formulario, considerando si su ámbito es organizacional o por unidad operativa.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'GetPortfolioSequenceByTag';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Portfolio.PortfolioSequence cuyo IdForm coincida con el Tag recibido.; Si el ámbito (Scope) es ''OU'', se requiere una unidad operativa válida para localizar el detalle correspondiente.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'GetPortfolioSequenceByTag';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'@IdSequence siempre se devuelve con valor -1 cuando @StatusResult=0.; @StatusResult=1 implica que @Message es cadena vacía y @IdSequence es un Id válido (>0).; El ámbito de búsqueda del detalle depende exclusivamente del Scope configurado en PortfolioSequence (''O'' u ''OU'').; Cualquier error en tiempo de ejecución se traduce en resultado fallido sin propagar la excepción.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'GetPortfolioSequenceByTag';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuentas por Cobrar; Secuencia numérica; Tag de formulario; Ámbito organizacional; Unidad operativa; Liquidación; Portafolio de servicios', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'GetPortfolioSequenceByTag';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (output): Cuando existe PortfolioSequence para el Tag y se localiza un PortfolioSequenceDetail según el Scope, retorna @IdSequence con el Id encontrado, @StatusResult=1 y @Message vacío.; [RETURN_RESULT] (output): Cuando no existe PortfolioSequence para el Tag, retorna @IdSequence=-1, @StatusResult=0 y mensaje ''No se encontró secuencia numérica para el Tag <Tag> de Cuentas por Cobrar''.; [RETURN_RESULT] (output): Cuando existe PortfolioSequence pero no se halla detalle (por Scope ''O'' u ''OU''), retorna @IdSequence=-1, @StatusResult=0 y mensaje con plantilla ''{0}'' concatenando el Tag.; [RETURN_RESULT] (output): Ante cualquier excepción capturada, retorna @IdSequence=-1, @StatusResult=0 y mensaje genérico de Tag no encontrado.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'GetPortfolioSequenceByTag';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @PortfolioSequenceId IS NOT NULL AND > 0 → Evalúa el Scope para decidir cómo buscar el detalle de la secuencia. else Marca resultado fallido con @IdSequence=-1 y mensaje de Tag no encontrado.; si @Scope = ''O'' (ámbito organización) → Selecciona el primer PortfolioSequenceDetail filtrando solo por IdSequensePortfolioC = @PortfolioSequenceId.; si @Scope = ''OU'' (ámbito unidad operativa) → Selecciona el primer PortfolioSequenceDetail filtrando por IdSequensePortfolioC y por IdOperatingUnit = @OperativeUnitId.; si @IdSequence IS NULL OR <= 0 tras la búsqueda de detalle → Devuelve fallo con @IdSequence=-1 y mensaje de no hallazgo. else Devuelve éxito con @StatusResult=1.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'GetPortfolioSequenceByTag';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioSequence; Portfolio.PortfolioSequenceDetail', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'GetPortfolioSequenceByTag';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'GetPortfolioSequenceByTag';
-- GO

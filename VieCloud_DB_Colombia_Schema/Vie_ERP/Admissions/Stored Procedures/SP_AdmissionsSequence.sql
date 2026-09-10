-- =============================================
-- Author:      Yefersson Caicedo
-- Create Date: 2023-08-17
-- Description: Obtener la secuencia numérica para admisiones.
-- =============================================
CREATE PROCEDURE [Admissions].[SP_AdmissionsSequence]
(
    -- Add the parameters for the stored procedure here
    @IdForm1642 int = 1642,
	@OperatingUnitId INT
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
    declare @idSequenceDetail int, @pattern varchar(300), @NextS bigint, @Scope varchar(5), @IdSequence int
						, @IdSequenceCommon int, @Prefix varchar(20) = '', @IdAdmissionSequence int, @Sequence varchar(20)

					select  @IdAdmissionSequence= Id,@Scope = Scope from Admissions.AdmissionsSequence With(Nolock) Where IdForm = @IdForm1642
					select @IdSequence = IdSequense, @IdSequenceCommon = IdSequense from Admissions.AdmissionsSequenceDetail where AdmissionsSequenceId =  @IdAdmissionSequence

					if @Scope = 'O' begin --- Secuencia por Prefijo
						Select Top 1 @pattern = cs.Pattern
							, @idSequenceDetail = psd.Id  
						From Admissions.AdmissionsSequenceDetail psd With(Nolock)
						Inner Join Common.Sequense cs With(Nolock) on cs.Id = psd.IdSequense 
						Where psd.AdmissionsSequenceId = @IdAdmissionSequence
					end
					ELSE BEGIN -- Secuencia por Unidad operativa
						SELECT @pattern = cs.Pattern, @idSequenceDetail = psd.Id  
						FROM Admissions.AdmissionsSequenceDetail psd WITH(NOLOCK)
						INNER JOIN Common.Sequense cs WITH(NOLOCK) ON cs.Id = psd.IdSequense 
						WHERE psd.AdmissionsSequenceId = @IdAdmissionSequence AND IdOperatingUnit = @OperatingUnitId
					END
					
					Update Admissions.AdmissionsSequenceDetail Set @NextS = [Next] += 1 where Id = @idSequenceDetail
					Select @Sequence = dbo.GetSequence(@Prefix,@pattern,(@NextS - 1))
					select @Sequence
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera y retorna el siguiente número consecutivo (secuencia) para un proceso de admisión, según el formulario y la unidad operativa indicados. Consulta la configuración de secuencias en AdmissionsSequence para determinar si la numeración aplica de forma global (por prefijo) o específica por unidad operativa, luego obtiene el patrón de formato desde Common.Sequense y actualiza el contador en AdmissionsSequenceDetail incrementando el siguiente número disponible. Finalmente, formatea el consecutivo usando la función dbo.GetSequence y lo devuelve como resultado, garantizando numeración única y ordenada para ingresos, atenciones y registros de admisión de pacientes.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'PROCEDURE', @level1name = N'SP_AdmissionsSequence';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'PROCEDURE', @level1name = N'SP_AdmissionsSequence';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera y devuelve el siguiente número consecutivo formateado para una admisión, eligiendo la secuencia según alcance global o por unidad operativa.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_AdmissionsSequence';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Admissions.AdmissionsSequence cuyo IdForm coincida con el formulario indicado (1642 por defecto).; Debe existir un detalle en Admissions.AdmissionsSequenceDetail asociado al IdAdmissionSequence; si el alcance es por unidad operativa, debe existir el detalle para la IdOperatingUnit indicada.; El detalle debe estar enlazado a un patrón en Common.Sequense vía IdSequense.; Debe existir la función escalar dbo.GetSequence para formatear el consecutivo.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_AdmissionsSequence';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El número devuelto corresponde al valor de [Next] previo al incremento (@NextS - 1), garantizando que el siguiente consecutivo almacenado quede reservado para la próxima invocación.; El prefijo utilizado en la generación siempre se pasa vacío ('''') a dbo.GetSequence; el formato lo determina exclusivamente el Pattern de Common.Sequense.; Cada llamada consume (incrementa) un consecutivo, incluso si el resultado no se utiliza posteriormente.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_AdmissionsSequence';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Admisión; Secuencia/consecutivo de admisión; Unidad operativa; Patrón de numeración; Alcance de secuencia (global vs por unidad operativa)', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_AdmissionsSequence';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Admissions.AdmissionsSequenceDetail: Incrementa en 1 la columna [Next] del detalle seleccionado (Id = @idSequenceDetail) y captura el nuevo valor en @NextS para construir la secuencia.; [RETURN_RESULT] (resultset): Devuelve un único valor escalar @Sequence resultante de dbo.GetSequence(@Prefix, @pattern, @NextS - 1), es decir, el consecutivo previo al incremento formateado con el patrón.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_AdmissionsSequence';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Scope = ''O'' en Admissions.AdmissionsSequence → Selecciona el patrón y el detalle de secuencia únicamente por AdmissionsSequenceId (secuencia por prefijo / global, TOP 1). else Selecciona el patrón y el detalle filtrando además por IdOperatingUnit = @OperatingUnitId (secuencia por unidad operativa).', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_AdmissionsSequence';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.GetSequence', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_AdmissionsSequence';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Admissions.AdmissionsSequence; Admissions.AdmissionsSequenceDetail; Common.Sequense', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_AdmissionsSequence';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_AdmissionsSequence';
-- GO

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
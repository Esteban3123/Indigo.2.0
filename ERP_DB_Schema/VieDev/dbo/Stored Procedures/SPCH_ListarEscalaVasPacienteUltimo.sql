
CREATE PROCEDURE [dbo].[SPCH_ListarEscalaVasPacienteUltimo]
(
@Paciente Varchar(25)
)
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @Consecutivo as int
	SELECT  @Consecutivo = CODCONSEC FROM HCESCVASC WHERE IPCODPACI = @Paciente

	SELECT 
			--CODTIPDOL AS TipoDolor,
			--CODPARCUE AS ParteCuerpo,
			--PTSESTREP AS PuntosReposo,
			--PTSESTMOV AS PuntosMovimiento,
			--POSIMGCUX AS PosicionX,
			--POSIMGCUY AS PosicionY
			CODTIPDOL,
			CODPARCUE,
			PTSESTREP,
			PTSESTMOV,
			POSIMGCUX,
			POSIMGCUY
		FROM
			HCESCVASD
		WHERE
			CODCONSEC =@Consecutivo

end
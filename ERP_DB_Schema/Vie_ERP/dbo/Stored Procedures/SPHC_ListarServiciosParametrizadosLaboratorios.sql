
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_ListarServiciosParametrizadosLaboratorios]
(
@TipoServicio int,
@CentroAtencion Char(10)
)
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select 
		B.CODCENATE as Centro ,RTRIM(A.CODSERIPS) as Codigo ,RTRIM(A.DESSERIPS) as Descripcion, B.LABRECMUE as TiempoMaximoRecoleccion, cast(UNITIEREC as int) AS UnidadRecolectada,
		LABRECMUE2 as ReporteRecolectada,cast(UNITIEREC2 as int) as UnidadReporteRecolectada,LABENTRES as TiempoMaximoResultado,
		cast(UNITIEENT as int) as UnidadResultado,LABENTRES2 as ReporteResultado,cast(UNITIEENT2 as int)  as UnidadReporteResultados , 
		B.ID , B.USUARIOCREACION, B.FECHACREACION, B.USUARIOMODIFICACION, B.FECHAMODIFICACION, 
		Rtrim(U1.CODUSUARI) + ' - ' + Rtrim(U1.NOMUSUARI) AS 'Usuario_Creacion', Rtrim(U2.CODUSUARI) + ' - ' + Rtrim(U2.NOMUSUARI) AS 'Usuario_Modificacion' 
			
	From
		dbo.INCUPSIPS As A 
		Left Outer Join dbo.HCPARALELAB As B On A.CODSERIPS = B.CODSERIPS And B.CODCENATE = @CentroAtencion
		Left Outer Join dbo.SEGusuaru As U1 On B.USUARIOCREACION = U1.CODUSUARI 
		Left Outer Join.dbo.SEGusuaru As U2 On B.USUARIOMODIFICACION = U2.CODUSUARI                                     

	Where 
		A.TIPSERIPS = @TipoServicio

END
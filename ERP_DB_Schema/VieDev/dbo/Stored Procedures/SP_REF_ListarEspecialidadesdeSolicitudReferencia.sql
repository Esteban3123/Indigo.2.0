-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,09-04-2019,>
-- Description:	<Description,Sp que me lista las especialidades de Solicitud de referencia ,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_REF_ListarEspecialidadesdeSolicitudReferencia] 
(
@AUTO as varchar(200)
)

AS
BEGIN
	
	SET NOCOUNT ON;

	Declare @valores nvarchar(MAX)
	SELECT @valores= COALESCE(Rtrim(@valores)+ ' - ', '') + Rtrim(DESESPECI) FROM HCREFESPECIALIDADES a
	 Inner join INESPECIA B ON A.CODESPECI = B.CODESPECI WHERE  IDHCREFCONT = @AUTO
	select @valores as 'Especialidades'

	--exec sp_executesql @valores 

END

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,09-04-2019,>
-- Description:	<Description, Luista los CUPS con descripcion relacionada>
-- =============================================
CREATE PROCEDURE [dbo].[SP_AD_ListarCupsConfiguracionServiciosSuceptibles] 

AS
BEGIN
	
	SET NOCOUNT ON;

	Select  cs.CODCONCEC, Convert(BIT,0) As 'Seleccion',Rtrim(s.CODSERIPS) as 'Codigo',rtrim(s.DESSERIPS) as 'Nombre',
		  Rtrim(B.DESGRUIPS) as 'Nombre Grupo', Rtrim(DESSUBIPS) as 'Nombre SubGrupo',
		   case S.TIPSERIPS when '1' then 'Laboratorios'  when '2' Then 'Patologias' when '3' Then 'Imagenes Diagnosticas' when '4' then 'Procedimeintos no Qx' when '5' then 'Procedimientos Qx'  when '6' then 'Interconsultas' when '7' then 'Ninguno' when '8' then 'Consulta Externa' when '9' Then 'Hemocomponentes' end as 'Tipo Servicio',
		   Descriptions.Name as DescripcionRelacionada,IDDESCRIPCIONRELACIONADA,cs.INAPLICA,cs.SUSCEPTIB,
		   CAST(0 as bit)  Actualizado,
		   Case when exists(Select IDADCONFSER From ADCONFSERD Where IDADCONFSER = CS.CODCONCEC) THEN 'Servicios parametrizados' ELSE 'Servicios sin parametrizar' END AS 'Parametrizado'
	From  ADCONFSER cs
		--INNER JOIN ADCONFSERD F ON cs.CODCONCEC = F.IDADCONFSER
		INNER JOIN INCUPSIPS S  ON  cs.CODSERIPS = S.CODSERIPS 
		INNER JOIN Contract.CUPSEntity C  with(nolock) ON S.CODSERIPS = C.Code
	    LEFT JOIN INCUPSGRU B with(nolock) ON S.CODGRUIPS = B.CODGRUIPS 
	    LEFT JOIN INCUPSSUB E with(nolock) ON S.CODGRUSUB = E.CODGRUSUB
		LEFT join contract.CUPSEntityContractDescriptions CupsDescriptions with(nolock) on  CS.IDDESCRIPCIONRELACIONADA  = CupsDescriptions.id 
        LEFT join contract.ContractDescriptions Descriptions with(nolock) on Descriptions.id = CupsDescriptions.ContractDescriptionId
	Where SIPSESTADO = 1 ORDER BY TIPSERIPS asc

	

END
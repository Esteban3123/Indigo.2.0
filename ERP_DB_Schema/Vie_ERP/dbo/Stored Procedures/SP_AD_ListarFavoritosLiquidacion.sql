CREATE PROCEDURE [dbo].[SP_AD_ListarFavoritosLiquidacion]
(
@Centro char(10)
)
AS
BEGIN
	SET NOCOUNT ON;
        SELECT 
			CODCENATE,
			RTRIM(A.CODSERIPS) AS Codigo,
			IDDESCRIPCIONRELACIONADA,
			A.ID, 
			A.IDADPARCOEX,
			RTRIM(DESSERIPS) AS Servicio,
			Descriptions.Code + ' - ' + Descriptions.Name as DescripcionRelacionada,
			ARSCODIGO AS AreaServicio,
			Case 
			when IPSMACCOS is null then 'No'
			when IPSMACCOS is not null and IPSMACCOS = 0 then 'No'
			when IPSMACCOS is not null and IPSMACCOS = 1 then 'Si'
			End AS PermiteCambio,			 
			TIPSERIPS AS Tipo, 
			Case TIPSERIPS 
				when 1 then 'Laboratorios'
				when 2 then 'Patologias'
				when 3 then 'Imagenes Diagnosticas'
				when 4 then 'Procedimeintos no Qx'
				when 5 then 'Procedimientos Qx'
				when 6 then 'Interconsultas'
				when 7 then 'Ninguno'
				when 8 then 'Consulta Externa'
			End As TipoServicio
       FROM
		   dbo.ADFAVLIQU A 
		   INNER JOIN  dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS
		   left join contract.CUPSEntityContractDescriptions CupsDescriptions with(nolock) on  CupsDescriptions.Id = IDDESCRIPCIONRELACIONADA
		   left join contract.ContractDescriptions Descriptions with(nolock) on Descriptions.id = CupsDescriptions.ContractDescriptionId
       WHERE
			CODCENATE=@Centro

END
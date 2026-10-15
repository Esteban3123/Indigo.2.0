
CREATE PROCEDURE [dbo].[SPHC_ListarProcedimientoTerapiasRealizadas]
(
      -- Add the parameters for the stored procedure here
	@Identificacion as varchar(25),
	@Ingreso as varchar(25)
)
AS
BEGIN

SELECT 
	ROW_NUMBER() OVER(ORDER BY Codigo) AS Id, * 
FROM
(

				SELECT * FROM ( 
					SELECT 	
							'Pendientes a realizar' AS Terapias,
							A.CODSERIPS AS Codigo,
							CONCAT(RTRIM(A.CODSERIPS),'_',(A.IDDESCRIPCIONRELACIONADA)) AS CodigoDescripcion, 
							RTRIM(DESSERIPS) as Descripcion
							,(SUM(A.CANSERIPS) - (SELECT COUNT(*) FROM  dbo.HCPROCTER Z  WHERE Z.IPCODPACI= @Identificacion  AND Z.NUMINGRES=@Ingreso AND Z.CODSERIPS = A.CODSERIPS AND ISNULL(Z.IDDESCRIPCIONRELACIONADA, 0) = ISNULL(A.IDDESCRIPCIONRELACIONADA, 0))) AS cantidadpendiente
							, A.IDDESCRIPCIONRELACIONADA as DescripcionRelacionada, CD.Name as ContractDescriptionName
					
						FROM 
							.dbo.HCORDPRON  A 
							INNER JOIN .dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS
							LEFT JOIN contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA
							LEFT JOIN contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId
						WHERE
							IPCODPACI=@Identificacion
							AND NUMINGRES= @Ingreso  
							AND TIPSERTER= '1'
							AND TIPSERIPS = '4'	
							AND ESTSERIPS <> 5
							AND MANEXTPRO = 0
						GROUP BY 
							B.DESSERIPS, 
							A.CODSERIPS,
							A.IDDESCRIPCIONRELACIONADA,
							CD.name

				) TMP WHERE TMP.cantidadpendiente <> 0

				UNION ALL 
						SELECT 
							'Todas' AS Terapias,
							CE.Code AS Codigo,
							CONCAT(RTRIM(CE.Code),'_',RTRIM(CDD.Id)) AS CodigoDescripcion,
							RTRIM(CE.Description) as Descripcion,
							'' AS cantidadrealizada, CDD.Id as DescripcionRelacionada, CD.Name as ContractDescriptionName
						from contract.CUPSEntity CE
							LEFT JOIN Contract.CUPSEntityContractDescriptions CDD ON CE.Id = CDD.CUPSEntityId
							LEFT JOIN contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId
						WHERE 
							CE.Status = 1 
							AND CE.TherapyProcedure = 1	
							AND CE.ServiceType = 4

		) as TMP
END
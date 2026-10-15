-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_ListarProcedimientoTerapiaOrdenes]
(
    -- Add the parameters for the stored procedure here
	@Identificacion as varchar(25),
	@Ingreso as varchar(25),
	@Centro as varchar(25),
	@Unidadfuncional as varchar(25)
)
AS
BEGIN
		SELECT 
			'Terapias ordenadas' Agrupacion, 
			RTRIM(A.CODSERIPS) as CodigoProcedimiento, 
			RTRIM(B.DESSERIPS) as DescripcionProcedimiento,
			(SELECT COUNT(*) FROM  .dbo.HCPROCTER Z 
			WHERE Z.IPCODPACI=@Identificacion
			AND Z.NUMINGRES=@Ingreso
			AND Z.CODSERIPS = A.CODSERIPS AND ISNULL(Z.IDDESCRIPCIONRELACIONADA, 0) = ISNULL(A.IDDESCRIPCIONRELACIONADA, 0)) AS cantidadrealizada ,
			SUM(A.CANSERIPS) AS Cantidad, CD.Name as ContractDescriptionName, A.IDDESCRIPCIONRELACIONADA AS ContractDescriptionId
			
			FROM 
				.dbo.HCORDPRON A 
				INNER JOIN .dbo.INCUPSIPS B ON A.CODSERIPS = B.CODSERIPS 
				LEFT JOIN contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA
				LEFT JOIN contract.ContractDescriptions CD on CD.Id = CDD.ContractDescriptionId
			WHERE 
				A.IPCODPACI = @Identificacion 
				AND A.NUMINGRES = @Ingreso 
				AND A.CODCENATE = @Centro 
				AND TIPSERTER = '1'  and A.MANEXTPRO = 0
				AND ESTSERIPS <> 5
			GROUP BY 
				B.DESSERIPS, 
				A.CODSERIPS,
				CD.Name,
				A.IDDESCRIPCIONRELACIONADA

UNION ALL 
		SELECT 
		  'Terapias sin orden' Agrupacion, 
		  RTRIM(A.CODSERIPS) as CodigoProcedimiento, 
		  RTRIM(B.DESSERIPS) as DescripcionProcedimiento,
		  (SELECT COUNT(*) FROM  .dbo.HCPROCTER Z 
			WHERE Z.IPCODPACI=@Identificacion
			AND Z.NUMINGRES=@Ingreso
			AND Z.CODSERIPS = A.CODSERIPS AND ISNULL(Z.IDDESCRIPCIONRELACIONADA, 0) = ISNULL(A.IDDESCRIPCIONRELACIONADA, 0)) AS cantidadrealizada ,
		  0 AS Cantidad, CD.Name as ContractDescriptionName, A.IDDESCRIPCIONRELACIONADA AS ContractDescriptionId

		FROM 
		  .dbo.HCPROCTER A 
		  INNER JOIN .dbo.INCUPSIPS B ON A.CODSERIPS = B.CODSERIPS 
		  LEFT JOIN contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA
		  LEFT JOIN contract.ContractDescriptions CD on CD.Id = CDD.ContractDescriptionId
		WHERE 
		  A.IPCODPACI = @Identificacion 
		  AND A.NUMINGRES = @Ingreso 
		  AND A.CODCENATE = @Centro 
		  --AND A.UFUCODIGO = @Unidadfuncional Se comenta porque no se está teniendo en cuenta todas las terapias del paciente y ocultaba algunas
		  AND B.TIPSERTER = '1' 
		  AND NOT EXISTS (SELECT CODSERIPS 
							FROM HCORDPRON X 
							WHERE X.CODSERIPS = A.CODSERIPS 
							AND X.IDDESCRIPCIONRELACIONADA = A.IDDESCRIPCIONRELACIONADA
						    AND X.IPCODPACI = A.IPCODPACI 
							AND X.NUMINGRES = A.NUMINGRES
							AND X.MANEXTPRO = 0 --ODO Se agrega porque no se están listando las terapias realizadas si el médico ordena la misma terapia extramural 
						  ) 
		GROUP BY 
		  B.DESSERIPS, 
		  A.CODSERIPS,
		  CD.Name,
		  A.IDDESCRIPCIONRELACIONADA

END
CREATE VIEW [dbo].[ViewDashBoardPharmacy_SurgicalPackageDeatils]
AS

 SELECT CAST(NEWID()as varchar(50)) as Id,
		RTRIM(A.NUMINGRES) AS Ingreso,
		RTRIM(c.Name) AS Entidad,
		c.Code AS CodigoEntidad,
		CODCONTRA AS CodigoContrato ,
		CODPANATE AS CodigoPlan,
		RTRIM(CG.Name) AS ContratoPlan,
		RTRIM(DESPRODUC) AS Producto,
		--RTRIM(DESPRODUC) AS Producto,
		RTRIM ( A.CODPRODUC) as CodProducto,
		iif(D.TIPPRODUC = 3, '1', A.TIPOREGIS) AS Tipo,
		-- FIX: Cuando hay 2 paquetes con el mismo producto, CANPEDPRO de HCFARMEPD es la suma
		-- total (ej. 4), pero cada fila del subquery trae la cantidad propia del paquete (ej. 2).
		-- Usar A.CANPEDPRO mostraba 4 en ambas filas haciendo parecer que cada paquete pedía 4.
		ISNULL(dr.Quantity, A.CANPEDPRO) AS CantidadSolicitada,
		ISNULL(dr.Quantity, 0) AS CantidadPaqueteEnf,
		CAST('' AS INT) AS CantidadEntregada,
		-- FIX CantidadPendiente: distribución proporcional de CANPENPRO según la participación
		-- del paquete en el total solicitado. Ejemplos:
		--   1 paquete  Qty=2, CANPEDPRO=2, CANPENPRO=1 → ROUND(1 * 2/2) = 1  ✓
		--   2 paquetes Qty=2, CANPEDPRO=4, CANPENPRO=4 → ROUND(4 * 2/4) = 2 c/u ✓
		--   2 paquetes Qty=2, CANPEDPRO=4, CANPENPRO=2 → ROUND(2 * 2/4) = 1 c/u ✓
		ISNULL(
			CASE
				WHEN A.CANPEDPRO = 0 THEN 0
				ELSE ROUND(CAST(A.CANPENPRO AS FLOAT) * CAST(dr.Quantity AS FLOAT) / CAST(A.CANPEDPRO AS FLOAT), 0)
			END,
			A.CANPENPRO
		) AS CantidadPendiente,
		CAST(0 AS BIT) AS Unico,A.NOPOSPROD AS NOPOS, A.CODUNIMED AS UnidadMedida,
		CONCAT(Rtrim( A.CODPROSAL), ' - ',rtrim(e.NOMMEDICO)) as Medico, 
		e.CODIGONIT as NitMedico ,
		CAST('' AS INT) AS FilaSeleccionada,
		A.IDETIPHIS,
		A.NUMEFOLIO,
		'0' AS Opcion,
		'0' AS OpcionAnulado,
		'' AS Procedimiento,
		CAST('0' AS BIT) AS MarcarOpcion , 
		RTRIM(A.IPCODPACI) as CodigoPaciente,A.CODCONCEC as Consecutivo, A.PROESTADO as Estado ,
		CONCAT(f.CODESPECI ,' - ', f.DESESPECI) as Especialidad,
		IIF
			( dr.Id IS NULL,
				CONCAT('Procedimiento QX: ',Rtrim(Ser.CODSERIPS), ' - ', Rtrim(Ser.DESSERIPS)),
				CONCAT('Paquete de enfermería - ',dr.NOMBRE)	
			) As ServicioIPS,
		A.IDAGEPROGQX,
		isnull(dr.NOMBRE, 'Producto adicional') as PackageCodeName,
		dr.PackageId,
		D.TIPPRODUC,
		DR.IdCostCenter
FROM dbo.HCFARMEPD A with(nolock)
INNER JOIN dbo.ADINGRESO B with(nolock) ON A.NUMINGRES=B.NUMINGRES
INNER JOIN Contract.CareGroup as CG with(nolock) on CG.Id = B.GENCAREGROUP
INNER JOIN Contract.HealthAdministrator c with(nolock) on b.GENCONENTITY = c.Id
INNER JOIN dbo.IHLISTPRO D with(nolock) ON A.CODPRODUC=D.CODPRODUC 
INNER JOIN dbo.INPROFSAL AS E with(nolock) ON A.CODPROSAL = E.CODPROSAL
INNER JOIN dbo.INESPECIA as F with(nolock) ON e.CODESPEC1 = f.CODESPECI 
INNER JOIN dbo.AGEPROGQX AS P with(nolock) ON A.IDAGEPROGQX = P.CODAUTONU
INNER JOIN dbo.INCUPSIPS AS Ser with(nolock) ON Ser.CODSERIPS = P.CODSERIPS 
------------- paquete de enfermeria -----------------------------
LEFT JOIN (
	SELECT DISTINCT(npod.Id), npo.Id PackageId, npo.NUMINGRES, npod.Quantity, paq.NOMBRE, npod.CODPRODUC, npo.IDHCFARMEPC, paqd.IdCostCenter
	FROM MedicalHistory.NursingPackagesOrder npo
	JOIN MedicalHistory.NursingPackagesOrderDetail npod ON npo.Id = npod.IdNursingPackagesOrder
	JOIN dbo.AGPAQUETES paq ON paq.ID = npo.IDAGPAQUETES
	OUTER APPLY(
		SELECT TOP 1 paqd.IdCostCenter
		FROM AGPAQUETESD paqd
		WHERE paq.ID = paqd.IDAGPAQUETE
			AND npod.CODPRODUC = paqd.CODPRODUC
			AND paq.ParameterizationCostCenter = 1
	) paqd
) as dr ON dr.NUMINGRES = a.NUMINGRES AND dr.CODPRODUC = a.CODPRODUC AND dr.IDHCFARMEPC = A.CODCONCEC
WHERE A.CANPENPRO >0 AND A.IDAGEPROGQX IS NOT NULL
CREATE VIEW [dbo].[ViewDashboardPharmacyDetailDevolution]
AS
	WITH DetailCUM AS (
		SELECT	dpc.Id,
				dpc.ProductId,
				dpc.BatchCode,
				dpc.IDHCFISIPRO,
				pr.Code,
				pr.[Name],
				hc.CANACTPRO
		       FROM HCDEVMEDD h
			   JOIN MedicalHistory.DetailPhysicalCUM dpc ON h.IdDetailPhysicalCUM = dpc.Id
			   JOIN Inventory.InventoryProduct pr ON dpc.ProductId = pr.Id
			   JOIN dbo.HCFISIPRO hc ON dpc.IDHCFISIPRO = hc.ID
			   WHERE h.IdDetailPhysicalCUM IS NOT NULL
					AND h.PROESTADO = 1
	)
	SELECT	CAST(NEWID()as varchar(50)) as Id,
			A.CODCONCEC as Consecutivo,
			A.NUMINGRES as Ingreso,
			A.CODCENATE,
			A.UFUCODIGO,
			rtrim(C.Name) as Entidad,
			RTRIM(CG.Name) AS ContratoPlan, 
			RTRIM(A.IPCODPACI) as CodigoPacienteDevolucion, 
			A.CODPROSAL, 
			CONCAT(Rtrim( A.CODPROSAL), ' - ',rtrim(e.NOMMEDICO)) as Medico,
			CONCAT(rtrim(f.CODESPECI) ,' - ', rtrim(f.DESESPECI)) as Especialidad,
			A.CODPRODUC, 
			RTRIM(d.DESPRODUC) AS Producto,  		
			D.TIPPRODUC  AS Tipo, 
			A.CANDEVOLV as CantidadDevuelta, 
			A.CANPENDIE as CantidadPendiente,
			D.NOPOSPROD AS NOPOS,
			'' PBS,
			'' UNIRS,
			A.FECRESGIS, 
			A.PROESTADO, 
			A.CODUSUARI,
			A.CODCONCEC EntityId,
			'HCDEVMEDD' EntityName,
			IIF(A.IdDetailPhysicalCUM IS NULL, FP.CANACTPRO, dc.CANACTPRO) As CantidadFisico,
			dc.ProductId as FinalProductId,
			dc.Code as FinalProductCode,
			dc.[Name] as FinalProductName,
			A.Id as HCDEVMEDDId,
			dc.BatchCode,
			D.TIPPRODUC,
			[Inventory].[FlagCustodyProduct](A.CODPRODUC,A.NUMINGRES) Custody
	FROM dbo.HCDEVMEDD AS A 
	JOIN dbo.ADINGRESO AS B ON A.NUMINGRES = B.NUMINGRES 
	JOIN Contract.CareGroup as CG on CG.Id = B.GENCAREGROUP 
	JOIN Contract.HealthAdministrator AS C ON B.GENCONENTITY = C.Id 
	JOIN dbo.IHLISTPRO AS D ON A.CODPRODUC = D.CODPRODUC 
	JOIN dbo.INPROFSAL AS E ON A.CODPROSAL = E.CODPROSAL 
	JOIN dbo.INESPECIA as F ON e.CODESPEC1 = f.CODESPECI 
	LEFT JOIN DetailCUM dc ON a.IdDetailPhysicalCUM = dc.Id AND A.IdDetailPhysicalCUM IS NOT NULL
	LEFT JOIN dbo.HCFISIPRO as FP on A.NUMINGRES = FP.NUMINGRES AND FP.CODPRODUC = A.CODPRODUC AND FP.UFUCODIGO = a.UFUCODIGO AND A.IdDetailPhysicalCUM IS NULL
	WHERE A.PROESTADO = '1' 
	/** Comentado por Cristhian Salazar, pues esto estaba genrando error al momento de anular la solicitud que tenian detalles pendientes pero que el inve. fisico ya estaba en cero y se estaban represando***/

	--and fp.TIPREGIST = 1 
	--and (FP.ID is null or FP.CANACTPRO > 0)
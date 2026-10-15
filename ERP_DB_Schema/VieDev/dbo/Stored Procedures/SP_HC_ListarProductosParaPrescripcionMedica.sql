CREATE PROCEDURE [dbo].[SP_HC_ListarProductosParaPrescripcionMedica]
(
@Almacen char(4),
@Denominacion char(20),
@VersionERP int,
@CentroAtencion char(20)
)
AS
BEGIN
	SET NOCOUNT ON;

	
	
	--fox
IF @VersionERP=1


				SELECT  max(B.IPRCODIGO),RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Medicamento,NOPOSPROD AS 'NO POS'
			,SUM(CAST(COALESCE(NULLIF(D.IFICANTID,0),0) AS INT)) AS Disponibles,TIPFORMED,CODGRUFAR,A.CODJUMEES As JustificacionMedicamentosEspeciales
			,Antibiotic = CAST(0 AS BIT), ResistenciaBacteriana = CAST(0 AS BIT), ObservationRB = '', Conditioned = CAST(0 AS BIT), UNIRS = 'No'
			, PBS = CASE WHEN NOPOSPROD = 1 THEN 'No' ELSE 'Si' END
		FROM dbo.IHLISTPRO A 
		inner join dbo.IHRINDDGH AS B ON A.CODPRODUC=B.CODPRODUC
	inner join dbo.INPRODUC C ON B.IPRCODIGO=C.IPRCODIGO 
	left outer join dbo.INFISICO D on D.IPRCODIGO=C.IPRCODIGO 
	inner join dbo.INALMACE E ON D.IALCODIGO=E.IALCODIGO
					WHERE CODDCIMED=@Denominacion AND TIPPRODUC IN ('1','3') AND PROESTADO = 1 AND ESPDILPRO='0' AND E.IALCODIGO=@Almacen
		GROUP BY A.CODPRODUC,DESPRODUC,NOPOSPROD,A.TIPPRODUC,TIPFORMED,CODGRUFAR,A.CODJUMEES
       
       --net
ELSE IF @VersionERP=2
	
	
				SELECT MAX( B.IPRCODIGO) AS IPRCODIGO ,RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Medicamento,NOPOSPROD AS 'NO POS'
			,SUM(CAST(COALESCE(NULLIF(D.IFICANTID,0),0) AS INT)) AS Disponibles,TIPFORMED,CODGRUFAR,A.CODJUMEES As JustificacionMedicamentosEspeciales
			,Antibiotic = CAST(0 AS BIT), ResistenciaBacteriana = CAST(0 AS BIT), ObservationRB = '', Conditioned = CAST(0 AS BIT), UNIRS = 'No'
			, PBS = CASE WHEN NOPOSPROD = 1 THEN 'No' ELSE 'Si' END
		FROM dbo.IHLISTPRO A 
		inner join dbo.IHRINDDGH AS B ON A.CODPRODUC=B.CODPRODUC
	inner join dbo.INNPRODUC C ON B.IPRCODIGO=C.IPRCODIGO 
	left outer join dbo.INNFISICO D on D.INNPRODUC=C.OID
				WHERE (c.OID in (select  INNPRODUC from dbo.INNFISICO a inner join
				dbo.INNALMACE b on a.INNALMACE=b.OID /*where b.IALCODIGO=@Almacen*/) or  c.OID not in (select  INNPRODUC from dbo.INNFISICO)) and
				 CODDCIMED=@Denominacion AND TIPPRODUC IN ('1','3') AND PROESTADO = 1 AND ESPDILPRO='0'
		GROUP BY A.CODPRODUC,DESPRODUC,NOPOSPROD,A.TIPPRODUC,TIPFORMED,CODGRUFAR,A.CODJUMEES
       
ELSE

		--select  CodeCUM,Codigo,Medicamento,[NO POS],sum(isnull(INV.Quantity,0)) as Disponibles,TIPFORMED,CODGRUFAR, JustificacionMedicamentosEspeciales, Antibiotic, ResistenciaBacteriana, ObservationRB
		--from (
		--SELECT  MAX(B.CodeCUM)as CodeCUM,RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Medicamento, D.NOPOSPROD AS 'NO POS',D.TIPFORMED,D.CODGRUFAR,D.CODJUMEES As JustificacionMedicamentosEspeciales, C.Antibiotic
		--, ResistenciaBacteriana = CAST(CASE WHEN BRM.Id IS NULL THEN 0 ELSE 1 END AS BIT), ObservationRB = ISNULL(BRM.Observation,'')
		--		from Inventory.InventoryProduct AS B
		--		INNER JOIN Inventory.ATC AS C ON C.Id = B.ATCId
		--		INNER JOIN dbo.IHLISTPRO AS D ON D.CODPRODUC = C.Code
		--		LEFT OUTER JOIN INVENTORY.BacterialResistanceMedication BRM
		--		ON BRM.AtcId = C.Id AND BRM.StateBRM IN (1,2) AND [Common].[GETDATE]() BETWEEN BRM.StartDate AND BRM.EndDate
		--		where  D.CODDCIMED = @Denominacion AND 
		--		D.TIPPRODUC IN ('1','3') AND D.PROESTADO = 1 and B.Status = 1 AND D.ESPDILPRO='0' 
		--		GROUP BY D.CODPRODUC,D.DESPRODUC,D.NOPOSPROD,D.TIPPRODUC,D.TIPFORMED,D.CODGRUFAR,D.CODJUMEES, C.Antibiotic, CAST(CASE WHEN BRM.Id IS NULL THEN 0 ELSE 1 END AS BIT), ISNULL(BRM.Observation,'')
		--) as PRO
		--		left join (
		--		select ATC.Code,Quantity from Inventory.PhysicalInventory phy
		--		INNER JOIN Inventory.Warehouse AS E ON phy.WarehouseId = E.Id
		--		inner join Inventory.InventoryProduct invpro on invpro.Id = phy.ProductId
		--		inner join Inventory.ATC atc on atc.Id = invpro.ATCId
		--		where E.Code = @Almacen  and 
		--		invpro.Status = 1 and atc.DiluentProduct = 0) as INV on PRO.Codigo = INV.Code
		--		group by CodeCUM,Codigo,Medicamento,[NO POS],TIPFORMED,CODGRUFAR, JustificacionMedicamentosEspeciales, Antibiotic, ResistenciaBacteriana, ObservationRB

		--DECLARE @CentroAtencion char(20)

		DECLARE @Almacenes as table
		(Id int)

		if @CentroAtencion is not null and len(rtrim(@CentroAtencion))> 0
		begin
			INSERT INTO @Almacenes
			select Id from Inventory.Warehouse where CodeCenterAttention = @CentroAtencion
		end

		INSERT INTO @Almacenes
		select Id from Inventory.Warehouse where CodeCenterAttention IS NULL

		SELECT MAX(B.CodeCUM)as CodeCUM,sum(isnull(phy.Quantity,0)) as Disponibles, RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Medicamento, D.NOPOSPROD AS 'NO POS',D.TIPFORMED,D.CODGRUFAR,D.CODJUMEES As JustificacionMedicamentosEspeciales
		, C.Antibiotic, ResistenciaBacteriana = CAST(CASE WHEN BRM.Id IS NULL THEN 0 ELSE 1 END AS BIT), ObservationRB = ISNULL(BRM.Observation,''), C.Conditioned, UNIRS = CASE WHEN C.UNIRS = 1 THEN 'Si' ELSE 'No' END
		, PBS = CASE WHEN C.Conditioned = 1 THEN 'Condicionado'
		   WHEN	D.NOPOSPROD = 1 THEN 'No'
		   ELSE 'Si'
		   END
		,D.PROCONTRO,
		B.DairyComponent AS 'ComponenteLacteo',
		B.DairyComponentType AS 'TipoComponenteLacteo'
		from 
		dbo.IHLISTPRO D With(Nolock)
		INNER JOIN Inventory.ATC C  With(Nolock) ON RTRIM(D.CODPRODUC) = C.Code and C.DiluentProduct = 0
		LEFT OUTER JOIN Inventory.InventoryProduct B  With(Nolock) on B.Status = 1 AND B.ATCId = C.Id
		LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = B.Id
		AND phy.WarehouseId IN (SELECT Id from @Almacenes)
		LEFT OUTER JOIN Inventory.Warehouse E With(Nolock) ON phy.WarehouseId = E.Id 
		LEFT OUTER JOIN INVENTORY.BacterialResistanceMedication BRM
		ON BRM.AtcId = C.Id AND BRM.StateBRM IN (1,2) AND [Common].[GETDATE]() BETWEEN BRM.StartDate AND BRM.EndDate
		WHERE D.CODDCIMED = @Denominacion AND 
		D.TIPPRODUC IN ('1', '3') AND D.ESPDILPRO = 0 AND D.PROESTADO = 1
 		GROUP BY RTRIM(D.CODPRODUC), RTRIM(D.DESPRODUC), D.NOPOSPROD, D.TIPFORMED, D.CODGRUFAR, D.CODJUMEES
		, C.Antibiotic, CAST(CASE WHEN BRM.Id IS NULL THEN 0 ELSE 1 END AS BIT), ISNULL(BRM.Observation,''), C.Conditioned, C.UNIRS, D.PROCONTRO, B.DairyComponent, B.DairyComponentType 
END
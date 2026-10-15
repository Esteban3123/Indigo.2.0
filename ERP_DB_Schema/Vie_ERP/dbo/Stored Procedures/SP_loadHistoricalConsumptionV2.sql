-- Stored Procedure

-- =============================================
-- Author:		Juan Diego Díaz
-- Create date: 2018-08-08
-- Description:	Obtiene el historico de consumos de medicamentos
-- por dia / mes / año de todos los productos de forma paginada
-- =============================================
CREATE PROCEDURE [dbo].[SP_loadHistoricalConsumptionV2]
	-- Add the parameters for the stored procedure here
	 @init as int,
	 @offset as int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	;WITH hc_table AS
		(Select
			convert(varchar(20),Pac.IPFECNACI,103)  as 'F. Nacimiento'
			,Case Pac.ipsexopac when 1 then 'M'ELSE'F' end 'Sexo'
			,Rtrim(M.MUNNOMBRE)  as 'Municipio'
			,Rtrim(E.nomdepart) as 'Departamento'
			,Rtrim(O.CODENTIDA) as 'Cód. Entidad'
			,Rtrim(O.NOMENTIDA) as 'Nom. Entidad'
			,CONVERT(int, I.NUMINGRES) as Ingreso
			,FORMAT (C.DocumentDate,'dd/MM/yyyy') as 'F. Ingreso'
			,DAY(C.DocumentDate) as 'Día'
			,MONTH(C.DocumentDate) as 'Mes'
			,YEAR(C.DocumentDate) as 'Año'
			,RTRIM(Q2.CODDIAGNO) as 'Cód. Diagnostico'
			,RTRIM(Q2.NOMDIAGNO) as 'Nom. Diagnostico'
			,RTRIM(P.CodeAlternative) as 'Cód. Producto'
			,RTRIM(P.Name) as 'Desc. Product'
			,RTRIM(P.Code) as 'Cód. Producto Homologación'
			,RTRIM(Z.UFUCODIGO) as 'Cód. UF'
			,RTRIM(Z.UFUDESCRI) as 'Desc. UF'
			,D.Quantity as 'Cantidad'
			,ROW_NUMBER() OVER(ORDER BY C.DocumentDate,I.NUMINGRES,P.Code,Q2.CODDIAGNO) AS row_number
            from inventory.PharmaceuticalDispensing as C
            inner JOIN inventory.PharmaceuticalDispensingDetail as D with(nolock) ON C.Id=D.PharmaceuticalDispensingId
            inner join Inventory.InventoryProduct as P with(nolock) ON D.ProductId=P.Id
            inner JOIN dbo.ADINGRESO as I with(nolock) ON I.NUMINGRES=C.AdmissionNumber
            Inner Join dbo.INDIAGNOS Q with(nolock)    ON Q.CODDIAGNO = I.CODDIAING
            Inner Join dbo.INDIAGNOS Q2 with(nolock)   ON Q2.CODDIAGNO = I.CODDIAEGR
            Inner Join dbo.INPACIENT Pac with(nolock)  ON Pac.IPCODPACI = I.IPCODPACI
            Inner Join dbo.INUNIFUNC Z with(nolock)    ON Z.UFUCODIGO = I.UFUCODIGO
            Inner Join dbo.INENTIDAD O with(nolock)     ON O.CODENTIDA = Pac.CODENTIDA
            Inner Join dbo.ADCENATEN J with(nolock)     ON J.CODCENATE = I.CODCENATE
            Inner Join dbo.INMUNICIP M with(nolock)     ON J.DEPMUNCOD = M.DEPMUNCOD
            Inner Join dbo.INDEPARTA E with(nolock)     ON M.DEPCODIGO = E.DEPCODIGO
            where
            C.Status=2
		)
		Select [F. Nacimiento],Sexo,Municipio,Departamento,[Cód. Entidad],[Nom. Entidad],Ingreso,
		[F. Ingreso],Día,Mes,Año,[Cód. Diagnostico],[Nom. Diagnostico],[Cód. Producto],[Desc. Product],
		[Cód. Producto Homologación], [Cód. UF],[Desc. UF],Cantidad from hc_table 
		WHERE row_number BETWEEN @init AND @offset

END
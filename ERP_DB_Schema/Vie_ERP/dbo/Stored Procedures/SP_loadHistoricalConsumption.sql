-- =============================================
-- Author:		Juan Diego Díaz
-- Create date: 2018-08-08
-- Description:	Obtiene el historico de consumos de medicamentos
-- por dia / mes / año de todos los productos de forma paginada
-- =============================================
CREATE PROCEDURE [dbo].[SP_loadHistoricalConsumption]
	-- Add the parameters for the stored procedure here
	 @init as int,
	 @offset as int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	;WITH hc_table AS
		(
		select 
			FORMAT (IPFECNACI,'dd/MM/yyyy') as 'F. Nacimiento'
			,Case ipsexopac when 1 then 'M'ELSE'F' end 'Sexo'
			,Rtrim(M.MUNNOMBRE)  as 'Municipio'
			,Rtrim(D.nomdepart) as 'Departamento'
			,Rtrim(E.CODENTIDA) as 'Cód. Entidad'
			,Rtrim(E.NOMENTIDA) as 'Nom. Entidad'
			,CONVERT(int, I.NUMINGRES) as Ingreso
			,FORMAT (IFECHAING,'dd/MM/yyyy') as 'F. Ingreso'
			,DAY(IFECHAING) as 'Día'
			,MONTH(IFECHAING) as 'Mes'
			,YEAR(IFECHAING) as 'Año'
			, RTRIM(DIAG.CODDIAGNO) as 'Cód. Diagnostico'
			, RTRIM(DIAG.NOMDIAGNO) as 'Nom. Diagnostico'
			, RTRIM(Producto.CODPRODUC) as 'Cód. Producto'
			, RTRIM(Producto.DESPRODUC) as 'Desc. Product'
			, RTRIM(inv.Code) as 'Cód. Producto Homologación'
			, RTRIM(Unidad.UFUCODIGO) as 'Cód. UF'
			, RTRIM(Unidad.UFUDESCRI) as 'Desc. UF'
			,HOJAMED.DOSISPROD as 'Cantidad'
			, RTRIM(UnidadMedida.Desunimed) as 'UnidadMedida'
			,ROW_NUMBER() OVER(ORDER BY IFECHAING,I.NUMINGRES,Producto.CODPRODUC,DIAG.CODDIAGNO) AS row_number
			from inpacient p
			Inner Join dbo.INUBICACI U with(nolock) ON U.AUUBICACI= P.AUUBICACI
			Inner Join dbo.INMUNICIP M with(nolock) on M.DEPMUNCOD = U.DEPMUNCOD
			Inner Join dbo.INDEPARTA D with(nolock) on M.DEPCODIGO = D.DEPCODIGO
			Inner Join INENTIDAD E with(nolock) ON E.CODENTIDA = P.CODENTIDA
			Inner Join  HCHOJAMED HOJAMED with(nolock) ON HOJAMED.IPCODPACI = P.IPCODPACI
			Inner Join  HCPRESCRA PRES with(nolock) ON PRES.codconcec = HOJAMED.CONSECPRESCRA
			inner join HCHISPACA H with(nolock)  ON H.IPCODPACI = PRES.IPCODPACI AND H.NUMINGRES = PRES.NUMINGRES AND H.NUMEFOLIO = PRES.NUMEFOLIO
			Inner Join  ADINGRESO I with(nolock) ON I.NUMINGRES = H.NUMINGRES
			Inner Join INDIAGNOS DIAG with(nolock) ON DIAG.CODDIAGNO = H.CODDIAGNO
			Inner Join INUNIFUNC Unidad with(nolock) ON  Unidad.UFUCODIGO = H.UFUCODIGO
			inner join ADCENATEN CentroAtencion with(nolock) ON  CentroAtencion.CODCENATE = H.CODCENATE
			inner join IHLISTPRO Producto with(nolock) ON  Producto.CODPRODUC = HOJAMED.CODPRODUC
			INNER JOIN Inventory.ATC atc ON atc.Code = Producto.CODPRODUC
			INNER JOIN Inventory.InventoryProduct inv ON atc.Id = inv.ATCId
			inner join INUNIMEDI UnidadMedida with(nolock) ON  UnidadMedida.CODUNIMED = HOJAMED.CODUNIMED
			where MEDESTADO = 2
		)
		Select [F. Nacimiento],Sexo,Municipio,Departamento,[Cód. Entidad],[Nom. Entidad],Ingreso,
		[F. Ingreso],Día,Mes,Año,[Cód. Diagnostico],[Nom. Diagnostico],[Cód. Producto],[Desc. Product],
		[Cód. Producto Homologación], [Cód. UF],[Desc. UF],Cantidad,UnidadMedida from hc_table 
		WHERE row_number BETWEEN @init AND @offset

END
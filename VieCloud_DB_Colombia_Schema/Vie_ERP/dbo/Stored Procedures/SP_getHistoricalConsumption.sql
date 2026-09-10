-- =============================================
-- Author:		Juan Diego Díaz
-- Create date: 2018-08-08
-- Description:	Obtiene el historico de consumos de medicamentos
-- por mes / año
-- =============================================
CREATE PROCEDURE [dbo].[SP_getHistoricalConsumption]
	-- Add the parameters for the stored procedure here
	 @Products as char(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

		--select
		--DISTINCT
		--RTRIM(Producto.DESPRODUC) as Producto
		--,RTRIM(Producto.CODPRODUC) as CodPro
		--,UnidadMedida.Desunimed as UnidadMedida
		--,FORMAT (IFECHAING,'dd/MM/yyyy') as 'F. Ingreso'
		--,datepart(day,I.IFECHAING) as 'Dia'
		--,datepart(month,I.IFECHAING) as 'Mes'
		--,datepart(year,I.IFECHAING) as 'Año'
		--,SUM(HOJAMED.DOSISPROD) as Cantidad
		--from inpacient p
		--Inner Join  HCHOJAMED HOJAMED with(nolock) ON HOJAMED.IPCODPACI = P.IPCODPACI
		--Inner Join  HCPRESCRA PRES with(nolock) ON PRES.codconcec = HOJAMED.CONSECPRESCRA
		--inner join HCHISPACA H with(nolock)  ON H.IPCODPACI = PRES.IPCODPACI AND 
		--H.NUMINGRES = PRES.NUMINGRES AND H.NUMEFOLIO = PRES.NUMEFOLIO
		--Inner Join  ADINGRESO I with(nolock) ON I.NUMINGRES = H.NUMINGRES
		--inner join IHLISTPRO Producto with(nolock) ON  Producto.CODPRODUC = HOJAMED.CODPRODUC
		----inner join INUNIMEDI UnidadMedida with(nolock) ON  UnidadMedida.CODUNIMED = HOJAMED.CODUNIMED
		--where MEDESTADO = 2 AND Producto.CODPRODUC IN (select * from SplitStrings_Moden(@Products, ','))
		--GROUP BY datepart(day,I.IFECHAING),datepart(month,I.IFECHAING),datepart(year,I.IFECHAING),
		--Producto.DESPRODUC,Producto.CODPRODUC

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
			,RTRIM(DIAG.CODDIAGNO) as 'Cód. Diagnostico'
			,RTRIM(DIAG.NOMDIAGNO) as 'Nom. Diagnostico'
			,RTRIM(Producto.CODPRODUC) as 'Cód. Producto'
			,RTRIM(Producto.DESPRODUC) as 'Desc. Product'
			,RTRIM(inv.Code) as 'Cód. Producto Homologación'
			,RTRIM(Unidad.UFUCODIGO) as 'Cód. UF'
			,RTRIM(Unidad.UFUDESCRI) as 'Desc. UF'
			,HOJAMED.DOSISPROD as 'Cantidad'
			,RTRIM(UnidadMedida.Desunimed) as 'UnidadMedida'
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
			where MEDESTADO = 2 AND inv.Code IN (select * from SplitStrings_Moden(@Products, ','))
			--Producto.CODPRODUC IN (select * from SplitStrings_Moden(@Products, ','))

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el historial de consumo de medicamentos por mes y año, filtrando por uno o varios productos (identificados por código de homologación del inventario). Para cada prescripción despachada, retorna datos del paciente (fecha de nacimiento, sexo, municipio y departamento de residencia, entidad aseguradora), del episodio de atención (número de ingreso, fecha de ingreso, día, mes y año), del diagnóstico CIE-10 asociado al folio clínico, del medicamento o producto (código, descripción, código homologado, unidad de medida) y de la unidad funcional donde se atendió. Integra la hoja de medicamentos (HCHOJAMED), la prescripción (HCPRESCRA), la historia clínica (HCHISPACA), el ingreso (ADINGRESO), el paciente (INPACIENT), la ubicación geográfica (INUBICACI, INMUNICIP, INDEPARTA), la entidad pagadora (INENTIDAD) y el catálogo de diagnósticos (INDIAGNOS), cruzando además con el inventario de productos para obtener el código de homologación ATC. Se usa para analítica farmacéutica, control de consumos de medicamentos y reportes de gestión de medicamentos por período.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_getHistoricalConsumption';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_getHistoricalConsumption';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el histórico de consumo de medicamentos administrados a pacientes (hoja de medicación), enriquecido con datos demográficos, ubicación, entidad, ingreso, diagnóstico, unidad funcional y homologación de inventario, filtrado por una lista de códigos de producto homologados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_getHistoricalConsumption';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe recibir una cadena de códigos de producto separados por coma en el parámetro de entrada.; Debe existir la función SplitStrings_Moden para tokenizar la lista.; Los productos consultados deben estar homologados en Inventory.ATC e Inventory.InventoryProduct.; Los registros de hoja de medicación deben tener estado MEDESTADO=2 (administrados/aplicados) para ser considerados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_getHistoricalConsumption';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen consumos cuyo estado de medicación es 2.; El filtro de productos se aplica sobre el código homologado de InventoryProduct (inv.Code), no sobre el código nativo del producto.; Cada consumo se asocia obligatoriamente a un paciente, ingreso, folio de historia, diagnóstico, unidad funcional y centro de atención (joins internos).; Solo se retornan productos que tengan correspondencia en la clasificación ATC y en InventoryProduct.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_getHistoricalConsumption';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Sexo; Fecha de nacimiento; Municipio; Departamento; Entidad (asegurador/EPS); Ingreso hospitalario; Diagnóstico; Unidad funcional; Centro de atención; Hoja de medicación; Prescripción; Producto/Medicamento; Clasificación ATC; Homologación de inventario; Unidad de medida; Dosis/Cantidad administrada; Histórico de consumo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_getHistoricalConsumption';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas de consumo histórico solo cuando HCHOJAMED.MEDESTADO = 2 y el código homologado inv.Code está dentro de la lista recibida en @Products.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_getHistoricalConsumption';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ipsexopac = 1 → Se reporta el sexo como ''M'' (masculino) else Se reporta el sexo como ''F'' (femenino)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_getHistoricalConsumption';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SplitStrings_Moden', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_getHistoricalConsumption';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.inpacient; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; dbo.INENTIDAD; dbo.HCHOJAMED; dbo.HCPRESCRA; dbo.HCHISPACA; dbo.ADINGRESO; dbo.INDIAGNOS; dbo.INUNIFUNC; dbo.ADCENATEN; dbo.IHLISTPRO; Inventory.ATC; Inventory.InventoryProduct; dbo.INUNIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_getHistoricalConsumption';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_getHistoricalConsumption';
-- GO

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene el histórico paginado de consumos de medicamentos por paciente, mostrando para cada registro: datos demográficos del paciente (fecha de nacimiento, sexo, municipio y departamento de residencia), entidad aseguradora o pagadora, número y fecha del ingreso (con día, mes y año separados), diagnóstico CIE-10, producto farmacéutico dispensado con su código de homologación en el inventario, unidad funcional donde se atendió, cantidad y unidad de medida. Cruza información de admisiones (ADINGRESO), historia clínica y hojas de medicación (HCHISPACA, HCHOJAMED, HCPRESCRA), catálogos maestros de pacientes, ubicación geográfica, entidades, diagnósticos y productos del inventario farmacéutico. Se usa principalmente para reportería y análisis de consumo de medicamentos por período, centro de atención y patología, soportando procesos de farmacología, auditoría y gestión de costos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_loadHistoricalConsumption';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_loadHistoricalConsumption';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve, de forma paginada, el histórico de consumos de medicamentos administrados a pacientes, enriquecido con datos demográficos, geográficos, de ingreso, diagnóstico, unidad funcional y homologación de producto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_loadHistoricalConsumption';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en HCHOJAMED con MEDESTADO = 2 (medicamento efectivamente administrado/aplicado); Cada producto consumido debe estar homologado en Inventory.ATC e Inventory.InventoryProduct (INNER JOIN obligatorio); El paciente debe tener ubicación, municipio y departamento registrados; El consumo debe estar asociado a una prescripción (HCPRESCRA), historia clínica (HCHISPACA) e ingreso (ADINGRESO) válidos; Los parámetros @init y @offset definen el rango de filas a devolver', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_loadHistoricalConsumption';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan consumos con MEDESTADO = 2; Solo se incluyen productos que tengan correspondencia en la clasificación ATC y en InventoryProduct (homologación obligatoria); El sexo siempre se normaliza a ''M'' o ''F''; La paginación se realiza por ROW_NUMBER con orden estable: fecha de ingreso, número de ingreso, código de producto, código de diagnóstico; Las fechas se entregan formateadas como dd/MM/yyyy', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_loadHistoricalConsumption';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; consumo de medicamentos; prescripción; hoja de medicamentos; ingreso hospitalario; diagnóstico; unidad funcional; centro de atención; entidad (asegurador); homologación de producto; clasificación ATC; unidad de medida; ubicación geográfica (municipio/departamento)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_loadHistoricalConsumption';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando MEDESTADO = 2 y existen los joins de homologación ATC/InventoryProduct, se retornan las filas cuyo ROW_NUMBER (ordenado por fecha de ingreso, número de ingreso, código de producto y código de diagnóstico) esté entre @init y @offset', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_loadHistoricalConsumption';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ipsexopac = 1 → Se reporta el sexo como ''M'' (masculino) else Se reporta el sexo como ''F'' (femenino); si MEDESTADO = 2 → El registro de la hoja de medicamentos se incluye en el histórico de consumos else Se excluye del resultado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_loadHistoricalConsumption';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPACIENT; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; dbo.INENTIDAD; dbo.HCHOJAMED; dbo.HCPRESCRA; dbo.HCHISPACA; dbo.ADINGRESO; dbo.INDIAGNOS; dbo.INUNIFUNC; dbo.ADCENATEN; dbo.IHLISTPRO; Inventory.ATC; Inventory.InventoryProduct; dbo.INUNIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_loadHistoricalConsumption';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_loadHistoricalConsumption';
-- GO

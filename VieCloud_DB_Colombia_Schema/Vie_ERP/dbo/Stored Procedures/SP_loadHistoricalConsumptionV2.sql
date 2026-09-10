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
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Retorna de forma paginada el histórico de dispensaciones farmacéuticas completadas (estado 2), cruzando el detalle de cada dispensación con el ingreso hospitalario del paciente, sus datos demográficos, entidad aseguradora, diagnósticos de ingreso y egreso (CIE-10), producto dispensado y unidad funcional. El resultado incluye la fecha desglosada en día/mes/año para análisis temporal del consumo de medicamentos por ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_loadHistoricalConsumptionV2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_loadHistoricalConsumptionV2';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve, de forma paginada, el histórico de consumos/dispensaciones de medicamentos y productos con datos del paciente, ingreso, diagnóstico de egreso, unidad funcional y ubicación geográfica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_loadHistoricalConsumptionV2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir dispensaciones farmacéuticas con Status = 2 (dispensación efectiva/confirmada).; Cada dispensación debe estar asociada a un ingreso (AdmissionNumber) existente en ADINGRESO.; El ingreso debe tener diagnóstico de ingreso (CODDIAING) y diagnóstico de egreso (CODDIAEGR) registrados y vigentes en INDIAGNOS.; El paciente, la unidad funcional, la entidad, el centro de atención, el municipio y el departamento referenciados deben existir en sus catálogos.; Los parámetros @init y @offset definen el rango de filas a devolver sobre el resultado ordenado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_loadHistoricalConsumptionV2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan dispensaciones farmacéuticas con Status = 2.; El diagnóstico mostrado corresponde al diagnóstico de egreso del ingreso (CODDIAEGR), no al de ingreso.; El sexo del paciente se normaliza a dos valores: ''M'' o ''F''.; Las fechas de ingreso se entregan formateadas como dd/MM/yyyy y la fecha de nacimiento en formato 103 (dd/mm/yyyy).; La paginación es estable porque el orden está determinado por DocumentDate, NUMINGRES, Code del producto y código de diagnóstico.; La ubicación geográfica (municipio/departamento) se deriva del centro de atención del ingreso, no del domicilio del paciente.; La entidad reportada es la entidad de afiliación del paciente (Pac.CODENTIDA), no necesariamente la del ingreso.; Todas las consultas a tablas maestras se hacen con NOLOCK, permitiendo lecturas sucias.; Solo se incluyen consumos cuyo paciente, ingreso, diagnósticos, unidad funcional, entidad, centro de atención, municipio y departamento existan (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_loadHistoricalConsumptionV2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Consumo de medicamentos; Producto / medicamento; Paciente; Ingreso/admisión; Diagnóstico de ingreso; Diagnóstico de egreso; Unidad funcional; Entidad (asegurador/pagador); Centro de atención; Municipio/Departamento; Paginación de histórico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_loadHistoricalConsumptionV2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando C.Status = 2, retorna las filas de consumo cuyo ROW_NUMBER (ordenado por DocumentDate, NUMINGRES, Product.Code, Diagnóstico de egreso) está entre @init y @offset.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_loadHistoricalConsumptionV2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Pac.ipsexopac = 1 → Reporta el sexo del paciente como ''M'' (masculino). else Reporta el sexo como ''F'' (femenino) para cualquier otro valor.; si C.Status = 2 → Incluye la dispensación en el histórico de consumos. else Excluye la dispensación del resultado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_loadHistoricalConsumptionV2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'inventory.PharmaceuticalDispensing; inventory.PharmaceuticalDispensingDetail; Inventory.InventoryProduct; dbo.ADINGRESO; dbo.INDIAGNOS; dbo.INPACIENT; dbo.INUNIFUNC; dbo.INENTIDAD; dbo.ADCENATEN; dbo.INMUNICIP; dbo.INDEPARTA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_loadHistoricalConsumptionV2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_loadHistoricalConsumptionV2';
-- GO

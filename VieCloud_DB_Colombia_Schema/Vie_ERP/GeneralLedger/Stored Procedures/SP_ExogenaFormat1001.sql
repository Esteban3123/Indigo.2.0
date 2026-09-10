-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-10
-- Description:	SP que genera la informacion para el XML Formato 1001
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ExogenaFormat1001]
   @xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @YearData INT,
			@ExogenousFormatId INT

	SELECT	@YearData = t.x.value('Year[1]','int'),
			@ExogenousFormatId = t.x.value('ExogenousFormatId[1]','int')
	FROM @xmlCriterias.nodes('/Data') t(x)

	SELECT	v.Concept,
			IIF(v.IsMinimumValue = 0, v.IdentificationType, 43) IdentificationType,
			IIF(v.IsMinimumValue = 0, v.IdentificationNumber, '222222222') IdentificationNumber,
			IIF(v.IsMinimumValue = 0, v.DigitVerification, NULL) DigitVerification,
			IIF(v.IsMinimumValue = 0, v.FirstLastName, NULL) FirstLastName,
			IIF(v.IsMinimumValue = 0, v.SecondLastName, NULL) SecondLastName,
			IIF(v.IsMinimumValue = 0, v.FirstName, NULL) FirstName,
			IIF(v.IsMinimumValue = 0, v.SecondName, NULL) SecondName,
			IIF(v.IsMinimumValue = 0, v.BusinessName, 'CUANTIAS MENORES') BusinessName,
			IIF(v.IsMinimumValue = 0, v.Addresss, NULL) Addresss,
			IIF(v.IsMinimumValue = 0, v.CityCode, NULL) CityCode,
			IIF(v.IsMinimumValue = 0, v.DepartmentCode, NULL) DepartmentCode,
			IIF(v.IsMinimumValue = 0, v.CodeCountry, NULL) CodeCountry,
			SUM(v.PaymentDeductible) PaymentDeductible,
			SUM(v.PaymentNoDeductible) PaymentNoDeductible,
			SUM(v.IVADeductible) IVADeductible,
			SUM(v.IVANoDeductible) IVANoDeductible,
			SUM(v.ReteDeductible) ReteDeductible,
			SUM(v.ReteNoDeductible) ReteNoDeductible,
			SUM(v.ReteIVAComun) ReteIVAComun,
			SUM(v.ReteIVAExt) ReteIVAExt
	FROM
	(
		SELECT	v.Concept,
				CASE p.IdentificationType
					WHEN 3 THEN 11
					WHEN 2 THEN 12
					WHEN 0 THEN 13
					WHEN 1 THEN 22
					WHEN 7 THEN 31
					WHEN 4 THEN 41
					ELSE 43
				END AS IdentificationType,
				tp.Nit AS IdentificationNumber,
				IIF(p.IdentificationType = 7, tp.DigitVerification, NULL) DigitVerification,
				IIF(p.IdentificationType = 7, NULL, p.FirstLastName) FirstLastName,
				IIF(p.IdentificationType = 7, NULL, p.SecondLastName) SecondLastName,
				IIF(p.IdentificationType = 7, NULL, p.FirstName) FirstName,
				IIF(p.IdentificationType = 7, NULL, p.SecondName) SecondName,
				IIF(p.IdentificationType = 7, tp.Name, NULL) AS BusinessName,
				a.Addresss AS Addresss,
				c.Code CityCode,
				d.Code DepartmentCode,
				ct.Code CodeCountry,
				CAST(SUM(IIF(v.ConceptType = 1, v.Value, 0)) AS DECIMAL(18, 0)) AS PaymentDeductible,
				CAST(SUM(IIF(v.ConceptType = 2, v.Value, 0)) AS DECIMAL(18, 0)) AS PaymentNoDeductible,
				CAST(SUM(IIF(v.ConceptType = 3, v.Value, 0)) AS DECIMAL(18, 0)) AS IVADeductible,
				CAST(SUM(IIF(v.ConceptType = 4, v.Value, 0)) AS DECIMAL(18, 0)) AS IVANoDeductible,
				CAST(SUM(IIF(v.ConceptType = 5, v.Value, 0)) AS DECIMAL(18, 0)) AS ReteDeductible,
				CAST(SUM(IIF(v.ConceptType = 6, v.Value, 0)) AS DECIMAL(18, 0)) AS ReteNoDeductible,
				CAST(SUM(IIF(v.ConceptType = 7, v.Value, 0)) AS DECIMAL(18, 0)) AS ReteIVAComun,
				CAST(SUM(IIF(v.ConceptType = 8, v.Value, 0)) AS DECIMAL(18, 0))	AS ReteIVAExt,
				IIF(v.MinimumValue < SUM(v.Value), 0, 1) IsMinimumValue
		FROM GeneralLedger.ViewReportExogenousFormat v
		JOIN Common.ThirdParty tp WITH (NOLOCK) ON v.ThirdPartyId = tp.Id
		JOIN Common.Person p WITH (NOLOCK) ON tp.PersonId = p.Id
		LEFT JOIN [Common].[ViewLatestAddressByPerson] va ON p.Id = va.PersonId
		LEFT JOIN Common.Address a WITH (NOLOCK) ON va.AddressId = a.Id
		LEFT JOIN Common.City c WITH (NOLOCK) ON a.CityId = c.Id
		LEFT JOIN Common.Department d WITH (NOLOCK) ON c.DepartamentId = d.Id
		LEFT JOIN Common.Country ct WITH (NOLOCK) ON d.CountryId = ct.Id
		WHERE v.Year = @YearData
			AND v.ExogenousFormatId = @ExogenousFormatId
			AND v.[Format] = 1001
		GROUP BY v.Concept,
			v.MinimumValue,
			p.IdentificationType,
			tp.Nit,
			IIF(p.IdentificationType = 7, tp.DigitVerification, NULL),
			IIF(p.IdentificationType = 7, NULL, p.FirstLastName),
			IIF(p.IdentificationType = 7, NULL, p.SecondLastName),
			IIF(p.IdentificationType = 7, NULL, p.FirstName),
			IIF(p.IdentificationType = 7, NULL, p.SecondName),
			IIF(p.IdentificationType = 7, tp.Name, NULL),
			a.Addresss, c.Code, d.Code, ct.Code
	) v
	GROUP BY v.Concept,
		IIF(v.IsMinimumValue = 0, v.IdentificationType, 43),
		IIF(v.IsMinimumValue = 0, v.IdentificationNumber, '222222222'),
		IIF(v.IsMinimumValue = 0, v.DigitVerification, NULL),
		IIF(v.IsMinimumValue = 0, v.FirstLastName, NULL),
		IIF(v.IsMinimumValue = 0, v.SecondLastName, NULL),
		IIF(v.IsMinimumValue = 0, v.FirstName, NULL),
		IIF(v.IsMinimumValue = 0, v.SecondName, NULL),
		IIF(v.IsMinimumValue = 0, v.BusinessName, 'CUANTIAS MENORES'),
		IIF(v.IsMinimumValue = 0, v.Addresss, NULL),
		IIF(v.IsMinimumValue = 0, v.CityCode, NULL),
		IIF(v.IsMinimumValue = 0, v.DepartmentCode, NULL),
		IIF(v.IsMinimumValue = 0, v.CodeCountry, NULL)
	ORDER BY 1, 3
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera la información tributaria requerida para el Formato 1001 de la información exógena ante la DIAN, correspondiente a un año fiscal específico. Consolida pagos, retenciones en la fuente, IVA deducible y no deducible, y retenciones de IVA por cada tercero (proveedor, contratista o persona natural/jurídica), agrupando bajo la categoría ''CUANTÍAS MENORES'' aquellos beneficiarios cuyo valor total no supera el mínimo exigido. Para construir el reporte, cruza los movimientos contables de la vista de formato exógeno con los datos de identificación del tercero (NIT, dígito de verificación, tipo de documento DIAN), nombre o razón social, y la dirección más reciente de la persona, incluyendo ciudad, departamento y país. Recibe como parámetro de entrada un XML con el año de reporte y el identificador del formato exógeno a procesar.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat1001';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ExogenaFormat1001';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el detalle consolidado por concepto y tercero requerido por la DIAN para el Formato 1001 de información exógena (pagos y retenciones), agrupando bajo ''CUANTÍAS MENORES'' a quienes no superan el tope mínimo.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener los nodos /Data/Year y /Data/ExogenousFormatId; Debe existir información en ViewReportExogenousFormat para el año, ExogenousFormatId y Format=1001 indicados; Cada tercero referenciado debe existir en Common.ThirdParty y estar asociado a una persona en Common.Person; Los tipos de identificación de la persona deben mapearse al catálogo DIAN (3,2,0,1,7,4); cualquier otro se asume tipo 43', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los terceros cuya sumatoria de valores no supere la cuantía mínima se consolidan bajo un seudo-tercero ''CUANTIAS MENORES'' con NIT 222222222 y tipo de identificación 43; Para personas jurídicas (IdentificationType=7) nunca se reportan nombres/apellidos; para los demás nunca se reporta razón social ni dígito de verificación; Todos los valores monetarios se truncan a DECIMAL(18,0) (sin decimales); Los ConceptType del 1 al 8 se pivotean en columnas fijas: PaymentDeductible, PaymentNoDeductible, IVADeductible, IVANoDeductible, ReteDeductible, ReteNoDeductible, ReteIVAComun, ReteIVAExt; Solo se incluyen registros con Format=1001 y del año y formato exógeno solicitados', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Información Exógena DIAN; Formato 1001 (Pagos y abonos en cuenta); Cuantías menores; Tercero (NIT / persona natural); Tipo de identificación DIAN; Dígito de verificación; IVA deducible / no deducible; Retención en la fuente; ReteIVA común y régimen extraordinario; Dirección, ciudad, departamento, país', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultado de la consulta: Devuelve un conjunto de filas agregado por Concepto y tercero (o por agrupación ''CUANTIAS MENORES'' cuando IsMinimumValue=1), filtrado por Year=@YearData, ExogenousFormatId=@ExogenousFormatId y Format=1001, ordenado por Concepto e IdentificationNumber', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si p.IdentificationType IN (3,2,0,1,7,4) → Mapea a códigos DIAN: 3→11, 2→12, 0→13, 1→22, 7→31, 4→41 else Cualquier otro tipo se mapea a 43 (por defecto); si p.IdentificationType = 7 (NIT/persona jurídica) → Se usa razón social (tp.Name) y dígito de verificación; nombres y apellidos quedan NULL else Se usan nombres y apellidos de la persona natural; razón social y dígito verificación quedan NULL; si v.MinimumValue >= SUM(v.Value) (no supera la cuantía mínima) → Se marca IsMinimumValue=1 y el tercero se reporta como ''CUANTIAS MENORES'' con identificación 222222222, tipo 43 y se anonimizan datos personales/dirección else Se reportan los datos reales del tercero', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.ViewReportExogenousFormat; Common.ThirdParty; Common.Person; Common.ViewLatestAddressByPerson; Common.Address; Common.City; Common.Department; Common.Country', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ExogenaFormat1001';
-- GO

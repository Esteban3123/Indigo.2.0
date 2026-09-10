-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 12/09/2018
-- Description:	Sp que se encarga de listar los productos de una fórmula médica
-- =============================================
CREATE PROCEDURE [Inventory].[SP_ListHCPRESCRDByCODCONCEC]
	@CODCONCEC varchar(18)
AS
BEGIN
	SET NOCOUNT ON

	SELECT
		hc.IDETIPHIS
	   ,hc.NUMEFOLIO
	   ,hc.FECHAORDE
	   ,hc.CODPROSAL
	   ,hc.IPCODPACI
	   ,hc.NUMINGRES
	   ,hc.CODCENATE
	   ,hc.UFUCODIGO
	   ,hd.CODPRODUC
	   ,hd.CODVIAADM
	   ,hd.CODFORMED
	   ,hd.CANPEDPRO
	   ,hd.PREESTADO
	   ,hd.FOLIOINIC
	   ,hd.DESADMINI
	   ,hd.DOSISPRFN
	   ,ISNULL(hd.CODUNIMED, '') CODUNIMED
	   ,pro.CODDCIMED
	   ,pro.DESPRODUC
	   ,pro.NOPOSPROD
	   ,pro.TIPPRODUC
	   ,pro.CONCENMED
	   ,pro.PRESENMED
	   ,pac.IPTIPODOC
	   ,pac.CODIGONIT
	   ,pac.IPEXPEDIC
	   ,pac.IPPRIAPEL
	   ,pac.IPSEGAPEL
	   ,pac.IPPRINOMB
	   ,pac.IPSEGNOMB
	   ,pac.IPNOMCOMP
	   ,pac.CODEMPRES
	   ,pac.IPTIPOPAC
	   ,pac.IPTIPOAFI
	   ,pac.CAPACIPAG
	   ,pac.CODENTIDA
	   ,pac.CCCONTRAT
	   ,pac.CPPLANBEN
	   ,pac.AUUBICACI
	   ,pac.NIVCODIGO
	   ,pac.IPDIRECCI
	   ,pac.IPTELEFON
	   ,pac.IPTELMOVI
	   ,pac.IPFECNACI
	   ,pac.CODACTIVI
	   ,pac.IPSEXOPAC
	   ,pac.IPESTADOC
	   ,pac.IPGRUPSAN
	   ,pac.IPRHSANGR
	   ,pac.TIPCOBSAL
	   ,pac.CORELEPAC
	   ,pac.CODGRUPOE
	   ,pac.ESTADOPAC
	   ,pac.OBSERVACI
	   ,pac.INDAUDFOR
	   ,pac.PACIEFOTO
	   ,pac.PACIEHUELL
	   ,pac.NUMCARPET
	   ,pac.CODUSUCRE
	   ,pac.FECREGCRE
	   ,pac.CODUSUMOD
	   ,pac.FECREGMOD
	   ,pac.IPESTRATO
	   ,pac.CREDCODIGO
	   ,pac.DISCCODIGO
	   ,pac.IDICODIGO
	   ,pac.NIVECODIGO
	   ,pac.GRUPCODIGO
	   ,pac.ZONAPARTADA
	   ,pac.GENEXPEDITIONCITY
	   ,pac.IPORIENTSEXUAL
	   ,pac.IPIDENTSEXUAL
	   ,pac.IPORIENTSEXOTRO
	   ,pac.IPIDENTSEXOTRO
	   ,pac.PESO
	   ,pac.IPSEXO
	   ,pac.IDENTMAMA
	   ,pac.IDENTOBSERVAC
	   ,pac.ID
	   ,med.CODIGONIT CODIGONITPROFSAL
	   ,med.NOMMEDICO
	   ,med.MEDPRINOM
	   ,med.MEDSEGNOM
	   ,med.MEDPRIAPEL
	   ,med.MEDSEGAPEL
	   ,med.CODESPEC1
	   ,esp.DESESPECI
	   ,ing.GENCONENTITY
	   ,ing.CODCONTRA
	   ,ing.CODPANATE
	   ,ing.GENCAREGROUP
	   ,fu.UFUDESCRI
	FROM dbo.HCPRESCRD hd
	INNER JOIN dbo.HCPRESCRC hc ON hc.CODCONCEC = hd.CODCONCEC
	INNER JOIN dbo.IHLISTPRO pro ON pro.CODPRODUC = hd.CODPRODUC
	INNER JOIN dbo.INPACIENT pac ON pac.IPCODPACI = hd.IPCODPACI
	INNER JOIN dbo.INPROFSAL med ON med.CODPROSAL = hc.CODPROSAL
	INNER JOIN dbo.ADINGRESO ing ON ing.NUMINGRES = hc.NUMINGRES
	INNER JOIN dbo.INESPECIA esp ON esp.CODESPECI = med.CODESPEC1
	INNER JOIN dbo.INUNIFUNC fu ON fu.UFUCODIGO = hc.UFUCODIGO
	WHERE hd.CODCONCEC = @CODCONCEC
UNION ALL
	SELECT 
		hc.IDETIPHIS
	   ,hc.NUMEFOLIO
	   ,hc.FECHAORDE
	   ,hc.CODPROSAL
	   ,hc.IPCODPACI
	   ,hc.NUMINGRES
	   ,hc.CODCENATE
	   ,hc.UFUCODIGO
	   ,hd.CODPRODUC
	   ,CODVIAADM = ''
	   ,CODFORMED = ''
	   ,hd.CANPEDPRO
	   ,hd.PREESTADO
	   ,hc.NUMEFOLIO FOLIOINIC
	   ,DESADMINI = ''
	   ,DOSISPRFN = 0
	   ,CODUNIMED = ''
	   ,pro.CODDCIMED
	   ,pro.DESPRODUC
	   ,pro.NOPOSPROD
	   ,pro.TIPPRODUC
	   ,pro.CONCENMED
	   ,pro.PRESENMED
	   ,pac.IPTIPODOC
	   ,pac.CODIGONIT
	   ,pac.IPEXPEDIC
	   ,pac.IPPRIAPEL
	   ,pac.IPSEGAPEL
	   ,pac.IPPRINOMB
	   ,pac.IPSEGNOMB
	   ,pac.IPNOMCOMP
	   ,pac.CODEMPRES
	   ,pac.IPTIPOPAC
	   ,pac.IPTIPOAFI
	   ,pac.CAPACIPAG
	   ,pac.CODENTIDA
	   ,pac.CCCONTRAT
	   ,pac.CPPLANBEN
	   ,pac.AUUBICACI
	   ,pac.NIVCODIGO
	   ,pac.IPDIRECCI
	   ,pac.IPTELEFON
	   ,pac.IPTELMOVI
	   ,pac.IPFECNACI
	   ,pac.CODACTIVI
	   ,pac.IPSEXOPAC
	   ,pac.IPESTADOC
	   ,pac.IPGRUPSAN
	   ,pac.IPRHSANGR
	   ,pac.TIPCOBSAL
	   ,pac.CORELEPAC
	   ,pac.CODGRUPOE
	   ,pac.ESTADOPAC
	   ,pac.OBSERVACI
	   ,pac.INDAUDFOR
	   ,pac.PACIEFOTO
	   ,pac.PACIEHUELL
	   ,pac.NUMCARPET
	   ,pac.CODUSUCRE
	   ,pac.FECREGCRE
	   ,pac.CODUSUMOD
	   ,pac.FECREGMOD
	   ,pac.IPESTRATO
	   ,pac.CREDCODIGO
	   ,pac.DISCCODIGO
	   ,pac.IDICODIGO
	   ,pac.NIVECODIGO
	   ,pac.GRUPCODIGO
	   ,pac.ZONAPARTADA
	   ,pac.GENEXPEDITIONCITY
	   ,pac.IPORIENTSEXUAL
	   ,pac.IPIDENTSEXUAL
	   ,pac.IPORIENTSEXOTRO
	   ,pac.IPIDENTSEXOTRO
	   ,pac.PESO
	   ,pac.IPSEXO
	   ,pac.IDENTMAMA
	   ,pac.IDENTOBSERVAC
	   ,pac.ID
	   ,med.CODIGONIT CODIGONITPROFSAL
	   ,med.NOMMEDICO
	   ,med.MEDPRINOM
	   ,med.MEDSEGNOM
	   ,med.MEDPRIAPEL
	   ,med.MEDSEGAPEL
	   ,med.CODESPEC1
	   ,esp.DESESPECI
	   ,ing.GENCONENTITY
	   ,ing.CODCONTRA
	   ,ing.CODPANATE
	   ,ing.GENCAREGROUP
	   ,fu.UFUDESCRI
	FROM dbo.HCSOLINSC A
	INNER JOIN dbo.HCSOLINSD HD ON A.CODCONCEC = HD.CODCONCEC
	INNER JOIN dbo.HCPRESCRC hc ON hc.CODCONCEC = A.CODCONCEC
	INNER JOIN dbo.ADINGRESO ing ON ing.NUMINGRES = hc.NUMINGRES
	INNER JOIN dbo.INPACIENT pac ON pac.IPCODPACI = hd.IPCODPACI
	INNER JOIN dbo.IHLISTPRO PRO ON HD.CODPRODUC = PRO.CODPRODUC
	INNER JOIN dbo.INPROFSAL MED ON A.CODPROSAL = MED.CODPROSAL
	INNER JOIN dbo.INESPECIA esp ON esp.CODESPECI = med.CODESPEC1
	INNER JOIN dbo.INUNIFUNC FU ON A.UFUCODIGO = FU.UFUCODIGO AND (UFUTIPUNI='15' OR UFUTIPUNI='24')
	INNER JOIN
	(
		SELECT NUMINGRES, NUMEFOLIO
		FROM dbo.HCPRESCRD
		WHERE CODCONCEC = @CODCONCEC
		GROUP BY NUMINGRES, NUMEFOLIO
	) pre ON A.NUMINGRES = pre.NUMINGRES AND a.NUMEFOLIO = pre.NUMEFOLIO
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene el detalle completo de todos los medicamentos incluidos en una fórmula médica (receta), identificada por su código de conciliación (CODCONCEC). Combina las líneas de prescripción con el encabezado de la receta, el catálogo de productos farmacéuticos, los datos completos del paciente, el profesional de la salud prescriptor y su especialidad, el ingreso o admisión asociado y la unidad funcional donde se emitió la orden. Maneja dos escenarios mediante UNION ALL: prescripciones estándar registradas en HCPRESCRD/HCPRESCRC, y solicitudes de nutrición o servicios especiales registradas en HCSOLINSC/HCSOLINSD vinculadas a la misma receta. Se utiliza desde el módulo de inventario y farmacia para consultar los productos de una fórmula médica antes de su dispensación o validación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ListHCPRESCRDByCODCONCEC';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ListHCPRESCRDByCODCONCEC';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve, para un concepto de prescripción dado, los productos asociados combinando las prescripciones médicas de medicamentos y las solicitudes de insumos, enriquecidos con datos de paciente, profesional, especialidad, ingreso y unidad funcional.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ListHCPRESCRDByCODCONCEC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El @CODCONCEC debe existir en HCPRESCRD o en HCSOLINSC/HCSOLINSD para retornar filas.; Para el segundo bloque (solicitudes de insumos), debe existir al menos una prescripción en HCPRESCRD con el mismo CODCONCEC que comparta NUMINGRES y NUMEFOLIO con la solicitud.; El paciente, profesional (con especialidad CODESPEC1), ingreso, producto y unidad funcional deben existir en sus maestros para que la fila aparezca (joins INNER).; En el segundo bloque, la unidad funcional debe ser de tipo UFUTIPUNI=''15'' o ''24''.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ListHCPRESCRDByCODCONCEC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El segundo bloque siempre devuelve CODVIAADM='''', CODFORMED='''', DESADMINI='''', DOSISPRFN=0 y CODUNIMED='''' porque las solicitudes de insumos no manejan vía/forma/dosis de administración.; En el segundo bloque, FOLIOINIC se fija al NUMEFOLIO del encabezado de prescripción (no hay folio inicial propio para insumos).; CODUNIMED del primer bloque nunca es NULL: se sustituye por '''' cuando es nulo.; Solo se incluyen solicitudes de insumos cuya unidad funcional sea de tipo ''15'' o ''24''.; Solo se listan ítems cuyo producto exista en el catálogo IHLISTPRO (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ListHCPRESCRDByCODCONCEC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Fórmula médica; Prescripción de medicamentos; Solicitud de insumos; Paciente; Profesional de la salud; Especialidad médica; Ingreso/admisión; Unidad funcional; Producto farmacéutico; Vía de administración; Forma medicamentosa; Dosis; Folio de orden', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ListHCPRESCRDByCODCONCEC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Retorna UNION ALL de dos consultas: (1) detalle de medicamentos prescritos desde HCPRESCRD/HCPRESCRC; (2) detalle de insumos solicitados desde HCSOLINSC/HCSOLINSD vinculados al mismo concepto, NUMINGRES y NUMEFOLIO de una prescripción existente.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ListHCPRESCRDByCODCONCEC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCRD; dbo.HCPRESCRC; dbo.IHLISTPRO; dbo.INPACIENT; dbo.INPROFSAL; dbo.ADINGRESO; dbo.INESPECIA; dbo.INUNIFUNC; dbo.HCSOLINSC; dbo.HCSOLINSD', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ListHCPRESCRDByCODCONCEC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ListHCPRESCRDByCODCONCEC';
-- GO

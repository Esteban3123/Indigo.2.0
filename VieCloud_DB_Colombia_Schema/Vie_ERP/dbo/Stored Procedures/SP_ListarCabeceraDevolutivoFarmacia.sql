/**** 
SP para listar la cabecera para la creación del JSON para las devoluciones para UNIHEALTH
****/
CREATE PROCEDURE [dbo].[SP_ListarCabeceraDevolutivoFarmacia]
(
  @Consecutivo as Int
)
WITH RECOMPILE
AS
BEGIN
	SET NOCOUNT ON;
		
			SELECT STUFF(
    (SELECT DISTINCT TOP 3 ', ' + RTRIM(F.UFUCODIGO) FROM CHREGESTA C 
	INNER JOIN CHCAMASHO F ON C.CODICAMAS = F.CODICAMAS
	WHERE C.NUMINGRES = A.NUMINGRES
    FOR XML PATH ('')),
1,2, '')
				 AS 'COD UNIDAD FUNCIONAL',
				STUFF(
    (SELECT DISTINCT TOP 3 ', ' + RTRIM(I.UFUDESCRI) FROM CHREGESTA C 
	INNER JOIN CHCAMASHO F ON C.CODICAMAS = F.CODICAMAS 
	INNER JOIN INUNIFUNC I ON F.UFUCODIGO = I.UFUCODIGO 
	WHERE C.NUMINGRES = A.NUMINGRES
    FOR XML PATH ('')),
1,2, '') AS 'UNIDAD FUNCIONAL', RTRIM(A.CODBODEGA) AS 'COD BODEGA', RTRIM(B.DESBODEGA) AS 'BODEGA', A.CODCONCEC AS 'ID', 
				A.FECHDEVOL AS 'FECHA DEVOLUTIVO',RTRIM(A.NUMINGRES) AS 'INGRESO',A.IPCODPACI AS 'PACIENTE'
			FROM HCDEVMEDC A
				INNER JOIN IHBODEGAS B ON B.CODBODEGA = A.CODBODEGA
			WHERE A.CODCONCEC = @Consecutivo 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene la cabecera del comprobante de devolución de medicamentos a farmacia para un consecutivo de devolución específico, usado en la generación del JSON requerido por UNIHEALTH. Recupera los datos principales del encabezado: paciente, número de ingreso, fecha de devolución, bodega destino y las unidades funcionales (servicios o salas) por las que transitó el paciente durante ese ingreso, tomando esta información de las devoluciones de medicamentos (HCDEVMEDC), el catálogo de bodegas (IHBODEGAS), los registros de estancia y cama (CHREGESTA y CHCAMASHO) y el maestro de unidades funcionales (INUNIFUNC). Sirve como punto de entrada para construir documentos de devolución de medicamentos que se transmiten al sistema externo UNIHEALTH.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ListarCabeceraDevolutivoFarmacia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ListarCabeceraDevolutivoFarmacia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye la cabecera de un devolutivo de farmacia (bodega, ingreso, paciente, fecha y unidades funcionales asociadas a las camas del ingreso) para armar el JSON de devoluciones hacia UNIHEALTH.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarCabeceraDevolutivoFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro de devolutivo de farmacia con el consecutivo proporcionado.; La bodega referenciada por el devolutivo debe existir en el catálogo de bodegas.; Para resolver unidad funcional, el ingreso debe tener registros de estancia con cama asociada y la cama debe estar mapeada a una unidad funcional vigente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarCabeceraDevolutivoFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen hasta 3 unidades funcionales distintas asociadas al ingreso del devolutivo, concatenadas por coma.; El listado de unidades funcionales (código y descripción) se obtiene a partir de las camas asignadas al ingreso correspondiente del devolutivo.; Se filtra siempre por un único consecutivo de devolutivo, devolviendo a lo sumo una fila de cabecera.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarCabeceraDevolutivoFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Devolución de farmacia; Unidad funcional; Bodega; Ingreso hospitalario; Paciente; Cama hospitalaria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarCabeceraDevolutivoFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCDEVMEDC: Cuando HCDEVMEDC.CODCONCEC coincide con el consecutivo recibido, retorna una fila con datos de la cabecera del devolutivo y, agregadas vía FOR XML PATH, hasta las 3 primeras unidades funcionales distintas (código y descripción) derivadas de las camas del ingreso (NUMINGRES) en CHREGESTA→CHCAMASHO→INUNIFUNC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarCabeceraDevolutivoFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCDEVMEDC; dbo.IHBODEGAS; dbo.CHREGESTA; dbo.CHCAMASHO; dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarCabeceraDevolutivoFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarCabeceraDevolutivoFarmacia';
-- GO

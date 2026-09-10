
CREATE PROCEDURE [dbo].[SPREP_CH_EstanciaPaciente]
(
@CodigoPaciente Varchar(25),
@NumeroIngreso Char(10),
@UnidadFuncional Char(10),
@CodigoCamas int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @CodigoPaciente <> 'NOT NULL' 
        -- Insert statements for procedure here
        SELECT FECINIEST AS 'FECHA INICIAL DE ESTANCIA', A.CODICAMAS AS 'CODIGO DE LA CAMA', RTRIM(A.CODICAMAS) + ' - ' + DESCCAMAS AS 'DESCRIPCION DE LA CAMA', 
        DESTIPEST AS 'TIPO DE ESTANCIA', UFUDESCRI AS 'UNIDAD FUNCIONAL'
        FROM CHREGESTA A WITH(NOLOCK)
        INNER JOIN CHCAMASHO B WITH(NOLOCK) ON A.CODICAMAS=B.CODICAMAS
        INNER JOIN CHTIPESTA C WITH(NOLOCK) ON A.CODTIPEST=C.CODTIPEST
        INNER JOIN INUNIFUNC D WITH(NOLOCK) ON B.UFUCODIGO=D.UFUCODIGO
        WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND B.UFUCODIGO=@UnidadFuncional AND A.CODICAMAS=@CodigoCamas

     ELSE
	    SELECT TOP(1) FECINIEST AS 'FECHA INICIAL DE ESTANCIA', A.CODICAMAS AS 'CODIGO DE LA CAMA', RTRIM(A.CODICAMAS) + ' - ' + DESCCAMAS AS 'DESCRIPCION DE LA CAMA', 
        DESTIPEST AS 'TIPO DE ESTANCIA', UFUDESCRI AS 'UNIDAD FUNCIONAL'
        FROM CHREGESTA A WITH(NOLOCK)
        INNER JOIN CHCAMASHO B WITH(NOLOCK) ON A.CODICAMAS=B.CODICAMAS
        INNER JOIN CHTIPESTA C WITH(NOLOCK) ON A.CODTIPEST=C.CODTIPEST
        INNER JOIN INUNIFUNC D WITH(NOLOCK) ON B.UFUCODIGO=D.UFUCODIGO
        WHERE A.IPCODPACI='' AND A.NUMINGRES='' AND B.UFUCODIGO='' AND A.CODICAMAS=''

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta el historial de estancias de un paciente durante un ingreso hospitalario, mostrando para cada estancia la fecha de inicio, la cama asignada con su descripción, el tipo de estado o estancia (urgencias, hospitalización, etc.) y la unidad funcional donde se ubicó. Recibe como parámetros la cédula del paciente, el número de ingreso, la unidad funcional y el código de cama; si se proporciona un código de paciente válido, filtra el resultado exacto, de lo contrario retorna un registro vacío de referencia. Integra los registros históricos de estancias (CHREGESTA), el maestro de camas (CHCAMASHO), el catálogo de tipos de estado (CHTIPESTA) y el catálogo de unidades funcionales (INUNIFUNC). Se usa principalmente para reportes clínicos y seguimiento de la trayectoria de hospitalización del paciente a lo largo de un ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_CH_EstanciaPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_CH_EstanciaPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta la estancia hospitalaria de un paciente (fecha de inicio, cama, tipo de estancia y unidad funcional) para un ingreso, unidad y cama específicos, con un modo alternativo de retorno vacío de referencia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_EstanciaPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cadena literal ''NOT NULL'' se usa como bandera para distinguir consulta real vs. consulta de referencia vacía; Deben existir relaciones válidas entre estancia, cama, tipo de estancia y unidad funcional para que un registro sea retornado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_EstanciaPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las consultas siempre cruzan estancia con cama, tipo de estancia y unidad funcional mediante INNER JOIN, por lo que solo se devuelven estancias con catálogos completos y consistentes; Todas las lecturas se realizan con NOLOCK (lecturas sucias permitidas); La descripción de cama siempre se entrega como concatenación ''CODICAMAS - DESCCAMAS'' con RTRIM sobre el código; El procedimiento nunca modifica datos; es de solo lectura', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_EstanciaPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Estancia; Cama hospitalaria; Tipo de estancia; Unidad funcional; Hospitalización', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_EstanciaPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.CHREGESTA: Cuando el código de paciente es distinto de ''NOT NULL'', retorna las estancias que cumplen IPCODPACI, NUMINGRES, UFUCODIGO y CODICAMAS recibidos; [RETURN_RESULT] dbo.CHREGESTA: Cuando el código de paciente es igual a ''NOT NULL'', retorna TOP 1 fila filtrando por valores vacíos en paciente, ingreso, unidad funcional y cama (resultado prácticamente vacío de referencia)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_EstanciaPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El código de paciente recibido es distinto del literal ''NOT NULL'' → Retorna las estancias filtradas por paciente, ingreso, unidad funcional y cama indicados else Retorna TOP 1 registro usando filtros vacíos ('''') como caso por defecto/fallback de referencia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_EstanciaPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHREGESTA; dbo.CHCAMASHO; dbo.CHTIPESTA; dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_EstanciaPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_EstanciaPaciente';
-- GO

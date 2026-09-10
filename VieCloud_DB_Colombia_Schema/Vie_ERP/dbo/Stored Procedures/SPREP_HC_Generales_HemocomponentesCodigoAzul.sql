CREATE PROCEDURE [dbo].[SPREP_HC_Generales_HemocomponentesCodigoAzul]
(
    @CodigoPaciente VARCHAR(25),
    @NumeroIngreso CHAR(25),
    @NumeroCodigoAzul NVARCHAR(500) -- Acepta una lista de valores
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SQL NVARCHAR(MAX)

	 SET @SQL = '
    SELECT 
        HCORHEMCOID,
        COM.DESCOMSAM, 
        COUNT(*) AS CANTIDAD,
        A.NUMCODAZU AS ''CONSECUTIVO DEL CODIGO AZUL'',
		A.IPCODPACI, 
		A.CODCENATE, 
		A.UFUCODIGO
    FROM HCORHEMBOL BOLSA 
    INNER JOIN HCCOMSAN COM ON COM.ID = BOLSA.COMSAMID 
    INNER JOIN HCORHEMCO HEM ON BOLSA.HCORHEMCOID = HEM.ID
    INNER JOIN HCCODAZUC A ON HEM.NUMEFOLIO = A.NUMCODAZU 
                           AND HEM.IPCODPACI = A.IPCODPACI 
                           AND HEM.NUMINGRES = A.NUMINGRES 
                           AND HEM.IDETIPHIS = ''CODIGOAZU''
    WHERE A.NUMCODAZU IN (' + @NumeroCodigoAzul + ')
      AND A.IPCODPACI = ''' + @CodigoPaciente + '''
      AND A.NUMINGRES = ''' + @NumeroIngreso + '''
    GROUP BY A.IPCODPACI,A.CODCENATE,A.UFUCODIGO,COM.DESCOMSAM, A.NUMCODAZU, HCORHEMCOID ORDER BY A.NUMCODAZU DESC;'

    EXEC(@SQL) 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de hemocomponentes (bolsas de sangre y sus componentes) asociados a uno o varios eventos de Código Azul en historia clínica. Recibe como parámetros la cédula del paciente, el número de ingreso hospitalario y uno o más números de Código Azul (lista separada por comas), y devuelve el detalle agrupado de cada tipo de componente sanguíneo con su cantidad utilizada, junto con el consecutivo del Código Azul, el centro de atención y la unidad funcional. Se utiliza para consultar y auditar el consumo de hemocomponentes durante eventos críticos de Código Azul dentro de la historia clínica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_HemocomponentesCodigoAzul';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_HemocomponentesCodigoAzul';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta el conteo de hemocomponentes (bolsas) transfundidos asociados a uno o varios eventos de Código Azul de un paciente en un ingreso específico, agrupado por tipo de componente sanguíneo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_HemocomponentesCodigoAzul';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y el número de ingreso deben existir y tener registros en HCCODAZUC; Los folios de Código Azul indicados deben corresponder a registros de hemocomponentes con IDETIPHIS=''CODIGOAZU'' en HCORHEMCO; La lista de números de Código Azul debe venir formateada con comillas/comas válidas para inyectarse en la cláusula IN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_HemocomponentesCodigoAzul';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se contabilizan hemocomponentes cuyo origen tipificado (IDETIPHIS) corresponde a ''CODIGOAZU''; El vínculo entre hemocomponente y evento se establece por triple coincidencia: folio=NUMCODAZU, paciente y número de ingreso; Los resultados se ordenan por consecutivo de Código Azul en forma descendente (más reciente primero)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_HemocomponentesCodigoAzul';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión; Código Azul; Hemocomponente; Bolsa de sangre; Componente sanguíneo; Centro de atención; Unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_HemocomponentesCodigoAzul';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCORHEMBOL: Devuelve cantidad de bolsas (COUNT(*)) por componente sanguíneo y por evento de Código Azul cuando A.NUMCODAZU está en la lista, A.IPCODPACI y A.NUMINGRES coinciden, y HEM.IDETIPHIS=''CODIGOAZU''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_HemocomponentesCodigoAzul';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORHEMBOL; dbo.HCCOMSAN; dbo.HCORHEMCO; dbo.HCCODAZUC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_HemocomponentesCodigoAzul';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_HemocomponentesCodigoAzul';
-- GO

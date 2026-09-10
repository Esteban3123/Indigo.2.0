
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_MezclasyLiquidosInternos]
(
@CodigoPaciente Varchar(25),
@NumeroFolio nChar(10),
@NumeroIngreso Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
          
SELECT CODCONCEC AS CONSECUTIVO,RTRIM(MEZLIQPAC)AS 'MEZCLAS Y LIQUIDOS',RTRIM(ADMMEZLIQ) AS 'ADMINISTRACION MEZCLAS Y LIQUIDOS',RTRIM(INDAPLMED) AS 'INDICACIONES DE APLICACION',CASE WHEN FOLIOINIC=NUMEFOLIO THEN 'N' WHEN TRATMODIF='1' THEN 'M' ELSE '' END AS LEYENDA,CODCENATE,UFUCODIGO,IPCODPACI
                   
FROM HCINFLIDI WITH(NOLOCK)
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMEFOLIO=@NumeroFolio AND IDETIPHIS<>'CODIGOAZU'

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera las mezclas y líquidos intravenosos prescritos a un paciente en su historia clínica, filtrando por cédula del paciente, número de ingreso y número de folio. Consulta la tabla HCINFLIDI para obtener el detalle de cada mezcla o líquido (nombre, instrucciones de administración, indicaciones de aplicación) junto con una leyenda que indica si el registro es nuevo (''N'') o fue modificado (''M'') respecto al folio inicial del tratamiento. Se usa principalmente en la visualización e impresión del folio de historia clínica en la sección de terapia intravenosa, excluyendo registros de tipo ''CODIGOAZU''.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_MezclasyLiquidosInternos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_MezclasyLiquidosInternos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recuperar las mezclas y líquidos internos administrados a un paciente en un ingreso y folio específicos, indicando si el registro es inicial o modificado, para reportes de historia clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir información de mezclas y líquidos asociada al paciente, ingreso y folio indicados; Los registros deben tener un tipo de historia distinto de ''CODIGOAZU'' para ser considerados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven registros del paciente, ingreso y folio especificados; Se excluyen siempre los registros cuyo tipo de historia sea ''CODIGOAZU''; La lectura se realiza con NOLOCK (lecturas sucias permitidas); La leyenda solo puede tomar tres valores: ''N'' (nuevo), ''M'' (modificado) o vacío', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Folio de historia clínica; Ingreso del paciente; Mezclas y líquidos; Administración de mezclas y líquidos; Indicaciones de aplicación; Centro de atención; Unidad funcional; Tratamiento modificado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCINFLIDI: Cuando IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMEFOLIO=@NumeroFolio AND IDETIPHIS<>''CODIGOAZU'' → devuelve consecutivo, mezclas y líquidos, su administración, indicaciones de aplicación, leyenda (N/M/vacío), centro de atención, unidad funcional y código del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si FOLIOINIC = NUMEFOLIO → Marca la leyenda como ''N'' (registro inicial/nuevo en ese folio) else Si TRATMODIF=''1'' marca leyenda como ''M'' (tratamiento modificado); en cualquier otro caso, leyenda vacía', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCINFLIDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MezclasyLiquidosInternos';
-- GO

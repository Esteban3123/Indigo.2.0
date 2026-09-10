-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,24-07-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas de IAAS Qx>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CAL_ListarFichaIAASQx]
(
  @IdFicha as Int
)
AS
BEGIN
  SET NOCOUNT ON;

Select Case B.TIPOIDENTIFICA when 1 then 'X' END AS 'Cédula',CASE B.TIPOIDENTIFICA when 2 then 'X' END AS 'Extranjera',CASE B.TIPOIDENTIFICA when 3 then 'X' END AS 'Tarjeta identidad',CASE B.TIPOIDENTIFICA when 4 then 'X' END AS 'Registro Civil',CASE B.TIPOIDENTIFICA when 5 then 'X' END AS 'Pasaporte',CASE B.TIPOIDENTIFICA when 6 then 'X' END AS 'Adulto sin ID',CASE B.TIPOIDENTIFICA when 7 then 'X' END AS 'Menor sin ID',CASE B.TIPOIDENTIFICA when 8 then 'X' END AS 'NU',
       RTRIM(B.NOMBRECOMP) AS 'Nombre Completo',RTRIM(B.IPCODPACI) AS 'Numero Identificacion',
	   Case A.COMPLEJIDAD WHEN 1 then 'X' end as 'Baja',Case A.COMPLEJIDAD WHEN 2 then 'X' end as 'Media',Case A.COMPLEJIDAD WHEN 3 then 'X' end as 'Alta',
	   case A.SERVICIOPROCEDIMIENTO when 1 then 'X' end as 'Programado ambulatorio',case A.SERVICIOPROCEDIMIENTO when 2 then 'X' end as 'Urgencias',case A.SERVICIOPROCEDIMIENTO when 3 then 'X' end as 'Programado hospitalizado',
	   case A.PROCEDIMIENTOMEDICO when 1 then 'X' end as 'Cesárea',case A.PROCEDIMIENTOMEDICO when 2 then 'X' end as 'Herniorrafia',case A.PROCEDIMIENTOMEDICO when 3 then 'X' end as 'Parto',case A.PROCEDIMIENTOMEDICO when 4 then 'X' end as 'Revascularización',case A.PROCEDIMIENTOMEDICO when 5 then 'X' end as 'Colecistectomía',case A.PROCEDIMIENTOMEDICO when 6 then 'X' end as 'Otro Procedimiento medico',
	   case A.DIABETES WHEN 1 THEN 'X' END AS 'Diabetes',
	   case A.INMUNOSUPRESION WHEN 1 THEN 'X' END AS 'Inmunosupresión',
	   case A.OBESIDAD WHEN 1 THEN 'X' END AS 'Obesidad',
	   case A.DESNUTRICION WHEN 1 THEN 'X' END AS 'Desnutrición',
	   case A.PRECLAMPSIA WHEN 1 THEN 'X' END AS 'Preeclampsia',
	   case A.ANEMIA WHEN 1 THEN 'X' END AS 'Anemia',
	   case A.CLASIFICACIONASA WHEN 1 THEN 'X' END AS 'ASA 1',
	   case A.CLASIFICACIONASA WHEN 2 THEN 'X' END AS 'ASA 2',
	   case A.CLASIFICACIONASA WHEN 3 THEN 'X' END AS 'ASA 3',
	   case A.CLASIFICACIONASA WHEN 4 THEN 'X' END AS 'ASA 4',
	   case A.CLASIFICACIONASA WHEN 5 THEN 'X' END AS 'ASA 5',
	   case A.TIPOHERIDA WHEN 1 THEN 'X' END AS 'Limpia',
	   case A.TIPOHERIDA WHEN 2 THEN 'X' END AS 'Limpia contaminada',
	   case A.TIPOHERIDA WHEN 3 THEN 'X' END AS 'Herida contaminada ',
	   case A.TIPOHERIDA WHEN 4 THEN 'X' END AS 'Herida sucia',
	   Rtrim(A.DURACIONPROCEDIMIENTO) AS 'Duracion',
	   case A.SUPERFICIEPRIMARIA WHEN 1 THEN 'X' END AS 'Superficial primaria',
	   case A.SUPERFICIESECUNDAR WHEN 1 THEN 'X' END AS 'Superficial secundaria ',
	   case A.PROFUNDAPRIMARIA WHEN 1 THEN 'X' END AS 'Profunda primaria',
	   case A.PROFUNDASECUNDARIA WHEN 1 THEN 'X' END AS 'Profunda secundaria',
	   case A.ORGANOESPACIO WHEN 1 THEN 'X' END AS 'Organo espacio',
       case A.PROFIAXISANTIBIOTICA when 1 then 'X' end as 'Si Profilaxis',case A.PROFIAXISANTIBIOTICA when 0 then 'X' end as 'No Profilaxis',
	   Rtrim(A.CUAL) AS 'Cual Profilaxis',
	   case A.TIEMPOANTIBIOTICO WHEN 1 THEN 'X' END AS 'Antes',
	   case A.TIEMPOANTIBIOTICO WHEN 2 THEN 'X' END AS 'Durante',
	   case A.TIEMPOANTIBIOTICO WHEN 3 THEN 'X' END AS 'Después',
	   case A.TIEMPOANTIBIOTICO WHEN 4 THEN 'X' END AS 'Ninguna',
	   case A.REQUIRIONUEVAINTERVEN when 1 then 'X' end as 'Si requierio intervencion',case A.REQUIRIONUEVAINTERVEN when 0 then 'X' end as 'No requierio intervencion',
	   convert(varchar(10), FECHATOMAMUESTRA1,103) as 'Fecha Toma 1',
	   convert(varchar(10),FECHATOMAMUESTRA2,103) as 'Fecha Toma 2',
	   convert(varchar(10),FECHATOMAMUESTRA3,103) as 'Fecha Toma 3',
	   RTRIM(A.CODIGOMUESTRA1) as 'Codigo Muestra 1',
	   RTRIM(A.CODIGOMUESTRA2) as 'Codigo Muestra 2',
	   RTRIM(A.CODIGOMUESTRA3) as 'Codigo Muestra 3',
	   RTRIM(A.CODIGOPRUEBA1) as 'Codigo Prueba 1',
	   RTRIM(A.CODIGOPRUEBA2) as 'Codigo Prueba 2',
	   RTRIM(A.CODIGOPRUEBA3) as 'Codigo Prueba 3',
	   RTRIM(A.MICROORGANISMOAISLADO1) as 'MicroOrganismos 1',
	   RTRIM(A.MICROORGANISMOAISLADO2) as 'MicroOrganismos 2',
	   RTRIM(A.MICROORGANISMOAISLADO3) as 'MicroOrganismos 3'
from [dbo].[CALIAASQX] A 
Inner Join dbo.CALREPORTE B on A.IDCALREPORTE = B.ID
where A.IDCALREPORTE = @IdFicha

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el detalle completo de una ficha de vigilancia de Infección Asociada a la Atención en Salud de tipo Quirúrgico (IAAS-Qx) a partir de su identificador. Combina los datos del reporte de evento adverso (paciente, tipo de documento, nombre, número de identificación/cédula) con los factores de riesgo del paciente (diabetes, obesidad, inmunosupresión, desnutrición, preeclampsia, anemia), las características del procedimiento quirúrgico (complejidad, servicio, tipo de procedimiento como cesárea o colecistectomía, clasificación ASA, tipo de herida, duración), la clasificación de la infección del sitio quirúrgico (superficial primaria/secundaria, profunda, órgano-espacio), el uso de profilaxis antibiótica (si/no, cuál antibiótico, momento de administración) y los resultados microbiológicos de hasta tres muestras (fecha de toma, código de muestra, código de prueba y microorganismo aislado). Se usa en el módulo de calidad asistencial para imprimir o consultar el formulario individual de seguimiento de ISQ, generalmente utilizado por el equipo de epidemiología o seguridad del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CAL_ListarFichaIAASQx';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CAL_ListarFichaIAASQx';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los datos de una ficha de vigilancia de infección asociada a la atención en salud quirúrgica (IAAS Qx) en formato apto para impresión, marcando con ''X'' las opciones seleccionadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaIAASQx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en CALIAASQX cuyo IDCALREPORTE coincida con el identificador recibido; Debe existir el reporte padre en CALREPORTE relacionado por ID con CALIAASQX.IDCALREPORTE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaIAASQx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada grupo de categorías es mutuamente excluyente: solo una opción queda marcada con ''X'' por grupo; Las fechas de toma de muestra se presentan en formato dd/MM/yyyy (estilo 103); Los textos se entregan sin espacios finales (RTRIM); La consulta opera sobre una única ficha (filtrada por IDCALREPORTE) y por tanto retorna a lo sumo una fila', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaIAASQx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Infección Asociada a la Atención en Salud (IAAS); Infección de sitio quirúrgico; Clasificación ASA (riesgo anestésico); Tipo de herida quirúrgica; Profilaxis antibiótica; Comorbilidades (diabetes, inmunosupresión, obesidad, desnutrición, preeclampsia, anemia); Tipos de identificación del paciente; Complejidad del procedimiento; Procedimientos quirúrgicos (cesárea, herniorrafia, parto, revascularización, colecistectomía); Muestras y pruebas microbiológicas; Microorganismos aislados; Reintervención quirúrgica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaIAASQx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.CALIAASQX: Cuando IDCALREPORTE coincide con el parámetro, retorna fila combinada con CALREPORTE traduciendo códigos a marcas ''X'' por categoría', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaIAASQx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPOIDENTIFICA del paciente (1..8) → Marca ''X'' en la columna correspondiente: Cédula, Extranjera, Tarjeta identidad, Registro Civil, Pasaporte, Adulto sin ID, Menor sin ID o NU; si COMPLEJIDAD (1..3) → Marca ''X'' en Baja, Media o Alta; si SERVICIOPROCEDIMIENTO (1..3) → Marca ''X'' en Programado ambulatorio, Urgencias o Programado hospitalizado; si PROCEDIMIENTOMEDICO (1..6) → Marca ''X'' en Cesárea, Herniorrafia, Parto, Revascularización, Colecistectomía u Otro Procedimiento medico; si CLASIFICACIONASA (1..5) → Marca ''X'' en ASA 1 a ASA 5 según el valor; si TIPOHERIDA (1..4) → Marca ''X'' en Limpia, Limpia contaminada, Herida contaminada o Herida sucia; si PROFIAXISANTIBIOTICA (1/0) → Marca ''X'' en Si Profilaxis o No Profilaxis; si TIEMPOANTIBIOTICO (1..4) → Marca ''X'' en Antes, Durante, Después o Ninguna; si REQUIRIONUEVAINTERVEN (1/0) → Marca ''X'' en Si/No requirió intervención; si Banderas clínicas =1 (DIABETES, INMUNOSUPRESION, OBESIDAD, DESNUTRICION, PRECLAMPSIA, ANEMIA) → Marca ''X'' en la comorbilidad correspondiente; si Tipo de infección de sitio quirúrgico =1 (SUPERFICIEPRIMARIA, SUPERFICIESECUNDAR, PROFUNDAPRIMARIA, PROFUNDASECUNDARIA, ORGANOESPACIO) → Marca ''X'' en la clasificación correspondiente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaIAASQx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CALIAASQX; dbo.CALREPORTE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaIAASQx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaIAASQx';
-- GO

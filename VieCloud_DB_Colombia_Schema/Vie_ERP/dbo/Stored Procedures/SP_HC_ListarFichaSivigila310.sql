-- Stored Procedure
-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,26-11-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila310]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;  

			Select
				CASE A.CARNEVACU  WHEN '1' THEN 'X' END AS 'CARNEVACU si',CASE A.CARNEVACU WHEN '0' THEN 'X' END AS 'CARNEVACU no',
				CASE A.VACUFIEAMA  WHEN '1' THEN 'X' END AS 'VACUFIEAMA Si',CASE A.VACUFIEAMA WHEN '2' THEN 'X' END AS 'VACUFIEAMA No', CASE A.VACUFIEAMA WHEN '3' THEN 'X' END AS 'Desconocido',
				convert(varchar(10),A.FECHAAPLI,103) As 'FECHAAPLI', convert(varchar(10),A.FECHATOMA,103) As 'FECHATOMA', convert(varchar(10),A.FECHARECE,103) As 'FECHARECE',
				convert(varchar(10),A.FECHARESUL,103) As 'FECHARESUL', Rtrim(A.VALOR) As 'VALOR',
				CASE A.CASOFIEAMA  WHEN '1' THEN 'X' END AS 'Selvático',CASE A.CASOFIEAMA WHEN '0' THEN 'X' END AS 'Urbano',
				CASE A.MUESTRA  WHEN '1' THEN 'Sangre Total' WHEN '2' THEN 'Tejido' WHEN '3' THEN 'Suero' END AS 'MUESTRA',
				CASE A.PRUEBA  WHEN '1' THEN 'PCR' WHEN '2' THEN 'Aislamiento' WHEN '3' THEN 'TGO' WHEN '4' THEN'TGP' WHEN '5' THEN 'Bilirrubina Total' WHEN '6' THEN 'Bilirrubina Directa'
				WHEN '7' THEN 'Bilirrubina Indirecta' WHEN '8' THEN 'Creatinina' WHEN '9' THEN 'BUN' WHEN '10' THEN 'Parología' WHEN '11' THEN 'Estudio Directo' WHEN '12' THEN 'Elisa'
				WHEN '13' THEN 'Tiempo de Protombina' WHEN '14' THEN 'Tiempo Parcial' END AS 'PRUEBA',
				CASE A.AGENTE  WHEN '1' THEN 'Fiebre Amarilla' END AS 'AGENTE',
				CASE A.RESULTADO  WHEN '1' THEN 'Positivo' WHEN '2' THEN 'Negativo' WHEN '3' THEN 'No Procesado' WHEN '4' THEN 'Inadecuado' WHEN '5' THEN 'Valor Registrado' END AS 'RESULTADO',
				CASE A.CEFALEA  WHEN '1' THEN 'X' END AS 'CEFALEA',CASE A.VOMITO WHEN '1' THEN 'X' END AS 'VOMITO', CASE A.ICTERICIA WHEN '1' THEN 'X' END AS 'ICTERICIA',
				CASE A.FIEBRE  WHEN '1' THEN 'X' END AS 'FIEBRE',CASE A.MIALGIAS WHEN '1' THEN 'X' END AS 'MIALGIAS', CASE A.ARTRALGIAS WHEN '1' THEN 'X' END AS 'ARTRALGIAS',
				CASE A.SFAGET  WHEN '1' THEN 'X' END AS 'SFAGET',CASE A.OLIGURIA WHEN '1' THEN 'X' END AS 'OLIGURIA', CASE A.CHOQUEXSHOCK WHEN '1' THEN 'X' END AS 'CHOQUEXSHOCK',
				CASE A.BRADICARDIA  WHEN '1' THEN 'X' END AS 'BRADICARDIA',CASE A.FALLARENAL WHEN '1' THEN 'X' END AS 'FALLARENAL', CASE A.FALLAHEPA WHEN '1' THEN 'X' END AS 'FALLAHEPA',
				CASE A.HEPATOMEGALIA  WHEN '1' THEN 'X' END AS 'HEPATOMEGALIA',				
				A.VERSION AS 'VERSION', 
				A.JSON AS 'JSON', 
				CASE TRY_CAST( JSON_VALUE(A.JSON,'$.SH_HEMOPTISIS') AS BIT) WHEN 1 THEN 'X' END AS 'Hemoptisis', 
				CASE TRY_CAST( JSON_VALUE(A.JSON,'$.SH_HIPEREMIA') AS BIT) WHEN 1 THEN 'X' END AS 'Hiperemia', 
				CASE TRY_CAST( JSON_VALUE(A.JSON,'$.SH_HEMATEMESIS') AS BIT) WHEN 1 THEN 'X' END AS 'Hematemesis', 
				CASE TRY_CAST( JSON_VALUE(A.JSON,'$.SH_PETEQUIAS') AS BIT) WHEN 1 THEN 'X' END AS 'Petequias', 
				CASE TRY_CAST( JSON_VALUE(A.JSON,'$.SH_METRORRAGIA') AS BIT) WHEN 1 THEN 'X' END AS 'Metrorragia', 
				CASE TRY_CAST( JSON_VALUE(A.JSON,'$.SH_MELENAS') AS BIT) WHEN 1 THEN 'X' END AS 'Melenas', 
				CASE TRY_CAST( JSON_VALUE(A.JSON,'$.SH_EQUIMOSIS') AS BIT) WHEN 1 THEN 'X' END AS 'Equimosis', 
				CASE TRY_CAST( JSON_VALUE(A.JSON,'$.SH_EPISTAXIS') AS BIT) WHEN 1 THEN 'X' END AS 'Epistaxis', 
				CASE TRY_CAST( JSON_VALUE(A.JSON,'$.SH_HEMATURIA') AS BIT) WHEN 1 THEN 'X' END AS 'Hematuria', 

				CASE WHEN ',' + JSON_VALUE(A.JSON, '$.OTROS_SIGNOS') + ',' LIKE '%,1,%' THEN 'X' END AS [Dolor abdominal],
				CASE WHEN ',' + JSON_VALUE(A.JSON, '$.OTROS_SIGNOS') + ',' LIKE '%,2,%' THEN 'X' END AS [Diarrea],
				CASE WHEN ',' + JSON_VALUE(A.JSON, '$.OTROS_SIGNOS') + ',' LIKE '%,3,%' THEN 'X' END AS [Nauseas],
				JSON_VALUE(JSON, '$.PAIS_INFECCION') AS [Pais infeccion],  
				CASE WHEN U.AUUBICACI IS NOT NULL THEN RTRIM(O.depcodigo) END as [Departamento infeccion],
				CASE WHEN U.AUUBICACI IS NOT NULL THEN RTRIM(R.MUNCODIGO) END as [Municipio infeccion],
				CASE  WHEN TRY_CAST(JSON_VALUE(A.JSON, '$.Familiar_Sintomatico') AS BIT) = 1 THEN 'X' END AS [Familiar positivo],
				CASE WHEN TRY_CAST(JSON_VALUE(A.JSON, '$.Familiar_Sintomatico') AS BIT) = 0 THEN 'X' END AS [Familiar negativo],
			    CASE  WHEN TRY_CAST(JSON_VALUE(A.JSON, '$.Desplazamiento') AS BIT) = 1 THEN 'X' END AS [Desplazamiento positivo],
				CASE WHEN TRY_CAST(JSON_VALUE(A.JSON, '$.Desplazamiento') AS BIT) = 0 THEN 'X'END AS [Desplazamiento negativo],
				JSON_VALUE(JSON, '$.PAIS_DESPLAZAMIENTO') AS [Pais desplazamiento],  
				CASE WHEN D.AUUBICACI IS NOT NULL THEN RTRIM(DD.depcodigo) END as [Departamento desplazamiento],
				CASE WHEN D.AUUBICACI IS NOT NULL THEN RTRIM(MD.MUNCODIGO) END as [Municipio desplazamiento],
				JSON_VALUE(JSON, '$.VEREDA_DESPLAZAMIENTO') AS [Vereda desplazamiento],  

				CASE WHEN U.AUUBICACI IS NOT NULL THEN CONCAT( RTRIM(Q.StandardCode), '-', RTRIM(Q.Name), ' / ', RTRIM(O.depcodigo), '-', RTRIM(O.nomdepart), ' / ', RTRIM(R.MUNCODIGO), '-', RTRIM(R.MUNNOMBRE) ) END as 'SITIOPROBA'

			From 
				HCFICHA310 A
				Left Join INUBICACI U on JSON_VALUE(A.JSON,'$.CODSITIO_INFECCION') = U.AUUBICACI 
				Left Join INMUNICIP R on U.DEPMUNCOD = R.DEPMUNCOD 
				Left Join INDEPARTA O on R.DEPCODIGO = O.DEPCODIGO 
				Left Join Common.Country Q ON O.IDPAIS = Q.Id 

				Left Join INUBICACI D on JSON_VALUE(A.JSON,'$.CODSITIO_DESPLAZAMIENTO') = D.AUUBICACI 
				Left Join INMUNICIP MD on D.DEPMUNCOD = MD.DEPMUNCOD 
                Left Join INDEPARTA DD on MD.DEPCODIGO = DD.DEPCODIGO

			Where 
				A.IDFICHANOTIFICACION  = @IdFicha 
				AND ISJSON(A.JSON) > 0

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el detalle completo de una ficha de notificación epidemiológica de fiebre amarilla (código SIVIGILA 310) a partir de su identificador o número de ficha. Consolida los datos clínicos del caso: síntomas presentes (fiebre, cefalea, ictericia, vómito, mialgias, artralgias, hemorragias, entre otros), antecedentes de vacunación contra fiebre amarilla (carné de vacunación, fecha de aplicación), información de laboratorio (tipo de muestra, prueba realizada, agente, resultado y fechas de toma, recepción y resultado), clasificación del caso (selvático o urbano) y el sitio probable de infección geográfico compuesto a partir del país, departamento y municipio. Los datos geográficos del sitio de infección se obtienen cruzando el código de ubicación almacenado en el campo JSON de la ficha con los catálogos maestros de ubicaciones, municipios, departamentos y países. Es utilizado por la interfaz clínica y epidemiológica para visualizar o imprimir la ficha oficial SIVIGILA 310 de un paciente sospechoso o confirmado de fiebre amarilla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila310';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila310';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la información detallada de una ficha de notificación SIVIGILA (sintomatología, vacunación, muestras/pruebas, resultados y datos geográficos de infección/desplazamiento) para impresión o visualización.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila310';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La ficha debe existir en HCFICHA310 con el identificador suministrado.; El campo JSON de la ficha debe contener un JSON válido (ISJSON > 0); en caso contrario no se retorna fila.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila310';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan fichas cuyo campo JSON sea un JSON válido.; Las fechas se entregan formateadas en estilo 103 (dd/mm/yyyy).; Los signos/síntomas y banderas se representan como ''X'' cuando el código almacenado es ''1''.; Los campos derivados de JSON usan TRY_CAST y JSON_VALUE, por lo que valores no parseables resultan en NULL en lugar de error.; La detección de OTROS_SIGNOS asume lista separada por comas y se compara envolviendo con comas para coincidencia exacta de cada código.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila310';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación SIVIGILA; Fiebre amarilla (caso selvático/urbano); Vacunación y carné de vacunación; Muestras de laboratorio (sangre, tejido, suero); Pruebas diagnósticas (PCR, Elisa, TGO/TGP, bilirrubinas, creatinina, BUN, patología, tiempos de coagulación); Signos y síntomas clínicos (cefalea, vómito, ictericia, fiebre, mialgias, artralgias, oliguria, choque, bradicardia, falla renal/hepática, hepatomegalia, hemorragias); Sitio probable de infección y desplazamiento (país, departamento, municipio, vereda); Antecedente familiar sintomático; Desplazamiento epidemiológico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila310';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA310: Cuando IDFICHANOTIFICACION = @IdFicha y el campo JSON es válido (ISJSON>0), retorna un resultset con datos clínicos, muestras, pruebas y ubicaciones de infección/desplazamiento.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila310';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CARNEVACU = ''1'' / ''0'' → Marca ''X'' en columna ''CARNEVACU si'' o ''CARNEVACU no'' respectivamente.; si VACUFIEAMA = ''1'' / ''2'' / ''3'' → Marca ''Si'', ''No'' o ''Desconocido'' para vacunación de fiebre amarilla.; si CASOFIEAMA = ''1'' / ''0'' → Clasifica el caso como ''Selvático'' o ''Urbano''.; si MUESTRA in (1,2,3) → Traduce a ''Sangre Total'', ''Tejido'' o ''Suero''.; si PRUEBA in (1..14) → Traduce el código a su nombre clínico (PCR, Aislamiento, TGO, TGP, Bilirrubinas, Creatinina, BUN, Patología, Estudio Directo, Elisa, Tiempos de coagulación).; si AGENTE = ''1'' → Identifica el agente como ''Fiebre Amarilla''.; si RESULTADO in (1..5) → Traduce a ''Positivo'', ''Negativo'', ''No Procesado'', ''Inadecuado'' o ''Valor Registrado''.; si JSON.OTROS_SIGNOS contiene los códigos 1, 2 o 3 (lista CSV) → Marca ''X'' en Dolor abdominal, Diarrea o Nauseas según corresponda.; si JSON.Familiar_Sintomatico = 1/0 (BIT) → Marca familiar positivo o negativo.; si JSON.Desplazamiento = 1/0 (BIT) → Marca desplazamiento positivo o negativo.; si Existe ubicación de infección (U.AUUBICACI no nulo) → Expone Departamento, Municipio y SITIOPROBA (país-departamento-municipio) de infección. else Devuelve dichos campos en NULL.; si Existe ubicación de desplazamiento (D.AUUBICACI no nulo) → Expone Departamento y Municipio de desplazamiento. else Devuelve dichos campos en NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila310';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA310; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; Common.Country', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila310';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila310';
-- GO



-- Stored Procedure
-- =============================================
-- Autor:		Yefersson David Caicedo Alvarez
-- Fecha Creación: 26/08/2022
-- Descripción:	Sp que me lista la Información de las Fichas del Sivigila, Ficha 880
-- Modificó:    
-- Fecha Modificación : 
-- =============================================

CREATE PROC [dbo].[SP_HC_ListarFichaSivigila880]
(
@IdFicha as Int
)

AS
BEGIN 

	SELECT

		CASE ERUPCION WHEN '1' THEN 'X' END AS 'Erupcion_Si', CASE ERUPCION WHEN '2' THEN 'X' END AS 'Erupcion_No', CASE ERUPCION WHEN '3' THEN 'X' END AS 'Erupcion_Des',
		CASE TIPOERUPCION WHEN '1' THEN 'X' END AS 'Vesicular', CASE TIPOERUPCION WHEN '2' THEN 'X' END AS 'Maculopapular',
		CONVERT(varchar(10),FECHAINIERUPCION,103) as 'FechaInierupcion',
		CASE FIEBRE WHEN '1' THEN 'X' END AS 'Fiebre_Si', CASE FIEBRE WHEN '2' THEN 'X' END AS 'Fiebre_No', CASE FIEBRE WHEN '3' THEN 'X' END AS 'Fiebre_Des',
		CASE ULCEGENITALES WHEN '1' THEN 'X' END AS 'Ulcegenitales_Si', CASE ULCEGENITALES WHEN '2' THEN 'X' END AS 'Ulcegenitales_No', CASE ULCEGENITALES WHEN '3' THEN 'X' END AS 'Ulcegenitales_Des',
		CASE OTROSINTOMAS WHEN '1' THEN 'X' END AS 'Otrosintomas_Si', CASE OTROSINTOMAS WHEN '2' THEN 'X' END AS 'Otrosintomas_No', CASE OTROSINTOMAS WHEN '3' THEN 'X' END AS 'Otrosintomas_Des', 
		Rtrim(CUALESOTROSINTOMAS)  as 'CualesOtros',
		CASE COMPLICACIONES WHEN '1' THEN 'X' END AS 'Complicaciones_Si', CASE COMPLICACIONES WHEN '2' THEN 'X' END AS 'Complicaciones_No',
		CASE TUVOCONTPROBA WHEN '1' THEN 'X' END AS 'Tuvocontacto_Si', CASE TUVOCONTPROBA WHEN '2' THEN 'X' END AS 'Tuvocontacto_No', CASE TUVOCONTPROBA WHEN '3' THEN 'X' END AS 'Tuvocontacto_Des',
		CONVERT(varchar(10),FECHACONTACTO,103) as 'FechaContacto',
		CASE TUVOCONTSEXUAL WHEN '1' THEN 'X' END AS 'Tuvoconsexual_Si', CASE TUVOCONTSEXUAL WHEN '2' THEN 'X' END AS 'Tuvoconsexual_No', CASE TUVOCONTSEXUAL WHEN '3' THEN 'X' END AS 'Tuvoconsexual_Des', 
		CASE TIENENUEVAPSEXUAL WHEN '1' THEN 'X' END AS 'Tienenueva_Si', CASE TIENENUEVAPSEXUAL WHEN '2' THEN 'X' END AS 'Tienenueva_No',
		CASE ANTEVIAJEEXT21D WHEN '1' THEN 'X' END AS 'Anteviaje_Si', CASE ANTEVIAJEEXT21D WHEN '2' THEN 'X' END AS 'Anteviaje_No',
		Rtrim(CUALPAISEXTVIAJO)  AS 'CualPais',
		CASE SEGUICONTACTOS WHEN '1' THEN 'X' END AS 'SeguiContactos_Si', CASE SEGUICONTACTOS WHEN '2' THEN 'X' END AS 'SeguiContactos_No', CASE SEGUICONTACTOS WHEN '3' THEN 'X' END AS 'SeguiContactos_Des',
		CONVERT(varchar(10),FECHAFINALSEGCONT,103) as 'FechaFinalCont',
		CASE FUENTEINFECCION WHEN '1' THEN 'Importado' WHEN '2' THEN 'Relacionado con la importación' WHEN '3' THEN 'Fuente desconocida' WHEN '4' THEN 'Relacionado con desconocido' END AS 'FuenteInfeccion',
		CASE CASODESCARTADO WHEN '1' THEN 'Laboratorio negativo' WHEN '2' THEN 'Dengue' WHEN '3' THEN 'Herpes 6,6' WHEN '4' THEN 'Reacción alérgica' WHEN '5' THEN 'Varicela' WHEN '6' THEN 'Sifilis' WHEN '7' THEN 'Otro diagnóstico' END AS 'CasoDescartado',
		CASE COMPLICEREBRAL WHEN '1' THEN 'X' END AS 'Complicer', CASE COMPLIPULMONAR WHEN '1' THEN 'X' END AS 'Complipul', CASE COMPLIOFTALMICA WHEN '1' THEN 'X' END AS 'Complioft',
		CONVERT(varchar(10),FECHATOMA1,103) as 'FechaToma1',
		CONVERT(varchar(10),FECHARECEP1,103) as 'FechaRecep1',
		CASE MUESTRA1 WHEN '1' THEN '3' WHEN '2' THEN '4' WHEN '3' THEN '6' WHEN '4' THEN '13' WHEN '5' THEN '14' END AS 'Muestra1',
		CASE PRUEBA1 WHEN '1' THEN '4' END AS 'Prueba1',
		CASE AGENTE1 WHEN '1' THEN '2J' WHEN '2' THEN '8' END AS 'Agente1',
		Rtrim(RESULTADO1) AS 'Resultado1',
		CONVERT(varchar(10),FECHARES1,103) as 'FechaRes1',
		Rtrim(VALOREGIS1) AS 'Valor1',
		CONVERT(varchar(10),FECHATOMA2,103) as 'FechaToma2',
		CONVERT(varchar(10),FECHARECEP2,103) as 'FechaRecep2',
		CASE MUESTRA2 WHEN '1' THEN '3' WHEN '2' THEN '4' WHEN '3' THEN '6' WHEN '4' THEN '13' WHEN '5' THEN '14' END AS 'Muestra2',
		CASE PRUEBA2 WHEN '1' THEN '4' END AS 'Prueba2', 
		CASE AGENTE2 WHEN '1' THEN '2J' WHEN '2' THEN '8' END AS 'Agente2',
		Rtrim(RESULTADO2) AS 'Resultado2',
		CONVERT(varchar(10),FECHARES2,103) as 'FechaRes2',
		Rtrim(VALOREGIS2) AS 'Valor2', 
		CONVERT(varchar(10),FECHATOMA3,103) as 'FechaToma3',
		CONVERT(varchar(10),FECHARECEP3,103) as 'FechaRecep3',
		CASE MUESTRA3 WHEN '1' THEN '3' WHEN '2' THEN '4' WHEN '3' THEN '6' WHEN '4' THEN '13' WHEN '5' THEN '14' END AS 'Muestra3', 
		CASE PRUEBA3 WHEN '1' THEN '4' END AS 'Prueba3',
		CASE AGENTE3 WHEN '1' THEN '2J' WHEN '2' THEN '8' END AS 'Agente3',
		Rtrim(RESULTADO3) AS 'Resultado3',
		CONVERT(varchar(10),FECHARES3,103) as 'FechaRes3',
		Rtrim(VALOREGIS3) AS 'Valor3',
		CONVERT(varchar(10),FECHATOMA4,103) as 'FechaToma4',
		CONVERT(varchar(10),FECHARECEP4,103) as 'FechaRecep4',
		CASE MUESTRA4 WHEN '1' THEN '3' WHEN '2' THEN '4' WHEN '3' THEN '6' WHEN '4' THEN '13' WHEN '5' THEN '14' END AS 'Muestra4',
		CASE PRUEBA4 WHEN '1' THEN '4' END AS 'Prueba4',
		CASE AGENTE4 WHEN '1' THEN '2J' WHEN '2' THEN '8' END AS 'Agente4',
		Rtrim(RESULTADO4) AS 'Resultado4',
		CONVERT(varchar(10),FECHARES4,103) as 'FechaRes4',
		Rtrim(VALOREGIS4) AS 'Valor4',
		VERSION AS 'VERSION',
		JSON AS 'JSON',
		Rtrim(JSON_VALUE(JSON,'$.PAIS_CODIGO') ) As 'PAIS_CODIGO'

	FROM HCFICHA880
	WHERE IDFICHANOTIFICACION  = @IdFicha 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera la información completa de la ficha SIVIGILA 880 para un caso específico de viruela símica (monkeypox) o enfermedad exantemática similar, dado el identificador interno de la ficha. Retorna todos los campos clínicos y epidemiológicos del caso: síntomas (erupción, fiebre, úlceras genitales, complicaciones cerebrales, pulmonares y oftálmicas), antecedentes de contacto probable y contacto sexual, antecedentes de viaje al exterior en los últimos 21 días, seguimiento de contactos, fuente de infección y motivo de descarte del caso. También expone hasta cuatro muestras de laboratorio con su fecha de toma, fecha de recepción, tipo de muestra, prueba realizada, agente identificado, resultado y valor registrado. Este procedimiento es usado por el módulo de notificación epidemiológica obligatoria para imprimir o consultar la ficha 880 de un paciente notificado ante el SIVIGILA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila880';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila880';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la información de la ficha de notificación Sivigila 880 (viruela símica/exantemáticas) decodificando códigos numéricos en marcas/etiquetas para presentación o exportación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila880';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA880 cuyo IDFICHANOTIFICACION coincida con el identificador recibido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila880';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las fechas se entregan siempre en formato dd/mm/yyyy (style 103); Las banderas Si/No/Desconocido son mutuamente excluyentes en la salida (solo una columna queda con ''X''); Los códigos numéricos internos se traducen a los códigos/etiquetas oficiales del Sivigila; Los campos de texto se entregan con espacios derechos eliminados (RTRIM); El campo PAIS_CODIGO se obtiene extrayendo la propiedad PAIS_CODIGO del documento JSON almacenado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila880';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Sivigila; Ficha de notificación 880; Erupción vesicular/maculopapular; Fiebre; Úlceras genitales; Complicaciones cerebral/pulmonar/oftálmica; Contacto con caso probable; Contacto sexual; Antecedente de viaje al exterior; Seguimiento de contactos; Fuente de infección (importado/relacionado); Caso descartado (Dengue, Herpes, Varicela, Sífilis, etc.); Muestras de laboratorio; Pruebas diagnósticas; Agentes etiológicos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila880';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA880: Cuando IDFICHANOTIFICACION = @IdFicha, retorna fila con campos decodificados (marcas ''X'' para Si/No/Desconocido, fechas en formato 103 dd/mm/yyyy y catálogos de muestra/prueba/agente/fuente/descarte)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila880';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si FUENTEINFECCION = ''1'' | ''2'' | ''3'' | ''4'' → Etiqueta como ''Importado'', ''Relacionado con la importación'', ''Fuente desconocida'' o ''Relacionado con desconocido'' respectivamente; si CASODESCARTADO = ''1''..''7'' → Mapea a ''Laboratorio negativo'',''Dengue'',''Herpes 6,6'',''Reacción alérgica'',''Varicela'',''Sifilis'',''Otro diagnóstico''; si MUESTRAn = ''1''..''5'' → Traduce a códigos de muestra ''3'',''4'',''6'',''13'',''14''; si AGENTEn = ''1'' | ''2'' → Traduce a código de agente ''2J'' o ''8''; si PRUEBAn = ''1'' → Traduce a código de prueba ''4''; si TIPOERUPCION = ''1'' | ''2'' → Marca ''Vesicular'' o ''Maculopapular'' con ''X''; si Campos Si/No/Desconocido = ''1''/''2''/''3'' → Activan respectivamente las columnas _Si, _No, _Des con ''X''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila880';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA880', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila880';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila880';
-- GO

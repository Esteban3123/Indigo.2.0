

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
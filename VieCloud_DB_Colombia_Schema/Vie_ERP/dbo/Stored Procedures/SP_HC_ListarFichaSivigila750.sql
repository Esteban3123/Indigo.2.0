-- Stored Procedure

-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,22-11-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila750]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 

	--Select 	*	From HCFICHA750 where IDFICHANOTIFICACION  = 13

	Select
	CASE CONDMOMEDIAG WHEN '1' THEN 'X' END AS 'Embarazo', CASE CONDMOMEDIAG WHEN '2' THEN 'X' END AS 'Parto', CASE CONDMOMEDIAG WHEN '3' THEN 'X' END AS 'Puerperio',
	CASE CONDMOMEDIAG WHEN '4' THEN 'X' END AS 'Post Aborto',
	CASE CTRLPRENEMBACT WHEN '1' THEN 'X' END AS 'CTRLPRENEMBACT Si', CASE CTRLPRENEMBACT WHEN '0' THEN 'X' END AS 'CTRLPRENEMBACT No',
	Rtrim(EDADGESTPRIMCTRL) As 'EDADGESTPRIMCTRL',
	CASE DIAGEMBACT WHEN '1' THEN 'X' END AS 'Primera Vez', CASE DIAGEMBACT WHEN '0' THEN 'X' END AS 'Reinfección',
	CASE PRUEBATREPO WHEN '1' THEN 'X' END AS 'PRUEBATREPO Si', CASE PRUEBATREPO WHEN '0' THEN 'X' END AS 'PRUEBATREPO No',
	Rtrim(EDGESTREAPRUE) As 'EDGESTREAPRUE',
	CASE CUAL WHEN '1' THEN 'X' END AS 'TPPA', CASE CUAL WHEN '2' THEN 'X' END AS 'TPHA', CASE CUAL WHEN '3' THEN 'X' END AS 'Prueba Rapida', CASE CUAL WHEN '4' THEN 'X' END AS 'Otra',
	CASE RESULTADO WHEN '1' THEN 'X' END AS 'RESULTADO Si', CASE RESULTADO WHEN '0' THEN 'X' END AS 'RESULTADO No',
	CASE PRUEBANOTREPO WHEN '1' THEN 'X' END AS 'PRUEBANOTREPO Si', CASE PRUEBANOTREPO WHEN '0' THEN 'X' END AS 'PRUEBANOTREPO No',
	Rtrim(EDGESTREAPRUE2) As 'EDGESTREAPRUE2',
	CASE RESULTADO2 WHEN '1' THEN 'X' END AS '2 DILS', CASE RESULTADO2 WHEN '2' THEN 'X' END AS '4 DILS', CASE RESULTADO2 WHEN '3' THEN 'X' END AS '8 DILS',
	CASE RESULTADO2 WHEN '4' THEN 'X' END AS '16 DILS', CASE RESULTADO2 WHEN '5' THEN 'X' END AS '32 DILS', CASE RESULTADO2 WHEN '6' THEN 'X' END AS '64 DILS',
	CASE RESULTADO2 WHEN '7' THEN 'X' END AS '128 DILS', CASE RESULTADO2 WHEN '8' THEN 'X' END AS '256 DILS', CASE RESULTADO2 WHEN '9' THEN 'X' END AS '512 DILS',
	CASE RESULTADO2 WHEN '10' THEN 'X' END AS '1024 DILS', CASE RESULTADO2 WHEN '11' THEN 'X' END AS '2048 DILS',
	CASE PENICILINA WHEN '1' THEN 'X' END AS '0 Dosis', CASE PENICILINA WHEN '2' THEN 'X' END AS '1 Dosis', CASE PENICILINA WHEN '3' THEN 'X' END AS '2 Dosis',
	CASE PENICILINA WHEN '4' THEN 'X' END AS '3 Dosis',
	convert(varchar(10),FECHAAPLI,103) As 'FECHAAPLI',
	CASE TRATCONTAC WHEN '1' THEN 'X' END AS 'TRATCONTAC Si', CASE TRATCONTAC WHEN '0' THEN 'X' END AS 'TRATCONTAC No',
	VERSION AS 'VERSION', JSON AS 'JSON'
	From HCFICHA750 where IDFICHANOTIFICACION  = @IdFicha

END

--select * from hcfichanotificacion where id = '8'
--select * from HCFICHA750
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el detalle clínico y epidemiológico de una ficha SIVIGILA tipo 750 (sífilis gestacional y congénita) a partir de su identificador de ficha. Consulta la tabla HCFICHA750 y transforma los campos codificados en etiquetas legibles para el formulario de notificación obligatoria, incluyendo: momento del diagnóstico (embarazo, parto, puerperio, post aborto), control prenatal de la gestante activa, tipo y resultado de pruebas treponémicas (TPPA, TPHA, prueba rápida) y no treponémicas con diluciones, dosis de penicilina aplicadas, fecha de aplicación del tratamiento y tratamiento del contacto. Se usa para imprimir o visualizar la ficha de vigilancia epidemiológica de sífilis gestacional/congénita en el módulo de historia clínica, integrando los datos de notificación al SIVIGILA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila750';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila750';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve, en formato presentable (marcas ''X'' por opción), los datos de la ficha SIVIGILA 750 (sífilis gestacional/congénita) asociada a una notificación específica, para impresión o visualización del formulario.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila750';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA750 con IDFICHANOTIFICACION igual al identificador recibido; Los códigos almacenados en CONDMOMEDIAG, CTRLPRENEMBACT, DIAGEMBACT, PRUEBATREPO, CUAL, RESULTADO, PRUEBANOTREPO, RESULTADO2, PENICILINA y TRATCONTAC deben corresponder a los dominios esperados (1..n / 0/1) para producir marcas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila750';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retorna información de la ficha cuya IDFICHANOTIFICACION coincide con el parámetro de entrada; Los campos categóricos se traducen a marca ''X'' en columnas mutuamente excluyentes; valores fuera del dominio definido producen NULL; Las fechas se formatean a dd/MM/yyyy (estilo 103); No modifica datos; es una consulta de solo lectura', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila750';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación SIVIGILA; Sífilis gestacional/congénita (ficha 750); Embarazo, parto, puerperio, post aborto; Control prenatal; Edad gestacional; Pruebas treponémicas (TPPA, TPHA, Prueba Rápida); Pruebas no treponémicas (diluciones); Tratamiento con penicilina; Tratamiento de contactos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila750';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA750: WHERE IDFICHANOTIFICACION = @IdFicha → retorna un result set con los campos de la ficha 750 transformados a marcas ''X'' según el valor codificado de cada variable y la fecha de aplicación formateada como dd/MM/yyyy', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila750';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CONDMOMEDIAG ∈ {1,2,3,4} → Marca con ''X'' la condición gestacional: 1=Embarazo, 2=Parto, 3=Puerperio, 4=Post Aborto; si CTRLPRENEMBACT = 1 / 0 → Marca ''X'' en columna Sí o No de control prenatal en embarazo actual; si DIAGEMBACT = 1 / 0 → Marca ''X'' en ''Primera Vez'' o ''Reinfección'' del diagnóstico de embarazo actual; si PRUEBATREPO = 1 / 0 → Marca ''X'' en Sí o No para prueba treponémica reportada; si CUAL ∈ {1,2,3,4} → Marca ''X'' según tipo de prueba: 1=TPPA, 2=TPHA, 3=Prueba Rápida, 4=Otra; si RESULTADO = 1 / 0 → Marca ''X'' en Sí o No del resultado de la prueba; si PRUEBANOTREPO = 1 / 0 → Marca ''X'' en Sí o No para prueba no treponémica reportada; si RESULTADO2 ∈ {1..11} → Marca ''X'' en la dilución correspondiente: 2,4,8,16,32,64,128,256,512,1024 o 2048 DILS; si PENICILINA ∈ {1,2,3,4} → Marca ''X'' en número de dosis aplicadas: 0, 1, 2 o 3 dosis; si TRATCONTAC = 1 / 0 → Marca ''X'' en Sí o No para tratamiento de contactos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila750';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA750', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila750';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila750';
-- GO

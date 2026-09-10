-- Stored Procedure

-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,20-11-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila305]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 

	--Select 	*	From HCFICHA305 where IDFICHANOTIFICACION  = 13
	Select
	CASE TIERESIACT WHEN '1' THEN 'X' END AS 'Menor5anios', CASE TIERESIACT WHEN '2' THEN 'X' END AS 'Entre5y15anios', CASE TIERESIACT WHEN '3' THEN 'X' END AS '15ymas',
	CASE HISTOCIRU WHEN '1' THEN 'X' END AS 'HISTOCIRU Si',CASE HISTOCIRU WHEN '0' THEN 'X' END AS 'HISTOCIRU No',
	CASE CUALOJO1 WHEN '1' THEN 'X' END AS 'derecho1', CASE CUALOJO1 WHEN '2' THEN 'X' END AS 'izquierdo1', CASE CUALOJO1 WHEN '3' THEN 'X' END AS 'Ambos1',
	CASE PARPOJODER WHEN '1' THEN 'X' END AS 'parpsup', CASE PARPOJODER WHEN '2' THEN 'X' END AS 'parpinf', CASE PARPOJODER WHEN '3' THEN 'X' END AS 'parpambos',
	CASE PARPOJOIZQ WHEN '1' THEN 'X' END AS 'parpsupiz', CASE PARPOJOIZQ WHEN '2' THEN 'X' END AS 'parpinfiz', CASE PARPOJOIZQ WHEN '3' THEN 'X' END AS 'parpambosiz',
	CASE CICAMUCO WHEN '1' THEN 'X' END AS 'CICAMUCO Si',CASE CICAMUCO WHEN '0' THEN 'X' END AS 'CICAMUCO No',
	CASE CUALOJO2 WHEN '1' THEN 'X' END AS 'derecho2', CASE CUALOJO2 WHEN '2' THEN 'X' END AS 'izquierdo2', CASE CUALOJO2 WHEN '3' THEN 'X' END AS 'Ambos2',

	CASE DEPIPARPSUP WHEN '1' THEN 'X' END AS 'DEPIPARPSUP Si',CASE DEPIPARPSUP WHEN '0' THEN 'X' END AS 'DEPIPARPSUP No',
	CASE DEPIPARPINF WHEN '1' THEN 'X' END AS 'DEPIPARPINF Si',CASE DEPIPARPINF WHEN '0' THEN 'X' END AS 'DEPIPARPINF No',
	CASE PESTCONT1 WHEN '1' THEN 'X' END AS 'PESTCONT1 Si',CASE PESTCONT1 WHEN '0' THEN 'X' END AS 'PESTCONT1 No',
	CASE PESTCONT2 WHEN '1' THEN 'X' END AS 'PESTCONT2 Si',CASE PESTCONT2 WHEN '0' THEN 'X' END AS 'PESTCONT2 No',
	CASE PESTTOCCORN WHEN '1' THEN 'X' END AS 'PESTTOCCORN Si',CASE PESTTOCCORN WHEN '0' THEN 'X' END AS 'PESTTOCCORN No',
	CASE CUALOJO3 WHEN '1' THEN 'X' END AS 'derecho3', CASE CUALOJO3 WHEN '2' THEN 'X' END AS 'izquierdo3', CASE CUALOJO3 WHEN '3' THEN 'X' END AS 'Ambos3',
	CASE CUALOJO4 WHEN '1' THEN 'X' END AS 'derecho4', CASE CUALOJO4 WHEN '2' THEN 'X' END AS 'izquierdo4', CASE CUALOJO4 WHEN '3' THEN 'X' END AS 'Ambos4',
	CASE CUALOJO5 WHEN '1' THEN 'X' END AS 'derecho5', CASE CUALOJO5 WHEN '2' THEN 'X' END AS 'izquierdo5', CASE CUALOJO5 WHEN '3' THEN 'X' END AS 'Ambos5',
	CASE CUALOJO6 WHEN '1' THEN 'X' END AS 'derecho6', CASE CUALOJO6 WHEN '2' THEN 'X' END AS 'izquierdo6', CASE CUALOJO6 WHEN '3' THEN 'X' END AS 'Ambos6',
	CASE CUALOJO7 WHEN '1' THEN 'X' END AS 'derecho7', CASE CUALOJO7 WHEN '2' THEN 'X' END AS 'izquierdo7', CASE CUALOJO7 WHEN '3' THEN 'X' END AS 'Ambos7',

	CASE EVIDENCIA1 WHEN '1' THEN 'X' END AS '0', CASE EVIDENCIA1 WHEN '2' THEN 'X' END AS '1', CASE EVIDENCIA1 WHEN '3' THEN 'X' END AS '2',
	CASE EVIDENCIA1 WHEN '4' THEN 'X' END AS '3', CASE EVIDENCIA1 WHEN '5' THEN 'X' END AS '4',
	CASE EVIDENCIA2 WHEN '1' THEN 'X' END AS '00', CASE EVIDENCIA2 WHEN '2' THEN 'X' END AS '11', CASE EVIDENCIA2 WHEN '3' THEN 'X' END AS '22',
	CASE EVIDENCIA2 WHEN '4' THEN 'X' END AS '33', CASE EVIDENCIA2 WHEN '5' THEN 'X' END AS '44',
	CASE EVIDENCIA3 WHEN '1' THEN 'X' END AS '000', CASE EVIDENCIA3 WHEN '2' THEN 'X' END AS '111', CASE EVIDENCIA3 WHEN '3' THEN 'X' END AS '222',
	CASE EVIDENCIA3 WHEN '4' THEN 'X' END AS '333', CASE EVIDENCIA3 WHEN '5' THEN 'X' END AS '444',
	CASE EVIDENCIA4 WHEN '1' THEN 'X' END AS '0000', CASE EVIDENCIA4 WHEN '2' THEN 'X' END AS '1111', CASE EVIDENCIA4 WHEN '3' THEN 'X' END AS '2222',
	CASE EVIDENCIA4 WHEN '4' THEN 'X' END AS '3333', CASE EVIDENCIA4 WHEN '5' THEN 'X' END AS '4444',
	CASE EVIDENCIA5 WHEN '1' THEN 'X' END AS '00000', CASE EVIDENCIA5 WHEN '2' THEN 'X' END AS '11111', CASE EVIDENCIA5 WHEN '3' THEN 'X' END AS '22222',
	CASE EVIDENCIA5 WHEN '4' THEN 'X' END AS '33333', CASE EVIDENCIA5 WHEN '5' THEN 'X' END AS '44444',

	CASE EVIDENCIA11 WHEN '1' THEN 'X' END AS 'cero', CASE EVIDENCIA11 WHEN '2' THEN 'X' END AS 'uno', CASE EVIDENCIA11 WHEN '3' THEN 'X' END AS 'dos',
	CASE EVIDENCIA11 WHEN '4' THEN 'X' END AS 'tres', CASE EVIDENCIA11 WHEN '5' THEN 'X' END AS 'cuatro',
	CASE EVIDENCIA22 WHEN '1' THEN 'X' END AS 'cero2', CASE EVIDENCIA22 WHEN '2' THEN 'X' END AS 'uno2', CASE EVIDENCIA22 WHEN '3' THEN 'X' END AS 'dos2',
	CASE EVIDENCIA22 WHEN '4' THEN 'X' END AS 'tres2', CASE EVIDENCIA22 WHEN '5' THEN 'X' END AS 'cuatro2',
	CASE EVIDENCIA33 WHEN '1' THEN 'X' END AS 'cero3', CASE EVIDENCIA33 WHEN '2' THEN 'X' END AS 'uno3', CASE EVIDENCIA33 WHEN '3' THEN 'X' END AS 'dos3',
	CASE EVIDENCIA33 WHEN '4' THEN 'X' END AS 'tres3', CASE EVIDENCIA33 WHEN '5' THEN 'X' END AS 'cuatro3',
	CASE EVIDENCIA44 WHEN '1' THEN 'X' END AS 'cero4', CASE EVIDENCIA44 WHEN '2' THEN 'X' END AS 'uno4', CASE EVIDENCIA44 WHEN '3' THEN 'X' END AS 'dos4',
	CASE EVIDENCIA44 WHEN '4' THEN 'X' END AS 'tres4', CASE EVIDENCIA44 WHEN '5' THEN 'X' END AS 'cuatro4',
	CASE EVIDENCIA55 WHEN '1' THEN 'X' END AS 'cero5', CASE EVIDENCIA55 WHEN '2' THEN 'X' END AS 'uno5', CASE EVIDENCIA55 WHEN '3' THEN 'X' END AS 'dos5',
	CASE EVIDENCIA55 WHEN '4' THEN 'X' END AS 'tres5', CASE EVIDENCIA55 WHEN '5' THEN 'X' END AS 'cuatro5',

	CASE OPACORN WHEN '1' THEN 'X' END AS 'derec', CASE OPACORN WHEN '2' THEN 'X' END AS 'izqui', CASE OPACORN WHEN '3' THEN 'X' END AS 'both',
	CASE OPACORN WHEN '4' THEN 'X' END AS 'not present',
	CASE ENGROPARP WHEN '1' THEN 'X' END AS 'derec2', CASE ENGROPARP WHEN '2' THEN 'X' END AS 'izqui2', CASE ENGROPARP WHEN '3' THEN 'X' END AS 'both2',
	CASE ENGROPARP WHEN '4' THEN 'X' END AS 'not present2',
	CASE PESTMAL WHEN '1' THEN 'X' END AS 'derec3', CASE PESTMAL WHEN '2' THEN 'X' END AS 'izqui3', CASE PESTMAL WHEN '3' THEN 'X' END AS 'both3',
	CASE PESTMAL WHEN '4' THEN 'X' END AS 'not present3',
	CASE SENSCUERP WHEN '1' THEN 'X' END AS 'derec4', CASE SENSCUERP WHEN '2' THEN 'X' END AS 'izqui4', CASE SENSCUERP WHEN '3' THEN 'X' END AS 'both4',
	CASE SENSCUERP WHEN '4' THEN 'X' END AS 'not present4',
	CASE FOTOFOB WHEN '1' THEN 'X' END AS 'derec5', CASE FOTOFOB WHEN '2' THEN 'X' END AS 'izqui5', CASE FOTOFOB WHEN '3' THEN 'X' END AS 'both5',
	CASE FOTOFOB WHEN '4' THEN 'X' END AS 'not present5', Rtrim(MEDIOJODER) As 'MEDIOJODER', Rtrim(MEDIOJOIZQ) As 'MEDIOJOIZQ'

	From HCFICHA305 where IDFICHANOTIFICACION  = @IdFicha

END

--select * from hcfichanotificacion where id = '8'
--select * from HCFICHA305
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera y formatea los datos de la ficha clínica de notificación SIVIGILA 305 (notificación oftalmológica) para un ingreso o ficha específica, identificada por su ID y número de ficha. Consulta la tabla HCFICHA305 y transforma cada campo codificado (rangos de edad del paciente, antecedentes quirúrgicos oculares, afecciones de párpados, córnea, mucosa, pestañas, grado de evidencia de lesiones y sensaciones visuales como fotofobia o cuerpo extraño) en marcas ''X'' listas para imprimir o exportar en el formulario oficial de vigilancia epidemiológica. Su propósito principal es la generación del reporte de notificación obligatoria de enfermedades oculares ante el sistema SIVIGILA, cubriendo hallazgos en ojo derecho, izquierdo o ambos ojos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila305';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila305';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los datos de la ficha de notificación SIVIGILA 305 transformando códigos numéricos en marcas ''X'' por categoría para su impresión/visualización.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila305';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA305 cuyo IDFICHANOTIFICACION coincida con el parámetro recibido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila305';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retorna información de la ficha cuyo IDFICHANOTIFICACION coincide con el parámetro.; Las columnas de marca quedan en NULL cuando el código fuente no coincide con ninguno de los valores esperados (no se imprime ''X'').; Los textos libres MEDIOJODER y MEDIOJOIZQ se devuelven sin espacios finales (RTRIM).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila305';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación SIVIGILA; Tracoma / examen ocular (ojo derecho/izquierdo/ambos); Párpado superior/inferior; Opacidad corneal; Engrosamiento palpebral; Pestañas mal posicionadas / fotofobia; Grupos etarios (<5, 5-15, ≥15 años); Historia de cirugía y cicatriz mucosa', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila305';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA305: Retorna las columnas de la ficha 305 filtradas por IDFICHANOTIFICACION = @IdFicha, mapeando códigos a ''X'' según categoría (p.ej. TIERESIACT=1 → Menor5anios, =2 → Entre5y15anios, =3 → 15ymas).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila305';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIERESIACT ∈ {1,2,3} → Marca ''X'' en grupo etario: <5 años, 5-15 años o ≥15 años respectivamente.; si HISTOCIRU / CICAMUCO / DEPIPARPSUP / DEPIPARPINF / PESTCONT1 / PESTCONT2 / PESTTOCCORN ∈ {0,1} → 1 → marca columna ''Si'', 0 → marca columna ''No'' del indicador correspondiente.; si CUALOJO1..7 ∈ {1,2,3} → 1=derecho, 2=izquierdo, 3=ambos ojos.; si PARPOJODER / PARPOJOIZQ ∈ {1,2,3} → 1=párpado superior, 2=párpado inferior, 3=ambos párpados.; si EVIDENCIA1..5 y EVIDENCIA11..55 ∈ {1..5} → Marca ''X'' en la columna correspondiente al grado/nivel de evidencia (0 a 4).; si OPACORN / ENGROPARP / PESTMAL / SENSCUERP / FOTOFOB ∈ {1,2,3,4} → 1=derecho, 2=izquierdo, 3=ambos, 4=no presente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila305';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA305', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila305';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila305';
-- GO

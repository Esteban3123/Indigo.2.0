
-- Stored Procedure
-- =============================================
-- Autor:		Jean Carlos Roldan Lozano
-- Fecha Creación: 21-11-2018
-- Descripción:	Sp que me lista la Información de las Fichas del Sivigila, Ficha 365
-- Modificó:    Yezid Garcia Medina
-- Fecha Modificación : 24-01-2022
-- =============================================

CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila770]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 
			Select
				CASE LLANTONACER WHEN '1' THEN 'X' END AS 'LLANTONACER Si', CASE LLANTONACER WHEN '0' THEN 'X' END AS 'LLANTONACER No',
				CASE MAMANORMAL WHEN '1' THEN 'X' END AS 'MAMANORMAL Si', CASE MAMANORMAL WHEN '0' THEN 'X' END AS 'MAMANORMAL No',
				CASE DEJOMAMAR WHEN '1' THEN 'X' END AS 'DEJOMAMAR Si', CASE DEJOMAMAR WHEN '0' THEN 'X' END AS 'DEJOMAMAR No',
				CASE HIPERTEMIA WHEN '1' THEN 'X' END AS 'HIPERTEMIA Si', CASE HIPERTEMIA WHEN '0' THEN 'X' END AS 'HIPERTEMIA No',
				CASE FONTANELA WHEN '1' THEN 'X' END AS 'FONTANELA Si', CASE FONTANELA WHEN '0' THEN 'X' END AS 'FONTANELA No',
				CASE RIGIDEZNUCA WHEN '1' THEN 'X' END AS 'RIGIDEZNUCA Si', CASE RIGIDEZNUCA WHEN '0' THEN 'X' END AS 'RIGIDEZNUCA No',
				CASE TRISMUS WHEN '1' THEN 'X' END AS 'TRISMUS Si', CASE TRISMUS WHEN '0' THEN 'X' END AS 'TRISMUS No',
				CASE CONVULSIONES WHEN '1' THEN 'X' END AS 'CONVULSIONES Si', CASE CONVULSIONES WHEN '0' THEN 'X' END AS 'CONVULSIONES No',
				CASE CONTRACCIONES WHEN '1' THEN 'X' END AS 'CONTRACCIONES Si', CASE CONTRACCIONES WHEN '0' THEN 'X' END AS 'CONTRACCIONES No',
				CASE OPISTOTONOS WHEN '1' THEN 'X' END AS 'OPISTOTONOS Si', CASE OPISTOTONOS WHEN '0' THEN 'X' END AS 'OPISTOTONOS No',
				CASE ESPASMOS WHEN '1' THEN 'X' END AS 'ESPASMOS Si', CASE ESPASMOS WHEN '0' THEN 'X' END AS 'ESPASMOS No',
				CASE LLANTOEXCE WHEN '1' THEN 'X' END AS 'LLANTOEXCE Si', CASE LLANTOEXCE WHEN '0' THEN 'X' END AS 'LLANTOEXCE No',
				CASE SEPSISUMBI WHEN '1' THEN 'X' END AS 'SEPSISUMBI Si', CASE SEPSISUMBI WHEN '0' THEN 'X' END AS 'SEPSISUMBI No',
				CASE ASISTICONTROL WHEN '1' THEN 'X' END AS 'ASISTICONTROL Si', CASE ASISTICONTROL WHEN '0' THEN 'X' END AS 'ASISTICONTROL No',
				CASE ATENMED WHEN '1' THEN 'X' END AS 'ATENMED Si', CASE ATENMED WHEN '0' THEN 'X' END AS 'ATENMED No',
				CASE ATENDENF WHEN '1' THEN 'X' END AS 'ATENDENF Si', CASE ATENDENF WHEN '0' THEN 'X' END AS 'ATENDENF No',
				CASE ATENDAUX WHEN '1' THEN 'X' END AS 'ATENDAUX Si', CASE ATENDAUX WHEN '0' THEN 'X' END AS 'ATENDAUX No',
				CASE ATENPROM WHEN '1' THEN 'X' END AS 'ATENPROM Si', CASE ATENPROM WHEN '0' THEN 'X' END AS 'ATENPROM No',
				CASE ATENOTRO WHEN '1' THEN 'X' END AS 'ATENOTRO Si', CASE ATENOTRO WHEN '0' THEN 'X' END AS 'ATENOTRO No',
				CASE LUGAREMB WHEN '1' THEN 'X' END AS 'LUGAREMB Si', CASE LUGAREMB WHEN '0' THEN 'X' END AS 'LUGAREMB No',
				CASE ANTEVAC WHEN '1' THEN 'X' END AS 'ANTEVAC Si', CASE ANTEVAC WHEN '0' THEN 'X' END AS 'ANTEVAC No',
				CASE LUGARPART WHEN '1' THEN 'X' END AS 'LUGARPART Si', CASE LUGARPART WHEN '0' THEN 'X' END AS 'LUGARPART No',
				CASE APLISUST WHEN '1' THEN 'X' END AS 'APLISUST Si', CASE APLISUST WHEN '0' THEN 'X' END AS 'APLISUST No',
				CASE QUIENATENPAR WHEN '1' THEN 'X' END AS 'Médico', CASE QUIENATENPAR WHEN '2' THEN 'X' END AS 'Enfermera', CASE QUIENATENPAR WHEN '3' THEN 'X' END AS 'Auxiliar',
				CASE QUIENATENPAR WHEN '4' THEN 'X' END AS 'Promotora', CASE QUIENATENPAR WHEN '5' THEN 'X' END AS 'ParteraComp', CASE QUIENATENPAR WHEN '6' THEN 'X' END AS 'ParteranoComp',
				CASE QUIENATENPAR WHEN '7' THEN 'X' END AS 'Familiar', CASE QUIENATENPAR WHEN '8' THEN 'X' END AS 'Sola', CASE QUIENATENPAR WHEN '9' THEN 'X' END AS 'Otro',
				Rtrim(NOMMADRE) As 'NOMMADRE', Rtrim(EDADMADRE) As 'EDADMADRE', Rtrim(NUMEMBA) As 'NUMEMBA', Rtrim(EXPLINOASIS) As 'EXPLINOASIS', Rtrim(OTROQUIEN) As 'OTROQUIEN',
				Rtrim(NUMCONTROL) As 'NUMCONTROL', Rtrim(QUEMUNI) As 'QUEMUNI', Rtrim(NUMDOSIS) As 'NUMDOSIS', Rtrim(EXPLINORECIB) As 'EXPLINORECIB', Rtrim(CUALSUST) As 'CUALSUST',
				convert(varchar(10),QUEFECHA,103) As 'QUEFECHA', convert(varchar(10),ULTICONTROL,103) As 'ULTICONTROL', convert(varchar(10),FECHTD1,103) As 'FECHTD1',
				convert(varchar(10),FECHTD2,103) As 'FECHTD2', convert(varchar(10),FECHTD3,103) As 'FECHTD3', convert(varchar(10),FECHTD4,103) As 'FECHTD4', 
				VERSION AS 'VERSION', 
				JSON AS 'JSON' 
			From 
				HCFICHA770 
			Where 
				IDFICHANOTIFICACION  = @IdFicha				
			
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el contenido completo de la ficha de notificación epidemiológica SIVIGILA 770 correspondiente a un caso de tétanos neonatal, dado el identificador interno de la ficha. Consulta la tabla HCFICHA770 y devuelve todos los signos y síntomas del recién nacido (llanto al nacer, succión, hipertermia, rigidez de nuca, trismus, convulsiones, opistótonos, espasmos, sepsis umbilical, entre otros) como valores Sí/No listos para imprimir en el formulario oficial. También retorna los datos de la madre (nombre, edad, número de embarazos), información del control prenatal (asistencia, tipo de personal que atendió el parto, lugar del parto, antecedente de vacunación antitetánica, dosis aplicadas y fechas) y la explicación de no asistencia o no recepción de vacuna. Se usa para visualizar e imprimir la ficha epidemiológica 770 de tétanos neonatal en la historia clínica del paciente notificado ante el SIVIGILA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila770';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila770';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea para presentación los datos de una ficha Sivigila 770 (tétanos neonatal), convirtiendo respuestas binarias en marcas ''X'' y dando formato a fechas y textos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila770';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA770 con IDFICHANOTIFICACION igual al identificador suministrado; Los campos de respuesta tipo Sí/No deben estar almacenados como ''1'' o ''0''; QUIENATENPAR debe contener un valor entre ''1'' y ''9'' para identificar el tipo de persona que atendió el parto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila770';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los campos booleanos (1/0) se transforman a marca ''X'' en columnas separadas Si/No para presentación en formato ficha impresa; El campo QUIENATENPAR codifica con valores 1-9 el tipo de persona que atendió el parto y se despliega como casilla marcada ''X'' por categoría; Las fechas se entregan en formato dd/mm/yyyy (estilo 103); Se aplica RTRIM a campos de texto para eliminar espacios en blanco a la derecha; Solo se retorna información de una única ficha identificada por IDFICHANOTIFICACION', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila770';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila; Tétanos neonatal (signos: rigidez de nuca, trismus, opistótonos, espasmos, convulsiones); Antecedentes de vacunación (TD dosis 1-4); Control prenatal; Atención del parto (médico, enfermera, auxiliar, promotora, partera, familiar); Sepsis umbilical; Signos clínicos del recién nacido (llanto al nacer, succión, hipertermia, fontanela)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila770';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA770: Cuando IDFICHANOTIFICACION = @IdFicha, retorna el conjunto de columnas formateadas (marcas ''X'' Si/No, fechas en formato 103, textos sin espacios) de la ficha', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila770';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA770', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila770';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila770';
-- GO

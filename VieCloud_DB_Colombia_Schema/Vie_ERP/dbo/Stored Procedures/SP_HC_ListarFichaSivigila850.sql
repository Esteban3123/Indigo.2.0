-- Stored Procedure
-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,06-11-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila850]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
  	
		BEGIN 

			Select
				CASE MECAPROBA  WHEN '1' THEN 'X' END AS 'Heterosexual',CASE MECAPROBA WHEN '2' THEN 'X' END AS 'Homosexual', CASE MECAPROBA WHEN '3' THEN 'X' END AS 'Bisexual',
				CASE MECAPROBA  WHEN '4' THEN 'X' END AS 'Materno',CASE MECAPROBA WHEN '5' THEN 'X' END AS 'Transfusion', CASE MECAPROBA WHEN '6' THEN 'X' END AS 'Inyeccion',
				CASE MECAPROBA  WHEN '7' THEN 'X' END AS 'Accidente',CASE MECAPROBA WHEN '8' THEN 'X' END AS 'Transplante', CASE MECAPROBA WHEN '9' THEN 'X' END AS 'Piercing',
				CASE MECAPROBA  WHEN '10' THEN 'X' END AS 'Hemodialisis',CASE MECAPROBA WHEN '11' THEN 'X' END AS 'Tatuajes', CASE MECAPROBA WHEN '12' THEN 'X' END AS 'Otros',
				CASE MECAPROBA  WHEN '13' THEN 'X' END AS 'Acupuntura', Rtrim(NOMMADRE) As 'NOMMADRE', 
				CASE Rtrim(TIPOID)  WHEN '1' THEN 'RC' WHEN '2' THEN 'TI' WHEN '3' THEN 'CC' WHEN '4' THEN 'CE' WHEN '5' THEN 'PA' WHEN '6' THEN 'MS' WHEN '7' THEN 'AS' WHEN '8' THEN 'PE' WHEN '9' THEN 'CN' WHEN '10' THEN 'PT' END AS 'TIPOID', 				
				Rtrim(NUMIDENT) As 'NUMIDENT', 
				CASE IDENTGEN  WHEN '1' THEN 'X' END AS 'Masculino',CASE IDENTGEN WHEN '2' THEN 'X' END AS 'Femenino', CASE IDENTGEN WHEN '3' THEN 'X' END AS 'Transgenero',
				CASE DONOSANGRE  WHEN '1' THEN 'X' END AS 'DonoSi',CASE DONOSANGRE WHEN '0' THEN 'X' END AS 'DonoNo',
				CASE TIPOPRUEBA  WHEN '1' THEN 'X' END AS 'Western',CASE TIPOPRUEBA WHEN '2' THEN 'X' END AS 'Carga', CASE TIPOPRUEBA WHEN '3' THEN 'X' END AS 'Rapida',
				CASE TIPOPRUEBA  WHEN '4' THEN 'X' END AS 'Elisa', convert(varchar(10),FECHARESUL,103) As 'Fecha Resultado', Rtrim(VALORCARGA) As 'VALORCARGA',
				CASE ESTADCLINI  WHEN '1' THEN 'X' END AS 'VIH',CASE ESTADCLINI WHEN '2' THEN 'X' END AS 'SIDA', CASE ESTADCLINI WHEN '3' THEN 'X' END AS 'MUERTO',
				CASE CANDIESO WHEN '1' THEN 'X' END AS 'CANDIESO', CASE CANDIVIA WHEN '1' THEN 'X' END AS 'CANDIVIA', CASE TUBERPULM WHEN '1' THEN 'X' END AS 'TUBERPULM',
				CASE CANCERCERV WHEN '1' THEN 'X' END AS 'CANCERCERV', CASE TUBEREXTRA WHEN '1' THEN 'X' END AS 'TUBEREXTRA', CASE COCCIDIO WHEN '1' THEN 'X' END AS 'COCCIDIO',
				CASE CITOMEGA WHEN '1' THEN 'X' END AS 'CITOMEGA', CASE RETINICITO WHEN '1' THEN 'X' END AS 'RETINICITO', CASE ENCEFALOVIH WHEN '1' THEN 'X' END AS 'ENCEFALOVIH',
				CASE OTRASMICRO WHEN '1' THEN 'X' END AS 'OTRASMICRO', CASE HISTOEXTRA WHEN '1' THEN 'X' END AS 'HISTOEXTRA', CASE ISOSPOCRON WHEN '1' THEN 'X' END AS 'ISOSPOCRON',
				CASE HERPESZOST WHEN '1' THEN 'X' END AS 'HERPESZOST', CASE HISTODISEM WHEN '1' THEN 'X' END AS 'HISTODISEM', CASE LINFBURKI WHEN '1' THEN 'X' END AS 'LINFBURKI',
				CASE NEUMOPNEUMO WHEN '1' THEN 'X' END AS 'NEUMOPNEUMO', CASE NEUMORECU WHEN '1' THEN 'X' END AS 'NEUMORECU', CASE LINFOINMU WHEN '1' THEN 'X' END AS 'LINFOINMU',
				CASE CRIPTOCRON WHEN '1' THEN 'X' END AS 'CRIPTOCRON', CASE CRIPTOEXTRA WHEN '1' THEN 'X' END AS 'CRIPTOEXTRA', CASE SARCOKAPOSI WHEN '1' THEN 'X' END AS 'SARCOKAPOSI',
				CASE SINDROEMAC WHEN '1' THEN 'X' END AS 'SINDROEMAC', CASE LEUCOMULTI WHEN '1' THEN 'X' END AS 'LEUCOMULTI', CASE SEPTIRECUR WHEN '1' THEN 'X' END AS 'SEPTIRECUR',
				CASE TOXOCERE WHEN '1' THEN 'X' END AS 'TOXOCERE', CASE HEPATB WHEN '1' THEN 'X' END AS 'HEPATB', CASE HEPATC WHEN '1' THEN 'X' END AS 'HEPATC',
				CASE MENINGITIS WHEN '1' THEN 'X' END AS 'MENINGITIS',
				VERSION AS 'VERSION',
				JSON AS 'JSON'

			From 
				HCFICHA850 
			Where
				IDFICHANOTIFICACION  = @IdFicha
		END
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el detalle completo de una ficha epidemiológica SIVIGILA del evento 850 (VIH/SIDA) a partir de su identificador o número de ficha. Retorna la información clínica y epidemiológica del paciente notificado: mecanismo probable de transmisión (heterosexual, homosexual, materno, transfusión, entre otros), tipo y número de documento de identidad, identidad de género, antecedente de donación de sangre, tipo de prueba diagnóstica realizada (Western Blot, carga viral, prueba rápida, ELISA), fecha y valor del resultado, estado clínico actual (VIH, SIDA o fallecido), y presencia de enfermedades oportunistas o definitorias de SIDA como candidiasis, tuberculosis pulmonar y extrapulmonar, cáncer cervical, citomegalovirus, retinitis, encefalitis, neumonía por Pneumocystis, linfomas, criptococosis, sarcoma de Kaposi, toxoplasmosis cerebral, hepatitis B y C, meningitis, entre otras. Se utiliza para imprimir o visualizar la ficha de notificación obligatoria ante el sistema de vigilancia epidemiológica nacional, consultando la tabla HCFICHA850.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila850';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila850';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y transforma la información de una ficha de notificación SIVIGILA (código 850, VIH/SIDA) convirtiendo códigos numéricos en marcas legibles para impresión/visualización del formato.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila850';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA850 con el IDFICHANOTIFICACION recibido; El identificador de ficha debe ser entero válido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila850';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La consulta es de solo lectura, no modifica datos; Solo retorna información de una única ficha (filtrada por IDFICHANOTIFICACION); Los códigos numéricos almacenados se traducen siempre a marcas ''X'' o siglas estandarizadas para presentación; La fecha de resultado se entrega en formato dd/mm/yyyy (estilo 103); El parámetro @NumFicha se recibe pero no se usa en el filtro', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila850';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación SIVIGILA; VIH/SIDA; Mecanismo probable de transmisión; Tipo de documento de identidad; Identidad de género; Donación de sangre; Tipo de prueba diagnóstica (Western Blot, Carga viral, Prueba rápida, ELISA); Estado clínico (VIH/SIDA/Muerto); Enfermedades oportunistas (candidiasis, tuberculosis, cáncer cervical, citomegalovirus, herpes zóster, neumonía, criptococosis, sarcoma de Kaposi, toxoplasmosis cerebral, hepatitis B y C, meningitis, etc.); Carga viral; Coinfecciones', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila850';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA850: Cuando IDFICHANOTIFICACION coincide con el parámetro de entrada, retorna las columnas de la ficha transformadas (códigos a ''X'' o etiquetas)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila850';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MECAPROBA entre ''1'' y ''13'' → Marca con ''X'' la columna correspondiente al mecanismo probable de transmisión (Heterosexual, Homosexual, Bisexual, Materno, Transfusión, Inyección, Accidente, Trasplante, Piercing, Hemodiálisis, Tatuajes, Otros, Acupuntura); si TIPOID entre ''1'' y ''10'' → Traduce el código a sigla de documento: 1=RC, 2=TI, 3=CC, 4=CE, 5=PA, 6=MS, 7=AS, 8=PE, 9=CN, 10=PT; si IDENTGEN ''1'', ''2'' o ''3'' → Marca con ''X'' Masculino, Femenino o Transgénero respectivamente; si DONOSANGRE = ''1'' o ''0'' → Marca ''DonoSi'' o ''DonoNo'' respectivamente; si TIPOPRUEBA entre ''1'' y ''4'' → Marca con ''X'' tipo de prueba: 1=Western, 2=Carga, 3=Rápida, 4=Elisa; si ESTADCLINI ''1'', ''2'' o ''3'' → Marca estado clínico: VIH, SIDA o MUERTO; si Cualquier columna de enfermedad oportunista (CANDIESO, CANDIVIA, TUBERPULM, CANCERCERV, etc.) = ''1'' → Marca con ''X'' la enfermedad/condición asociada presente en el paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila850';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA850', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila850';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila850';
-- GO

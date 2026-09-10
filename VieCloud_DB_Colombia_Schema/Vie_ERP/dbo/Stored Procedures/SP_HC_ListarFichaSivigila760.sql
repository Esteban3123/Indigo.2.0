-- Stored Procedure

-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,31-10-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila760]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 

	Select
	CASE DOLORCUELLO WHEN '1' THEN 'X' END AS 'DOLORCUELLO Si', CASE DOLORCUELLO WHEN '2' THEN 'X' END AS 'DOLORCUELLO No', CASE DOLORCUELLO WHEN '3' THEN 'X' END AS 'DOLORCUELLO Desconocido',
	CASE DOLORGARGANTA WHEN '1' THEN 'X' END AS 'DOLORGARGANTA Si', CASE DOLORGARGANTA WHEN '2' THEN 'X' END AS 'DOLORGARGANTA No', CASE DOLORGARGANTA WHEN '3' THEN 'X' END AS 'DOLORGARGANTA Desconocido',
	CASE IMPOABRIR WHEN '1' THEN 'X' END AS 'IMPOABRIR Si', CASE IMPOABRIR WHEN '2' THEN 'X' END AS 'IMPOABRIR No', CASE IMPOABRIR WHEN '3' THEN 'X' END AS 'IMPOABRIR Desconocido',
	CASE DISFAGIA WHEN '1' THEN 'X' END AS 'DISFAGIA Si', CASE DISFAGIA WHEN '2' THEN 'X' END AS 'DISFAGIA No', CASE DISFAGIA WHEN '3' THEN 'X' END AS 'DISFAGIA Desconocido',
	CASE CONVULSIONES WHEN '1' THEN 'X' END AS 'CONVULSIONES Si', CASE CONVULSIONES WHEN '2' THEN 'X' END AS 'CONVULSIONES No', CASE CONVULSIONES WHEN '3' THEN 'X' END AS 'CONVULSIONES Desconocido',
	CASE CONTRAMUSC WHEN '1' THEN 'X' END AS 'CONTRAMUSC Si', CASE CONTRAMUSC WHEN '2' THEN 'X' END AS 'CONTRAMUSC No', CASE CONTRAMUSC WHEN '3' THEN 'X' END AS 'CONTRAMUSC Desconocido',
	CASE RIGIDEZMUSC WHEN '1' THEN 'X' END AS 'RIGIDEZMUSC Si', CASE RIGIDEZMUSC WHEN '2' THEN 'X' END AS 'RIGIDEZMUSC No', CASE RIGIDEZMUSC WHEN '3' THEN 'X' END AS 'RIGIDEZMUSC Desconocido',
	CASE ESPASGENER WHEN '1' THEN 'X' END AS 'ESPASGENER Si', CASE ESPASGENER WHEN '2' THEN 'X' END AS 'ESPASGENER No', CASE ESPASGENER WHEN '3' THEN 'X' END AS 'ESPASGENER Desconocido',
	CASE RIGIDEZNUCA WHEN '1' THEN 'X' END AS 'RIGIDEZNUCA Si', CASE RIGIDEZNUCA WHEN '2' THEN 'X' END AS 'RIGIDEZNUCA No', CASE RIGIDEZNUCA WHEN '3' THEN 'X' END AS 'RIGIDEZNUCA Desconocido',
	CASE AFECTNERVIOS WHEN '1' THEN 'X' END AS 'AFECTNERVIOS Si', CASE AFECTNERVIOS WHEN '2' THEN 'X' END AS 'AFECTNERVIOS No', CASE AFECTNERVIOS WHEN '3' THEN 'X' END AS 'AFECTNERVIOS Desconocido',
	CASE TRISMUS WHEN '1' THEN 'X' END AS 'TRISMUS Si', CASE TRISMUS WHEN '2' THEN 'X' END AS 'TRISMUS No', CASE TRISMUS WHEN '3' THEN 'X' END AS 'TRISMUS Desconocido',
	CASE OPISTOTONOS WHEN '1' THEN 'X' END AS 'OPISTOTONOS Si', CASE OPISTOTONOS WHEN '2' THEN 'X' END AS 'OPISTOTONOS No', CASE OPISTOTONOS WHEN '3' THEN 'X' END AS 'OPISTOTONOS Desconocido',
	CASE FIEBRE WHEN '1' THEN 'X' END AS 'FIEBRE Si', CASE FIEBRE WHEN '2' THEN 'X' END AS 'FIEBRE No', CASE FIEBRE WHEN '3' THEN 'X' END AS 'FIEBRE Desconocido',
	Rtrim(OTRO) As 'OTRO'

	From HCFICHA760 where IDFICHANOTIFICACION  = @IdFicha

END

--select * from hcfichanotificacion where id = '8'
--select * from HCFICHA760
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera los síntomas clínicos registrados en la ficha SIVIGILA 760 correspondiente a notificación de tétanos. Consulta la tabla HCFICHA760 filtrando por el identificador de la ficha de notificación y devuelve cada síntoma (dolor de cuello, dolor de garganta, imposibilidad de abrir la boca, disfagia, convulsiones, contracturas musculares, rigidez muscular, espasmos generalizados, rigidez de nuca, afectación de nervios, trismus, opistótonos, fiebre y otros) transformado en columnas con valor ''X'' según si la respuesta fue Sí, No o Desconocido. Se utiliza para imprimir o visualizar el formulario estandarizado de vigilancia epidemiológica SIVIGILA evento 760 (tétanos) dentro de la historia clínica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila760';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila760';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea los síntomas clínicos registrados en la ficha Sivigila 760, traduciendo los códigos 1/2/3 a marcas Si/No/Desconocido para presentación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila760';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA760 cuyo identificador de ficha de notificación coincida con el parámetro recibido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila760';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada síntoma se reporta en tres columnas mutuamente excluyentes (Si/No/Desconocido) marcando ''X'' según el valor 1/2/3 almacenado; Valores fuera de {1,2,3} resultan en NULL en las tres columnas del síntoma; El campo OTRO se devuelve sin espacios a la derecha (RTRIM)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila760';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila; Síntomas clínicos (dolor de cuello, disfagia, convulsiones, rigidez muscular, trismus, opistótonos, fiebre, etc.); Vigilancia epidemiológica (ficha 760 - Tétanos accidental)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila760';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA760: Devuelve un único registro de HCFICHA760 filtrado por IDFICHANOTIFICACION = @IdFicha, con cada síntoma expandido a tres columnas (Si/No/Desconocido) marcadas con ''X'' según el código almacenado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila760';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA760', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila760';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila760';
-- GO

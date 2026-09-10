-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,08-05-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila110]
(
  @IdFicha as Int,
  @NumFicha as Int
)

AS
BEGIN
  SET NOCOUNT ON;
  			
			Select
				 CASE RNTIPDOC  WHEN 1 THEN 'X' END AS 'Registro civil', CASE RNTIPDOC  WHEN 2 THEN 'X' END AS 'Menor sin identificación' , CASE RNTIPDOC  WHEN 3 THEN 'X' END AS 'Permiso Especial Permanencia', CASE RNTIPDOC  WHEN 4 THEN 'X' END AS 'Certificado Nacido Vivo'
				 ,RTRIM(RNIPCODPACI) as 'Identificacion'
				 ,convert(varchar(10),RNFECHNACI,103) as 'FechaNacimientos'
				 ,RNEDAD as 'Edad'
				 ,CASE RNSEXO  WHEN '1' THEN 'X' END AS 'Masculino',CASE RNSEXO  WHEN '2' THEN 'X' END AS 'Femenino' 
				 ,RNPESO as 'Peso'
				 ,RNTALLA as 'Talla'
				 ,GESTPARTO as 'GestacionParto'
				 ,CASE CLASIFPESO  WHEN '1' THEN 'X' END AS 'Bajopesoalnacer',CASE CLASIFPESO  WHEN '2' THEN 'X' END AS 'Muybajopesonacer'   
				 ,CASE SITIOPARTO  WHEN '1' THEN 'X' END AS 'Instituciónsalud',CASE SITIOPARTO  WHEN '2' THEN 'X' END AS 'Domicilio',CASE SITIOPARTO  WHEN '3' THEN 'X' END AS 'Otro',CASE SITIOPARTO  WHEN '4' THEN 'X' END AS 'Via Publica'
				 ,CASE MULTIPLIEMBARA  WHEN '1' THEN 'X' END AS 'Simple',CASE MULTIPLIEMBARA  WHEN '2' THEN 'X' END AS 'Doble',CASE MULTIPLIEMBARA  WHEN '3' THEN 'X' END AS 'Triple'
     			 ,EMBARAZOPREVIOS AS 'EmbarazosPrevios'
				 ,NUMHIJOS as 'NumerosHijos'
				 ,CASE NIVELEDUCATIVO  WHEN 1 THEN 'X' END AS 'NEducativo_Primaria', CASE NIVELEDUCATIVO  WHEN 2 THEN 'X' END AS 'NEducativo_Secundaria', CASE NIVELEDUCATIVO  WHEN 3 THEN 'X' END AS 'NEducativo_Tecnico', CASE NIVELEDUCATIVO  WHEN 4 THEN 'X' END AS 'NEducativo_Ninguno'
				 , VERSION AS 'VERSION' 
				 , JSON AS 'JSON'

			 From 
				 HCFICHA110 
		
			Where 
				 IDFICHANOTIFICACION  = @IdFicha
		
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que consulta y devuelve la información de la ficha de notificación SIVIGILA sección 110, correspondiente a datos perinatales y antropométricos del recién nacido o del evento notificado. Recupera campos como tipo de documento, cédula o identificación del paciente, fecha de nacimiento, edad, sexo, peso, talla, semanas de gestación al parto, clasificación del peso al nacer (bajo peso, muy bajo peso), lugar del parto (institución de salud, domicilio, vía pública, otro), tipo de embarazo (simple, doble, triple), embarazos previos, número de hijos vivos y nivel educativo de la madre. Existe para imprimir o exportar la ficha oficial de vigilancia epidemiológica SIVIGILA requerida por el sistema de salud pública colombiano, filtrando por el identificador único de la ficha de notificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila110';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila110';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea los datos de una ficha de notificación del Sivigila (versión 110) para reporte/visualización, traduciendo códigos a marcas tipo ''X'' por categoría.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila110';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA110 cuyo IDFICHANOTIFICACION coincida con el parámetro de entrada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila110';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retorna información de una ficha (filtrada por IDFICHANOTIFICACION).; Las columnas categóricas son mutuamente excluyentes: solo una recibe ''X'' por grupo de CASE.; La fecha de nacimiento se devuelve siempre en formato dd/mm/yyyy (estilo 103).; El parámetro @NumFicha no participa en el filtrado (no se usa en la consulta).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila110';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila; Tipo de documento del paciente; Recién nacido; Peso y talla al nacer; Gestación / parto; Clasificación de bajo peso al nacer; Sitio de parto; Embarazo múltiple; Embarazos previos; Número de hijos; Nivel educativo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila110';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA110: Cuando IDFICHANOTIFICACION = @IdFicha, retorna las columnas de la ficha transformando códigos (tipo documento, sexo, clasificación de peso, sitio de parto, multiplicidad de embarazo, nivel educativo) en marcas ''X'' según el valor.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila110';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RNTIPDOC = 1/2/3/4 → Marca con ''X'' la columna correspondiente: Registro civil, Menor sin identificación, Permiso Especial Permanencia o Certificado Nacido Vivo.; si RNSEXO = ''1'' o ''2'' → Marca con ''X'' la columna Masculino o Femenino respectivamente.; si CLASIFPESO = ''1'' o ''2'' → Marca con ''X'' Bajopesoalnacer o Muybajopesonacer.; si SITIOPARTO = ''1''/''2''/''3''/''4'' → Marca con ''X'' Institución de salud, Domicilio, Otro o Vía pública.; si MULTIPLIEMBARA = ''1''/''2''/''3'' → Marca con ''X'' Simple, Doble o Triple.; si NIVELEDUCATIVO = 1/2/3/4 → Marca con ''X'' Primaria, Secundaria, Técnico o Ninguno.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila110';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA110', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila110';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila110';
-- GO

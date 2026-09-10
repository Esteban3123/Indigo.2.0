-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,08-05-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila560]
(
  @IdFicha as Int
  )

AS
BEGIN
  SET NOCOUNT ON;
    
			Select 
				Rtrim(GESTACION) As 'Gestaciones',Rtrim(PARTOSVAG) As 'Partos Vaginales',Rtrim(CESAREAS) As 'Cesareas',Rtrim(ABORTOS) As 'Abortos',Rtrim(VIVOS) As 'Vivos',Rtrim(MUERTOS) As 'Vivos', 
				Rtrim(NUMCPN) As 'Numero C_P_N',Rtrim(SEMANACPN) As 'Semana Inicio C_P_N',	 
				CASE TIPOPARTO  WHEN '1' THEN 'X' END AS 'Vaginal',CASE TIPOPARTO  WHEN '2' THEN 'X' END AS 'Cesárea',CASE TIPOPARTO  WHEN '3' THEN 'X' END AS 'Instrumentado',CASE TIPOPARTO  WHEN '4' THEN 'X' END AS 'Ignorado',CASE TIPOPARTO  WHEN '5' THEN 'X' END AS 'No nació', 
				FECHAPARTO As 'Fecha Parto', 
				CASE SITIODEFUN  WHEN '1' THEN 'X' END AS 'Domicilio Defuncion',CASE SITIODEFUN  WHEN '2' THEN 'X' END AS 'Otr Sitio Defusion',CASE SITIODEFUN  WHEN '3' THEN 'X' END AS 'Baja complejidad',CASE SITIODEFUN  WHEN '4' THEN 'X' END AS 'Mediana complejidad',CASE SITIODEFUN  WHEN '5' THEN 'X' END AS 'Alta complejidad',CASE SITIODEFUN  WHEN '6' THEN 'X' END AS 'UCI',CASE SITIODEFUN  WHEN '7' THEN 'X' END AS 'Traslado interinstituciona',CASE SITIODEFUN  WHEN '8' THEN 'X' END AS 'Traslado domicilio IPS', 
				CASE PARTOATENDI  WHEN '1' THEN 'X' END AS 'Parto Médico general',CASE PARTOATENDI  WHEN '2' THEN 'X' END AS 'Parto Médico obstetra',CASE PARTOATENDI  WHEN '3' THEN 'X' END AS 'Parto Enfermera',CASE PARTOATENDI  WHEN '4' THEN 'X' END AS ' Parto Auxiliar enfermería',CASE PARTOATENDI  WHEN '5' THEN 'X' END AS 'Parto Promotor',CASE PARTOATENDI  WHEN '6' THEN 'X' END AS 'parto Partera',CASE PARTOATENDI  WHEN '7' THEN 'X' END AS 'Parto Otro',CASE PARTOATENDI  WHEN '8' THEN 'X' END AS 'Ella misma', 
				CASE SITIOPARTO  WHEN '1' THEN 'X' END AS 'Institucional',CASE SITIOPARTO  WHEN '2' THEN 'X' END AS 'Domicilio Parto',CASE SITIOPARTO  WHEN '3' THEN 'X' END AS 'Otro Sitio Parto', 
				CASE NIVELATEN2  WHEN '1' THEN 'X' END AS 'Nivel Baja complejidad 2 ',CASE NIVELATEN2  WHEN '2' THEN 'X' END AS 'Nivel Mediana complejidad 2 ',CASE NIVELATEN2  WHEN '3' THEN 'X' END AS 'Nivel Alta complejidad 2', 
				CASE MOMENTOMUER WHEN '1' THEN 'X' END AS 'Anteparto',CASE MOMENTOMUER WHEN '2' THEN 'X' END AS 'Intraparto',CASE MOMENTOMUER WHEN '3' THEN 'X' END AS 'Prealta en postparto',CASE MOMENTOMUER WHEN '4' THEN 'X' END AS 'Postalta en postparto',CASE MOMENTOMUER WHEN '5' THEN 'X' END AS 'Reingreso en postparto',CASE MOMENTOMUER WHEN '6' THEN 'X' END AS 'No aplica', 
				Rtrim(EDADGESTACIO) As 'Edad Gestacional', 
				Rtrim(EDADNEONATAL) As 'Edad Neonatal Muerte', 
				Rtrim(PESONACER) As 'Peso al nacer', 
				Rtrim(TALLANACER) As 'Talla al Nacer', 
				CASE SEXO  WHEN '1' THEN 'X' END AS 'Masculino',CASE SEXO  WHEN '2' THEN 'X' END AS 'Femenino',CASE SEXO  WHEN '3' THEN 'X' END AS 'Indeterminado', 
				CASE CAUSAMUERTE  WHEN '1' THEN 'X' END AS 'Historia clínica',CASE CAUSAMUERTE  WHEN '2' THEN 'X' END AS 'Autopsia verbal',CASE CAUSAMUERTE  WHEN '3' THEN 'X' END AS 'Necropsia', 
				'' As 'Causa Muerte Agrupadas', 
				'' As 'Causa Neoatales', 
				VERSION AS 'VERSION', 
				JSON AS 'JSON' 
		
			From 
				HCFICHA560 C 

		   Where 
				IDFICHANOTIFICACION = @IdFicha 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y retorna los datos obstétricos y perinatales registrados en la ficha 560 del SIVIGILA para una notificación específica, identificada por el identificador de ficha (@IdFicha). Recupera antecedentes gineco-obstétricos de la madre (gestaciones, partos vaginales, cesáreas, abortos, hijos vivos y muertos, controles prenatales y semana de inicio de CPN), información del parto (tipo, fecha, sitio, personal que atendió y nivel de atención) y datos del recién nacido o mortinato (edad gestacional, edad neonatal al momento de muerte, peso y talla al nacer, sexo, sitio de defunción, momento de la muerte y fuente de la causa de muerte). Sirve para generar el reporte impreso o digital de la ficha de notificación obligatoria 560 de mortalidad perinatal y neonatal tardía exigida por el SIVIGILA, componiendo visualmente cada campo codificado en opciones marcadas con ''X'' según corresponda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila560';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila560';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea los datos clínicos de una ficha de notificación Sivigila (mortalidad perinatal/neonatal) para su visualización, traduciendo códigos numéricos a marcas tipo casilla.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila560';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA560 cuyo IDFICHANOTIFICACION coincida con el identificador recibido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila560';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los códigos numéricos de catálogos se traducen a una marca ''X'' en la columna correspondiente; valores fuera del dominio quedan en NULL; Los campos de texto se entregan sin espacios finales (RTRIM); Las columnas ''Causa Muerte Agrupadas'' y ''Causa Neoatales'' siempre se devuelven como cadena vacía', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila560';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha Sivigila; Notificación epidemiológica; Mortalidad perinatal y neonatal; Antecedentes obstétricos (gestaciones, partos, cesáreas, abortos); Control prenatal (CPN); Tipo y sitio de parto; Atención del parto; Nivel de complejidad de atención; Momento de la muerte; Datos del recién nacido (edad gestacional, neonatal, peso, talla, sexo); Fuente de causa de muerte', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila560';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA560: Devuelve un único conjunto con los datos obstétricos, del parto, defunción y neonatales asociados a la ficha filtrada por IDFICHANOTIFICACION', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila560';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPOPARTO ∈ {1..5} → Marca con ''X'' la categoría correspondiente: Vaginal, Cesárea, Instrumentado, Ignorado o No nació; si SITIODEFUN ∈ {1..8} → Marca el sitio de defunción: Domicilio, Otro sitio, Baja/Mediana/Alta complejidad, UCI, Traslado interinstitucional o Traslado a domicilio IPS; si PARTOATENDI ∈ {1..8} → Marca quién atendió el parto: Médico general, Médico obstetra, Enfermera, Auxiliar, Promotor, Partera, Otro o Ella misma; si SITIOPARTO ∈ {1..3} → Marca el sitio del parto: Institucional, Domicilio u Otro; si NIVELATEN2 ∈ {1..3} → Marca el nivel de atención: Baja, Mediana o Alta complejidad; si MOMENTOMUER ∈ {1..6} → Marca el momento de la muerte: Anteparto, Intraparto, Prealta/Postalta/Reingreso en postparto o No aplica; si SEXO ∈ {1..3} → Marca Masculino, Femenino o Indeterminado; si CAUSAMUERTE ∈ {1..3} → Marca fuente de la causa de muerte: Historia clínica, Autopsia verbal o Necropsia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila560';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA560', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila560';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila560';
-- GO

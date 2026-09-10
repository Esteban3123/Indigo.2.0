
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila900]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
		
		Select
				
				CASE SOSPECHAEVENTO  WHEN '1' THEN 'X' END AS 'Síndrome_mano_pie_boca',
				CASE SOSPECHAEVENTO WHEN '2' THEN 'X' END AS 'Conjuntivitis',
				CASE SOSPECHAEVENTO WHEN '3' THEN 'X' END AS 'Accidente_escorpionico/Picadura',
				CASE SOSPECHAEVENTO WHEN '4' THEN 'X' END AS 'Brucelosis',
				CASE SOSPECHAEVENTO WHEN '5' THEN 'X' END AS 'Hepatitis',
				CASE SOSPECHAEVENTO WHEN '6' THEN 'X' END AS 'Otros',
				OTROSEVENTOS as 'Otros_eventos',
				VERSION AS 'VERSION', 
				JSON AS 'JSON' 
	
			From 
				HCFICHA900 

			Where 
				IDFICHANOTIFICACION  = @IdFicha 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y devuelve el detalle de una ficha SIVIGILA 900 registrada en historia clínica, identificada por su ID interno. Traduce el código numérico del evento sospechoso a su nombre descriptivo (síndrome mano-pie-boca, conjuntivitis, accidente escorpiónico, brucelosis, hepatitis u otros), y retorna además el campo de otros eventos, la versión del formulario y el contenido completo en formato JSON. Se usa para visualizar o imprimir la ficha de notificación obligatoria de eventos de salud pública (SIVIGILA) diligenciada en la historia clínica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila900';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila900';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los datos de la ficha de notificación SIVIGILA código 900, traduciendo el código de sospecha de evento a marcas ''X'' por tipo de evento (mano-pie-boca, conjuntivitis, accidente escorpiónico, brucelosis, hepatitis, otros).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila900';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA900 con IDFICHANOTIFICACION igual al identificador recibido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila900';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo uno de los seis eventos sospechosos puede quedar marcado con ''X'' en una misma fila, según el valor único de SOSPECHAEVENTO; Los eventos no coincidentes con los códigos 1-6 quedan en NULL en sus respectivas columnas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila900';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación SIVIGILA 900; Síndrome mano-pie-boca; Conjuntivitis; Accidente escorpiónico/Picadura; Brucelosis; Hepatitis; Eventos de notificación en salud pública', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila900';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA900: Cuando IDFICHANOTIFICACION coincide con el parámetro de ficha, retorna columnas de marcado por evento sospechoso, otros eventos, versión y JSON de la ficha', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila900';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si SOSPECHAEVENTO = ''1'' → Marca ''X'' en Síndrome_mano_pie_boca; si SOSPECHAEVENTO = ''2'' → Marca ''X'' en Conjuntivitis; si SOSPECHAEVENTO = ''3'' → Marca ''X'' en Accidente escorpiónico/Picadura; si SOSPECHAEVENTO = ''4'' → Marca ''X'' en Brucelosis; si SOSPECHAEVENTO = ''5'' → Marca ''X'' en Hepatitis; si SOSPECHAEVENTO = ''6'' → Marca ''X'' en Otros', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila900';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA900', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila900';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila900';
-- GO

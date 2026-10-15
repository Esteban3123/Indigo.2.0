
CREATE PROCEDURE [dbo].[SPREP_HC_Ingreso_ListarIngresos]
(
@NumeroIngreso nChar(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
 SELECT IAUTORIZA AS 'NUMERO AUTORIZACION INGRESO',ITIPORIES AS 'TIPO DE RIESGO',ITIPORIES AS 'TIPO DE RIESGO',IFECHAING AS 'FECHA DEL INGRESO',CASE ICAUSAING WHEN 1 THEN 'Heridos en combate' WHEN 2 THEN 'Enfermedad profesional' WHEN 3 THEN 'Enfermedad general adulto' WHEN 4 THEN 'Enfermedad general pediatria' WHEN 5 THEN 'Odontología' WHEN 6 THEN 'Accidente de transito' WHEN 7 THEN 'Catastrofe/Fisalud' WHEN 8 THEN 'Quemados' WHEN 9 THEN 'Maternidad' WHEN 10 THEN 'Accidente Laboral' WHEN 11 THEN 'Cirugia Programada' END AS 'CAUSA DEL INGRESO'
                 
FROM ADINGRESO WITH(NOLOCK)

WHERE NUMINGRES = @NumeroIngreso

END
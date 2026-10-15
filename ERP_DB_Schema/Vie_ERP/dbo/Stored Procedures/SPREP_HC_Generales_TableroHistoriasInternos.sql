
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_TableroHistoriasInternos]
(
@CodigoPaciente Varchar(25),
@NumeroFolio nChar(10),
@NumeroIngreso Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
          
SELECT FECHISPAC AS 'FECHA HISTORIA CLINICA',CODCENATE AS 'CODIGO CENTRO DE ATENCION', UFUCODIGO AS 'CODIGO UNIDAD FUNCIONAL',INDICAMED AS 'INDICACIONES MEDICAS',
       CODUSUARI AS 'USUARIO QUE VISA LA HISTORIA', ESTAFOLIO AS ESTADO
FROM HCHISPACI WITH(NOLOCK)
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMEFOLIO=@NumeroFolio

END
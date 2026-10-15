
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_DatosMedicosGeneral]
(
@CodigoPaciente Varchar(25),
@NumeroFolio Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    
SELECT RTRIM(NOMMEDICO) AS 'NOMBRE MEDICO',RTRIM(DESESPECI) AS 'DESCRIPCION DE LA ESPECIALIDAD',TARJETAPR AS 'TARJETA PROFESIONAL',MEDIFIRMA AS 'FIRMA PROFESIONAL'
FROM HCFIRMFOL A With(Nolock) 
INNER JOIN INPROFSAL AS B With(Nolock)  ON A.CODPROCRE=B.CODPROSAL 
INNER JOIN INESPECIA AS C With(Nolock)  ON B.CODESPEC1=C.CODESPECI

WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMEFOLIO=@NumeroFolio
                  
END
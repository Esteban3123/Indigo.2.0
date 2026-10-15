

CREATE PROCEDURE [dbo].[SP_HC_ListarSolicitudesEmergenciaTodo]
(
	@Paciente AS VARCHAR(25),
	@Ingreso AS CHAR(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 

	SELECT 
		Convert(BIT,0) As 'Seleccion', CODCONSEC AS 'Consecutivo', CONCAT('Folio de emergencia: ', NUMCODAZU) AS 'Folio'
	FROM
		HCCODAZUC
	WHERE
		IPCODPACI = @Paciente AND NUMINGRES= @Ingreso AND JUSCODAZU IS NULL;


	SELECT -- MEDICAMENTOS E INSUMOS
		    A.CODCONSEC AS 'Consecutivo', 'Medicamentos e insumos' AS 'Tipo', A.FECHAORDE AS 'FechaSolicitud', B.CODPRODUC AS 'Codigo',
		    RTRIM(DESPRODUC) AS 'Descripcion', '' AS 'DescripcionRelacionada', Indications, CANPEDPRO AS 'Pedida', 0 AS 'Utilizada' 
	    FROM dbo.HCCODAZUC A 
		    INNER JOIN dbo.HCCODAZUD B ON A.CODCONSEC=b.CODCONSEC 
		    INNER JOIN dbo.IHLISTPRO C ON B.CODPRODUC=C.CODPRODUC
	    WHERE 
		    IPCODPACI= @Paciente AND NUMINGRES= @Ingreso AND JUSCODAZU IS NULL
	    UNION -- IMAGENES
	    SELECT 
		    C.CODCONSEC AS 'Consecutivo', 'Imágenes diagnósticas' AS 'Tipo', C.FECHAORDE AS 'FechaSolicitud', A.CODSERIPS AS 'Codigo',
		    RTRIM(DESSERIPS) AS 'Descripcion', CD.Code + ' - ' + CD.Name AS 'DescripcionRelacionada', '' AS 'Indications', A.CANSERIPS AS 'Pedida', 0 AS 'Utilizada'
	    FROM dbo.HCORDIMAG A 
		    INNER JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
		    INNER JOIN dbo.HCCODAZUC C ON A.IPCODPACI=C.IPCODPACI And A.NUMINGRES=C.NUMINGRES And A.NUMEFOLIO=C.NUMCODAZU 
		    LEFT JOIN contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
		    LEFT JOIN contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId 
	    WHERE 
		    A.IPCODPACI= @Paciente AND A.NUMINGRES= @Ingreso AND MANEXTPRO = 0 AND IDETIPHIS='CODIGOAZU' AND JUSCODAZU IS NULL
	    UNION -- LABORATORIOS
	    SELECT 
		    C.CODCONSEC AS 'Consecutivo', 'Laboratorios' AS 'Tipo', C.FECHAORDE AS 'FechaSolicitud', A.CODSERIPS AS 'Codigo',
		    RTRIM(DESSERIPS) AS 'Descripcion', CD.Code + ' - ' + CD.Name AS 'DescripcionRelacionada', '' AS 'Indications', A.CANSERIPS AS 'Pedida', 0 AS 'Utilizada'
	     FROM dbo.HCORDLABO A 
		    INNER JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
		    INNER JOIN dbo.HCCODAZUC C ON A.IPCODPACI=C.IPCODPACI And A.NUMINGRES=C.NUMINGRES And A.NUMEFOLIO=C.NUMCODAZU 
		    LEFT JOIN contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
		    LEFT JOIN contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId 
	    WHERE 
		    A.IPCODPACI= @Paciente AND A.NUMINGRES= @Ingreso AND MANEXTPRO=0 AND IDETIPHIS='CODIGOAZU' AND JUSCODAZU IS NULL
	    UNION -- PATOLOGIAS
	    SELECT 
		    C.CODCONSEC AS 'Consecutivo', 'Patologias' AS 'Tipo', C.FECHAORDE AS 'FechaSolicitud', A.CODSERIPS AS 'Codigo',
		    RTRIM(DESSERIPS) AS 'Descripcion', CD.Code + ' - ' + CD.Name AS 'DescripcionRelacionada', '' AS 'Indications', A.CANSERIPS AS 'Pedida', 0 AS 'Utilizada'
	     FROM dbo.HCORDPATO A 
		    INNER JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
		    INNER JOIN dbo.HCCODAZUC C ON A.IPCODPACI=C.IPCODPACI And A.NUMINGRES=C.NUMINGRES And A.NUMEFOLIO=C.NUMCODAZU 
		    LEFT JOIN contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
		    LEFT JOIN contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId 
	    WHERE 
		    A.IPCODPACI= @Paciente AND A.NUMINGRES= @Ingreso AND MANEXTPRO=0 AND IDETIPHIS='CODIGOAZU' AND JUSCODAZU IS NULL
	    UNION -- HEMOCOMPONENTES
	    SELECT 
		    A.CODCONSEC AS 'Consecutivo', 'Hemocomponentes' AS 'Tipo', A.FECHAORDE AS 'FechaSolicitud', COM.CODCOMSAM AS 'Codigo',
		    RTRIM(COM.DESCOMSAM) AS 'Descripcion', '' AS 'DescripcionRelacionada', '' AS 'Indications', COUNT(*) AS 'Pedida', 0 AS 'Utilizada'
	    FROM dbo.HCORHEMBOL BOLSA 
		    INNER JOIN dbo.HCCOMSAN COM ON COM.ID = BOLSA.COMSAMID 
		    INNER JOIN dbo.HCORHEMCO HEM  ON HEM.ID = BOLSA.HCORHEMCOID 
		    INNER JOIN dbo.HCCODAZUC A ON A.NUMCODAZU = HEM.NUMEFOLIO AND HEM.IPCODPACI = A.IPCODPACI AND HEM.IDETIPHIS = 'CODIGOAZU' AND HEM.NUMINGRES = A.NUMINGRES 
	    WHERE 
		    A.IPCODPACI = @Paciente AND A.NUMINGRES =  @Ingreso AND JUSCODAZU IS NULL
	    GROUP BY A.CODCONSEC, A.NUMCODAZU, A.FECHAORDE, COM.CODCOMSAM, COM.DESCOMSAM
	
END
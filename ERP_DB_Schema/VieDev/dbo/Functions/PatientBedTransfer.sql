-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE FUNCTION [dbo].[PatientBedTransfer]
(
    -- Add the parameters for the function here
    @PACIENTE varchar(25), @INGRESO Varchar(25)
)
RETURNS @Resultado Table(
	CODICAMAS varchar(25),
	Cama varchar(25),
	UFUCODIGO varchar(25),
	FechaTraslado DateTime,
	CodigoCamaTraslado varchar(25),
	CamaTraslado varchar(25),
	DEVOLUTIVO Bit,
	RECHAZODEV Bit,
	BedType Int,
	TypeTransfer Int
)
AS
BEGIN;
    WITH Numeracion AS (
			SELECT B.CODICAMAS, B.DESCCAMAS, B.UFUCODIGO, A.FECINIEST, B.CAMDEVMED AS DEVOLUTIVO, B.CAMRECDEV AS RECHAZODEV, ISNULL(B.BedType, 0) AS BedType, ISNULL(B.TypeTransfer,0) AS TypeTransfer,
			ROW_NUMBER() OVER (ORDER BY A.FECINIEST) AS NumeroFila
			FROM CHREGESTA A	
			INNER JOIN CHCAMASHO B on A.CODICAMAS = B.CODICAMAS
			WHERE IPCODPACI = @PACIENTE AND NUMINGRES = @INGRESO AND A.REGESTADO = 1
		),
		Conteo as (
			SELECT COUNT(*) AS Total FROM Numeracion
		),
		DATOS AS (
			SELECT 
				(SELECT CODICAMAS FROM Numeracion WHERE NumeroFila = 1) AS CODICAMAS,
				(SELECT DESCCAMAS FROM Numeracion WHERE NumeroFila = 1) AS Cama,
				(SELECT UFUCODIGO FROM Numeracion WHERE NumeroFila = 1) AS UFUCODIGO,
				(SELECT FECINIEST FROM Numeracion WHERE NumeroFila = 2) AS FechaTraslado,
				(SELECT CODICAMAS FROM Numeracion WHERE NumeroFila = 2) AS CodigoCamaTraslado,
				(SELECT DESCCAMAS FROM Numeracion WHERE NumeroFila = 2) AS CamaTraslado,
				(SELECT DEVOLUTIVO FROM Numeracion WHERE NumeroFila = 1) AS DEVOLUTIVO,
				(SELECT RECHAZODEV FROM Numeracion WHERE NumeroFila = 1) AS RECHAZODEV,
				(SELECT BedType FROM Numeracion WHERE NumeroFila = 2) AS BedType,
				(SELECT TypeTransfer FROM Numeracion WHERE NumeroFila = 1) AS TypeTransfer
			From Conteo Where Total = 2
			)
		INSERT INTO @Resultado
			SELECT * FROM DATOS WHERE DEVOLUTIVO = 1 OR RECHAZODEV = 1 OR (BedType = 2 AND TypeTransfer = 3);

    -- Return the result of the function
    RETURN;
END;
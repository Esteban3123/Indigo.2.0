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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que determina el traslado de cama de un paciente durante un ingreso hospitalario específico. Recibe la cédula del paciente y el número de ingreso, y consulta el historial de estados del ingreso (CHREGESTA) junto con el maestro de camas (CHCAMASHO) para identificar si el paciente tuvo exactamente dos asignaciones de cama durante el ingreso. Retorna la cama de origen con su unidad funcional, la fecha y cama de destino del traslado, y solo devuelve resultado cuando la cama es devolutiva, tiene rechazo de devolución, o corresponde a un tipo de cama y traslado específico (cama tipo 2 con traslado tipo 3). Se usa para identificar movimientos de traslado entre camas en hospitalización, incluyendo casos de devolución o rechazo de devolución de cama.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'PatientBedTransfer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'PatientBedTransfer';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los datos de la cama actual y la cama de traslado de un paciente cuando existen exactamente dos registros de estancia activa y aplica reglas específicas de devolutividad o tipo de traslado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PatientBedTransfer';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener registros de estancia activa (REGESTADO = 1) en CHREGESTA para el ingreso indicado.; Debe existir correspondencia entre la cama de la estancia (CODICAMAS) y el catálogo de camas CHCAMASHO.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PatientBedTransfer';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera estancias con REGESTADO = 1 (estancia activa/vigente).; Solo retorna información cuando el paciente ha tenido exactamente dos estancias activas (cama actual + una cama de traslado).; La cama origen corresponde a la estancia más antigua (NumeroFila = 1 ordenada por FECINIEST) y la cama de traslado a la siguiente (NumeroFila = 2).; Nunca devuelve más de una fila.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PatientBedTransfer';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Estancia; Cama hospitalaria; Traslado de cama; Cama devolutiva; Rechazo de devolutivo; Tipo de cama; Tipo de traslado; Unidad funcional (UFU)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PatientBedTransfer';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @Resultado: Cuando hay exactamente 2 estancias activas (Total = 2) y la primera cama es devolutiva (DEVOLUTIVO = 1) o tiene rechazo de devolutivo (RECHAZODEV = 1), o la segunda cama tiene BedType = 2 con TypeTransfer = 3 en la primera, se inserta una fila con la cama origen y la cama de traslado.; [RETURN_RESULT] @Resultado: Si el total de estancias activas es distinto de 2, no se devuelve ninguna fila.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PatientBedTransfer';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Conteo.Total = 2 (existen exactamente dos estancias activas para el paciente/ingreso) → Se construye la fila con datos de la primera estancia (cama origen) y la segunda (cama de traslado). else No se genera fila candidata.; si DEVOLUTIVO = 1 OR RECHAZODEV = 1 OR (BedType = 2 AND TypeTransfer = 3) → La fila se inserta en el resultado. else La fila se descarta y el resultado queda vacío.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PatientBedTransfer';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHREGESTA; dbo.CHCAMASHO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PatientBedTransfer';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PatientBedTransfer';
GO

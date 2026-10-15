CREATE PROCEDURE [dbo].[SPHC_ListarMedicamentosPendientesAplicar]
(
 @CentroAtencion  CHAR(10), 
 @UnidadFuncional CHAR(10)
)
AS
    BEGIN
        DECLARE @FechaInicio AS DATETIME;

        SET @FechaInicio = [Common].[GETDATE]();
        SET NOCOUNT ON;

		WITH DATOS AS (
			SELECT CONSECUTI AS Consecutivo, 
				   RTRIM(A.IPCODPACI) AS Identificacion, 
				   RTRIM(B.IPNOMCOMP) AS Paciente, 
				   RTRIM(DESPRODUC) AS Medicamento, 
				   FECPROAPL AS 'Fecha de Aplicacion',
				   CASE
					   WHEN RTRIM(A.DOSISPROD) IS NULL
					   THEN A.DESADMINI
					   ELSE RTRIM(A.DOSISPROD) + ' ' + RTRIM(ABRUNIMED)
				   END AS Dosis, 
				   A.CODUNIMED AS UnidadDosisAplicada,
				   CASE
					   WHEN DATEDIFF(MINUTE, FECPROAPL, [Common].[GETDATE]()) < 0
					   THEN 0
					   ELSE DATEDIFF(MINUTE, FECPROAPL, [Common].[GETDATE]())
				   END AS Minutos,
				   CASE
					   WHEN DATEDIFF(MINUTE, FECPROAPL, [Common].[GETDATE]()) < 0
					   THEN 0
					   ELSE DATEDIFF(MINUTE, FECPROAPL, [Common].[GETDATE]())
				   END AS Barra, 
				   A.CODPRODUC AS 'Codigo Medicamento', 
				   A.NUMINGRES AS Ingreso, 
				   PREESTADO AS EstadoPrescripcion,
				   CASE PREESTADO
					   WHEN '1'
					   THEN 'Medicamentos Programados'
					   WHEN '7'
					   THEN 'Medicamentos para Pacientes con Salida'
					   ELSE 'Medicamentos Sin Ordenar'
				   END AS Estado, 
				   A.DURACIDOS AS TTipoDuracion, 
				   L.CODTIPPAC AS TipoPaciente, 
				   RTRIM(G.CODICAMAS) + '  ' + RTRIM(K.DESCCAMAS) AS Cama, 
				   C.CODDCIMED AS 'DCI',A.IDHCORDQUIMIO,IDHCORDCICLOSD,IDCITA
				   , U.TIPUNIDAD AS 'TipoUnidadFuncional'
				   ,CASE WHEN A.IDHCORDQUIMIO IS NULL
				   THEN dbo.ValidateMedicationStatus(A.CONSECPRESCRA, A.CODPRODUC)
				   ELSE CASE dbo.ValidateSchemaStatus(2,A.IDHCORDQUIMIO, A.CODPRODUC, CICLO.CICLO, CICLO.DIA) WHEN 2 THEN 1 WHEN 3 THEN 1 WHEN 4 THEN 2 ELSE 0 END
				   END AS MODIFICADO
			FROM dbo.HCHOJAMED A
					 INNER JOIN dbo.INPACIENT B With (NoLock) ON A.IPCODPACI = B.IPCODPACI
					 INNER JOIN dbo.ADINGRESO L With (NoLock) ON L.NUMINGRES = A.NUMINGRES
					 INNER JOIN dbo.CHREGESTA G With (NoLock) ON A.IPCODPACI = G.IPCODPACI  AND A.NUMINGRES = G.NUMINGRES AND G.REGESTADO = 1
					 left outer JOIN dbo.CHCAMASHO K With (NoLock) ON K.CODICAMAS = G.CODICAMAS		 
					 INNER JOIN dbo.IHLISTPRO C With (NoLock) ON A.CODPRODUC = C.CODPRODUC
					 LEFT OUTER JOIN dbo.HCPRESCRA  D With (NoLock) ON A.IPCODPACI = D.IPCODPACI
														AND A.NUMINGRES = D.NUMINGRES
														AND A.CODPRODUC = D.CODPRODUC
														AND ISNULL(D.IDHCORDQUIMIO,0) = ISNULL(A.IDHCORDQUIMIO,0)
														AND PREESTADO IN('1', '7')
					 INNER JOIN dbo.INUNIMEDI U With (NoLock) ON A.CODUNIMED = U.CODUNIMED		
					 LEFT JOIN INDIGO999.EHR.HCORDCICLOSD CICLO ON A.IDHCORDCICLOSD = CICLO.ID 				 
					 --LEFT OUTER JOIN dbo.ADTRIAGEU H With (NoLock) ON H.NUMINGRES = A.NUMINGRES
					 --LEFT OUTER JOIN dbo.ADCONTURG I With (NoLock) ON I.CODCONCEC = H.CODCONCEC				 
			WHERE A.CODCENATE = @CentroAtencion
				  --AND A.UFUCODIGO = @UnidadFuncional (Se comenta para que liste la cama actual y no de donde fue programado)
				  AND K.UFUCODIGO = @UnidadFuncional --con esto va a la cama actual del paciente
				  AND A.MEDESTADO = '1'
				  AND A.CABESTADO = '1'
				  AND G.REGESTADO = 1
				  AND A.FECPROAPL < @FechaInicio
			  ) SELECT * FROM DATOS WHERE MODIFICADO <> 2
			 
    END;
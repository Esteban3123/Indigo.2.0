

-- =============================================
-- Author:		Yezid Garcia Medina
-- Create date: 10 Mayo 2021
-- Description:	Listar Medicamentos - Nota Servicio Farmaceutico
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarMedicamentos_NotaServicioFarmaceutico] 
(
@Paciente Varchar(25),
@Ingreso  Char(10)
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;
    -------------------
 
    
    DECLARE @MaxFolio_OtrosMedicamentos nchar(10)
    SELECT @MaxFolio_OtrosMedicamentos = ISNULL(MAX(CAST(NUMEFOLIO AS INT)), 0)  FROM HCNOSERFOTROMED AS A WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES = @Ingreso ;
 
    WITH CTE_ObservacionesMedicas AS (
                SELECT FD.MedicalObservation, FD.CODPRODUC, FA.NUMEFOLIO, FA.FECHISPAC, FD.IdSourceTable 
                FROM dbo.HCNOSERFA FA
                INNER JOIN dbo.HCNOSERFD FD ON FD.CODCONCEC = FA.AUTO 
                WHERE FD.MedicalObservation IS NOT NULL AND FA.IPCODPACI = @Paciente AND FA.NUMINGRES = @Ingreso),      
    Tabla
    as
    (
        SELECT
                CONVERT(VARCHAR(50),NEWID()) AS uniqueidentifier,
                A.ID AS 'AUTO',
                'Medicamentos' As 'Agrupador',
                'HCPRESCRA' As 'SourceTable',
                CAST (1 As INT) As 'TIPO',
                CAST (0 AS Numeric ) As 'IDMEZCLAS',
                J.FECHISPAC As 'Fecha_Orden',
                RTRIM(A.CODPRODUC) As 'Codigo', 
                RTRIM(E.DESPRODUC) As 'Medicamento',
                RTRIM(CASE WHEN FORMAPRESCRIBE IS NOT NULL THEN DESADMINI ELSE CASE WHEN DOSISPRFN IS NULL THEN DESADMINI WHEN DURACIDOS='Dosis Unica' THEN RTRIM(CAST(DOSISPRFN AS CHAR)) + ' ' + RTRIM(CAST(ABRUNIMED AS CHAR)) + ' Dosis Unica ' + RTRIM(H.DESVIAADM) ELSE  RTRIM(CAST(DOSISPRFN AS CHAR)) + ' ' + RTRIM(CAST(ABRUNIMED AS CHAR)) + ' Cada ' + RTRIM(CAST(FRECUENCI AS CHAR)) + CASE UNIFRECUE WHEN '1' THEN 'M ' WHEN '2' THEN 'H ' WHEN '3' THEN 'D ' END + RTRIM(H.DESVIAADM) END END) As 'Administracion',
                RTRIM(A.INDAPLMED) As 'Indicaciones',
                RTRIM(A.DURACIDOS) As 'Duracion',
                A.CANPEDPRO As 'Cantidad', 
                RTRIM(F.NOMDIAGNO) AS 'Diagnostico',
                RTRIM(FD.OBSERVACI) AS 'Observaciones',
                CAST(0 AS BIT) AS 'SelActivaMedicamentos',
                RTRIM(CODDCIMED) AS 'CODDCIMED',
                TOTPROUNI AS 'Concentracion',
                CAST(A.PREESTADO AS INT) AS 'Estado',
                MANEXTPRO As 'MANEXTPRO',
                FORMUMANU As 'FORMUMANU',
                RTRIM(B.NOMMEDICO) AS 'Medico',
                RTRIM(D.UFUDESCRI) + ' - '  + RTRIM(C.NOMCENATE) AS 'Unidad',
                A.IDETIPHIS as 'Tipo Historia',
                A.NUMEFOLIO AS 'Numero Folio',
                'Folio: ' + RTRIM(NUMFOLSUS) + ' - ' + RTRIM(MOTSUSMED) AS 'Motivo Suspension',
                CAST(0 AS INT ) AS 'IDHCNOSERFOTROMED',
                NULL AS 'FECHA_OTROSMEDICAMENTOS',
                NULL AS 'USUARIO_OTROSMEDICAMENTOS',
                FD.AppropriateTreatment, FD.ConfirmAppropriateTreatment, RTRIM(FD.ObservationConfirm) AS 'ObservationConfirm', FD.CurrentMedication, FD.[State] AS 'Validador',
                IIF(FA.NUMEFOLIO > A.NUMEFOLIO, FD.[State] , CASE WHEN A.INDAUDFOR = 99 THEN 4 ELSE FD.[State] END) AS 'State', ISNULL(FD.ID, 0) AS 'IDHCNOSERFD', ISNULL(FA.AUTO, 0) AS 'IDHCNOSERFA', 
                (SELECT TOP 1 MedicalObservation FROM CTE_ObservacionesMedicas FAO
                WHERE FAO.CODPRODUC = A.CODPRODUC AND (FAO.NUMEFOLIO <= FA.NUMEFOLIO AND FD.IdSourceTable = FAO.IdSourceTable)
                ORDER BY FA.FECHISPAC DESC) AS 'ObservacionesMedicas'
            FROM
                dbo.HCPRESCRA As A with(nolock)
                INNER JOIN dbo.INPROFSAL As B with(nolock) ON A.CODPROSAL=B.CODPROSAL 
                INNER JOIN dbo.ADcenaten As C with(nolock) ON A.CODCENATE=C.codcenate 
                INNER JOIN dbo.INUNIFUNC As D with(nolock) ON A.UFUCODIGO=D.UFUCODIGO
                INNER JOIN dbo.IHLISTPRO As E with(nolock) ON A.CODPRODUC=E.CODPRODUC 
                INNER JOIN dbo.INDIAGNOS As F with(nolock) ON A.CODDIAGNO = F.CODDIAGNO 
                LEFT OUTER JOIN dbo.INUNIMEDI As G with(nolock) ON A.CODUNIMFN = G.CODUNIMED 
                INNER JOIN dbo.HCVIAADMI As H with(nolock) ON A.CODVIAADM = H.CODVIAADM
                INNER JOIN dbo.IHFORMEDI As I with(nolock) ON A.CODFORMED = I.CODFORMED
                INNER JOIN dbo.HCHISPACA As J with(nolock) ON A.NUMEFOLIO=J.NUMEFOLIO AND A.IPCODPACI=J.IPCODPACI 
                LEFT OUTER JOIN dbo.INESPECIA As N with(nolock) ON J.CODESPTRA=N.CODESPECI 
                LEFT JOIN dbo.HCPRESCRC As V with(nolock) ON A.CODCONCEC= V.CODCONCEC 
                LEFT JOIN dbo.HCNOSERFD as FD with(nolock) ON FD.IdSourceTable = A.ID AND FD.CurrentMedication = 1
                LEFT JOIN dbo.HCNOSERFA as FA with(nolock) ON FA.AUTO = FD.CODCONCEC
            WHERE 
                A.IPCODPACI= @Paciente 
                AND A.NUMINGRES= @Ingreso
                AND A.PREESTADO IN (1,6) 
 
        UNION ALL
 
            SELECT
                CONVERT(VARCHAR(50),NEWID()) AS uniqueidentifier,
                CAST (admi.CONSECUTI As INT) AS 'AUTO',
                'Mezclas e infusiones' As 'Agrupador',
                'HCINFLIQA' As 'SourceTable',
                CAST (2 As INT) As 'TIPO',
                cab.CODCONCEC As 'IDMEZCLAS',
                admi.FECHAINIC As 'Fecha_Orden',
                '' As 'Codigo',
                RTRIM(admi.MEZLIQPAC) As 'Medicamento',
                RTRIM(admi.ADMMEZLIQ) As 'Administracion',
                RTRIM(admi.INDAPLMED) As 'Indicaciones',
                CASE admi.TIPMEZLIQ WHEN 1 THEN 'Mezcla continua' WHEN 2 THEN 'Líquido' WHEN 3 THEN 'Mezcla frecuencia' WHEN 4 THEN 'Mezcla magistral' END As 'Duracion',
                '' As 'Cantidad',
                RTRIM(F.NOMDIAGNO) AS 'Diagnostico',
                RTRIM(FD.OBSERVACI) AS 'Observaciones',
                CAST(0 AS BIT) AS 'SelActivaMedicamentos',
                '' AS 'CODDCIMED' ,
                CAST(0 AS Numeric) AS 'Concentracion' ,
                CASE admi.PREESTADO WHEN 1 THEN 1 WHEN 5 THEN 6 END AS 'Estado',
                CAST(0 AS BIT) As 'MANEXTPRO',
                CAST(0 AS BIT) As 'FORMUMANU',
                RTRIM(C.NOMMEDICO) AS 'Medico',
                RTRIM(D.UFUDESCRI) + ' - '  + RTRIM(E.NOMCENATE) AS 'Unidad',
                admi.IDETIPHIS as 'Tipo Historia',
                admi.NUMEFOLIO AS 'Numero Folio',
                'Folio: ' + RTRIM(admi.NUMFOLSUS) + ' - ' + RTRIM(admi.MOTSUSMED) AS 'Motivo Suspension', 
                CAST(0 AS INT ) AS 'IDHCNOSERFOTROMED',
                NULL AS 'FECHA_OTROSMEDICAMENTOS',
                NULL AS 'USUARIO_OTROSMEDICAMENTOS',
                FD.AppropriateTreatment, FD.ConfirmAppropriateTreatment, RTRIM(FD.ObservationConfirm) AS 'ObservationConfirm', FD.CurrentMedication, FD.[State] AS 'Validador',
                FD.[State] AS 'State', ISNULL(FD.ID, 0) AS 'IDHCNOSERFD', ISNULL(FA.AUTO, 0) AS 'IDHCNOSERFA', 
                (SELECT TOP 1 MedicalObservation FROM CTE_ObservacionesMedicas FAO
                WHERE FAO.IdSourceTable = admi.CONSECUTI AND (FAO.NUMEFOLIO <= FA.NUMEFOLIO AND FD.IdSourceTable = FAO.IdSourceTable)
                ORDER BY FA.FECHISPAC DESC) AS 'ObservacionesMedicas'               
            FROM 
                dbo.HCINFLIQA as admi with(nolock)
                INNER JOIN dbo.HCINFLIQC as cab with(nolock) ON cab.CODCONCEC = admi.CODCONCEC 
                INNER JOIN dbo.INPROFSAL as C with(nolock) ON admi.CODPROSAL = C.CODPROSAL
                INNER JOIN dbo.INUNIFUNC as D with(nolock) ON admi.UFUCODIGO = D.UFUCODIGO 
                INNER JOIN dbo.ADcenaten as E with(nolock) ON admi.CODCENATE = E.CODCENATE
                INNER JOIN dbo.INDIAGNOS As F with(nolock) ON admi.CODDIAGNO = F.CODDIAGNO
                LEFT JOIN dbo.HCNOSERFD as FD with(nolock) ON FD.IdSourceTable = admi.CONSECUTI  AND FD.CurrentMedication = 1 AND FD.TIPO = 2
                LEFT JOIN dbo.HCNOSERFA as FA with(nolock) ON FA.AUTO = FD.CODCONCEC
            WHERE 
                cab.IPCODPACI = @Paciente
                AND cab.NUMINGRES = @Ingreso
                AND admi.PREESTADO IN (1,5)
				and EXISTS (SELECT 1 FROM HCINFLIQD D WHERE admi.CODCONCEC = D.CODCONCEC) 
        UNION ALL
            SELECT
				CONVERT(VARCHAR(50),NEWID()) AS uniqueidentifier,
				CAST (admi.ID As INT) AS 'AUTO',
				'Mezclas e infusiones' As 'Agrupador',
				'HCNUTPAREC' As 'SourceTable',
				CAST (2 As INT) As 'TIPO',
				admi.ID As 'IDMEZCLAS',
				admi.FECHAORDEN As 'Fecha_Orden',
				'' As 'Codigo',
				'Nutrición Parenteral: ' + p.NAME As 'Medicamento',
				admi.INDICACIONADM As 'Administracion',
				RTRIM(admi.INDICACIONADI) As 'Indicaciones',
				CASE admi.TIPOINFUSION WHEN 1 THEN 'Continua' WHEN 2 THEN 'Ciclada' END As 'Duracion',
				CAST(0 AS BIT) As 'Cantidad',
				RTRIM(F.NOMDIAGNO) AS 'Diagnostico',
				RTRIM(FD.OBSERVACI) AS 'Observaciones',
				CAST(0 AS BIT) AS 'SelActivaMedicamentos',
				'' AS 'CODDCIMED' ,
				CAST(0 AS Numeric) AS 'Concentracion' ,
				admi.STATUS AS 'Estado',
				CAST(0 AS BIT) As 'MANEXTPRO',
				CAST(0 AS BIT) As 'FORMUMANU',
				RTRIM(C.NOMMEDICO) AS 'Medico',
				RTRIM(D.UFUDESCRI) + ' - '  + RTRIM(E.NOMCENATE) AS 'Unidad',
				CA.IDETIPHIS as 'Tipo Historia',
				admi.FOLIORDEN AS 'Numero Folio',
				'Folio: ' + RTRIM(admi.FOLIORDEN) + ' - ' + RTRIM(admi.MOTSUSMED) AS 'Motivo Suspension', 
				CAST(0 AS INT ) AS 'IDHCNOSERFOTROMED',
				NULL AS 'FECHA_OTROSMEDICAMENTOS',
				NULL AS 'USUARIO_OTROSMEDICAMENTOS',
				FD.AppropriateTreatment, FD.ConfirmAppropriateTreatment, RTRIM(FD.ObservationConfirm) AS 'ObservationConfirm', FD.CurrentMedication, FD.[State] AS 'Validador',
				FD.[State] AS 'State', ISNULL(FD.ID, 0) AS 'IDHCNOSERFD', ISNULL(FA.AUTO, 0) AS 'IDHCNOSERFA', FD.MedicalObservation AS 'ObservacionesMedicas'				
			FROM 
				dbo.HCNUTPAREC as admi with(nolock)
				--INNER JOIN dbo.HCNUTPAREND as cab with(nolock) ON cab.IDHCNUTPAREC = admi.ID 
				INNER JOIN dbo.INPROFSAL as C with(nolock) ON admi.CODPROSAL = C.CODPROSAL
				INNER JOIN dbo.INDIAGNOS As F with(nolock) ON admi.CODDIAGNO = F.CODDIAGNO
				LEFT JOIN dbo.HCNOSERFD as FD with(nolock) ON FD.IdSourceTable = admi.ID  AND FD.CurrentMedication = 1 AND FD.TIPO = 2
				LEFT JOIN HCPARNUTC p WITH (NOLOCK) ON admi.IDHCPARNUTC = p.ID 
				LEFT JOIN dbo.HCNOSERFA as FA with(nolock) ON FA.AUTO = FD.CODCONCEC
				LEFT JOIN dbo.HCHISPACA AS CA with(nolock) ON CA.ID = admi.IDHCHISPACA
				INNER JOIN dbo.INUNIFUNC as D with(nolock) ON CA.UFUCODIGO = D.UFUCODIGO 
				INNER JOIN dbo.ADcenaten as E with(nolock) ON CA.CODCENATE = E.CODCENATE

			WHERE 
				admi.IPCODPACI = @Paciente
				AND admi.NUMINGRES = @Ingreso
				and EXISTS (SELECT 1 FROM HCNUTPAREND D WHERE admi.ID = D.IDHCNUTPAREC)
				and admi.STATUS IN (1,3,4)

				UNION ALL
            SELECT
                CONVERT(VARCHAR(50),NEWID()) AS uniqueidentifier,
                A.ID AS 'AUTO',
                'Medicamentos gestión farmacéutica' As 'Agrupador',
                'HCNOSERFOTROMED' As 'SourceTable',
                CAST (3 As INT) As 'TIPO',
                CAST (0 AS Numeric ) As 'IDMEZCLAS',
                A.FECHAORDE As 'Fecha_Orden',
                RTRIM(B.CODPRODUC) As 'Codigo',
                RTRIM(B.DESPRODUC) As 'Medicamento',
                RTRIM(A.ADMINISTRACION) As 'Administracion',
                '' As 'Indicaciones',
                RTRIM(A.TIPODURACION) As 'Duracion', 
                A.CANTIDAD As 'Cantidad',
                '' AS 'Diagnostico',
                A.OBSERVACIONES AS 'Observaciones',
                CAST(0 AS BIT) AS 'SelActivaMedicamentos',
                RTRIM(B.CODDCIMED) AS 'CODDCIMED' ,
                CAST(0 AS Numeric) AS 'Concentracion' ,
                A.PREESTADO AS 'Estado',
                CAST(0 AS BIT) As 'MANEXTPRO',
                CAST(0 AS BIT) As 'FORMUMANU', 
                RTRIM(C.NOMMEDICO) AS 'Medico', 
                RTRIM(D.UFUDESCRI) + ' - '  + RTRIM(E.NOMCENATE) AS 'Unidad',
                A.IDETIPHIS as 'Tipo Historia', 
                A.NUMEFOLIO AS 'Numero Folio', 
                '' AS 'Motivo Suspension',
                A.ID AS 'IDHCNOSERFOTROMED',
                A.FECHACREACION AS 'FECHA_OTROSMEDICAMENTOS',
                RTRIM(A.USUARIOCREACION) AS 'USUARIO_OTROSMEDICAMENTOS',
                FD.AppropriateTreatment, FD.ConfirmAppropriateTreatment, RTRIM(FD.ObservationConfirm) AS 'ObservationConfirm', FD.CurrentMedication, FD.[State] AS 'Validador',
                A.PREESTADO AS 'State', ISNULL(FD.ID, 0) AS 'IDHCNOSERFD', ISNULL(FA.AUTO, 0) AS 'IDHCNOSERFA', FD.MedicalObservation AS 'ObservacionesMedicas'
            FROM 
                dbo.HCNOSERFOTROMED As A with(nolock) 
                INNER JOIN dbo.IHLISTPRO As B with(nolock) ON A.CODPRODUC = B.CODPRODUC                 
                INNER JOIN dbo.INPROFSAL as C with(nolock) ON A.CODPROSAL = C.CODPROSAL
                INNER JOIN dbo.INUNIFUNC as D with(nolock) ON A.UFUCODIGO = D.UFUCODIGO 
                INNER JOIN dbo.ADcenaten as E with(nolock) ON A.CODCENATE = E.CODCENATE
                LEFT JOIN dbo.HCNOSERFA as FA with(nolock) ON A.IPCODPACI = FA.IPCODPACI AND A.NUMINGRES = FA.NUMINGRES AND A.NUMEFOLIO = FA.NUMEFOLIO
                LEFT JOIN dbo.HCNOSERFD as FD with(nolock) ON FA.AUTO = FD.CODCONCEC AND A.CODPRODUC = FD.CODPRODUC AND FD.CurrentMedication = 1
           WHERE 
                 A.IPCODPACI = @Paciente
                 AND A.NUMINGRES = @Ingreso
                 AND A.PREESTADO IN (1,3)
                 AND A.NUMEFOLIO = @MaxFolio_OtrosMedicamentos
                                                
     )  
     SELECT          
            Z.* , 
            CASE WHEN inter.CODPRODUA IS NOT NULL THEN Cast(1 As Bit) ELSE Cast(0 As Bit) END AS 'Interaccion'          
     FROM Tabla As Z
     left outer join dbo.HCINTEMED As inter 
        ON inter.CODPRODUA in (Z.Codigo, Z.CODDCIMED) and inter.CODPRODUB in (select Codigo from Tabla union all select CODDCIMED from Tabla)
    Group By 
            Z.[AUTO], Z.[uniqueidentifier], Z.Administracion, Z.Agrupador, Z.SourceTable, Z.Cantidad, Z.Indicaciones, Z.Diagnostico, Z.CODDCIMED, Z.Codigo, Z.Concentracion, Z.Duracion, Z.Estado, Z.Fecha_Orden,
            Z.FORMUMANU, Z.IDMEZCLAS, Z.MANEXTPRO, Z.Medicamento, Z.Medico, Z.[Motivo Suspension] , Z.[Numero Folio],
            Z.Observaciones, Z.SelActivaMedicamentos, Z.TIPO, Z.[Tipo Historia], Z.Unidad,
            Z.IDHCNOSERFOTROMED, Z.FECHA_OTROSMEDICAMENTOS, Z.USUARIO_OTROSMEDICAMENTOS, Z.AppropriateTreatment, Z.ConfirmAppropriateTreatment, Z.ObservationConfirm, Z.CurrentMedication, Z.Validador, Z.[State], Z.IDHCNOSERFD, Z.IDHCNOSERFA, Z.ObservacionesMedicas,
            inter.CODPRODUA
            
    ORDER BY Z.TIPO ASC, Z.Fecha_Orden ASC, Z.Codigo ASC
 
END
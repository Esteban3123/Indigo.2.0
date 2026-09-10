

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los medicamentos, mezclas e infusiones y otros medicamentos registrados para un paciente en un ingreso específico, como parte de la Nota de Servicio Farmacéutico de historia clínica. Integra las prescripciones activas (HCPRESCRA) con el catálogo de productos farmacéuticos (IHLISTPRO), datos del médico prescriptor (INPROFSAL), unidad funcional y centro de atención (INUNIFUNC, ADCENATEN), diagnóstico asociado y vía de administración, además de enriquecer cada medicamento con las observaciones, validaciones y seguimiento farmacéutico registrados en las notas de servicio (HCNOSERFA/HCNOSERFD). También incluye mezclas e infusiones y medicamentos externos, consolidando en un único resultado todo el perfil farmacoterapéutico activo del paciente para ser visualizado y auditado por el farmacéutico clínico en el módulo de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarMedicamentos_NotaServicioFarmaceutico';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarMedicamentos_NotaServicioFarmaceutico';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único listado los medicamentos, mezclas/infusiones, nutriciones parenterales y medicamentos de gestión farmacéutica vigentes de un paciente/ingreso, junto con sus validaciones farmacéuticas y posibles interacciones, para la nota de servicio farmacéutico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentos_NotaServicioFarmaceutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y el ingreso deben existir y tener prescripciones, mezclas, nutriciones parenterales u otros medicamentos registrados; Para mezclas e infusiones (HCINFLIQA) debe existir al menos un detalle en HCINFLIQD asociado al CODCONCEC; Para nutrición parenteral debe existir al menos un detalle en HCNUTPAREND asociado al ID; Para medicamentos de gestión farmacéutica (HCNOSERFOTROMED) solo se considera el folio máximo registrado para el paciente/ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentos_NotaServicioFarmaceutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se cruza con HCNOSERFD cuando CurrentMedication = 1 (medicación vigente); Para mezclas y nutriciones parenterales, el cruce con HCNOSERFD exige además TIPO = 2; Las observaciones médicas mostradas (CTE_ObservacionesMedicas) corresponden al folio de nota farmacéutica más reciente cuyo NUMEFOLIO sea menor o igual al de la nota actual y coincida con el origen (IdSourceTable); Se calcula y muestra una bandera de interacción medicamentosa cruzando los códigos del listado contra HCINTEMED (CODPRODUA/CODPRODUB) considerando tanto el código del producto como su CODDCIMED; Cada fila se identifica con un GUID nuevo generado en tiempo de consulta; El TIPO clasifica el origen: 1=Medicamentos, 2=Mezclas/Infusiones/Nutrición parenteral, 3=Medicamentos de gestión farmacéutica; Los medicamentos de gestión farmacéutica se restringen al último folio de HCNOSERFOTROMED del paciente/ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentos_NotaServicioFarmaceutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Prescripción de medicamentos; Mezclas e infusiones; Nutrición parenteral; Nota de Servicio Farmacéutico; Validación / auditoría farmacéutica (INDAUDFOR); Suspensión de medicamentos (folio y motivo); Interacción medicamentosa; Vía de administración; Diagnóstico (CIE); Observaciones médicas; Tratamiento apropiado / medicación actual', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentos_NotaServicioFarmaceutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] result set: Devuelve unión de cuatro orígenes (HCPRESCRA, HCINFLIQA, HCNUTPAREC, HCNOSERFOTROMED) ordenado por TIPO, Fecha_Orden y Código, con bandera Interaccion=1 cuando exista coincidencia en HCINTEMED entre Codigo/CODDCIMED del registro y los de cualquier otro registro del listado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentos_NotaServicioFarmaceutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si HCPRESCRA.FORMAPRESCRIBE no es nulo, o DOSISPRFN es nulo → La administración mostrada es la descripción de la vía/forma (DESADMINI) else Se compone una cadena con dosis, unidad de medida, frecuencia y vía; si DURACIDOS=''Dosis Unica'' se rotula como ''Dosis Unica'', en caso contrario se usa ''Cada N'' con sufijo M/H/D según UNIFRECUE (1=minutos, 2=horas, 3=días); si FA.NUMEFOLIO > A.NUMEFOLIO en prescripciones → Se conserva el State original de HCNOSERFD else Si A.INDAUDFOR=99 el State se fuerza a 4 (auditado), de lo contrario se mantiene FD.State; si TIPMEZLIQ en HCINFLIQA → Se etiqueta la duración como ''Mezcla continua'' (1), ''Líquido'' (2), ''Mezcla frecuencia'' (3) o ''Mezcla magistral'' (4); si PREESTADO de mezclas/infusiones → Se traduce 1→1 y 5→6 al estado del listado consolidado; si TIPOINFUSION en nutrición parenteral → Se rotula ''Continua'' (1) o ''Ciclada'' (2); si HCPRESCRA.PREESTADO IN (1,6) → Se incluyen prescripciones activas o suspendidas según ese filtro else Se excluyen del resultado; si HCINFLIQA.PREESTADO IN (1,5) y existe detalle en HCINFLIQD → Se incluye la mezcla/infusión else Se excluye; si HCNUTPAREC.STATUS IN (1,3,4) y existe detalle en HCNUTPAREND → Se incluye la nutrición parenteral else Se excluye; si HCNOSERFOTROMED.PREESTADO IN (1,3) y NUMEFOLIO = folio máximo del paciente/ingreso → Se incluye el medicamento de gestión farmacéutica else Se excluye', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentos_NotaServicioFarmaceutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCNOSERFOTROMED; dbo.HCNOSERFA; dbo.HCNOSERFD; dbo.HCPRESCRA; dbo.INPROFSAL; dbo.ADcenaten; dbo.INUNIFUNC; dbo.IHLISTPRO; dbo.INDIAGNOS; dbo.INUNIMEDI; dbo.HCVIAADMI; dbo.IHFORMEDI; dbo.HCHISPACA; dbo.INESPECIA; dbo.HCPRESCRC; dbo.HCINFLIQA; dbo.HCINFLIQC; dbo.HCINFLIQD; dbo.HCNUTPAREC; dbo.HCNUTPAREND; dbo.HCPARNUTC; dbo.HCINTEMED', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentos_NotaServicioFarmaceutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentos_NotaServicioFarmaceutico';
-- GO

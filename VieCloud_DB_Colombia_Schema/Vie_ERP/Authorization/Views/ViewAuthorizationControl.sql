	
CREATE VIEW [Authorization].[ViewAuthorizationControl]
AS

	SELECT
    ac.Id                               AS Id,                          -- INT IDENTITY. PK; referenciada por Authorization.AuthorizationControlTrace
    ac.AdmissionNumber                  AS NumeroIngreso,               -- CHAR(10). Ingreso origen del control. Ref. lógica a dbo.ADINGRESO.NUMINGRES
    ac.Folio                            AS Folio,                       -- NCHAR(10) NULL. Folio dentro de la admisión (NUMEFOLIO)
    CASE ac.SubjectType                                                 -- TINYINT, FK a Authorization.AuthorizationControlSubjectType
        WHEN 1 THEN 'SERVICIO'                                          --   SubjectCode → dbo.INCUPSIPS.CODSERIPS
        WHEN 2 THEN 'MEDICAMENTO'                                       --   SubjectCode → dbo.IHLISTPRO.CODPRODUC
        WHEN 3 THEN 'INSUMO'                                            --   SubjectCode → dbo.IHLISTPRO.CODPRODUC
        WHEN 4 THEN 'URGENCIA'                                          --   SubjectRecordId → dbo.ADATEINIU.CODCONCEC
        WHEN 5 THEN 'ESTANCIA'                                          --   SubjectRecordId → CHREGESTA.ID / Billing.AccountControlStays.StayId
        ELSE        CAST(ac.SubjectType AS VARCHAR(10))
    END                                 AS TipoSujeto,
    ac.SourceTable                      AS TablaOrigen,                 -- VARCHAR(60) NULL. Tabla fuente del registro en SubjectRecordId: HCORDLABO/HCORDPATO/HCORDIMAG/HCORDPRON/HCORDPROQ/HCPRESCRA/HCSOLINSD/HCINFLIQA/HCORHEMCO; estancias: CHREGESTA o BILLING.ACCOUNTCONTROLSTAYS
    ac.SubjectCode                      AS CodigoSujeto,                -- VARCHAR(20) NULL. Código del sujeto (CUPS o producto). Ref. lógica sin FK física (ADR-007)
    ac.SubjectRecordId                  AS RegistroSujeto,              -- NUMERIC(18) NULL. PK del registro fuente (ver TablaOrigen). Urgencia: ADATEINIU.CODCONCEC; Estancia: CHREGESTA.ID
    CASE
        WHEN ac.SubjectType = 1 THEN
            CASE ac.ServiceType                                         -- TINYINT NULL. Subtipo legado (TIPOSERIPS); 0 = No aplica (meds/insumos)
                WHEN 1 THEN 'Laboratorio'
                WHEN 2 THEN 'Patología'
                WHEN 3 THEN 'Imagen'
                WHEN 4 THEN 'Procedimiento no Qx'
                WHEN 5 THEN 'Procedimiento Qx'
                WHEN 6 THEN 'Informe Qx'
                ELSE        CAST(ac.ServiceType AS VARCHAR(10))
            END
        ELSE NULL
    END                                 AS TipoServicio,
    CASE ac.Status                                                      -- TINYINT. Máquina de estados del Dashboard (PBI ERP-84); historial en AuthorizationControlTrace
        WHEN 1 THEN 'No Solicitado'                                     --   Estado inicial con que el consumer crea el control
        WHEN 2 THEN 'En Trámite'
        WHEN 3 THEN 'Radicado'
        WHEN 4 THEN 'Autorizado'
        WHEN 5 THEN 'Entregado'
        WHEN 6 THEN 'Confirmado'
        WHEN 7 THEN 'Cancelado'
        WHEN 8 THEN 'Rechazado'
        ELSE        CAST(ac.Status AS VARCHAR(10))
    END                                 AS Estado,
 
    -- ------------------------- Auditoría -------------------------
    ac.CreationUser                     AS UsuarioCreacion,             -- VARCHAR(20). Usuario que creó el control (CODUSUARI). Billing: ACS.CreationUser; si no, SYS_AUTHZ
    ac.CreationDate                     AS FechaCreacion,               -- DATETIME. Default Common.GETDATE()
    ac.ModificationUser                 AS UsuarioModificacion,         -- VARCHAR(20) NULL. Última modificación
    ac.ModificationDate                 AS FechaModificacion,           -- DATETIME NULL
    ac.CancellationUser                 AS UsuarioCancelacion,          -- VARCHAR(20) NULL. Quien anuló (CODUSUANU)
    ac.CancellationDate                 AS FechaCancelacion,            -- DATETIME NULL
    ac.CancellationJustification        AS JustificacionCancelacion,    -- VARCHAR(250) NULL. Se diligencia al anular (JUSANULA)
    ac.CancellationReasonsId            AS IdMotivoCancelacion,         -- INT NULL. FK a Authorization.CancellationReasons
 
    -- ------------------------- Gestión de la autorización -------------------------
    ISNULL(ac.RequestDate, ac.CreationDate) AS FechaSolicitud,          -- DATETIME. RequestDate con fallback a CreationDate
    ac.AssignedAuthorizer               AS AutorizadorAsignado,         -- VARCHAR(20) NULL. Usuario autorizador que gestiona la solicitud
    ac.FileNumber                       AS NumeroRadicado,              -- VARCHAR(100) NULL. Radicado ante la EPS; NULL hasta radicar
    ac.FiledDate                        AS FechaRadicacion,             -- DATETIME NULL
    ac.AuthorizationCode                AS CodigoAutorizacion,          -- VARCHAR(100) NULL. Código emitido por la entidad al aprobar
    ac.AuthorizedDate                   AS FechaAutorizacion,           -- DATETIME NULL
    ac.Observations                     AS Observaciones,               -- VARCHAR(MAX) NULL. Notas libres del autorizador
    ac.RejectionReason                  AS MotivoRechazo,               -- VARCHAR(500) NULL. Motivo de negación de la entidad
    ac.ClinicalJustification            AS JustificacionClinica,        -- VARCHAR(MAX) NULL. Justificación clínica de la solicitud (JUSCLISER)
 
    -- ------------------------- Contexto del ingreso -------------------------
    ac.PatientCode                      AS CodigoPaciente,              -- VARCHAR(25) PII/MASKED. Documento del paciente → dbo.INPACIENT.IPCODPACI
    ac.PatientName                      AS NombrePaciente,              -- NVARCHAR(255) PII/MASKED. Snapshot de INPACIENT
    ac.PatientIdType                    AS TipoIdentificacion,          -- VARCHAR(10) NULL. FK lógica a ADTIPOIDENTIFICA.CODIGO
    ac.PatientIdTypeName                AS NombreTipoIdentificacion,    -- VARCHAR(100) NULL. Snapshot ADTIPOIDENTIFICA.NOMBRE
    ac.PatientAge                       AS EdadPaciente,                -- INT NULL. Edad calculada al crear el control
    ac.EntityCode                       AS CodigoEntidad,               -- CHAR(9). EPS/pagador (CODENTIDA) → dbo.INENTIDAD
    ac.PayerName                        AS NombrePagador,               -- NVARCHAR(255) NULL. Snapshot de INENTIDAD
    ac.CareCenterCode                   AS CodigoCentroAtencion,        -- CHAR(10). CODCENATE → dbo.ADCENATEN
    ac.FunctionalUnitCode               AS CodigoUnidadFuncional,       -- CHAR(10) NULL. UFUCODIGO → dbo.INUNIFUNC
    ac.FunctionalUnitName               AS NombreUnidadFuncional,       -- NVARCHAR(255) NULL. Snapshot INUNIFUNC
    ac.RequestingUnitCode               AS CodigoUnidadSolicitante,     -- CHAR(10) NULL. Unidad que origina la solicitud (puede diferir de la del episodio)
    ac.CareGroupId                      AS IdGrupoAtencion,             -- INT NULL. Caregroup; resuelve susceptibilidad vía ADCONFSERD
    ac.CareGroupName                    AS NombreGrupoAtencion,         -- NVARCHAR(255) NULL. Snapshot Contract.CareGroup
    ac.AdmissionDate                    AS FechaIngreso,                -- DATETIME NULL. Snapshot ADINGRESO/ADATEINIU
    ac.AdmissionType                    AS TipoAdmision,                -- TINYINT NULL. Hospitalización/Urgencias/Consulta externa (snapshot)
 
    -- ------------------------- Médicos y diagnóstico -------------------------
    ac.TreatingPhysicianCode            AS CodigoMedicoTratante,        -- CHAR(20) NULL. Snapshot INPROFSAL.CODPROSAL
    ac.TreatingPhysicianName            AS NombreMedicoTratante,        -- NVARCHAR(120) NULL. Snapshot INPROFSAL.NOMMEDICO
    ac.TreatingSpecialtyCode            AS CodigoEspecialidadTratante,  -- CHAR(3) NULL. Snapshot INPROFSAL.CODESPEC1
    ac.TreatingSpecialtyName            AS NombreEspecialidadTratante,  -- NVARCHAR(120) NULL. Snapshot INESPECIA.DESESPECI
    ac.RequestingPhysicianCode          AS CodigoMedicoSolicita,        -- CHAR(20) NULL. Puede diferir del tratante
    ac.RequestingPhysicianName          AS NombreMedicoSolicita,        -- NVARCHAR(120) NULL
    ac.DiagnosisCode                    AS CodigoDiagnostico,           -- CHAR(4) NULL. CIE-10 principal, snapshot INDIAGNOS.CODDIAGNO
    ac.DiagnosisName                    AS NombreDiagnostico,           -- NVARCHAR(350) NULL. Snapshot INDIAGNOS.NOMDIAGNO
 
    -- ------------------------- Servicio y cantidades -------------------------
    ac.ServiceDescription               AS DescripcionServicio,         -- NVARCHAR(255) NULL. Código + nombre del servicio/med/insumo
    ac.RelatedDescription               AS DescripcionRelacionada,      -- NVARCHAR(255) NULL. Descripción contractual relacionada
    ac.ServiceBillingGroup              AS GrupoFacturacion,            -- VARCHAR(50) NULL. Clasificación dashboard: Estancia, Urgencias, Cirugía...
    ac.RequestedQuantity                AS CantidadSolicitada,          -- INT NULL. CANSERIPS; NULL si el sujeto no maneja cantidad
    ac.AuthorizedQuantity               AS CantidadAutorizada,          -- INT NULL. CANSERAUT; NULL hasta resolverse
 
    -- ------------------------- SLA / semáforo -------------------------
    ac.ParametrizedTime                 AS TiempoParametrizado,         -- INT NULL. Valor del SLA (interpretar con TimeUnit)
    CASE ac.TimeUnit                                                    -- TINYINT NULL
        WHEN 1 THEN 'Minutos'
        WHEN 2 THEN 'Horas'
        WHEN 3 THEN 'Días'
        ELSE        CAST(ac.TimeUnit AS VARCHAR(10))
    END                                 AS UnidadTiempo,
    ac.SemaphoreDeadline                AS FechaLimiteSemaforo,         -- DATETIME NULL. RequestDate + SLA; umbral del semáforo
 
    -- ------------------------- Flags -------------------------
    ac.IsSusceptible                    AS EsSusceptible,               -- BIT NULL. FOTO de susceptibilidad al crear (fuente de verdad: ADCONFSER/ADCONFSERD)
    ac.RequiresAuthorization            AS RequiereAutorizacion,        -- BIT NULL. CHREGESTA.NOREQAUTO; 1=Sí, 0=No, NULL=no determinado
    ac.IsCovered                        AS EsCubierto,                  -- BIT NULL. Cubierto por el contrato; NULL=sin verificar
    ac.IsQuoted                         AS EsCotizado,                  -- BIT NULL. Cotizado ante la entidad previo a solicitud
    ac.FromMedicalRecord                AS DesdeHC,                     -- BIT. 1=origen orden de Historia Clínica (PROSERIPS)
    ac.FromSurgicalReport               AS DesdeReporteQx,              -- BIT. 1=origen informe quirúrgico (SOLINFOQX)
    ac.FromExtramural                   AS DesdeExtramural,             -- BIT. 1=origen atención extramural (SOLEXTRAM)
 
    -- ------------------------- Estancia (SubjectType = 5) -------------------------
    ac.Bed                              AS IdCama,                      -- INT NULL. CHCAMASHO.CODICAMAS; NULL si no hay cama
    ac.BedDescription                   AS DescripcionCama,             -- NVARCHAR(100) NULL. Snapshot DESCCAMAS
    ac.BedClassCode                     AS CodigoClaseCama,             -- INT NULL. CODCLACAM: 1=Obs urgencias, 2=Recuperación postQx, 3=Hospitalaria, 4=Cuna
    ac.BedClassName                     AS NombreClaseCama,             -- VARCHAR(50) NULL
    ac.StayTypeCode                     AS CodigoTipoEstancia,          -- CHAR(3) NULL. Snapshot CHTIPESTA.CODTIPEST
    ac.StayTypeName                     AS NombreTipoEstancia,          -- VARCHAR(40) NULL. Snapshot CHTIPESTA.DESTIPEST
    ac.StayCupsId                       AS IdCupsEstancia,              -- INT NULL. FK lógica a Contract.CUPSEntity; billing: CUPS contrato-específico
    ac.StayCupsCode                     AS CodigoCupsEstancia,          -- VARCHAR(20) NULL
    ac.StayStartDate                    AS FechaInicioEstancia,         -- DATETIME NULL. Billing: rango acumulado (min inicio de las liquidaciones diarias)
    ac.StayEndDate                      AS FechaFinEstancia,            -- DATETIME NULL. Billing: max fin acumulado; NULL si estancia activa
    ac.AuthorizedDays                   AS DiasAutorizados,             -- INT NULL. Días autorizados por la entidad
    ac.StayDays                         AS DiasEstancia,                -- INT NULL. Días reales entre inicio y fin (recalculado por el consumer)
    ac.StayAlert                        AS AlertaEstancia,              -- VARCHAR(20) NULL. 'Alerta' (NOREQAUTO=1) / 'Normal'
    ac.RequiresAuthorizationJustification AS JustificacionNoRequiere    -- VARCHAR(500) NULL. CHREGESTA.NOREQAUTJUS
    ,x.FinancedResourceUPC,
	IIF(ISNULL(x.ProcedureCupsId, 0) > 0, 1, 0) Covered,
	ISNULL(x.Contracted, 0) Contracted,
	ISNULL(x.Quoted, 0) Quoted,
    iif(ev.Status = 4 ,0,1) as Authorized,
    ac.SubjectType
FROM [Authorization].[AuthorizationControl] ac
OUTER APPLY
(
    SELECT TOP (1)
           tpe.Id,
           tpe.Status,
           tpe.CreationDate
    FROM [Authorization].[AuthorizationEvents] tpe
    WHERE tpe.AuthorizationControlId = ac.Id
    ORDER BY tpe.CreationDate DESC
) ev
OUTER APPLY
(
    SELECT top 1
           ce.Id AS CupsId,
           ptc.Id as ProcedureCupsId,
           ptc.ProceduresTemplateId AS ProcedureTemplateId,
           ptc.Quoted,
           ptc.Contracted,
           ce.FinancedResourceUPC
    FROM Contract.CUPSEntity ce WITH(NOLOCK)
    INNER JOIN Contract.CareGroup cg WITH(NOLOCK) ON ac.CareGroupId = cg.Id
    LEFT JOIN Contract.ProcedureCups ptc WITH(NOLOCK) ON cg.ProcedureTemplateId = ptc.ProceduresTemplateId AND ce.Id = ptc.CupsId
    WHERE ce.Code = ac.SubjectCode
) x;

GO


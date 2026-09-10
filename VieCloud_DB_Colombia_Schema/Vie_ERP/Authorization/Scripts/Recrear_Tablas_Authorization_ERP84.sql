/* =============================================================================================
   Recreación de las tablas del modelo de control de Autorizaciones Intrahospitalarias (ERP-84)
   Ejecutar CONECTADO a la base del tenant (p.ej. INDIGO636). Orden = dependencias (FK/triggers).
   Incluye: esquema, catálogo SubjectType (+seed), AuthorizationControl, Trace, Alert,
            ProcessedInbox, y tablas del Agente Autorizador.
   ============================================================================================= */

IF SCHEMA_ID('Authorization') IS NULL EXEC('CREATE SCHEMA [Authorization]');
GO


/* ===================== AuthorizationControlSubjectType ===================== */
-- =============================================================================
-- NUEVO (2026-06-15) — Tarea 37517.
-- Catálogo del DISCRIMINADOR de tipo de sujeto de control polimórfico.
-- Da un hogar documentado y extensible al "tipo de sujeto" referenciado por
-- [Authorization].[AuthorizationControl].[SubjectType], evitando un enum mágico
-- disperso en código. Nuevos tipos de sujeto se agregan como datos (filas), sin
-- cambiar el esquema base (requisito de extensibilidad del modelo integral).
--
-- Valores canónicos previstos (sembrados por el equipo en despliegue/seed):
--   1 = SERVICIO     -> referencia lógica a [dbo].[INCUPSIPS].[CODSERIPS] (CUPS-RIPS)
--   2 = MEDICAMENTO  -> referencia lógica a [dbo].[IHLISTPRO].[CODPRODUC]
--   3 = INSUMO       -> referencia lógica a [dbo].[IHLISTPRO].[CODPRODUC]
--   4 = URGENCIA     -> referencia lógica a [dbo].[ADATEINIU].[CODCONCEC] (clave del AIU)
--   5 = ESTANCIA     -> PUNTO DE EXTENSIÓN DIFERIDO. Ver TODO abajo.
--
-- TODO (ESTANCIA — diferido, mejora incremental posterior):
--   El valor 'ESTANCIA' (5) existe en este catálogo como punto de extensión
--   documentado, PERO la tabla fuente del control de estancias queda PENDIENTE de
--   identificar por el usuario y NO debe inferirse (ver MODELO-SUSCEPTIBILIDAD-Y-CONTROL.md
--   §5 / §8 P1). NO se crea FK ni se infiere el nombre de la tabla destino de estancias.
--   Cuando el usuario confirme la tabla fuente, se documentará su referencia lógica aquí.
--
-- Referencias a maestros: LÓGICAS (no se declaran FK físicas). El sujeto es
-- polimórfico (una sola columna de código apunta a maestros distintos según el
-- tipo), por lo que un FK físico es inviable; además, el aislamiento del dominio
-- de Authorización (ADR-007) desaconseja acoplar el modelo de control al esquema
-- clínico transaccional. La susceptibilidad NO se modela aquí: se resuelve contra
-- [dbo].[ADCONFSER]/[dbo].[ADCONFSERD] (ADR-007).
-- =============================================================================
CREATE TABLE [Authorization].[AuthorizationControlSubjectType] (
    [SubjectType]      TINYINT       NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [MasterTableHint]  VARCHAR (128) NULL,
    [Status]           BIT           NOT NULL CONSTRAINT [DF_AuthorizationControlSubjectType_Status] DEFAULT ((1)),
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL CONSTRAINT [DF_AuthorizationControlSubjectType_CreationDate] DEFAULT (GETDATE()),
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    CONSTRAINT [PK_AuthorizationControlSubjectType] PRIMARY KEY CLUSTERED ([SubjectType] ASC),
    CONSTRAINT [UQ_AuthorizationControlSubjectType_Code] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo del discriminador de tipo de sujeto de control polimórfico del módulo de Autorización. Cada fila define un tipo de sujeto que un registro de [Authorization].[AuthorizationControl] puede referenciar (SERVICIO, MEDICAMENTO, INSUMO, URGENCIA y, como punto de extensión diferido, ESTANCIA). Permite agregar nuevos tipos de sujeto como datos, sin alterar el esquema base. La susceptibilidad NO se modela aquí; se resuelve contra [dbo].[ADCONFSER]/[ADCONFSERD].', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (TINYINT) discriminador del tipo de sujeto de control. Valores canónicos: 1=SERVICIO, 2=MEDICAMENTO, 3=INSUMO, 4=URGENCIA, 5=ESTANCIA (punto de extensión diferido). Es la clave referenciada por [Authorization].[AuthorizationControl].[SubjectType].', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'SubjectType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'SubjectType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código textual estable (VARCHAR 20) del tipo de sujeto para uso en código y APIs (p.ej. ''SERVICIO'', ''MEDICAMENTO'', ''INSUMO'', ''URGENCIA'', ''ESTANCIA''). Único en el catálogo.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo (VARCHAR 100) del tipo de sujeto de control para presentación en UI y reportes.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pista documental (VARCHAR 128, NULL) del maestro lógico al que apunta el código del sujeto de este tipo (p.ej. ''dbo.INCUPSIPS.CODSERIPS'', ''dbo.IHLISTPRO.CODPRODUC'', ''dbo.ADATEINIU.CODCONCEC''). Es solo documentación; NO crea FK. Para ESTANCIA queda NULL hasta que el usuario confirme la tabla fuente (pendiente, no inferir).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'MasterTableHint';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'MasterTableHint';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del tipo de sujeto (BIT): 1=activo (seleccionable), 0=inactivo. Permite retirar un tipo del uso sin borrar historial. Por defecto 1.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario (VARCHAR 20) que creó la fila del catálogo. Auditoría de origen.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación de la fila del catálogo. Por defecto GETDATE().', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario (VARCHAR 20, NULL) que realizó la última modificación de la fila del catálogo.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME, NULL) de la última modificación de la fila del catálogo.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlSubjectType', @level2type = N'COLUMN', @level2name = N'ModificationDate';


/* ===================== AuthorizationControl ===================== */
-- =============================================================================
-- NUEVO (2026-06-15) — Tarea 37517.
-- Cabecera del modelo INTEGRAL de control y trazabilidad de autorizaciones.
-- Reemplaza funcionalmente a [dbo].[ADAUTOSER] (deprecada): una sola estructura
-- cubre los distintos orígenes mediante un SUJETO DE CONTROL POLIMÓRFICO, en lugar
-- de una tabla por tipo. Ver docs/architecture/MODELO-SUSCEPTIBILIDAD-Y-CONTROL.md §6
-- y ADR-007 / ADR-008.
--
-- SUJETO DE CONTROL POLIMÓRFICO ([SubjectType] + [SubjectCode] / [SubjectRecordId]):
--   1 = SERVICIO     -> [SubjectCode]     = ref. lógica a [dbo].[INCUPSIPS].[CODSERIPS]  (CHAR(20))
--   2 = MEDICAMENTO  -> [SubjectCode]     = ref. lógica a [dbo].[IHLISTPRO].[CODPRODUC]  (CHAR(20))
--   3 = INSUMO       -> [SubjectCode]     = ref. lógica a [dbo].[IHLISTPRO].[CODPRODUC]  (CHAR(20))
--   4 = URGENCIA     -> [SubjectRecordId] = ref. lógica a [dbo].[ADATEINIU].[CODCONCEC]  (NUMERIC(18))
--                       (cada registro de ADATEINIU tiene su propio control de urgencias,
--                        distinto del envío regulatorio ESTTRANSA del propio AIU)
--   5 = ESTANCIA     -> PUNTO DE EXTENSIÓN DIFERIDO (mejora incremental posterior).
--
-- TODO (ESTANCIA — diferido, NO inferir):
--   Se admite el valor 'ESTANCIA' (5) en el discriminador [SubjectType] (ver
--   [Authorization].[AuthorizationControlSubjectType]), PERO la tabla fuente del
--   control de estancias está PENDIENTE de identificar por el usuario y NO debe
--   inferirse (MODELO-SUSCEPTIBILIDAD-Y-CONTROL.md §5 / §8 P1). Por eso:
--     - NO se crea FK ni tabla destino de estancias.
--     - NO se infiere su nombre.
--     - Cuando llegue una estancia, su clave se guardará en [SubjectRecordId] (o en
--       [SubjectCode] si la clave es textual), y aquí se documentará la referencia
--       lógica. La elección de columna se confirmará con el usuario al definir la tabla.
--
-- REFERENCIAS POLIMÓRFICAS = LÓGICAS (sin FK físicas):
--   No se declaran FK físicas hacia los maestros del sujeto ([INCUPSIPS], [IHLISTPRO],
--   [ADATEINIU]) porque una misma columna apunta a maestros distintos según [SubjectType]
--   (un FK físico exige un único destino) y porque el aislamiento del dominio de
--   Autorización (ADR-007) desaconseja acoplar el control al esquema clínico
--   transaccional. La integridad del sujeto se valida en la capa de aplicación / SP.
--
-- SUSCEPTIBILIDAD: NO se duplica aquí. Se resuelve contra [dbo].[ADCONFSER] /
--   [dbo].[ADCONFSERD] (por CODSERIPS + INAPLICA o caregroup). [IsSusceptible] es solo
--   una foto del resultado evaluado al crear el control (auditoría del momento), no la
--   fuente de verdad de susceptibilidad.
-- =============================================================================
/****** Object:  Table [Authorization].[AuthorizationControl]    Script Date: 22/06/2026 3:40:50 p. m. ******/

CREATE TABLE [Authorization].[AuthorizationControl](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SubjectType] [tinyint] NOT NULL,
	[SubjectCode] [varchar](20) NULL,
	[SubjectRecordId] [numeric](18, 0) NULL,
	[SourceTable] [varchar](60) NULL,
	[ServiceType] [tinyint] NULL,
	[PatientCode] [varchar](25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
	[EntityCode] [char](9) NOT NULL,
	[AdmissionNumber] [char](10) NOT NULL,
	[Folio] [nchar](10) NULL,
	[CareCenterCode] [char](10) NOT NULL,
	[FunctionalUnitCode] [char](10) NULL,
	[CareGroupId] [int] NULL,
	[Status] [tinyint] NOT NULL,
	[RequestedQuantity] [int] NULL,
	[AuthorizedQuantity] [int] NULL,
	[IsSusceptible] [bit] NULL,
	[FromMedicalRecord] [bit] NOT NULL,
	[FromSurgicalReport] [bit] NOT NULL,
	[FromExtramural] [bit] NOT NULL,
	[ClinicalJustification] [varchar](max) NULL,
	[CancellationJustification] [varchar](250) NULL,
	[CreationUser] [varchar](20) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationUser] [varchar](20) NULL,
	[ModificationDate] [datetime] NULL,
	[CancellationUser] [varchar](20) NULL,
	[CancellationDate] [datetime] NULL,
	[RequestDate] [datetime] NULL,
	[AssignedAuthorizer] [varchar](20) NULL,
	[FileNumber] [varchar](100) NULL,
	[FiledDate] [datetime] NULL,
	[AuthorizationCode] [varchar](100) NULL,
	[AuthorizedDate] [datetime] NULL,
	[AuthorizationExpiredDate] [datetime] NULL,
	[Observations] [varchar](max) NULL,
	[RejectionReason] [varchar](500) NULL,
	[ParametrizedTime] [int] NULL,
	[TimeUnit] [tinyint] NULL,
	[SemaphoreDeadline] [datetime] NULL,
	[PatientName] [nvarchar](255) MASKED WITH (FUNCTION = 'default()') NULL,
	[PatientIdType] [varchar](10) NULL,
	[PatientAge] [int] NULL,
	[AdmissionDate] [datetime] NULL,
	[Bed] [int] NULL,
	[AdmissionType] [tinyint] NULL,
	[StayTypeCode] [char](3) NULL,
	[StayTypeName] [varchar](40) NULL,
	[CareGroupName] [nvarchar](255) NULL,
	[PayerName] [nvarchar](255) NULL,
	[FunctionalUnitName] [nvarchar](255) NULL,
	[TreatingPhysicianCode] [char](20) NULL,
	[TreatingPhysicianName] [nvarchar](120) NULL,
	[TreatingSpecialtyCode] [char](3) NULL,
	[TreatingSpecialtyName] [nvarchar](120) NULL,
	[RequestingPhysicianCode] [char](20) NULL,
	[RequestingPhysicianName] [nvarchar](120) NULL,
	[DiagnosisCode] [char](4) NULL,
	[DiagnosisName] [nvarchar](350) NULL,
	[ServiceDescription] [nvarchar](255) NULL,
	[RelatedDescription] [nvarchar](255) NULL,
	[ServiceBillingGroup] [varchar](50) NULL,
	[RequestingUnitCode] [char](10) NULL,
	[IsCovered] [bit] NULL,
	[BedDescription] [nvarchar](100) NULL,
	[BedClassCode] [int] NULL,
	[BedClassName] [varchar](50) NULL,
	[StayCupsId] [int] NULL,
	[StayCupsCode] [varchar](20) NULL,
	[StayStartDate] [datetime] NULL,
	[StayEndDate] [datetime] NULL,
	[AuthorizedDays] [int] NULL,
	[StayDays] [int] NULL,
	[RequiresAuthorization] [bit] NULL,
	[RequiresAuthorizationJustification] [varchar](500) NULL,
	[StayAlert] [varchar](20) NULL,
	[IsQuoted] [bit] NULL,
	[PatientIdTypeName] [varchar](100) NULL,
	[CancellationReasonsId] [int] NULL,
	[StayId] [int] NULL,
	[ConfirmationUser] [varchar](20) NULL,
    [ConfirmationDate] [datetime] NULL,
    [ConfirmJustification] [nvarchar](500) NULL
 CONSTRAINT [PK_AuthorizationControl] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

-- Índice para la subquery EXISTS de alerta en las vistas del dashboard intrahospitalario.
-- Sin este índice el EXISTS hace full scan por cada fila retornada.
CREATE NONCLUSTERED INDEX [IX_AuthorizationControl_AlertaIngreso]
    ON [Authorization].[AuthorizationControl] ([AdmissionNumber], [PatientCode], [SubjectType], [Status])
    INCLUDE ([Id])
GO

-- Invariante de negocio a nivel de BD: máximo 1 control de ESTANCIA "abierto" por ingreso
-- (estados terminales Cancelado=7 y Rechazado=8 quedan fuera del filtro).
-- Cierra la carrera de creación entre mensajes concurrentes del mismo ingreso: con MessageId
-- por unidad diaria de billing (TENANT:{t}:STAY:{id}:ACS:{id}) dos mensajes pueden pasar el
-- inbox en paralelo y ambos tomar el path de CREATE. El writer (AuthorizationControlWriter)
-- captura la violación de este índice y convierte el CREATE perdedor en UPDATE del ganador.
CREATE UNIQUE NONCLUSTERED INDEX [UX_AuthorizationControl_OpenStayPerAdmission]
    ON [Authorization].[AuthorizationControl] ([AdmissionNumber])
    WHERE [SubjectType] = 5 AND [Status] <> 7 AND [Status] <> 8
GO

-- Índice cubriente para el filtro principal de las 5 vistas con predicado fijo:
--   Solicitudes  (SubjectType IN (1,2,3) / Status IN (1,2))
--   Radicado     (SubjectType IN (1,2,3,4,5) / Status = 3)
--   Autorizado   (SubjectType IN (1,2,3,4,5) / Status = 4)
--   Emergency    (SubjectType = 4 / Status IN (1,2) / SubjectRecordId IS NOT NULL)
--   Estancias    (SubjectType = 5 / Status <> 7)
-- Key: (SubjectType, Status, CareCenterCode) — seek exacto en SubjectType+Status
--   de la vista, luego residual en CareCenterCode añadido por la capa de aplicación.
-- INCLUDE: columnas de filtro adicional y de FechaFiltro (ISNULL(DateX, CreationDate));
--   evita key-lookup para las condiciones WHERE del dashboard sin cargar las ~75 cols de display.
CREATE NONCLUSTERED INDEX [IX_AuthorizationControl_DashboardFilter]
    ON [Authorization].[AuthorizationControl]
        ([SubjectType] ASC, [Status] ASC, [CareCenterCode] ASC)
    INCLUDE (
        [FunctionalUnitCode],
        [CareGroupId],
        [AssignedAuthorizer],
        [CreationUser],
        [AdmissionNumber],
        [PatientCode],
        [SubjectRecordId],
        [RequestDate],
        [FiledDate],
        [AuthorizedDate],
        [AdmissionDate],
        [CreationDate],
        [SemaphoreDeadline]
    )
    WITH (ONLINE = ON, FILLFACTOR = 85, SORT_IN_TEMPDB = ON)
GO

-- Índice para ViewDashboardIntrahospitalTrazabilidad.
-- Esa vista NO filtra por SubjectType ni Status en su definición (scan total),
-- por lo que el único predicado garantizado al resolver la vista es CareCenterCode
-- (filtro que siempre inyecta la capa de aplicación). Liderar el índice por
-- CareCenterCode permite seek por sede; SubjectType y Status reducen el resultado
-- dentro de cada sede cuando el usuario aplica filtros adicionales.
CREATE NONCLUSTERED INDEX [IX_AuthorizationControl_Trazabilidad]
    ON [Authorization].[AuthorizationControl]
        ([CareCenterCode] ASC, [SubjectType] ASC, [Status] ASC)
    INCLUDE (
        [FunctionalUnitCode],
        [CareGroupId],
        [AssignedAuthorizer],
        [CreationUser],
        [AdmissionNumber],
        [PatientCode],
        [RequestDate],
        [CreationDate]
    )
    WITH (ONLINE = ON, FILLFACTOR = 85, SORT_IN_TEMPDB = ON)
GO

ADD SENSITIVITY CLASSIFICATION TO [Authorization].[AuthorizationControl].[PatientCode] WITH (label = 'Confidential - PII', information_type = 'National ID');
GO

ADD SENSITIVITY CLASSIFICATION TO [Authorization].[AuthorizationControl].[PatientName] WITH (label = 'Confidential - PII', information_type = 'Name');
GO

ALTER TABLE [Authorization].[AuthorizationControl] ADD  CONSTRAINT [DF_AuthorizationControl_FromMedicalRecord]  DEFAULT ((0)) FOR [FromMedicalRecord]
GO

ALTER TABLE [Authorization].[AuthorizationControl] ADD  CONSTRAINT [DF_AuthorizationControl_FromSurgicalReport]  DEFAULT ((0)) FOR [FromSurgicalReport]
GO

ALTER TABLE [Authorization].[AuthorizationControl] ADD  CONSTRAINT [DF_AuthorizationControl_FromExtramural]  DEFAULT ((0)) FOR [FromExtramural]
GO

ALTER TABLE [Authorization].[AuthorizationControl] ADD  CONSTRAINT [DF_AuthorizationControl_CreationDate]  DEFAULT ([Common].[GETDATE]()) FOR [CreationDate]
GO

ALTER TABLE [Authorization].[AuthorizationControl]  WITH CHECK ADD  CONSTRAINT [FK_AuthorizationControl_SubjectType] FOREIGN KEY([SubjectType])
REFERENCES [Authorization].[AuthorizationControlSubjectType] ([SubjectType])
GO

ALTER TABLE [Authorization].[AuthorizationControl] CHECK CONSTRAINT [FK_AuthorizationControl_SubjectType]
GO

ALTER TABLE [Authorization].[AuthorizationControl]  WITH CHECK ADD  CONSTRAINT [CK_AuthorizationControl_SubjectRef] CHECK  (([SubjectType]=(4) AND [SubjectRecordId] IS NOT NULL OR ([SubjectType]=(3) OR [SubjectType]=(2) OR [SubjectType]=(1)) AND [SubjectCode] IS NOT NULL OR [SubjectType]=(5)))
GO

ALTER TABLE [Authorization].[AuthorizationControl] CHECK CONSTRAINT [CK_AuthorizationControl_SubjectRef]
GO

-- =============================================================================
-- Distribucion de Usuarios de Autorizaciones (2026-07-21) — propagacion de la
-- asignacion automatica de ingreso hacia AssignedAuthorizer, EN VEZ del diseño
-- original (LEFT JOIN + COALESCE en las 6 vistas Authorization.ViewDashboardIntrahospital*,
-- que requeria tocar esas vistas y coordinar con el dev de cliente el criterio de
-- filtro). AssignedAuthorizer ya es la columna que TODAS las vistas leen directo
-- (AC.AssignedAuthorizer) y la que ya se escribe hoy desde el flujo manual existente
-- (grid generico de guardado, mismo patron que ServicioDecreto3047.cs linea ~6814).
-- Este trigger cubre el caso "la fila de AuthorizationControl se crea DESPUES de que
-- el ingreso ya fue asignado": al insertarse, si AssignedAuthorizer viene NULL, se
-- rellena con la asignacion vigente del ingreso (Authorization.AdmissionAuthorizerAssignmentLog,
-- IsCurrent=1). Nunca pisa una asignacion manual ya existente (solo actua cuando es NULL).
-- El caso inverso (AuthorizationControl ya existia SIN asignar y el ingreso se asigna
-- DESPUES) lo cubre el trigger simetrico en AdmissionAuthorizerAssignmentLog.sql.
-- =============================================================================
CREATE TRIGGER [Authorization].[TR_AuthorizationControl_PropagateAdmissionAssignment]
    ON [Authorization].[AuthorizationControl]
    AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE ac
            SET ac.[AssignedAuthorizer] = aal.[AssignedUserCode]
        FROM [Authorization].[AuthorizationControl] ac
        INNER JOIN inserted i ON i.[Id] = ac.[Id]
        INNER JOIN [Authorization].[AdmissionAuthorizerAssignmentLog] aal
            ON aal.[AdmissionNumber] = i.[AdmissionNumber] AND aal.[IsCurrent] = 1
        WHERE ac.[AssignedAuthorizer] IS NULL;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador único (INT IDENTITY) del registro de control de autorización. Clave referenciada por [Authorization].[AuthorizationControlTrace].' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Discriminador (TINYINT, FK a [Authorization].[AuthorizationControlSubjectType]) del tipo de sujeto de control: 1=SERVICIO, 2=MEDICAMENTO, 3=INSUMO, 4=URGENCIA, 5=ESTANCIA (diferido). Determina cómo interpretar [SubjectCode]/[SubjectRecordId].' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'SubjectType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'SubjectType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código textual del sujeto (VARCHAR 20, NULL) cuando el tipo se identifica por código: referencia lógica a [dbo].[INCUPSIPS].[CODSERIPS] (SERVICIO) o [dbo].[IHLISTPRO].[CODPRODUC] (MEDICAMENTO/INSUMO). Referencia lógica, sin FK física (sujeto polimórfico + aislamiento ADR-007). NULL para URGENCIA (usa [SubjectRecordId]).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'SubjectCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'SubjectCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Clave numérica del sujeto (NUMERIC 18, NULL) cuando el tipo se identifica por registro: referencia lógica a [dbo].[ADATEINIU].[CODCONCEC] (URGENCIA). Para ESTANCIA (5): PK del registro fuente según [SourceTable] — [Billing].[AccountControlStays].[Id] en eventos billing.stay-committed.v1, o [dbo].[CHREGESTA].[ID] en eventos de hospitalización. La identidad clínica de la estancia (CHREGESTA.ID) queda SIEMPRE en [StayId]. Referencia lógica, sin FK física. NULL para SERVICIO/MEDICAMENTO/INSUMO (usan [SubjectCode]).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'SubjectRecordId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'SubjectRecordId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Subtipo de servicio (TINYINT, NULL) equivalente al TIPOSERIPS legado: 1=Laboratorio, 2=Patología, 3=Imagen, 4=Procedimiento no Qx, 5=Procedimiento Qx, 6=Informe Qx. Aplica principalmente cuando [SubjectType]=SERVICIO.' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'ServiceType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'ServiceType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificación del paciente (VARCHAR 25, PII ofuscado): cédula, documento o equivalente. Contexto del ingreso. Referencia lógica a [dbo].[INPACIENT].[IPCODPACI].' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'PatientCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'PatientCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código de la EPS / entidad responsable de pago (CHAR 9). Equivale a CODENTIDA. Referencia lógica a [dbo].[INENTIDAD].[CODENTIDA].' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'EntityCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'EntityCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Número de admisión (CHAR 10) que identifica el ingreso donde se origina el control. Equivale a NUMINGRES. Referencia lógica a [dbo].[ADINGRESO].[NUMINGRES].' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'AdmissionNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'AdmissionNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Número de folio (NCHAR 10, NULL). Identificador del folio dentro de la admisión o atención. Equivale a NUMEFOLIO.' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'Folio'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'Folio'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código del centro de atención (CHAR 10). Equivale a CODCENATE. Referencia lógica a [dbo].[ADCENATEN].[CODCENATE].' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'CareCenterCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'CareCenterCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código de la unidad funcional (CHAR 10, NULL): urgencia, medicina general, UCI, quirófano, etc. Equivale a UFUCODIGO. Referencia lógica a [dbo].[INUNIFUNC].[UFUCODIGO].' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'FunctionalUnitCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'FunctionalUnitCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del grupo de atención / caregroup (INT, NULL). Equivale a IDCareGroup. Permite resolver susceptibilidad por grupo contra [dbo].[ADCONFSERD].' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'CareGroupId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'CareGroupId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado actual de la autorización (TINYINT). Máquina de estados del Dashboard Intrahospitalario (PBI ERP-84): 1=No solicitado, 2=En trámite, 3=Radicado, 4=Autorizado, 5=Entregado, 6=Confirmado, 7=Cancelado, 8=Rechazado. (1 era "Pendiente" en el modelo previo; es el estado inicial con que el consumer crea el control.) El historial de transiciones vive en [Authorization].[AuthorizationControlTrace].' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'Status'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'Status'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cantidad solicitada (INT, NULL) del sujeto de control cuando aplica (servicios/productos). Equivale a CANSERIPS. NULL cuando el tipo de sujeto no maneja cantidad (p.ej. urgencia).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'RequestedQuantity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'RequestedQuantity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cantidad autorizada (INT, NULL) del sujeto de control cuando aplica. Equivale a CANSERAUT. NULL hasta que se resuelva la autorización o cuando el tipo no maneja cantidad.' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'AuthorizedQuantity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'AuthorizedQuantity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Foto (BIT, NULL) del resultado de susceptibilidad evaluado al crear el control. NO es fuente de verdad: la susceptibilidad se resuelve contra [dbo].[ADCONFSER]/[ADCONFSERD]. Equivale a SERSUSCEP. Se conserva para auditoría del momento de la decisión.' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'IsSusceptible'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'IsSusceptible'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Marca de origen (BIT): 1=el control proviene de una orden de la Historia Clínica. Equivale a PROSERIPS. Por defecto 0.' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'FromMedicalRecord'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'FromMedicalRecord'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Marca de origen (BIT): 1=el control proviene de un informe quirúrgico. Equivale a SOLINFOQX. Por defecto 0.' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'FromSurgicalReport'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'FromSurgicalReport'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Marca de origen (BIT): 1=el control proviene de atención extramural. Equivale a SOLEXTRAM. Por defecto 0.' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'FromExtramural'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'FromExtramural'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Justificación clínica (VARCHAR MAX, NULL) de la solicitud. Equivale a JUSCLISER.' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'ClinicalJustification'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'ClinicalJustification'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Justificación de anulación (VARCHAR 250, NULL). Se diligencia al pasar a estado Anulado(4). Equivale a JUSANULA.' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'CancellationJustification'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'CancellationJustification'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código de usuario (VARCHAR 20) que creó el registro de control. Equivale a CODUSUARI. Auditoría de origen.' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'CreationUser'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'CreationUser'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha y hora (DATETIME) de creación del registro de control. Equivale a FECREGIST. Por defecto GETDATE().' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'CreationDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'CreationDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código de usuario (VARCHAR 20, NULL) que realizó la última modificación del control.' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'ModificationUser'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'ModificationUser'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha y hora (DATETIME, NULL) de la última modificación del control.' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'ModificationDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'ModificationDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código de usuario (VARCHAR 20, NULL) que ejecutó la anulación del control. Equivale a CODUSUANU.' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'CancellationUser'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'CancellationUser'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha y hora (DATETIME, NULL) de la anulación del control.' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'CancellationDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'CancellationDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha y hora en que se registró la solicitud de autorización en el sistema (DATETIME, NOT NULL en negocio).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'RequestDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código del usuario autorizador asignado para gestionar esta solicitud (VARCHAR 20, FK lógica a tabla de usuarios).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'AssignedAuthorizer'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Número de radicado asignado al tramitar la solicitud ante la entidad administradora (VARCHAR; NULL hasta que se radica).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'FileNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha y hora en que se radicó la solicitud ante la entidad administradora (DATETIME, NULL hasta radicación).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'FiledDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Número o código de autorización emitido por la entidad administradora cuando aprueba la solicitud (VARCHAR; NULL hasta autorización).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'AuthorizationCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha y hora en que la entidad otorgó la autorización (DATETIME, NULL hasta que se autoriza).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'AuthorizedDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de vencimiento de la autorización otorgada por la entidad administradora; se propaga desde AuthorizationEvents cuando Status=4 (Autorizado); DATETIME NULL.' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'AuthorizationExpiredDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Observaciones libres del autorizador durante la gestión del control (VARCHAR MAX, NULL si no hay notas).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'Observations'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Motivo de rechazo cuando la entidad administradora niega la autorización (VARCHAR; NULL si no hay rechazo).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'RejectionReason'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor numérico del tiempo SLA configurado para este tipo de servicio/portafolio (INT; se interpreta según TimeUnit).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'ParametrizedTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unidad del tiempo SLA: 1=Minutos, 2=Horas, 3=Días (TINYINT; NULL si no hay SLA configurado).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'TimeUnit'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha y hora límite calculada a partir de RequestDate + ParametrizedTime*TimeUnit; sirve como umbral del semáforo de vencimiento (DATETIME, NULL si sin SLA).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'SemaphoreDeadline'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre completo del paciente en el momento de crear el control (snapshot PII; VARCHAR, origen dbo.INPACIENT).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'PatientName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código del tipo de documento de identificación del paciente (VARCHAR, FK a dbo.ADTIPOIDENTIFICA.CODIGO).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'PatientIdType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Edad del paciente calculada al momento de crear el control (VARCHAR; puede incluir unidad, p.ej. 2 años 3 meses).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'PatientAge'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha y hora de ingreso/admisión del paciente al episodio clínico (DATETIME, snapshot de dbo.ADINGRESO o ADATEINIU).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'AdmissionDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código de la cama asignada al paciente en el momento del control (VARCHAR; NULL para controles ambulatorios/urgencias sin cama).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'Bed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tipo de admisión del episodio (p.ej. Hospitalización, Urgencias, Consulta externa; VARCHAR snapshot).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'AdmissionType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código del tipo de estancia hospitalaria (snapshot de dbo.CHTIPESTA.CODTIPEST; NULL si no aplica).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'StayTypeCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del tipo de estancia hospitalaria para display (snapshot de CHTIPESTA.NOMTIPEST; NULL si no aplica).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'StayTypeName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del grupo de atención al que pertenece el paciente (EPS, ARL, Particular, etc.; snapshot de Contract.CareGroup).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'CareGroupName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre de la entidad responsable del pago/aseguradora (snapshot de dbo.INENTIDAD; VARCHAR).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'PayerName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre de la unidad funcional donde se presta la atención (snapshot de dbo.INUNIFUNC; VARCHAR).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'FunctionalUnitName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código del médico tratante del episodio clínico (snapshot de dbo.INPROFSAL.CODPROSAL; VARCHAR 20).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'TreatingPhysicianCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre completo del médico tratante (snapshot de dbo.INPROFSAL.NOMMEDICO; VARCHAR).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'TreatingPhysicianName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código de la especialidad principal del médico tratante (snapshot de dbo.INPROFSAL.CODESPEC1; CHAR 3).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'TreatingSpecialtyCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre de la especialidad principal del médico tratante (snapshot de dbo.INESPECIA.DESESPECI; VARCHAR).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'TreatingSpecialtyName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código del médico que solicitó el servicio u orden (puede diferir del médico tratante; snapshot de INPROFSAL.CODPROSAL).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'RequestingPhysicianCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del médico solicitante (snapshot de dbo.INPROFSAL.NOMMEDICO; VARCHAR).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'RequestingPhysicianName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código CIE-10 del diagnóstico principal del episodio clínico (CHAR 4 max, snapshot de dbo.INDIAGNOS.CODDIAGNO; NULL si no disponible).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'DiagnosisCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripción del diagnóstico principal (snapshot de dbo.INDIAGNOS.NOMDIAGNO; VARCHAR; NULL si no disponible).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'DiagnosisName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripción del servicio, procedimiento o medicamento que se solicita autorizar (VARCHAR; snapshot o concatenación de código + nombre).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'ServiceDescription'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripción adicional relacionada con el servicio, p.ej. insumo quirúrgico asociado o indicación adicional (VARCHAR; NULL si no aplica).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'RelatedDescription'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Grupo de facturación del servicio para clasificación en el dashboard (p.ej. Estancia, Urgencias, Cirugía; VARCHAR; NULL si no aplica).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'ServiceBillingGroup'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código de la unidad funcional que origina la solicitud de autorización (VARCHAR 20; puede diferir de FunctionalUnitCode del episodio).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'RequestingUnitCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Indica si el servicio está cubierto por el plan o contrato de la entidad administradora del paciente (BIT; 1=cubierto, 0=no cubierto, NULL=sin verificar).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'IsCovered'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripción completa de la cama (nombre o número de cama; VARCHAR snapshot).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'BedDescription'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código del tipo/clase de cama (p.ej. UCI, General, Privada; VARCHAR snapshot).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'BedClassCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del tipo/clase de cama para display en el dashboard (VARCHAR snapshot).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'BedClassName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID interno del servicio CUPS asociado a la estancia hospitalaria (INT FK a CUPSEntity; aplica solo a SubjectType=5 Estancias).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'StayCupsId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Código CUPS del procedimiento o servicio de la estancia (VARCHAR snapshot; NULL si no aplica).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'StayCupsCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha y hora de inicio de la estancia hospitalaria (DATETIME; NULL para controles no tipo Estancia).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'StayStartDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha y hora de fin de la estancia hospitalaria (DATETIME; NULL si la estancia sigue activa o no aplica).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'StayEndDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Número de días autorizados por la entidad para la estancia hospitalaria (INT; NULL si no se ha autorizado o no aplica).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'AuthorizedDays'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Días reales de estancia transcurridos desde StayStartDate hasta StayEndDate o fecha actual (INT calculado; NULL si no aplica).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'StayDays'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Indica si la estancia requiere autorización explícita de la entidad administradora (BIT; 1=Sí, 0=No; NULL=no determinado).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'RequiresAuthorization'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Justificación clínica o administrativa para la decisión de requerir o no autorización de la estancia (VARCHAR MAX; NULL si no aplica).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'RequiresAuthorizationJustification'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Indicador de alerta activa sobre la estancia: vencimiento de SLA, días excedidos, etc. (BIT; 1=alerta activa, 0=sin alerta).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'StayAlert'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Indica si el servicio fue cotizado ante la entidad administradora previo a la solicitud formal (BIT; 1=sí cotizado, 0=no cotizado).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'IsQuoted'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Descripción del tipo de documento de identificación del paciente (VARCHAR 100, snapshot de ADTIPOIDENTIFICA.NOMBRE).' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl', @level2type=N'COLUMN',@level2name=N'PatientIdTypeName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cabecera del modelo integral de control y trazabilidad de autorizaciones. Registra, por ingreso, el sujeto de control (polimórfico: servicio, medicamento, insumo, urgencia y —diferido— estancia), su contexto de ingreso, el estado de su autorización, cantidades solicitada/autorizada, marcas de origen (HC, informe quirúrgico, extramural) y auditoría. Reemplaza funcionalmente a [dbo].[ADAUTOSER]. La susceptibilidad NO se duplica aquí; se resuelve contra [dbo].[ADCONFSER]/[ADCONFSERD].' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-opus-4-8_2026-06-15_task-37517' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControl'
GO


EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Id del motivo de cancelacion de la tabla  [Authorization].CancellationReasons',
    @level0type = N'SCHEMA',
    @level0name = N'Authorization',
    @level1type = N'TABLE',
    @level1name = N'AuthorizationControl',
    @level2type = N'COLUMN',
    @level2name = N'CancellationReasonsId';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description',
    @value=N'Identidad clínica de la estancia hospitalaria (INT, NULL): [dbo].[CHREGESTA].[ID]. Solo aplica para SubjectType=5 (ESTANCIA); NULL en los demás tipos. Complementa a [SubjectRecordId], que guarda el PK del registro FUENTE según [SourceTable] (para eventos billing.stay-committed.v1 es [Billing].[AccountControlStays].[Id] — pueden existir N registros diarios de billing para el mismo CHREGESTA.ID; este campo conserva la identidad única de la estancia, la misma usada en el MessageId de deduplicación TENANT:{t}:STAY:{StayId}:ACS:{Id}). Referencia lógica, sin FK física (ADR-007).',
    @level0type=N'SCHEMA', @level0name=N'Authorization',
    @level1type=N'TABLE',  @level1name=N'AuthorizationControl',
    @level2type=N'COLUMN', @level2name=N'StayId';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource',
    @value=N'mgasca_2026-07-03_correcciones-estancias',
    @level0type=N'SCHEMA', @level0name=N'Authorization',
    @level1type=N'TABLE',  @level1name=N'AuthorizationControl',
    @level2type=N'COLUMN', @level2name=N'StayId';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description',
    @value=N'Nombre de la tabla fuente de la orden clínica (VARCHAR 60, NULL). Para órdenes (SubjectType=1/2/3): tabla HC de origen del registro cuyo PK está en SubjectRecordId. Valores canónicos: HCORDLABO (laboratorio), HCORDPATO (patología), HCORDIMAG (imágenes), HCORDPRON (procedimiento no Qx), HCORDPROQ (procedimiento Qx), HCPRESCRA (prescripción médica), HCSOLINSD (insumos), HCINFLIQA (mezclas), HCORHEMCO (hemocomponentes). NULL para SubjectType=4 (URGENCIA) y SubjectType=5 (ESTANCIA) que usan sus propias referencias.',
    @level0type=N'SCHEMA', @level0name=N'Authorization',
    @level1type=N'TABLE',  @level1name=N'AuthorizationControl',
    @level2type=N'COLUMN', @level2name=N'SourceTable';
GO
    EXEC sp_addextendedproperty 
    @name = N'MS_Description',
    @value = N'Usuario que confirmó la autorización.',
    @level0type = N'SCHEMA', @level0name = 'Authorization',
    @level1type = N'TABLE',  @level1name = 'AuthorizationControl',
    @level2type = N'COLUMN', @level2name = 'ConfirmationUser';
 
GO
EXEC sp_addextendedproperty 
    @name = N'MS_Description',
    @value = N'Fecha y hora en que se confirmó la autorización.',
    @level0type = N'SCHEMA', @level0name = 'Authorization',
    @level1type = N'TABLE',  @level1name = 'AuthorizationControl',
    @level2type = N'COLUMN', @level2name = 'ConfirmationDate';
GO
EXEC sp_addextendedproperty 
    @name = N'MS_Description',
    @value = N'Justificación registrada al confirmar la autorización.',
    @level0type = N'SCHEMA', @level0name = 'Authorization',
    @level1type = N'TABLE',  @level1name = 'AuthorizationControl',
    @level2type = N'COLUMN', @level2name = 'ConfirmJustification'


/* ===================== AuthorizationControlTrace ===================== */
-- =============================================================================
-- NUEVO (2026-06-15) — Tarea 37517.
-- Traza (historial de transiciones) del modelo integral de control de autorizaciones.
-- Registra, en append-only, cada cambio de estado de un [Authorization].[AuthorizationControl]:
-- estado anterior y nuevo, usuario, fecha, acción ejecutada y justificación.
-- Reemplaza funcionalmente la trazabilidad antes implícita en [dbo].[ADAUTOSER].
-- Ver docs/architecture/MODELO-SUSCEPTIBILIDAD-Y-CONTROL.md §6 y ADR-008 (la
-- trazabilidad del dashboard se sirve desde SQL, no desde Cosmos ni la outbox).
--
-- Patrón append-only: cada transición es una fila nueva; no se actualizan filas de
-- traza existentes. El estado vigente del control vive en la cabecera
-- ([AuthorizationControl].[Status]); esta tabla conserva el camino completo.
-- =============================================================================
CREATE TABLE [Authorization].[AuthorizationControlTrace] (
    [Id]                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AuthorizationControlId] INT           NOT NULL,
    [PreviousStatus]         TINYINT       NULL,
    [NewStatus]              TINYINT       NOT NULL,
    [Action]                 TINYINT       NOT NULL,
    [Justification]          VARCHAR (MAX) NULL,
    [ActionUser]             VARCHAR (20)  NOT NULL,
    [ActionDate]             DATETIME      NOT NULL CONSTRAINT [DF_AuthorizationControlTrace_ActionDate] DEFAULT (Common.GETDATE()),
    CONSTRAINT [PK_AuthorizationControlTrace] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationControlTrace_AuthorizationControl] FOREIGN KEY ([AuthorizationControlId]) REFERENCES [Authorization].[AuthorizationControl] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AuthorizationControlTrace_ControlId]
    ON [Authorization].[AuthorizationControlTrace]([AuthorizationControlId] ASC, [ActionDate] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Traza append-only del historial de transiciones de estado de los registros de [Authorization].[AuthorizationControl]. Cada fila documenta un cambio de estado (estado anterior y nuevo), la acción ejecutada, el usuario, la fecha y la justificación. Sustenta la pestaña de Trazabilidad del dashboard (ADR-008) sin depender de Cosmos ni de la outbox clínica.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de la fila de traza.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK a [Authorization].[AuthorizationControl]) del registro de control cuyo cambio de estado se traza.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'AuthorizationControlId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'AuthorizationControlId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado anterior (TINYINT, NULL) del control antes de la transición. Mismos valores que [AuthorizationControl].[Status] (1=Pendiente, 2=Solicitado, 3=Autorizado, 4=Anulado). NULL en la fila inicial de creación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'PreviousStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'PreviousStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado nuevo (TINYINT) del control después de la transición. Mismos valores que [AuthorizationControl].[Status] (1=Pendiente, 2=Solicitado, 3=Autorizado, 4=Anulado).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'NewStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'NewStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Acción ejecutada (TINYINT) que originó la transición: 1=Crear, 2=Solicitar, 3=Autorizar, 4=Anular, 5=Modificar. Documenta el verbo del cambio, complementario al par estado anterior/nuevo.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'Action';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'Action';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación o nota (VARCHAR MAX, NULL) asociada a la transición (motivo de solicitud, de autorización o de anulación, según la acción).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'Justification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'Justification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario (VARCHAR 20) que ejecutó la acción que originó la transición. Trazabilidad de quién cambió el estado.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'ActionUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'ActionUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se ejecutó la acción / transición. Por defecto Common.GETDATE() para usar la hora institucional. Ordena el historial.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'ActionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_task-37517', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationControlTrace', @level2type = N'COLUMN', @level2name = N'ActionDate';


/* ===================== AuthorizationControlAlert ===================== */

CREATE TABLE [Authorization].[AuthorizationControlAlert](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AuthorizationControlId] [int] NOT NULL,
	[Comments] [varchar](max) NOT NULL,
	[Status] [bit] NOT NULL,
	[CreationUser] [varchar](20) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationUser] [varchar](20) NULL,
	[ModificationDate] [datetime] NULL,
 CONSTRAINT [PK_AuthorizationControlAlert] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [Authorization].[AuthorizationControlAlert]  WITH CHECK ADD  CONSTRAINT [FK_AuthorizationControlAlert_AuthorizationControl] FOREIGN KEY([AuthorizationControlId])
REFERENCES [Authorization].[AuthorizationControl] ([Id])
GO

ALTER TABLE [Authorization].[AuthorizationControlAlert] CHECK CONSTRAINT [FK_AuthorizationControlAlert_AuthorizationControl]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del registro' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControlAlert', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id de la cabecera del trámite' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControlAlert', @level2type=N'COLUMN',@level2name=N'AuthorizationControlId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Comentarios' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControlAlert', @level2type=N'COLUMN',@level2name=N'Comments'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Estado del registro: Activo o Suspendido' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControlAlert', @level2type=N'COLUMN',@level2name=N'Status'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario quien creó la alerta' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControlAlert', @level2type=N'COLUMN',@level2name=N'CreationUser'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de creación de la alerta' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControlAlert', @level2type=N'COLUMN',@level2name=N'CreationDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Usuario quien modificó la alerta' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControlAlert', @level2type=N'COLUMN',@level2name=N'ModificationUser'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha de modificación de la alerta' , @level0type=N'SCHEMA',@level0name=N'Authorization', @level1type=N'TABLE',@level1name=N'AuthorizationControlAlert', @level2type=N'COLUMN',@level2name=N'ModificationDate'
GO




/* ===================== ProcessedInbox ===================== */
-- =============================================================================
-- NUEVO (2026-06-15) — Hallazgo HIGH H1 (ADR-010 Capa 4: Inbox transaccional).
-- Bandeja de entrada (INBOX) de IDEMPOTENCIA de los consumidores de mensajería del
-- dominio de Autorización. Implementa la Capa 4 del patrón de deduplicación de ADR-010:
-- el consumidor registra, EN LA MISMA TRANSACCIÓN que su efecto de negocio, el MessageId
-- determinístico de cada mensaje procesado. La inserción es la guardia de exactly-once:
-- si el MessageId ya existe (reintrega de Service Bus / republicación), el UNIQUE la
-- rechaza y el consumidor trata el mensaje como duplicado, en lugar de volver a aplicar
-- el efecto. Ver docs/architecture/ADR-010-DEDUPLICACION.md y AUTORIZACION-CONSUMER-DESIGN.md.
--
-- POR QUÉ ESTA TABLA (vs. la dedup por llave de negocio que reemplaza):
--   El consumidor de Autorización deduplicaba por la llave de negocio del control
--   (Admission + SubjectType + SubjectCode + Folio) mediante un SELECT previo NO
--   transaccional. Eso (a) descarta órdenes RE-VERSIONADAS legítimas que comparten la
--   misma llave de negocio y (b) no es atómico frente a entregas concurrentes. El INBOX
--   transaccional por MessageId determinístico (emitido por el Normalizer, ADR-010 Capa 2)
--   es la fuente de verdad de "ya procesé este mensaje"; la llave de negocio se conserva
--   SOLO como defensa adicional, no como sustituto.
--
-- MONO-TENANT POR DISEÑO: la tabla vive en la base del tenant (una base por tenant,
--   ADR-002/003). [TenantId] se guarda como AUDITORÍA del tenant resuelto en el Control
--   Plane, NO como parte de la llave de deduplicación: la unicidad es (ConsumerName,
--   MessageId) dentro de esta base. [ConsumerName] permite que varios consumidores
--   compartan la tabla sin colisionar entre sí.
--
-- APPEND-ONLY: cada fila representa un mensaje ya visto. No se actualiza (salvo, en el
--   propio INSERT, la marca [IgnoredAsDuplicate] cuando el flujo detecta el duplicado por
--   la vía de negocio antes del choque de UNIQUE). No se borra aquí; la purga/retención es
--   una tarea operativa separada (igual que [Integrations].[ProcessedEvents]).
-- =============================================================================
CREATE TABLE [Authorization].[ProcessedInbox] (
    [Id]                 BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ConsumerName]       NVARCHAR (100) NOT NULL,
    [MessageId]          NVARCHAR (500) NOT NULL,
    [TenantId]           NVARCHAR (64)  NULL,
    [IgnoredAsDuplicate] BIT            NOT NULL CONSTRAINT [DF_ProcessedInbox_IgnoredAsDuplicate] DEFAULT ((0)),
    [ProcessedAtUtc]     DATETIME2 (7)  NOT NULL CONSTRAINT [DF_ProcessedInbox_ProcessedAtUtc] DEFAULT (sysutcdatetime()),
    CONSTRAINT [PK_ProcessedInbox] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_ProcessedInbox_Consumer_Message] UNIQUE NONCLUSTERED ([ConsumerName] ASC, [MessageId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandeja de entrada (INBOX) de idempotencia de los consumidores de mensajería del dominio de Autorización (Capa 4 de ADR-010). Cada fila registra, en la misma transacción que su efecto de negocio, el MessageId determinístico de un mensaje ya procesado por un consumidor. La inserción es la guardia de exactly-once: el índice único (ConsumerName, MessageId) rechaza reentregas y republicaciones, evitando reaplicar el efecto. Reemplaza la deduplicación NO transaccional por llave de negocio (que descartaba órdenes re-versionadas). Mono-tenant: vive en la base del tenant; [TenantId] es auditoría, no parte de la llave.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_h1-adr-010', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (BIGINT IDENTITY) de la fila del inbox. Clave primaria clustered. Es solo la identidad de la fila; la deduplicación se gobierna por el índice único (ConsumerName, MessageId), no por este Id.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_h1-adr-010', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre lógico del consumidor que procesó el mensaje (NVARCHAR 100), por ejemplo ''AuthorizationOrderConsumer''. Forma parte de la llave de deduplicación junto con [MessageId], de modo que distintos consumidores puedan compartir esta tabla sin colisionar entre sí.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'ConsumerName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_h1-adr-010', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'ConsumerName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'MessageId determinístico del mensaje de Service Bus tal como lo emite el productor/normalizer (ADR-010 Capa 2). Es la identidad estable del mensaje a través de reentregas y republicaciones. Junto con [ConsumerName] forma la llave única de deduplicación. NVARCHAR 500 para acomodar MessageIds compuestos (p.ej. con prefijo de tenant).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'MessageId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_h1-adr-010', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'MessageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tenant (NVARCHAR 64, NULL) resuelto en el Control Plane para el mensaje. Se guarda como AUDITORÍA; NO forma parte de la llave de deduplicación, porque la tabla es mono-tenant (vive en la base del tenant). NULL si el tenant aún no se resolvió al registrar la fila.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'TenantId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_h1-adr-010', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'TenantId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca (BIT) que indica que la fila se registró para un mensaje detectado como DUPLICADO (el efecto de negocio NO se reaplicó). 1 = el mensaje ya había sido procesado (reentrega/republicación) y se ignoró; 0 = primer procesamiento exitoso del mensaje. Por defecto 0. Permite distinguir, en auditoría, los procesamientos efectivos de las reentregas absorbidas.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'IgnoredAsDuplicate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_h1-adr-010', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'IgnoredAsDuplicate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC (DATETIME2(7)) en que se registró el procesamiento del mensaje. Por defecto sysutcdatetime() (reloj del servidor, UTC). Sustenta auditoría y la purga/retención operativa del inbox.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'ProcessedAtUtc';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-opus-4-8_2026-06-15_h1-adr-010', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ProcessedInbox', @level2type = N'COLUMN', @level2name = N'ProcessedAtUtc';


/* ===================== AuthorizationManagementParameters ===================== */
-- =============================================================================
-- Parámetros de gestión de autorización (asignación automática de usuarios
-- facturadores a los ingresos hospitalarios). Registro único (settings-style,
-- igual que Authorization.SettingsAuthorization): la aplicación siempre
-- lee/escribe la única fila existente (no hay columna de tenant/centro de atención).
-- =============================================================================

CREATE TABLE [Authorization].[AuthorizationManagementParameters] (
    [Id]                    INT IDENTITY(1,1) NOT NULL,
    [AutomaticAssignment]   BIT NOT NULL,
    [EntryType]             VARCHAR(50) NULL,   -- claves de tipo de ingreso seleccionadas, separadas por coma (ej. "1")
    [StartDateAssignment]   DATETIME NULL,
    [CreationUser]          VARCHAR(20) NOT NULL,
    [CreationDate]          DATETIME NOT NULL CONSTRAINT [DF_AuthorizationManagementParameters_CreationDate] DEFAULT (GETUTCDATE()),
    [ModificationUser]      VARCHAR(20) NULL,
    [ModificationDate]      DATETIME NULL,
    CONSTRAINT [PK_AuthorizationManagementParameters] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

-- =============================================================================
-- Extended properties
-- =============================================================================

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de parámetros de gestión de autorización; clave primaria IDENTITY; INT. Se espera una única fila en la tabla.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-14', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la asignación de usuarios facturadores a los ingresos hospitalarios se realiza de forma automática; BIT NOT NULL. Cuando es 0, los segmentos de tipo de ingreso, fecha de asignación y usuarios asignados no aplican.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'AutomaticAssignment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si la asignación es automática', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'AutomaticAssignment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-14', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'AutomaticAssignment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Claves del/los tipo(s) de ingreso para los que aplica la asignación automática, separadas por coma (selección múltiple); VARCHAR(50) NULL.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'EntryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Claves de tipo de ingreso seleccionadas', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'EntryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-14', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'EntryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha a partir de la cual aplica la asignación automática de usuarios facturadores; DATETIME NULL, requerida en el cliente cuando AutomaticAssignment = 1.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'StartDateAssignment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de inicio de la asignación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'StartDateAssignment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-14', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'StartDateAssignment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario del sistema que creó el registro de parámetros; VARCHAR(20) NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que creó el registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-14', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC de creación del registro; DATETIME NOT NULL DEFAULT GETUTCDATE().', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora de creación del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-14', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario del sistema que realizó la última modificación del registro; VARCHAR(20) NULL.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que modificó el registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-14', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC de la última modificación del registro; DATETIME NULL.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora de la última modificación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-14', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de gestión de autorización: configura si la asignación de usuarios facturadores a los ingresos hospitalarios es automática, a partir de qué fecha, y para qué tipo de ingreso aplica. Registro único (settings-style); los usuarios asignados y sus novedades se almacenan en Authorization.UsersAssignment y Authorization.UserNovelties respectivamente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-5_2026-07-14', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationManagementParameters';
GO


/* ===================== UsersAssignment ===================== */
-- =============================================================================
-- Usuarios facturadores asignados a los ingresos hospitalarios, gestionados
-- desde Authorization.AccountManagementParameters. UserCode referencia de forma
-- lógica (no FK física, distinta base de datos) al usuario de seguridad
-- (INDIGOSECV2), igual convención que AuthorizationScheduleTemplateUsers.
-- =============================================================================

CREATE TABLE [Authorization].[UsersAssignment] (
    [Id]         INT IDENTITY(1,1) NOT NULL,
    [UserCode]   VARCHAR(20) NOT NULL,
    [FullName]   NVARCHAR(100) NOT NULL,
    [Status]     BIT NOT NULL CONSTRAINT [DF_UsersAssignment_Status] DEFAULT ((1)),
    [EntryType]  TINYINT NOT NULL CONSTRAINT [DF_UsersAssignment_EntryType] DEFAULT ((0)),  -- 1=Hospitalario
    [IsRemoved]  BIT NOT NULL CONSTRAINT [DF_UsersAssignment_IsRemoved] DEFAULT ((0)),
    CONSTRAINT [PK_UsersAssignment] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX_UsersAssignment_UserCode]
    ON [Authorization].[UsersAssignment] ([UserCode] ASC)
    WHERE [IsRemoved] = 0
GO

-- =============================================================================
-- Extended properties
-- =============================================================================

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de usuario asignado; clave primaria IDENTITY; INT.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-10', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del usuario de seguridad (base de datos INDIGOSECV2) asignado como facturador; referencia lógica, sin FK física por ser una base de datos distinta; VARCHAR(20) NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-10', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del usuario asignado, almacenado de forma denormalizada para evitar una consulta cruzada a INDIGOSECV2 en cada listado; NVARCHAR(100) NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'FullName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre completo del usuario', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'FullName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-10', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'FullName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el usuario asignado está activo para recibir asignaciones automáticas; BIT NOT NULL DEFAULT 1.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado activo/inactivo del usuario', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-10', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de ingreso para el que el usuario está asignado: 1=Hospitalario; TINYINT NOT NULL DEFAULT 0.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'EntryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de ingreso: 1=Hospitalario', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'EntryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-10', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'EntryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de eliminación lógica: 1 indica que el usuario fue quitado de la asignación desde el cliente (no se elimina físicamente la fila); BIT NOT NULL DEFAULT 0.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'IsRemoved';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca de eliminación lógica', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'IsRemoved';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-10', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment', @level2type = N'COLUMN', @level2name = N'IsRemoved';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuarios facturadores asignados a los ingresos hospitalarios en el módulo de gestión de cuentas. Cada usuario puede tener novedades asociadas en Authorization.UserNovelties. La eliminación es lógica (IsRemoved), no física.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-5_2026-07-10', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UsersAssignment';
GO


/* ===================== UserNovelties ===================== */
-- =============================================================================
-- Novedades (incapacidad, permiso, vacaciones, etc.) registradas para un usuario
-- asignado en Authorization.UsersAssignment.
-- =============================================================================

CREATE TABLE [Authorization].[UserNovelties] (
    [Id]              INT IDENTITY(1,1) NOT NULL,
    [AssignedUserId]  INT NOT NULL,
    [NoveltyDate]     DATETIME NOT NULL CONSTRAINT [DF_UserNovelties_NoveltyDate] DEFAULT (GETUTCDATE()),
    [Description]     NVARCHAR(300) NOT NULL,
    [IsUserActive]    BIT NOT NULL CONSTRAINT [DF_UserNovelties_IsUserActive] DEFAULT ((0)),
    [TypeNovely]      TINYINT NULL,
    [EndDate]         DATETIME NULL,
    CONSTRAINT [PK_UserNovelties] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserNovelties_UsersAssignment]
        FOREIGN KEY ([AssignedUserId])
        REFERENCES [Authorization].[UsersAssignment] ([Id])
)
GO

CREATE NONCLUSTERED INDEX [IX_UserNovelties_AssignedUserId]
    ON [Authorization].[UserNovelties] ([AssignedUserId] ASC)
    INCLUDE ([NoveltyDate])
GO

-- =============================================================================
-- Extended properties
-- =============================================================================

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de novedad; clave primaria IDENTITY; INT.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-10', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario asignado (Authorization.UsersAssignment) al que pertenece la novedad; FK a UsersAssignment.Id; INT NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'AssignedUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del usuario asignado al que pertenece la novedad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'AssignedUserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-10', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'AssignedUserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC en que se registra la novedad; DATETIME NOT NULL DEFAULT GETUTCDATE().', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'NoveltyDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la novedad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'NoveltyDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-10', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'NoveltyDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la novedad reportada para el usuario asignado (ej. incapacidad, permiso, vacaciones); NVARCHAR(300) NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción de la novedad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-10', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del usuario derivado de la novedad (activo/inactivo para asignación) al momento de registrarla; BIT NOT NULL DEFAULT 0.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'IsUserActive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del usuario (activo/inactivo) asociado a la novedad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'IsUserActive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-10', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'IsUserActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Novedades (incapacidad, permiso, vacaciones u otra causa) registradas para un usuario asignado en Authorization.UsersAssignment; cada novedad puede reflejar un cambio en el estado activo/inactivo del usuario para la asignación automática.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-5_2026-07-10', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de novedad. Valores permitidos: 1 = Incapacidad, 2 = Permiso, 3 = Vacaciones, 4 = Otro; TINYINT NULL.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'TypeNovely';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de novedad: 1=Incapacidad, 2=Permiso, 3=Vacaciones, 4=Otro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'TypeNovely';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-14', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'TypeNovely';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final de la novedad (ej. fin de incapacidad, permiso o vacaciones); DATETIME NULL. Se registra en la novedad y no en Authorization.AccountManagementParameters.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha final de la novedad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-5_2026-07-14', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'UserNovelties', @level2type = N'COLUMN', @level2name = N'EndDate';
GO

/* ===================== AdmissionAuthorizerAssignmentLog ===================== */
-- =============================================================================
-- Log append-only de asignacion automatica de ingresos hospitalarios a agentes
-- autorizadores (feature Distribucion de Usuarios de Autorizaciones). Ancla en
-- AdmissionNumber (NUMINGRES), NO en Authorization.AuthorizationControl.Id: un
-- mismo ingreso genera N filas de AuthorizationControl (una por SubjectType —
-- Servicio, Medicamento, Insumo, Urgencia, Estancia); lo que se asigna es el
-- ingreso completo, no una fila de control puntual. Ver
-- docs/PBI_BD_DISTRIBUCION_AUTORIZADORES_INGRESOS.md.
--
-- El pool de agentes habilitados y sus novedades vive en
-- Authorization.UsersAssignment / Authorization.UserNovelties (ya existentes,
-- gestionados desde FrmParametrosGestionAutorizacion). AssignedUserCode aqui
-- referencia de forma logica UsersAssignment.UserCode (no FK fisica, misma
-- convencion que UsersAssignment.UserCode hacia INDIGOSECV2).
-- =============================================================================

CREATE TABLE [Authorization].[AdmissionAuthorizerAssignmentLog] (
    [Id]                       INT IDENTITY(1,1) NOT NULL,
    [AdmissionNumber]          CHAR(10) NOT NULL,
    [AssignedUserCode]         VARCHAR(20) NOT NULL,
    [PreviousAssignedUserCode] VARCHAR(20) NULL,
    [Action]                   VARCHAR(20) NOT NULL CONSTRAINT [DF_AdmissionAuthorizerAssignmentLog_Action] DEFAULT (N'Automatica'),
    [IsCurrent]                BIT NOT NULL CONSTRAINT [DF_AdmissionAuthorizerAssignmentLog_IsCurrent] DEFAULT ((1)),
    [AssignmentDate]           DATETIME NOT NULL CONSTRAINT [DF_AdmissionAuthorizerAssignmentLog_AssignmentDate] DEFAULT (GETUTCDATE()),
    CONSTRAINT [PK_AdmissionAuthorizerAssignmentLog] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

-- Garantiza que exista a lo sumo UNA fila vigente por ingreso — es a la vez la
-- guarda de idempotencia de negocio que debe consultar el consumer antes de
-- asignar (si ya existe fila IsCurrent=1 para el AdmissionNumber, no reasigna).
CREATE UNIQUE NONCLUSTERED INDEX [UX_AdmissionAuthorizerAssignmentLog_AdmissionNumber_Current]
    ON [Authorization].[AdmissionAuthorizerAssignmentLog] ([AdmissionNumber] ASC)
    WHERE [IsCurrent] = 1
GO

-- Soporta el COUNT(*) por usuario que usa el algoritmo de "menor carga actual".
CREATE NONCLUSTERED INDEX [IX_AdmissionAuthorizerAssignmentLog_UserCode_Current]
    ON [Authorization].[AdmissionAuthorizerAssignmentLog] ([AssignedUserCode] ASC)
    WHERE [IsCurrent] = 1
GO

-- =============================================================================
-- Distribucion de Usuarios de Autorizaciones (2026-07-21) — propagacion hacia
-- AuthorizationControl.AssignedAuthorizer, caso simetrico al trigger
-- TR_AuthorizationControl_PropagateAdmissionAssignment (AuthorizationControl.sql):
-- cubre "el ingreso se asigna DESPUES de que ya existian filas de AuthorizationControl
-- sin asignar" (por ejemplo, el evento clinical.admission-registered.v1 llega con
-- retraso respecto a la creacion de la primera solicitud de autorizacion del mismo
-- ingreso). Al insertarse una fila nueva aqui (solo pasa con Outcome=Assigned en
-- AdmissionAssignmentWriter — las demas salidas del writer, AlreadyAssigned/
-- AutomaticAssignmentDisabled/NoEligibleUsers/DuplicateSkipped, no insertan nada,
-- asi que este trigger no dispara para esos casos), se hace backfill de las filas de
-- AuthorizationControl de ese AdmissionNumber que SIGAN sin asignar (AssignedAuthorizer
-- IS NULL) — nunca pisa una asignacion manual ya existente.
-- =============================================================================
CREATE TRIGGER [Authorization].[TR_AdmissionAuthorizerAssignmentLog_BackfillAuthorizationControl]
    ON [Authorization].[AdmissionAuthorizerAssignmentLog]
    AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE ac
            SET ac.[AssignedAuthorizer] = i.[AssignedUserCode]
        FROM [Authorization].[AuthorizationControl] ac
        INNER JOIN inserted i ON i.[AdmissionNumber] = ac.[AdmissionNumber]
        WHERE i.[IsCurrent] = 1
          AND ac.[AssignedAuthorizer] IS NULL;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

-- =============================================================================
-- Extended properties
-- =============================================================================

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de asignación; clave primaria IDENTITY; INT.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-5_2026-07-17', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso hospitalario (dbo.ADINGRESO.NUMINGRES); referencia lógica, sin FK física (mismo criterio de aislamiento que Authorization.AuthorizationControl.AdmissionNumber). Ancla de la asignación: todo el ingreso, no una fila de control puntual; CHAR(10) NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-5_2026-07-17', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del agente autorizador asignado; referencia lógica a Authorization.UsersAssignment.UserCode (no FK física); VARCHAR(20) NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'AssignedUserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-5_2026-07-17', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'AssignedUserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario previamente asignado, solo con valor cuando la fila representa una reasignación (Action=Manual); VARCHAR(20) NULL.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'PreviousAssignedUserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-5_2026-07-17', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'PreviousAssignedUserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen de la asignación: ''Automatica'' (consumer de distribución, algoritmo de menor carga) o ''Manual'' (reasignación desde el dashboard, FrmAssignUser). VARCHAR(20) NOT NULL DEFAULT ''Automatica''.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'Action';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-5_2026-07-17', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'Action';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si esta fila es la asignación vigente para el AdmissionNumber (solo puede haber una vigente por ingreso, garantizado por UX_AdmissionAuthorizerAssignmentLog_AdmissionNumber_Current). BIT NOT NULL DEFAULT 1.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'IsCurrent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-5_2026-07-17', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'IsCurrent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC en que se registró la asignación; DATETIME NOT NULL DEFAULT GETUTCDATE().', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'AssignmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-5_2026-07-17', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog', @level2type = N'COLUMN', @level2name = N'AssignmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Log append-only de asignación automática (y, a futuro, manual) de ingresos hospitalarios a agentes autorizadores. Ancla en AdmissionNumber, no en AuthorizationControl.Id. Alimentado por Indigo.AzClinicalAuthorizationControl (consumer AuthorizationAdmissionDistributionConsumer) al procesar clinical-admission-facts. Fuente de verdad de la asignación; se propaga hacia Authorization.AuthorizationControl.AssignedAuthorizer (columna ya leída directo por las 6 vistas Authorization.ViewDashboardIntrahospital*) vía 2 triggers simétricos: TR_AdmissionAuthorizerAssignmentLog_BackfillAuthorizationControl (este archivo, dispara al insertar aquí) y TR_AuthorizationControl_PropagateAdmissionAssignment (AuthorizationControl.sql, dispara al insertar ahí) — ninguno pisa una asignación manual (solo actúan si AssignedAuthorizer es NULL). Sin cambios a las vistas ni coordinación con cliente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-5_2026-07-17', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AdmissionAuthorizerAssignmentLog';
GO


/* ===== Seed del catálogo AuthorizationControlSubjectType (FK de AuthorizationControl) ===== */
MERGE [Authorization].[AuthorizationControlSubjectType] AS t
USING (VALUES (1,'SERVICIO','Servicio'),(2,'MEDICAMENTO','Medicamento'),(3,'INSUMO','Insumo'),(4,'URGENCIA','Urgencia'),(5,'ESTANCIA','Estancia')) AS s(SubjectType,Code,Name)
ON t.SubjectType = s.SubjectType
WHEN NOT MATCHED THEN INSERT (SubjectType,Code,Name,Status,CreationUser,CreationDate) VALUES (s.SubjectType,s.Code,s.Name,1,'SYS_SEED',GETDATE());
GO

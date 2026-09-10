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

CREATE TABLE [Inventory].[Diagnostic] (
    [Id]                        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                      VARCHAR (20)  NOT NULL,
    [Name]                      VARCHAR (100) NOT NULL,
    [MinimumAge]                NUMERIC (3)   NOT NULL,
    [MinimumAgeMeasurementUnit] TINYINT       NOT NULL,
    [MaximumAge]                NUMERIC (3)   NOT NULL,
    [MaximumAgeMeasurementUnit] TINYINT       NOT NULL,
    [AppliesToMale]             BIT           NOT NULL,
    [AppliesToFemale]           BIT           NOT NULL,
    [RequiresNotification]      BIT           NOT NULL,
    [CodeCIE10]                 VARCHAR (4)   NOT NULL,
    [NotAllowMainDiagnosis]     BIT           NOT NULL,
    [Status]                    BIT           NOT NULL,
    [CreationUser]              VARCHAR (20)  CONSTRAINT [DF_Diagnostic_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]              DATETIME      CONSTRAINT [DF_Diagnostic_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]          VARCHAR (20)  NULL,
    [ModificationDate]          DATETIME      NULL,
    [TimeStamp]                 ROWVERSION    NOT NULL,
    CONSTRAINT [PK_Diagnostic__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Diagnostic__Code]
    ON [Inventory].[Diagnostic]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) del evento de creación, registro o modificación del diagnóstico. Registra automáticamente el instante exacto de cambios en la tabla.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del registro de diagnóstico. Campo opcional que se completa al actualizar datos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (VARCHAR 20) del usuario que realizó la última modificación del diagnóstico. Campo auditoría, opcional.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro diagnóstico. Se asigna automáticamente en el insert mediante función getdate().', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (VARCHAR 20) del usuario que creó el registro de diagnóstico. Valor por defecto: 999. Campo auditoría obligatorio.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo (BIT) del diagnóstico: 1=Activo (disponible para uso), 0=Inactivo (deshabilitado).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Documento 1 - Activo 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que restringe usar el diagnóstico como principal al egreso: 1=No permitir como diagnóstico principal, 0=Sí permitir. Compatibilidad con RIPS.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'NotAllowMainDiagnosis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'No permitir egreso como diagnostico principal   1 - No lo permite   0 - Si lo permite', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'NotAllowMainDiagnosis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'NotAllowMainDiagnosis';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CIE10 (VARCHAR 4) clasificación internacional de enfermedades, décima versión. Estándar OMS para codificación de diagnósticos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'CodeCIE10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo CIE10 (Clasificación internacional de enfermedades, décima versión)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'CodeCIE10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'CodeCIE10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que marca diagnósticos de notificación obligatoria a autoridades sanitarias: 1=Requiere reporte al estado, 0=No requiere. Relevancia para RIPS y vigilancia epidemiológica.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'RequiresNotification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Exige Notificacion Obligatoria, este campo se utiliza mas que todo para reportes donde se tiene que notificar al estado ciertos diagnosticos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'RequiresNotification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'RequiresNotification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de aplicabilidad del diagnóstico al sexo femenino: 1=Aplica, 0=No aplica. Validación por género.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'AppliesToFemale';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aplica al Sexo Femenino', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'AppliesToFemale';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'AppliesToFemale';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de aplicabilidad del diagnóstico al sexo masculino: 1=Aplica, 0=No aplica. Validación por género.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'AppliesToMale';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aplica al Sexo Masculino', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'AppliesToMale';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'AppliesToMale';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida (TINYINT) para edad máxima de aplicabilidad: 1=Años, 2=Meses, 3=Días. Control de rango etario del diagnóstico.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'MaximumAgeMeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de Medida para Edad Mixima:  1: Años  2: Meses  3: Dias', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'MaximumAgeMeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'MaximumAgeMeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad máxima (NUMERIC 3) en que es aplicable el diagnóstico, según unidad especificada. Validación de rango etario superior.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'MaximumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Mixima en la que aplica el Diagnostico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'MaximumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'MaximumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida (TINYINT) para edad mínima de aplicabilidad: 1=Años, 2=Meses, 3=Días. Control de rango etario del diagnóstico.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'MinimumAgeMeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de Medida para Edad Minima:  1: Años  2: Meses  3: Dias', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'MinimumAgeMeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'MinimumAgeMeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad mínima (NUMERIC 3) en que es aplicable el diagnóstico, según unidad especificada. Validación de rango etario inferior.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'MinimumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Minima en la que aplica el Diagnostico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'MinimumAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'MinimumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo (VARCHAR 100) del diagnóstico. Descripción legible de la enfermedad, condición o hallazgo clínico.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del diagnostico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (VARCHAR 20) que identifica el diagnóstico dentro del inventario. Identificador primario de búsqueda.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico único (INT IDENTITY) del diagnóstico. Clave primaria clustered, autoincremental. Referencia en atenciones y eventos de salud.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del diagnostico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de diagnósticos médicos disponibles en el sistema. Registra los códigos CIE-10, restricciones de edad y género, obligatoriedad de notificación y demás atributos que definen cada diagnóstico válido para su uso en historias clínicas y órdenes médicas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'Diagnostic';

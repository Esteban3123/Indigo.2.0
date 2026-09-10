CREATE TABLE [Billing].[RIPSSupportRecord] (
    [Id]                     INT          IDENTITY (1, 1) NOT NULL,
    [Code]                   VARCHAR (20) NOT NULL,
    [OrderDate]              DATETIME     NOT NULL,
    [RIPSHospitalStayRecord] BIT          DEFAULT ((0)) NOT NULL,
    [Status]                 TINYINT      NOT NULL,
    [ServiceOrderId]         INT          NULL,
    [CreationUser]           VARCHAR (20) CONSTRAINT [DF_RIPSSupportRecord_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]           DATETIME     CONSTRAINT [DF_RIPSSupportRecord_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]       VARCHAR (20) NULL,
    [ModificationDate]       DATETIME     NULL,
    [TimeStamp]              ROWVERSION   NOT NULL,
    [AdmissionNumber]        CHAR (10)    NOT NULL,
    CONSTRAINT [PK_RIPSSupportRecord_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RIPSSupportRecord_ServiceOrder] FOREIGN KEY ([ServiceOrderId]) REFERENCES [Billing].[ServiceOrder] ([Id])
);




GO





GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_RIPSSupportRecord__Code]
    ON [Billing].[RIPSSupportRecord]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de soporte para archivo RIPS (Registro Individual de Prestaciones de Salud). Almacena registros de control y trazabilidad de datos clínicos y facturación enviados a entidades regulatorias, incluyendo estancias de hospitalización e información de órdenes de servicio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tabla que guarda el registro de soporte para el archivo RIPS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) de auditoría que registra automáticamente el instante exacto de creación, modificación o cambio de estado del registro RIPS de soporte, para trazabilidad e integridad de datos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del registro de soporte RIPS. Campo de auditoría que indica cuándo se actualizaron datos clínicos o de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR, PII) que realizó la última modificación o actualización del registro de soporte RIPS. Campo de auditoría para rastrabilidad.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación inicial del registro de soporte RIPS. Valor por defecto automático del sistema, campo de auditoría.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR, PII) que creó el registro de soporte RIPS. Por defecto 999 (sistema). Auditoria de origen del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la orden de servicio, atención o procedimiento asociado al registro de soporte RIPS. FK a Billing.ServiceOrder para vincular facturación con atención clínica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'ServiceOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la orden de servicio asociada al registro de soporte.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'ServiceOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'ServiceOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado (TINYINT) del registro de soporte RIPS: pendiente, procesado, rechazado, glosa, etc. Indica el ciclo de vida en el envío regulatorio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'Status';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (BIT) que indica si este registro de soporte RIPS corresponde a una estancia de hospitalización (internación/urgencia) o atención ambulatoria.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'RIPSHospitalStayRecord';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Flag de creación de estancia de hospitalización RIPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'RIPSHospitalStayRecord';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'RIPSHospitalStayRecord';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATETIME) en que se originó la orden de servicio, atención o procedimiento. Referencia temporal clínica del evento de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'OrderDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la orden.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'OrderDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'OrderDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (VARCHAR 20) del registro de soporte RIPS. Identificador funcional para buscar y referencias cruzadas en reportes regulatorios.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de soporte RIPS. Clave primaria secuencial de la tabla.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso o admisión del paciente al que pertenece el registro de soporte RIPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';

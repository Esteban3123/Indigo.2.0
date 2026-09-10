CREATE TABLE [Common].[BasicAudit] (
    [Id]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Tag]             INT           NOT NULL,
    [Entity]          VARCHAR (100) NOT NULL,
    [RegisterId]      VARCHAR (20)  NULL,
    [UserCode]        VARCHAR (20)  NULL,
    [UserName]        VARCHAR (200) NULL,
    [TransactionDate] DATETIME      NOT NULL,
    [Operation]       TINYINT       NOT NULL,
    [UserMachine]     VARCHAR (50)  NULL,
    [Parameters]      VARCHAR (100) NULL,
    [ReportName]      VARCHAR (50)  NULL,
    CONSTRAINT [PK_BasicAudit] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del reporte generado o consultado durante la operación auditada (VARCHAR 50, referencia a reporting)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'ReportName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del repórte', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'ReportName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'ReportName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros o argumentos de entrada utilizados en la transacción (VARCHAR 100, filtros o criterios aplicados)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'Parameters';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parametros', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'Parameters';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'Parameters';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o identificación de la máquina/equipo desde el cual se ejecutó la operación (VARCHAR 50, hostname/IP)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'UserMachine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Máquina', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'UserMachine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'UserMachine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de operación realizada: 1=Crear, 2=Modificar, 3=Confirmar, 4=Anular, 5=Eliminar (TINYINT)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'Operation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Crear, 2 - Modificar, 3 - Confirmar, 4 - Anular, 5 - Eliminar', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'Operation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'Operation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora exacta de la operación auditada (DATETIME), registro temporal de cambios', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'TransactionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fceha de la Operacion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'TransactionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'TransactionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo o alias del usuario de seguridad (VARCHAR 200) que ejecutó la acción', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'UserName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de usuario ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'UserName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'UserName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del usuario de seguridad que realizó la operación (VARCHAR 20, referencia a usuario del sistema)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario de seguridad', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro auditado, equivalente a PK de la entidad monitoreada (ej: Id de paciente, factura, ingreso)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'RegisterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Registro Auditado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'RegisterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'RegisterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la tabla o entidad auditada (VARCHAR 100), referencia a módulo del ERP/EHR (ej: Pacientes, Facturas, Ingresos)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'Entity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Entidad que se audito', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'Entity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'Entity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico del formulario o módulo auditado (ej: ingreso, factura, paciente, procedimiento)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'Tag';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tag Del Formulario', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'Tag';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'Tag';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único (INT IDENTITY) de registro de auditoría en la tabla BasicAudit', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de auditoría básica del sistema: guarda la trazabilidad de operaciones realizadas por usuarios sobre entidades del sistema (quién hizo qué, cuándo y desde qué máquina). Permite rastrear inserciones, modificaciones y eliminaciones con fines de control y seguridad.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BasicAudit';

CREATE TABLE [Integrations].[CIMAHospital_Softland_Synch] (
    [Id]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DataId]          VARCHAR (50)  NOT NULL,
    [Type]            TINYINT       NULL,
    [Action]          TINYINT       NOT NULL,
    [TransactionDate] DATETIME      NOT NULL,
    [State]           TINYINT       NOT NULL,
    [ErrorMessage]    VARCHAR (800) NULL,
    CONSTRAINT [PK_HospitalCIMA_Softland_Synch] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto descriptivo del error (VARCHAR 800) generado durante la sincronización entre CIMA Hospital y Softland; contiene detalles técnicos o de validación si falló la transacción', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'ErrorMessage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mensaje de error generado al sincronizar', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'ErrorMessage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'ErrorMessage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de sincronización (TINYINT): 1=Sincronizado exitosamente, 0=No sincronizado o pendiente; indica si el registro fue transmitido a Softland', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Synchronized  0 - Not Synchronized', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación o registro del evento de sincronización en la tabla de integración', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'TransactionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creacion del registro', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'TransactionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'TransactionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de operación ejecutada (TINYINT): 1=Insert (ingreso nuevo), 2=Update (modificación), 3=Delete (eliminación) enviada a Softland', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'Action';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Insert  2 - Update  3 - Delete', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'Action';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'Action';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de entidad sincronizada (TINYINT): 1=Admisión/Ingreso, 2=Paciente, 3=Orden de Laboratorio, 4=Orden de Imagen, 5=Orden de Patología, 6=Orden de Procedimiento Quirúrgico, 7=Orden de Procedimiento No Quirúrgico, 8=Orden de Interconsulta', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Admission  2 - Patient  3 - LaboratoryOrder  4 - ImageOrder  5 - PathologyOrder  6 - SurgicalProcedureOrder  7 - NotSurgicalProcedureOrder  8 - InterconsultationOrder      ', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (VARCHAR 50) del registro fuente en CIMA Hospital que se sincroniza hacia Softland; referencia a la tabla de origen', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'DataId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla a sincronizar', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'DataId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'DataId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) del registro en la tabla de trazabilidad de sincronización', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de sincronización entre el sistema CIMA Hospital y el ERP Softland. Almacena el estado de cada transacción de integración (pendiente, procesada, con error) para rastrear el intercambio de datos contables, administrativos o clínicos entre ambas plataformas.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch';

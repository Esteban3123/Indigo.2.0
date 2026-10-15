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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mensaje de error generado al sincronizar', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'ErrorMessage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Synchronized  0 - Not Synchronized', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion del registro', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'TransactionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Insert  2 - Update  3 - Delete', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'Action';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Admission  2 - Patient  3 - LaboratoryOrder  4 - ImageOrder  5 - PathologyOrder  6 - SurgicalProcedureOrder  7 - NotSurgicalProcedureOrder  8 - InterconsultationOrder      ', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla a sincronizar', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'DataId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'CIMAHospital_Softland_Synch', @level2type = N'COLUMN', @level2name = N'Id';


CREATE TABLE [Authorization].[ManagementMedicalOrder] (
    [Id]                              INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OperatingUnitId]                 INT                                                                              NOT NULL,
    [EntityName]                      VARCHAR (50)                                                                     NOT NULL,
    [EntityId]                        INT                                                                              NOT NULL,
    [CareCenterCode]                  VARCHAR (20)                                                                     NOT NULL,
    [FunctionalUnitCode]              VARCHAR (20)                                                                     NOT NULL,
    [AdmissionNumber]                 VARCHAR (20)                                                                     NOT NULL,
    [Folio]                           VARCHAR (20)                                                                     NOT NULL,
    [PatientCode]                     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [ProfessionalCode]                VARCHAR (20)                                                                     NOT NULL,
    [RequestDate]                     DATETIME                                                                         NOT NULL,
    [RequestQuantity]                 INT                                                                              NOT NULL,
    [Type]                            TINYINT                                                                          NOT NULL,
    [ItemCode]                        VARCHAR (20)                                                                     NOT NULL,
    [CancellationReasonsId]           INT                                                                              NULL,
    [CancellationReasonsObservations] VARCHAR (MAX)                                                                    NULL,
    [Status]                          TINYINT                                                                          NOT NULL,
    [CreationUser]                    VARCHAR (20)                                                                     NOT NULL,
    [CreationDate]                    DATETIME                                                                         NOT NULL,
    [ModificationUser]                VARCHAR (20)                                                                     NULL,
    [ModificationDate]                DATETIME                                                                         NULL,
    [CancellationUser]                VARCHAR (20)                                                                     NULL,
    [CancellationDate]                DATETIME                                                                         NULL,
    CONSTRAINT [PK_ManagementMedicalOrder] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ManagementMedicalOrder_CancellationReasons] FOREIGN KEY ([CancellationReasonsId]) REFERENCES [Authorization].[CancellationReasons] ([Id]),
    CONSTRAINT [FK_ManagementMedicalOrder_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [Authorization].[ManagementMedicalOrder] NOCHECK CONSTRAINT [FK_ManagementMedicalOrder_CancellationReasons];


GO
ALTER TABLE [Authorization].[ManagementMedicalOrder] NOCHECK CONSTRAINT [FK_ManagementMedicalOrder_OperatingUnit];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ManagementMedicalOrder_Unique]
    ON [Authorization].[ManagementMedicalOrder]([EntityId] ASC, [EntityName] ASC, [ItemCode] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en la que se cancela el registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ManagementMedicalOrder', @level2type = N'COLUMN', @level2name = N'CancellationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de usuario que cancela el registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ManagementMedicalOrder', @level2type = N'COLUMN', @level2name = N'CancellationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en la que se modifica el registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ManagementMedicalOrder', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ManagementMedicalOrder', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en la que se realiza el registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ManagementMedicalOrder', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de usuario que realiza el registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ManagementMedicalOrder', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro:  1 - Cancelado requiere desistimiento  2 - Cancelado  3 - Confirmado', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ManagementMedicalOrder', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de la cancelación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ManagementMedicalOrder', @level2type = N'COLUMN', @level2name = N'CancellationReasonsObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del motivo de cancelación', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ManagementMedicalOrder', @level2type = N'COLUMN', @level2name = N'CancellationReasonsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Representa al código del servicio o al código del producto dependiendo de la tabla de Crystal de donde venga el registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ManagementMedicalOrder', @level2type = N'COLUMN', @level2name = N'ItemCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo solicitado:   1 - Servicio  2 - Producto', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ManagementMedicalOrder', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad solicitada', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ManagementMedicalOrder', @level2type = N'COLUMN', @level2name = N'RequestQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en la cual se realizó la solicitud del registro en las tablas de Crystal', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ManagementMedicalOrder', @level2type = N'COLUMN', @level2name = N'RequestDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del profesional que realiza la solicitud del registro en las tablas de Crystal', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ManagementMedicalOrder', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificación del paciente', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ManagementMedicalOrder', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No. del Folio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ManagementMedicalOrder', @level2type = N'COLUMN', @level2name = N'Folio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No. de admisión', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ManagementMedicalOrder', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código unidad funcional', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ManagementMedicalOrder', @level2type = N'COLUMN', @level2name = N'FunctionalUnitCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código centro atención', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ManagementMedicalOrder', @level2type = N'COLUMN', @level2name = N'CareCenterCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla de Crystal del cual viene el registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ManagementMedicalOrder', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la tabla de Crystal del cual viene el registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ManagementMedicalOrder', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ManagementMedicalOrder', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ManagementMedicalOrder', @level2type = N'COLUMN', @level2name = N'Id';


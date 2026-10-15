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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro:  1 - Cancelado requiere desistimiento  2 - Cancelado  3 - Confirmado', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ManagementMedicalOrder', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo solicitado:   1 - Servicio  2 - Producto', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'ManagementMedicalOrder', @level2type = N'COLUMN', @level2name = N'Type';


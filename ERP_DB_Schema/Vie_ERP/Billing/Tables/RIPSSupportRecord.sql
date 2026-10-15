CREATE TABLE [Billing].[RIPSSupportRecord] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                  VARCHAR (20)  NOT NULL,
    [OrderDate]             DATETIME      NOT NULL,
    [RIPSHospitalStayRecord] BIT          CONSTRAINT [DF_RIPSSupportRecord_RIPSHospitalStayRecord] DEFAULT ((0)) NOT NULL,
    [Status]                TINYINT       NOT NULL,
    [AdmissionNumber]       CHAR (10)     NOT NULL,
    [ServiceOrderId]        INT           NULL,
    [CreationUser]          VARCHAR (20)  CONSTRAINT [DF_RIPSSupportRecord_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]          DATETIME      CONSTRAINT [DF_RIPSSupportRecord_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]      VARCHAR (20)  NULL,
    [ModificationDate]      DATETIME      NULL,
    [TimeStamp]             ROWVERSION   NOT NULL,
    CONSTRAINT [PK_RIPSSupportRecord_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RIPSSupportRecord_ServiceOrder] FOREIGN KEY ([ServiceOrderId]) REFERENCES [Billing].[ServiceOrder] ([Id])
);


GO
ALTER TABLE [Billing].[RIPSSupportRecord] NOCHECK CONSTRAINT [FK_RIPSSupportRecord_ServiceOrder];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_RIPSSupportRecord__Code]
    ON [Billing].[RIPSSupportRecord]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla que guarda el registro de soporte para el archivo RIPS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de admisión del paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden de servicio asociada al registro de soporte.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'ServiceOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro. Registrado = 0, Confirmado = 1, Anulado = 3', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag de creación de estancia de hospitalización RIPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'RIPSHospitalStayRecord';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la orden.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'OrderDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSSupportRecord', @level2type = N'COLUMN', @level2name = N'Id';

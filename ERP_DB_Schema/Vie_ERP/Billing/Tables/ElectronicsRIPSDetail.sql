CREATE TABLE [Billing].[ElectronicsRIPSDetail] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [ElectronicsRIPSId] INT            NOT NULL,
    [MessageCode]       VARCHAR (25)   NOT NULL,
    [Message]           VARCHAR (1000) NOT NULL,
    [CreationUser]      VARCHAR (20)   NOT NULL,
    [CreationDate]      DATETIME       NOT NULL,
    [Path]              VARCHAR (500)  NULL,
    [TypeMessage]       VARCHAR (50)   NULL,
    [SourcePath]        VARCHAR (500)  NULL,
    CONSTRAINT [PK_ElectronicRIPSDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ElectronicsRIPSDetail_ElectronicsRIPS] FOREIGN KEY ([ElectronicsRIPSId]) REFERENCES [Billing].[ElectronicsRIPS] ([Id])
);


GO
ALTER TABLE [Billing].[ElectronicsRIPSDetail] NOCHECK CONSTRAINT [FK_ElectronicsRIPSDetail_ElectronicsRIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Postula la información del dato que se reportó en cada campo al momento de transmitir el JSON al MPS ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'SourcePath';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Postula el detalle del rechazo generado por la validación que realiza el Ministerio de salud y proteccion de cada factura o nota transmitida ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'Path';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'mensaje del validador de rips electronicos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'Message';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del mensaje dado por el validador de rips electronicos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'MessageCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la trazabilidad del rips electronico', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'ElectronicsRIPSId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicsRIPSDetail', @level2type = N'COLUMN', @level2name = N'Id';


CREATE TABLE [Portfolio].[InitialBalanceInvoiceDetail] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InitialBalanceInvoiceId]     INT             NOT NULL,
    [UserConsecutive]             INT             NOT NULL,
    [ServiceType]                 TINYINT         NOT NULL,
    [Consecutive]                 INT             NOT NULL,
    [ServiceCode]                 VARCHAR (20)    NULL,
    [Quantity]                    INT             NOT NULL,
    [UnitValue]                   NUMERIC (18, 2) NOT NULL,
    [ServiceValue]                NUMERIC (18, 2) NOT NULL,
    [Balance]                     NUMERIC (18, 2) NOT NULL,
    [AttentionStartDate]          DATETIME        NULL,
    [CreationUser]                VARCHAR (20)    NOT NULL,
    [CreationDate]                DATETIME        NOT NULL,
    [ModificationUser]            VARCHAR (20)    NULL,
    [ModificationDate]            DATETIME        NULL,
    CONSTRAINT [PK_InitialBalanceInvoiceDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InitialBalanceInvoiceDetail_InitialBalanceInvoice] FOREIGN KEY ([InitialBalanceInvoiceId]) REFERENCES [Portfolio].[InitialBalanceInvoice] ([Id]),
    CONSTRAINT [UQ_InitialBalanceInvoiceDetail_Natural] UNIQUE ([InitialBalanceInvoiceId], [ServiceType], [Consecutive])
);


GO
ALTER TABLE [Portfolio].[InitialBalanceInvoiceDetail] NOCHECK CONSTRAINT [FK_InitialBalanceInvoiceDetail_InitialBalanceInvoice];


GO
CREATE NONCLUSTERED INDEX [IX_InitialBalanceInvoiceDetail_HeaderType]
    ON [Portfolio].[InitialBalanceInvoiceDetail]([InitialBalanceInvoiceId] ASC, [ServiceType] ASC)
    INCLUDE([Balance], [ServiceValue]) WITH (FILLFACTOR = 90);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle de factura de saldo inicial. Snapshot mínimo financiero (enfoque Hybrid SQL+Cosmos): SQL guarda valores transaccionales, Cosmos hidrata datos clínicos bajo demanda.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoiceDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador único del registro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoiceDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FK a Portfolio.InitialBalanceInvoice (header).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoiceDetail', @level2type = N'COLUMN', @level2name = N'InitialBalanceInvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo del usuario en el arreglo "usuarios[].consecutivo" del JSON RIPS. Permite distinguir items en facturas capitadas que tienen N usuarios.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoiceDetail', @level2type = N'COLUMN', @level2name = N'UserConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de servicio del JSON RIPS: 1 - Consulta (servicios.consultas[]), 2 - Procedimiento (servicios.procedimientos[]), 3 - Urgencia (servicios.urgencias[]), 4 - Recién Nacido (servicios.recienNacidos[]), 5 - Medicamento (servicios.medicamentos[]), 6 - Otro Servicio (servicios.otrosServicios[]), 7 - Hospitalización (servicios.hospitalizacion[]).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo del item dentro del arreglo de servicios.{tipo}[]. Corresponde al campo "consecutivo" del item en el JSON RIPS.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoiceDetail', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del servicio. Mapping por ServiceType: Consulta - codConsulta; Procedimiento - codProcedimiento; Medicamento - codTecnologiaSalud; Otro Servicio - codTecnologiaSalud; Urgencia/Recién Nacido/Hospitalización - NULL.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad del servicio. Mapping por ServiceType: Medicamento - cantidadMedicamento; Otro Servicio - cantidadOS; resto - 1 por defecto.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoiceDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor unitario del servicio. Mapping por ServiceType: Consulta/Procedimiento - vrServicio; Medicamento - vrUnitMedicamento; Otro Servicio - vrUnitOS; Urgencia/Recién Nacido/Hospitalización - 0 (servicios sin valor facturable directo).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoiceDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor total del servicio. Corresponde al campo "vrServicio" del item del JSON RIPS. Para servicios sin valor (Urgencia/Recién Nacido/Hospitalización) es 0. Snapshot inmutable post-confirmación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ServiceValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saldo restante del item. Inicialmente igual a ServiceValue. Decrece al aplicar notas crédito/débito tipo 6 (Factura Detallada). Mutable.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoiceDetail', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y hora de inicio de atención. Corresponde al campo "fechaInicioAtencion" del item del JSON RIPS. NULL para servicios sin esta fecha (ej. Recién Nacido usa fechaNacimiento, no se mapea aquí).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoiceDetail', @level2type = N'COLUMN', @level2name = N'AttentionStartDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoiceDetail', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoiceDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'InitialBalanceInvoiceDetail', @level2type = N'COLUMN', @level2name = N'ModificationDate';

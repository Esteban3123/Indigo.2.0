CREATE TABLE [Billing].[RIPSServiceHospitalRelation] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [InvoiceNumber]   VARCHAR (20)  NOT NULL,
    [ServiceNameJson] VARCHAR (250) NOT NULL,
    [CreationDate]    DATETIME      NOT NULL,
    [TimeStamp]       ROWVERSION    NOT NULL,
    CONSTRAINT [PK_RIPSServiceHospitalRelation] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de los servicios del Json del ministerio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSServiceHospitalRelation', @level2type = N'COLUMN', @level2name = N'ServiceNameJson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numero de factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSServiceHospitalRelation', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSServiceHospitalRelation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla que guarda la relacion de los servicios Hospitalarios(Urgencias,Hospitalizacion, Recien Nacido) de la factura con el Json Generado para evitar duplicidad en el reporte de la informacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSServiceHospitalRelation';


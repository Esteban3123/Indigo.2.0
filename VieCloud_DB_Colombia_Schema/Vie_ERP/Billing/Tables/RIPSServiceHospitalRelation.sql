CREATE TABLE [Billing].[RIPSServiceHospitalRelation] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [InvoiceNumber]   VARCHAR (20)  NOT NULL,
    [ServiceNameJson] VARCHAR (250) NOT NULL,
    [CreationDate]    DATETIME      NOT NULL,
    [TimeStamp]       ROWVERSION    NOT NULL,
    CONSTRAINT [PK_RIPSServiceHospitalRelation] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador o nombre del servicio hospitalario en formato JSON conforme a estructura ministerial (Urgencias, Hospitalización, Recién Nacido). VARCHAR(250). Referencia al consecutivo de servicio en el JSON de RIPS para evitar duplicados en reportes.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSServiceHospitalRelation', @level2type = N'COLUMN', @level2name = N'ServiceNameJson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de los servicios del Json del ministerio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSServiceHospitalRelation', @level2type = N'COLUMN', @level2name = N'ServiceNameJson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSServiceHospitalRelation', @level2type = N'COLUMN', @level2name = N'ServiceNameJson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de factura o comprobante de pago asociado. VARCHAR(20). Clave que vincula la relación con la factura original para trazabilidad en reportes RIPS y auditoría de servicios facturados.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSServiceHospitalRelation', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSServiceHospitalRelation', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSServiceHospitalRelation', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial del registro de relación servicio-factura. INT IDENTITY. Clave primaria que garantiza unicidad de la asociación servicio-JSON-factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSServiceHospitalRelation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSServiceHospitalRelation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSServiceHospitalRelation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de relación entre servicios hospitalarios (Urgencias, Hospitalización, Recién Nacido) y su representación JSON en reportes RIPS. Previene duplicidad de información en facturas al vincular el número de factura con el contenido JSON generado para el ministerio de salud.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSServiceHospitalRelation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tabla que guarda la relacion de los servicios Hospitalarios(Urgencias,Hospitalizacion, Recien Nacido) de la factura con el Json Generado para evitar duplicidad en el reporte de la informacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSServiceHospitalRelation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSServiceHospitalRelation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se registró la relación entre el servicio hospitalario y la factura en el sistema.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSServiceHospitalRelation', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSServiceHospitalRelation', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo interna del sistema para control de concurrencia y auditoría de cambios en el registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSServiceHospitalRelation', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RIPSServiceHospitalRelation', @level2type = N'COLUMN', @level2name = N'TimeStamp';

CREATE TABLE [dbo].[HCPAQORDENESD] (
    [ID]                       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCPAQORDENESC]          INT           NOT NULL,
    [CODIGOSERVICIO]           CHAR (20)     NOT NULL,
    [TIPOSERVICIO]             INT           NULL,
    [IDDESCRIPCIONRELACIONADA] INT           NULL,
    [OrderQuantity]            INT           NULL,
    [ClinicalDataRelevant]     VARCHAR (500) NULL,
    CONSTRAINT [PK_HCPAQORDENESD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPAQORDENESD_HCPAQORDENESC] FOREIGN KEY ([IDHCPAQORDENESC]) REFERENCES [dbo].[HCPAQORDENESC] ([ID])
);


GO
ALTER TABLE [dbo].[HCPAQORDENESD] NOCHECK CONSTRAINT [FK_HCPAQORDENESD_HCPAQORDENESC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo para almacenar los datos clínicos relevantes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'ClinicalDataRelevant';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contiene la cantidad registrada para la orden en page de ordenes del formulario de Paquetes de ordenes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'OrderQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'11-10-2024: columna que se pone obsoleta, puesto que este dato debe tomarlo siempre como esta la parametrización.



1 - Laboratorios  2 - Imagenes  3 - Procedimiento Qx  4 - Procedimiento NoQx  5 - Patologias  6 - Medicamentos  7 - Interconsultas  8 - Insumos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'TIPOSERVICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'CODIGOSERVICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla cabecera ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'IDHCPAQORDENESC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAQORDENESD', @level2type = N'COLUMN', @level2name = N'ID';

